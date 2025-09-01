using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003F5 RID: 1013
	[Flags]
	public enum CullingOptions
	{
		// Token: 0x04000CA3 RID: 3235
		None = 0,
		// Token: 0x04000CA4 RID: 3236
		ForceEvenIfCameraIsNotActive = 1,
		// Token: 0x04000CA5 RID: 3237
		OcclusionCull = 2,
		// Token: 0x04000CA6 RID: 3238
		NeedsLighting = 4,
		// Token: 0x04000CA7 RID: 3239
		NeedsReflectionProbes = 8,
		// Token: 0x04000CA8 RID: 3240
		Stereo = 16,
		// Token: 0x04000CA9 RID: 3241
		DisablePerObjectCulling = 32,
		// Token: 0x04000CAA RID: 3242
		ShadowCasters = 64
	}
}
