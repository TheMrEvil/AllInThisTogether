using System;
using System.Runtime.Diagnostics;

namespace System.Runtime.Serialization
{
	// Token: 0x0200015C RID: 348
	internal static class FxTrace
	{
		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x00047158 File Offset: 0x00045358
		public static EtwDiagnosticTrace Trace
		{
			get
			{
				return Fx.Trace;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001260 RID: 4704 RVA: 0x0004715F File Offset: 0x0004535F
		public static ExceptionTrace Exception
		{
			get
			{
				return new ExceptionTrace("System.Runtime.Serialization", FxTrace.Trace);
			}
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x00003127 File Offset: 0x00001327
		public static bool IsEventEnabled(int index)
		{
			return false;
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0000A8EE File Offset: 0x00008AEE
		public static void UpdateEventDefinitions(EventDescriptor[] ed, ushort[] events)
		{
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00047170 File Offset: 0x00045370
		// Note: this type is marked as 'beforefieldinit'.
		static FxTrace()
		{
		}

		// Token: 0x04000749 RID: 1865
		public static bool ShouldTraceError = true;

		// Token: 0x0400074A RID: 1866
		public static bool ShouldTraceVerbose = true;
	}
}
