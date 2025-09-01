using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x0200002C RID: 44
	[PostProcess(typeof(OverlayRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Screen/Overlay", true)]
	[Serializable]
	public sealed class Overlay : PostProcessEffectSettings
	{
		// Token: 0x06000089 RID: 137 RVA: 0x000062F4 File Offset: 0x000044F4
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && !(this.overlayTex.value == null) && this.intensity != 0f;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00006330 File Offset: 0x00004530
		public Overlay()
		{
		}

		// Token: 0x040000CF RID: 207
		[Tooltip("The texture's alpha channel controls its opacity")]
		public TextureParameter overlayTex = new TextureParameter
		{
			value = null
		};

		// Token: 0x040000D0 RID: 208
		public TextureParameter shadowTex = new TextureParameter
		{
			value = null
		};

		// Token: 0x040000D1 RID: 209
		[Range(0f, 1f)]
		public FloatParameter intensity = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x040000D2 RID: 210
		[Range(0f, 1f)]
		[Tooltip("The screen's luminance values control the opacity of the image")]
		public FloatParameter luminanceThreshold = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x040000D3 RID: 211
		[Range(0f, 250f)]
		public FloatParameter edgDistMin = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x040000D4 RID: 212
		[Range(0f, 250f)]
		public FloatParameter distanceMin = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x040000D5 RID: 213
		[Range(0f, 250f)]
		[Tooltip("The screen's luminance values control the opacity of the image")]
		public FloatParameter distance = new FloatParameter
		{
			value = 25f
		};

		// Token: 0x040000D6 RID: 214
		[Tooltip("Blends the gradient through various Photoshop-like blending modes")]
		public Overlay.BlendModeParameter blendMode = new Overlay.BlendModeParameter
		{
			value = Overlay.BlendMode.Linear
		};

		// Token: 0x040000D7 RID: 215
		[Range(0f, 1f)]
		public FloatParameter tiling = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x02000074 RID: 116
		public enum BlendMode
		{
			// Token: 0x040001BE RID: 446
			Linear,
			// Token: 0x040001BF RID: 447
			Additive,
			// Token: 0x040001C0 RID: 448
			Multiply,
			// Token: 0x040001C1 RID: 449
			Screen
		}

		// Token: 0x02000075 RID: 117
		[Serializable]
		public sealed class BlendModeParameter : ParameterOverride<Overlay.BlendMode>
		{
			// Token: 0x060000FF RID: 255 RVA: 0x00008560 File Offset: 0x00006760
			public BlendModeParameter()
			{
			}
		}
	}
}
