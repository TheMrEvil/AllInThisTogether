using System;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace System.Data.Odbc
{
	// Token: 0x020002FB RID: 763
	internal sealed class OdbcStatementHandle : OdbcHandle
	{
		// Token: 0x060021E5 RID: 8677 RVA: 0x0009DD5A File Offset: 0x0009BF5A
		internal OdbcStatementHandle(OdbcConnectionHandle connectionHandle) : base(ODBC32.SQL_HANDLE.STMT, connectionHandle)
		{
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x0009DD64 File Offset: 0x0009BF64
		internal ODBC32.RetCode BindColumn2(int columnNumber, ODBC32.SQL_C targetType, HandleRef buffer, IntPtr length, IntPtr srLen_or_Ind)
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLBindCol(this, checked((ushort)columnNumber), targetType, buffer, length, srLen_or_Ind);
			ODBC.TraceODBC(3, "SQLBindCol", retCode);
			return retCode;
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x0009DD90 File Offset: 0x0009BF90
		internal ODBC32.RetCode BindColumn3(int columnNumber, ODBC32.SQL_C targetType, IntPtr srLen_or_Ind)
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLBindCol(this, checked((ushort)columnNumber), targetType, ADP.PtrZero, ADP.PtrZero, srLen_or_Ind);
			ODBC.TraceODBC(3, "SQLBindCol", retCode);
			return retCode;
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x0009DDC0 File Offset: 0x0009BFC0
		internal ODBC32.RetCode BindParameter(short ordinal, short parameterDirection, ODBC32.SQL_C sqlctype, ODBC32.SQL_TYPE sqltype, IntPtr cchSize, IntPtr scale, HandleRef buffer, IntPtr bufferLength, HandleRef intbuffer)
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLBindParameter(this, checked((ushort)ordinal), parameterDirection, sqlctype, (short)sqltype, cchSize, scale, buffer, bufferLength, intbuffer);
			ODBC.TraceODBC(3, "SQLBindParameter", retCode);
			return retCode;
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x0009DDF4 File Offset: 0x0009BFF4
		internal ODBC32.RetCode Cancel()
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLCancel(this);
			ODBC.TraceODBC(3, "SQLCancel", retCode);
			return retCode;
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x0009DE18 File Offset: 0x0009C018
		internal ODBC32.RetCode CloseCursor()
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLCloseCursor(this);
			ODBC.TraceODBC(3, "SQLCloseCursor", retCode);
			return retCode;
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x0009DE3C File Offset: 0x0009C03C
		internal ODBC32.RetCode ColumnAttribute(int columnNumber, short fieldIdentifier, CNativeBuffer characterAttribute, out short stringLength, out SQLLEN numericAttribute)
		{
			IntPtr value;
			ODBC32.RetCode retCode = Interop.Odbc.SQLColAttributeW(this, checked((short)columnNumber), fieldIdentifier, characterAttribute, characterAttribute.ShortLength, out stringLength, out value);
			numericAttribute = new SQLLEN(value);
			ODBC.TraceODBC(3, "SQLColAttributeW", retCode);
			return retCode;
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x0009DE78 File Offset: 0x0009C078
		internal ODBC32.RetCode Columns(string tableCatalog, string tableSchema, string tableName, string columnName)
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLColumnsW(this, tableCatalog, ODBC.ShortStringLength(tableCatalog), tableSchema, ODBC.ShortStringLength(tableSchema), tableName, ODBC.ShortStringLength(tableName), columnName, ODBC.ShortStringLength(columnName));
			ODBC.TraceODBC(3, "SQLColumnsW", retCode);
			return retCode;
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x0009DEB8 File Offset: 0x0009C0B8
		internal ODBC32.RetCode Execute()
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLExecute(this);
			ODBC.TraceODBC(3, "SQLExecute", retCode);
			return retCode;
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x0009DEDC File Offset: 0x0009C0DC
		internal ODBC32.RetCode ExecuteDirect(string commandText)
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLExecDirectW(this, commandText, -3);
			ODBC.TraceODBC(3, "SQLExecDirectW", retCode);
			return retCode;
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x0009DF00 File Offset: 0x0009C100
		internal ODBC32.RetCode Fetch()
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLFetch(this);
			ODBC.TraceODBC(3, "SQLFetch", retCode);
			return retCode;
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x0009DF24 File Offset: 0x0009C124
		internal ODBC32.RetCode FreeStatement(ODBC32.STMT stmt)
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLFreeStmt(this, stmt);
			ODBC.TraceODBC(3, "SQLFreeStmt", retCode);
			return retCode;
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x0009DF48 File Offset: 0x0009C148
		internal ODBC32.RetCode GetData(int index, ODBC32.SQL_C sqlctype, CNativeBuffer buffer, int cb, out IntPtr cbActual)
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLGetData(this, checked((ushort)index), sqlctype, buffer, new IntPtr(cb), out cbActual);
			ODBC.TraceODBC(3, "SQLGetData", retCode);
			return retCode;
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x0009DF78 File Offset: 0x0009C178
		internal ODBC32.RetCode GetStatementAttribute(ODBC32.SQL_ATTR attribute, out IntPtr value, out int stringLength)
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLGetStmtAttrW(this, attribute, out value, ADP.PtrSize, out stringLength);
			ODBC.TraceODBC(3, "SQLGetStmtAttrW", retCode);
			return retCode;
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x0009DFA4 File Offset: 0x0009C1A4
		internal ODBC32.RetCode GetTypeInfo(short fSqlType)
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLGetTypeInfo(this, fSqlType);
			ODBC.TraceODBC(3, "SQLGetTypeInfo", retCode);
			return retCode;
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x0009DFC8 File Offset: 0x0009C1C8
		internal ODBC32.RetCode MoreResults()
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLMoreResults(this);
			ODBC.TraceODBC(3, "SQLMoreResults", retCode);
			return retCode;
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x0009DFEC File Offset: 0x0009C1EC
		internal ODBC32.RetCode NumberOfResultColumns(out short columnsAffected)
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLNumResultCols(this, out columnsAffected);
			ODBC.TraceODBC(3, "SQLNumResultCols", retCode);
			return retCode;
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x0009E010 File Offset: 0x0009C210
		internal ODBC32.RetCode Prepare(string commandText)
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLPrepareW(this, commandText, -3);
			ODBC.TraceODBC(3, "SQLPrepareW", retCode);
			return retCode;
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x0009E034 File Offset: 0x0009C234
		internal ODBC32.RetCode PrimaryKeys(string catalogName, string schemaName, string tableName)
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLPrimaryKeysW(this, catalogName, ODBC.ShortStringLength(catalogName), schemaName, ODBC.ShortStringLength(schemaName), tableName, ODBC.ShortStringLength(tableName));
			ODBC.TraceODBC(3, "SQLPrimaryKeysW", retCode);
			return retCode;
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x0009E06C File Offset: 0x0009C26C
		internal ODBC32.RetCode Procedures(string procedureCatalog, string procedureSchema, string procedureName)
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLProceduresW(this, procedureCatalog, ODBC.ShortStringLength(procedureCatalog), procedureSchema, ODBC.ShortStringLength(procedureSchema), procedureName, ODBC.ShortStringLength(procedureName));
			ODBC.TraceODBC(3, "SQLProceduresW", retCode);
			return retCode;
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x0009E0A4 File Offset: 0x0009C2A4
		internal ODBC32.RetCode ProcedureColumns(string procedureCatalog, string procedureSchema, string procedureName, string columnName)
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLProcedureColumnsW(this, procedureCatalog, ODBC.ShortStringLength(procedureCatalog), procedureSchema, ODBC.ShortStringLength(procedureSchema), procedureName, ODBC.ShortStringLength(procedureName), columnName, ODBC.ShortStringLength(columnName));
			ODBC.TraceODBC(3, "SQLProcedureColumnsW", retCode);
			return retCode;
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x0009E0E4 File Offset: 0x0009C2E4
		internal ODBC32.RetCode RowCount(out SQLLEN rowCount)
		{
			IntPtr value;
			ODBC32.RetCode retCode = Interop.Odbc.SQLRowCount(this, out value);
			rowCount = new SQLLEN(value);
			ODBC.TraceODBC(3, "SQLRowCount", retCode);
			return retCode;
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x0009E114 File Offset: 0x0009C314
		internal ODBC32.RetCode SetStatementAttribute(ODBC32.SQL_ATTR attribute, IntPtr value, ODBC32.SQL_IS stringLength)
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLSetStmtAttrW(this, (int)attribute, value, (int)stringLength);
			ODBC.TraceODBC(3, "SQLSetStmtAttrW", retCode);
			return retCode;
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x0009E138 File Offset: 0x0009C338
		internal ODBC32.RetCode SpecialColumns(string quotedTable)
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLSpecialColumnsW(this, ODBC32.SQL_SPECIALCOLS.ROWVER, null, 0, null, 0, quotedTable, ODBC.ShortStringLength(quotedTable), ODBC32.SQL_SCOPE.SESSION, ODBC32.SQL_NULLABILITY.NO_NULLS);
			ODBC.TraceODBC(3, "SQLSpecialColumnsW", retCode);
			return retCode;
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x0009E168 File Offset: 0x0009C368
		internal ODBC32.RetCode Statistics(string tableCatalog, string tableSchema, string tableName, short unique, short accuracy)
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLStatisticsW(this, tableCatalog, ODBC.ShortStringLength(tableCatalog), tableSchema, ODBC.ShortStringLength(tableSchema), tableName, ODBC.ShortStringLength(tableName), unique, accuracy);
			ODBC.TraceODBC(3, "SQLStatisticsW", retCode);
			return retCode;
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x0009E1A4 File Offset: 0x0009C3A4
		internal ODBC32.RetCode Statistics(string tableName)
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLStatisticsW(this, null, 0, null, 0, tableName, ODBC.ShortStringLength(tableName), 0, 1);
			ODBC.TraceODBC(3, "SQLStatisticsW", retCode);
			return retCode;
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x0009E1D4 File Offset: 0x0009C3D4
		internal ODBC32.RetCode Tables(string tableCatalog, string tableSchema, string tableName, string tableType)
		{
			ODBC32.RetCode retCode = Interop.Odbc.SQLTablesW(this, tableCatalog, ODBC.ShortStringLength(tableCatalog), tableSchema, ODBC.ShortStringLength(tableSchema), tableName, ODBC.ShortStringLength(tableName), tableType, ODBC.ShortStringLength(tableType));
			ODBC.TraceODBC(3, "SQLTablesW", retCode);
			return retCode;
		}
	}
}
