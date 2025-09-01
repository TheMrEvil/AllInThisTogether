using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000025 RID: 37
	public sealed class LensFlaresRenderer : PostProcessEffectRenderer<LensFlares>
	{
		// Token: 0x06000073 RID: 115 RVA: 0x000055BD File Offset: 0x000037BD
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Lensflares");
			this.emissionTex = Shader.PropertyToID("_BloomTex");
			this.flaresTex = Shader.PropertyToID("_FlaresTex");
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000055EF File Offset: 0x000037EF
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000055F8 File Offset: 0x000037F8
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			CommandBuffer command = context.command;
			propertySheet.properties.SetFloat("_Intensity", base.settings.intensity);
			float value = Mathf.GammaToLinearSpace(base.settings.luminanceThreshold.value);
			propertySheet.properties.SetFloat("_Threshold", value);
			propertySheet.properties.SetFloat("_Distance", base.settings.distance);
			propertySheet.properties.SetFloat("_Falloff", base.settings.falloff);
			propertySheet.properties.SetFloat("_Ghosts", (float)base.settings.iterations);
			propertySheet.properties.SetFloat("_HaloSize", base.settings.haloSize);
			propertySheet.properties.SetFloat("_HaloWidth", base.settings.haloWidth);
			propertySheet.properties.SetFloat("_ChromaticAbberation", base.settings.chromaticAbberation);
			propertySheet.properties.SetTexture("_ColorTex", base.settings.colorTex.value ? base.settings.colorTex : Texture2D.whiteTexture);
			propertySheet.properties.SetTexture("_MaskTex", base.settings.maskTex.value ? base.settings.maskTex : Texture2D.whiteTexture);
			context.command.GetTemporaryRT(this.emissionTex, context.width, context.height, 0, FilterMode.Bilinear, RenderTextureFormat.DefaultHDR);
			context.command.BlitFullscreenTriangle(context.source, this.emissionTex, propertySheet, 0, false, null, false);
			context.command.SetGlobalTexture("_BloomTex", this.emissionTex);
			context.command.GetTemporaryRT(this.flaresTex, context.width, context.height, 0, FilterMode.Bilinear, RenderTextureFormat.DefaultHDR);
			context.command.BlitFullscreenTriangle(this.emissionTex, this.flaresTex, propertySheet, 1, false, null, false);
			context.command.SetGlobalTexture("_FlaresTex", this.flaresTex);
			int nameID = Shader.PropertyToID("_Temp1");
			int nameID2 = Shader.PropertyToID("_Temp2");
			command.GetTemporaryRT(nameID, context.width / 2, context.height / 2, 0, FilterMode.Bilinear);
			command.GetTemporaryRT(nameID2, context.width / 2, context.height / 2, 0, FilterMode.Bilinear);
			command.Blit(this.flaresTex, nameID);
			command.ReleaseTemporaryRT(this.flaresTex);
			for (int i = 0; i < base.settings.passes; i++)
			{
				command.SetGlobalVector("_BlurOffsets", new Vector4(base.settings.blur / (float)context.screenWidth, 0f, 0f, 0f));
				context.command.BlitFullscreenTriangle(nameID, nameID2, propertySheet, 2, false, null, false);
				command.SetGlobalVector("_BlurOffsets", new Vector4(0f, base.settings.blur / (float)context.screenHeight, 0f, 0f));
				context.command.BlitFullscreenTriangle(nameID2, nameID, propertySheet, 2, false, null, false);
			}
			context.command.SetGlobalTexture("_FlaresTex", nameID);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, base.settings.debug ? 4 : 3, false, null, false);
			context.command.ReleaseTemporaryRT(this.emissionTex);
			context.command.ReleaseTemporaryRT(this.flaresTex);
			context.command.ReleaseTemporaryRT(nameID);
			context.command.ReleaseTemporaryRT(nameID2);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00005A4A File Offset: 0x00003C4A
		public LensFlaresRenderer()
		{
		}

		// Token: 0x040000B4 RID: 180
		private Shader shader;

		// Token: 0x040000B5 RID: 181
		private int emissionTex;

		// Token: 0x040000B6 RID: 182
		private int flaresTex;

		// Token: 0x040000B7 RID: 183
		private RenderTexture aoRT;

		// Token: 0x0200006C RID: 108
		private enum Pass
		{
			// Token: 0x040001A8 RID: 424
			LuminanceDiff,
			// Token: 0x040001A9 RID: 425
			Ghosting,
			// Token: 0x040001AA RID: 426
			Blur,
			// Token: 0x040001AB RID: 427
			Blend,
			// Token: 0x040001AC RID: 428
			Debug
		}
	}
}
