using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003D4 RID: 980
	[Flags]
	public enum CopyTextureSupport
	{
		// Token: 0x04000BEB RID: 3051
		None = 0,
		// Token: 0x04000BEC RID: 3052
		Basic = 1,
		// Token: 0x04000BED RID: 3053
		Copy3D = 2,
		// Token: 0x04000BEE RID: 3054
		DifferentTypes = 4,
		// Token: 0x04000BEF RID: 3055
		TextureToRT = 8,
		// Token: 0x04000BF0 RID: 3056
		RTToTexture = 16
	}
}
