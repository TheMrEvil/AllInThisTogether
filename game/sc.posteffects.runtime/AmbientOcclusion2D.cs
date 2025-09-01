using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000002 RID: 2
	[PostProcess(typeof(AmbientOcclusion2DRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Rendering/Ambient Occlusion 2D", true)]
	[Serializable]
	public sealed class AmbientOcclusion2D : PostProcessEffectSettings
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.intensity != 0f;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002078 File Offset: 0x00000278
		public AmbientOcclusion2D()
		{
		}

		// Token: 0x04000001 RID: 1
		[DisplayName("Debug")]
		[Tooltip("Shows only the effect, to alow for finetuning")]
		public BoolParameter aoOnly = new BoolParameter
		{
			value = false
		};

		// Token: 0x04000002 RID: 2
		[Header("Luminance-Based Amient Occlusion")]
		[Range(0f, 1f)]
		[Tooltip("Intensity")]
		public FloatParameter intensity = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000003 RID: 3
		[Range(0.01f, 1f)]
		[Tooltip("Luminance threshold, pixels above this threshold will contribute to the effect")]
		public FloatParameter luminanceThreshold = new FloatParameter
		{
			value = 0.05f
		};

		// Token: 0x04000004 RID: 4
		[Range(0f, 3f)]
		[Tooltip("Distance")]
		public FloatParameter distance = new FloatParameter
		{
			value = 0.3f
		};

		// Token: 0x04000005 RID: 5
		[Header("Blur")]
		[Range(0f, 3f)]
		[DisplayName("Blur")]
		[Tooltip("The amount of blurring that must be performed")]
		public FloatParameter blurAmount = new FloatParameter
		{
			value = 0.85f
		};

		// Token: 0x04000006 RID: 6
		[Range(1f, 8f)]
		[Tooltip("The number of times the effect is blurred. More iterations provide a smoother effect but induce more drawcalls.")]
		public IntParameter iterations = new IntParameter
		{
			value = 4
		};

		// Token: 0x04000007 RID: 7
		[Range(1f, 4f)]
		[Tooltip("Every step halves the resolution of the blur effect. Lower resolution provides a smoother blur but may induce flickering.")]
		public IntParameter downscaling = new IntParameter
		{
			value = 2
		};
	}
}
