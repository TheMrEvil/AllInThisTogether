using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x0200004B RID: 75
	[PostProcess(typeof(TransitionRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Screen/Transition", true)]
	[Serializable]
	public sealed class Transition : PostProcessEffectSettings
	{
		// Token: 0x060000DF RID: 223 RVA: 0x0000821A File Offset: 0x0000641A
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.progress != 0f;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00008240 File Offset: 0x00006440
		public Transition()
		{
		}

		// Token: 0x0400015F RID: 351
		public TextureParameter gradientTex = new TextureParameter
		{
			value = null,
			defaultState = TextureParameterDefault.None
		};

		// Token: 0x04000160 RID: 352
		[Range(0f, 1f)]
		[Tooltip("Progress")]
		public FloatParameter progress = new FloatParameter
		{
			value = 0f
		};
	}
}
