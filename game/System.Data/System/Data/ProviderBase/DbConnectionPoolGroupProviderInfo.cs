using System;

namespace System.Data.ProviderBase
{
	// Token: 0x02000360 RID: 864
	internal class DbConnectionPoolGroupProviderInfo
	{
		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x060027A4 RID: 10148 RVA: 0x000B0510 File Offset: 0x000AE710
		// (set) Token: 0x060027A5 RID: 10149 RVA: 0x000B0518 File Offset: 0x000AE718
		internal DbConnectionPoolGroup PoolGroup
		{
			get
			{
				return this._poolGroup;
			}
			set
			{
				this._poolGroup = value;
			}
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x00003D93 File Offset: 0x00001F93
		public DbConnectionPoolGroupProviderInfo()
		{
		}

		// Token: 0x040019C4 RID: 6596
		private DbConnectionPoolGroup _poolGroup;
	}
}
