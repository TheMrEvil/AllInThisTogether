using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000004 RID: 4
	[PostProcess(typeof(BlackBarsRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Screen/Black Bars", true)]
	[Serializable]
	public sealed class BlackBars : PostProcessEffectSettings
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002457 File Offset: 0x00000657
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.size != 0f && this.maxSize != 0f;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002490 File Offset: 0x00000690
		public BlackBars()
		{
		}

		// Token: 0x0400000C RID: 12
		[DisplayName("Direction")]
		[Tooltip("")]
		public BlackBars.DirectionParam mode = new BlackBars.DirectionParam
		{
			value = BlackBars.Direction.Horizontal
		};

		// Token: 0x0400000D RID: 13
		[Range(0f, 1f)]
		[Tooltip("Size")]
		public FloatParameter size = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x0400000E RID: 14
		[Range(0f, 1f)]
		[Tooltip("Max Size")]
		public FloatParameter maxSize = new FloatParameter
		{
			value = 0.33f
		};

		// Token: 0x02000051 RID: 81
		public enum Direction
		{
			// Token: 0x0400016E RID: 366
			Horizontal,
			// Token: 0x0400016F RID: 367
			Vertical
		}

		// Token: 0x02000052 RID: 82
		[Serializable]
		public sealed class DirectionParam : ParameterOverride<BlackBars.Direction>
		{
			// Token: 0x060000F0 RID: 240 RVA: 0x000084E8 File Offset: 0x000066E8
			public DirectionParam()
			{
			}
		}
	}
}
