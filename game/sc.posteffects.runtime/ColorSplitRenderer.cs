using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x0200000D RID: 13
	public sealed class ColorSplitRenderer : PostProcessEffectRenderer<ColorSplit>
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00003147 File Offset: 0x00001347
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Color Split");
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003159 File Offset: 0x00001359
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003164 File Offset: 0x00001364
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			propertySheet.properties.SetFloat("_Offset", base.settings.offset.value / 100f);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, (int)base.settings.mode.value, false, null, false);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000031DC File Offset: 0x000013DC
		public ColorSplitRenderer()
		{
		}

		// Token: 0x04000034 RID: 52
		private Shader shader;
	}
}
