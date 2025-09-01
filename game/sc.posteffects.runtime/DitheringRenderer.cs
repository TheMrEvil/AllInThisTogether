using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000013 RID: 19
	public sealed class DitheringRenderer : PostProcessEffectRenderer<Dithering>
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00003593 File Offset: 0x00001793
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Dithering");
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000035A5 File Offset: 0x000017A5
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000035B0 File Offset: 0x000017B0
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			Texture value = (base.settings.lut.value == null) ? RuntimeUtilities.blackTexture : base.settings.lut.value;
			propertySheet.properties.SetTexture("_LUT", value);
			float z = (QualitySettings.activeColorSpace == ColorSpace.Gamma) ? Mathf.LinearToGammaSpace(base.settings.luminanceThreshold.value) : base.settings.luminanceThreshold.value;
			Vector4 value2 = new Vector4(0f, base.settings.tiling, z, base.settings.intensity);
			propertySheet.properties.SetVector("_Dithering_Coords", value2);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0, false, null, false);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000036A2 File Offset: 0x000018A2
		public DitheringRenderer()
		{
		}

		// Token: 0x04000042 RID: 66
		private Shader shader;
	}
}
