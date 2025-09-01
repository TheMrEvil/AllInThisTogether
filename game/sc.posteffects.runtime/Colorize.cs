using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x0200000E RID: 14
	[PostProcess(typeof(ColorizeRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Image/Colorize", true)]
	[Serializable]
	public sealed class Colorize : PostProcessEffectSettings
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000031E4 File Offset: 0x000013E4
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.intensity != 0f && this.colorRamp.value;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000321C File Offset: 0x0000141C
		public Colorize()
		{
		}

		// Token: 0x04000035 RID: 53
		[Tooltip("Blends the gradient through various Photoshop-like blending modes")]
		public Colorize.BlendModeParameter mode = new Colorize.BlendModeParameter
		{
			value = Colorize.BlendMode.Linear
		};

		// Token: 0x04000036 RID: 54
		[Range(0f, 1f)]
		[Tooltip("Fades the effect in or out")]
		public FloatParameter intensity = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000037 RID: 55
		[Tooltip("Supply a gradient texture.\n\nLuminance values are colorized from left to right")]
		public TextureParameter colorRamp = new TextureParameter
		{
			value = null
		};

		// Token: 0x02000058 RID: 88
		public enum BlendMode
		{
			// Token: 0x0400017E RID: 382
			Linear,
			// Token: 0x0400017F RID: 383
			Additive,
			// Token: 0x04000180 RID: 384
			Multiply,
			// Token: 0x04000181 RID: 385
			Screen
		}

		// Token: 0x02000059 RID: 89
		[Serializable]
		public sealed class BlendModeParameter : ParameterOverride<Colorize.BlendMode>
		{
			// Token: 0x060000F3 RID: 243 RVA: 0x00008500 File Offset: 0x00006700
			public BlendModeParameter()
			{
			}
		}
	}
}
