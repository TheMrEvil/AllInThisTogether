using System;

namespace UnityEngine
{
	// Token: 0x02000186 RID: 390
	[Flags]
	public enum MaterialGlobalIlluminationFlags
	{
		// Token: 0x04000574 RID: 1396
		None = 0,
		// Token: 0x04000575 RID: 1397
		RealtimeEmissive = 1,
		// Token: 0x04000576 RID: 1398
		BakedEmissive = 2,
		// Token: 0x04000577 RID: 1399
		EmissiveIsBlack = 4,
		// Token: 0x04000578 RID: 1400
		AnyEmissive = 3
	}
}
