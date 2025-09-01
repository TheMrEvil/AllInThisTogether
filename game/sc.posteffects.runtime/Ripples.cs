using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000038 RID: 56
	[PostProcess(typeof(RipplesRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Screen/Ripples", true)]
	[Serializable]
	public sealed class Ripples : PostProcessEffectSettings
	{
		// Token: 0x060000AD RID: 173 RVA: 0x00006F6B File Offset: 0x0000516B
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.strength != 0f;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00006F94 File Offset: 0x00005194
		public Ripples()
		{
		}

		// Token: 0x040000FB RID: 251
		[DisplayName("Method")]
		public Ripples.RipplesModeParam mode = new Ripples.RipplesModeParam
		{
			value = Ripples.RipplesMode.Radial
		};

		// Token: 0x040000FC RID: 252
		[Range(0f, 10f)]
		[DisplayName("Intensity")]
		public FloatParameter strength = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x040000FD RID: 253
		[Range(1f, 10f)]
		[Tooltip("The frequency of the waves")]
		public FloatParameter distance = new FloatParameter
		{
			value = 5f
		};

		// Token: 0x040000FE RID: 254
		[Range(0f, 10f)]
		[Tooltip("Speed")]
		public FloatParameter speed = new FloatParameter
		{
			value = 3f
		};

		// Token: 0x040000FF RID: 255
		[Range(0f, 5f)]
		[Tooltip("Width")]
		public FloatParameter width = new FloatParameter
		{
			value = 1.5f
		};

		// Token: 0x04000100 RID: 256
		[Range(0f, 5f)]
		[Tooltip("Height")]
		public FloatParameter height = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x02000078 RID: 120
		public enum RipplesMode
		{
			// Token: 0x040001C8 RID: 456
			Radial,
			// Token: 0x040001C9 RID: 457
			OmniDirectional
		}

		// Token: 0x02000079 RID: 121
		[Serializable]
		public sealed class RipplesModeParam : ParameterOverride<Ripples.RipplesMode>
		{
			// Token: 0x06000101 RID: 257 RVA: 0x00008570 File Offset: 0x00006770
			public RipplesModeParam()
			{
			}
		}
	}
}
