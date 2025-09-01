using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003B9 RID: 953
	[Flags]
	public enum ShadowMapPass
	{
		// Token: 0x04000B08 RID: 2824
		PointlightPositiveX = 1,
		// Token: 0x04000B09 RID: 2825
		PointlightNegativeX = 2,
		// Token: 0x04000B0A RID: 2826
		PointlightPositiveY = 4,
		// Token: 0x04000B0B RID: 2827
		PointlightNegativeY = 8,
		// Token: 0x04000B0C RID: 2828
		PointlightPositiveZ = 16,
		// Token: 0x04000B0D RID: 2829
		PointlightNegativeZ = 32,
		// Token: 0x04000B0E RID: 2830
		DirectionalCascade0 = 64,
		// Token: 0x04000B0F RID: 2831
		DirectionalCascade1 = 128,
		// Token: 0x04000B10 RID: 2832
		DirectionalCascade2 = 256,
		// Token: 0x04000B11 RID: 2833
		DirectionalCascade3 = 512,
		// Token: 0x04000B12 RID: 2834
		Spotlight = 1024,
		// Token: 0x04000B13 RID: 2835
		Pointlight = 63,
		// Token: 0x04000B14 RID: 2836
		Directional = 960,
		// Token: 0x04000B15 RID: 2837
		All = 2047
	}
}
