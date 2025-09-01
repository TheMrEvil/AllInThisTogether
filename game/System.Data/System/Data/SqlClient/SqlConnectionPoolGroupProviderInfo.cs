using System;
using System.Data.ProviderBase;

namespace System.Data.SqlClient
{
	// Token: 0x020001D2 RID: 466
	internal sealed class SqlConnectionPoolGroupProviderInfo : DbConnectionPoolGroupProviderInfo
	{
		// Token: 0x060016D2 RID: 5842 RVA: 0x0006814B File Offset: 0x0006634B
		internal SqlConnectionPoolGroupProviderInfo(SqlConnectionString connectionOptions)
		{
			this._failoverPartner = connectionOptions.FailoverPartner;
			if (string.IsNullOrEmpty(this._failoverPartner))
			{
				this._failoverPartner = null;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x00068173 File Offset: 0x00066373
		internal string FailoverPartner
		{
			get
			{
				return this._failoverPartner;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x060016D4 RID: 5844 RVA: 0x0006817B File Offset: 0x0006637B
		internal bool UseFailoverPartner
		{
			get
			{
				return this._useFailoverPartner;
			}
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x00068184 File Offset: 0x00066384
		internal void AliasCheck(string server)
		{
			if (this._alias != server)
			{
				lock (this)
				{
					if (this._alias == null)
					{
						this._alias = server;
					}
					else if (this._alias != server)
					{
						base.PoolGroup.Clear();
						this._alias = server;
					}
				}
			}
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x000681FC File Offset: 0x000663FC
		internal void FailoverCheck(SqlInternalConnection connection, bool actualUseFailoverPartner, SqlConnectionString userConnectionOptions, string actualFailoverPartner)
		{
			if (this.UseFailoverPartner != actualUseFailoverPartner)
			{
				base.PoolGroup.Clear();
				this._useFailoverPartner = actualUseFailoverPartner;
			}
			if (!this._useFailoverPartner && this._failoverPartner != actualFailoverPartner)
			{
				lock (this)
				{
					if (this._failoverPartner != actualFailoverPartner)
					{
						this._failoverPartner = actualFailoverPartner;
					}
				}
			}
		}

		// Token: 0x04000E47 RID: 3655
		private string _alias;

		// Token: 0x04000E48 RID: 3656
		private string _failoverPartner;

		// Token: 0x04000E49 RID: 3657
		private bool _useFailoverPartner;
	}
}
