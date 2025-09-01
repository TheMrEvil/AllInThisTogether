using System;
using System.Data.Odbc;
using System.Runtime.InteropServices;
using System.Text;
using System.Transactions;
using Microsoft.Win32.SafeHandles;

// Token: 0x02000010 RID: 16
internal static class Interop
{
	// Token: 0x02000011 RID: 17
	internal static class Odbc
	{
		// Token: 0x06000070 RID: 112
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLAllocHandle(ODBC32.SQL_HANDLE HandleType, IntPtr InputHandle, out IntPtr OutputHandle);

		// Token: 0x06000071 RID: 113
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLAllocHandle(ODBC32.SQL_HANDLE HandleType, OdbcHandle InputHandle, out IntPtr OutputHandle);

		// Token: 0x06000072 RID: 114
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLBindCol(OdbcStatementHandle StatementHandle, ushort ColumnNumber, ODBC32.SQL_C TargetType, HandleRef TargetValue, IntPtr BufferLength, IntPtr StrLen_or_Ind);

		// Token: 0x06000073 RID: 115
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLBindCol(OdbcStatementHandle StatementHandle, ushort ColumnNumber, ODBC32.SQL_C TargetType, IntPtr TargetValue, IntPtr BufferLength, IntPtr StrLen_or_Ind);

		// Token: 0x06000074 RID: 116
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLBindParameter(OdbcStatementHandle StatementHandle, ushort ParameterNumber, short ParamDirection, ODBC32.SQL_C SQLCType, short SQLType, IntPtr cbColDef, IntPtr ibScale, HandleRef rgbValue, IntPtr BufferLength, HandleRef StrLen_or_Ind);

		// Token: 0x06000075 RID: 117
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLCancel(OdbcStatementHandle StatementHandle);

		// Token: 0x06000076 RID: 118
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLCloseCursor(OdbcStatementHandle StatementHandle);

		// Token: 0x06000077 RID: 119
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLColAttributeW(OdbcStatementHandle StatementHandle, short ColumnNumber, short FieldIdentifier, CNativeBuffer CharacterAttribute, short BufferLength, out short StringLength, out IntPtr NumericAttribute);

		// Token: 0x06000078 RID: 120
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern ODBC32.RetCode SQLColumnsW(OdbcStatementHandle StatementHandle, string CatalogName, short NameLen1, string SchemaName, short NameLen2, string TableName, short NameLen3, string ColumnName, short NameLen4);

		// Token: 0x06000079 RID: 121
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLDisconnect(IntPtr ConnectionHandle);

		// Token: 0x0600007A RID: 122
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern ODBC32.RetCode SQLDriverConnectW(OdbcConnectionHandle hdbc, IntPtr hwnd, string connectionstring, short cbConnectionstring, IntPtr connectionstringout, short cbConnectionstringoutMax, out short cbConnectionstringout, short fDriverCompletion);

		// Token: 0x0600007B RID: 123
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLEndTran(ODBC32.SQL_HANDLE HandleType, IntPtr Handle, short CompletionType);

		// Token: 0x0600007C RID: 124
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern ODBC32.RetCode SQLExecDirectW(OdbcStatementHandle StatementHandle, string StatementText, int TextLength);

		// Token: 0x0600007D RID: 125
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLExecute(OdbcStatementHandle StatementHandle);

		// Token: 0x0600007E RID: 126
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLFetch(OdbcStatementHandle StatementHandle);

		// Token: 0x0600007F RID: 127
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLFreeHandle(ODBC32.SQL_HANDLE HandleType, IntPtr StatementHandle);

		// Token: 0x06000080 RID: 128
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLFreeStmt(OdbcStatementHandle StatementHandle, ODBC32.STMT Option);

		// Token: 0x06000081 RID: 129
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLGetConnectAttrW(OdbcConnectionHandle ConnectionHandle, ODBC32.SQL_ATTR Attribute, byte[] Value, int BufferLength, out int StringLength);

		// Token: 0x06000082 RID: 130
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLGetData(OdbcStatementHandle StatementHandle, ushort ColumnNumber, ODBC32.SQL_C TargetType, CNativeBuffer TargetValue, IntPtr BufferLength, out IntPtr StrLen_or_Ind);

		// Token: 0x06000083 RID: 131
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLGetDescFieldW(OdbcDescriptorHandle StatementHandle, short RecNumber, ODBC32.SQL_DESC FieldIdentifier, CNativeBuffer ValuePointer, int BufferLength, out int StringLength);

		// Token: 0x06000084 RID: 132
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern ODBC32.RetCode SQLGetDiagRecW(ODBC32.SQL_HANDLE HandleType, OdbcHandle Handle, short RecNumber, StringBuilder rchState, out int NativeError, StringBuilder MessageText, short BufferLength, out short TextLength);

