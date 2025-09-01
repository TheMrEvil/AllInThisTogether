using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003B6 RID: 950
	public enum CameraEvent
	{
		// Token: 0x04000AE5 RID: 2789
		BeforeDepthTexture,
		// Token: 0x04000AE6 RID: 2790
		AfterDepthTexture,
		// Token: 0x04000AE7 RID: 2791
		BeforeDepthNormalsTexture,
		// Token: 0x04000AE8 RID: 2792
		AfterDepthNormalsTexture,
		// Token: 0x04000AE9 RID: 2793
		BeforeGBuffer,
		// Token: 0x04000AEA RID: 2794
		AfterGBuffer,
		// Token: 0x04000AEB RID: 2795
		BeforeLighting,
		// Token: 0x04000AEC RID: 2796
		AfterLighting,
		// Token: 0x04000AED RID: 2797
		BeforeFinalPass,
		// Token: 0x04000AEE RID: 2798
		AfterFinalPass,
		// Token: 0x04000AEF RID: 2799
		BeforeForwardOpaque,
		// Token: 0x04000AF0 RID: 2800
		AfterForwardOpaque,
		// Token: 0x04000AF1 RID: 2801
		BeforeImageEffectsOpaque,
		// Token: 0x04000AF2 RID: 2802
		AfterImageEffectsOpaque,
		// Token: 0x04000AF3 RID: 2803
		BeforeSkybox,
		// Token: 0x04000AF4 RID: 2804
		AfterSkybox,
		// Token: 0x04000AF5 RID: 2805
		BeforeForwardAlpha,
		// Token: 0x04000AF6 RID: 2806
		AfterForwardAlpha,
		// Token: 0x04000AF7 RID: 2807
		BeforeImageEffects,
		// Token: 0x04000AF8 RID: 2808
		AfterImageEffects,
		// Token: 0x04000AF9 RID: 2809
		AfterEverything,
		// Token: 0x04000AFA RID: 2810
		BeforeReflections,
		// Token: 0x04000AFB RID: 2811
		AfterReflections,
		// Token: 0x04000AFC RID: 2812
		BeforeHaloAndLensFlares,
		// Token: 0x04000AFD RID: 2813
		AfterHaloAndLensFlares
	}
}
