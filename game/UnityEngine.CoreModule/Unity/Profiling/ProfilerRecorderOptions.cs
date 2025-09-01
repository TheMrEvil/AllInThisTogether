using System;

namespace Unity.Profiling
{
	// Token: 0x02000049 RID: 73
	[Flags]
	public enum ProfilerRecorderOptions
	{
		// Token: 0x04000123 RID: 291
		None = 0,
		// Token: 0x04000124 RID: 292
		StartImmediately = 1,
		// Token: 0x04000125 RID: 293
		KeepAliveDuringDomainReload = 2,
		// Token: 0x04000126 RID: 294
		CollectOnlyOnCurrentThread = 4,
		// Token: 0x04000127 RID: 295
		WrapAroundWhenCapacityReached = 8,
		// Token: 0x04000128 RID: 296
		SumAllSamplesInFrame = 16,
		// Token: 0x04000129 RID: 297
		GpuRecorder = 64,
		// Token: 0x0400012A RID: 298
		Default = 24
	}
}