		// Token: 0x06000085 RID: 133
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern ODBC32.RetCode SQLGetDiagFieldW(ODBC32.SQL_HANDLE HandleType, OdbcHandle Handle, short RecNumber, short DiagIdentifier, StringBuilder rchState, short BufferLength, out short StringLength);

		// Token: 0x06000086 RID: 134
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLGetFunctions(OdbcConnectionHandle hdbc, ODBC32.SQL_API fFunction, out short pfExists);

		// Token: 0x06000087 RID: 135
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLGetInfoW(OdbcConnectionHandle hdbc, ODBC32.SQL_INFO fInfoType, byte[] rgbInfoValue, short cbInfoValueMax, out short pcbInfoValue);

		// Token: 0x06000088 RID: 136
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLGetInfoW(OdbcConnectionHandle hdbc, ODBC32.SQL_INFO fInfoType, byte[] rgbInfoValue, short cbInfoValueMax, IntPtr pcbInfoValue);

		// Token: 0x06000089 RID: 137
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLGetStmtAttrW(OdbcStatementHandle StatementHandle, ODBC32.SQL_ATTR Attribute, out IntPtr Value, int BufferLength, out int StringLength);

		// Token: 0x0600008A RID: 138
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLGetTypeInfo(OdbcStatementHandle StatementHandle, short fSqlType);

		// Token: 0x0600008B RID: 139
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLMoreResults(OdbcStatementHandle StatementHandle);

		// Token: 0x0600008C RID: 140
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLNumResultCols(OdbcStatementHandle StatementHandle, out short ColumnCount);

		// Token: 0x0600008D RID: 141
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern ODBC32.RetCode SQLPrepareW(OdbcStatementHandle StatementHandle, string StatementText, int TextLength);

		// Token: 0x0600008E RID: 142
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern ODBC32.RetCode SQLPrimaryKeysW(OdbcStatementHandle StatementHandle, string CatalogName, short NameLen1, string SchemaName, short NameLen2, string TableName, short NameLen3);

		// Token: 0x0600008F RID: 143
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern ODBC32.RetCode SQLProcedureColumnsW(OdbcStatementHandle StatementHandle, string CatalogName, short NameLen1, string SchemaName, short NameLen2, string ProcName, short NameLen3, string ColumnName, short NameLen4);

		// Token: 0x06000090 RID: 144
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern ODBC32.RetCode SQLProceduresW(OdbcStatementHandle StatementHandle, string CatalogName, short NameLen1, string SchemaName, short NameLen2, string ProcName, short NameLen3);

		// Token: 0x06000091 RID: 145
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLRowCount(OdbcStatementHandle StatementHandle, out IntPtr RowCount);

		// Token: 0x06000092 RID: 146
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLSetConnectAttrW(OdbcConnectionHandle ConnectionHandle, ODBC32.SQL_ATTR Attribute, IDtcTransaction Value, int StringLength);

		// Token: 0x06000093 RID: 147
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern ODBC32.RetCode SQLSetConnectAttrW(OdbcConnectionHandle ConnectionHandle, ODBC32.SQL_ATTR Attribute, string Value, int StringLength);

		// Token: 0x06000094 RID: 148
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLSetConnectAttrW(OdbcConnectionHandle ConnectionHandle, ODBC32.SQL_ATTR Attribute, IntPtr Value, int StringLength);

		// Token: 0x06000095 RID: 149
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLSetConnectAttrW(IntPtr ConnectionHandle, ODBC32.SQL_ATTR Attribute, IntPtr Value, int StringLength);

		// Token: 0x06000096 RID: 150
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLSetDescFieldW(OdbcDescriptorHandle StatementHandle, short ColumnNumber, ODBC32.SQL_DESC FieldIdentifier, HandleRef CharacterAttribute, int BufferLength);

		// Token: 0x06000097 RID: 151
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLSetDescFieldW(OdbcDescriptorHandle StatementHandle, short ColumnNumber, ODBC32.SQL_DESC FieldIdentifier, IntPtr CharacterAttribute, int BufferLength);

		// Token: 0x06000098 RID: 152
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLSetEnvAttr(OdbcEnvironmentHandle EnvironmentHandle, ODBC32.SQL_ATTR Attribute, IntPtr Value, ODBC32.SQL_IS StringLength);

		// Token: 0x06000099 RID: 153
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLSetStmtAttrW(OdbcStatementHandle StatementHandle, int Attribute, IntPtr Value, int StringLength);

