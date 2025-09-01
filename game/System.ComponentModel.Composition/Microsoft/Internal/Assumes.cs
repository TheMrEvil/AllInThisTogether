using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security;

namespace Microsoft.Internal
{
	// Token: 0x02000003 RID: 3
	internal static class Assumes
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[DebuggerStepThrough]
		internal static void NotNull<T>(T value) where T : class
		{
			Assumes.IsTrue(value != null);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002060 File Offset: 0x00000260
		[DebuggerStepThrough]
		internal static void NotNull<T1, T2>(T1 value1, T2 value2) where T1 : class where T2 : class
		{
			Assumes.NotNull<T1>(value1);
			Assumes.NotNull<T2>(value2);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000206E File Offset: 0x0000026E
		[DebuggerStepThrough]
		internal static void NotNull<T1, T2, T3>(T1 value1, T2 value2, T3 value3) where T1 : class where T2 : class where T3 : class
		{
			Assumes.NotNull<T1>(value1);
			Assumes.NotNull<T2>(value2);
			Assumes.NotNull<T3>(value3);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002082 File Offset: 0x00000282
		[DebuggerStepThrough]
		internal static void NotNullOrEmpty(string value)
		{
			Assumes.NotNull<string>(value);
			Assumes.IsTrue(value.Length > 0);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002098 File Offset: 0x00000298
		[DebuggerStepThrough]
		internal static void IsTrue(bool condition)
		{
			if (!condition)
			{
				throw Assumes.UncatchableException(null);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020A4 File Offset: 0x000002A4
		[DebuggerStepThrough]
		internal static void IsTrue(bool condition, string message)
		{
			if (!condition)
			{
				throw Assumes.UncatchableException(message);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020B0 File Offset: 0x000002B0
		[DebuggerStepThrough]
		internal static T NotReachable<T>()
		{
			throw Assumes.UncatchableException("Code path should never be reached!");
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020BC File Offset: 0x000002BC
		[DebuggerStepThrough]
		private static Exception UncatchableException(string message)
		{
			return new Assumes.InternalErrorException(message);
		}

		// Token: 0x02000004 RID: 4
		[Serializable]
		private class InternalErrorException : Exception
		{
			// Token: 0x06000009 RID: 9 RVA: 0x000020C4 File Offset: 0x000002C4
			public InternalErrorException(string message) : base(string.Format(CultureInfo.CurrentCulture, Strings.InternalExceptionMessage, message))
			{
			}

			// Token: 0x0600000A RID: 10 RVA: 0x000020DC File Offset: 0x000002DC
			[SecuritySafeCritical]
			protected InternalErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
			{
			}
		}
	}
}
