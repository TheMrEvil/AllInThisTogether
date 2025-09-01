using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000024 RID: 36
	[PostProcess(typeof(LensFlaresRenderer), PostProcessEvent.BeforeStack, "SC Post Effects/Rendering/Lens Flares", true)]
	[Serializable]
	public sealed class LensFlares : PostProcessEffectSettings
	{
		// Token: 0x06000072 RID: 114 RVA: 0x000054A0 File Offset: 0x000036A0
		public LensFlares()
		{
		}

		// Token: 0x040000A7 RID: 167
		public BoolParameter debug = new BoolParameter
		{
			value = false
		};

		// Token: 0x040000A8 RID: 168
		[Space]
		[Range(0f, 1f)]
		[DisplayName("Intensity")]
		public FloatParameter intensity = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x040000A9 RID: 169
		[Range(0.01f, 5f)]
		[DisplayName("Threshold")]
		[Tooltip("Luminance threshold, pixels above this threshold will contribute to the effect")]
		public FloatParameter luminanceThreshold = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x040000AA RID: 170
		[Header("Flares")]
		[Range(1f, 4f)]
		[DisplayName("Number")]
		public IntParameter iterations = new IntParameter
		{
			value = 2
		};

		// Token: 0x040000AB RID: 171
		[Range(1f, 2f)]
		[DisplayName("Distance")]
		[Tooltip("Offsets the Flares towards the edge of the screen")]
		public FloatParameter distance = new FloatParameter
		{
			value = 1.5f
		};

		// Token: 0x040000AC RID: 172
		[Range(1f, 10f)]
		[DisplayName("Falloff")]
		[Tooltip("Fades out the Flares towards the edge of the screen")]
		public FloatParameter falloff = new FloatParameter
		{
			value = 10f
		};

		// Token: 0x040000AD RID: 173
		[Header("Halo")]
		[Tooltip("Creates a halo at the center of the screen when looking directly at a bright spot")]
		[Range(0f, 1f)]
		[DisplayName("Size")]
		public FloatParameter haloSize = new FloatParameter
		{
			value = 0.2f
		};

		// Token: 0x040000AE RID: 174
		[Range(0f, 100f)]
		[DisplayName("Width")]
		public FloatParameter haloWidth = new FloatParameter
		{
			value = 70f
		};

		// Token: 0x040000AF RID: 175
		[Header("Colors and masking")]
		[DisplayName("Mask")]
		[Tooltip("Use a texture to mask out the effect")]
		public TextureParameter maskTex = new TextureParameter
		{
			value = null
		};

		// Token: 0x040000B0 RID: 176
		[Range(0f, 20f)]
		[DisplayName("Chromatic Abberation")]
		[Tooltip("Refracts the color channels")]
		public FloatParameter chromaticAbberation = new FloatParameter
		{
			value = 10f
		};

		// Token: 0x040000B1 RID: 177
		[DisplayName("Gradient")]
		[Tooltip("Color the flares from the center of the screen to the outer edges")]
		public TextureParameter colorTex = new TextureParameter
		{
			value = null
		};

		// Token: 0x040000B2 RID: 178
		[Header("Blur")]
		[Range(1f, 8f)]
		[DisplayName("Blur")]
		[Tooltip("The amount of blurring that must be performed")]
		public FloatParameter blur = new FloatParameter
		{
			value = 2f
		};

		// Token: 0x040000B3 RID: 179
		[Range(1f, 12f)]
		[DisplayName("Iterations")]
		[Tooltip("The number of times the effect is blurred. More iterations provide a smoother effect but induce more drawcalls.")]
		public IntParameter passes = new IntParameter
		{
			value = 3
		};
	}
}
