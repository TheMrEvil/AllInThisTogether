using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace System.Data.ProviderBase
{
	// Token: 0x0200035A RID: 858
	internal sealed class DbConnectionPool
	{
		// Token: 0x06002763 RID: 10083 RVA: 0x000AF080 File Offset: 0x000AD280
		internal DbConnectionPool(DbConnectionFactory connectionFactory, DbConnectionPoolGroup connectionPoolGroup, DbConnectionPoolIdentity identity, DbConnectionPoolProviderInfo connectionPoolProviderInfo)
		{
			if (identity != null && identity.IsRestricted)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.AttemptingToPoolOnRestrictedToken);
			}
			this._state = DbConnectionPool.State.Initializing;
			Random obj = DbConnectionPool.s_random;
			lock (obj)
			{
				this._cleanupWait = DbConnectionPool.s_random.Next(12, 24) * 10 * 1000;
			}
			this._connectionFactory = connectionFactory;
			this._connectionPoolGroup = connectionPoolGroup;
			this._connectionPoolGroupOptions = connectionPoolGroup.PoolGroupOptions;
			this._connectionPoolProviderInfo = connectionPoolProviderInfo;
			this._identity = identity;
			this._waitHandles = new DbConnectionPool.PoolWaitHandles();
			this._errorWait = 5000;
			this._errorTimer = null;
			this._objectList = new List<DbConnectionInternal>(this.MaxPoolSize);
			this._transactedConnectionPool = new DbConnectionPool.TransactedConnectionPool(this);
			this._poolCreateRequest = new WaitCallback(this.PoolCreateRequest);
			this._state = DbConnectionPool.State.Running;
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06002764 RID: 10084 RVA: 0x000AF194 File Offset: 0x000AD394
		private int CreationTimeout
		{
			get
			{
				return this.PoolGroupOptions.CreationTimeout;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06002765 RID: 10085 RVA: 0x000AF1A1 File Offset: 0x000AD3A1
		internal int Count
		{
			get
			{
				return this._totalObjects;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06002766 RID: 10086 RVA: 0x000AF1A9 File Offset: 0x000AD3A9
		internal DbConnectionFactory ConnectionFactory
		{
			get
			{
				return this._connectionFactory;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06002767 RID: 10087 RVA: 0x000AF1B1 File Offset: 0x000AD3B1
		internal bool ErrorOccurred
		{
			get
			{
				return this._errorOccurred;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06002768 RID: 10088 RVA: 0x000AF1BB File Offset: 0x000AD3BB
		private bool HasTransactionAffinity
		{
			get
			{
				return this.PoolGroupOptions.HasTransactionAffinity;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06002769 RID: 10089 RVA: 0x000AF1C8 File Offset: 0x000AD3C8
		internal TimeSpan LoadBalanceTimeout
		{
			get
			{
				return this.PoolGroupOptions.LoadBalanceTimeout;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x0600276A RID: 10090 RVA: 0x000AF1D8 File Offset: 0x000AD3D8
		private bool NeedToReplenish
		{
			get
			{
				if (DbConnectionPool.State.Running != this._state)
				{
					return false;
				}
				int count = this.Count;
				if (count >= this.MaxPoolSize)
				{
					return false;
				}
				if (count < this.MinPoolSize)
				{
					return true;
				}
				int num = this._stackNew.Count + this._stackOld.Count;
				int waitCount = this._waitCount;
				return num < waitCount || (num == waitCount && count > 1);
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x0600276B RID: 10091 RVA: 0x000AF23C File Offset: 0x000AD43C
		internal DbConnectionPoolIdentity Identity
		{
			get
			{
				return this._identity;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x0600276C RID: 10092 RVA: 0x000AF244 File Offset: 0x000AD444
		internal bool IsRunning
		{
			get
			{
				return DbConnectionPool.State.Running == this._state;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x0600276D RID: 10093 RVA: 0x000AF24F File Offset: 0x000AD44F
		private int MaxPoolSize
		{
			get
			{
				return this.PoolGroupOptions.MaxPoolSize;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x0600276E RID: 10094 RVA: 0x000AF25C File Offset: 0x000AD45C
		private int MinPoolSize
		{
			get
			{
				return this.PoolGroupOptions.MinPoolSize;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x0600276F RID: 10095 RVA: 0x000AF269 File Offset: 0x000AD469
		internal DbConnectionPoolGroup PoolGroup
		{
			get
			{
				return this._connectionPoolGroup;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06002770 RID: 10096 RVA: 0x000AF271 File Offset: 0x000AD471
		internal DbConnectionPoolGroupOptions PoolGroupOptions
		{
			get
			{
				return this._connectionPoolGroupOptions;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06002771 RID: 10097 RVA: 0x000AF279 File Offset: 0x000AD479
		internal DbConnectionPoolProviderInfo ProviderInfo
		{
			get
			{
				return this._connectionPoolProviderInfo;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06002772 RID: 10098 RVA: 0x000AF281 File Offset: 0x000AD481
		internal bool UseLoadBalancing
		{
			get
			{
				return this.PoolGroupOptions.UseLoadBalancing;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06002773 RID: 10099 RVA: 0x000AF28E File Offset: 0x000AD48E
		private bool UsingIntegrateSecurity
		{
			get
			{
				return this._identity != null && DbConnectionPoolIdentity.NoIdentity != this._identity;
			}
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x000AF2AC File Offset: 0x000AD4AC
		private void CleanupCallback(object state)
		{
			while (this.Count > this.MinPoolSize && this._waitHandles.PoolSemaphore.WaitOne(0))
			{
				DbConnectionInternal dbConnectionInternal;
				if (!this._stackOld.TryPop(out dbConnectionInternal))
				{
					this._waitHandles.PoolSemaphore.Release(1);
					break;
				}
				bool flag = true;
				DbConnectionInternal obj = dbConnectionInternal;
				lock (obj)
				{
					if (dbConnectionInternal.IsTransactionRoot)
					{
						flag = false;
					}
				}
				if (flag)
				{
					this.DestroyObject(dbConnectionInternal);
				}
				else
				{
					dbConnectionInternal.SetInStasis();
				}
			}
			if (this._waitHandles.PoolSemaphore.WaitOne(0))
			{
				DbConnectionInternal item;
				while (this._stackNew.TryPop(out item))
				{
					this._stackOld.Push(item);
				}
				this._waitHandles.PoolSemaphore.Release(1);
			}
			this.QueuePoolCreateRequest();
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x000AF390 File Offset: 0x000AD590
		internal void Clear()
		{
			List<DbConnectionInternal> objectList = this._objectList;
			DbConnectionInternal dbConnectionInternal;
			lock (objectList)
			{
				int count = this._objectList.Count;
				for (int i = 0; i < count; i++)
				{
					dbConnectionInternal = this._objectList[i];
					if (dbConnectionInternal != null)
					{
						dbConnectionInternal.DoNotPoolThisConnection();
					}
				}
				goto IL_57;
			}
			IL_50:
			this.DestroyObject(dbConnectionInternal);
			IL_57:
			if (!this._stackNew.TryPop(out dbConnectionInternal))
			{
				while (this._stackOld.TryPop(out dbConnectionInternal))
				{
					this.DestroyObject(dbConnectionInternal);
				}
				this.ReclaimEmancipatedObjects();
				return;
			}
			goto IL_50;
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x000AF434 File Offset: 0x000AD634
		private Timer CreateCleanupTimer()
		{
			return ADP.UnsafeCreateTimer(new TimerCallback(this.CleanupCallback), null, this._cleanupWait, this._cleanupWait);
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x000AF454 File Offset: 0x000AD654
		private DbConnectionInternal CreateObject(DbConnection owningObject, DbConnectionOptions userOptions, DbConnectionInternal oldConnection)
		{
			DbConnectionInternal dbConnectionInternal = null;
			try
			{
				dbConnectionInternal = this._connectionFactory.CreatePooledConnection(this, owningObject, this._connectionPoolGroup.ConnectionOptions, this._connectionPoolGroup.PoolKey, userOptions);
				if (dbConnectionInternal == null)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.CreateObjectReturnedNull);
				}
				if (!dbConnectionInternal.CanBePooled)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.NewObjectCannotBePooled);
				}
				dbConnectionInternal.PrePush(null);
				List<DbConnectionInternal> objectList = this._objectList;
				lock (objectList)
				{
					if (oldConnection != null && oldConnection.Pool == this)
					{
						this._objectList.Remove(oldConnection);
					}
					this._objectList.Add(dbConnectionInternal);
					this._totalObjects = this._objectList.Count;
				}
				if (oldConnection != null)
				{
					DbConnectionPool pool = oldConnection.Pool;
					if (pool != null && pool != this)
					{
						objectList = pool._objectList;
						lock (objectList)
						{
							pool._objectList.Remove(oldConnection);
							pool._totalObjects = pool._objectList.Count;
						}
					}
				}
				this._errorWait = 5000;
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				dbConnectionInternal = null;
				this._resError = ex;
				Timer timer = new Timer(new TimerCallback(this.ErrorCallback), null, -1, -1);
				try
				{
				}
				finally
				{
					this._waitHandles.ErrorEvent.Set();
					this._errorOccurred = true;
					this._errorTimer = timer;
					timer.Change(this._errorWait, this._errorWait);
				}
				if (30000 < this._errorWait)
				{
					this._errorWait = 60000;
				}
				else
				{
					this._errorWait *= 2;
				}
				throw;
			}
			return dbConnectionInternal;
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x000AF61C File Offset: 0x000AD81C
		private void DeactivateObject(DbConnectionInternal obj)
		{
			obj.DeactivateConnection();
			bool flag = false;
			bool flag2 = false;
			if (obj.IsConnectionDoomed)
			{
				flag2 = true;
			}
			else
			{
				lock (obj)
				{
					if (this._state == DbConnectionPool.State.ShuttingDown)
					{
						if (obj.IsTransactionRoot)
						{
							obj.SetInStasis();
						}
						else
						{
							flag2 = true;
						}
					}
					else if (obj.IsNonPoolableTransactionRoot)
					{
						obj.SetInStasis();
					}
					else if (obj.CanBePooled)
					{
						Transaction enlistedTransaction = obj.EnlistedTransaction;
						if (null != enlistedTransaction)
						{
							this._transactedConnectionPool.PutTransactedObject(enlistedTransaction, obj);
						}
						else
						{
							flag = true;
						}
					}
					else if (obj.IsTransactionRoot && !obj.IsConnectionDoomed)
					{
						obj.SetInStasis();
					}
					else
					{
						flag2 = true;
					}
				}
			}
			if (flag)
			{
				this.PutNewObject(obj);
				return;
			}
			if (flag2)
			{
				this.DestroyObject(obj);
				this.QueuePoolCreateRequest();
			}
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x000AF6FC File Offset: 0x000AD8FC
		internal void DestroyObject(DbConnectionInternal obj)
		{
			if (!obj.IsTxRootWaitingForTxEnd)
			{
				List<DbConnectionInternal> objectList = this._objectList;
				lock (objectList)
				{
					this._objectList.Remove(obj);
					this._totalObjects = this._objectList.Count;
				}
				obj.Dispose();
			}
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x000AF764 File Offset: 0x000AD964
		private void ErrorCallback(object state)
		{
			this._errorOccurred = false;
			this._waitHandles.ErrorEvent.Reset();
			Timer errorTimer = this._errorTimer;
			this._errorTimer = null;
			if (errorTimer != null)
			{
				errorTimer.Dispose();
			}
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x000AF7A4 File Offset: 0x000AD9A4
		private Exception TryCloneCachedException()
		{
			if (this._resError == null)
			{
				return null;
			}
			SqlException ex = this._resError as SqlException;
			if (ex != null)
			{
				return ex.InternalClone();
			}
			return this._resError;
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x000AF7D8 File Offset: 0x000AD9D8
		private void WaitForPendingOpen()
		{
			DbConnectionPool.PendingGetConnection pendingGetConnection;
			do
			{
				bool flag = false;
				try
				{
					try
					{
					}
					finally
					{
						flag = (Interlocked.CompareExchange(ref this._pendingOpensWaiting, 1, 0) == 0);
					}
					if (!flag)
					{
						break;
					}
					while (this._pendingOpens.TryDequeue(out pendingGetConnection))
					{
						if (!pendingGetConnection.Completion.Task.IsCompleted)
						{
							uint waitForMultipleObjectsTimeout;
							if (pendingGetConnection.DueTime == -1L)
							{
								waitForMultipleObjectsTimeout = uint.MaxValue;
							}
							else
							{
								waitForMultipleObjectsTimeout = (uint)Math.Max(ADP.TimerRemainingMilliseconds(pendingGetConnection.DueTime), 0L);
							}
							DbConnectionInternal dbConnectionInternal = null;
							bool flag2 = false;
							Exception ex = null;
							try
							{
								bool allowCreate = true;
								bool onlyOneCheckConnection = false;
								ADP.SetCurrentTransaction(pendingGetConnection.Completion.Task.AsyncState as Transaction);
								flag2 = !this.TryGetConnection(pendingGetConnection.Owner, waitForMultipleObjectsTimeout, allowCreate, onlyOneCheckConnection, pendingGetConnection.UserOptions, out dbConnectionInternal);
							}
							catch (Exception ex)
							{
							}
							if (ex != null)
							{
								pendingGetConnection.Completion.TrySetException(ex);
							}
							else if (flag2)
							{
								pendingGetConnection.Completion.TrySetException(ADP.ExceptionWithStackTrace(ADP.PooledOpenTimeout()));
							}
							else if (!pendingGetConnection.Completion.TrySetResult(dbConnectionInternal))
							{
								this.PutObject(dbConnectionInternal, pendingGetConnection.Owner);
							}
						}
					}
				}
				finally
				{
					if (flag)
					{
						Interlocked.Exchange(ref this._pendingOpensWaiting, 0);
					}
				}
			}
			while (this._pendingOpens.TryPeek(out pendingGetConnection));
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x000AF958 File Offset: 0x000ADB58
		internal bool TryGetConnection(DbConnection owningObject, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions, out DbConnectionInternal connection)
		{
			uint num = 0U;
			bool allowCreate = false;
			if (retry == null)
			{
				num = (uint)this.CreationTimeout;
				if (num == 0U)
				{
					num = uint.MaxValue;
				}
				allowCreate = true;
			}
			if (this._state != DbConnectionPool.State.Running)
			{
				connection = null;
				return true;
			}
			bool onlyOneCheckConnection = true;
			if (this.TryGetConnection(owningObject, num, allowCreate, onlyOneCheckConnection, userOptions, out connection))
			{
				return true;
			}
			if (retry == null)
			{
				return true;
			}
			DbConnectionPool.PendingGetConnection item = new DbConnectionPool.PendingGetConnection((this.CreationTimeout == 0) ? -1L : (ADP.TimerCurrent() + ADP.TimerFromSeconds(this.CreationTimeout / 1000)), owningObject, retry, userOptions);
			this._pendingOpens.Enqueue(item);
			if (this._pendingOpensWaiting == 0)
			{
				new Thread(new ThreadStart(this.WaitForPendingOpen))
				{
					IsBackground = true
				}.Start();
			}
			connection = null;
			return false;
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x000AFA04 File Offset: 0x000ADC04
		private bool TryGetConnection(DbConnection owningObject, uint waitForMultipleObjectsTimeout, bool allowCreate, bool onlyOneCheckConnection, DbConnectionOptions userOptions, out DbConnectionInternal connection)
		{
			DbConnectionInternal dbConnectionInternal = null;
			Transaction transaction = null;
			if (this.HasTransactionAffinity)
			{
				dbConnectionInternal = this.GetFromTransactedPool(out transaction);
			}
			if (dbConnectionInternal == null)
			{
				Interlocked.Increment(ref this._waitCount);
				for (;;)
				{
					int num = 3;
					try
					{
						try
						{
						}
						finally
						{
							num = WaitHandle.WaitAny(this._waitHandles.GetHandles(allowCreate), (int)waitForMultipleObjectsTimeout);
						}
						switch (num)
						{
						case 0:
							Interlocked.Decrement(ref this._waitCount);
							dbConnectionInternal = this.GetFromGeneralPool();
							if (dbConnectionInternal != null && !dbConnectionInternal.IsConnectionAlive(false))
							{
								this.DestroyObject(dbConnectionInternal);
								dbConnectionInternal = null;
								if (onlyOneCheckConnection)
								{
									if (this._waitHandles.CreationSemaphore.WaitOne((int)waitForMultipleObjectsTimeout))
									{
										try
										{
											dbConnectionInternal = this.UserCreateRequest(owningObject, userOptions, null);
											break;
										}
										finally
										{
											this._waitHandles.CreationSemaphore.Release(1);
										}
									}
									connection = null;
									return false;
								}
							}
							break;
						case 1:
							Interlocked.Decrement(ref this._waitCount);
							throw this.TryCloneCachedException();
						case 2:
							try
							{
								dbConnectionInternal = this.UserCreateRequest(owningObject, userOptions, null);
							}
							catch
							{
								if (dbConnectionInternal == null)
								{
									Interlocked.Decrement(ref this._waitCount);
								}
								throw;
							}
							finally
							{
								if (dbConnectionInternal != null)
								{
									Interlocked.Decrement(ref this._waitCount);
								}
							}
							if (dbConnectionInternal == null && this.Count >= this.MaxPoolSize && this.MaxPoolSize != 0 && !this.ReclaimEmancipatedObjects())
							{
								allowCreate = false;
							}
							break;
						default:
							if (num == 258)
							{
								Interlocked.Decrement(ref this._waitCount);
								connection = null;
								return false;
							}
							Interlocked.Decrement(ref this._waitCount);
							throw ADP.InternalError(ADP.InternalErrorCode.UnexpectedWaitAnyResult);
						}
					}
					finally
					{
						if (2 == num)
						{
							this._waitHandles.CreationSemaphore.Release(1);
						}
					}
					if (dbConnectionInternal != null)
					{
						goto IL_185;
					}
				}
				bool result;
				return result;
			}
			IL_185:
			if (dbConnectionInternal != null)
			{
				this.PrepareConnection(owningObject, dbConnectionInternal, transaction);
			}
			connection = dbConnectionInternal;
			return true;
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x000AFC28 File Offset: 0x000ADE28
		private void PrepareConnection(DbConnection owningObject, DbConnectionInternal obj, Transaction transaction)
		{
			lock (obj)
			{
				obj.PostPop(owningObject);
			}
			try
			{
				obj.ActivateConnection(transaction);
			}
			catch
			{
				this.PutObject(obj, owningObject);
				throw;
			}
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x000AFC88 File Offset: 0x000ADE88
		internal DbConnectionInternal ReplaceConnection(DbConnection owningObject, DbConnectionOptions userOptions, DbConnectionInternal oldConnection)
		{
			DbConnectionInternal dbConnectionInternal = this.UserCreateRequest(owningObject, userOptions, oldConnection);
			if (dbConnectionInternal != null)
			{
				this.PrepareConnection(owningObject, dbConnectionInternal, oldConnection.EnlistedTransaction);
				oldConnection.PrepareForReplaceConnection();
				oldConnection.DeactivateConnection();
				oldConnection.Dispose();
			}
			return dbConnectionInternal;
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x000AFCC4 File Offset: 0x000ADEC4
		private DbConnectionInternal GetFromGeneralPool()
		{
			DbConnectionInternal result = null;
			if (!this._stackNew.TryPop(out result) && !this._stackOld.TryPop(out result))
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x000AFCF8 File Offset: 0x000ADEF8
		private DbConnectionInternal GetFromTransactedPool(out Transaction transaction)
		{
			transaction = ADP.GetCurrentTransaction();
			DbConnectionInternal dbConnectionInternal = null;
			if (null != transaction && this._transactedConnectionPool != null)
			{
				dbConnectionInternal = this._transactedConnectionPool.GetTransactedObject(transaction);
				if (dbConnectionInternal != null)
				{
					if (dbConnectionInternal.IsTransactionRoot)
					{
						try
						{
							dbConnectionInternal.IsConnectionAlive(true);
							return dbConnectionInternal;
						}
						catch
						{
							this.DestroyObject(dbConnectionInternal);
							throw;
						}
					}
					if (!dbConnectionInternal.IsConnectionAlive(false))
					{
						this.DestroyObject(dbConnectionInternal);
						dbConnectionInternal = null;
					}
				}
			}
			return dbConnectionInternal;
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x000AFD70 File Offset: 0x000ADF70
		private void PoolCreateRequest(object state)
		{
			if (DbConnectionPool.State.Running == this._state)
			{
				if (!this._pendingOpens.IsEmpty && this._pendingOpensWaiting == 0)
				{
					new Thread(new ThreadStart(this.WaitForPendingOpen))
					{
						IsBackground = true
					}.Start();
				}
				this.ReclaimEmancipatedObjects();
				if (!this.ErrorOccurred && this.NeedToReplenish)
				{
					if (this.UsingIntegrateSecurity && !this._identity.Equals(DbConnectionPoolIdentity.GetCurrent()))
					{
						return;
					}
					int num = 3;
					try
					{
						try
						{
						}
						finally
						{
							num = WaitHandle.WaitAny(this._waitHandles.GetHandles(true), this.CreationTimeout);
						}
						if (2 == num)
						{
							if (!this.ErrorOccurred)
							{
								while (this.NeedToReplenish)
								{
									DbConnectionInternal dbConnectionInternal;
									try
									{
										dbConnectionInternal = this.CreateObject(null, null, null);
									}
									catch
									{
										break;
									}
									if (dbConnectionInternal == null)
									{
										break;
									}
									this.PutNewObject(dbConnectionInternal);
								}
							}
						}
						else if (258 == num)
						{
							this.QueuePoolCreateRequest();
						}
					}
					finally
					{
						if (2 == num)
						{
							this._waitHandles.CreationSemaphore.Release(1);
						}
					}
				}
			}
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x000AFE94 File Offset: 0x000AE094
		internal void PutNewObject(DbConnectionInternal obj)
		{
			this._stackNew.Push(obj);
			this._waitHandles.PoolSemaphore.Release(1);
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x000AFEB4 File Offset: 0x000AE0B4
		internal void PutObject(DbConnectionInternal obj, object owningObject)
		{
			lock (obj)
			{
				obj.PrePush(owningObject);
			}
			this.DeactivateObject(obj);
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x000AFEF8 File Offset: 0x000AE0F8
		internal void PutObjectFromTransactedPool(DbConnectionInternal obj)
		{
			if (this._state == DbConnectionPool.State.Running && obj.CanBePooled)
			{
				this.PutNewObject(obj);
				return;
			}
			this.DestroyObject(obj);
			this.QueuePoolCreateRequest();
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x000AFF20 File Offset: 0x000AE120
		private void QueuePoolCreateRequest()
		{
			if (DbConnectionPool.State.Running == this._state)
			{
				ThreadPool.QueueUserWorkItem(this._poolCreateRequest);
			}
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x000AFF38 File Offset: 0x000AE138
		private bool ReclaimEmancipatedObjects()
		{
			bool result = false;
			List<DbConnectionInternal> list = new List<DbConnectionInternal>();
			List<DbConnectionInternal> objectList = this._objectList;
			int count;
			lock (objectList)
			{
				count = this._objectList.Count;
				for (int i = 0; i < count; i++)
				{
					DbConnectionInternal dbConnectionInternal = this._objectList[i];
					if (dbConnectionInternal != null)
					{
						bool flag2 = false;
						try
						{
							Monitor.TryEnter(dbConnectionInternal, ref flag2);
							if (flag2 && dbConnectionInternal.IsEmancipated)
							{
								dbConnectionInternal.PrePush(null);
								list.Add(dbConnectionInternal);
							}
						}
						finally
						{
							if (flag2)
							{
								Monitor.Exit(dbConnectionInternal);
							}
						}
					}
				}
			}
			count = list.Count;
			for (int j = 0; j < count; j++)
			{
				DbConnectionInternal dbConnectionInternal2 = list[j];
				result = true;
				dbConnectionInternal2.DetachCurrentTransactionIfEnded();
				this.DeactivateObject(dbConnectionInternal2);
			}
			return result;
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x000B0024 File Offset: 0x000AE224
		internal void Startup()
		{
			this._cleanupTimer = this.CreateCleanupTimer();
			if (this.NeedToReplenish)
			{
				this.QueuePoolCreateRequest();
			}
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x000B0040 File Offset: 0x000AE240
		internal void Shutdown()
		{
			this._state = DbConnectionPool.State.ShuttingDown;
			Timer cleanupTimer = this._cleanupTimer;
			this._cleanupTimer = null;
			if (cleanupTimer != null)
			{
				cleanupTimer.Dispose();
			}
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x000B006C File Offset: 0x000AE26C
		internal void TransactionEnded(Transaction transaction, DbConnectionInternal transactedObject)
		{
			DbConnectionPool.TransactedConnectionPool transactedConnectionPool = this._transactedConnectionPool;
			if (transactedConnectionPool != null)
			{
				transactedConnectionPool.TransactionEnded(transaction, transactedObject);
			}
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x000B008C File Offset: 0x000AE28C
		private DbConnectionInternal UserCreateRequest(DbConnection owningObject, DbConnectionOptions userOptions, DbConnectionInternal oldConnection = null)
		{
			DbConnectionInternal result = null;
			if (this.ErrorOccurred)
			{
				throw this.TryCloneCachedException();
			}
			if ((oldConnection != null || this.Count < this.MaxPoolSize || this.MaxPoolSize == 0) && (oldConnection != null || (this.Count & 1) == 1 || !this.ReclaimEmancipatedObjects()))
			{
				result = this.CreateObject(owningObject, userOptions, oldConnection);
			}
			return result;
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x000B00E4 File Offset: 0x000AE2E4
		// Note: this type is marked as 'beforefieldinit'.
		static DbConnectionPool()
		{
		}

		// Token: 0x04001995 RID: 6549
		private const int MAX_Q_SIZE = 1048576;

		// Token: 0x04001996 RID: 6550
		private const int SEMAPHORE_HANDLE = 0;

		// Token: 0x04001997 RID: 6551
		private const int ERROR_HANDLE = 1;

		// Token: 0x04001998 RID: 6552
		private const int CREATION_HANDLE = 2;

		// Token: 0x04001999 RID: 6553
		private const int BOGUS_HANDLE = 3;

		// Token: 0x0400199A RID: 6554
		private const int ERROR_WAIT_DEFAULT = 5000;

		// Token: 0x0400199B RID: 6555
		private static readonly Random s_random = new Random(5101977);

		// Token: 0x0400199C RID: 6556
		private readonly int _cleanupWait;

		// Token: 0x0400199D RID: 6557
		private readonly DbConnectionPoolIdentity _identity;

		// Token: 0x0400199E RID: 6558
		private readonly DbConnectionFactory _connectionFactory;

		// Token: 0x0400199F RID: 6559
		private readonly DbConnectionPoolGroup _connectionPoolGroup;

		// Token: 0x040019A0 RID: 6560
		private readonly DbConnectionPoolGroupOptions _connectionPoolGroupOptions;

		// Token: 0x040019A1 RID: 6561
		private DbConnectionPoolProviderInfo _connectionPoolProviderInfo;

		// Token: 0x040019A2 RID: 6562
		private DbConnectionPool.State _state;

		// Token: 0x040019A3 RID: 6563
		private readonly ConcurrentStack<DbConnectionInternal> _stackOld = new ConcurrentStack<DbConnectionInternal>();

		// Token: 0x040019A4 RID: 6564
		private readonly ConcurrentStack<DbConnectionInternal> _stackNew = new ConcurrentStack<DbConnectionInternal>();

		// Token: 0x040019A5 RID: 6565
		private readonly ConcurrentQueue<DbConnectionPool.PendingGetConnection> _pendingOpens = new ConcurrentQueue<DbConnectionPool.PendingGetConnection>();

		// Token: 0x040019A6 RID: 6566
		private int _pendingOpensWaiting;

		// Token: 0x040019A7 RID: 6567
		private readonly WaitCallback _poolCreateRequest;

		// Token: 0x040019A8 RID: 6568
		private int _waitCount;

		// Token: 0x040019A9 RID: 6569
		private readonly DbConnectionPool.PoolWaitHandles _waitHandles;

		// Token: 0x040019AA RID: 6570
		private Exception _resError;

		// Token: 0x040019AB RID: 6571
		private volatile bool _errorOccurred;

		// Token: 0x040019AC RID: 6572
		private int _errorWait;

		// Token: 0x040019AD RID: 6573
		private Timer _errorTimer;

		// Token: 0x040019AE RID: 6574
		private Timer _cleanupTimer;

		// Token: 0x040019AF RID: 6575
		private readonly DbConnectionPool.TransactedConnectionPool _transactedConnectionPool;

		// Token: 0x040019B0 RID: 6576
		private readonly List<DbConnectionInternal> _objectList;

		// Token: 0x040019B1 RID: 6577
		private int _totalObjects;

		// Token: 0x0200035B RID: 859
		private enum State
		{
			// Token: 0x040019B3 RID: 6579
			Initializing,
			// Token: 0x040019B4 RID: 6580
			Running,
			// Token: 0x040019B5 RID: 6581
			ShuttingDown
		}

		// Token: 0x0200035C RID: 860
		private sealed class TransactedConnectionList : List<DbConnectionInternal>
		{
			// Token: 0x0600278E RID: 10126 RVA: 0x000B00F5 File Offset: 0x000AE2F5
			internal TransactedConnectionList(int initialAllocation, Transaction tx) : base(initialAllocation)
			{
				this._transaction = tx;
			}

			// Token: 0x0600278F RID: 10127 RVA: 0x000B0105 File Offset: 0x000AE305
			internal void Dispose()
			{
				if (null != this._transaction)
				{
					this._transaction.Dispose();
				}
			}

			// Token: 0x040019B6 RID: 6582
			private Transaction _transaction;
		}

		// Token: 0x0200035D RID: 861
		private sealed class PendingGetConnection
		{
			// Token: 0x06002790 RID: 10128 RVA: 0x000B0120 File Offset: 0x000AE320
			public PendingGetConnection(long dueTime, DbConnection owner, TaskCompletionSource<DbConnectionInternal> completion, DbConnectionOptions userOptions)
			{
				this.DueTime = dueTime;
				this.Owner = owner;
				this.Completion = completion;
			}

			// Token: 0x170006BD RID: 1725
			// (get) Token: 0x06002791 RID: 10129 RVA: 0x000B013D File Offset: 0x000AE33D
			// (set) Token: 0x06002792 RID: 10130 RVA: 0x000B0145 File Offset: 0x000AE345
			public long DueTime
			{
				[CompilerGenerated]
				get
				{
					return this.<DueTime>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<DueTime>k__BackingField = value;
				}
			}

			// Token: 0x170006BE RID: 1726
			// (get) Token: 0x06002793 RID: 10131 RVA: 0x000B014E File Offset: 0x000AE34E
			// (set) Token: 0x06002794 RID: 10132 RVA: 0x000B0156 File Offset: 0x000AE356
			public DbConnection Owner
			{
				[CompilerGenerated]
				get
				{
					return this.<Owner>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<Owner>k__BackingField = value;
				}
			}

			// Token: 0x170006BF RID: 1727
			// (get) Token: 0x06002795 RID: 10133 RVA: 0x000B015F File Offset: 0x000AE35F
			// (set) Token: 0x06002796 RID: 10134 RVA: 0x000B0167 File Offset: 0x000AE367
			public TaskCompletionSource<DbConnectionInternal> Completion
			{
				[CompilerGenerated]
				get
				{
					return this.<Completion>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<Completion>k__BackingField = value;
				}
			}

			// Token: 0x170006C0 RID: 1728
			// (get) Token: 0x06002797 RID: 10135 RVA: 0x000B0170 File Offset: 0x000AE370
			// (set) Token: 0x06002798 RID: 10136 RVA: 0x000B0178 File Offset: 0x000AE378
			public DbConnectionOptions UserOptions
			{
				[CompilerGenerated]
				get
				{
					return this.<UserOptions>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<UserOptions>k__BackingField = value;
				}
			}

			// Token: 0x040019B7 RID: 6583
			[CompilerGenerated]
			private long <DueTime>k__BackingField;

			// Token: 0x040019B8 RID: 6584
			[CompilerGenerated]
			private DbConnection <Owner>k__BackingField;

			// Token: 0x040019B9 RID: 6585
			[CompilerGenerated]
			private TaskCompletionSource<DbConnectionInternal> <Completion>k__BackingField;

			// Token: 0x040019BA RID: 6586
			[CompilerGenerated]
			private DbConnectionOptions <UserOptions>k__BackingField;
		}

		// Token: 0x0200035E RID: 862
		private sealed class TransactedConnectionPool
		{
			// Token: 0x06002799 RID: 10137 RVA: 0x000B0181 File Offset: 0x000AE381
			internal TransactedConnectionPool(DbConnectionPool pool)
			{
				this._pool = pool;
				this._transactedCxns = new Dictionary<Transaction, DbConnectionPool.TransactedConnectionList>();
			}

			// Token: 0x170006C1 RID: 1729
			// (get) Token: 0x0600279A RID: 10138 RVA: 0x000B01AB File Offset: 0x000AE3AB
			internal int ObjectID
			{
				get
				{
					return this._objectID;
				}
			}

			// Token: 0x170006C2 RID: 1730
			// (get) Token: 0x0600279B RID: 10139 RVA: 0x000B01B3 File Offset: 0x000AE3B3
			internal DbConnectionPool Pool
			{
				get
				{
					return this._pool;
				}
			}

			// Token: 0x0600279C RID: 10140 RVA: 0x000B01BC File Offset: 0x000AE3BC
			internal DbConnectionInternal GetTransactedObject(Transaction transaction)
			{
				DbConnectionInternal result = null;
				bool flag = false;
				Dictionary<Transaction, DbConnectionPool.TransactedConnectionList> transactedCxns = this._transactedCxns;
				DbConnectionPool.TransactedConnectionList transactedConnectionList;
				lock (transactedCxns)
				{
					flag = this._transactedCxns.TryGetValue(transaction, out transactedConnectionList);
				}
				if (flag)
				{
					DbConnectionPool.TransactedConnectionList obj = transactedConnectionList;
					lock (obj)
					{
						int num = transactedConnectionList.Count - 1;
						if (0 <= num)
						{
							result = transactedConnectionList[num];
							transactedConnectionList.RemoveAt(num);
						}
					}
				}
				return result;
			}

			// Token: 0x0600279D RID: 10141 RVA: 0x000B0258 File Offset: 0x000AE458
			internal void PutTransactedObject(Transaction transaction, DbConnectionInternal transactedObject)
			{
				bool flag = false;
				Dictionary<Transaction, DbConnectionPool.TransactedConnectionList> transactedCxns = this._transactedCxns;
				lock (transactedCxns)
				{
					DbConnectionPool.TransactedConnectionList transactedConnectionList;
					if (flag = this._transactedCxns.TryGetValue(transaction, out transactedConnectionList))
					{
						DbConnectionPool.TransactedConnectionList obj = transactedConnectionList;
						lock (obj)
						{
							transactedConnectionList.Add(transactedObject);
						}
					}
				}
				if (!flag)
				{
					Transaction transaction2 = null;
					DbConnectionPool.TransactedConnectionList transactedConnectionList2 = null;
					try
					{
						transaction2 = transaction.Clone();
						transactedConnectionList2 = new DbConnectionPool.TransactedConnectionList(2, transaction2);
						transactedCxns = this._transactedCxns;
						lock (transactedCxns)
						{
							DbConnectionPool.TransactedConnectionList transactedConnectionList;
							if (flag = this._transactedCxns.TryGetValue(transaction, out transactedConnectionList))
							{
								DbConnectionPool.TransactedConnectionList obj = transactedConnectionList;
								lock (obj)
								{
									transactedConnectionList.Add(transactedObject);
									return;
								}
							}
							transactedConnectionList2.Add(transactedObject);
							this._transactedCxns.Add(transaction2, transactedConnectionList2);
							transaction2 = null;
						}
					}
					finally
					{
						if (null != transaction2)
						{
							if (transactedConnectionList2 != null)
							{
								transactedConnectionList2.Dispose();
							}
							else
							{
								transaction2.Dispose();
							}
						}
					}
				}
			}

			// Token: 0x0600279E RID: 10142 RVA: 0x000B03A4 File Offset: 0x000AE5A4
			internal void TransactionEnded(Transaction transaction, DbConnectionInternal transactedObject)
			{
				int num = -1;
				Dictionary<Transaction, DbConnectionPool.TransactedConnectionList> transactedCxns = this._transactedCxns;
				lock (transactedCxns)
				{
					DbConnectionPool.TransactedConnectionList transactedConnectionList;
					if (this._transactedCxns.TryGetValue(transaction, out transactedConnectionList))
					{
						bool flag2 = false;
						DbConnectionPool.TransactedConnectionList obj = transactedConnectionList;
						lock (obj)
						{
							num = transactedConnectionList.IndexOf(transactedObject);
							if (num >= 0)
							{
								transactedConnectionList.RemoveAt(num);
							}
							if (0 >= transactedConnectionList.Count)
							{
								this._transactedCxns.Remove(transaction);
								flag2 = true;
							}
						}
						if (flag2)
						{
							transactedConnectionList.Dispose();
						}
					}
				}
				if (0 <= num)
				{
					this.Pool.PutObjectFromTransactedPool(transactedObject);
				}
			}

			// Token: 0x040019BB RID: 6587
			private Dictionary<Transaction, DbConnectionPool.TransactedConnectionList> _transactedCxns;

			// Token: 0x040019BC RID: 6588
			private DbConnectionPool _pool;

			// Token: 0x040019BD RID: 6589
			private static int _objectTypeCount;

			// Token: 0x040019BE RID: 6590
			internal readonly int _objectID = Interlocked.Increment(ref DbConnectionPool.TransactedConnectionPool._objectTypeCount);
		}

		// Token: 0x0200035F RID: 863
		private sealed class PoolWaitHandles
		{
			// Token: 0x0600279F RID: 10143 RVA: 0x000B0464 File Offset: 0x000AE664
			internal PoolWaitHandles()
			{
				this._poolSemaphore = new Semaphore(0, 1048576);
				this._errorEvent = new ManualResetEvent(false);
				this._creationSemaphore = new Semaphore(1, 1);
				this._handlesWithCreate = new WaitHandle[]
				{
					this._poolSemaphore,
					this._errorEvent,
					this._creationSemaphore
				};
				this._handlesWithoutCreate = new WaitHandle[]
				{
					this._poolSemaphore,
					this._errorEvent
				};
			}

			// Token: 0x170006C3 RID: 1731
			// (get) Token: 0x060027A0 RID: 10144 RVA: 0x000B04E6 File Offset: 0x000AE6E6
			internal Semaphore CreationSemaphore
			{
				get
				{
					return this._creationSemaphore;
				}
			}

			// Token: 0x170006C4 RID: 1732
			// (get) Token: 0x060027A1 RID: 10145 RVA: 0x000B04EE File Offset: 0x000AE6EE
			internal ManualResetEvent ErrorEvent
			{
				get
				{
					return this._errorEvent;
				}
			}

			// Token: 0x170006C5 RID: 1733
			// (get) Token: 0x060027A2 RID: 10146 RVA: 0x000B04F6 File Offset: 0x000AE6F6
			internal Semaphore PoolSemaphore
			{
				get
				{
					return this._poolSemaphore;
				}
			}

			// Token: 0x060027A3 RID: 10147 RVA: 0x000B04FE File Offset: 0x000AE6FE
			internal WaitHandle[] GetHandles(bool withCreate)
			{
				if (!withCreate)
				{
					return this._handlesWithoutCreate;
				}
				return this._handlesWithCreate;
			}

			// Token: 0x040019BF RID: 6591
			private readonly Semaphore _poolSemaphore;

			// Token: 0x040019C0 RID: 6592
			private readonly ManualResetEvent _errorEvent;

			// Token: 0x040019C1 RID: 6593
			private readonly Semaphore _creationSemaphore;

			// Token: 0x040019C2 RID: 6594
			private readonly WaitHandle[] _handlesWithCreate;

			// Token: 0x040019C3 RID: 6595
			private readonly WaitHandle[] _handlesWithoutCreate;
		}
	}
}
