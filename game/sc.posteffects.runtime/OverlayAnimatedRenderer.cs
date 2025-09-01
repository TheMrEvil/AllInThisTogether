using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x0200002E RID: 46
	public sealed class OverlayAnimatedRenderer : PostProcessEffectRenderer<OverlayAnimated>
	{
		// Token: 0x0600008E RID: 142 RVA: 0x000065CC File Offset: 0x000047CC
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/OverlayAnimated");
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000065DE File Offset: 0x000047DE
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000065E8 File Offset: 0x000047E8
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			if (base.settings.overlayTex.value)
			{
				propertySheet.properties.SetTexture("_OverlayTex", base.settings.overlayTex);
			}
			if (base.settings.overlayDistort.value)
			{
				propertySheet.properties.SetTexture("_NoiseTex", base.settings.overlayDistort);
			}
			if (base.settings.overlayMask.value)
			{
				propertySheet.properties.SetTexture("_MaskTex", base.settings.overlayMask);
			}
			propertySheet.properties.SetVector("_Params", new Vector4(base.settings.intensity, Mathf.Pow(base.settings.tilingFloat + 1f, 2f), base.settings.autoAspect ? 1f : 0f, (float)base.settings.blendMode.value));
			propertySheet.properties.SetVector("_DistortParams", new Vector4(base.settings.distortScale, Mathf.Pow(base.settings.distortTilingFloat + 1f, 2f), base.settings.distortSpeed, 0f));
			propertySheet.properties.SetVector("_MaskParams", new Vector4(base.settings.maskStart, base.settings.maskEnd, base.settings.invertMask ? 1f : 0f, 0f));
			float value = (QualitySettings.activeColorSpace == ColorSpace.Gamma) ? Mathf.LinearToGammaSpace(base.settings.luminanceThreshold.value) : base.settings.luminanceThreshold.value;
			propertySheet.properties.SetFloat("_LuminanceThreshold", value);
			propertySheet.properties.SetColor("_Color", base.settings.color);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0, false, null, false);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000684C File Offset: 0x00004A4C
		public OverlayAnimatedRenderer()
		{
		}

		// Token: 0x040000E9 RID: 233
		private Shader shader;
	}
}
