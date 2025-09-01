using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000007 RID: 7
	public sealed class BlurRenderer : PostProcessEffectRenderer<Blur>
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000266F File Offset: 0x0000086F
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Blur");
			this.screenCopyID = Shader.PropertyToID("_ScreenCopyTexture");
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002694 File Offset: 0x00000894
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			CommandBuffer command = context.command;
			command.GetTemporaryRT(this.screenCopyID, context.width, context.height, 0, FilterMode.Bilinear, context.sourceFormat);
			command.Blit(context.source, this.screenCopyID);
			int nameID = Shader.PropertyToID("_Temp1");
			int nameID2 = Shader.PropertyToID("_Temp2");
			command.GetTemporaryRT(nameID, context.screenWidth / base.settings.downscaling, context.screenHeight / base.settings.downscaling, 0, FilterMode.Bilinear);
			command.GetTemporaryRT(nameID2, context.screenWidth / base.settings.downscaling, context.screenHeight / base.settings.downscaling, 0, FilterMode.Bilinear);
			command.Blit(this.screenCopyID, nameID);
			int pass = (base.settings.mode == Blur.BlurMethod.Gaussian) ? 2 : 3;
			for (int i = 0; i < base.settings.iterations; i++)
			{
				if (base.settings.iterations > 12)
				{
					return;
				}
				command.SetGlobalVector("_BlurOffsets", new Vector4(base.settings.amount / (float)context.screenWidth, 0f, 0f, 0f));
				context.command.BlitFullscreenTriangle(nameID, nameID2, propertySheet, pass, false, null, false);
				command.SetGlobalVector("_BlurOffsets", new Vector4(0f, base.settings.amount / (float)context.screenHeight, 0f, 0f));
				context.command.BlitFullscreenTriangle(nameID2, nameID, propertySheet, pass, false, null, false);
				if (base.settings.highQuality)
				{
					command.SetGlobalVector("_BlurOffsets", new Vector4(base.settings.amount / (float)context.screenWidth, 0f, 0f, 0f));
					context.command.BlitFullscreenTriangle(nameID, nameID2, propertySheet, pass, false, null, false);
					command.SetGlobalVector("_BlurOffsets", new Vector4(0f, base.settings.amount / (float)context.screenHeight, 0f, 0f));
					context.command.BlitFullscreenTriangle(nameID2, nameID, propertySheet, pass, false, null, false);
				}
			}
			command.SetGlobalTexture("_BlurredTex", nameID);
			if (base.settings.distanceFade.value)
			{
				command.SetGlobalVector("_FadeParams", new Vector4(base.settings.startFadeDistance.value, base.settings.endFadeDistance.value, 0f, 0f));
			}
			command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, base.settings.distanceFade.value ? 1 : 0, false, null, false);
			command.ReleaseTemporaryRT(this.screenCopyID);
			command.ReleaseTemporaryRT(nameID);
			command.ReleaseTemporaryRT(nameID2);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002A13 File Offset: 0x00000C13
		public BlurRenderer()
		{
		}

		// Token: 0x04000018 RID: 24
		private Shader shader;

		// Token: 0x04000019 RID: 25
		private int screenCopyID;

		// Token: 0x02000055 RID: 85
		private enum Pass
		{
			// Token: 0x04000174 RID: 372
			Blend,
			// Token: 0x04000175 RID: 373
			BlendDepthFade,
			// Token: 0x04000176 RID: 374
			Gaussian,
			// Token: 0x04000177 RID: 375
			Box
		}
	}
}
