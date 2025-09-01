using System;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000017 RID: 23
	[PostProcess(typeof(BloomRenderer), "Unity/Bloom", true)]
	[Serializable]
	public sealed class Bloom : PostProcessEffectSettings
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002AD8 File Offset: 0x00000CD8
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.intensity.value > 0f;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002AFC File Offset: 0x00000CFC
		public Bloom()
		{
		}

		// Token: 0x04000047 RID: 71
		[Min(0f)]
		[Tooltip("Strength of the bloom filter. Values higher than 1 will make bloom contribute more energy to the final render.")]
		public FloatParameter intensity = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000048 RID: 72
		[Min(0f)]
		[Tooltip("Filters out pixels under this level of brightness. Value is in gamma-space.")]
		public FloatParameter threshold = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x04000049 RID: 73
		[Range(0f, 1f)]
		[Tooltip("Makes transitions between under/over-threshold gradual. 0 for a hard threshold, 1 for a soft threshold).")]
		public FloatParameter softKnee = new FloatParameter
		{
			value = 0.5f
		};

		// Token: 0x0400004A RID: 74
		[Tooltip("Clamps pixels to control the bloom amount. Value is in gamma-space.")]
		public FloatParameter clamp = new FloatParameter
		{
			value = 65472f
		};

		// Token: 0x0400004B RID: 75
		[Range(1f, 10f)]
		[Tooltip("Changes the extent of veiling effects. For maximum quality, use integer values. Because this value changes the internal iteration count, You should not animating it as it may introduce issues with the perceived radius.")]
		public FloatParameter diffusion = new FloatParameter
		{
			value = 7f
		};

		// Token: 0x0400004C RID: 76
		[Range(-1f, 1f)]
		[Tooltip("Distorts the bloom to give an anamorphic look. Negative values distort vertically, positive values distort horizontally.")]
		public FloatParameter anamorphicRatio = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x0400004D RID: 77
		[ColorUsage(false, true)]
		[Tooltip("Global tint of the bloom filter.")]
		public ColorParameter color = new ColorParameter
		{
			value = Color.white
		};

		// Token: 0x0400004E RID: 78
		[FormerlySerializedAs("mobileOptimized")]
		[Tooltip("Boost performance by lowering the effect quality. This settings is meant to be used on mobile and other low-end platforms but can also provide a nice performance boost on desktops and consoles.")]
		public BoolParameter fastMode = new BoolParameter
		{
			value = false
		};

		// Token: 0x0400004F RID: 79
		[Tooltip("The lens dirt texture used to add smudges or dust to the bloom effect.")]
		[DisplayName("Texture")]
		public TextureParameter dirtTexture = new TextureParameter
		{
			value = null
		};

		// Token: 0x04000050 RID: 80
		[Min(0f)]
		[Tooltip("The intensity of the lens dirtiness.")]
		[DisplayName("Intensity")]
		public FloatParameter dirtIntensity = new FloatParameter
		{
			value = 0f
		};
	}
}
