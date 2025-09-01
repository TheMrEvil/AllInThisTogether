using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x020001D3 RID: 467
	internal class SqlConnectionPoolKey : DbConnectionPoolKey
	{
		// Token: 0x060016D7 RID: 5847 RVA: 0x0006827C File Offset: 0x0006647C
		internal SqlConnectionPoolKey(string connectionString, SqlCredential credential, string accessToken) : base(connectionString)
		{
			this._credential = credential;
			this._accessToken = accessToken;
			this.CalculateHashCode();
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x00068299 File Offset: 0x00066499
		private SqlConnectionPoolKey(SqlConnectionPoolKey key) : base(key)
		{
			this._credential = key.Credential;
			this._accessToken = key.AccessToken;
			this.CalculateHashCode();
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x000682C0 File Offset: 0x000664C0
		public override object Clone()
		{
			return new SqlConnectionPoolKey(this);
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x060016DA RID: 5850 RVA: 0x000682C8 File Offset: 0x000664C8
		// (set) Token: 0x060016DB RID: 5851 RVA: 0x000682D0 File Offset: 0x000664D0
		internal override string ConnectionString
		{
			get
			{
				return base.ConnectionString;
			}
			set
			{
				base.ConnectionString = value;
				this.CalculateHashCode();
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x000682DF File Offset: 0x000664DF
		internal SqlCredential Credential
		{
			get
			{
				return this._credential;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x000682E7 File Offset: 0x000664E7
		internal string AccessToken
		{
			get
			{
				return this._accessToken;
			}
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x000682F0 File Offset: 0x000664F0
		public override bool Equals(object obj)
		{
			SqlConnectionPoolKey sqlConnectionPoolKey = obj as SqlConnectionPoolKey;
			return sqlConnectionPoolKey != null && this._credential == sqlConnectionPoolKey._credential && this.ConnectionString == sqlConnectionPoolKey.ConnectionString && this._accessToken == sqlConnectionPoolKey._accessToken;
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x00068338 File Offset: 0x00066538
		public override int GetHashCode()
		{
			return this._hashValue;
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x00068340 File Offset: 0x00066540
		private void CalculateHashCode()
		{
			this._hashValue = base.GetHashCode();
			if (this._credential != null)
			{
				this._hashValue = this._hashValue * 17 + this._credential.GetHashCode();
				return;
			}
			if (this._accessToken != null)
			{
				this._hashValue = this._hashValue * 17 + this._accessToken.GetHashCode();
			}
		}

		// Token: 0x04000E4A RID: 3658
		private int _hashValue;

		// Token: 0x04000E4B RID: 3659
		private SqlCredential _credential;

		// Token: 0x04000E4C RID: 3660
		private readonly string _accessToken;
	}
}
