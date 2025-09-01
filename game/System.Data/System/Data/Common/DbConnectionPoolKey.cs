using System;

namespace System.Data.Common
{
	// Token: 0x0200036E RID: 878
	internal class DbConnectionPoolKey : ICloneable
	{
		// Token: 0x06002924 RID: 10532 RVA: 0x000B47FE File Offset: 0x000B29FE
		internal DbConnectionPoolKey(string connectionString)
		{
			this._connectionString = connectionString;
		}

		// Token: 0x06002925 RID: 10533 RVA: 0x000B480D File Offset: 0x000B2A0D
		protected DbConnectionPoolKey(DbConnectionPoolKey key)
		{
			this._connectionString = key.ConnectionString;
		}

		// Token: 0x06002926 RID: 10534 RVA: 0x000B4821 File Offset: 0x000B2A21
		public virtual object Clone()
		{
			return new DbConnectionPoolKey(this);
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06002927 RID: 10535 RVA: 0x000B4829 File Offset: 0x000B2A29
		// (set) Token: 0x06002928 RID: 10536 RVA: 0x000B4831 File Offset: 0x000B2A31
		internal virtual string ConnectionString
		{
			get
			{
				return this._connectionString;
			}
			set
			{
				this._connectionString = value;
			}
		}

		// Token: 0x06002929 RID: 10537 RVA: 0x000B483C File Offset: 0x000B2A3C
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != typeof(DbConnectionPoolKey))
			{
				return false;
			}
			DbConnectionPoolKey dbConnectionPoolKey = obj as DbConnectionPoolKey;
			return dbConnectionPoolKey != null && this._connectionString == dbConnectionPoolKey._connectionString;
		}

		// Token: 0x0600292A RID: 10538 RVA: 0x000B4882 File Offset: 0x000B2A82
		public override int GetHashCode()
		{
			if (this._connectionString != null)
			{
				return this._connectionString.GetHashCode();
			}
			return 0;
		}

		// Token: 0x04001A51 RID: 6737
		private string _connectionString;
	}
}
