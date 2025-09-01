using System;
using System.Data;
using System.Globalization;
using System.Security;
using System.Threading;

// Token: 0x02000002 RID: 2
internal static class DataSetUtil
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	internal static void CheckArgumentNull<T>(T argumentValue, string argumentName) where T : class
	{
		if (argumentValue == null)
		{
			throw DataSetUtil.ArgumentNull(argumentName);
		}
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002061 File Offset: 0x00000261
	private static T TraceException<T>(string trace, T e)
	{
		return e;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002064 File Offset: 0x00000264
	private static T TraceExceptionAsReturnValue<T>(T e)
	{
		return DataSetUtil.TraceException<T>("<comm.ADP.TraceException|ERR|THROW> '%ls'\n", e);
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00002071 File Offset: 0x00000271
	internal static ArgumentException Argument(string message)
	{
		return DataSetUtil.TraceExceptionAsReturnValue<ArgumentException>(new ArgumentException(message));
	}

	// Token: 0x06000005 RID: 5 RVA: 0x0000207E File Offset: 0x0000027E
	internal static ArgumentNullException ArgumentNull(string message)
	{
		return DataSetUtil.TraceExceptionAsReturnValue<ArgumentNullException>(new ArgumentNullException(message));
	}

	// Token: 0x06000006 RID: 6 RVA: 0x0000208B File Offset: 0x0000028B
	internal static ArgumentOutOfRangeException ArgumentOutOfRange(string message, string parameterName)
	{
		return DataSetUtil.TraceExceptionAsReturnValue<ArgumentOutOfRangeException>(new ArgumentOutOfRangeException(parameterName, message));
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002099 File Offset: 0x00000299
	internal static InvalidCastException InvalidCast(string message)
	{
		return DataSetUtil.TraceExceptionAsReturnValue<InvalidCastException>(new InvalidCastException(message));
	}

	// Token: 0x06000008 RID: 8 RVA: 0x000020A6 File Offset: 0x000002A6
	internal static InvalidOperationException InvalidOperation(string message)
	{
		return DataSetUtil.TraceExceptionAsReturnValue<InvalidOperationException>(new InvalidOperationException(message));
	}

	// Token: 0x06000009 RID: 9 RVA: 0x000020B3 File Offset: 0x000002B3
	internal static NotSupportedException NotSupported(string message)
	{
		return DataSetUtil.TraceExceptionAsReturnValue<NotSupportedException>(new NotSupportedException(message));
	}

	// Token: 0x0600000A RID: 10 RVA: 0x000020C0 File Offset: 0x000002C0
	internal static ArgumentOutOfRangeException InvalidEnumerationValue(Type type, int value)
	{
		return DataSetUtil.ArgumentOutOfRange(string.Format("The {0} enumeration value, {1}, is not valid.", type.Name, value.ToString(CultureInfo.InvariantCulture)), type.Name);
	}

	// Token: 0x0600000B RID: 11 RVA: 0x000020E9 File Offset: 0x000002E9
	internal static ArgumentOutOfRangeException InvalidDataRowState(DataRowState value)
	{
		return DataSetUtil.InvalidEnumerationValue(typeof(DataRowState), (int)value);
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000020FB File Offset: 0x000002FB
	internal static ArgumentOutOfRangeException InvalidLoadOption(LoadOption value)
	{
		return DataSetUtil.InvalidEnumerationValue(typeof(LoadOption), (int)value);
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00002110 File Offset: 0x00000310
	internal static bool IsCatchableExceptionType(Exception e)
	{
		Type type = e.GetType();
		return type != DataSetUtil.s_stackOverflowType && type != DataSetUtil.s_outOfMemoryType && type != DataSetUtil.s_threadAbortType && type != DataSetUtil.s_nullReferenceType && type != DataSetUtil.s_accessViolationType && !DataSetUtil.s_securityType.IsAssignableFrom(type);
	}

	// Token: 0x0600000E RID: 14 RVA: 0x00002178 File Offset: 0x00000378
	// Note: this type is marked as 'beforefieldinit'.
	static DataSetUtil()
	{
	}

	// Token: 0x04000001 RID: 1
	private static readonly Type s_stackOverflowType = typeof(StackOverflowException);

	// Token: 0x04000002 RID: 2
	private static readonly Type s_outOfMemoryType = typeof(OutOfMemoryException);

	// Token: 0x04000003 RID: 3
	private static readonly Type s_threadAbortType = typeof(ThreadAbortException);

	// Token: 0x04000004 RID: 4
	private static readonly Type s_nullReferenceType = typeof(NullReferenceException);

	// Token: 0x04000005 RID: 5
	private static readonly Type s_accessViolationType = typeof(AccessViolationException);

	// Token: 0x04000006 RID: 6
	private static readonly Type s_securityType = typeof(SecurityException);
}
