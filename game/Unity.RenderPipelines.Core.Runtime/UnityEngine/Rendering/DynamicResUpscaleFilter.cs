using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000053 RID: 83
	public enum DynamicResUpscaleFilter : byte
	{
		// Token: 0x040001E3 RID: 483
		[Obsolete("Bilinear upscale filter is considered obsolete and is not supported anymore, please use CatmullRom for a very cheap, but blurry filter.", false)]
		Bilinear,
		// Token: 0x040001E4 RID: 484
		CatmullRom,
		// Token: 0x040001E5 RID: 485
		[Obsolete("Lanczos upscale filter is considered obsolete and is not supported anymore, please use Contrast Adaptive Sharpening for very sharp filter or FidelityFX Super Resolution 1.0.", false)]
		Lanczos,
		// Token: 0x040001E6 RID: 486
		ContrastAdaptiveSharpen,
		// Token: 0x040001E7 RID: 487
		[InspectorName("FidelityFX Super Resolution 1.0")]
		EdgeAdaptiveScalingUpres,
		// Token: 0x040001E8 RID: 488
		[InspectorName("TAA Upscale")]
		TAAU
	}
}
