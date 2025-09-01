using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000034 RID: 52
	[PostProcess(typeof(RadialBlurRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Blurring/Radial Blur", true)]
	[Serializable]
	public sealed class RadialBlur : PostProcessEffectSettings
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x00006D05 File Offset: 0x00004F05
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.amount != 0f;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00006D2B File Offset: 0x00004F2B
		public RadialBlur()
		{
		}

		// Token: 0x040000F4 RID: 244
		[Range(0f, 1f)]
		public FloatParameter amount = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x040000F5 RID: 245
		[Range(3f, 12f)]
		public IntParameter iterations = new IntParameter
		{
			value = 6
		};
	}
}
