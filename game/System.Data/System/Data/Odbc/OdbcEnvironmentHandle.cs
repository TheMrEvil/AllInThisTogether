using System;

namespace System.Data.Odbc
{
	// Token: 0x020002E6 RID: 742
	internal sealed class OdbcEnvironmentHandle : OdbcHandle
	{
		// Token: 0x060020F9 RID: 8441 RVA: 0x0009A48C File Offset: 0x0009868C
		internal OdbcEnvironmentHandle() : base(ODBC32.SQL_HANDLE.ENV, null)
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLSetEnvAttr(this, ODBC32.SQL_ATTR.ODBC_VERSION, ODBC32.SQL_OV_ODBC3, ODBC32.SQL_IS.INTEGER);
			retCode = Interop.Odbc.SQLSetEnvAttr(this, ODBC32.SQL_ATTR.CONNECTION_POOLING, ODBC32.SQL_CP_ONE_PER_HENV, ODBC32.SQL_IS.INTEGER);
			if (retCode > ODBC32.RetCode.SUCCESS_WITH_INFO)
			{
				base.Dispose();
				throw ODBC.CantEnableConnectionpooling(retCode);
			}
		}
	}
}
