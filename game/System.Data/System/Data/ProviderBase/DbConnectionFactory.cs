using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace System.Data.ProviderBase
{
	// Token: 0x0200034C RID: 844
	internal abstract class DbConnectionFactory
	{
		// Token: 0x0600266F RID: 9839 RVA: 0x000AA8B2 File Offset: 0x000A8AB2
		protected DbConnectionFactory()
		{
			this._connectionPoolGroups = new Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup>();
			this._poolsToRelease = new List<DbConnectionPool>();
			this._poolGroupsToRelease = new List<DbConnectionPoolGroup>();
			this._pruningTimer = this.CreatePruningTimer();
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06002670 RID: 9840
		public abstract DbProviderFactory ProviderFactory { get; }

		// Token: 0x06002671 RID: 9841 RVA: 0x000AA8E8 File Offset: 0x000A8AE8
		public void ClearAllPools()
		{
			foreach (KeyValuePair<DbConnectionPoolKey, DbConnectionPoolGroup> keyValuePair in this._connectionPoolGroups)
			{
				DbConnectionPoolGroup value = keyValuePair.Value;
				if (value != null)
				{
					value.Clear();
				}
			}
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x000AA948 File Offset: 0x000A8B48
		public void ClearPool(DbConnection connection)
		{
			ADP.CheckArgumentNull(connection, "connection");
			DbConnectionPoolGroup connectionPoolGroup = this.GetConnectionPoolGroup(connection);
			if (connectionPoolGroup != null)
			{
				connectionPoolGroup.Clear();
			}
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x000AA974 File Offset: 0x000A8B74
		public void ClearPool(DbConnectionPoolKey key)
		{
			ADP.CheckArgumentNull(key.ConnectionString, "key.ConnectionString");
			DbConnectionPoolGroup dbConnectionPoolGroup;
			if (this._connectionPoolGroups.TryGetValue(key, out dbConnectionPoolGroup))
			{
				dbConnectionPoolGroup.Clear();
			}
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x00003E32 File Offset: 0x00002032
		internal virtual DbConnectionPoolProviderInfo CreateConnectionPoolProviderInfo(DbConnectionOptions connectionOptions)
		{
			return null;
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x000AA9A8 File Offset: 0x000A8BA8
		internal DbConnectionInternal CreateNonPooledConnection(DbConnection owningConnection, DbConnectionPoolGroup poolGroup, DbConnectionOptions userOptions)
		{
			DbConnectionOptions connectionOptions = poolGroup.ConnectionOptions;
			DbConnectionPoolGroupProviderInfo providerInfo = poolGroup.ProviderInfo;
			DbConnectionPoolKey poolKey = poolGroup.PoolKey;
			DbConnectionInternal dbConnectionInternal = this.CreateConnection(connectionOptions, poolKey, providerInfo, null, owningConnection, userOptions);
			if (dbConnectionInternal != null)
			{
				dbConnectionInternal.MakeNonPooledObject(owningConnection);
			}
			return dbConnectionInternal;
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x000AA9E4 File Offset: 0x000A8BE4
		internal DbConnectionInternal CreatePooledConnection(DbConnectionPool pool, DbConnection owningObject, DbConnectionOptions options, DbConnectionPoolKey poolKey, DbConnectionOptions userOptions)
		{
			DbConnectionPoolGroupProviderInfo providerInfo = pool.PoolGroup.ProviderInfo;
			DbConnectionInternal dbConnectionInternal = this.CreateConnection(options, poolKey, providerInfo, pool, owningObject, userOptions);
			if (dbConnectionInternal != null)
			{
				dbConnectionInternal.MakePooledConnection(pool);
			}
			return dbConnectionInternal;
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x00003E32 File Offset: 0x00002032
		internal virtual DbConnectionPoolGroupProviderInfo CreateConnectionPoolGroupProviderInfo(DbConnectionOptions connectionOptions)
		{
			return null;
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x000AAA17 File Offset: 0x000A8C17
		private Timer CreatePruningTimer()
		{
			return ADP.UnsafeCreateTimer(new TimerCallback(this.PruneConnectionPoolGroups), null, 240000, 30000);
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x000AAA38 File Offset: 0x000A8C38
		protected DbConnectionOptions FindConnectionOptions(DbConnectionPoolKey key)
		{
			DbConnectionPoolGroup dbConnectionPoolGroup;
			if (!string.IsNullOrEmpty(key.ConnectionString) && this._connectionPoolGroups.TryGetValue(key, out dbConnectionPoolGroup))
			{
				return dbConnectionPoolGroup.ConnectionOptions;
			}
			return null;
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x000AAA6A File Offset: 0x000A8C6A
		private static Task<DbConnectionInternal> GetCompletedTask()
		{
			Task<DbConnectionInternal> result;
			if ((result = DbConnectionFactory.s_completedTask) == null)
			{
				result = (DbConnectionFactory.s_completedTask = Task.FromResult<DbConnectionInternal>(null));
			}
			return result;
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x000AAA84 File Offset: 0x000A8C84
		private DbConnectionPool GetConnectionPool(DbConnection owningObject, DbConnectionPoolGroup connectionPoolGroup)
		{
			if (connectionPoolGroup.IsDisabled && connectionPoolGroup.PoolGroupOptions != null)
			{
				DbConnectionPoolGroupOptions poolGroupOptions = connectionPoolGroup.PoolGroupOptions;
				DbConnectionOptions connectionOptions = connectionPoolGroup.ConnectionOptions;
				connectionPoolGroup = this.GetConnectionPoolGroup(connectionPoolGroup.PoolKey, poolGroupOptions, ref connectionOptions);
				this.SetConnectionPoolGroup(owningObject, connectionPoolGroup);
			}
			return connectionPoolGroup.GetConnectionPool(this);
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x000AAAD0 File Offset: 0x000A8CD0
		internal DbConnectionPoolGroup GetConnectionPoolGroup(DbConnectionPoolKey key, DbConnectionPoolGroupOptions poolOptions, ref DbConnectionOptions userConnectionOptions)
		{
			if (string.IsNullOrEmpty(key.ConnectionString))
			{
				return null;
			}
			Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> connectionPoolGroups = this._connectionPoolGroups;
			DbConnectionPoolGroup dbConnectionPoolGroup;
			if (!connectionPoolGroups.TryGetValue(key, out dbConnectionPoolGroup) || (dbConnectionPoolGroup.IsDisabled && dbConnectionPoolGroup.PoolGroupOptions != null))
			{
				DbConnectionOptions dbConnectionOptions = this.CreateConnectionOptions(key.ConnectionString, userConnectionOptions);
				if (dbConnectionOptions == null)
				{
					throw ADP.InternalConnectionError(ADP.ConnectionError.ConnectionOptionsMissing);
				}
				if (userConnectionOptions == null)
				{
					userConnectionOptions = dbConnectionOptions;
				}
				if (poolOptions == null)
				{
					if (dbConnectionPoolGroup != null)
					{
						poolOptions = dbConnectionPoolGroup.PoolGroupOptions;
					}
					else
					{
						poolOptions = this.CreateConnectionPoolGroupOptions(dbConnectionOptions);
					}
				}
				lock (this)
				{
					connectionPoolGroups = this._connectionPoolGroups;
					if (!connectionPoolGroups.TryGetValue(key, out dbConnectionPoolGroup))
					{
						DbConnectionPoolGroup dbConnectionPoolGroup2 = new DbConnectionPoolGroup(dbConnectionOptions, key, poolOptions);
						dbConnectionPoolGroup2.ProviderInfo = this.CreateConnectionPoolGroupProviderInfo(dbConnectionOptions);
						Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> dictionary = new Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup>(1 + connectionPoolGroups.Count);
						foreach (KeyValuePair<DbConnectionPoolKey, DbConnectionPoolGroup> keyValuePair in connectionPoolGroups)
						{
							dictionary.Add(keyValuePair.Key, keyValuePair.Value);
						}
						dictionary.Add(key, dbConnectionPoolGroup2);
						dbConnectionPoolGroup = dbConnectionPoolGroup2;
						this._connectionPoolGroups = dictionary;
					}
					return dbConnectionPoolGroup;
				}
			}
			if (userConnectionOptions == null)
			{
				userConnectionOptions = dbConnectionPoolGroup.ConnectionOptions;
			}
			return dbConnectionPoolGroup;
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x000AAC20 File Offset: 0x000A8E20
		private void PruneConnectionPoolGroups(object state)
		{
			List<DbConnectionPool> poolsToRelease = this._poolsToRelease;
			lock (poolsToRelease)
			{
				if (this._poolsToRelease.Count != 0)
				{
					foreach (DbConnectionPool dbConnectionPool in this._poolsToRelease.ToArray())
					{
						if (dbConnectionPool != null)
						{
							dbConnectionPool.Clear();
							if (dbConnectionPool.Count == 0)
							{
								this._poolsToRelease.Remove(dbConnectionPool);
							}
						}
					}
				}
			}
			List<DbConnectionPoolGroup> poolGroupsToRelease = this._poolGroupsToRelease;
			lock (poolGroupsToRelease)
			{
				if (this._poolGroupsToRelease.Count != 0)
				{
					foreach (DbConnectionPoolGroup dbConnectionPoolGroup in this._poolGroupsToRelease.ToArray())
					{
						if (dbConnectionPoolGroup != null && dbConnectionPoolGroup.Clear() == 0)
						{
							this._poolGroupsToRelease.Remove(dbConnectionPoolGroup);
						}
					}
				}
			}
			lock (this)
			{
				Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> connectionPoolGroups = this._connectionPoolGroups;
				Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> dictionary = new Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup>(connectionPoolGroups.Count);
				foreach (KeyValuePair<DbConnectionPoolKey, DbConnectionPoolGroup> keyValuePair in connectionPoolGroups)
				{
					if (keyValuePair.Value != null)
					{
						if (keyValuePair.Value.Prune())
						{
							this.QueuePoolGroupForRelease(keyValuePair.Value);
						}
						else
						{
							dictionary.Add(keyValuePair.Key, keyValuePair.Value);
						}
					}
				}
				this._connectionPoolGroups = dictionary;
			}
		}

		// Token: 0x0600267E RID: 9854 RVA: 0x000AADD8 File Offset: 0x000A8FD8
		internal void QueuePoolForRelease(DbConnectionPool pool, bool clearing)
		{
			pool.Shutdown();
			List<DbConnectionPool> poolsToRelease = this._poolsToRelease;
			lock (poolsToRelease)
			{
				if (clearing)
				{
					pool.Clear();
				}
				this._poolsToRelease.Add(pool);
			}
		}

		// Token: 0x0600267F RID: 9855 RVA: 0x000AAE30 File Offset: 0x000A9030
		internal void QueuePoolGroupForRelease(DbConnectionPoolGroup poolGroup)
		{
			List<DbConnectionPoolGroup> poolGroupsToRelease = this._poolGroupsToRelease;
			lock (poolGroupsToRelease)
			{
				this._poolGroupsToRelease.Add(poolGroup);
			}
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x000AAE78 File Offset: 0x000A9078
		protected virtual DbConnectionInternal CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningConnection, DbConnectionOptions userOptions)
		{
			return this.CreateConnection(options, poolKey, poolGroupProviderInfo, pool, owningConnection);
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x000AAE88 File Offset: 0x000A9088
		internal DbMetaDataFactory GetMetaDataFactory(DbConnectionPoolGroup connectionPoolGroup, DbConnectionInternal internalConnection)
		{
			DbMetaDataFactory dbMetaDataFactory = connectionPoolGroup.MetaDataFactory;
			if (dbMetaDataFactory == null)
			{
				bool flag = false;
				dbMetaDataFactory = this.CreateMetaDataFactory(internalConnection, out flag);
				if (flag)
				{
					connectionPoolGroup.MetaDataFactory = dbMetaDataFactory;
				}
			}
			return dbMetaDataFactory;
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x000AAEB6 File Offset: 0x000A90B6
		protected virtual DbMetaDataFactory CreateMetaDataFactory(DbConnectionInternal internalConnection, out bool cacheMetaDataFactory)
		{
			cacheMetaDataFactory = false;
			throw ADP.NotSupported();
		}

		// Token: 0x06002683 RID: 9859
		protected abstract DbConnectionInternal CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningConnection);

		// Token: 0x06002684 RID: 9860
		protected abstract DbConnectionOptions CreateConnectionOptions(string connectionString, DbConnectionOptions previous);

		// Token: 0x06002685 RID: 9861
		protected abstract DbConnectionPoolGroupOptions CreateConnectionPoolGroupOptions(DbConnectionOptions options);

		// Token: 0x06002686 RID: 9862
		internal abstract DbConnectionPoolGroup GetConnectionPoolGroup(DbConnection connection);

		// Token: 0x06002687 RID: 9863
		internal abstract DbConnectionInternal GetInnerConnection(DbConnection connection);

		// Token: 0x06002688 RID: 9864
		internal abstract void PermissionDemand(DbConnection outerConnection);

		// Token: 0x06002689 RID: 9865
		internal abstract void SetConnectionPoolGroup(DbConnection outerConnection, DbConnectionPoolGroup poolGroup);

		// Token: 0x0600268A RID: 9866
		internal abstract void SetInnerConnectionEvent(DbConnection owningObject, DbConnectionInternal to);

		// Token: 0x0600268B RID: 9867
		internal abstract bool SetInnerConnectionFrom(DbConnection owningObject, DbConnectionInternal to, DbConnectionInternal from);

		// Token: 0x0600268C RID: 9868
		internal abstract void SetInnerConnectionTo(DbConnection owningObject, DbConnectionInternal to);

		// Token: 0x0600268D RID: 9869 RVA: 0x000AAEC0 File Offset: 0x000A90C0
		internal bool TryGetConnection(DbConnection owningConnection, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions, DbConnectionInternal oldConnection, out DbConnectionInternal connection)
		{
			DbConnectionFactory.<>c__DisplayClass40_0 CS$<>8__locals1 = new DbConnectionFactory.<>c__DisplayClass40_0();
			CS$<>8__locals1.retry = retry;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.owningConnection = owningConnection;
			CS$<>8__locals1.userOptions = userOptions;
			CS$<>8__locals1.oldConnection = oldConnection;
			connection = null;
			int num = 10;
			int num2 = 1;
			for (;;)
			{
				CS$<>8__locals1.poolGroup = this.GetConnectionPoolGroup(CS$<>8__locals1.owningConnection);
				DbConnectionPool connectionPool = this.GetConnectionPool(CS$<>8__locals1.owningConnection, CS$<>8__locals1.poolGroup);
				if (connectionPool == null)
				{
					CS$<>8__locals1.poolGroup = this.GetConnectionPoolGroup(CS$<>8__locals1.owningConnection);
					if (CS$<>8__locals1.retry != null)
					{
						break;
					}
					connection = this.CreateNonPooledConnection(CS$<>8__locals1.owningConnection, CS$<>8__locals1.poolGroup, CS$<>8__locals1.userOptions);
				}
				else
				{
					if (((SqlConnection)CS$<>8__locals1.owningConnection).ForceNewConnection)
					{
						connection = connectionPool.ReplaceConnection(CS$<>8__locals1.owningConnection, CS$<>8__locals1.userOptions, CS$<>8__locals1.oldConnection);
					}
					else if (!connectionPool.TryGetConnection(CS$<>8__locals1.owningConnection, CS$<>8__locals1.retry, CS$<>8__locals1.userOptions, out connection))
					{
						return false;
					}
					if (connection == null)
					{
						if (connectionPool.IsRunning)
						{
							goto Block_8;
						}
						Thread.Sleep(num2);
						num2 *= 2;
					}
				}
				if (connection != null || num-- <= 0)
				{
					goto IL_268;
				}
			}
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			Task<DbConnectionInternal>[] obj = DbConnectionFactory.s_pendingOpenNonPooled;
			Task<DbConnectionInternal> task3;
			lock (obj)
			{
				int i;
				for (i = 0; i < DbConnectionFactory.s_pendingOpenNonPooled.Length; i++)
				{
					Task task4 = DbConnectionFactory.s_pendingOpenNonPooled[i];
					if (task4 == null)
					{
						DbConnectionFactory.s_pendingOpenNonPooled[i] = DbConnectionFactory.GetCompletedTask();
						break;
					}
					if (task4.IsCompleted)
					{
						break;
					}
				}
				if (i == DbConnectionFactory.s_pendingOpenNonPooled.Length)
				{
					i = (int)((ulong)DbConnectionFactory.s_pendingOpenNonPooledNext % (ulong)((long)DbConnectionFactory.s_pendingOpenNonPooled.Length));
					DbConnectionFactory.s_pendingOpenNonPooledNext += 1U;
				}
				Task<DbConnectionInternal> task2 = DbConnectionFactory.s_pendingOpenNonPooled[i];
				Func<Task<DbConnectionInternal>, DbConnectionInternal> continuationFunction;
				if ((continuationFunction = CS$<>8__locals1.<>9__1) == null)
				{
					continuationFunction = (CS$<>8__locals1.<>9__1 = delegate(Task<DbConnectionInternal> _)
					{
						Transaction currentTransaction = ADP.GetCurrentTransaction();
						DbConnectionInternal result;
						try
						{
							ADP.SetCurrentTransaction(CS$<>8__locals1.retry.Task.AsyncState as Transaction);
							DbConnectionInternal dbConnectionInternal = CS$<>8__locals1.<>4__this.CreateNonPooledConnection(CS$<>8__locals1.owningConnection, CS$<>8__locals1.poolGroup, CS$<>8__locals1.userOptions);
							if (CS$<>8__locals1.oldConnection != null && CS$<>8__locals1.oldConnection.State == ConnectionState.Open)
							{
								CS$<>8__locals1.oldConnection.PrepareForReplaceConnection();
								CS$<>8__locals1.oldConnection.Dispose();
							}
							result = dbConnectionInternal;
						}
						finally
						{
							ADP.SetCurrentTransaction(currentTransaction);
						}
						return result;
					});
				}
				task3 = task2.ContinueWith<DbConnectionInternal>(continuationFunction, cancellationTokenSource.Token, TaskContinuationOptions.LongRunning, TaskScheduler.Default);
				DbConnectionFactory.s_pendingOpenNonPooled[i] = task3;
			}
			if (CS$<>8__locals1.owningConnection.ConnectionTimeout > 0)
			{
				int millisecondsDelay = CS$<>8__locals1.owningConnection.ConnectionTimeout * 1000;
				cancellationTokenSource.CancelAfter(millisecondsDelay);
			}
			task3.ContinueWith(delegate(Task<DbConnectionInternal> task)
			{
				cancellationTokenSource.Dispose();
				if (task.IsCanceled)
				{
					CS$<>8__locals1.retry.TrySetException(ADP.ExceptionWithStackTrace(ADP.NonPooledOpenTimeout()));
					return;
				}
				if (task.IsFaulted)
				{
					CS$<>8__locals1.retry.TrySetException(task.Exception.InnerException);
					return;
				}
				if (!CS$<>8__locals1.retry.TrySetResult(task.Result))
				{
					task.Result.DoomThisConnection();
					task.Result.Dispose();
				}
			}, TaskScheduler.Default);
			return false;
			Block_8:
			throw ADP.PooledOpenTimeout();
			IL_268:
			if (connection == null)
			{
				throw ADP.PooledOpenTimeout();
			}
			return true;
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x000AB154 File Offset: 0x000A9354
		// Note: this type is marked as 'beforefieldinit'.
		static DbConnectionFactory()
		{
		}

		// Token: 0x04001932 RID: 6450
		private Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> _connectionPoolGroups;

		// Token: 0x04001933 RID: 6451
		private readonly List<DbConnectionPool> _poolsToRelease;

		// Token: 0x04001934 RID: 6452
		private readonly List<DbConnectionPoolGroup> _poolGroupsToRelease;

		// Token: 0x04001935 RID: 6453
		private readonly Timer _pruningTimer;

		// Token: 0x04001936 RID: 6454
		private const int PruningDueTime = 240000;

		// Token: 0x04001937 RID: 6455
		private const int PruningPeriod = 30000;

		// Token: 0x04001938 RID: 6456
		private static uint s_pendingOpenNonPooledNext = 0U;

		// Token: 0x04001939 RID: 6457
		private static Task<DbConnectionInternal>[] s_pendingOpenNonPooled = new Task<DbConnectionInternal>[Environment.ProcessorCount];

		// Token: 0x0400193A RID: 6458
		private static Task<DbConnectionInternal> s_completedTask;

		// Token: 0x0200034D RID: 845
		[CompilerGenerated]
		private sealed class <>c__DisplayClass40_0
		{
			// Token: 0x0600268F RID: 9871 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass40_0()
			{
			}

			// Token: 0x06002690 RID: 9872 RVA: 0x000AB16C File Offset: 0x000A936C
			internal DbConnectionInternal <TryGetConnection>b__1(Task<DbConnectionInternal> _)
			{
				Transaction currentTransaction = ADP.GetCurrentTransaction();
				DbConnectionInternal result;
				try
				{
					ADP.SetCurrentTransaction(this.retry.Task.AsyncState as Transaction);
					DbConnectionInternal dbConnectionInternal = this.<>4__this.CreateNonPooledConnection(this.owningConnection, this.poolGroup, this.userOptions);
					if (this.oldConnection != null && this.oldConnection.State == ConnectionState.Open)
					{
						this.oldConnection.PrepareForReplaceConnection();
						this.oldConnection.Dispose();
					}
					result = dbConnectionInternal;
				}
				finally
				{
					ADP.SetCurrentTransaction(currentTransaction);
				}
				return result;
			}

			// Token: 0x0400193B RID: 6459
			public TaskCompletionSource<DbConnectionInternal> retry;

			// Token: 0x0400193C RID: 6460
			public DbConnectionFactory <>4__this;

			// Token: 0x0400193D RID: 6461
			public DbConnection owningConnection;

			// Token: 0x0400193E RID: 6462
			public DbConnectionPoolGroup poolGroup;

			// Token: 0x0400193F RID: 6463
			public DbConnectionOptions userOptions;

			// Token: 0x04001940 RID: 6464
			public DbConnectionInternal oldConnection;

			// Token: 0x04001941 RID: 6465
			public Func<Task<DbConnectionInternal>, DbConnectionInternal> <>9__1;
		}

		// Token: 0x0200034E RID: 846
		[CompilerGenerated]
		private sealed class <>c__DisplayClass40_1
		{
			// Token: 0x06002691 RID: 9873 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass40_1()
			{
			}

			// Token: 0x06002692 RID: 9874 RVA: 0x000AB200 File Offset: 0x000A9400
			internal void <TryGetConnection>b__0(Task<DbConnectionInternal> task)
			{
				this.cancellationTokenSource.Dispose();
				if (task.IsCanceled)
				{
					this.CS$<>8__locals1.retry.TrySetException(ADP.ExceptionWithStackTrace(ADP.NonPooledOpenTimeout()));
					return;
				}
				if (task.IsFaulted)
				{
					this.CS$<>8__locals1.retry.TrySetException(task.Exception.InnerException);
					return;
				}
				if (!this.CS$<>8__locals1.retry.TrySetResult(task.Result))
				{
					task.Result.DoomThisConnection();
					task.Result.Dispose();
				}
			}

			// Token: 0x04001942 RID: 6466
			public CancellationTokenSource cancellationTokenSource;

			// Token: 0x04001943 RID: 6467
			public DbConnectionFactory.<>c__DisplayClass40_0 CS$<>8__locals1;
		}
	}
}