		// Token: 0x0600009A RID: 154
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern ODBC32.RetCode SQLSpecialColumnsW(OdbcStatementHandle StatementHandle, ODBC32.SQL_SPECIALCOLS IdentifierType, string CatalogName, short NameLen1, string SchemaName, short NameLen2, string TableName, short NameLen3, ODBC32.SQL_SCOPE Scope, ODBC32.SQL_NULLABILITY Nullable);

		// Token: 0x0600009B RID: 155
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern ODBC32.RetCode SQLStatisticsW(OdbcStatementHandle StatementHandle, string CatalogName, short NameLen1, string SchemaName, short NameLen2, string TableName, short NameLen3, short Unique, short Reserved);

		// Token: 0x0600009C RID: 156
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern ODBC32.RetCode SQLTablesW(OdbcStatementHandle StatementHandle, string CatalogName, short NameLen1, string SchemaName, short NameLen2, string TableName, short NameLen3, string TableType, short NameLen4);
	}

	// Token: 0x02000012 RID: 18
	internal static class Libraries
	{
		// Token: 0x04000042 RID: 66
		internal const string Advapi32 = "advapi32.dll";

		// Token: 0x04000043 RID: 67
		internal const string BCrypt = "BCrypt.dll";

		// Token: 0x04000044 RID: 68
		internal const string CoreComm_L1_1_1 = "api-ms-win-core-comm-l1-1-1.dll";

		// Token: 0x04000045 RID: 69
		internal const string Crypt32 = "crypt32.dll";

		// Token: 0x04000046 RID: 70
		internal const string Error_L1 = "api-ms-win-core-winrt-error-l1-1-0.dll";

		// Token: 0x04000047 RID: 71
		internal const string HttpApi = "httpapi.dll";

		// Token: 0x04000048 RID: 72
		internal const string IpHlpApi = "iphlpapi.dll";

		// Token: 0x04000049 RID: 73
		internal const string Kernel32 = "kernel32.dll";

		// Token: 0x0400004A RID: 74
		internal const string Memory_L1_3 = "api-ms-win-core-memory-l1-1-3.dll";

		// Token: 0x0400004B RID: 75
		internal const string Mswsock = "mswsock.dll";

		// Token: 0x0400004C RID: 76
		internal const string NCrypt = "ncrypt.dll";

		// Token: 0x0400004D RID: 77
		internal const string NtDll = "ntdll.dll";

		// Token: 0x0400004E RID: 78
		internal const string Odbc32 = "odbc32.dll";

		// Token: 0x0400004F RID: 79
		internal const string OleAut32 = "oleaut32.dll";

		// Token: 0x04000050 RID: 80
		internal const string PerfCounter = "perfcounter.dll";

		// Token: 0x04000051 RID: 81
		internal const string RoBuffer = "api-ms-win-core-winrt-robuffer-l1-1-0.dll";

		// Token: 0x04000052 RID: 82
		internal const string Secur32 = "secur32.dll";

		// Token: 0x04000053 RID: 83
		internal const string Shell32 = "shell32.dll";

		// Token: 0x04000054 RID: 84
		internal const string SspiCli = "sspicli.dll";

		// Token: 0x04000055 RID: 85
		internal const string User32 = "user32.dll";

		// Token: 0x04000056 RID: 86
		internal const string Version = "version.dll";

		// Token: 0x04000057 RID: 87
		internal const string WebSocket = "websocket.dll";

		// Token: 0x04000058 RID: 88
		internal const string WinHttp = "winhttp.dll";

		// Token: 0x04000059 RID: 89
		internal const string Ws2_32 = "ws2_32.dll";

		// Token: 0x0400005A RID: 90
		internal const string Wtsapi32 = "wtsapi32.dll";

		// Token: 0x0400005B RID: 91
		internal const string CompressionNative = "clrcompression.dll";
	}

	// Token: 0x02000013 RID: 19
	internal class Kernel32
	{
		// Token: 0x0600009D RID: 157
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern bool FreeLibrary([In] IntPtr hModule);

		// Token: 0x0600009E RID: 158
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi)]
		public static extern IntPtr GetProcAddress(SafeLibraryHandle hModule, string lpProcName);

		// Token: 0x0600009F RID: 159
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi)]
		public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

		// Token: 0x060000A0 RID: 160
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		public static extern SafeLibraryHandle LoadLibraryExW([In] string lpwLibFileName, [In] IntPtr hFile, [In] uint dwFlags);

		// Token: 0x060000A1 RID: 161 RVA: 0x00003D93 File Offset: 0x00001F93
		public Kernel32()
		{
		}

		// Token: 0x0400005C RID: 92
		public const int LOAD_LIBRARY_AS_DATAFILE = 2;

		// Token: 0x0400005D RID: 93
		public const int LOAD_LIBRARY_SEARCH_SYSTEM32 = 2048;
	}
}
