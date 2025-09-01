using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x0200001F RID: 31
	public sealed class HueShift3DRenderer : PostProcessEffectRenderer<HueShift3D>
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00005117 File Offset: 0x00003317
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/3D Hue Shift");
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00005129 File Offset: 0x00003329
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00005134 File Offset: 0x00003334
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			HueShift3D.isOrtho = context.camera.orthographic;
			propertySheet.properties.SetVector("_Params", new Vector4(base.settings.speed.value, base.settings.size.value, base.settings.geoInfluence.value, base.settings.intensity.value));
			if (base.settings.gradientTex.value)
			{
				propertySheet.properties.SetTexture("_GradientTex", base.settings.gradientTex.value);
			}
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, (base.settings.colorSource.value == HueShift3D.ColorSource.RGBSpectrum) ? 0 : 1, false, null, false);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00005228 File Offset: 0x00003428
		public override DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.DepthNormals;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000522B File Offset: 0x0000342B
		public HueShift3DRenderer()
		{
		}

		// Token: 0x0400009E RID: 158
		private Shader shader;

		// Token: 0x02000069 RID: 105
		private enum Pass
		{
			// Token: 0x040001A2 RID: 418
			ColorSpectrum,
			// Token: 0x040001A3 RID: 419
			GradientTexture
		}
	}
}
