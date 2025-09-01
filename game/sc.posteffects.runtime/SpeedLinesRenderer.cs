using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000044 RID: 68
	public sealed class SpeedLinesRenderer : PostProcessEffectRenderer<SpeedLines>
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x000076BF File Offset: 0x000058BF
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/SpeedLines");
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000076D1 File Offset: 0x000058D1
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000076DC File Offset: 0x000058DC
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			float y = 2f + (base.settings.falloff.value - 0f) * 14f / 1f;
			propertySheet.properties.SetVector("_Params", new Vector4(base.settings.intensity.value, y, base.settings.size.value * 2f, 0f));
			if (base.settings.noiseTex.value)
			{
				propertySheet.properties.SetTexture("_NoiseTex", base.settings.noiseTex.value);
			}
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0, false, null, false);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000077C0 File Offset: 0x000059C0
		public SpeedLinesRenderer()
		{
		}

		// Token: 0x04000140 RID: 320
		private Shader shader;
	}
}
