using System;
using System.Diagnostics;

namespace System
{
	// Token: 0x02000219 RID: 537
	internal static class BCLDebug
	{
		// Token: 0x06001812 RID: 6162 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[Conditional("_DEBUG")]
		public static void Assert(bool condition, string message)
		{
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[Conditional("_DEBUG")]
		internal static void Correctness(bool expr, string msg)
		{
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[Conditional("_DEBUG")]
		public static void Log(string message)
		{
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[Conditional("_DEBUG")]
		public static void Log(string switchName, string message)
		{
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[Conditional("_DEBUG")]
		public static void Log(string switchName, BCLDebugLogLevel level, params object[] messages)
		{
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[Conditional("_DEBUG")]
		internal static void Perf(bool expr, string msg)
		{
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[Conditional("_LOGGING")]
		public static void Trace(string switchName, params object[] messages)
		{
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		internal static bool CheckEnabled(string switchName)
		{
			return false;
		}
	}
}
