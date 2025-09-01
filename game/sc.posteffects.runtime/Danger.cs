using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000010 RID: 16
	[PostProcess(typeof(DangerRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Screen/Danger", true)]
	[Serializable]
	public sealed class Danger : PostProcessEffectSettings
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00003347 File Offset: 0x00001547
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.size != 0f && this.intensity != 0f;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003380 File Offset: 0x00001580
		public Danger()
		{
		}

		// Token: 0x04000039 RID: 57
		public TextureParameter overlayTex = new TextureParameter
		{
			value = null,
			defaultState = TextureParameterDefault.None
		};

		// Token: 0x0400003A RID: 58
		public ColorParameter color = new ColorParameter
		{
			value = new Color(0.66f, 0f, 0f)
		};

		// Token: 0x0400003B RID: 59
		[Range(0f, 1f)]
		[DisplayName("Opacity")]
		public FloatParameter intensity = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x0400003C RID: 60
		[Range(0f, 1f)]
		[Tooltip("Size")]
		public FloatParameter size = new FloatParameter
		{
			value = 0f
		};
	}
}
