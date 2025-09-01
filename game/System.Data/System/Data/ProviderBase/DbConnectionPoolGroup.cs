using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;

namespace System.Data.ProviderBase
{
	// Token: 0x02000350 RID: 848
	internal sealed class DbConnectionPoolGroup
	{
		// Token: 0x060026D0 RID: 9936 RVA: 0x000AB9E2 File Offset: 0x000A9BE2
		internal DbConnectionPoolGroup(DbConnectionOptions connectionOptions, DbConnectionPoolKey key, DbConnectionPoolGroupOptions poolGroupOptions)
		{
			this._connectionOptions = connectionOptions;
			this._poolKey = key;
			this._poolGroupOptions = poolGroupOptions;
			this._poolCollection = new ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool>();
			this._state = 1;
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x060026D1 RID: 9937 RVA: 0x000ABA11 File Offset: 0x000A9C11
		internal DbConnectionOptions ConnectionOptions
		{
			get
			{
				return this._connectionOptions;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x060026D2 RID: 9938 RVA: 0x000ABA19 File Offset: 0x000A9C19
		internal DbConnectionPoolKey PoolKey
		{
			get
			{
				return this._poolKey;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x060026D3 RID: 9939 RVA: 0x000ABA21 File Offset: 0x000A9C21
		// (set) Token: 0x060026D4 RID: 9940 RVA: 0x000ABA29 File Offset: 0x000A9C29
		internal DbConnectionPoolGroupProviderInfo ProviderInfo
		{
			get
			{
				return this._providerInfo;
			}
			set
			{
				this._providerInfo = value;
				if (value != null)
				{
					this._providerInfo.PoolGroup = this;
				}
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x060026D5 RID: 9941 RVA: 0x000ABA41 File Offset: 0x000A9C41
		internal bool IsDisabled
		{
			get
			{
				return 4 == this._state;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x060026D6 RID: 9942 RVA: 0x000ABA4C File Offset: 0x000A9C4C
		internal DbConnectionPoolGroupOptions PoolGroupOptions
		{
			get
			{
				return this._poolGroupOptions;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x060026D7 RID: 9943 RVA: 0x000ABA54 File Offset: 0x000A9C54
		// (set) Token: 0x060026D8 RID: 9944 RVA: 0x000ABA5C File Offset: 0x000A9C5C
		internal DbMetaDataFactory MetaDataFactory
		{
			get
			{
				return this._metaDataFactory;
			}
			set
			{
				this._metaDataFactory = value;
			}
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x000ABA68 File Offset: 0x000A9C68
		internal int Clear()
		{
			ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool> concurrentDictionary = null;
			lock (this)
			{
				if (this._poolCollection.Count > 0)
				{
					concurrentDictionary = this._poolCollection;
					this._poolCollection = new ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool>();
				}
			}
			if (concurrentDictionary != null)
			{
				foreach (KeyValuePair<DbConnectionPoolIdentity, DbConnectionPool> keyValuePair in concurrentDictionary)
				{
					DbConnectionPool value = keyValuePair.Value;
					if (value != null)
					{
						value.ConnectionFactory.QueuePoolForRelease(value, true);
					}
				}
			}
			return this._poolCollection.Count;
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x000ABB1C File Offset: 0x000A9D1C
		internal DbConnectionPool GetConnectionPool(DbConnectionFactory connectionFactory)
		{
			DbConnectionPool dbConnectionPool = null;
			if (this._poolGroupOptions != null)
			{
				DbConnectionPoolIdentity dbConnectionPoolIdentity = DbConnectionPoolIdentity.NoIdentity;
				if (this._poolGroupOptions.PoolByIdentity)
				{
					dbConnectionPoolIdentity = DbConnectionPoolIdentity.GetCurrent();
					if (dbConnectionPoolIdentity.IsRestricted)
					{
						dbConnectionPoolIdentity = null;
					}
				}
				if (dbConnectionPoolIdentity != null && !this._poolCollection.TryGetValue(dbConnectionPoolIdentity, out dbConnectionPool))
				{
					DbConnectionPoolGroup obj = this;
					lock (obj)
					{
						if (!this._poolCollection.TryGetValue(dbConnectionPoolIdentity, out dbConnectionPool))
						{
							DbConnectionPoolProviderInfo connectionPoolProviderInfo = connectionFactory.CreateConnectionPoolProviderInfo(this.ConnectionOptions);
							DbConnectionPool dbConnectionPool2 = new DbConnectionPool(connectionFactory, this, dbConnectionPoolIdentity, connectionPoolProviderInfo);
							if (this.MarkPoolGroupAsActive())
							{
								dbConnectionPool2.Startup();
								this._poolCollection.TryAdd(dbConnectionPoolIdentity, dbConnectionPool2);
								dbConnectionPool = dbConnectionPool2;
							}
							else
							{
								dbConnectionPool2.Shutdown();
							}
						}
					}
				}
			}
			if (dbConnectionPool == null)
			{
				DbConnectionPoolGroup obj = this;
				lock (obj)
				{
					this.MarkPoolGroupAsActive();
				}
			}
			return dbConnectionPool;
		}

		// Token: 0x060026DB RID: 9947 RVA: 0x000ABC18 File Offset: 0x000A9E18
		private bool MarkPoolGroupAsActive()
		{
			if (2 == this._state)
			{
				this._state = 1;
			}
			return 1 == this._state;
		}

		// Token: 0x060026DC RID: 9948 RVA: 0x000ABC34 File Offset: 0x000A9E34
		internal bool Prune()
		{
			bool result;
			lock (this)
			{
				if (this._poolCollection.Count > 0)
				{
					ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool> concurrentDictionary = new ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool>();
					foreach (KeyValuePair<DbConnectionPoolIdentity, DbConnectionPool> keyValuePair in this._poolCollection)
					{
						DbConnectionPool value = keyValuePair.Value;
						if (value != null)
						{
							if (!value.ErrorOccurred && value.Count == 0)
							{
								value.ConnectionFactory.QueuePoolForRelease(value, false);
							}
							else
							{
								concurrentDictionary.TryAdd(keyValuePair.Key, keyValuePair.Value);
							}
						}
					}
					this._poolCollection = concurrentDictionary;
				}
				if (this._poolCollection.Count == 0)
				{
					if (1 == this._state)
					{
						this._state = 2;
					}
					else if (2 == this._state)
					{
						this._state = 4;
					}
				}
				result = (4 == this._state);
			}
			return result;
		}

		// Token: 0x04001953 RID: 6483
		private readonly DbConnectionOptions _connectionOptions;

		// Token: 0x04001954 RID: 6484
		private readonly DbConnectionPoolKey _poolKey;

		// Token: 0x04001955 RID: 6485
		private readonly DbConnectionPoolGroupOptions _poolGroupOptions;

		// Token: 0x04001956 RID: 6486
		private ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool> _poolCollection;

		// Token: 0x04001957 RID: 6487
		private int _state;

		// Token: 0x04001958 RID: 6488
		private DbConnectionPoolGroupProviderInfo _providerInfo;

		// Token: 0x04001959 RID: 6489
		private DbMetaDataFactory _metaDataFactory;

		// Token: 0x0400195A RID: 6490
		private const int PoolGroupStateActive = 1;

		// Token: 0x0400195B RID: 6491
		private const int PoolGroupStateIdle = 2;

		// Token: 0x0400195C RID: 6492
		private const int PoolGroupStateDisabled = 4;
	}
}
