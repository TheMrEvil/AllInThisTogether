using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace System.Data.ProviderBase
{
	// Token: 0x0200034F RID: 847
	internal abstract class DbConnectionInternal
	{
		// Token: 0x06002693 RID: 9875 RVA: 0x000AB28F File Offset: 0x000A948F
		protected DbConnectionInternal() : this(ConnectionState.Open, true, false)
		{
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x000AB29A File Offset: 0x000A949A
		internal DbConnectionInternal(ConnectionState state, bool hidePassword, bool allowSetConnectionString)
		{
			this._allowSetConnectionString = allowSetConnectionString;
			this._hidePassword = hidePassword;
			this._state = state;
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06002695 RID: 9877 RVA: 0x000AB2C4 File Offset: 0x000A94C4
		internal bool AllowSetConnectionString
		{
			get
			{
				return this._allowSetConnectionString;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06002696 RID: 9878 RVA: 0x000AB2CC File Offset: 0x000A94CC
		internal bool CanBePooled
		{
			get
			{
				return !this._connectionIsDoomed && !this._cannotBePooled && !this._owningObject.IsAlive;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06002697 RID: 9879 RVA: 0x000AB2EE File Offset: 0x000A94EE
		protected internal bool IsConnectionDoomed
		{
			get
			{
				return this._connectionIsDoomed;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06002698 RID: 9880 RVA: 0x000AB2F6 File Offset: 0x000A94F6
		internal bool IsEmancipated
		{
			get
			{
				return this._pooledCount < 1 && !this._owningObject.IsAlive;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06002699 RID: 9881 RVA: 0x000AB311 File Offset: 0x000A9511
		internal bool IsInPool
		{
			get
			{
				return this._pooledCount == 1;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x0600269A RID: 9882 RVA: 0x000AB31C File Offset: 0x000A951C
		protected internal object Owner
		{
			get
			{
				return this._owningObject.Target;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x0600269B RID: 9883 RVA: 0x000AB329 File Offset: 0x000A9529
		internal DbConnectionPool Pool
		{
			get
			{
				return this._connectionPool;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x0600269C RID: 9884 RVA: 0x000AB331 File Offset: 0x000A9531
		protected internal DbReferenceCollection ReferenceCollection
		{
			get
			{
				return this._referenceCollection;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x0600269D RID: 9885
		public abstract string ServerVersion { get; }

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x0600269E RID: 9886 RVA: 0x00008E4B File Offset: 0x0000704B
		public virtual string ServerVersionNormalized
		{
			get
			{
				throw ADP.NotSupported();
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x0600269F RID: 9887 RVA: 0x000AB339 File Offset: 0x000A9539
		public bool ShouldHidePassword
		{
			get
			{
				return this._hidePassword;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x060026A0 RID: 9888 RVA: 0x000AB341 File Offset: 0x000A9541
		public ConnectionState State
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x000AB349 File Offset: 0x000A9549
		internal void AddWeakReference(object value, int tag)
		{
			if (this._referenceCollection == null)
			{
				this._referenceCollection = this.CreateReferenceCollection();
				if (this._referenceCollection == null)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.CreateReferenceCollectionReturnedNull);
				}
			}
			this._referenceCollection.Add(value, tag);
		}

		// Token: 0x060026A2 RID: 9890
		public abstract DbTransaction BeginTransaction(IsolationLevel il);

		// Token: 0x060026A3 RID: 9891 RVA: 0x000AB37C File Offset: 0x000A957C
		public virtual void ChangeDatabase(string value)
		{
			throw ADP.MethodNotImplemented("ChangeDatabase");
		}

		// Token: 0x060026A4 RID: 9892 RVA: 0x00007EED File Offset: 0x000060ED
		internal virtual void PrepareForReplaceConnection()
		{
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x00007EED File Offset: 0x000060ED
		protected virtual void PrepareForCloseConnection()
		{
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x00003E32 File Offset: 0x00002032
		protected virtual object ObtainAdditionalLocksForClose()
		{
			return null;
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x00007EED File Offset: 0x000060ED
		protected virtual void ReleaseAdditionalLocksForClose(object lockToken)
		{
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x000AB388 File Offset: 0x000A9588
		protected virtual DbReferenceCollection CreateReferenceCollection()
		{
			throw ADP.InternalError(ADP.InternalErrorCode.AttemptingToConstructReferenceCollectionOnStaticObject);
		}

		// Token: 0x060026A9 RID: 9897
		protected abstract void Deactivate();

		// Token: 0x060026AA RID: 9898 RVA: 0x000AB394 File Offset: 0x000A9594
		internal void DeactivateConnection()
		{
			if (!this._connectionIsDoomed && this.Pool.UseLoadBalancing && DateTime.UtcNow.Ticks - this._createTime.Ticks > this.Pool.LoadBalanceTimeout.Ticks)
			{
				this.DoNotPoolThisConnection();
			}
			this.Deactivate();
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x000AB3F0 File Offset: 0x000A95F0
		protected internal void DoNotPoolThisConnection()
		{
			this._cannotBePooled = true;
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x000AB3F9 File Offset: 0x000A95F9
		protected internal void DoomThisConnection()
		{
			this._connectionIsDoomed = true;
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x000AB402 File Offset: 0x000A9602
		protected internal virtual DataTable GetSchema(DbConnectionFactory factory, DbConnectionPoolGroup poolGroup, DbConnection outerConnection, string collectionName, string[] restrictions)
		{
			return factory.GetMetaDataFactory(poolGroup, this).GetSchema(outerConnection, collectionName, restrictions);
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x000AB416 File Offset: 0x000A9616
		internal void MakeNonPooledObject(object owningObject)
		{
			this._connectionPool = null;
			this._owningObject.Target = owningObject;
			this._pooledCount = -1;
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x000AB432 File Offset: 0x000A9632
		internal void MakePooledConnection(DbConnectionPool connectionPool)
		{
			this._createTime = DateTime.UtcNow;
			this._connectionPool = connectionPool;
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x000AB448 File Offset: 0x000A9648
		internal void NotifyWeakReference(int message)
		{
			DbReferenceCollection referenceCollection = this.ReferenceCollection;
			if (referenceCollection != null)
			{
				referenceCollection.Notify(message);
			}
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x000AB466 File Offset: 0x000A9666
		internal virtual void OpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory)
		{
			if (!this.TryOpenConnection(outerConnection, connectionFactory, null, null))
			{
				throw ADP.InternalError(ADP.InternalErrorCode.SynchronousConnectReturnedPending);
			}
		}

		// Token: 0x060026B2 RID: 9906 RVA: 0x000AA7CC File Offset: 0x000A89CC
		internal virtual bool TryOpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			throw ADP.ConnectionAlreadyOpen(this.State);
		}

		// Token: 0x060026B3 RID: 9907 RVA: 0x000AB47C File Offset: 0x000A967C
		internal virtual bool TryReplaceConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			throw ADP.MethodNotImplemented("TryReplaceConnection");
		}

		// Token: 0x060026B4 RID: 9908 RVA: 0x000AB488 File Offset: 0x000A9688
		protected bool TryOpenConnectionInternal(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			if (connectionFactory.SetInnerConnectionFrom(outerConnection, DbConnectionClosedConnecting.SingletonInstance, this))
			{
				DbConnectionInternal dbConnectionInternal = null;
				try
				{
					connectionFactory.PermissionDemand(outerConnection);
					if (!connectionFactory.TryGetConnection(outerConnection, retry, userOptions, this, out dbConnectionInternal))
					{
						return false;
					}
				}
				catch
				{
					connectionFactory.SetInnerConnectionTo(outerConnection, this);
					throw;
				}
				if (dbConnectionInternal == null)
				{
					connectionFactory.SetInnerConnectionTo(outerConnection, this);
					throw ADP.InternalConnectionError(ADP.ConnectionError.GetConnectionReturnsNull);
				}
				connectionFactory.SetInnerConnectionEvent(outerConnection, dbConnectionInternal);
				return true;
			}
			return true;
		}

		// Token: 0x060026B5 RID: 9909 RVA: 0x000AB4FC File Offset: 0x000A96FC
		internal void PrePush(object expectedOwner)
		{
			if (expectedOwner == null)
			{
				if (this._owningObject.Target != null)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.UnpooledObjectHasOwner);
				}
			}
			else if (this._owningObject.Target != expectedOwner)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.UnpooledObjectHasWrongOwner);
			}
			if (this._pooledCount != 0)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.PushingObjectSecondTime);
			}
			this._pooledCount++;
			this._owningObject.Target = null;
		}

		// Token: 0x060026B6 RID: 9910 RVA: 0x000AB560 File Offset: 0x000A9760
		internal void PostPop(object newOwner)
		{
			if (this._owningObject.Target != null)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.PooledObjectHasOwner);
			}
			this._owningObject.Target = newOwner;
			this._pooledCount--;
			if (this.Pool != null)
			{
				if (this._pooledCount != 0)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.PooledObjectInPoolMoreThanOnce);
				}
			}
			else if (-1 != this._pooledCount)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.NonPooledObjectUsedMoreThanOnce);
			}
		}

		// Token: 0x060026B7 RID: 9911 RVA: 0x000AB5C4 File Offset: 0x000A97C4
		internal void RemoveWeakReference(object value)
		{
			DbReferenceCollection referenceCollection = this.ReferenceCollection;
			if (referenceCollection != null)
			{
				referenceCollection.Remove(value);
			}
		}

		// Token: 0x060026B8 RID: 9912 RVA: 0x00006D61 File Offset: 0x00004F61
		internal virtual bool IsConnectionAlive(bool throwOnException = false)
		{
			return true;
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x060026B9 RID: 9913 RVA: 0x000AB5E2 File Offset: 0x000A97E2
		// (set) Token: 0x060026BA RID: 9914 RVA: 0x000AB5EC File Offset: 0x000A97EC
		protected internal Transaction EnlistedTransaction
		{
			get
			{
				return this._enlistedTransaction;
			}
			set
			{
				Transaction enlistedTransaction = this._enlistedTransaction;
				if ((null == enlistedTransaction && null != value) || (null != enlistedTransaction && !enlistedTransaction.Equals(value)))
				{
					Transaction transaction = null;
					Transaction transaction2 = null;
					try
					{
						if (null != value)
						{
							transaction = value.Clone();
						}
						lock (this)
						{
							transaction2 = Interlocked.Exchange<Transaction>(ref this._enlistedTransaction, transaction);
							this._enlistedTransactionOriginal = value;
							value = transaction;
							transaction = null;
						}
					}
					finally
					{
						if (null != transaction2 && transaction2 != this._enlistedTransaction)
						{
							transaction2.Dispose();
						}
						if (null != transaction && transaction != this._enlistedTransaction)
						{
							transaction.Dispose();
						}
					}
					if (null != value)
					{
						this.TransactionOutcomeEnlist(value);
					}
				}
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x060026BB RID: 9915 RVA: 0x000AB6D0 File Offset: 0x000A98D0
		protected bool EnlistedTransactionDisposed
		{
			get
			{
				bool result;
				try
				{
					Transaction enlistedTransactionOriginal = this._enlistedTransactionOriginal;
					bool flag = enlistedTransactionOriginal != null && enlistedTransactionOriginal.TransactionInformation == null;
					result = flag;
				}
				catch (ObjectDisposedException)
				{
					result = true;
				}
				return result;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x060026BC RID: 9916 RVA: 0x000AB718 File Offset: 0x000A9918
		internal bool IsTxRootWaitingForTxEnd
		{
			get
			{
				return this._isInStasis;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x060026BD RID: 9917 RVA: 0x00006D61 File Offset: 0x00004F61
		protected virtual bool UnbindOnTransactionCompletion
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x060026BE RID: 9918 RVA: 0x00006D64 File Offset: 0x00004F64
		protected internal virtual bool IsNonPoolableTransactionRoot
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x060026BF RID: 9919 RVA: 0x00006D64 File Offset: 0x00004F64
		internal virtual bool IsTransactionRoot
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x060026C0 RID: 9920 RVA: 0x00006D61 File Offset: 0x00004F61
		protected virtual bool ReadyToPrepareTransaction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060026C1 RID: 9921
		protected abstract void Activate(Transaction transaction);

		// Token: 0x060026C2 RID: 9922 RVA: 0x000AB720 File Offset: 0x000A9920
		internal void ActivateConnection(Transaction transaction)
		{
			this.Activate(transaction);
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x000AB72C File Offset: 0x000A992C
		internal virtual void CloseConnection(DbConnection owningObject, DbConnectionFactory connectionFactory)
		{
			if (connectionFactory.SetInnerConnectionFrom(owningObject, DbConnectionOpenBusy.SingletonInstance, this))
			{
				lock (this)
				{
					object lockToken = this.ObtainAdditionalLocksForClose();
					try
					{
						this.PrepareForCloseConnection();
						DbConnectionPool pool = this.Pool;
						this.DetachCurrentTransactionIfEnded();
						if (pool != null)
						{
							pool.PutObject(this, owningObject);
						}
						else
						{
							this.Deactivate();
							this._owningObject.Target = null;
							if (this.IsTransactionRoot)
							{
								this.SetInStasis();
							}
							else
							{
								this.Dispose();
							}
						}
					}
					finally
					{
						this.ReleaseAdditionalLocksForClose(lockToken);
						connectionFactory.SetInnerConnectionEvent(owningObject, DbConnectionClosedPreviouslyOpened.SingletonInstance);
					}
				}
			}
		}

		// Token: 0x060026C4 RID: 9924 RVA: 0x000AB7E0 File Offset: 0x000A99E0
		internal virtual void DelegatedTransactionEnded()
		{
			if (1 != this._pooledCount)
			{
				if (-1 == this._pooledCount && !this._owningObject.IsAlive)
				{
					this.TerminateStasis(false);
					this.Deactivate();
					this.Dispose();
				}
				return;
			}
			this.TerminateStasis(true);
			this.Deactivate();
			DbConnectionPool pool = this.Pool;
			if (pool == null)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.PooledObjectWithoutPool);
			}
			pool.PutObjectFromTransactedPool(this);
		}

		// Token: 0x060026C5 RID: 9925 RVA: 0x000AB848 File Offset: 0x000A9A48
		public virtual void Dispose()
		{
			this._connectionPool = null;
			this._connectionIsDoomed = true;
			this._enlistedTransactionOriginal = null;
			Transaction transaction = Interlocked.Exchange<Transaction>(ref this._enlistedTransaction, null);
			if (transaction != null)
			{
				transaction.Dispose();
			}
		}

		// Token: 0x060026C6 RID: 9926
		public abstract void EnlistTransaction(Transaction transaction);

		// Token: 0x060026C7 RID: 9927 RVA: 0x00007EED File Offset: 0x000060ED
		protected virtual void CleanupTransactionOnCompletion(Transaction transaction)
		{
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x000AB888 File Offset: 0x000A9A88
		internal void DetachCurrentTransactionIfEnded()
		{
			Transaction enlistedTransaction = this.EnlistedTransaction;
			if (enlistedTransaction != null)
			{
				bool flag;
				try
				{
					flag = (enlistedTransaction.TransactionInformation.Status > TransactionStatus.Active);
				}
				catch (TransactionException)
				{
					flag = true;
				}
				if (flag)
				{
					this.DetachTransaction(enlistedTransaction, true);
				}
			}
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x000AB8D8 File Offset: 0x000A9AD8
		internal void DetachTransaction(Transaction transaction, bool isExplicitlyReleasing)
		{
			lock (this)
			{
				DbConnection dbConnection = (DbConnection)this.Owner;
				if (isExplicitlyReleasing || this.UnbindOnTransactionCompletion || dbConnection == null)
				{
					Transaction enlistedTransaction = this._enlistedTransaction;
					if (enlistedTransaction != null && transaction.Equals(enlistedTransaction))
					{
						this.EnlistedTransaction = null;
						if (this.IsTxRootWaitingForTxEnd)
						{
							this.DelegatedTransactionEnded();
						}
					}
				}
			}
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x000AB958 File Offset: 0x000A9B58
		internal void CleanupConnectionOnTransactionCompletion(Transaction transaction)
		{
			this.DetachTransaction(transaction, false);
			DbConnectionPool pool = this.Pool;
			if (pool != null)
			{
				pool.TransactionEnded(transaction, this);
			}
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x000AB980 File Offset: 0x000A9B80
		private void TransactionCompletedEvent(object sender, TransactionEventArgs e)
		{
			Transaction transaction = e.Transaction;
			this.CleanupTransactionOnCompletion(transaction);
			this.CleanupConnectionOnTransactionCompletion(transaction);
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x000AB9A2 File Offset: 0x000A9BA2
		private void TransactionOutcomeEnlist(Transaction transaction)
		{
			transaction.TransactionCompleted += this.TransactionCompletedEvent;
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x000AB9B6 File Offset: 0x000A9BB6
		internal void SetInStasis()
		{
			this._isInStasis = true;
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x000AB9BF File Offset: 0x000A9BBF
		private void TerminateStasis(bool returningToPool)
		{
			this._isInStasis = false;
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x000AB9C8 File Offset: 0x000A9BC8
		// Note: this type is marked as 'beforefieldinit'.
		static DbConnectionInternal()
		{
		}

		// Token: 0x04001944 RID: 6468
		internal static readonly StateChangeEventArgs StateChangeClosed = new StateChangeEventArgs(ConnectionState.Open, ConnectionState.Closed);

		// Token: 0x04001945 RID: 6469
		internal static readonly StateChangeEventArgs StateChangeOpen = new StateChangeEventArgs(ConnectionState.Closed, ConnectionState.Open);

		// Token: 0x04001946 RID: 6470
		private readonly bool _allowSetConnectionString;

		// Token: 0x04001947 RID: 6471
		private readonly bool _hidePassword;

		// Token: 0x04001948 RID: 6472
		private readonly ConnectionState _state;

		// Token: 0x04001949 RID: 6473
		private readonly WeakReference _owningObject = new WeakReference(null, false);

		// Token: 0x0400194A RID: 6474
		private DbConnectionPool _connectionPool;

		// Token: 0x0400194B RID: 6475
		private DbReferenceCollection _referenceCollection;

		// Token: 0x0400194C RID: 6476
		private int _pooledCount;

		// Token: 0x0400194D RID: 6477
		private bool _connectionIsDoomed;

		// Token: 0x0400194E RID: 6478
		private bool _cannotBePooled;

		// Token: 0x0400194F RID: 6479
		private DateTime _createTime;

		// Token: 0x04001950 RID: 6480
		private bool _isInStasis;

		// Token: 0x04001951 RID: 6481
		private Transaction _enlistedTransaction;

		// Token: 0x04001952 RID: 6482
		private Transaction _enlistedTransactionOriginal;
	}
}
