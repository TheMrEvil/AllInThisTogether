using System;
using System.Data.ProviderBase;

namespace System.Data.SqlClient
{
	// Token: 0x020001D4 RID: 468
	internal sealed class SqlConnectionPoolProviderInfo : DbConnectionPoolProviderInfo
	{
		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060016E1 RID: 5857 RVA: 0x000683A0 File Offset: 0x000665A0
		// (set) Token: 0x060016E2 RID: 5858 RVA: 0x000683A8 File Offset: 0x000665A8
		internal string InstanceName
		{
			get
			{
				return this._instanceName;
			}
			set
			{
				this._instanceName = value;
			}
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x000683B1 File Offset: 0x000665B1
		public SqlConnectionPoolProviderInfo()
		{
		}

		// Token: 0x04000E4D RID: 3661
		private string _instanceName;
	}
}
