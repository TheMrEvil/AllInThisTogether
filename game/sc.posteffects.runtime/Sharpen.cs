using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x0200003F RID: 63
	[PostProcess(typeof(SharpenRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Image/Sharpen", true)]
	[Serializable]
	public sealed class Sharpen : PostProcessEffectSettings
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x0000727D File Offset: 0x0000547D
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.amount != 0f;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000072A3 File Offset: 0x000054A3
		public Sharpen()
		{
		}

		// Token: 0x04000132 RID: 306
		[Range(0f, 1f)]
		public FloatParameter amount = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000133 RID: 307
		[Range(0.1f, 2f)]
		public FloatParameter radius = new FloatParameter
		{
			value = 1f
		};
	}
}
