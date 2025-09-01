using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000037 RID: 55
	public sealed class RefractionRenderer : PostProcessEffectRenderer<Refraction>
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x00006E99 File Offset: 0x00005099
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Refraction");
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00006EAB File Offset: 0x000050AB
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00006EB4 File Offset: 0x000050B4
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			propertySheet.properties.SetFloat("_Amount", base.settings.amount);
			if (base.settings.refractionTex.value)
			{
				propertySheet.properties.SetTexture("_RefractionTex", base.settings.refractionTex);
			}
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, base.settings.convertNormalMap ? 1 : 0, false, null, false);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00006F63 File Offset: 0x00005163
		public RefractionRenderer()
		{
		}

		// Token: 0x040000FA RID: 250
		private Shader shader;
	}
}
