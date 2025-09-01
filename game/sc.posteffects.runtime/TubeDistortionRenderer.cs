using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x0200004E RID: 78
	public sealed class TubeDistortionRenderer : PostProcessEffectRenderer<TubeDistortion>
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x00008396 File Offset: 0x00006596
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Tube Distortion");
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000083A8 File Offset: 0x000065A8
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000083B0 File Offset: 0x000065B0
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			propertySheet.properties.SetFloat("_Amount", base.settings.amount.value);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, (int)base.settings.mode.value, false, null, false);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00008422 File Offset: 0x00006622
		public TubeDistortionRenderer()
		{
		}

		// Token: 0x04000164 RID: 356
		private Shader shader;
	}
}
