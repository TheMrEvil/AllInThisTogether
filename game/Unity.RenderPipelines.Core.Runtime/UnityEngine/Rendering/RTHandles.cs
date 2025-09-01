using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x02000091 RID: 145
	public static class RTHandles
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x000152AA File Offset: 0x000134AA
		public static int maxWidth
		{
			get
			{
				return RTHandles.s_DefaultInstance.GetMaxWidth();
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x000152B6 File Offset: 0x000134B6
		public static int maxHeight
		{
			get
			{
				return RTHandles.s_DefaultInstance.GetMaxHeight();
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x000152C2 File Offset: 0x000134C2
		public static RTHandleProperties rtHandleProperties
		{
			get
			{
				return RTHandles.s_DefaultInstance.rtHandleProperties;
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x000152D0 File Offset: 0x000134D0
		public static RTHandle Alloc(int width, int height, int slices = 1, DepthBits depthBufferBits = DepthBits.None, GraphicsFormat colorFormat = GraphicsFormat.R8G8B8A8_SRGB, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, MSAASamples msaaSamples = MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, string name = "")
		{
			return RTHandles.s_DefaultInstance.Alloc(width, height, slices, depthBufferBits, colorFormat, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, memoryless, name);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0001530C File Offset: 0x0001350C
		public static RTHandle Alloc(int width, int height, TextureWrapMode wrapModeU, TextureWrapMode wrapModeV, TextureWrapMode wrapModeW = TextureWrapMode.Repeat, int slices = 1, DepthBits depthBufferBits = DepthBits.None, GraphicsFormat colorFormat = GraphicsFormat.R8G8B8A8_SRGB, FilterMode filterMode = FilterMode.Point, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, MSAASamples msaaSamples = MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, string name = "")
		{
			return RTHandles.s_DefaultInstance.Alloc(width, height, wrapModeU, wrapModeV, wrapModeW, slices, depthBufferBits, colorFormat, filterMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, memoryless, name);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0001534C File Offset: 0x0001354C
		public static RTHandle Alloc(Vector2 scaleFactor, int slices = 1, DepthBits depthBufferBits = DepthBits.None, GraphicsFormat colorFormat = GraphicsFormat.R8G8B8A8_SRGB, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, MSAASamples msaaSamples = MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, string name = "")
		{
			return RTHandles.s_DefaultInstance.Alloc(scaleFactor, slices, depthBufferBits, colorFormat, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, memoryless, name);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00015384 File Offset: 0x00013584
		public static RTHandle Alloc(ScaleFunc scaleFunc, int slices = 1, DepthBits depthBufferBits = DepthBits.None, GraphicsFormat colorFormat = GraphicsFormat.R8G8B8A8_SRGB, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, MSAASamples msaaSamples = MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, string name = "")
		{
			return RTHandles.s_DefaultInstance.Alloc(scaleFunc, slices, depthBufferBits, colorFormat, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, memoryless, name);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x000153BB File Offset: 0x000135BB
		public static RTHandle Alloc(Texture tex)
		{
			return RTHandles.s_DefaultInstance.Alloc(tex);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x000153C8 File Offset: 0x000135C8
		public static RTHandle Alloc(RenderTexture tex)
		{
			return RTHandles.s_DefaultInstance.Alloc(tex);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x000153D5 File Offset: 0x000135D5
		public static RTHandle Alloc(RenderTargetIdentifier tex)
		{
			return RTHandles.s_DefaultInstance.Alloc(tex);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x000153E2 File Offset: 0x000135E2
		public static RTHandle Alloc(RenderTargetIdentifier tex, string name)
		{
			return RTHandles.s_DefaultInstance.Alloc(tex, name);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x000153F0 File Offset: 0x000135F0
		private static RTHandle Alloc(RTHandle tex)
		{
			Debug.LogError("Allocation a RTHandle from another one is forbidden.");
			return null;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x000153FD File Offset: 0x000135FD
		public static void Initialize(int width, int height)
		{
			RTHandles.s_DefaultInstance.Initialize(width, height);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0001540B File Offset: 0x0001360B
		public static void Release(RTHandle rth)
		{
			RTHandles.s_DefaultInstance.Release(rth);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00015418 File Offset: 0x00013618
		public static void SetHardwareDynamicResolutionState(bool hwDynamicResRequested)
		{
			RTHandles.s_DefaultInstance.SetHardwareDynamicResolutionState(hwDynamicResRequested);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00015425 File Offset: 0x00013625
		public static void SetReferenceSize(int width, int height)
		{
			RTHandles.s_DefaultInstance.SetReferenceSize(width, height);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00015433 File Offset: 0x00013633
		public static void ResetReferenceSize(int width, int height)
		{
			RTHandles.s_DefaultInstance.ResetReferenceSize(width, height);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00015444 File Offset: 0x00013644
		public static Vector2 CalculateRatioAgainstMaxSize(int width, int height)
		{
			RTHandleSystem rthandleSystem = RTHandles.s_DefaultInstance;
			Vector2Int vector2Int = new Vector2Int(width, height);
			return rthandleSystem.CalculateRatioAgainstMaxSize(vector2Int);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00015465 File Offset: 0x00013665
		// Note: this type is marked as 'beforefieldinit'.
		static RTHandles()
		{
		}

		// Token: 0x04000303 RID: 771
		private static RTHandleSystem s_DefaultInstance = new RTHandleSystem();
	}
}
