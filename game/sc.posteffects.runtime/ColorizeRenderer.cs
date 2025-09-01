using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x0200000F RID: 15
	public sealed class ColorizeRenderer : PostProcessEffectRenderer<Colorize>
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00003269 File Offset: 0x00001469
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Colorize");
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000327B File Offset: 0x0000147B
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003284 File Offset: 0x00001484
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			if (base.settings.colorRamp.value)
			{
				propertySheet.properties.SetTexture("_ColorRamp", base.settings.colorRamp);
			}
			propertySheet.properties.SetFloat("_Intensity", base.settings.intensity);
			propertySheet.properties.SetFloat("_BlendMode", (float)base.settings.mode.value);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0, false, null, false);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000333F File Offset: 0x0000153F
		public ColorizeRenderer()
		{
		}

		// Token: 0x04000038 RID: 56
		private Shader shader;
	}
}
