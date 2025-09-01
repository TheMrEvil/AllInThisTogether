using System;
using System.Data.Common;

namespace System.Data.Odbc
{
	// Token: 0x020002DD RID: 733
	internal sealed class OdbcConnectionString : DbConnectionOptions
	{
		// Token: 0x0600205E RID: 8286 RVA: 0x00096CA0 File Offset: 0x00094EA0
		internal OdbcConnectionString(string connectionString, bool validate) : base(connectionString, null, true)
		{
			if (!validate)
			{
				string text = null;
				int num = 0;
				this._expandedConnectionString = base.ExpandDataDirectories(ref text, ref num);
			}
			if ((validate || this._expandedConnectionString == null) && connectionString != null && 1024 < connectionString.Length)
			{
				throw ODBC.ConnectionStringTooLong();
			}
		}

		// Token: 0x04001776 RID: 6006
		private readonly string _expandedConnectionString;
	}
}
