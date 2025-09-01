using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000040 RID: 64
	public sealed class SharpenRenderer : PostProcessEffectRenderer<Sharpen>
	{
		// Token: 0x060000BB RID: 187 RVA: 0x000072D7 File Offset: 0x000054D7
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Sharpen");
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000072E9 File Offset: 0x000054E9
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000072F4 File Offset: 0x000054F4
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			propertySheet.properties.SetVector("_Params", new Vector4(base.settings.amount, base.settings.radius, 0f, 0f));
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0, false, null, false);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00007376 File Offset: 0x00005576
		public SharpenRenderer()
		{
		}

		// Token: 0x04000134 RID: 308
		private Shader shader;
	}
}
