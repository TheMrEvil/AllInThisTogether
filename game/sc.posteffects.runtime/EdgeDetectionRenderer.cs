using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000017 RID: 23
	public sealed class EdgeDetectionRenderer : PostProcessEffectRenderer<EdgeDetection>
	{
		// Token: 0x0600003F RID: 63 RVA: 0x0000394B File Offset: 0x00001B4B
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Edge Detection");
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000395D File Offset: 0x00001B5D
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003968 File Offset: 0x00001B68
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			CommandBuffer command = context.command;
			Matrix4x4 gpuprojectionMatrix = GL.GetGPUProjectionMatrix(context.camera.projectionMatrix, false);
			gpuprojectionMatrix[2, 3] = (gpuprojectionMatrix[3, 2] = 0f);
			gpuprojectionMatrix[3, 3] = 1f;
			Matrix4x4 value = Matrix4x4.Inverse(gpuprojectionMatrix * context.camera.worldToCameraMatrix) * Matrix4x4.TRS(new Vector3(0f, 0f, -gpuprojectionMatrix[2, 2]), Quaternion.identity, Vector3.one);
			propertySheet.properties.SetMatrix("clipToWorld", value);
			Vector2 v = new Vector2(base.settings.sensitivityDepth, base.settings.sensitivityNormals);
			propertySheet.properties.SetVector("_Sensitivity", v);
			propertySheet.properties.SetFloat("_BackgroundFade", base.settings.edgesOnly * base.settings.edgeOpacity);
			propertySheet.properties.SetFloat("_EdgeSize", (float)base.settings.edgeSize);
			propertySheet.properties.SetFloat("_Exponent", base.settings.edgeExp);
			propertySheet.properties.SetFloat("_Threshold", base.settings.lumThreshold);
			propertySheet.properties.SetColor("_EdgeColor", base.settings.edgeColor);
			propertySheet.properties.SetFloat("_EdgeOpacity", base.settings.edgeOpacity);
			propertySheet.properties.SetFloat("_EdgeDistanceMin", base.settings.edgeDistMin);
			propertySheet.properties.SetFloat("_EdgeDistance", base.settings.edgeDistance);
			propertySheet.properties.SetFloat("_FadeMult", base.settings.fadeMult);
			propertySheet.properties.SetVector("_FadeParams", new Vector4(base.settings.startFadeDistance.value, base.settings.endFadeDistance.value, (float)(base.settings.invertFadeDistance.value ? 1 : 0), (float)(base.settings.distanceFade.value ? 1 : 0)));
			propertySheet.properties.SetVector("_SobelParams", new Vector4((float)(base.settings.sobelThin ? 1 : 0), 0f, 0f, 0f));
			propertySheet.properties.SetFloat("_BackgroundIntensity", base.settings.backgroundIntensity);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, (int)base.settings.mode.value, false, null, false);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003C80 File Offset: 0x00001E80
		public override DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.DepthNormals;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003C83 File Offset: 0x00001E83
		public EdgeDetectionRenderer()
		{
		}

		// Token: 0x04000058 RID: 88
		private Shader shader;
	}
}
