using System;

namespace UnityEngine.ProBuilder
{
	// Token: 0x0200003F RID: 63
	[Flags]
	public enum MeshArrays
	{
		// Token: 0x04000165 RID: 357
		Position = 1,
		// Token: 0x04000166 RID: 358
		Texture0 = 2,
		// Token: 0x04000167 RID: 359
		Texture1 = 4,
		// Token: 0x04000168 RID: 360
		Lightmap = 4,
		// Token: 0x04000169 RID: 361
		Texture2 = 8,
		// Token: 0x0400016A RID: 362
		Texture3 = 16,
		// Token: 0x0400016B RID: 363
		Color = 32,
		// Token: 0x0400016C RID: 364
		Normal = 64,
		// Token: 0x0400016D RID: 365
		Tangent = 128,
		// Token: 0x0400016E RID: 366
		All = 255
	}
}
