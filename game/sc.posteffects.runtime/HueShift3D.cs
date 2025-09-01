using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x0200001E RID: 30
	[PostProcess(typeof(HueShift3DRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Image/3D Hue Shift", true)]
	[Serializable]
	public sealed class HueShift3D : PostProcessEffectSettings
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00005060 File Offset: 0x00003260
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.intensity != 0f;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00005088 File Offset: 0x00003288
		public HueShift3D()
		{
		}

		// Token: 0x04000097 RID: 151
		[DisplayName("Method")]
		[Tooltip("Box blurring uses fewer texture samples but has a limited blur range")]
		public HueShift3D.ColorSourceParameter colorSource = new HueShift3D.ColorSourceParameter
		{
			value = HueShift3D.ColorSource.RGBSpectrum
		};

		// Token: 0x04000098 RID: 152
		[DisplayName("Gradient Texture")]
		public TextureParameter gradientTex = new TextureParameter
		{
			value = null
		};

		// Token: 0x04000099 RID: 153
		[Range(0f, 1f)]
		[DisplayName("Opacity")]
		public FloatParameter intensity = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x0400009A RID: 154
		[Range(0f, 1f)]
		[Tooltip("Speed")]
		public FloatParameter speed = new FloatParameter
		{
			value = 0.3f
		};

		// Token: 0x0400009B RID: 155
		[Range(0f, 3f)]
		[Tooltip("Size")]
		public FloatParameter size = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x0400009C RID: 156
		[DisplayName("Geometry normal influence")]
		[Range(0f, 10f)]
		[Tooltip("Bends the effect over the scene's geometry normals\n\nHigh values may induce banding artifacts")]
		public FloatParameter geoInfluence = new FloatParameter
		{
			value = 5f
		};

		// Token: 0x0400009D RID: 157
		public static bool isOrtho;

		// Token: 0x02000067 RID: 103
		public enum ColorSource
		{
			// Token: 0x0400019F RID: 415
			RGBSpectrum,
			// Token: 0x040001A0 RID: 416
			GradientTexture
		}

		// Token: 0x02000068 RID: 104
		[Serializable]
		public sealed class ColorSourceParameter : ParameterOverride<HueShift3D.ColorSource>
		{
			// Token: 0x060000FA RID: 250 RVA: 0x00008538 File Offset: 0x00006738
			public ColorSourceParameter()
			{
			}
		}
	}
}
