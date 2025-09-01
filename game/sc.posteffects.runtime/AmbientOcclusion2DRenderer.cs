using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000003 RID: 3
	public sealed class AmbientOcclusion2DRenderer : PostProcessEffectRenderer<AmbientOcclusion2D>
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002119 File Offset: 0x00000319
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Ambient Occlusion 2D");
			this.aoTexID = Shader.PropertyToID("_AO");
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000213B File Offset: 0x0000033B
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002144 File Offset: 0x00000344
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			CommandBuffer command = context.command;
			propertySheet.properties.SetFloat("_SampleDistance", base.settings.distance);
			float value = (QualitySettings.activeColorSpace == ColorSpace.Gamma) ? Mathf.GammaToLinearSpace(base.settings.luminanceThreshold.value) : base.settings.luminanceThreshold.value;
			propertySheet.properties.SetFloat("_Threshold", value);
			propertySheet.properties.SetFloat("_Blur", base.settings.blurAmount);
			propertySheet.properties.SetFloat("_Intensity", base.settings.intensity);
			context.command.GetTemporaryRT(this.aoTexID, context.width, context.height, 0, FilterMode.Bilinear, context.sourceFormat);
			context.command.BlitFullscreenTriangle(context.source, this.aoTexID, propertySheet, 0, false, null, false);
			int nameID = Shader.PropertyToID("_Temp1");
			int nameID2 = Shader.PropertyToID("_Temp2");
			command.GetTemporaryRT(nameID, context.screenWidth / base.settings.downscaling, context.screenHeight / base.settings.downscaling, 0, FilterMode.Bilinear);
			command.GetTemporaryRT(nameID2, context.screenWidth / base.settings.downscaling, context.screenHeight / base.settings.downscaling, 0, FilterMode.Bilinear);
			command.Blit(this.aoTexID, nameID);
			for (int i = 0; i < base.settings.iterations; i++)
			{
				command.SetGlobalVector("_BlurOffsets", new Vector4(base.settings.blurAmount / (float)context.screenWidth, 0f, 0f, 0f));
				context.command.BlitFullscreenTriangle(nameID, nameID2, propertySheet, 1, false, null, false);
				command.SetGlobalVector("_BlurOffsets", new Vector4(0f, base.settings.blurAmount / (float)context.screenHeight, 0f, 0f));
				context.command.BlitFullscreenTriangle(nameID2, nameID, propertySheet, 1, false, null, false);
			}
			context.command.SetGlobalTexture("_AO", nameID);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, base.settings.aoOnly ? 3 : 2, false, null, false);
			context.command.ReleaseTemporaryRT(nameID);
			context.command.ReleaseTemporaryRT(nameID2);
			context.command.ReleaseTemporaryRT(this.aoTexID);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000244F File Offset: 0x0000064F
		public AmbientOcclusion2DRenderer()
		{
		}

		// Token: 0x04000008 RID: 8
		private Shader shader;

		// Token: 0x04000009 RID: 9
		private int aoTexID;

		// Token: 0x0400000A RID: 10
		private int screenCopyID;

		// Token: 0x0400000B RID: 11
		private RenderTexture aoRT;

		// Token: 0x02000050 RID: 80
		private enum Pass
		{
			// Token: 0x04000169 RID: 361
			LuminanceDiff,
			// Token: 0x0400016A RID: 362
			Blur,
			// Token: 0x0400016B RID: 363
			Blend,
			// Token: 0x0400016C RID: 364
			Debug
		}
	}
}
