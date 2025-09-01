using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000008 RID: 8
	[PostProcess(typeof(CausticsRenderer), PostProcessEvent.BeforeStack, "SC Post Effects/Environment/Caustics", true)]
	[Serializable]
	public sealed class Caustics : PostProcessEffectSettings
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002A1B File Offset: 0x00000C1B
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.intensity > 0f && this.causticsTexture.value != null;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002A50 File Offset: 0x00000C50
		public Caustics()
		{
		}

		// Token: 0x0400001A RID: 26
		public TextureParameter causticsTexture = new TextureParameter
		{
			value = null
		};

		// Token: 0x0400001B RID: 27
		[Range(0f, 5f)]
		public FloatParameter intensity = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x0400001C RID: 28
		[Tooltip("Draws the caustics on pixels brighter than this threshold, useful to hide the caustics in shadows")]
		[Range(0f, 2f)]
		public FloatParameter luminanceThreshold = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x0400001D RID: 29
		public BoolParameter projectFromSun = new BoolParameter
		{
			value = false
		};

		// Token: 0x0400001E RID: 30
		[Space]
		public FloatParameter minHeight = new FloatParameter
		{
			value = -5f
		};

		// Token: 0x0400001F RID: 31
		[Range(0f, 1f)]
		public FloatParameter minHeightFalloff = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x04000020 RID: 32
		public FloatParameter maxHeight = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000021 RID: 33
		[Range(0f, 1f)]
		public FloatParameter maxHeightFalloff = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x04000022 RID: 34
		[Space]
		[Range(0.1f, 3f)]
		public FloatParameter size = new FloatParameter
		{
			value = 0.5f
		};

		// Token: 0x04000023 RID: 35
		[Range(0f, 1f)]
		public FloatParameter speed = new FloatParameter
		{
			value = 0.2f
		};

		// Token: 0x04000024 RID: 36
		[Space]
		public BoolParameter distanceFade = new BoolParameter
		{
			value = false
		};

		// Token: 0x04000025 RID: 37
		public FloatParameter startFadeDistance = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000026 RID: 38
		public FloatParameter endFadeDistance = new FloatParameter
		{
			value = 200f
		};
	}
}
