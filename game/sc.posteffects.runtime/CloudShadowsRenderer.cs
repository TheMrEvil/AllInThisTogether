using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x0200000B RID: 11
	public sealed class CloudShadowsRenderer : PostProcessEffectRenderer<CloudShadows>
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002EC5 File Offset: 0x000010C5
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Cloud Shadows");
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002ED8 File Offset: 0x000010D8
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			CommandBuffer command = context.command;
			Camera camera = context.camera;
			CloudShadows.isOrtho = context.camera.orthographic;
			Texture value = (base.settings.texture.value == null) ? RuntimeUtilities.whiteTexture : base.settings.texture.value;
			propertySheet.properties.SetTexture("_NoiseTex", value);
			Matrix4x4 gpuprojectionMatrix = GL.GetGPUProjectionMatrix(camera.projectionMatrix, false);
			gpuprojectionMatrix[2, 3] = (gpuprojectionMatrix[3, 2] = 0f);
			gpuprojectionMatrix[3, 3] = 1f;
			Matrix4x4 value2 = Matrix4x4.Inverse(gpuprojectionMatrix * camera.worldToCameraMatrix) * Matrix4x4.TRS(new Vector3(0f, 0f, -gpuprojectionMatrix[2, 2]), Quaternion.identity, Vector3.one);
			propertySheet.properties.SetMatrix("clipToWorld", value2);
			float num = base.settings.speed * 0.1f;
			propertySheet.properties.SetVector("_CloudParams", new Vector4(base.settings.size * 0.01f, base.settings.direction.value.x * num, base.settings.direction.value.y * num, base.settings.density));
			propertySheet.properties.SetFloat("_ProjectionEnabled", (float)(base.settings.projectFromSun.value ? 1 : 0));
			command.SetGlobalVector("_FadeParams", new Vector4(base.settings.startFadeDistance.value, base.settings.endFadeDistance.value, 0f, 0f));
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0, false, null, false);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000030E6 File Offset: 0x000012E6
		public override DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.Depth;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000030E9 File Offset: 0x000012E9
		public CloudShadowsRenderer()
		{
		}

		// Token: 0x04000031 RID: 49
		private Shader shader;
	}
}
