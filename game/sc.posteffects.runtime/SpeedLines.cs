using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000043 RID: 67
	[PostProcess(typeof(SpeedLinesRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Screen/Speed Lines", true)]
	[Serializable]
	public sealed class SpeedLines : PostProcessEffectSettings
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x0000761F File Offset: 0x0000581F
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.intensity != 0f && !(this.noiseTex.value == null);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00007658 File Offset: 0x00005858
		public SpeedLines()
		{
		}

		// Token: 0x0400013C RID: 316
		[Tooltip("Assign any grayscale texture with a vertically repeating pattern and a falloff from left to right")]
		public TextureParameter noiseTex = new TextureParameter
		{
			value = null
		};

		// Token: 0x0400013D RID: 317
		[Range(0f, 1f)]
		public FloatParameter intensity = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x0400013E RID: 318
		[Range(0f, 1f)]
		[Tooltip("Determines the radial tiling of the noise texture")]
		public FloatParameter size = new FloatParameter
		{
			value = 0.5f
		};

		// Token: 0x0400013F RID: 319
		[Range(0f, 1f)]
		public FloatParameter falloff = new FloatParameter
		{
			value = 0.25f
		};
	}
}
