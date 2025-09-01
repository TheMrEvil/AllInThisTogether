using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000035 RID: 53
	public sealed class RadialBlurRenderer : PostProcessEffectRenderer<RadialBlur>
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00006D5B File Offset: 0x00004F5B
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Radial Blur");
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00006D6D File Offset: 0x00004F6D
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00006D78 File Offset: 0x00004F78
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			CommandBuffer command = context.command;
			propertySheet.properties.SetFloat("_Amount", base.settings.amount.value / 50f);
			propertySheet.properties.SetFloat("_Iterations", (float)base.settings.iterations.value);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0, false, null, false);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00006E09 File Offset: 0x00005009
		public RadialBlurRenderer()
		{
		}

		// Token: 0x040000F6 RID: 246
		private Shader shader;
	}
}
