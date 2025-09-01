using System;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.Diagnostics
{
	// Token: 0x020000FD RID: 253
	internal static class CompositionTraceSource
	{
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x00014BAF File Offset: 0x00012DAF
		public static bool CanWriteInformation
		{
			get
			{
				return CompositionTraceSource.Source.CanWriteInformation;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x00014BBB File Offset: 0x00012DBB
		public static bool CanWriteWarning
		{
			get
			{
				return CompositionTraceSource.Source.CanWriteWarning;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x00014BC7 File Offset: 0x00012DC7
		public static bool CanWriteError
		{
			get
			{
				return CompositionTraceSource.Source.CanWriteError;
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00014BD3 File Offset: 0x00012DD3
		public static void WriteInformation(CompositionTraceId traceId, string format, params object[] arguments)
		{
			CompositionTraceSource.EnsureEnabled(CompositionTraceSource.CanWriteInformation);
			CompositionTraceSource.Source.WriteInformation(traceId, format, arguments);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00014BEC File Offset: 0x00012DEC
		public static void WriteWarning(CompositionTraceId traceId, string format, params object[] arguments)
		{
			CompositionTraceSource.EnsureEnabled(CompositionTraceSource.CanWriteWarning);
			CompositionTraceSource.Source.WriteWarning(traceId, format, arguments);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00014C05 File Offset: 0x00012E05
		public static void WriteError(CompositionTraceId traceId, string format, params object[] arguments)
		{
			CompositionTraceSource.EnsureEnabled(CompositionTraceSource.CanWriteError);
			CompositionTraceSource.Source.WriteError(traceId, format, arguments);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00014C1E File Offset: 0x00012E1E
		private static void EnsureEnabled(bool condition)
		{
			Assumes.IsTrue(condition, "To avoid unnecessary work when a trace level has not been enabled, check CanWriteXXX before calling this method.");
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00014C2B File Offset: 0x00012E2B
		// Note: this type is marked as 'beforefieldinit'.
		static CompositionTraceSource()
		{
		}

		// Token: 0x040002DF RID: 735
		private static readonly DebuggerTraceWriter Source = new DebuggerTraceWriter();
	}
}
