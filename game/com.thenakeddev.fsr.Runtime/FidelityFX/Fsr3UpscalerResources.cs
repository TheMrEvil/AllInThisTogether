using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace FidelityFX
{
	// Token: 0x02000015 RID: 21
	internal class Fsr3UpscalerResources
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00004F7C File Offset: 0x0000317C
		public void Create(Fsr3.ContextDescription contextDescription)
		{
			float[] array = new float[128];
			for (int i = 0; i < 128; i++)
			{
				float num = Fsr3.Lanczos2(2f * (float)i / 127f);
				array[i] = num;
			}
			Vector2Int maxRenderSize = contextDescription.MaxRenderSize;
			Vector2Int vector2Int = maxRenderSize / 2;
			this.LanczosLut = new Texture2D(128, 1, GraphicsFormat.R32_SFloat, TextureCreationFlags.None)
			{
				name = "FSR3UPSCALER_LanczosLutData"
			};
			this.LanczosLut.SetPixelData<float>(array, 0, 0);
			this.LanczosLut.Apply();
			this.DefaultReactive = new Texture2D(1, 1, GraphicsFormat.R8_UNorm, TextureCreationFlags.None)
			{
				name = "FSR3UPSCALER_DefaultReactivityMask"
			};
			this.DefaultReactive.SetPixel(0, 0, Color.clear);
			this.DefaultReactive.Apply();
			this.DefaultExposure = new Texture2D(1, 1, GraphicsFormat.R32G32_SFloat, TextureCreationFlags.None)
			{
				name = "FSR3UPSCALER_DefaultExposure"
			};
			this.DefaultExposure.SetPixel(0, 0, Color.clear);
			this.DefaultExposure.Apply();
			this.SpdAtomicCounter = new RenderTexture(1, 1, 0, GraphicsFormat.R32_UInt)
			{
				name = "FSR3UPSCALER_SpdAtomicCounter",
				enableRandomWrite = true
			};
			this.SpdAtomicCounter.Create();
			int mipCount = 1 + Mathf.FloorToInt(Mathf.Log((float)Math.Max(vector2Int.x, vector2Int.y), 2f));
			this.SpdMips = new RenderTexture(vector2Int.x, vector2Int.y, 0, GraphicsFormat.R16G16_SFloat, mipCount)
			{
				name = "FSR3UPSCALER_SpdMips",
				enableRandomWrite = true,
				useMipMap = true,
				autoGenerateMips = false
			};
			this.SpdMips.Create();
			this.DilatedVelocity = new RenderTexture(maxRenderSize.x, maxRenderSize.y, 0, GraphicsFormat.R16G16_SFloat)
			{
				name = "FSR3UPSCALER_DilatedVelocity",
				enableRandomWrite = true
			};
			this.DilatedVelocity.Create();
			this.DilatedDepth = new RenderTexture(maxRenderSize.x, maxRenderSize.y, 0, GraphicsFormat.R32_SFloat)
			{
				name = "FSR3UPSCALER_DilatedDepth",
				enableRandomWrite = true
			};
			this.DilatedDepth.Create();
			this.ReconstructedPrevNearestDepth = new RenderTexture(maxRenderSize.x, maxRenderSize.y, 0, GraphicsFormat.R32_UInt)
			{
				name = "FSR3UPSCALER_ReconstructedPrevNearestDepth",
				enableRandomWrite = true
			};
			this.ReconstructedPrevNearestDepth.Create();
			this.FrameInfo = new RenderTexture(1, 1, 0, GraphicsFormat.R32G32B32A32_SFloat)
			{
				name = "FSR3UPSCALER_FrameInfo",
				enableRandomWrite = true
			};
			this.FrameInfo.Create();
			Fsr3UpscalerResources.CreateDoubleBufferedResource(this.Accumulation, "FSR3UPSCALER_Accumulation", maxRenderSize, GraphicsFormat.R8_UNorm);
			Fsr3UpscalerResources.CreateDoubleBufferedResource(this.Luma, "FSR3UPSCALER_Luma", maxRenderSize, GraphicsFormat.R16_SFloat);
			Fsr3UpscalerResources.CreateDoubleBufferedResource(this.InternalUpscaled, "FSR3UPSCALER_InternalUpscaled", contextDescription.MaxUpscaleSize, GraphicsFormat.R16G16B16A16_SFloat);
			Fsr3UpscalerResources.CreateDoubleBufferedResource(this.LumaHistory, "FSR3UPSCALER_LumaHistory", maxRenderSize, GraphicsFormat.R16G16B16A16_SFloat);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00005244 File Offset: 0x00003444
		public void CreateTcrAutogenResources(Fsr3.ContextDescription contextDescription)
		{
			this.AutoReactive = new RenderTexture(contextDescription.MaxRenderSize.x, contextDescription.MaxRenderSize.y, 0, GraphicsFormat.R8_UNorm)
			{
				name = "FSR3UPSCALER_AutoReactive",
				enableRandomWrite = true
			};
			this.AutoReactive.Create();
			this.AutoComposition = new RenderTexture(contextDescription.MaxRenderSize.x, contextDescription.MaxRenderSize.y, 0, GraphicsFormat.R8_UNorm)
			{
				name = "FSR3UPSCALER_AutoComposition",
				enableRandomWrite = true
			};
			this.AutoComposition.Create();
			Fsr3UpscalerResources.CreateDoubleBufferedResource(this.PrevPreAlpha, "FSR3UPSCALER_PrevPreAlpha", contextDescription.MaxRenderSize, GraphicsFormat.B10G11R11_UFloatPack32);
			Fsr3UpscalerResources.CreateDoubleBufferedResource(this.PrevPostAlpha, "FSR3UPSCALER_PrevPostAlpha", contextDescription.MaxRenderSize, GraphicsFormat.B10G11R11_UFloatPack32);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00005308 File Offset: 0x00003508
		public static void CreateAliasableResources(CommandBuffer commandBuffer, Fsr3.ContextDescription contextDescription, Fsr3.DispatchDescription dispatchParams)
		{
			Vector2Int maxUpscaleSize = contextDescription.MaxUpscaleSize;
			Vector2Int maxRenderSize = contextDescription.MaxRenderSize;
			Vector2Int vector2Int = maxRenderSize / 2;
			commandBuffer.GetTemporaryRT(Fsr3ShaderIDs.UavIntermediate, maxRenderSize.x, maxRenderSize.y, 0, FilterMode.Point, GraphicsFormat.R16_SFloat, 1, true);
			commandBuffer.GetTemporaryRT(Fsr3ShaderIDs.UavShadingChange, vector2Int.x, vector2Int.y, 0, FilterMode.Point, GraphicsFormat.R8_UNorm, 1, true);
			commandBuffer.GetTemporaryRT(Fsr3ShaderIDs.UavNewLocks, maxUpscaleSize.x, maxUpscaleSize.y, 0, FilterMode.Point, GraphicsFormat.R8_UNorm, 1, true);
			commandBuffer.GetTemporaryRT(Fsr3ShaderIDs.UavFarthestDepthMip1, vector2Int.x, vector2Int.y, 0, FilterMode.Point, GraphicsFormat.R16_SFloat, 1, true);
			commandBuffer.GetTemporaryRT(Fsr3ShaderIDs.UavDilatedReactiveMasks, maxRenderSize.x, maxRenderSize.y, 0, FilterMode.Point, GraphicsFormat.R8G8B8A8_UNorm, 1, true);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000053C3 File Offset: 0x000035C3
		public static void DestroyAliasableResources(CommandBuffer commandBuffer)
		{
			commandBuffer.ReleaseTemporaryRT(Fsr3ShaderIDs.UavDilatedReactiveMasks);
			commandBuffer.ReleaseTemporaryRT(Fsr3ShaderIDs.UavFarthestDepthMip1);
			commandBuffer.ReleaseTemporaryRT(Fsr3ShaderIDs.UavNewLocks);
			commandBuffer.ReleaseTemporaryRT(Fsr3ShaderIDs.UavShadingChange);
			commandBuffer.ReleaseTemporaryRT(Fsr3ShaderIDs.UavIntermediate);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000053FC File Offset: 0x000035FC
		private static void CreateDoubleBufferedResource(RenderTexture[] resource, string name, Vector2Int size, GraphicsFormat format)
		{
			for (int i = 0; i < 2; i++)
			{
				resource[i] = new RenderTexture(size.x, size.y, 0, format)
				{
					name = name + (i + 1).ToString(),
					enableRandomWrite = true
				};
				resource[i].Create();
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00005454 File Offset: 0x00003654
		public void Destroy()
		{
			this.DestroyTcrAutogenResources();
			Fsr3UpscalerResources.DestroyResource(this.LumaHistory);
			Fsr3UpscalerResources.DestroyResource(this.InternalUpscaled);
			Fsr3UpscalerResources.DestroyResource(this.Luma);
			Fsr3UpscalerResources.DestroyResource(this.Accumulation);
			Fsr3UpscalerResources.DestroyResource(ref this.FrameInfo);
			Fsr3UpscalerResources.DestroyResource(ref this.ReconstructedPrevNearestDepth);
			Fsr3UpscalerResources.DestroyResource(ref this.DilatedDepth);
			Fsr3UpscalerResources.DestroyResource(ref this.DilatedVelocity);
			Fsr3UpscalerResources.DestroyResource(ref this.SpdMips);
			Fsr3UpscalerResources.DestroyResource(ref this.SpdAtomicCounter);
			Fsr3UpscalerResources.DestroyResource(ref this.DefaultReactive);
			Fsr3UpscalerResources.DestroyResource(ref this.DefaultExposure);
			Fsr3UpscalerResources.DestroyResource(ref this.LanczosLut);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000054F6 File Offset: 0x000036F6
		public void DestroyTcrAutogenResources()
		{
			Fsr3UpscalerResources.DestroyResource(this.PrevPostAlpha);
			Fsr3UpscalerResources.DestroyResource(this.PrevPreAlpha);
			Fsr3UpscalerResources.DestroyResource(ref this.AutoComposition);
			Fsr3UpscalerResources.DestroyResource(ref this.AutoReactive);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00005524 File Offset: 0x00003724
		private static void DestroyResource(ref Texture2D resource)
		{
			if (resource == null)
			{
				return;
			}
			UnityEngine.Object.Destroy(resource);
			resource = null;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000553B File Offset: 0x0000373B
		private static void DestroyResource(ref RenderTexture resource)
		{
			if (resource == null)
			{
				return;
			}
			resource.Release();
			resource = null;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00005554 File Offset: 0x00003754
		private static void DestroyResource(RenderTexture[] resource)
		{
			for (int i = 0; i < resource.Length; i++)
			{
				Fsr3UpscalerResources.DestroyResource(ref resource[i]);
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000557C File Offset: 0x0000377C
		public Fsr3UpscalerResources()
		{
		}

		// Token: 0x04000078 RID: 120
		public Texture2D LanczosLut;

		// Token: 0x04000079 RID: 121
		public Texture2D DefaultExposure;

		// Token: 0x0400007A RID: 122
		public Texture2D DefaultReactive;

		// Token: 0x0400007B RID: 123
		public RenderTexture SpdAtomicCounter;

		// Token: 0x0400007C RID: 124
		public RenderTexture SpdMips;

		// Token: 0x0400007D RID: 125
		public RenderTexture DilatedVelocity;

		// Token: 0x0400007E RID: 126
		public RenderTexture DilatedDepth;

		// Token: 0x0400007F RID: 127
		public RenderTexture ReconstructedPrevNearestDepth;

		// Token: 0x04000080 RID: 128
		public RenderTexture FrameInfo;

		// Token: 0x04000081 RID: 129
		public RenderTexture AutoReactive;

		// Token: 0x04000082 RID: 130
		public RenderTexture AutoComposition;

		// Token: 0x04000083 RID: 131
		public readonly RenderTexture[] Accumulation = new RenderTexture[2];

		// Token: 0x04000084 RID: 132
		public readonly RenderTexture[] Luma = new RenderTexture[2];

		// Token: 0x04000085 RID: 133
		public readonly RenderTexture[] InternalUpscaled = new RenderTexture[2];

		// Token: 0x04000086 RID: 134
		public readonly RenderTexture[] LumaHistory = new RenderTexture[2];

		// Token: 0x04000087 RID: 135
		public readonly RenderTexture[] PrevPreAlpha = new RenderTexture[2];

		// Token: 0x04000088 RID: 136
		public readonly RenderTexture[] PrevPostAlpha = new RenderTexture[2];
	}
}
