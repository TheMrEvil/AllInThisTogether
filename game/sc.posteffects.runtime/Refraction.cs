using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000036 RID: 54
	[PostProcess(typeof(RefractionRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Screen/Refraction", true)]
	[Serializable]
	public sealed class Refraction : PostProcessEffectSettings
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00006E11 File Offset: 0x00005011
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.amount != 0f && !(this.refractionTex.value == null);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00006E4C File Offset: 0x0000504C
		public Refraction()
		{
		}

		// Token: 0x040000F7 RID: 247
		[Tooltip("Takes a DUDV map (normal map without a blue channel) to perturb the image")]
		public TextureParameter refractionTex = new TextureParameter
		{
			value = null
		};

		// Token: 0x040000F8 RID: 248
		[DisplayName("Using normal map")]
		[Tooltip("In the absense of a DUDV map, the supplied normal map can be converted in the shader")]
		public BoolParameter convertNormalMap = new BoolParameter
		{
			value = false
		};

		// Token: 0x040000F9 RID: 249
		[Range(0f, 1f)]
		[Tooltip("Amount")]
		public FloatParameter amount = new FloatParameter
		{
			value = 0f
		};
	}
}
