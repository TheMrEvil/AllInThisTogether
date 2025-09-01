using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000021 RID: 33
	public sealed class KaleidoscopeRenderer : PostProcessEffectRenderer<Kaleidoscope>
	{
		// Token: 0x06000067 RID: 103 RVA: 0x0000526E File Offset: 0x0000346E
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Kaleidoscope");
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00005280 File Offset: 0x00003480
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00005288 File Offset: 0x00003488
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			propertySheet.properties.SetFloat("_Splits", 6.2831855f / (float)Mathf.Max(1, base.settings.splits.value));
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0, false, null, false);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000052F8 File Offset: 0x000034F8
		public KaleidoscopeRenderer()
		{
		}

		// Token: 0x040000A0 RID: 160
		private Shader shader;
	}
}
