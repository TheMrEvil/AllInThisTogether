using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x0200004D RID: 77
	[PostProcess(typeof(TubeDistortionRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Screen/Tube Distortion", true)]
	[Serializable]
	public sealed class TubeDistortion : PostProcessEffectSettings
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x00008340 File Offset: 0x00006540
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.amount != 0f;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00008366 File Offset: 0x00006566
		public TubeDistortion()
		{
		}

		// Token: 0x04000162 RID: 354
		public TubeDistortion.DistortionModeParam mode = new TubeDistortion.DistortionModeParam
		{
			value = TubeDistortion.DistortionMode.Buldged
		};

		// Token: 0x04000163 RID: 355
		[Range(0f, 1f)]
		public FloatParameter amount = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x02000089 RID: 137
		public enum DistortionMode
		{
			// Token: 0x040001EE RID: 494
			Buldged,
			// Token: 0x040001EF RID: 495
			Pinched,
			// Token: 0x040001F0 RID: 496
			Beveled
		}

		// Token: 0x0200008A RID: 138
		[Serializable]
		public sealed class DistortionModeParam : ParameterOverride<TubeDistortion.DistortionMode>
		{
			// Token: 0x06000108 RID: 264 RVA: 0x000085A8 File Offset: 0x000067A8
			public DistortionModeParam()
			{
			}
		}
	}
}
