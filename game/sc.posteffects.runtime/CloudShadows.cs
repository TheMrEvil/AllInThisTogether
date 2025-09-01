using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x0200000A RID: 10
	[PostProcess(typeof(CloudShadowsRenderer), PostProcessEvent.BeforeStack, "SC Post Effects/Environment/Cloud Shadows", true)]
	[Serializable]
	public sealed class CloudShadows : PostProcessEffectSettings
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002DC7 File Offset: 0x00000FC7
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.density != 0f && !(this.texture.value == null);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002E00 File Offset: 0x00001000
		public CloudShadows()
		{
		}

		// Token: 0x04000028 RID: 40
		[DisplayName("Texture (R)")]
		[Tooltip("The red channel of this texture is used to sample the clouds")]
		public TextureParameter texture = new TextureParameter
		{
			value = null
		};

		// Token: 0x04000029 RID: 41
		[Range(0f, 1f)]
		[DisplayName("Density")]
		public FloatParameter density = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x0400002A RID: 42
		[Space]
		[Range(0f, 1f)]
		[DisplayName("Size")]
		public FloatParameter size = new FloatParameter
		{
			value = 0.5f
		};

		// Token: 0x0400002B RID: 43
		[Range(0f, 1f)]
		[DisplayName("Speed")]
		public FloatParameter speed = new FloatParameter
		{
			value = 0.5f
		};

		// Token: 0x0400002C RID: 44
		[DisplayName("Direction")]
		[Tooltip("Set the X and Z world-space direction the clouds should move in")]
		public Vector2Parameter direction = new Vector2Parameter
		{
			value = new Vector2(0f, 1f)
		};

		// Token: 0x0400002D RID: 45
		public BoolParameter projectFromSun = new BoolParameter
		{
			value = false
		};

		// Token: 0x0400002E RID: 46
		public FloatParameter startFadeDistance = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x0400002F RID: 47
		public FloatParameter endFadeDistance = new FloatParameter
		{
			value = 200f
		};

		// Token: 0x04000030 RID: 48
		public static bool isOrtho;
	}
}
