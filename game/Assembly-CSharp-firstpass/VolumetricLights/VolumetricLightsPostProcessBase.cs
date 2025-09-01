using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace VolumetricLights
{
	// Token: 0x02000025 RID: 37
	[ExecuteAlways]
	[ImageEffectAllowedInSceneView]
	[HelpURL("https://kronnect.com/guides/volumetric-lights-2-builtin-new-feature-volumetric-lights-post-process/")]
	public abstract class VolumetricLightsPostProcessBase : MonoBehaviour
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x0000812A File Offset: 0x0000632A
		private static int GetScaledSize(int size, float factor)
		{
			size = (int)((float)size / factor);
			size /= 2;
			if (size < 1)
			{
				size = 1;
			}
			return size * 2;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00008142 File Offset: 0x00006342
		private void OnEnable()
		{
			if (this.vlRenderPass == null)
			{
				this.vlRenderPass = new VolumetricLightsPostProcessBase.VolumetricLightsRenderPass();
			}
			this.blurShader = Shader.Find("Hidden/VolumetricLights/Blur");
			this.copyShader = Shader.Find("Hidden/VolumetricLights/CopyExact");
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00008177 File Offset: 0x00006377
		private void OnDisable()
		{
			VolumetricLightsPostProcessBase.installed = false;
			if (this.vlRenderPass != null)
			{
				this.vlRenderPass.Cleanup();
			}
			Shader.SetGlobalFloat(VolumetricLightsPostProcessBase.ShaderParams.Downscaling, 0f);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000081A4 File Offset: 0x000063A4
		private void OnValidate()
		{
			this.brightness = Mathf.Max(0f, this.brightness);
			this.ditherStrength = Mathf.Clamp(this.ditherStrength, 0f, 0.2f);
			this.blurEdgeDepthThreshold = Mathf.Max(0.0001f, this.blurEdgeDepthThreshold);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000081F8 File Offset: 0x000063F8
		public bool isActive
		{
			get
			{
				return (this.downscaling > 1f || this.blurPasses > 0) && VolumetricLight.volumetricLights.Count > 0;
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000821F File Offset: 0x0000641F
		private void OnPreRender()
		{
			Shader.SetGlobalInt(VolumetricLightsPostProcessBase.ShaderParams.ForcedInvisible, this.isActive ? 1 : 0);
			Shader.SetGlobalFloat(VolumetricLightsPostProcessBase.ShaderParams.Downscaling, this.downscaling - 1f);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000824D File Offset: 0x0000644D
		public void AddRenderPasses(RenderTexture source, RenderTexture destination)
		{
			this.vlRenderPass.Setup(this, this.blurShader, this.copyShader);
			this.vlRenderPass.Execute(source, destination);
			VolumetricLightsPostProcessBase.installed = true;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000827C File Offset: 0x0000647C
		protected VolumetricLightsPostProcessBase()
		{
		}

		// Token: 0x04000154 RID: 340
		[SerializeField]
		[HideInInspector]
		private Shader blurShader;

		// Token: 0x04000155 RID: 341
		[SerializeField]
		[HideInInspector]
		private Shader copyShader;

		// Token: 0x04000156 RID: 342
		private VolumetricLightsPostProcessBase.VolumetricLightsRenderPass vlRenderPass;

		// Token: 0x04000157 RID: 343
		public static bool installed;

		// Token: 0x04000158 RID: 344
		public BlendMode blendMode;

		// Token: 0x04000159 RID: 345
		[Range(1f, 4f)]
		public float downscaling = 1f;

		// Token: 0x0400015A RID: 346
		[Range(0f, 4f)]
		public int blurPasses = 1;

		// Token: 0x0400015B RID: 347
		[Range(1f, 4f)]
		public float blurDownscaling = 1f;

		// Token: 0x0400015C RID: 348
		[Range(0.1f, 4f)]
		public float blurSpread = 1f;

		// Token: 0x0400015D RID: 349
		[Tooltip("Uses 32 bit floating point pixel format for rendering & blur fog volumes.")]
		public bool blurHDR = true;

		// Token: 0x0400015E RID: 350
		[Tooltip("Enable to use an edge-aware blur.")]
		public bool blurEdgePreserve;

		// Token: 0x0400015F RID: 351
		[Tooltip("Bilateral filter edge detection threshold.")]
		public float blurEdgeDepthThreshold = 0.001f;

		// Token: 0x04000160 RID: 352
		public float brightness = 1f;

		// Token: 0x04000161 RID: 353
		[Range(0f, 0.2f)]
		public float ditherStrength;

		// Token: 0x02000193 RID: 403
		private static class ShaderParams
		{
			// Token: 0x06000EB3 RID: 3763 RVA: 0x0005F8D4 File Offset: 0x0005DAD4
			// Note: this type is marked as 'beforefieldinit'.
			static ShaderParams()
			{
			}

			// Token: 0x04000C9A RID: 3226
			public static int LightBuffer = Shader.PropertyToID("_LightBuffer");

			// Token: 0x04000C9B RID: 3227
			public static int MainTex = Shader.PropertyToID("_MainTex");

			// Token: 0x04000C9C RID: 3228
			public static int BlurRT = Shader.PropertyToID("_BlurTex");

			// Token: 0x04000C9D RID: 3229
			public static int BlurRT2 = Shader.PropertyToID("_BlurTex2");

			// Token: 0x04000C9E RID: 3230
			public static int BlendDest = Shader.PropertyToID("_BlendDest");

			// Token: 0x04000C9F RID: 3231
			public static int BlendSrc = Shader.PropertyToID("_BlendSrc");

			// Token: 0x04000CA0 RID: 3232
			public static int BlendOp = Shader.PropertyToID("_BlendOp");

			// Token: 0x04000CA1 RID: 3233
			public static int MiscData = Shader.PropertyToID("_MiscData");

			// Token: 0x04000CA2 RID: 3234
			public static int ForcedInvisible = Shader.PropertyToID("_ForcedInvisible");

			// Token: 0x04000CA3 RID: 3235
			public static int DownsampledDepth = Shader.PropertyToID("_DownsampledDepth");

			// Token: 0x04000CA4 RID: 3236
			public static int BlueNoiseTexture = Shader.PropertyToID("_BlueNoise");

			// Token: 0x04000CA5 RID: 3237
			public static int BlurScale = Shader.PropertyToID("_BlurScale");

			// Token: 0x04000CA6 RID: 3238
			public static int Downscaling = Shader.PropertyToID("_Downscaling");

			// Token: 0x04000CA7 RID: 3239
			public const string SKW_DITHER = "DITHER";

			// Token: 0x04000CA8 RID: 3240
			public const string SKW_EDGE_PRESERVE = "EDGE_PRESERVE";

			// Token: 0x04000CA9 RID: 3241
			public const string SKW_EDGE_PRESERVE_UPSCALING = "EDGE_PRESERVE_UPSCALING";
		}

		// Token: 0x02000194 RID: 404
		private class VolumetricLightsRenderPass
		{
			// Token: 0x06000EB4 RID: 3764 RVA: 0x0005F9A4 File Offset: 0x0005DBA4
			public void Cleanup()
			{
				UnityEngine.Object.DestroyImmediate(this.blurMat);
				Shader.SetGlobalInt(VolumetricLightsPostProcessBase.ShaderParams.ForcedInvisible, 0);
			}

			// Token: 0x06000EB5 RID: 3765 RVA: 0x0005F9BC File Offset: 0x0005DBBC
			public void Setup(VolumetricLightsPostProcessBase settings, Shader blurShader, Shader copyShader)
			{
				this.settings = settings;
				if (this.cmd == null)
				{
					this.cmd = new CommandBuffer();
					this.cmd.name = "Volumetric Lights Post Process Rendering";
				}
				if (this.blurMat == null)
				{
					this.blurMat = new Material(blurShader);
					Texture2D value = Resources.Load<Texture2D>("Textures/blueNoiseVL128");
					this.blurMat.SetTexture(VolumetricLightsPostProcessBase.ShaderParams.BlueNoiseTexture, value);
				}
				if (this.copyMat == null)
				{
					this.copyMat = new Material(copyShader);
				}
				switch (settings.blendMode)
				{
				case BlendMode.Additive:
					this.blurMat.SetInt(VolumetricLightsPostProcessBase.ShaderParams.BlendOp, 0);
					this.blurMat.SetInt(VolumetricLightsPostProcessBase.ShaderParams.BlendSrc, 1);
					this.blurMat.SetInt(VolumetricLightsPostProcessBase.ShaderParams.BlendDest, 1);
					break;
				case BlendMode.Blend:
					this.blurMat.SetInt(VolumetricLightsPostProcessBase.ShaderParams.BlendOp, 0);
					this.blurMat.SetInt(VolumetricLightsPostProcessBase.ShaderParams.BlendSrc, 1);
					this.blurMat.SetInt(VolumetricLightsPostProcessBase.ShaderParams.BlendDest, 10);
					break;
				case BlendMode.PreMultiply:
					this.blurMat.SetInt(VolumetricLightsPostProcessBase.ShaderParams.BlendOp, 0);
					this.blurMat.SetInt(VolumetricLightsPostProcessBase.ShaderParams.BlendSrc, 5);
					this.blurMat.SetInt(VolumetricLightsPostProcessBase.ShaderParams.BlendDest, 1);
					break;
				case BlendMode.Substractive:
					this.blurMat.SetInt(VolumetricLightsPostProcessBase.ShaderParams.BlendOp, 2);
					this.blurMat.SetInt(VolumetricLightsPostProcessBase.ShaderParams.BlendSrc, 1);
					this.blurMat.SetInt(VolumetricLightsPostProcessBase.ShaderParams.BlendDest, 1);
					break;
				}
				this.blurMat.SetVector(VolumetricLightsPostProcessBase.ShaderParams.MiscData, new Vector4(settings.ditherStrength * 0.1f, settings.brightness, settings.blurEdgeDepthThreshold, 0f));
				if (settings.ditherStrength > 0f)
				{
					this.blurMat.EnableKeyword("DITHER");
				}
				else
				{
					this.blurMat.DisableKeyword("DITHER");
				}
				this.blurMat.DisableKeyword("EDGE_PRESERVE");
				this.blurMat.DisableKeyword("EDGE_PRESERVE_UPSCALING");
				if (settings.blurPasses > 0 && settings.blurEdgePreserve)
				{
					this.blurMat.EnableKeyword((settings.downscaling > 1f) ? "EDGE_PRESERVE_UPSCALING" : "EDGE_PRESERVE");
				}
				if (this.cmd == null)
				{
					this.cmd = new CommandBuffer();
					this.cmd.name = "m_ProfilerTag";
				}
			}

			// Token: 0x06000EB6 RID: 3766 RVA: 0x0005FC14 File Offset: 0x0005DE14
			public void Execute(RenderTexture source, RenderTexture destination)
			{
				if (!this.settings.isActive)
				{
					Graphics.Blit(source, destination, this.copyMat, 0);
					return;
				}
				this.cmd.Clear();
				this.cmd.SetGlobalInt(VolumetricLightsPostProcessBase.ShaderParams.ForcedInvisible, 0);
				RenderTextureDescriptor descriptor = source.descriptor;
				descriptor.width = VolumetricLightsPostProcessBase.GetScaledSize(descriptor.width, this.settings.downscaling);
				descriptor.height = VolumetricLightsPostProcessBase.GetScaledSize(descriptor.height, this.settings.downscaling);
				descriptor.depthBufferBits = 0;
				descriptor.useMipMap = false;
				descriptor.msaaSamples = 1;
				RenderTextureDescriptor renderTextureDescriptor = descriptor;
				this.cmd.GetTemporaryRT(VolumetricLightsPostProcessBase.ShaderParams.LightBuffer, descriptor, FilterMode.Bilinear);
				RenderTargetIdentifier renderTarget = new RenderTargetIdentifier(VolumetricLightsPostProcessBase.ShaderParams.LightBuffer, 0, CubemapFace.Unknown, -1);
				this.cmd.SetRenderTarget(renderTarget);
				this.cmd.ClearRenderTarget(false, true, new Color(0f, 0f, 0f, 0f));
				foreach (VolumetricLight volumetricLight in VolumetricLight.volumetricLights)
				{
					if (volumetricLight != null)
					{
						this.cmd.DrawRenderer(volumetricLight.meshRenderer, volumetricLight.meshRenderer.sharedMaterial);
					}
				}
				this.cmd.SetGlobalInt(VolumetricLightsPostProcessBase.ShaderParams.ForcedInvisible, 1);
				renderTextureDescriptor.colorFormat = (this.settings.blurHDR ? RenderTextureFormat.ARGBHalf : RenderTextureFormat.ARGB32);
				bool flag = this.settings.downscaling > 1f;
				if (flag)
				{
					RenderTextureDescriptor desc = renderTextureDescriptor;
					desc.colorFormat = RenderTextureFormat.RHalf;
					this.cmd.GetTemporaryRT(VolumetricLightsPostProcessBase.ShaderParams.DownsampledDepth, desc, FilterMode.Bilinear);
					this.FullScreenBlit(this.cmd, source, VolumetricLightsPostProcessBase.ShaderParams.DownsampledDepth, this.blurMat, 4);
				}
				if (this.settings.blurPasses < 1)
				{
					this.cmd.SetGlobalFloat(VolumetricLightsPostProcessBase.ShaderParams.BlurScale, this.settings.blurSpread);
					this.FullScreenBlit(this.cmd, VolumetricLightsPostProcessBase.ShaderParams.LightBuffer, source, this.blurMat, 3);
				}
				else
				{
					renderTextureDescriptor.width = VolumetricLightsPostProcessBase.GetScaledSize(renderTextureDescriptor.width, this.settings.blurDownscaling);
					renderTextureDescriptor.height = VolumetricLightsPostProcessBase.GetScaledSize(renderTextureDescriptor.height, this.settings.blurDownscaling);
					this.cmd.GetTemporaryRT(VolumetricLightsPostProcessBase.ShaderParams.BlurRT, renderTextureDescriptor, FilterMode.Bilinear);
					this.cmd.GetTemporaryRT(VolumetricLightsPostProcessBase.ShaderParams.BlurRT2, renderTextureDescriptor, FilterMode.Bilinear);
					this.cmd.SetGlobalFloat(VolumetricLightsPostProcessBase.ShaderParams.BlurScale, this.settings.blurSpread * this.settings.blurDownscaling);
					this.FullScreenBlit(this.cmd, VolumetricLightsPostProcessBase.ShaderParams.LightBuffer, VolumetricLightsPostProcessBase.ShaderParams.BlurRT, this.blurMat, 0);
					this.cmd.SetGlobalFloat(VolumetricLightsPostProcessBase.ShaderParams.BlurScale, this.settings.blurSpread);
					for (int i = 0; i < this.settings.blurPasses - 1; i++)
					{
						this.FullScreenBlit(this.cmd, VolumetricLightsPostProcessBase.ShaderParams.BlurRT, VolumetricLightsPostProcessBase.ShaderParams.BlurRT2, this.blurMat, 1);
						this.FullScreenBlit(this.cmd, VolumetricLightsPostProcessBase.ShaderParams.BlurRT2, VolumetricLightsPostProcessBase.ShaderParams.BlurRT, this.blurMat, 0);
					}
					if (flag)
					{
						this.FullScreenBlit(this.cmd, VolumetricLightsPostProcessBase.ShaderParams.BlurRT, VolumetricLightsPostProcessBase.ShaderParams.BlurRT2, this.blurMat, 5);
						this.FullScreenBlit(this.cmd, VolumetricLightsPostProcessBase.ShaderParams.BlurRT2, source, this.blurMat, 3);
					}
					else
					{
						this.FullScreenBlit(this.cmd, VolumetricLightsPostProcessBase.ShaderParams.BlurRT, source, this.blurMat, 2);
					}
					this.cmd.ReleaseTemporaryRT(VolumetricLightsPostProcessBase.ShaderParams.BlurRT2);
					this.cmd.ReleaseTemporaryRT(VolumetricLightsPostProcessBase.ShaderParams.BlurRT);
				}
				this.cmd.ReleaseTemporaryRT(VolumetricLightsPostProcessBase.ShaderParams.LightBuffer);
				if (flag)
				{
					this.cmd.ReleaseTemporaryRT(VolumetricLightsPostProcessBase.ShaderParams.DownsampledDepth);
				}
				Graphics.ExecuteCommandBuffer(this.cmd);
				Graphics.Blit(source, destination, this.copyMat, 0);
			}

			// Token: 0x170001BE RID: 446
			// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x00060044 File Offset: 0x0005E244
			private Mesh fullscreenMesh
			{
				get
				{
					if (VolumetricLightsPostProcessBase.VolumetricLightsRenderPass._fullScreenMesh != null)
					{
						return VolumetricLightsPostProcessBase.VolumetricLightsRenderPass._fullScreenMesh;
					}
					float y = 1f;
					float y2 = 0f;
					VolumetricLightsPostProcessBase.VolumetricLightsRenderPass._fullScreenMesh = new Mesh();
					VolumetricLightsPostProcessBase.VolumetricLightsRenderPass._fullScreenMesh.SetVertices(new List<Vector3>
					{
						new Vector3(-1f, -1f, 0f),
						new Vector3(-1f, 1f, 0f),
						new Vector3(1f, -1f, 0f),
						new Vector3(1f, 1f, 0f)
					});
					VolumetricLightsPostProcessBase.VolumetricLightsRenderPass._fullScreenMesh.SetUVs(0, new List<Vector2>
					{
						new Vector2(0f, y2),
						new Vector2(0f, y),
						new Vector2(1f, y2),
						new Vector2(1f, y)
					});
					VolumetricLightsPostProcessBase.VolumetricLightsRenderPass._fullScreenMesh.SetIndices(new int[]
					{
						0,
						1,
						2,
						2,
						1,
						3
					}, MeshTopology.Triangles, 0, false);
					VolumetricLightsPostProcessBase.VolumetricLightsRenderPass._fullScreenMesh.UploadMeshData(true);
					return VolumetricLightsPostProcessBase.VolumetricLightsRenderPass._fullScreenMesh;
				}
			}

			// Token: 0x06000EB8 RID: 3768 RVA: 0x00060173 File Offset: 0x0005E373
			private void FullScreenBlit(CommandBuffer cmd, RenderTargetIdentifier source, RenderTargetIdentifier destination, Material material, int passIndex)
			{
				destination = new RenderTargetIdentifier(destination, 0, CubemapFace.Unknown, -1);
				cmd.SetRenderTarget(destination);
				cmd.SetGlobalTexture(VolumetricLightsPostProcessBase.ShaderParams.MainTex, source);
				cmd.DrawMesh(this.fullscreenMesh, Matrix4x4.identity, material, 0, passIndex);
			}

			// Token: 0x06000EB9 RID: 3769 RVA: 0x000601A9 File Offset: 0x0005E3A9
			public VolumetricLightsRenderPass()
			{
			}

			// Token: 0x04000CAA RID: 3242
			private const string m_ProfilerTag = "Volumetric Lights Post Process Rendering";

			// Token: 0x04000CAB RID: 3243
			private const string m_LightBufferName = "_LightBuffer";

			// Token: 0x04000CAC RID: 3244
			private VolumetricLightsPostProcessBase settings;

			// Token: 0x04000CAD RID: 3245
			private RenderTargetIdentifier m_LightBuffer;

			// Token: 0x04000CAE RID: 3246
			private CommandBuffer cmd;

			// Token: 0x04000CAF RID: 3247
			private Material blurMat;

			// Token: 0x04000CB0 RID: 3248
			private Material copyMat;

			// Token: 0x04000CB1 RID: 3249
			private static Mesh _fullScreenMesh;

			// Token: 0x02000246 RID: 582
			private enum Pass
			{
				// Token: 0x040010F0 RID: 4336
				BlurHorizontal,
				// Token: 0x040010F1 RID: 4337
				BlurVertical,
				// Token: 0x040010F2 RID: 4338
				BlurVerticalAndBlend,
				// Token: 0x040010F3 RID: 4339
				Blend,
				// Token: 0x040010F4 RID: 4340
				DownscaleDepth,
				// Token: 0x040010F5 RID: 4341
				BlurVerticalFinal
			}
		}
	}
}
