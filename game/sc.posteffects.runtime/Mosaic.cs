using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x0200002A RID: 42
	[PostProcess(typeof(MosaicRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Stylized/Mosaic", true)]
	[Serializable]
	public sealed class Mosaic : PostProcessEffectSettings
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00006162 File Offset: 0x00004362
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.size != 0f;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00006188 File Offset: 0x00004388
		public Mosaic()
		{
		}

		// Token: 0x040000CC RID: 204
		[DisplayName("Method")]
		[Tooltip("")]
		public Mosaic.MosaicModeParam mode = new Mosaic.MosaicModeParam
		{
			value = Mosaic.MosaicMode.Hexagons
		};

		// Token: 0x040000CD RID: 205
		[Range(0f, 1f)]
		[Tooltip("Size")]
		public FloatParameter size = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x02000072 RID: 114
		public enum MosaicMode
		{
			// Token: 0x040001BA RID: 442
			Triangles,
			// Token: 0x040001BB RID: 443
			Hexagons,
			// Token: 0x040001BC RID: 444
			Circles
		}

		// Token: 0x02000073 RID: 115
		[Serializable]
		public sealed class MosaicModeParam : ParameterOverride<Mosaic.MosaicMode>
		{
			// Token: 0x060000FE RID: 254 RVA: 0x00008558 File Offset: 0x00006758
			public MosaicModeParam()
			{
			}
		}
	}
}
