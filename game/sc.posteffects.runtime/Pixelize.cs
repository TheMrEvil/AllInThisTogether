using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000030 RID: 48
	[PostProcess(typeof(PixelizeRenderer), PostProcessEvent.BeforeStack, "SC Post Effects/Retro/Pixelize", true)]
	[Serializable]
	public sealed class Pixelize : PostProcessEffectSettings
	{
		// Token: 0x06000096 RID: 150 RVA: 0x00006A81 File Offset: 0x00004C81
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.amount != 0f;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00006AA7 File Offset: 0x00004CA7
		public Pixelize()
		{
		}

		// Token: 0x040000EB RID: 235
		[Range(0f, 1f)]
		[Tooltip("Amount")]
		public FloatParameter amount = new FloatParameter
		{
			value = 0f
		};
	}
}
