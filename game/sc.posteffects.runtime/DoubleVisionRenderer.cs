using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000015 RID: 21
	public sealed class DoubleVisionRenderer : PostProcessEffectRenderer<DoubleVision>
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00003700 File Offset: 0x00001900
		public override void Init()
		{
			this.DoubleVisionShader = Shader.Find("Hidden/SC Post Effects/Double Vision");
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003712 File Offset: 0x00001912
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000371C File Offset: 0x0000191C
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.DoubleVisionShader);
			propertySheet.properties.SetFloat("_Amount", base.settings.intensity.value / 10f);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, (int)base.settings.mode.value, false, null, false);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003794 File Offset: 0x00001994
		public DoubleVisionRenderer()
		{
		}

		// Token: 0x04000045 RID: 69
		private Shader DoubleVisionShader;
	}
}
