using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x0200001D RID: 29
	public sealed class GradientRenderer : PostProcessEffectRenderer<Gradient>
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00004F0D File Offset: 0x0000310D
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Gradient");
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004F1F File Offset: 0x0000311F
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004F28 File Offset: 0x00003128
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			if (base.settings.gradientTex.value)
			{
				propertySheet.properties.SetTexture("_Gradient", base.settings.gradientTex);
			}
			propertySheet.properties.SetColor("_Color1", base.settings.color1);
			propertySheet.properties.SetColor("_Color2", base.settings.color2);
			propertySheet.properties.SetFloat("_Rotation", base.settings.rotation * 6f);
			propertySheet.properties.SetFloat("_Intensity", base.settings.intensity);
			propertySheet.properties.SetFloat("_BlendMode", (float)base.settings.mode.value);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, (int)base.settings.input.value, false, null, false);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00005058 File Offset: 0x00003258
		public GradientRenderer()
		{
		}

		// Token: 0x04000096 RID: 150
		private Shader shader;
	}
}
