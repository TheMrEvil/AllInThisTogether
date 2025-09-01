using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000015 RID: 21
	[PostProcess(typeof(AutoExposureRenderer), "Unity/Auto Exposure", true)]
	[Serializable]
	public sealed class AutoExposure : PostProcessEffectSettings
	{
		// Token: 0x06000023 RID: 35 RVA: 0x000025A8 File Offset: 0x000007A8
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && SystemInfo.supportsComputeShaders && !RuntimeUtilities.isAndroidOpenGL && !RuntimeUtilities.isWebNonWebGPU && RenderTextureFormat.RFloat.IsSupported() && context.resources.computeShaders.autoExposure && context.resources.computeShaders.exposureHistogram;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002610 File Offset: 0x00000810
		public AutoExposure()
		{
		}

		// Token: 0x0400003B RID: 59
		[MinMax(1f, 99f)]
		[DisplayName("Filtering (%)")]
		[Tooltip("Filters the bright and dark parts of the histogram when computing the average luminance. This is to avoid very dark pixels and very bright pixels from contributing to the auto exposure. Unit is in percent.")]
		public Vector2Parameter filtering = new Vector2Parameter
		{
			value = new Vector2(50f, 95f)
		};

		// Token: 0x0400003C RID: 60
		[Range(-9f, 9f)]
		[DisplayName("Minimum (EV)")]
		[Tooltip("Minimum average luminance to consider for auto exposure. Unit is EV.")]
		public FloatParameter minLuminance = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x0400003D RID: 61
		[Range(-9f, 9f)]
		[DisplayName("Maximum (EV)")]
		[Tooltip("Maximum average luminance to consider for auto exposure. Unit is EV.")]
		public FloatParameter maxLuminance = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x0400003E RID: 62
		[Min(0f)]
		[DisplayName("Exposure Compensation")]
		[Tooltip("Use this to scale the global exposure of the scene.")]
		public FloatParameter keyValue = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x0400003F RID: 63
		[DisplayName("Type")]
		[Tooltip("Use \"Progressive\" if you want auto exposure to be animated. Use \"Fixed\" otherwise.")]
		public EyeAdaptationParameter eyeAdaptation = new EyeAdaptationParameter
		{
			value = EyeAdaptation.Progressive
		};

		// Token: 0x04000040 RID: 64
		[Min(0f)]
		[Tooltip("Adaptation speed from a dark to a light environment.")]
		public FloatParameter speedUp = new FloatParameter
		{
			value = 2f
		};

		// Token: 0x04000041 RID: 65
		[Min(0f)]
		[Tooltip("Adaptation speed from a light to a dark environment.")]
		public FloatParameter speedDown = new FloatParameter
		{
			value = 1f
		};
	}
}
