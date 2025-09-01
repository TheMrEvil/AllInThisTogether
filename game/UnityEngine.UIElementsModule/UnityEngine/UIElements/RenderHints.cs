using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000054 RID: 84
	[Flags]
	internal enum RenderHints
	{
		// Token: 0x040000FA RID: 250
		None = 0,
		// Token: 0x040000FB RID: 251
		GroupTransform = 1,
		// Token: 0x040000FC RID: 252
		BoneTransform = 2,
		// Token: 0x040000FD RID: 253
		ClipWithScissors = 4,
		// Token: 0x040000FE RID: 254
		MaskContainer = 8,
		// Token: 0x040000FF RID: 255
		DynamicColor = 16,
		// Token: 0x04000100 RID: 256
		DirtyOffset = 5,
		// Token: 0x04000101 RID: 257
		DirtyGroupTransform = 32,
		// Token: 0x04000102 RID: 258
		DirtyBoneTransform = 64,
		// Token: 0x04000103 RID: 259
		DirtyClipWithScissors = 128,
		// Token: 0x04000104 RID: 260
		DirtyMaskContainer = 256,
		// Token: 0x04000105 RID: 261
		DirtyDynamicColor = 512,
		// Token: 0x04000106 RID: 262
		DirtyAll = 992
	}
}
