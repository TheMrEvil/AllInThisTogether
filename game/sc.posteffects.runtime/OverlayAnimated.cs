using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x0200002D RID: 45
	[PostProcess(typeof(OverlayAnimatedRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Screen/Overlay Animated", true)]
	[Serializable]
	public class OverlayAnimated : PostProcessEffectSettings
	{
		// Token: 0x0600008B RID: 139 RVA: 0x000063FD File Offset: 0x000045FD
		private void UpdateTiling()
		{
			this.distortTilingFloat = this.distortTiling.value;
			this.tilingFloat = this.tiling.value;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00006424 File Offset: 0x00004624
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			if (this.enabled.value)
			{
				if (!Application.isPlaying)
				{
					this.UpdateTiling();
				}
				return !(this.overlayTex.value == null) && this.intensity != 0f;
			}
			return false;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00006478 File Offset: 0x00004678
		public OverlayAnimated()
		{
		}

		// Token: 0x040000D8 RID: 216
		public ColorParameter color = new ColorParameter
		{
			value = new Color(0.66f, 0f, 0f)
		};

		// Token: 0x040000D9 RID: 217
		[Tooltip("The texture's alpha channel controls its opacity")]
		public TextureParameter overlayTex = new TextureParameter
		{
			value = null
		};

		// Token: 0x040000DA RID: 218
		public TextureParameter overlayDistort = new TextureParameter
		{
			value = null
		};

		// Token: 0x040000DB RID: 219
		[Range(0f, 10f)]
		public FloatParameter distortSpeed = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x040000DC RID: 220
		[Range(0f, 2f)]
		public FloatParameter distortScale = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x040000DD RID: 221
		[Range(0f, 1f)]
		public FloatParameter distortTiling = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x040000DE RID: 222
		public float distortTilingFloat;

		// Token: 0x040000DF RID: 223
		[Tooltip("The texture's alpha channel controls its opacity")]
		public TextureParameter overlayMask = new TextureParameter
		{
			value = null
		};

		// Token: 0x040000E0 RID: 224
		[Range(0f, 10f)]
		public FloatParameter maskStart = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x040000E1 RID: 225
		[Range(0f, 2f)]
		public FloatParameter maskEnd = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x040000E2 RID: 226
		[Tooltip("Use white value as mask instead of black")]
		public BoolParameter invertMask = new BoolParameter
		{
			value = false
		};

		// Token: 0x040000E3 RID: 227
		[Range(0f, 1f)]
		public FloatParameter intensity = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x040000E4 RID: 228
		[Range(0f, 1f)]
		[Tooltip("The screen's luminance values control the opacity of the image")]
		public FloatParameter luminanceThreshold = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x040000E5 RID: 229
		[Tooltip("Maintains the image aspect ratio, regardless of the screen width")]
		public BoolParameter autoAspect = new BoolParameter
		{
			value = false
		};

		// Token: 0x040000E6 RID: 230
		[Tooltip("Blends the gradient through various Photoshop-like blending modes")]
		public OverlayAnimated.BlendModeParameter blendMode = new OverlayAnimated.BlendModeParameter
		{
			value = OverlayAnimated.BlendMode.Linear
		};

		// Token: 0x040000E7 RID: 231
		[Range(0f, 1f)]
		public FloatParameter tiling = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x040000E8 RID: 232
		public float tilingFloat;

		// Token: 0x02000076 RID: 118
		public enum BlendMode
		{
			// Token: 0x040001C3 RID: 451
			Linear,
			// Token: 0x040001C4 RID: 452
			Additive,
			// Token: 0x040001C5 RID: 453
			Multiply,
			// Token: 0x040001C6 RID: 454
			Screen
		}

		// Token: 0x02000077 RID: 119
		[Serializable]
		public sealed class BlendModeParameter : ParameterOverride<OverlayAnimated.BlendMode>
		{
			// Token: 0x06000100 RID: 256 RVA: 0x00008568 File Offset: 0x00006768
			public BlendModeParameter()
			{
			}
		}
	}
}
