using System;
using System.Runtime.InteropServices;

namespace System.Data.Odbc
{
	// Token: 0x020002EC RID: 748
	internal sealed class OdbcDescriptorHandle : OdbcHandle
	{
		// Token: 0x06002123 RID: 8483 RVA: 0x0009AA30 File Offset: 0x00098C30
		internal OdbcDescriptorHandle(OdbcStatementHandle statementHandle, ODBC32.SQL_ATTR attribute) : base(statementHandle, attribute)
		{
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x0009AA3C File Offset: 0x00098C3C
		internal ODBC32.RetCode GetDescriptionField(int i, ODBC32.SQL_DESC attribute, CNativeBuffer buffer, out int numericAttribute)
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLGetDescFieldW(this, checked((short)i), attribute, buffer, (int)buffer.ShortLength, out numericAttribute);
			ODBC.TraceODBC(3, "SQLGetDescFieldW", retCode);
			return retCode;
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x0009AA6C File Offset: 0x00098C6C
		internal ODBC32.RetCode SetDescriptionField1(short ordinal, ODBC32.SQL_DESC type, IntPtr value)
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLSetDescFieldW(this, ordinal, type, value, 0);
			ODBC.TraceODBC(3, "SQLSetDescFieldW", retCode);
			return retCode;
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x0009AA94 File Offset: 0x00098C94
		internal ODBC32.RetCode SetDescriptionField2(short ordinal, ODBC32.SQL_DESC type, HandleRef value)
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLSetDescFieldW(this, ordinal, type, value, 0);
			ODBC.TraceODBC(3, "SQLSetDescFieldW", retCode);
			return retCode;
		}
	}
}
