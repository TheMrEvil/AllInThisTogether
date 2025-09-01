using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200002B RID: 43
	[PostProcess(typeof(GrainRenderer), "Unity/Grain", true)]
	[Serializable]
	public sealed class Grain : PostProcessEffectSettings
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00006F4B File Offset: 0x0000514B
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.intensity.value > 0f;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00006F70 File Offset: 0x00005170
		public Grain()
		{
		}

		// Token: 0x040000F0 RID: 240
		[Tooltip("Enable the use of colored grain.")]
		public BoolParameter colored = new BoolParameter
		{
			value = true
		};

		// Token: 0x040000F1 RID: 241
		[Range(0f, 1f)]
		[Tooltip("Grain strength. Higher values mean more visible grain.")]
		public FloatParameter intensity = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x040000F2 RID: 242
		[Range(0.3f, 3f)]
		[Tooltip("Grain particle size.")]
		public FloatParameter size = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x040000F3 RID: 243
		[Range(0f, 1f)]
		[DisplayName("Luminance Contribution")]
		[Tooltip("Controls the noise response curve based on scene luminance. Lower values mean less noise in dark areas.")]
		public FloatParameter lumContrib = new FloatParameter
		{
			value = 0.8f
		};
	}
}
