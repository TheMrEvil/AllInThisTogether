using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200003E RID: 62
	[PostProcess(typeof(VignetteRenderer), "Unity/Vignette", true)]
	[Serializable]
	public sealed class Vignette : PostProcessEffectSettings
	{
		// Token: 0x060000CD RID: 205 RVA: 0x0000A234 File Offset: 0x00008434
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && ((this.mode.value == VignetteMode.Classic && this.intensity.value > 0f) || (this.mode.value == VignetteMode.Masked && this.opacity.value > 0f && this.mask.value != null));
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000A2A4 File Offset: 0x000084A4
		public Vignette()
		{
		}

		// Token: 0x0400013E RID: 318
		[Tooltip("Use the \"Classic\" mode for parametric controls. Use the \"Masked\" mode to use your own texture mask.")]
		public VignetteModeParameter mode = new VignetteModeParameter
		{
			value = VignetteMode.Classic
		};

		// Token: 0x0400013F RID: 319
		[Tooltip("Vignette color.")]
		public ColorParameter color = new ColorParameter
		{
			value = new Color(0f, 0f, 0f, 1f)
		};

		// Token: 0x04000140 RID: 320
		[Tooltip("Sets the vignette center point (screen center is [0.5, 0.5]).")]
		public Vector2Parameter center = new Vector2Parameter
		{
			value = new Vector2(0.5f, 0.5f)
		};

		// Token: 0x04000141 RID: 321
		[Range(0f, 1f)]
		[Tooltip("Amount of vignetting on screen.")]
		public FloatParameter intensity = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000142 RID: 322
		[Range(0.01f, 1f)]
		[Tooltip("Smoothness of the vignette borders.")]
		public FloatParameter smoothness = new FloatParameter
		{
			value = 0.2f
		};

		// Token: 0x04000143 RID: 323
		[Range(0f, 1f)]
		[Tooltip("Lower values will make a square-ish vignette.")]
		public FloatParameter roundness = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x04000144 RID: 324
		[Tooltip("Set to true to mark the vignette to be perfectly round. False will make its shape dependent on the current aspect ratio.")]
		public BoolParameter rounded = new BoolParameter
		{
			value = false
		};

		// Token: 0x04000145 RID: 325
		[Tooltip("A black and white mask to use as a vignette.")]
		public TextureParameter mask = new TextureParameter
		{
			value = null
		};

		// Token: 0x04000146 RID: 326
		[Range(0f, 1f)]
		[Tooltip("Mask opacity.")]
		public FloatParameter opacity = new FloatParameter
		{
			value = 1f
		};
	}
}
