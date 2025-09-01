using System;

namespace System.Diagnostics
{
	// Token: 0x020009BF RID: 2495
	internal static class DebugPrivate
	{
		// Token: 0x060059C3 RID: 22979 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[Conditional("DEBUG")]
		public static void Assert(bool condition)
		{
		}

		// Token: 0x060059C4 RID: 22980 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string message)
		{
		}

		// Token: 0x060059C5 RID: 22981 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string message, string detailMessage)
		{
		}

		// Token: 0x060059C6 RID: 22982 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string message, string detailMessageFormat, params object[] args)
		{
		}

		// Token: 0x060059C7 RID: 22983 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[Conditional("DEBUG")]
		public static void Fail(string message)
		{
		}

		// Token: 0x060059C8 RID: 22984 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[Conditional("DEBUG")]
		public static void Fail(string message, string detailMessage)
		{
		}
	}
}
