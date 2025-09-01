using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200002F RID: 47
	[PostProcess(typeof(MotionBlurRenderer), "Unity/Motion Blur", false)]
	[Serializable]
	public sealed class MotionBlur : PostProcessEffectSettings
	{
		// Token: 0x0600008A RID: 138 RVA: 0x00007495 File Offset: 0x00005695
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.shutterAngle.value > 0f && SystemInfo.supportsMotionVectors && RenderTextureFormat.RGHalf.IsSupported() && !context.stereoActive;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000074D1 File Offset: 0x000056D1
		public MotionBlur()
		{
		}

		// Token: 0x040000FD RID: 253
		[Range(0f, 360f)]
		[Tooltip("The angle of rotary shutter. Larger values give longer exposure.")]
		public FloatParameter shutterAngle = new FloatParameter
		{
			value = 270f
		};

		// Token: 0x040000FE RID: 254
		[Range(4f, 32f)]
		[Tooltip("The amount of sample points. This affects quality and performance.")]
		public IntParameter sampleCount = new IntParameter
		{
			value = 10
		};
	}
}
