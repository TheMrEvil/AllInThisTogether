using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x0200003A RID: 58
	[PostProcess(typeof(ScanlinesRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Retro/Scanlines", true)]
	[Serializable]
	public sealed class Scanlines : PostProcessEffectSettings
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00007144 File Offset: 0x00005344
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.intensity.value != 0f;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000716C File Offset: 0x0000536C
		public Scanlines()
		{
		}

		// Token: 0x04000102 RID: 258
		[Range(0f, 1f)]
		[Tooltip("Intensity")]
		public FloatParameter intensity = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000103 RID: 259
		[Range(0f, 2048f)]
		[DisplayName("Lines")]
		public FloatParameter amount = new FloatParameter
		{
			value = 700f
		};

		// Token: 0x04000104 RID: 260
		[Range(0f, 1f)]
		[Tooltip("Animation speed")]
		public FloatParameter speed = new FloatParameter
		{
			value = 0f
		};
	}
}
