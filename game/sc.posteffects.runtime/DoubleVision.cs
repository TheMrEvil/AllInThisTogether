using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000014 RID: 20
	[PostProcess(typeof(DoubleVisionRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Blurring/Double Vision", true)]
	[Serializable]
	public sealed class DoubleVision : PostProcessEffectSettings
	{
		// Token: 0x06000037 RID: 55 RVA: 0x000036AA File Offset: 0x000018AA
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.intensity != 0f;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000036D0 File Offset: 0x000018D0
		public DoubleVision()
		{
		}

		// Token: 0x04000043 RID: 67
		[DisplayName("Method")]
		[Tooltip("Choose to apply the effect over the entire screen or just the edges")]
		public DoubleVision.DoubleVisionMode mode = new DoubleVision.DoubleVisionMode
		{
			value = DoubleVision.Mode.FullScreen
		};

		// Token: 0x04000044 RID: 68
		[Range(0f, 1f)]
		[Tooltip("Intensity")]
		public FloatParameter intensity = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x0200005A RID: 90
		public enum Mode
		{
			// Token: 0x04000183 RID: 387
			FullScreen,
			// Token: 0x04000184 RID: 388
			Edges
		}

		// Token: 0x0200005B RID: 91
		[Serializable]
		public sealed class DoubleVisionMode : ParameterOverride<DoubleVision.Mode>
		{
			// Token: 0x060000F4 RID: 244 RVA: 0x00008508 File Offset: 0x00006708
			public DoubleVisionMode()
			{
			}
		}
	}
}
