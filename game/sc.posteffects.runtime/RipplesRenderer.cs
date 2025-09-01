using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000039 RID: 57
	public sealed class RipplesRenderer : PostProcessEffectRenderer<Ripples>
	{
		// Token: 0x060000AF RID: 175 RVA: 0x00007027 File Offset: 0x00005227
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Ripples");
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00007039 File Offset: 0x00005239
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00007044 File Offset: 0x00005244
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			propertySheet.properties.SetFloat("_Strength", base.settings.strength * 0.01f);
			propertySheet.properties.SetFloat("_Distance", base.settings.distance * 0.01f);
			propertySheet.properties.SetFloat("_Speed", base.settings.speed);
			propertySheet.properties.SetVector("_Size", new Vector2(base.settings.width, base.settings.height));
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, (int)base.settings.mode.value, false, null, false);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000713C File Offset: 0x0000533C
		public RipplesRenderer()
		{
		}

		// Token: 0x04000101 RID: 257
		private Shader shader;
	}
}
