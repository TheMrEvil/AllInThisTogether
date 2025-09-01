using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x0200003B RID: 59
	public sealed class ScanlinesRenderer : PostProcessEffectRenderer<Scanlines>
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x000071C1 File Offset: 0x000053C1
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Scanlines");
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000071D3 File Offset: 0x000053D3
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000071DC File Offset: 0x000053DC
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			propertySheet.properties.SetVector("_Params", new Vector4(base.settings.amount, base.settings.intensity / 1000f, base.settings.speed * 8f, 0f));
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0, false, null, false);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00007275 File Offset: 0x00005475
		public ScanlinesRenderer()
		{
		}

		// Token: 0x04000105 RID: 261
		private Shader shader;
	}
}
