using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x0200002B RID: 43
	public sealed class MosaicRenderer : PostProcessEffectRenderer<Mosaic>
	{
		// Token: 0x06000085 RID: 133 RVA: 0x000061B8 File Offset: 0x000043B8
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Mosaic");
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000061CA File Offset: 0x000043CA
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000061D4 File Offset: 0x000043D4
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			float num = base.settings.size.value;
			switch (base.settings.mode)
			{
			case Mosaic.MosaicMode.Triangles:
				num = 10f / base.settings.size.value;
				break;
			case Mosaic.MosaicMode.Hexagons:
				num = base.settings.size.value / 10f;
				break;
			case Mosaic.MosaicMode.Circles:
				num = (1f - base.settings.size.value) * 100f;
				break;
			}
			Vector4 value = new Vector4(num, (float)(context.screenWidth * 2 / context.screenHeight) * num / Mathf.Sqrt(3f), 0f, 0f);
			propertySheet.properties.SetVector("_Params", value);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, (int)base.settings.mode.value, false, null, false);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000062EC File Offset: 0x000044EC
		public MosaicRenderer()
		{
		}

		// Token: 0x040000CE RID: 206
		private Shader shader;
	}
}
