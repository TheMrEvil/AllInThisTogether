using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000005 RID: 5
	public sealed class BlackBarsRenderer : PostProcessEffectRenderer<BlackBars>
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000024E1 File Offset: 0x000006E1
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Black Bars");
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000024F3 File Offset: 0x000006F3
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000024FC File Offset: 0x000006FC
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			propertySheet.properties.SetVector("_Size", new Vector2(base.settings.size / 10f, base.settings.maxSize * 5f));
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, (int)base.settings.mode.value, false, null, false);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002594 File Offset: 0x00000794
		public BlackBarsRenderer()
		{
		}

		// Token: 0x0400000F RID: 15
		private Shader shader;
	}
}
