using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x0200001A RID: 26
	public sealed class FogRenderer : PostProcessEffectRenderer<Fog>
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00004098 File Offset: 0x00002298
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Fog");
			this.m_Pyramid = new FogRenderer.MipLevel[16];
			for (int i = 0; i < 16; i++)
			{
				this.m_Pyramid[i] = new FogRenderer.MipLevel
				{
					down = Shader.PropertyToID("_BloomMipDown" + i.ToString()),
					up = Shader.PropertyToID("_BloomMipUp" + i.ToString())
				};
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000411E File Offset: 0x0000231E
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00004128 File Offset: 0x00002328
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			CommandBuffer command = context.command;
			Camera camera = context.camera;
			if (base.settings.colorSource.value == Fog.FogColorSource.SkyboxColor)
			{
				if (camera.hideFlags != HideFlags.None && camera.name != "SceneCamera")
				{
					return;
				}
				if (!FogRenderer.skyboxCams.ContainsKey(camera))
				{
					FogRenderer.skyboxCams[camera] = camera.gameObject.GetComponent<RenderScreenSpaceSkybox>();
					if (!FogRenderer.skyboxCams[camera])
					{
						FogRenderer.skyboxCams[camera] = camera.gameObject.AddComponent<RenderScreenSpaceSkybox>();
					}
					FogRenderer.skyboxCams[camera].manuallyAdded = false;
				}
			}
			Matrix4x4 gpuprojectionMatrix = GL.GetGPUProjectionMatrix(camera.projectionMatrix, false);
			gpuprojectionMatrix[2, 3] = (gpuprojectionMatrix[3, 2] = 0f);
			gpuprojectionMatrix[3, 3] = 1f;
			Matrix4x4 value = Matrix4x4.Inverse(gpuprojectionMatrix * camera.worldToCameraMatrix) * Matrix4x4.TRS(new Vector3(0f, 0f, -gpuprojectionMatrix[2, 2]), Quaternion.identity, Vector3.one);
			propertySheet.properties.SetMatrix("clipToWorld", value);
			float num = camera.transform.position.y - base.settings.height;
			float z = (num <= 0f) ? 1f : 0f;
			float x = base.settings.lightScattering ? 1f : base.settings.skyboxInfluence;
			float z2 = base.settings.distanceFog ? 1f : 0f;
			float w = base.settings.heightFog ? 1f : 0f;
			int num2 = (int)(base.settings.useSceneSettings ? Fog.FogColorSource.UniformColor : base.settings.colorSource.value);
			FogMode fogMode = base.settings.useSceneSettings ? RenderSettings.fogMode : base.settings.fogMode;
			float num3 = base.settings.useSceneSettings ? RenderSettings.fogDensity : (base.settings.globalDensity / 100f);
			float num4 = base.settings.useSceneSettings ? RenderSettings.fogStartDistance : base.settings.fogStartDistance;
			float num5 = base.settings.useSceneSettings ? RenderSettings.fogEndDistance : base.settings.fogEndDistance;
			bool flag = fogMode == FogMode.Linear;
			float num6 = flag ? (num5 - num4) : 0f;
			float num7 = (Mathf.Abs(num6) > 0.0001f) ? (1f / num6) : 0f;
			Vector4 value2;
			value2.x = num3 * 1.2011224f;
			value2.y = num3 * 1.442695f;
			value2.z = (flag ? (-num7) : 0f);
			value2.w = (flag ? (num5 * num7) : 0f);
			float value3 = base.settings.gradientUseFarClipPlane.value ? base.settings.gradientDistance : context.camera.farClipPlane;
			if (base.settings.heightNoiseTex.value)
			{
				propertySheet.properties.SetTexture("_NoiseTex", base.settings.heightNoiseTex);
			}
			if (base.settings.fogColorGradient.value)
			{
				propertySheet.properties.SetTexture("_ColorGradient", base.settings.fogColorGradient);
			}
			command.SetGlobalFloat("_FarClippingPlane", value3);
			command.SetGlobalVector("_SceneFogParams", value2);
			command.SetGlobalVector("_SceneFogMode", new Vector4((float)fogMode, (float)(base.settings.useRadialDistance ? 1 : 0), (float)num2, (float)(base.settings.heightFogNoise ? 1 : 0)));
			command.SetGlobalVector("_NoiseParams", new Vector4(base.settings.heightNoiseSize * 0.01f, base.settings.heightNoiseSpeed * 0.01f, base.settings.heightNoiseStrength, 0f));
			command.SetGlobalVector("_DensityParams", new Vector4(base.settings.distanceDensity, base.settings.heightNoiseStrength, base.settings.skyboxMipLevel, 0f));
			command.SetGlobalVector("_HeightParams", new Vector4(base.settings.height, num, z, base.settings.heightDensity * 0.5f));
			command.SetGlobalVector("_DistanceParams", new Vector4(-num4, 0f, z2, w));
			command.SetGlobalColor("_FogColor", base.settings.useSceneSettings ? RenderSettings.fogColor : base.settings.fogColor);
			command.SetGlobalVector("_SkyboxParams", new Vector4(x, base.settings.skyboxMipLevel, 0f, 0f));
			Vector3 vector = base.settings.useLightDirection ? Fog.LightDirection : base.settings.lightDirection.value.normalized;
			float num8 = base.settings.useLightIntensity ? FogLightSource.intensity : base.settings.lightIntensity.value;
			num8 = (base.settings.enableDirectionalLight ? num8 : 0f);
			command.SetGlobalVector("_DirLightParams", new Vector4(vector.x, vector.y, vector.z, num8));
			Color color = base.settings.useLightColor ? FogLightSource.color : base.settings.lightColor.value;
			command.SetGlobalVector("_DirLightColor", new Vector4(color.r, color.g, color.b, 0f));
			bool flag2 = base.settings.lightScattering;
			if (flag2)
			{
				int num9 = Mathf.FloorToInt((float)context.screenWidth / 2f);
				int num10 = Mathf.FloorToInt((float)context.screenHeight / 2f);
				bool flag3 = context.stereoActive && context.stereoRenderingMode == PostProcessRenderContext.StereoRenderingMode.SinglePass && context.camera.stereoTargetEye == StereoTargetEyeMask.Both;
				int num11 = flag3 ? (num9 * 2) : num9;
				float num12 = Mathf.Log((float)Mathf.Max(num9, num10), 2f) + Mathf.Min(base.settings.scatterDiffusion.value, 10f) - 10f;
				int num13 = Mathf.FloorToInt(num12);
				int num14 = Mathf.Clamp(num13, 1, 16);
				float num15 = 0.5f + num12 - (float)num13;
				propertySheet.properties.SetFloat("_SampleScale", num15);
				float num16 = Mathf.GammaToLinearSpace(base.settings.scatterThreshold.value);
				float num17 = num16 * base.settings.scatterSoftKnee.value + 1E-05f;
				Vector4 value4 = new Vector4(num16, num16 - num17, num17 * 2f, 0.25f / num17);
				command.SetGlobalVector("_Threshold", value4);
				RenderTargetIdentifier source = context.source;
				for (int i = 0; i < num14; i++)
				{
					int down = this.m_Pyramid[i].down;
					int up = this.m_Pyramid[i].up;
					int pass = (i == 0) ? 0 : 1;
					context.GetScreenSpaceTemporaryRT(command, down, 0, context.sourceFormat, RenderTextureReadWrite.Default, FilterMode.Bilinear, num11, num10, false);
					context.GetScreenSpaceTemporaryRT(command, up, 0, context.sourceFormat, RenderTextureReadWrite.Default, FilterMode.Bilinear, num11, num10, false);
					command.BlitFullscreenTriangle(source, down, propertySheet, pass, false, null, false);
					source = down;
					num11 = ((flag3 && num11 / 2 % 2 > 0) ? (1 + num11 / 2) : (num11 / 2));
					num11 = Mathf.Max(num11, 1);
					num10 = Mathf.Max(num10 / 2, 1);
				}
				int num18 = this.m_Pyramid[num14 - 1].down;
				for (int j = num14 - 2; j >= 0; j--)
				{
					int down2 = this.m_Pyramid[j].down;
					int up2 = this.m_Pyramid[j].up;
					command.SetGlobalTexture("_BloomTex", down2);
					command.BlitFullscreenTriangle(num18, up2, propertySheet, 2, false, null, false);
					num18 = up2;
				}
				float y = RuntimeUtilities.Exp2(base.settings.scatterIntensity.value / 10f) - 1f;
				Vector4 value5 = new Vector4(num15, y, 0f, (float)num14);
				command.SetGlobalVector("_ScatteringParams", value5);
				command.SetGlobalTexture("_BloomTex", num18);
				for (int k = 0; k < num14; k++)
				{
					if (this.m_Pyramid[k].down != num18)
					{
						command.ReleaseTemporaryRT(this.m_Pyramid[k].down);
					}
					if (this.m_Pyramid[k].up != num18)
					{
						command.ReleaseTemporaryRT(this.m_Pyramid[k].up);
					}
				}
			}
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, flag2 ? 4 : 3, false, null, false);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00004B35 File Offset: 0x00002D35
		public override DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.Depth;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00004B38 File Offset: 0x00002D38
		public FogRenderer()
		{
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00004B40 File Offset: 0x00002D40
		// Note: this type is marked as 'beforefieldinit'.
		static FogRenderer()
		{
		}

		// Token: 0x04000081 RID: 129
		private Shader shader;

		// Token: 0x04000082 RID: 130
		private FogRenderer.MipLevel[] m_Pyramid;

		// Token: 0x04000083 RID: 131
		private const int k_MaxPyramidSize = 16;

		// Token: 0x04000084 RID: 132
		public static Dictionary<Camera, RenderScreenSpaceSkybox> skyboxCams = new Dictionary<Camera, RenderScreenSpaceSkybox>();

		// Token: 0x02000061 RID: 97
		private struct MipLevel
		{
			// Token: 0x0400018E RID: 398
			internal int down;

			// Token: 0x0400018F RID: 399
			internal int up;
		}

		// Token: 0x02000062 RID: 98
		private enum Pass
		{
			// Token: 0x04000191 RID: 401
			Prefilter,
			// Token: 0x04000192 RID: 402
			Downsample,
			// Token: 0x04000193 RID: 403
			Upsample,
			// Token: 0x04000194 RID: 404
			Blend,
			// Token: 0x04000195 RID: 405
			BlendScattering
		}
	}
}
