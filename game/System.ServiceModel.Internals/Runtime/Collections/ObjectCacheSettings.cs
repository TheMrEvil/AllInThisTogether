using System;

namespace System.Runtime.Collections
{
	// Token: 0x02000054 RID: 84
	internal class ObjectCacheSettings
	{
		// Token: 0x06000311 RID: 785 RVA: 0x000105EC File Offset: 0x0000E7EC
		public ObjectCacheSettings()
		{
			this.CacheLimit = 64;
			this.IdleTimeout = ObjectCacheSettings.DefaultIdleTimeout;
			this.LeaseTimeout = ObjectCacheSettings.DefaultLeaseTimeout;
			this.PurgeFrequency = 32;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0001061A File Offset: 0x0000E81A
		private ObjectCacheSettings(ObjectCacheSettings other)
		{
			this.CacheLimit = other.CacheLimit;
			this.IdleTimeout = other.IdleTimeout;
			this.LeaseTimeout = other.LeaseTimeout;
			this.PurgeFrequency = other.PurgeFrequency;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00010652 File Offset: 0x0000E852
		internal ObjectCacheSettings Clone()
		{
			return new ObjectCacheSettings(this);
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0001065A File Offset: 0x0000E85A
		// (set) Token: 0x06000315 RID: 789 RVA: 0x00010662 File Offset: 0x0000E862
		public int CacheLimit
		{
			get
			{
				return this.cacheLimit;
			}
			set
			{
				this.cacheLimit = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0001066B File Offset: 0x0000E86B
		// (set) Token: 0x06000317 RID: 791 RVA: 0x00010673 File Offset: 0x0000E873
		public TimeSpan IdleTimeout
		{
			get
			{
				return this.idleTimeout;
			}
			set
			{
				this.idleTimeout = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0001067C File Offset: 0x0000E87C
		// (set) Token: 0x06000319 RID: 793 RVA: 0x00010684 File Offset: 0x0000E884
		public TimeSpan LeaseTimeout
		{
			get
			{
				return this.leaseTimeout;
			}
			set
			{
				this.leaseTimeout = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0001068D File Offset: 0x0000E88D
		// (set) Token: 0x0600031B RID: 795 RVA: 0x00010695 File Offset: 0x0000E895
		public int PurgeFrequency
		{
			get
			{
				return this.purgeFrequency;
			}
			set
			{
				this.purgeFrequency = value;
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0001069E File Offset: 0x0000E89E
		// Note: this type is marked as 'beforefieldinit'.
		static ObjectCacheSettings()
		{
		}

		// Token: 0x040001FC RID: 508
		private int cacheLimit;

		// Token: 0x040001FD RID: 509
		private TimeSpan idleTimeout;

		// Token: 0x040001FE RID: 510
		private TimeSpan leaseTimeout;

		// Token: 0x040001FF RID: 511
		private int purgeFrequency;

		// Token: 0x04000200 RID: 512
		private const int DefaultCacheLimit = 64;

		// Token: 0x04000201 RID: 513
		private const int DefaultPurgeFrequency = 32;

		// Token: 0x04000202 RID: 514
		private static TimeSpan DefaultIdleTimeout = TimeSpan.FromMinutes(2.0);

		// Token: 0x04000203 RID: 515
		private static TimeSpan DefaultLeaseTimeout = TimeSpan.FromMinutes(5.0);
	}
}
