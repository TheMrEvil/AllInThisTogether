using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x0200001C RID: 28
	[PostProcess(typeof(GradientRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Screen/Gradient", true)]
	[Serializable]
	public sealed class Gradient : PostProcessEffectSettings
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00004DF0 File Offset: 0x00002FF0
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.intensity != 0f && (this.input.value != Gradient.Mode.Texture || !(this.gradientTex.value == null));
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00004E44 File Offset: 0x00003044
		public Gradient()
		{
		}

		// Token: 0x0400008E RID: 142
		[Range(0f, 1f)]
		[DisplayName("Opacity")]
		public FloatParameter intensity = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x0400008F RID: 143
		[Space]
		[Tooltip("Set the color either through 2 color fields, or a gradient texture")]
		public Gradient.GradientModeParameter input = new Gradient.GradientModeParameter
		{
			value = Gradient.Mode.ColorFields
		};

		// Token: 0x04000090 RID: 144
		[Tooltip("The color's alpha channel controls its opacity")]
		public ColorParameter color1 = new ColorParameter
		{
			value = new Color(0f, 0.8f, 0.56f, 0.5f)
		};

		// Token: 0x04000091 RID: 145
		[Tooltip("The color's alpha channel controls its opacity")]
		public ColorParameter color2 = new ColorParameter
		{
			value = new Color(0.81f, 0.37f, 1f, 0.5f)
		};

		// Token: 0x04000092 RID: 146
		[Range(0f, 1f)]
		[Space]
		[Tooltip("Controls the rotation of the gradient")]
		public FloatParameter rotation = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000093 RID: 147
		[DisplayName("Gradient")]
		[Tooltip("")]
		public TextureParameter gradientTex = new TextureParameter
		{
			value = null
		};

		// Token: 0x04000094 RID: 148
		[Tooltip("Blends the gradient through various Photoshop-like blending modes")]
		public Gradient.BlendModeParameter mode = new Gradient.BlendModeParameter
		{
			value = Gradient.BlendMode.Linear
		};

		// Token: 0x04000095 RID: 149
		private const int RESOLUTION = 64;

		// Token: 0x02000063 RID: 99
		public enum Mode
		{
			// Token: 0x04000197 RID: 407
			ColorFields,
			// Token: 0x04000198 RID: 408
			Texture
		}

		// Token: 0x02000064 RID: 100
		[Serializable]
		public sealed class GradientModeParameter : ParameterOverride<Gradient.Mode>
		{
			// Token: 0x060000F8 RID: 248 RVA: 0x00008528 File Offset: 0x00006728
			public GradientModeParameter()
			{
			}
		}

		// Token: 0x02000065 RID: 101
		public enum BlendMode
		{
			// Token: 0x0400019A RID: 410
			Linear,
			// Token: 0x0400019B RID: 411
			Additive,
			// Token: 0x0400019C RID: 412
			Multiply,
			// Token: 0x0400019D RID: 413
			Screen
		}

		// Token: 0x02000066 RID: 102
		[Serializable]
		public sealed class BlendModeParameter : ParameterOverride<Gradient.BlendMode>
		{
			// Token: 0x060000F9 RID: 249 RVA: 0x00008530 File Offset: 0x00006730
			public BlendModeParameter()
			{
			}
		}
	}
}
