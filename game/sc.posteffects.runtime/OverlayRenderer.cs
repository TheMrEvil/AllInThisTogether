using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x0200002F RID: 47
	public sealed class OverlayRenderer : PostProcessEffectRenderer<Overlay>
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00006854 File Offset: 0x00004A54
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Overlay");
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00006866 File Offset: 0x00004A66
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00006870 File Offset: 0x00004A70
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			Matrix4x4 gpuprojectionMatrix = GL.GetGPUProjectionMatrix(context.camera.projectionMatrix, false);
			gpuprojectionMatrix[2, 3] = (gpuprojectionMatrix[3, 2] = 0f);
			gpuprojectionMatrix[3, 3] = 1f;
			Matrix4x4 value = Matrix4x4.Inverse(gpuprojectionMatrix * context.camera.worldToCameraMatrix) * Matrix4x4.TRS(new Vector3(0f, 0f, -gpuprojectionMatrix[2, 2]), Quaternion.identity, Vector3.one);
			propertySheet.properties.SetMatrix("clipToWorld", value);
			if (base.settings.overlayTex.value)
			{
				propertySheet.properties.SetTexture("_OverlayTex", base.settings.overlayTex);
			}
			if (base.settings.shadowTex.value)
			{
				propertySheet.properties.SetTexture("_ShadowTex", base.settings.shadowTex);
			}
			propertySheet.properties.SetFloat("_DistanceMin", base.settings.distanceMin);
			propertySheet.properties.SetVector("_Params", new Vector4(base.settings.intensity, Mathf.Pow(base.settings.tiling + 1f, 2f), base.settings.distance, (float)base.settings.blendMode.value));
			float value2 = (QualitySettings.activeColorSpace == ColorSpace.Gamma) ? Mathf.LinearToGammaSpace(base.settings.luminanceThreshold.value) : base.settings.luminanceThreshold.value;
			propertySheet.properties.SetFloat("_LuminanceThreshold", value2);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0, false, null, false);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00006A79 File Offset: 0x00004C79
		public OverlayRenderer()
		{
		}

		// Token: 0x040000EA RID: 234
		private Shader shader;
	}
}
