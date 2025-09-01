using System;

namespace System.ComponentModel.Composition.Diagnostics
{
	// Token: 0x02000100 RID: 256
	internal abstract class TraceWriter
	{
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060006BB RID: 1723
		public abstract bool CanWriteInformation { get; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060006BC RID: 1724
		public abstract bool CanWriteWarning { get; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060006BD RID: 1725
		public abstract bool CanWriteError { get; }

		// Token: 0x060006BE RID: 1726
		public abstract void WriteInformation(CompositionTraceId traceId, string format, params object[] arguments);

		// Token: 0x060006BF RID: 1727
		public abstract void WriteWarning(CompositionTraceId traceId, string format, params object[] arguments);

		// Token: 0x060006C0 RID: 1728
		public abstract void WriteError(CompositionTraceId traceId, string format, params object[] arguments);

		// Token: 0x060006C1 RID: 1729 RVA: 0x00002BAC File Offset: 0x00000DAC
		protected TraceWriter()
		{
		}
	}
}
