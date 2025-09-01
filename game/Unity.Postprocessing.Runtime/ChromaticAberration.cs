using System;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000019 RID: 25
	[PostProcess(typeof(ChromaticAberrationRenderer), "Unity/Chromatic Aberration", true)]
	[Serializable]
	public sealed class ChromaticAberration : PostProcessEffectSettings
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00003288 File Offset: 0x00001488
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.intensity.value > 0f;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000032AC File Offset: 0x000014AC
		public ChromaticAberration()
		{
		}

		// Token: 0x04000053 RID: 83
		[Tooltip("Shifts the hue of chromatic aberrations.")]
		public TextureParameter spectralLut = new TextureParameter
		{
			value = null
		};

		// Token: 0x04000054 RID: 84
		[Range(0f, 1f)]
		[Tooltip("Amount of tangential distortion.")]
		public FloatParameter intensity = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000055 RID: 85
		[FormerlySerializedAs("mobileOptimized")]
		[Tooltip("Boost performances by lowering the effect quality. This settings is meant to be used on mobile and other low-end platforms but can also provide a nice performance boost on desktops and consoles.")]
		public BoolParameter fastMode = new BoolParameter
		{
			value = false
		};
	}
}
