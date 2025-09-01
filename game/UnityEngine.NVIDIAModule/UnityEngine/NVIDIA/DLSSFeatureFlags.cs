using System;

namespace UnityEngine.NVIDIA
{
	// Token: 0x02000005 RID: 5
	[Flags]
	public enum DLSSFeatureFlags
	{
		// Token: 0x04000002 RID: 2
		None = 0,
		// Token: 0x04000003 RID: 3
		IsHDR = 1,
		// Token: 0x04000004 RID: 4
		MVLowRes = 2,
		// Token: 0x04000005 RID: 5
		MVJittered = 4,
		// Token: 0x04000006 RID: 6
		DepthInverted = 8,
		// Token: 0x04000007 RID: 7
		DoSharpening = 16
	}
}
