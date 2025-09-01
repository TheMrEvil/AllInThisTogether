using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000033 RID: 51
	public sealed class PosterizeRenderer : PostProcessEffectRenderer<Posterize>
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00006C17 File Offset: 0x00004E17
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Posterize");
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00006C2C File Offset: 0x00004E2C
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			propertySheet.properties.SetFloat("_Opacity", base.settings.opacity.value);
			propertySheet.properties.SetVector("_Params", new Vector4((float)base.settings.hue.value, (float)base.settings.saturation.value, (float)base.settings.value.value, (float)base.settings.levels.value));
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, base.settings.hsvMode.value ? 1 : 0, false, null, false);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00006CFD File Offset: 0x00004EFD
		public PosterizeRenderer()
		{
		}

		// Token: 0x040000F3 RID: 243
		private Shader shader;
	}
}
