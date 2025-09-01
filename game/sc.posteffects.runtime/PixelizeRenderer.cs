using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000031 RID: 49
	public sealed class PixelizeRenderer : PostProcessEffectRenderer<Pixelize>
	{
		// Token: 0x06000098 RID: 152 RVA: 0x00006AC5 File Offset: 0x00004CC5
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Pixelize");
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00006AD7 File Offset: 0x00004CD7
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00006AE0 File Offset: 0x00004CE0
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			propertySheet.properties.SetFloat("_Resolution", base.settings.amount / 10f);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0, false, null, false);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00006B49 File Offset: 0x00004D49
		public PixelizeRenderer()
		{
		}

		// Token: 0x040000EC RID: 236
		private Shader shader;
	}
}
