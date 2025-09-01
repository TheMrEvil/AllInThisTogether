using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000041 RID: 65
	[PostProcess(typeof(SketchRenderer), PostProcessEvent.BeforeStack, "SC Post Effects/Stylized/Sketch", true)]
	[Serializable]
	public sealed class Sketch : PostProcessEffectSettings
	{
		// Token: 0x060000BF RID: 191 RVA: 0x0000737E File Offset: 0x0000557E
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.intensity != 0f && !(this.strokeTex.value == null);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000073B8 File Offset: 0x000055B8
		public Sketch()
		{
		}

		// Token: 0x04000135 RID: 309
		[Tooltip("The Red channel is used for darker shades, whereas the Green channel is for lighter.")]
		public TextureParameter strokeTex = new TextureParameter
		{
			value = null
		};

		// Token: 0x04000136 RID: 310
		[Space]
		[Tooltip("Choose the type of UV space being used")]
		public Sketch.SketchProjectioParameter projectionMode = new Sketch.SketchProjectioParameter
		{
			value = Sketch.SketchProjectionMode.WorldSpace
		};

		// Token: 0x04000137 RID: 311
		[Tooltip("Choose one of the different modes")]
		public Sketch.SketchModeParameter blendMode = new Sketch.SketchModeParameter
		{
			value = Sketch.SketchMode.EffectOnly
		};

		// Token: 0x04000138 RID: 312
		[Space]
		[Range(0f, 1f)]
		public FloatParameter intensity = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000139 RID: 313
		public Vector2Parameter brightness = new Vector2Parameter
		{
			value = new Vector2(0f, 1f)
		};

		// Token: 0x0400013A RID: 314
		[Range(1f, 32f)]
		public FloatParameter tiling = new FloatParameter
		{
			value = 8f
		};

		// Token: 0x0200007A RID: 122
		public enum SketchProjectionMode
		{
			// Token: 0x040001CB RID: 459
			WorldSpace,
			// Token: 0x040001CC RID: 460
			ScreenSpace
		}

		// Token: 0x0200007B RID: 123
		[Serializable]
		public sealed class SketchProjectioParameter : ParameterOverride<Sketch.SketchProjectionMode>
		{
			// Token: 0x06000102 RID: 258 RVA: 0x00008578 File Offset: 0x00006778
			public SketchProjectioParameter()
			{
			}
		}

		// Token: 0x0200007C RID: 124
		public enum SketchMode
		{
			// Token: 0x040001CE RID: 462
			EffectOnly,
			// Token: 0x040001CF RID: 463
			Multiply,
			// Token: 0x040001D0 RID: 464
			Add
		}

		// Token: 0x0200007D RID: 125
		[Serializable]
		public sealed class SketchModeParameter : ParameterOverride<Sketch.SketchMode>
		{
			// Token: 0x06000103 RID: 259 RVA: 0x00008580 File Offset: 0x00006780
			public SketchModeParameter()
			{
			}
		}
	}
}
