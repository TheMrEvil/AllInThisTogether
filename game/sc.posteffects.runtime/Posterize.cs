using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000032 RID: 50
	[PostProcess(typeof(PosterizeRenderer), PostProcessEvent.BeforeTransparent, "SC Post Effects/Retro/Posterize", true)]
	[Serializable]
	public sealed class Posterize : PostProcessEffectSettings
	{
		// Token: 0x0600009C RID: 156 RVA: 0x00006B51 File Offset: 0x00004D51
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && (this.hsvMode || this.levels != 256);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00006B84 File Offset: 0x00004D84
		public Posterize()
		{
		}

		// Token: 0x040000ED RID: 237
		public BoolParameter hsvMode = new BoolParameter
		{
			value = false
		};

		// Token: 0x040000EE RID: 238
		[Range(0f, 256f)]
		public IntParameter levels = new IntParameter
		{
			value = 256
		};

		// Token: 0x040000EF RID: 239
		[Range(0f, 1f)]
		public FloatParameter opacity = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x040000F0 RID: 240
		[Header("Levels")]
		[Range(0f, 256f)]
		public IntParameter hue = new IntParameter
		{
			value = 256
		};

		// Token: 0x040000F1 RID: 241
		[Range(0f, 256f)]
		public IntParameter saturation = new IntParameter
		{
			value = 256
		};

		// Token: 0x040000F2 RID: 242
		[Range(0f, 256f)]
		public IntParameter value = new IntParameter
		{
			value = 256
		};
	}
}
