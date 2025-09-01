using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000022 RID: 34
	[PostProcess(typeof(KuwaharaRenderer), PostProcessEvent.BeforeStack, "SC Post Effects/Stylized/Kuwahara", true)]
	[Serializable]
	public sealed class Kuwahara : PostProcessEffectSettings
	{
		// Token: 0x0600006B RID: 107 RVA: 0x00005300 File Offset: 0x00003500
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.radius != 0;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00005324 File Offset: 0x00003524
		public Kuwahara()
		{
		}

		// Token: 0x040000A1 RID: 161
		[DisplayName("Method")]
		[Tooltip("Choose to apply the effect to the entire screen, or fade in/out over a distance")]
		public Kuwahara.KuwaharaModeParam mode = new Kuwahara.KuwaharaModeParam
		{
			value = Kuwahara.KuwaharaMode.FullScreen
		};

		// Token: 0x040000A2 RID: 162
		[Range(0f, 8f)]
		[DisplayName("Radius")]
		public IntParameter radius = new IntParameter
		{
			value = 0
		};

		// Token: 0x040000A3 RID: 163
		public FloatParameter startFadeDistance = new FloatParameter
		{
			value = 100f
		};

		// Token: 0x040000A4 RID: 164
		public FloatParameter endFadeDistance = new FloatParameter
		{
			value = 500f
		};

		// Token: 0x0200006A RID: 106
		public enum KuwaharaMode
		{
			// Token: 0x040001A5 RID: 421
			FullScreen,
			// Token: 0x040001A6 RID: 422
			DepthFade
		}

		// Token: 0x0200006B RID: 107
		[Serializable]
		public sealed class KuwaharaModeParam : ParameterOverride<Kuwahara.KuwaharaMode>
		{
			// Token: 0x060000FB RID: 251 RVA: 0x00008540 File Offset: 0x00006740
			public KuwaharaModeParam()
			{
			}
		}
	}
}
