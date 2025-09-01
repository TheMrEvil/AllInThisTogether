using System;

namespace UnityEngine.VFX
{
	// Token: 0x0200000A RID: 10
	internal enum VFXSystemFlag
	{
		// Token: 0x040000CA RID: 202
		SystemDefault,
		// Token: 0x040000CB RID: 203
		SystemHasKill,
		// Token: 0x040000CC RID: 204
		SystemHasIndirectBuffer,
		// Token: 0x040000CD RID: 205
		SystemReceivedEventGPU = 4,
		// Token: 0x040000CE RID: 206
		SystemHasStrips = 8,
		// Token: 0x040000CF RID: 207
		SystemNeedsComputeBounds = 16,
		// Token: 0x040000D0 RID: 208
		SystemAutomaticBounds = 32,
		// Token: 0x040000D1 RID: 209
		SystemInWorldSpace = 64,
		// Token: 0x040000D2 RID: 210
		SystemHasDirectLink = 128
	}
}
