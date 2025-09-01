using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000027 RID: 39
	public sealed class LightStreaksRenderer : PostProcessEffectRenderer<LightStreaks>
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00005B5F File Offset: 0x00003D5F
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Light Streaks");
			this.emissionTex = Shader.PropertyToID("_BloomTex");
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00005B81 File Offset: 0x00003D81
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00005B8C File Offset: 0x00003D8C
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			CommandBuffer command = context.command;
			int pass = (base.settings.quality.value == LightStreaks.Quality.Performance) ? 1 : 2;
			float x = Mathf.GammaToLinearSpace(base.settings.luminanceThreshold.value);
			propertySheet.properties.SetVector("_Params", new Vector4(x, base.settings.intensity.value, 0f, 0f));
			context.command.GetTemporaryRT(this.emissionTex, context.width, context.height, 0, FilterMode.Bilinear, context.sourceFormat);
			context.command.BlitFullscreenTriangle(context.source, this.emissionTex, propertySheet, 0, false, null, false);
			int num = base.settings.downscaling + 1;
			int nameID = Shader.PropertyToID("_Temp1");
			int nameID2 = Shader.PropertyToID("_Temp2");
			command.GetTemporaryRT(nameID, context.width / num, context.height / num, 0, FilterMode.Bilinear);
			command.GetTemporaryRT(nameID2, context.width / num, context.height / num, 0, FilterMode.Bilinear);
			command.Blit(this.emissionTex, nameID);
			float num2 = Mathf.Clamp(base.settings.direction.value, -1f, 1f);
			float num3 = (num2 < 0f) ? (-num2 * 16f) : 0f;
			float num4 = (num2 > 0f) ? (num2 * 8f) : 0f;
			int num5 = (base.settings.quality.value == LightStreaks.Quality.Performance) ? (base.settings.iterations.value * 3) : base.settings.iterations.value;
			for (int i = 0; i < num5; i++)
			{
				command.SetGlobalVector("_BlurOffsets", new Vector4(num3 * base.settings.blur / (float)context.screenWidth, num4 / (float)context.screenHeight, 0f, 0f));
				context.command.BlitFullscreenTriangle(nameID, nameID2, propertySheet, pass, false, null, false);
				command.SetGlobalVector("_BlurOffsets", new Vector4(num3 * base.settings.blur * 2f / (float)context.screenWidth, num4 * 2f / (float)context.screenHeight, 0f, 0f));
				context.command.BlitFullscreenTriangle(nameID2, nameID, propertySheet, pass, false, null, false);
			}
			context.command.SetGlobalTexture("_BloomTex", nameID);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, base.settings.debug ? 4 : 3, false, null, false);
			context.command.ReleaseTemporaryRT(nameID);
			context.command.ReleaseTemporaryRT(nameID2);
			context.command.ReleaseTemporaryRT(this.emissionTex);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00005ED3 File Offset: 0x000040D3
		public LightStreaksRenderer()
		{
		}

		// Token: 0x040000C0 RID: 192
		private Shader shader;

		// Token: 0x040000C1 RID: 193
		private int emissionTex;

		// Token: 0x040000C2 RID: 194
		private RenderTexture aoRT;

		// Token: 0x0200006F RID: 111
		private enum Pass
		{
			// Token: 0x040001B1 RID: 433
			LuminanceDiff,
			// Token: 0x040001B2 RID: 434
			BlurFast,
			// Token: 0x040001B3 RID: 435
			Blur,
			// Token: 0x040001B4 RID: 436
			Blend,
			// Token: 0x040001B5 RID: 437
			Debug
		}
	}
}
