using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200002D RID: 45
	[PostProcess(typeof(LensDistortionRenderer), "Unity/Lens Distortion", true)]
	[Serializable]
	public sealed class LensDistortion : PostProcessEffectSettings
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00007260 File Offset: 0x00005460
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && !Mathf.Approximately(this.intensity, 0f) && (this.intensityX > 0f || this.intensityY > 0f) && !context.stereoActive;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000072C0 File Offset: 0x000054C0
		public LensDistortion()
		{
		}

		// Token: 0x040000F7 RID: 247
		[Range(-100f, 100f)]
		[Tooltip("Total distortion amount.")]
		public FloatParameter intensity = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x040000F8 RID: 248
		[Range(0f, 1f)]
		[DisplayName("X Multiplier")]
		[Tooltip("Intensity multiplier on the x-axis. Set it to 0 to disable distortion on this axis.")]
		public FloatParameter intensityX = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x040000F9 RID: 249
		[Range(0f, 1f)]
		[DisplayName("Y Multiplier")]
		[Tooltip("Intensity multiplier on the y-axis. Set it to 0 to disable distortion on this axis.")]
		public FloatParameter intensityY = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x040000FA RID: 250
		[Space]
		[Range(-1f, 1f)]
		[Tooltip("Distortion center point (x-axis).")]
		public FloatParameter centerX = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x040000FB RID: 251
		[Range(-1f, 1f)]
		[Tooltip("Distortion center point (y-axis).")]
		public FloatParameter centerY = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x040000FC RID: 252
		[Space]
		[Range(0.01f, 5f)]
		[Tooltip("Global screen scaling.")]
		public FloatParameter scale = new FloatParameter
		{
			value = 1f
		};
	}
}
