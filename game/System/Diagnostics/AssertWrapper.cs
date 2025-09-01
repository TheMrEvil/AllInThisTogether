using System;

namespace System.Diagnostics
{
	// Token: 0x02000248 RID: 584
	internal class AssertWrapper
	{
		// Token: 0x0600120F RID: 4623 RVA: 0x0004E27A File Offset: 0x0004C47A
		public static void ShowAssert(string stackTrace, StackFrame frame, string message, string detailMessage)
		{
			new DefaultTraceListener().Fail(message, detailMessage);
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x0000219B File Offset: 0x0000039B
		public AssertWrapper()
		{
		}
	}
}
