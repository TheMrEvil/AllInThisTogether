using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000046 RID: 70
	[PostProcess(typeof(SunshaftsRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Environment/Sun Shafts", true)]
	[Serializable]
	public sealed class Sunshafts : PostProcessEffectSettings
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x00007914 File Offset: 0x00005B14
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.sunShaftIntensity != 0f && this.length != 0f;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000794C File Offset: 0x00005B4C
		public Sunshafts()
		{
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00007A4C File Offset: 0x00005C4C
		// Note: this type is marked as 'beforefieldinit'.
		static Sunshafts()
		{
		}

		// Token: 0x04000147 RID: 327
		[Tooltip("Use the color of the Directional Light that's set as the caster")]
		public BoolParameter useCasterColor = new BoolParameter
		{
			value = true
		};

		// Token: 0x04000148 RID: 328
		[Tooltip("Use the intensity of the Directional Light that's set as the caster")]
		public BoolParameter useCasterIntensity = new BoolParameter
		{
			value = false
		};

		// Token: 0x04000149 RID: 329
		[DisplayName("Intensity")]
		public FloatParameter sunShaftIntensity = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x0400014A RID: 330
		[Tooltip("Additive mode adds the sunshaft color to the image, while Screen mode perserves color values")]
		public Sunshafts.SunShaftsSourceParameter blendMode = new Sunshafts.SunShaftsSourceParameter
		{
			value = SunshaftsBase.BlendMode.Screen
		};

		// Token: 0x0400014B RID: 331
		[DisplayName("Resolution")]
		[Tooltip("Low, quater resolution\n\nNormal, half resolution\n\nHigh, full resolution\n\nLower resolutions may induce jittering")]
		public Sunshafts.SunShaftsResolutionParameter resolution = new Sunshafts.SunShaftsResolutionParameter
		{
			value = SunshaftsBase.SunShaftsResolution.Normal
		};

		// Token: 0x0400014C RID: 332
		[Tooltip("Enabling this option doubles the amount of blurring performed. Resulting in smoother sunshafts at a higher performance cost.")]
		public BoolParameter highQuality = new BoolParameter
		{
			value = false
		};

		// Token: 0x0400014D RID: 333
		[Tooltip("Any color values over this threshold will contribute to the sunshafts effect")]
		[DisplayName("Sky color threshold")]
		public ColorParameter sunThreshold = new ColorParameter
		{
			value = Color.black
		};

		// Token: 0x0400014E RID: 334
		[DisplayName("Color")]
		public ColorParameter sunColor = new ColorParameter
		{
			value = new Color(1f, 1f, 1f)
		};

		// Token: 0x0400014F RID: 335
		[Range(0.1f, 1f)]
		[Tooltip("The degree to which the shafts’ brightness diminishes with distance from the caster")]
		public FloatParameter falloff = new FloatParameter
		{
			value = 0.5f
		};

		// Token: 0x04000150 RID: 336
		[Tooltip("The length of the sunrays from the caster's position to the camera")]
		[UnityEngine.Rendering.PostProcessing.Min(0f)]
		public FloatParameter length = new FloatParameter
		{
			value = 10f
		};

		// Token: 0x04000151 RID: 337
		[Range(0f, 1f)]
		public FloatParameter noiseStrength = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000152 RID: 338
		public static Vector3 sunPosition = Vector3.zero;

		// Token: 0x0200007E RID: 126
		[Serializable]
		public sealed class SunShaftsSourceParameter : ParameterOverride<SunshaftsBase.BlendMode>
		{
			// Token: 0x06000104 RID: 260 RVA: 0x00008588 File Offset: 0x00006788
			public SunShaftsSourceParameter()
			{
			}
		}

		// Token: 0x0200007F RID: 127
		[Serializable]
		public sealed class SunShaftsResolutionParameter : ParameterOverride<SunshaftsBase.SunShaftsResolution>
		{
			// Token: 0x06000105 RID: 261 RVA: 0x00008590 File Offset: 0x00006790
			public SunShaftsResolutionParameter()
			{
			}
		}
	}
}
