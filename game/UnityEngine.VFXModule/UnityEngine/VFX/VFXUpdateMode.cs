using System;

namespace UnityEngine.VFX
{
	// Token: 0x0200000B RID: 11
	[Flags]
	internal enum VFXUpdateMode
	{
		// Token: 0x040000D4 RID: 212
		FixedDeltaTime = 0,
		// Token: 0x040000D5 RID: 213
		DeltaTime = 1,
		// Token: 0x040000D6 RID: 214
		IgnoreTimeScale = 2,
		// Token: 0x040000D7 RID: 215
		ExactFixedTimeStep = 4,
		// Token: 0x040000D8 RID: 216
		DeltaTimeAndIgnoreTimeScale = 3,
		// Token: 0x040000D9 RID: 217
		FixedDeltaAndExactTime = 4,
		// Token: 0x040000DA RID: 218
		FixedDeltaAndExactTimeAndIgnoreTimeScale = 6
	}
}
