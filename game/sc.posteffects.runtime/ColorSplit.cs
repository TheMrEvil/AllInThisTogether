using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x0200000C RID: 12
	[PostProcess(typeof(ColorSplitRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Retro/Color Split", true)]
	[Serializable]
	public sealed class ColorSplit : PostProcessEffectSettings
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000030F1 File Offset: 0x000012F1
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.offset != 0f;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00003117 File Offset: 0x00001317
		public ColorSplit()
		{
		}

		// Token: 0x04000032 RID: 50
		[DisplayName("Method")]
		[Tooltip("Box filtered methods provide a subtle blur effect and are less efficient")]
		public ColorSplit.SplitModeParam mode = new ColorSplit.SplitModeParam
		{
			value = ColorSplit.SplitMode.Single
		};

		// Token: 0x04000033 RID: 51
		[Range(0f, 1f)]
		[Tooltip("The amount by which the color channels offset")]
		public FloatParameter offset = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x02000056 RID: 86
		public enum SplitMode
		{
			// Token: 0x04000179 RID: 377
			Single,
			// Token: 0x0400017A RID: 378
			SingleBoxFiltered,
			// Token: 0x0400017B RID: 379
			Double,
			// Token: 0x0400017C RID: 380
			DoubleBoxFiltered
		}

		// Token: 0x02000057 RID: 87
		[Serializable]
		public sealed class SplitModeParam : ParameterOverride<ColorSplit.SplitMode>
		{
			// Token: 0x060000F2 RID: 242 RVA: 0x000084F8 File Offset: 0x000066F8
			public SplitModeParam()
			{
			}
		}
	}
}
