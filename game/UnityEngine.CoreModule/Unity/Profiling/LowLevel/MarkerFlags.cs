using System;

namespace Unity.Profiling.LowLevel
{
	// Token: 0x0200004F RID: 79
	[Flags]
	public enum MarkerFlags : ushort
	{
		// Token: 0x0400013C RID: 316
		Default = 0,
		// Token: 0x0400013D RID: 317
		Script = 2,
		// Token: 0x0400013E RID: 318
		ScriptInvoke = 32,
		// Token: 0x0400013F RID: 319
		ScriptDeepProfiler = 64,
		// Token: 0x04000140 RID: 320
		AvailabilityEditor = 4,
		// Token: 0x04000141 RID: 321
		AvailabilityNonDevelopment = 8,
		// Token: 0x04000142 RID: 322
		Warning = 16,
		// Token: 0x04000143 RID: 323
		Counter = 128,
		// Token: 0x04000144 RID: 324
		SampleGPU = 256
	}
}
