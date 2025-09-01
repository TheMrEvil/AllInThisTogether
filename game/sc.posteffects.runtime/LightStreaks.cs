using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000026 RID: 38
	[PostProcess(typeof(LightStreaksRenderer), PostProcessEvent.BeforeStack, "SC Post Effects/Rendering/Light Streaks", true)]
	[Serializable]
	public sealed class LightStreaks : PostProcessEffectSettings
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00005A54 File Offset: 0x00003C54
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.blur != 0f && this.intensity != 0f && this.direction != 0f;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00005AAC File Offset: 0x00003CAC
		public LightStreaks()
		{
		}

		// Token: 0x040000B8 RID: 184
		[DisplayName("Quality")]
		[Tooltip("Choose between Box and Gaussian blurring methods.\n\nBox blurring is more efficient but has a limited blur range")]
		public LightStreaks.BlurMethodParameter quality = new LightStreaks.BlurMethodParameter
		{
			value = LightStreaks.Quality.Appearance
		};

		// Token: 0x040000B9 RID: 185
		[Range(0f, 1f)]
		[DisplayName("Streaks Only")]
		[Tooltip("Shows only the effect, to allow for finetuning")]
		public BoolParameter debug = new BoolParameter
		{
			value = false
		};

		// Token: 0x040000BA RID: 186
		[Header("Anamorphic Lensfares")]
		[Range(0f, 1f)]
		[Tooltip("Intensity")]
		public FloatParameter intensity = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x040000BB RID: 187
		[Range(0.01f, 5f)]
		[Tooltip("Luminance threshold, pixels above this threshold (material's emission value) will contribute to the effect")]
		public FloatParameter luminanceThreshold = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x040000BC RID: 188
		[Range(-1f, 1f)]
		[Tooltip("Negative values become horizontal whereas postive values are vertical")]
		public FloatParameter direction = new FloatParameter
		{
			value = -1f
		};

		// Token: 0x040000BD RID: 189
		[Header("Blur")]
		[Range(0f, 10f)]
		[DisplayName("Amount")]
		[Tooltip("The amount of blurring that must be performed")]
		public FloatParameter blur = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x040000BE RID: 190
		[Range(1f, 8f)]
		[Tooltip("The number of times the effect is blurred. More iterations provide a smoother effect but induce more drawcalls.")]
		public IntParameter iterations = new IntParameter
		{
			value = 2
		};

		// Token: 0x040000BF RID: 191
		[Range(1f, 4f)]
		[Tooltip("Every step halfs the resolution of the blur effect. Lower resolution provides a smoother blur but may induce flickering")]
		public IntParameter downscaling = new IntParameter
		{
			value = 2
		};

		// Token: 0x0200006D RID: 109
		public enum Quality
		{
			// Token: 0x040001AE RID: 430
			Performance,
			// Token: 0x040001AF RID: 431
			Appearance
		}

		// Token: 0x0200006E RID: 110
		[Serializable]
		public sealed class BlurMethodParameter : ParameterOverride<LightStreaks.Quality>
		{
			// Token: 0x060000FC RID: 252 RVA: 0x00008548 File Offset: 0x00006748
			public BlurMethodParameter()
			{
			}
		}
	}
}
