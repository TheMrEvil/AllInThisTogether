using System;

namespace UnityEngine.Profiling.Memory.Experimental
{
	// Token: 0x0200027E RID: 638
	[Flags]
	public enum CaptureFlags : uint
	{
		// Token: 0x04000919 RID: 2329
		ManagedObjects = 1U,
		// Token: 0x0400091A RID: 2330
		NativeObjects = 2U,
		// Token: 0x0400091B RID: 2331
		NativeAllocations = 4U,
		// Token: 0x0400091C RID: 2332
		NativeAllocationSites = 8U,
		// Token: 0x0400091D RID: 2333
		NativeStackTraces = 16U
	}
}
