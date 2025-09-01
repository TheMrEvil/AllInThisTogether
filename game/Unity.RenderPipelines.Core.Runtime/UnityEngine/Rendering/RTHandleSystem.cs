using System;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x02000094 RID: 148
	public class RTHandleSystem : IDisposable
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x00015471 File Offset: 0x00013671
		public RTHandleProperties rtHandleProperties
		{
			get
			{
				return this.m_RTHandleProperties;
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00015479 File Offset: 0x00013679
		public RTHandleSystem()
		{
			this.m_AutoSizedRTs = new HashSet<RTHandle>();
			this.m_ResizeOnDemandRTs = new HashSet<RTHandle>();
			this.m_MaxWidths = 1;
			this.m_MaxHeights = 1;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x000154A5 File Offset: 0x000136A5
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x000154B0 File Offset: 0x000136B0
		public void Initialize(int width, int height)
		{
			if (this.m_AutoSizedRTs.Count != 0)
			{
				string arg = "Unreleased RTHandles:";
				foreach (RTHandle rthandle in this.m_AutoSizedRTs)
				{
					arg = string.Format("{0}\n    {1}", arg, rthandle.name);
				}
				Debug.LogError(string.Format("RTHandle.Initialize should only be called once before allocating any Render Texture. This may be caused by an unreleased RTHandle resource.\n{0}\n", arg));
			}
			this.m_MaxWidths = width;
			this.m_MaxHeights = height;
			this.m_HardwareDynamicResRequested = DynamicResolutionHandler.instance.RequestsHardwareDynamicResolution();
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00015550 File Offset: 0x00013750
		public void Release(RTHandle rth)
		{
			if (rth != null)
			{
				rth.Release();
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0001555B File Offset: 0x0001375B
		internal void Remove(RTHandle rth)
		{
			this.m_AutoSizedRTs.Remove(rth);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0001556A File Offset: 0x0001376A
		public void ResetReferenceSize(int width, int height)
		{
			this.m_MaxWidths = width;
			this.m_MaxHeights = height;
			this.SetReferenceSize(width, height, true);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00015583 File Offset: 0x00013783
		public void SetReferenceSize(int width, int height)
		{
			this.SetReferenceSize(width, height, false);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00015590 File Offset: 0x00013790
		public void SetReferenceSize(int width, int height, bool reset)
		{
			this.m_RTHandleProperties.previousViewportSize = this.m_RTHandleProperties.currentViewportSize;
			this.m_RTHandleProperties.previousRenderTargetSize = this.m_RTHandleProperties.currentRenderTargetSize;
			Vector2 b = new Vector2((float)this.GetMaxWidth(), (float)this.GetMaxHeight());
			width = Mathf.Max(width, 1);
			height = Mathf.Max(height, 1);
			bool flag = width > this.GetMaxWidth() || height > this.GetMaxHeight() || reset;
			if (flag)
			{
				this.Resize(width, height, flag);
			}
			this.m_RTHandleProperties.currentViewportSize = new Vector2Int(width, height);
			this.m_RTHandleProperties.currentRenderTargetSize = new Vector2Int(this.GetMaxWidth(), this.GetMaxHeight());
			if (this.m_RTHandleProperties.previousViewportSize.x == 0)
			{
				this.m_RTHandleProperties.previousViewportSize = this.m_RTHandleProperties.currentViewportSize;
				this.m_RTHandleProperties.previousRenderTargetSize = this.m_RTHandleProperties.currentRenderTargetSize;
				b = new Vector2((float)this.GetMaxWidth(), (float)this.GetMaxHeight());
			}
			Vector2 vector = this.CalculateRatioAgainstMaxSize(this.m_RTHandleProperties.currentViewportSize);
			if (DynamicResolutionHandler.instance.HardwareDynamicResIsEnabled() && this.m_HardwareDynamicResRequested)
			{
				this.m_RTHandleProperties.rtHandleScale = new Vector4(vector.x, vector.y, this.m_RTHandleProperties.rtHandleScale.x, this.m_RTHandleProperties.rtHandleScale.y);
				return;
			}
			Vector2 vector2 = this.m_RTHandleProperties.previousViewportSize / b;
			this.m_RTHandleProperties.rtHandleScale = new Vector4(vector.x, vector.y, vector2.x, vector2.y);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00015738 File Offset: 0x00013938
		internal Vector2 CalculateRatioAgainstMaxSize(in Vector2Int viewportSize)
		{
			Vector2 vector = new Vector2((float)this.GetMaxWidth(), (float)this.GetMaxHeight());
			if (DynamicResolutionHandler.instance.HardwareDynamicResIsEnabled() && this.m_HardwareDynamicResRequested && viewportSize != DynamicResolutionHandler.instance.finalViewport)
			{
				Vector2 scales = viewportSize / DynamicResolutionHandler.instance.finalViewport;
				vector = DynamicResolutionHandler.instance.ApplyScalesOnSize(new Vector2Int(this.GetMaxWidth(), this.GetMaxHeight()), scales);
			}
			Vector2Int vector2Int = viewportSize;
			float x = (float)vector2Int.x / vector.x;
			vector2Int = viewportSize;
			return new Vector2(x, (float)vector2Int.y / vector.y);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x000157F8 File Offset: 0x000139F8
		public void SetHardwareDynamicResolutionState(bool enableHWDynamicRes)
		{
			if (enableHWDynamicRes != this.m_HardwareDynamicResRequested)
			{
				this.m_HardwareDynamicResRequested = enableHWDynamicRes;
				Array.Resize<RTHandle>(ref this.m_AutoSizedRTsArray, this.m_AutoSizedRTs.Count);
				this.m_AutoSizedRTs.CopyTo(this.m_AutoSizedRTsArray);
				int i = 0;
				int num = this.m_AutoSizedRTsArray.Length;
				while (i < num)
				{
					RTHandle rthandle = this.m_AutoSizedRTsArray[i];
					RenderTexture rt = rthandle.m_RT;
					if (rt)
					{
						rt.Release();
						rt.useDynamicScale = (this.m_HardwareDynamicResRequested && rthandle.m_EnableHWDynamicScale);
						rt.Create();
					}
					i++;
				}
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00015890 File Offset: 0x00013A90
		internal void SwitchResizeMode(RTHandle rth, RTHandleSystem.ResizeMode mode)
		{
			if (!rth.useScaling)
			{
				return;
			}
			if (mode != RTHandleSystem.ResizeMode.Auto)
			{
				if (mode == RTHandleSystem.ResizeMode.OnDemand)
				{
					this.m_AutoSizedRTs.Remove(rth);
					this.m_ResizeOnDemandRTs.Add(rth);
					return;
				}
			}
			else
			{
				if (this.m_ResizeOnDemandRTs.Contains(rth))
				{
					this.DemandResize(rth);
				}
				this.m_ResizeOnDemandRTs.Remove(rth);
				this.m_AutoSizedRTs.Add(rth);
			}
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x000158F8 File Offset: 0x00013AF8
		private void DemandResize(RTHandle rth)
		{
			RenderTexture rt = rth.m_RT;
			rth.referenceSize = new Vector2Int(this.m_MaxWidths, this.m_MaxHeights);
			Vector2Int rhs = rth.GetScaledSize(rth.referenceSize);
			rhs = Vector2Int.Max(Vector2Int.one, rhs);
			if (rt.width != rhs.x || rt.height != rhs.y)
			{
				rt.Release();
				rt.width = rhs.x;
				rt.height = rhs.y;
				rt.name = CoreUtils.GetRenderTargetAutoName(rt.width, rt.height, rt.volumeDepth, rt.graphicsFormat, rt.dimension, rth.m_Name, rt.useMipMap, rth.m_EnableMSAA, (MSAASamples)rt.antiAliasing, rt.useDynamicScale);
				rt.Create();
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x000159D0 File Offset: 0x00013BD0
		public int GetMaxWidth()
		{
			return this.m_MaxWidths;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x000159D8 File Offset: 0x00013BD8
		public int GetMaxHeight()
		{
			return this.m_MaxHeights;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x000159E0 File Offset: 0x00013BE0
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				Array.Resize<RTHandle>(ref this.m_AutoSizedRTsArray, this.m_AutoSizedRTs.Count);
				this.m_AutoSizedRTs.CopyTo(this.m_AutoSizedRTsArray);
				int i = 0;
				int num = this.m_AutoSizedRTsArray.Length;
				while (i < num)
				{
					RTHandle rth = this.m_AutoSizedRTsArray[i];
					this.Release(rth);
					i++;
				}
				this.m_AutoSizedRTs.Clear();
				Array.Resize<RTHandle>(ref this.m_AutoSizedRTsArray, this.m_ResizeOnDemandRTs.Count);
				this.m_ResizeOnDemandRTs.CopyTo(this.m_AutoSizedRTsArray);
				int j = 0;
				int num2 = this.m_AutoSizedRTsArray.Length;
				while (j < num2)
				{
					RTHandle rth2 = this.m_AutoSizedRTsArray[j];
					this.Release(rth2);
					j++;
				}
				this.m_ResizeOnDemandRTs.Clear();
				this.m_AutoSizedRTsArray = null;
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00015AAC File Offset: 0x00013CAC
		private void Resize(int width, int height, bool sizeChanged)
		{
			this.m_MaxWidths = Math.Max(width, this.m_MaxWidths);
			this.m_MaxHeights = Math.Max(height, this.m_MaxHeights);
			Vector2Int vector2Int = new Vector2Int(this.m_MaxWidths, this.m_MaxHeights);
			Array.Resize<RTHandle>(ref this.m_AutoSizedRTsArray, this.m_AutoSizedRTs.Count);
			this.m_AutoSizedRTs.CopyTo(this.m_AutoSizedRTsArray);
			int i = 0;
			int num = this.m_AutoSizedRTsArray.Length;
			while (i < num)
			{
				RTHandle rthandle = this.m_AutoSizedRTsArray[i];
				rthandle.referenceSize = vector2Int;
				RenderTexture rt = rthandle.m_RT;
				rt.Release();
				Vector2Int scaledSize = rthandle.GetScaledSize(vector2Int);
				rt.width = Mathf.Max(scaledSize.x, 1);
				rt.height = Mathf.Max(scaledSize.y, 1);
				rt.name = CoreUtils.GetRenderTargetAutoName(rt.width, rt.height, rt.volumeDepth, rt.graphicsFormat, rt.dimension, rthandle.m_Name, rt.useMipMap, rthandle.m_EnableMSAA, (MSAASamples)rt.antiAliasing, rt.useDynamicScale);
				rt.Create();
				i++;
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00015BDC File Offset: 0x00013DDC
		public RTHandle Alloc(int width, int height, int slices = 1, DepthBits depthBufferBits = DepthBits.None, GraphicsFormat colorFormat = GraphicsFormat.R8G8B8A8_SRGB, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, MSAASamples msaaSamples = MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, string name = "")
		{
			return this.Alloc(width, height, wrapMode, wrapMode, wrapMode, slices, depthBufferBits, colorFormat, filterMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, memoryless, name);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00015C18 File Offset: 0x00013E18
		public RTHandle Alloc(int width, int height, TextureWrapMode wrapModeU, TextureWrapMode wrapModeV, TextureWrapMode wrapModeW = TextureWrapMode.Repeat, int slices = 1, DepthBits depthBufferBits = DepthBits.None, GraphicsFormat colorFormat = GraphicsFormat.R8G8B8A8_SRGB, FilterMode filterMode = FilterMode.Point, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, MSAASamples msaaSamples = MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, string name = "")
		{
			bool flag = msaaSamples != MSAASamples.None;
			if (!flag && bindTextureMS)
			{
				Debug.LogWarning("RTHandle allocated without MSAA but with bindMS set to true, forcing bindMS to false.");
				bindTextureMS = false;
			}
			RenderTexture renderTexture;
			if (isShadowMap || depthBufferBits != DepthBits.None)
			{
				RenderTextureFormat format = isShadowMap ? RenderTextureFormat.Shadowmap : RenderTextureFormat.Depth;
				renderTexture = new RenderTexture(width, height, (int)depthBufferBits, format, RenderTextureReadWrite.Linear)
				{
					hideFlags = HideFlags.HideAndDontSave,
					volumeDepth = slices,
					filterMode = filterMode,
					wrapModeU = wrapModeU,
					wrapModeV = wrapModeV,
					wrapModeW = wrapModeW,
					dimension = dimension,
					enableRandomWrite = enableRandomWrite,
					useMipMap = useMipMap,
					autoGenerateMips = autoGenerateMips,
					anisoLevel = anisoLevel,
					mipMapBias = mipMapBias,
					antiAliasing = (int)msaaSamples,
					bindTextureMS = bindTextureMS,
					useDynamicScale = (this.m_HardwareDynamicResRequested && useDynamicScale),
					memorylessMode = memoryless,
					name = CoreUtils.GetRenderTargetAutoName(width, height, slices, format, name, useMipMap, flag, msaaSamples)
				};
			}
			else
			{
				renderTexture = new RenderTexture(width, height, (int)depthBufferBits, colorFormat)
				{
					hideFlags = HideFlags.HideAndDontSave,
					volumeDepth = slices,
					filterMode = filterMode,
					wrapModeU = wrapModeU,
					wrapModeV = wrapModeV,
					wrapModeW = wrapModeW,
					dimension = dimension,
					enableRandomWrite = enableRandomWrite,
					useMipMap = useMipMap,
					autoGenerateMips = autoGenerateMips,
					anisoLevel = anisoLevel,
					mipMapBias = mipMapBias,
					antiAliasing = (int)msaaSamples,
					bindTextureMS = bindTextureMS,
					useDynamicScale = (this.m_HardwareDynamicResRequested && useDynamicScale),
					memorylessMode = memoryless,
					name = CoreUtils.GetRenderTargetAutoName(width, height, slices, colorFormat, dimension, name, useMipMap, flag, msaaSamples, useDynamicScale)
				};
			}
			renderTexture.Create();
			RTHandle rthandle = new RTHandle(this);
			rthandle.SetRenderTexture(renderTexture);
			rthandle.useScaling = false;
			rthandle.m_EnableRandomWrite = enableRandomWrite;
			rthandle.m_EnableMSAA = flag;
			rthandle.m_EnableHWDynamicScale = useDynamicScale;
			rthandle.m_Name = name;
			rthandle.referenceSize = new Vector2Int(width, height);
			return rthandle;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00015DFC File Offset: 0x00013FFC
		public RTHandle Alloc(Vector2 scaleFactor, int slices = 1, DepthBits depthBufferBits = DepthBits.None, GraphicsFormat colorFormat = GraphicsFormat.R8G8B8A8_SRGB, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, MSAASamples msaaSamples = MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, string name = "")
		{
			int num = Mathf.Max(Mathf.RoundToInt(scaleFactor.x * (float)this.GetMaxWidth()), 1);
			int num2 = Mathf.Max(Mathf.RoundToInt(scaleFactor.y * (float)this.GetMaxHeight()), 1);
			RTHandle rthandle = this.AllocAutoSizedRenderTexture(num, num2, slices, depthBufferBits, colorFormat, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, memoryless, name);
			rthandle.referenceSize = new Vector2Int(num, num2);
			rthandle.scaleFactor = scaleFactor;
			return rthandle;
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00015E7C File Offset: 0x0001407C
		public RTHandle Alloc(ScaleFunc scaleFunc, int slices = 1, DepthBits depthBufferBits = DepthBits.None, GraphicsFormat colorFormat = GraphicsFormat.R8G8B8A8_SRGB, FilterMode filterMode = FilterMode.Point, TextureWrapMode wrapMode = TextureWrapMode.Repeat, TextureDimension dimension = TextureDimension.Tex2D, bool enableRandomWrite = false, bool useMipMap = false, bool autoGenerateMips = true, bool isShadowMap = false, int anisoLevel = 1, float mipMapBias = 0f, MSAASamples msaaSamples = MSAASamples.None, bool bindTextureMS = false, bool useDynamicScale = false, RenderTextureMemoryless memoryless = RenderTextureMemoryless.None, string name = "")
		{
			Vector2Int vector2Int = scaleFunc(new Vector2Int(this.GetMaxWidth(), this.GetMaxHeight()));
			int num = Mathf.Max(vector2Int.x, 1);
			int num2 = Mathf.Max(vector2Int.y, 1);
			RTHandle rthandle = this.AllocAutoSizedRenderTexture(num, num2, slices, depthBufferBits, colorFormat, filterMode, wrapMode, dimension, enableRandomWrite, useMipMap, autoGenerateMips, isShadowMap, anisoLevel, mipMapBias, msaaSamples, bindTextureMS, useDynamicScale, memoryless, name);
			rthandle.referenceSize = new Vector2Int(num, num2);
			rthandle.scaleFunc = scaleFunc;
			return rthandle;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00015EFC File Offset: 0x000140FC
		private RTHandle AllocAutoSizedRenderTexture(int width, int height, int slices, DepthBits depthBufferBits, GraphicsFormat colorFormat, FilterMode filterMode, TextureWrapMode wrapMode, TextureDimension dimension, bool enableRandomWrite, bool useMipMap, bool autoGenerateMips, bool isShadowMap, int anisoLevel, float mipMapBias, MSAASamples msaaSamples, bool bindTextureMS, bool useDynamicScale, RenderTextureMemoryless memoryless, string name)
		{
			bool flag = msaaSamples != MSAASamples.None;
			if (!flag && bindTextureMS)
			{
				Debug.LogWarning("RTHandle allocated without MSAA but with bindMS set to true, forcing bindMS to false.");
				bindTextureMS = false;
			}
			if (flag && enableRandomWrite)
			{
				Debug.LogWarning("RTHandle that is MSAA-enabled cannot allocate MSAA RT with 'enableRandomWrite = true'.");
				enableRandomWrite = false;
			}
			RenderTexture renderTexture;
			if (isShadowMap || depthBufferBits != DepthBits.None)
			{
				RenderTextureFormat format = isShadowMap ? RenderTextureFormat.Shadowmap : RenderTextureFormat.Depth;
				GraphicsFormat stencilFormat = isShadowMap ? GraphicsFormat.None : GraphicsFormat.R8_UInt;
				renderTexture = new RenderTexture(width, height, (int)depthBufferBits, format, RenderTextureReadWrite.Linear)
				{
					hideFlags = HideFlags.HideAndDontSave,
					volumeDepth = slices,
					filterMode = filterMode,
					wrapMode = wrapMode,
					dimension = dimension,
					enableRandomWrite = enableRandomWrite,
					useMipMap = useMipMap,
					autoGenerateMips = autoGenerateMips,
					anisoLevel = anisoLevel,
					mipMapBias = mipMapBias,
					antiAliasing = (int)msaaSamples,
					bindTextureMS = bindTextureMS,
					useDynamicScale = (this.m_HardwareDynamicResRequested && useDynamicScale),
					memorylessMode = memoryless,
					stencilFormat = stencilFormat,
					name = CoreUtils.GetRenderTargetAutoName(width, height, slices, colorFormat, dimension, name, useMipMap, flag, msaaSamples, useDynamicScale)
				};
			}
			else
			{
				renderTexture = new RenderTexture(width, height, (int)depthBufferBits, colorFormat)
				{
					hideFlags = HideFlags.HideAndDontSave,
					volumeDepth = slices,
					filterMode = filterMode,
					wrapMode = wrapMode,
					dimension = dimension,
					enableRandomWrite = enableRandomWrite,
					useMipMap = useMipMap,
					autoGenerateMips = autoGenerateMips,
					anisoLevel = anisoLevel,
					mipMapBias = mipMapBias,
					antiAliasing = (int)msaaSamples,
					bindTextureMS = bindTextureMS,
					useDynamicScale = (this.m_HardwareDynamicResRequested && useDynamicScale),
					memorylessMode = memoryless,
					name = CoreUtils.GetRenderTargetAutoName(width, height, slices, colorFormat, dimension, name, useMipMap, flag, msaaSamples, useDynamicScale)
				};
			}
			renderTexture.Create();
			RTHandle rthandle = new RTHandle(this);
			rthandle.SetRenderTexture(renderTexture);
			rthandle.m_EnableMSAA = flag;
			rthandle.m_EnableRandomWrite = enableRandomWrite;
			rthandle.useScaling = true;
			rthandle.m_EnableHWDynamicScale = useDynamicScale;
			rthandle.m_Name = name;
			this.m_AutoSizedRTs.Add(rthandle);
			return rthandle;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x000160E9 File Offset: 0x000142E9
		public RTHandle Alloc(RenderTexture texture)
		{
			RTHandle rthandle = new RTHandle(this);
			rthandle.SetRenderTexture(texture);
			rthandle.m_EnableMSAA = false;
			rthandle.m_EnableRandomWrite = false;
			rthandle.useScaling = false;
			rthandle.m_EnableHWDynamicScale = false;
			rthandle.m_Name = texture.name;
			return rthandle;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00016120 File Offset: 0x00014320
		public RTHandle Alloc(Texture texture)
		{
			RTHandle rthandle = new RTHandle(this);
			rthandle.SetTexture(texture);
			rthandle.m_EnableMSAA = false;
			rthandle.m_EnableRandomWrite = false;
			rthandle.useScaling = false;
			rthandle.m_EnableHWDynamicScale = false;
			rthandle.m_Name = texture.name;
			return rthandle;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00016157 File Offset: 0x00014357
		public RTHandle Alloc(RenderTargetIdentifier texture)
		{
			return this.Alloc(texture, "");
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00016165 File Offset: 0x00014365
		public RTHandle Alloc(RenderTargetIdentifier texture, string name)
		{
			RTHandle rthandle = new RTHandle(this);
			rthandle.SetTexture(texture);
			rthandle.m_EnableMSAA = false;
			rthandle.m_EnableRandomWrite = false;
			rthandle.useScaling = false;
			rthandle.m_EnableHWDynamicScale = false;
			rthandle.m_Name = name;
			return rthandle;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00016197 File Offset: 0x00014397
		private static RTHandle Alloc(RTHandle tex)
		{
			Debug.LogError("Allocation a RTHandle from another one is forbidden.");
			return null;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x000161A4 File Offset: 0x000143A4
		internal string DumpRTInfo()
		{
			string text = "";
			Array.Resize<RTHandle>(ref this.m_AutoSizedRTsArray, this.m_AutoSizedRTs.Count);
			this.m_AutoSizedRTs.CopyTo(this.m_AutoSizedRTsArray);
			int i = 0;
			int num = this.m_AutoSizedRTsArray.Length;
			while (i < num)
			{
				RenderTexture rt = this.m_AutoSizedRTsArray[i].rt;
				text = string.Format("{0}\nRT ({1})\t Format: {2} W: {3} H {4}\n", new object[]
				{
					text,
					i,
					rt.format,
					rt.width,
					rt.height
				});
				i++;
			}
			return text;
		}

		// Token: 0x04000309 RID: 777
		private bool m_HardwareDynamicResRequested;

		// Token: 0x0400030A RID: 778
		private HashSet<RTHandle> m_AutoSizedRTs;

		// Token: 0x0400030B RID: 779
		private RTHandle[] m_AutoSizedRTsArray;

		// Token: 0x0400030C RID: 780
		private HashSet<RTHandle> m_ResizeOnDemandRTs;

		// Token: 0x0400030D RID: 781
		private RTHandleProperties m_RTHandleProperties;

		// Token: 0x0400030E RID: 782
		private int m_MaxWidths;

		// Token: 0x0400030F RID: 783
		private int m_MaxHeights;

		// Token: 0x0200016D RID: 365
		internal enum ResizeMode
		{
			// Token: 0x04000578 RID: 1400
			Auto,
			// Token: 0x04000579 RID: 1401
			OnDemand
		}
	}
}
