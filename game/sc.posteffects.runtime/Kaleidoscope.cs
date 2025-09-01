using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000020 RID: 32
	[PostProcess(typeof(KaleidoscopeRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Misc/Kaleidoscope", true)]
	[Serializable]
	public sealed class Kaleidoscope : PostProcessEffectSettings
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00005233 File Offset: 0x00003433
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.splits != 0;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00005254 File Offset: 0x00003454
		public Kaleidoscope()
		{
		}

		// Token: 0x0400009F RID: 159
		[Range(0f, 10f)]
		[Tooltip("The number of times the screen is split up")]
		public IntParameter splits = new IntParameter
		{
			value = 0
		};
	}
}
