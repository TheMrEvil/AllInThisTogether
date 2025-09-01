using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000009 RID: 9
	public sealed class CausticsRenderer : PostProcessEffectRenderer<Caustics>
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002B75 File Offset: 0x00000D75
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Caustics");
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002B87 File Offset: 0x00000D87
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002B90 File Offset: 0x00000D90
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			CommandBuffer command = context.command;
			Matrix4x4 gpuprojectionMatrix = GL.GetGPUProjectionMatrix(context.camera.projectionMatrix, false);
			gpuprojectionMatrix[2, 3] = (gpuprojectionMatrix[3, 2] = 0f);
			gpuprojectionMatrix[3, 3] = 1f;
			Matrix4x4 value = Matrix4x4.Inverse(gpuprojectionMatrix * context.camera.worldToCameraMatrix) * Matrix4x4.TRS(new Vector3(0f, 0f, -gpuprojectionMatrix[2, 2]), Quaternion.identity, Vector3.one);
			propertySheet.properties.SetMatrix("clipToWorld", value);
			if (base.settings.causticsTexture.value)
			{
				propertySheet.properties.SetTexture("_CausticsTex", base.settings.causticsTexture.value);
			}
			propertySheet.properties.SetFloat("_LuminanceThreshold", Mathf.GammaToLinearSpace(base.settings.luminanceThreshold.value));
			propertySheet.properties.SetVector("_CausticsParams", new Vector4(base.settings.size, base.settings.speed, (float)(base.settings.projectFromSun.value ? 1 : 0), base.settings.intensity));
			propertySheet.properties.SetVector("_HeightParams", new Vector4(base.settings.minHeight.value, base.settings.minHeightFalloff.value, base.settings.maxHeight.value, base.settings.maxHeightFalloff.value));
			command.SetGlobalVector("_FadeParams", new Vector4(base.settings.startFadeDistance.value, base.settings.endFadeDistance.value, 0f, (float)(base.settings.distanceFade.value ? 1 : 0)));
			command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0, false, null, false);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002DBC File Offset: 0x00000FBC
		public override DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.Depth;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002DBF File Offset: 0x00000FBF
		public CausticsRenderer()
		{
		}

		// Token: 0x04000027 RID: 39
		private Shader shader;
	}
}
