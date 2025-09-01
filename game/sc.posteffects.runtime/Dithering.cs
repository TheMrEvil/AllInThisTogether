using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000012 RID: 18
	[PostProcess(typeof(DitheringRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Retro/Dithering", true)]
	[Serializable]
	public sealed class Dithering : PostProcessEffectSettings
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00003503 File Offset: 0x00001703
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.intensity != 0f;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000352C File Offset: 0x0000172C
		public Dithering()
		{
		}

		// Token: 0x0400003E RID: 62
		[DisplayName("Pattern")]
		[Tooltip("Note that the texture's filter mode (Point or Bilinear) greatly affects the behavior of the pattern")]
		public TextureParameter lut = new TextureParameter
		{
			value = null
		};

		// Token: 0x0400003F RID: 63
		[Range(0f, 1f)]
		[Tooltip("Fades the effect in or out")]
		public FloatParameter intensity = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000040 RID: 64
		[Range(0f, 1f)]
		[Tooltip("The screen's luminance values control the density of the dithering matrix")]
		public FloatParameter luminanceThreshold = new FloatParameter
		{
			value = 0.5f
		};

		// Token: 0x04000041 RID: 65
		[Range(0f, 2f)]
		[DisplayName("Tiling")]
		public FloatParameter tiling = new FloatParameter
		{
			value = 1f
		};
	}
}
