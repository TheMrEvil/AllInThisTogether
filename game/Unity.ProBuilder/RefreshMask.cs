using System;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000041 RID: 65
	[Flags]
	public enum RefreshMask
	{
		// Token: 0x04000174 RID: 372
		UV = 1,
		// Token: 0x04000175 RID: 373
		Colors = 2,
		// Token: 0x04000176 RID: 374
		Normals = 4,
		// Token: 0x04000177 RID: 375
		Tangents = 8,
		// Token: 0x04000178 RID: 376
		Collisions = 16,
		// Token: 0x04000179 RID: 377
		Bounds = 22,
		// Token: 0x0400017A RID: 378
		All = 31
	}
}
