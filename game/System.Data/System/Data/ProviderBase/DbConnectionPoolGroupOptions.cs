using System;

namespace System.Data.ProviderBase
{
	// Token: 0x02000362 RID: 866
	internal sealed class DbConnectionPoolGroupOptions
	{
		// Token: 0x060027AF RID: 10159 RVA: 0x000B06D8 File Offset: 0x000AE8D8
		public DbConnectionPoolGroupOptions(bool poolByIdentity, int minPoolSize, int maxPoolSize, int creationTimeout, int loadBalanceTimeout, bool hasTransactionAffinity)
		{
			this._poolByIdentity = poolByIdentity;
			this._minPoolSize = minPoolSize;
			this._maxPoolSize = maxPoolSize;
			this._creationTimeout = creationTimeout;
			if (loadBalanceTimeout != 0)
			{
				this._loadBalanceTimeout = new TimeSpan(0, 0, loadBalanceTimeout);
				this._useLoadBalancing = true;
			}
			this._hasTransactionAffinity = hasTransactionAffinity;
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x060027B0 RID: 10160 RVA: 0x000B072A File Offset: 0x000AE92A
		public int CreationTimeout
		{
			get
			{
				return this._creationTimeout;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x060027B1 RID: 10161 RVA: 0x000B0732 File Offset: 0x000AE932
		public bool HasTransactionAffinity
		{
			get
			{
				return this._hasTransactionAffinity;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x060027B2 RID: 10162 RVA: 0x000B073A File Offset: 0x000AE93A
		public TimeSpan LoadBalanceTimeout
		{
			get
			{
				return this._loadBalanceTimeout;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x060027B3 RID: 10163 RVA: 0x000B0742 File Offset: 0x000AE942
		public int MaxPoolSize
		{
			get
			{
				return this._maxPoolSize;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x060027B4 RID: 10164 RVA: 0x000B074A File Offset: 0x000AE94A
		public int MinPoolSize
		{
			get
			{
				return this._minPoolSize;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x060027B5 RID: 10165 RVA: 0x000B0752 File Offset: 0x000AE952
		public bool PoolByIdentity
		{
			get
			{
				return this._poolByIdentity;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x060027B6 RID: 10166 RVA: 0x000B075A File Offset: 0x000AE95A
		public bool UseLoadBalancing
		{
			get
			{
				return this._useLoadBalancing;
			}
		}

		// Token: 0x040019CB RID: 6603
		private readonly bool _poolByIdentity;

		// Token: 0x040019CC RID: 6604
		private readonly int _minPoolSize;

		// Token: 0x040019CD RID: 6605
		private readonly int _maxPoolSize;

		// Token: 0x040019CE RID: 6606
		private readonly int _creationTimeout;

		// Token: 0x040019CF RID: 6607
		private readonly TimeSpan _loadBalanceTimeout;

		// Token: 0x040019D0 RID: 6608
		private readonly bool _hasTransactionAffinity;

		// Token: 0x040019D1 RID: 6609
		private readonly bool _useLoadBalancing;
	}
}
