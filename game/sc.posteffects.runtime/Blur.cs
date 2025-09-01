using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000006 RID: 6
	[PostProcess(typeof(BlurRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Blurring/Blur", true)]
	[Serializable]
	public sealed class Blur : PostProcessEffectSettings
	{
		// Token: 0x0600000D RID: 13 RVA: 0x0000259C File Offset: 0x0000079C
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.amount > 0f;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000025C0 File Offset: 0x000007C0
		public Blur()
		{
		}

		// Token: 0x04000010 RID: 16
		[DisplayName("Method")]
		[Tooltip("Box blurring uses fewer texture samples but has a limited blur range")]
		public Blur.BlurMethodParameter mode = new Blur.BlurMethodParameter
		{
			value = Blur.BlurMethod.Gaussian
		};

		// Token: 0x04000011 RID: 17
		[Tooltip("When enabled, the amount of blur passes is doubled")]
		public BoolParameter highQuality = new BoolParameter
		{
			value = false
		};

		// Token: 0x04000012 RID: 18
		public BoolParameter distanceFade = new BoolParameter
		{
			value = false
		};

		// Token: 0x04000013 RID: 19
		public FloatParameter startFadeDistance = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000014 RID: 20
		public FloatParameter endFadeDistance = new FloatParameter
		{
			value = 500f
		};

		// Token: 0x04000015 RID: 21
		[Space]
		[Range(0f, 5f)]
		[Tooltip("The amount of blurring that must be performed")]
		public FloatParameter amount = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000016 RID: 22
		[Range(1f, 12f)]
		[Tooltip("The number of times the effect is blurred. More iterations provide a smoother effect but induce more drawcalls.")]
		public IntParameter iterations = new IntParameter
		{
			value = 6
		};

		// Token: 0x04000017 RID: 23
		[Range(1f, 4f)]
		[Tooltip("Every step halfs the resolution of the blur effect. Lower resolution provides a smoother blur but may induce flickering.")]
		public IntParameter downscaling = new IntParameter
		{
			value = 2
		};

		// Token: 0x02000053 RID: 83
		public enum BlurMethod
		{
			// Token: 0x04000171 RID: 369
			Gaussian,
			// Token: 0x04000172 RID: 370
			Box
		}

		// Token: 0x02000054 RID: 84
		[Serializable]
		public sealed class BlurMethodParameter : ParameterOverride<Blur.BlurMethod>
		{
			// Token: 0x060000F1 RID: 241 RVA: 0x000084F0 File Offset: 0x000066F0
			public BlurMethodParameter()
			{
			}
		}
	}
}
