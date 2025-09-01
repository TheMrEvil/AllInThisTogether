using System;
using System.Data.Common;
using System.Globalization;

namespace System.Data.Odbc
{
	// Token: 0x020002AA RID: 682
	internal static class ODBC
	{
		// Token: 0x06001F4B RID: 8011 RVA: 0x000936C7 File Offset: 0x000918C7
		internal static Exception ConnectionClosed()
		{
			return ADP.InvalidOperation(SR.GetString("The connection is closed."));
		}

		// Token: 0x06001F4C RID: 8012 RVA: 0x000936D8 File Offset: 0x000918D8
		internal static Exception OpenConnectionNoOwner()
		{
			return ADP.InvalidOperation(SR.GetString("An internal connection does not have an owner."));
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x000936E9 File Offset: 0x000918E9
		internal static Exception UnknownSQLType(ODBC32.SQL_TYPE sqltype)
		{
			return ADP.Argument(SR.GetString("Unknown SQL type - {0}.", new object[]
			{
				sqltype.ToString()
			}));
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x00093710 File Offset: 0x00091910
		internal static Exception ConnectionStringTooLong()
		{
			return ADP.Argument(SR.GetString("Connection string exceeds maximum allowed length of {0}.", new object[]
			{
				1024
			}));
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x00093734 File Offset: 0x00091934
		internal static ArgumentException GetSchemaRestrictionRequired()
		{
			return ADP.Argument(SR.GetString("The ODBC managed provider requires that the TABLE_NAME restriction be specified and non-null for the GetSchema indexes collection."));
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x00093745 File Offset: 0x00091945
		internal static ArgumentOutOfRangeException NotSupportedEnumerationValue(Type type, int value)
		{
			return ADP.ArgumentOutOfRange(SR.GetString("The {0} enumeration value, {1}, is not supported by the .Net Framework Odbc Data Provider.", new object[]
			{
				type.Name,
				value.ToString(CultureInfo.InvariantCulture)
			}), type.Name);
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x0009377A File Offset: 0x0009197A
		internal static ArgumentOutOfRangeException NotSupportedCommandType(CommandType value)
		{
			return ODBC.NotSupportedEnumerationValue(typeof(CommandType), (int)value);
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x0009378C File Offset: 0x0009198C
		internal static ArgumentOutOfRangeException NotSupportedIsolationLevel(IsolationLevel value)
		{
			return ODBC.NotSupportedEnumerationValue(typeof(IsolationLevel), (int)value);
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x0009379E File Offset: 0x0009199E
		internal static InvalidOperationException NoMappingForSqlTransactionLevel(int value)
		{
			return ADP.DataAdapter(SR.GetString("No valid mapping for a SQL_TRANSACTION '{0}' to a System.Data.IsolationLevel enumeration value.", new object[]
			{
				value.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x000937C4 File Offset: 0x000919C4
		internal static Exception NegativeArgument()
		{
			return ADP.Argument(SR.GetString("Invalid negative argument!"));
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x000937D5 File Offset: 0x000919D5
		internal static Exception CantSetPropertyOnOpenConnection()
		{
			return ADP.InvalidOperation(SR.GetString("Can't set property on an open connection."));
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x000937E6 File Offset: 0x000919E6
		internal static Exception CantEnableConnectionpooling(ODBC32.RetCode retcode)
		{
			return ADP.DataAdapter(SR.GetString("{0} - unable to enable connection pooling...", new object[]
			{
				ODBC32.RetcodeToString(retcode)
			}));
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x00093806 File Offset: 0x00091A06
		internal static Exception CantAllocateEnvironmentHandle(ODBC32.RetCode retcode)
		{
			return ADP.DataAdapter(SR.GetString("{0} - unable to allocate an environment handle.", new object[]
			{
				ODBC32.RetcodeToString(retcode)
			}));
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x00093826 File Offset: 0x00091A26
		internal static Exception FailedToGetDescriptorHandle(ODBC32.RetCode retcode)
		{
			return ADP.DataAdapter(SR.GetString("{0} - unable to get descriptor handle.", new object[]
			{
				ODBC32.RetcodeToString(retcode)
			}));
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x00093846 File Offset: 0x00091A46
		internal static Exception NotInTransaction()
		{
			return ADP.InvalidOperation(SR.GetString("Not in a transaction"));
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x00093857 File Offset: 0x00091A57
		internal static Exception UnknownOdbcType(OdbcType odbctype)
		{
			return ADP.InvalidEnumerationValue(typeof(OdbcType), (int)odbctype);
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x00007EED File Offset: 0x000060ED
		internal static void TraceODBC(int level, string method, ODBC32.RetCode retcode)
		{
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x00093869 File Offset: 0x00091A69
		internal static short ShortStringLength(string inputString)
		{
			return checked((short)ADP.StringLength(inputString));
		}

		// Token: 0x040015B1 RID: 5553
		internal const string Pwd = "pwd";
	}
}
