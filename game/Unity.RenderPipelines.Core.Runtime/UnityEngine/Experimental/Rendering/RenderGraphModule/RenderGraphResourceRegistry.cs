using System;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RendererUtils;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000032 RID: 50
	internal class RenderGraphResourceRegistry
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000AD80 File Offset: 0x00008F80
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x0000AD87 File Offset: 0x00008F87
		internal static RenderGraphResourceRegistry current
		{
			get
			{
				return RenderGraphResourceRegistry.m_CurrentRegistry;
			}
			set
			{
				RenderGraphResourceRegistry.m_CurrentRegistry = value;
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000AD90 File Offset: 0x00008F90
		internal RTHandle GetTexture(in TextureHandle handle)
		{
			TextureHandle textureHandle = handle;
			if (!textureHandle.IsValid())
			{
				return null;
			}
			TextureResource textureResource = this.GetTextureResource(handle.handle);
			RTHandle graphicsResource = textureResource.graphicsResource;
			if (graphicsResource == null)
			{
				if (handle.fallBackResource != TextureHandle.nullHandle.handle)
				{
					return this.GetTextureResource(handle.fallBackResource).graphicsResource;
				}
				if (!textureResource.imported)
				{
					throw new InvalidOperationException("Trying to use a texture (" + textureResource.GetName() + ") that was already released or not yet created. Make sure you declare it for reading in your pass or you don't read it before it's been written to at least once.");
				}
			}
			return graphicsResource;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000AE18 File Offset: 0x00009018
		internal bool TextureNeedsFallback(in TextureHandle handle)
		{
			TextureHandle textureHandle = handle;
			return textureHandle.IsValid() && this.GetTextureResource(handle.handle).NeedsFallBack();
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000AE48 File Offset: 0x00009048
		internal RendererList GetRendererList(in RendererListHandle handle)
		{
			RendererListHandle rendererListHandle = handle;
			if (!rendererListHandle.IsValid() || handle >= this.m_RendererListResources.size)
			{
				return RendererList.nullRendererList;
			}
			return this.m_RendererListResources[handle].rendererList;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000AEA0 File Offset: 0x000090A0
		internal ComputeBuffer GetComputeBuffer(in ComputeBufferHandle handle)
		{
			ComputeBufferHandle computeBufferHandle = handle;
			if (!computeBufferHandle.IsValid())
			{
				return null;
			}
			ComputeBuffer graphicsResource = this.GetComputeBufferResource(handle.handle).graphicsResource;
			if (graphicsResource == null)
			{
				throw new InvalidOperationException("Trying to use a compute buffer ({bufferResource.GetName()}) that was already released or not yet created. Make sure you declare it for reading in your pass or you don't read it before it's been written to at least once.");
			}
			return graphicsResource;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000AEDE File Offset: 0x000090DE
		private RenderGraphResourceRegistry()
		{
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000AF18 File Offset: 0x00009118
		internal RenderGraphResourceRegistry(RenderGraphDebugParams renderGraphDebug, RenderGraphLogger frameInformationLogger)
		{
			this.m_RenderGraphDebug = renderGraphDebug;
			this.m_FrameInformationLogger = frameInformationLogger;
			for (int i = 0; i < 2; i++)
			{
				this.m_RenderGraphResources[i] = new RenderGraphResourceRegistry.RenderGraphResourcesData();
			}
			this.m_RenderGraphResources[0].createResourceCallback = new RenderGraphResourceRegistry.ResourceCreateCallback(this.CreateTextureCallback);
			this.m_RenderGraphResources[0].releaseResourceCallback = new RenderGraphResourceRegistry.ResourceCallback(this.ReleaseTextureCallback);
			this.m_RenderGraphResources[0].pool = new TexturePool();
			this.m_RenderGraphResources[1].pool = new ComputeBufferPool();
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000AFDA File Offset: 0x000091DA
		internal void BeginRenderGraph(int executionCount)
		{
			this.m_ExecutionCount = executionCount;
			ResourceHandle.NewFrame(executionCount);
			if (this.m_RenderGraphDebug.enableLogging)
			{
				this.m_ResourceLogger.Initialize("RenderGraph Resources");
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000B006 File Offset: 0x00009206
		internal void BeginExecute(int currentFrameIndex)
		{
			this.m_CurrentFrameIndex = currentFrameIndex;
			this.ManageSharedRenderGraphResources();
			RenderGraphResourceRegistry.current = this;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000B01B File Offset: 0x0000921B
		internal void EndExecute()
		{
			RenderGraphResourceRegistry.current = null;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000B024 File Offset: 0x00009224
		private void CheckHandleValidity(in ResourceHandle res)
		{
			RenderGraphResourceType type = res.type;
			ResourceHandle resourceHandle = res;
			this.CheckHandleValidity(type, resourceHandle.index);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000B04C File Offset: 0x0000924C
		private void CheckHandleValidity(RenderGraphResourceType type, int index)
		{
			DynamicArray<IRenderGraphResource> resourceArray = this.m_RenderGraphResources[(int)type].resourceArray;
			if (index >= resourceArray.size)
			{
				throw new ArgumentException(string.Format("Trying to access resource of type {0} with an invalid resource index {1}", type, index));
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000B08C File Offset: 0x0000928C
		internal unsafe void IncrementWriteCount(in ResourceHandle res)
		{
			this.CheckHandleValidity(res);
			RenderGraphResourceRegistry.RenderGraphResourcesData[] renderGraphResources = this.m_RenderGraphResources;
			ResourceHandle resourceHandle = res;
			DynamicArray<IRenderGraphResource> resourceArray = renderGraphResources[resourceHandle.iType].resourceArray;
			resourceHandle = res;
			resourceArray[resourceHandle.index]->IncrementWriteCount();
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000B0D4 File Offset: 0x000092D4
		internal unsafe string GetRenderGraphResourceName(in ResourceHandle res)
		{
			this.CheckHandleValidity(res);
			RenderGraphResourceRegistry.RenderGraphResourcesData[] renderGraphResources = this.m_RenderGraphResources;
			ResourceHandle resourceHandle = res;
			DynamicArray<IRenderGraphResource> resourceArray = renderGraphResources[resourceHandle.iType].resourceArray;
			resourceHandle = res;
			return resourceArray[resourceHandle.index]->GetName();
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000B11B File Offset: 0x0000931B
		internal unsafe string GetRenderGraphResourceName(RenderGraphResourceType type, int index)
		{
			this.CheckHandleValidity(type, index);
			return this.m_RenderGraphResources[(int)type].resourceArray[index]->GetName();
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000B140 File Offset: 0x00009340
		internal unsafe bool IsRenderGraphResourceImported(in ResourceHandle res)
		{
			this.CheckHandleValidity(res);
			RenderGraphResourceRegistry.RenderGraphResourcesData[] renderGraphResources = this.m_RenderGraphResources;
			ResourceHandle resourceHandle = res;
			DynamicArray<IRenderGraphResource> resourceArray = renderGraphResources[resourceHandle.iType].resourceArray;
			resourceHandle = res;
			return resourceArray[resourceHandle.index]->imported;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000B187 File Offset: 0x00009387
		internal bool IsRenderGraphResourceShared(RenderGraphResourceType type, int index)
		{
			this.CheckHandleValidity(type, index);
			return index < this.m_RenderGraphResources[(int)type].sharedResourcesCount;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000B1A4 File Offset: 0x000093A4
		internal unsafe bool IsGraphicsResourceCreated(in ResourceHandle res)
		{
			this.CheckHandleValidity(res);
			RenderGraphResourceRegistry.RenderGraphResourcesData[] renderGraphResources = this.m_RenderGraphResources;
			ResourceHandle resourceHandle = res;
			DynamicArray<IRenderGraphResource> resourceArray = renderGraphResources[resourceHandle.iType].resourceArray;
			resourceHandle = res;
			return resourceArray[resourceHandle.index]->IsCreated();
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000B1EB File Offset: 0x000093EB
		internal bool IsRendererListCreated(in RendererListHandle res)
		{
			return this.m_RendererListResources[res].rendererList.isValid;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000B20D File Offset: 0x0000940D
		internal unsafe bool IsRenderGraphResourceImported(RenderGraphResourceType type, int index)
		{
			this.CheckHandleValidity(type, index);
			return this.m_RenderGraphResources[(int)type].resourceArray[index]->imported;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000B230 File Offset: 0x00009430
		internal unsafe int GetRenderGraphResourceTransientIndex(in ResourceHandle res)
		{
			this.CheckHandleValidity(res);
			RenderGraphResourceRegistry.RenderGraphResourcesData[] renderGraphResources = this.m_RenderGraphResources;
			ResourceHandle resourceHandle = res;
			DynamicArray<IRenderGraphResource> resourceArray = renderGraphResources[resourceHandle.iType].resourceArray;
			resourceHandle = res;
			return resourceArray[resourceHandle.index]->transientPassIndex;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000B278 File Offset: 0x00009478
		internal TextureHandle ImportTexture(RTHandle rt)
		{
			TextureResource textureResource;
			int handle = this.m_RenderGraphResources[0].AddNewRenderGraphResource<TextureResource>(out textureResource, true);
			textureResource.graphicsResource = rt;
			textureResource.imported = true;
			return new TextureHandle(handle, false);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000B2AC File Offset: 0x000094AC
		internal unsafe TextureHandle CreateSharedTexture(in TextureDesc desc, bool explicitRelease)
		{
			RenderGraphResourceRegistry.RenderGraphResourcesData renderGraphResourcesData = this.m_RenderGraphResources[0];
			int sharedResourcesCount = renderGraphResourcesData.sharedResourcesCount;
			TextureResource textureResource = null;
			int handle = -1;
			for (int i = 0; i < sharedResourcesCount; i++)
			{
				if (!renderGraphResourcesData.resourceArray[i]->shared)
				{
					textureResource = (TextureResource)(*renderGraphResourcesData.resourceArray[i]);
					handle = i;
					break;
				}
			}
			if (textureResource == null)
			{
				handle = this.m_RenderGraphResources[0].AddNewRenderGraphResource<TextureResource>(out textureResource, false);
				renderGraphResourcesData.sharedResourcesCount++;
			}
			textureResource.imported = true;
			textureResource.shared = true;
			textureResource.sharedExplicitRelease = explicitRelease;
			textureResource.desc = desc;
			return new TextureHandle(handle, true);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000B358 File Offset: 0x00009558
		internal void ReleaseSharedTexture(TextureHandle texture)
		{
			RenderGraphResourceRegistry.RenderGraphResourcesData renderGraphResourcesData = this.m_RenderGraphResources[0];
			if (texture.handle >= renderGraphResourcesData.sharedResourcesCount)
			{
				throw new InvalidOperationException("Tried to release a non shared texture.");
			}
			if (texture.handle == renderGraphResourcesData.sharedResourcesCount - 1)
			{
				renderGraphResourcesData.sharedResourcesCount--;
			}
			TextureResource textureResource = this.GetTextureResource(texture.handle);
			textureResource.ReleaseGraphicsResource();
			textureResource.Reset(null);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000B3C8 File Offset: 0x000095C8
		internal TextureHandle ImportBackbuffer(RenderTargetIdentifier rt)
		{
			if (this.m_CurrentBackbuffer != null)
			{
				this.m_CurrentBackbuffer.SetTexture(rt);
			}
			else
			{
				this.m_CurrentBackbuffer = RTHandles.Alloc(rt, "Backbuffer");
			}
			TextureResource textureResource;
			int handle = this.m_RenderGraphResources[0].AddNewRenderGraphResource<TextureResource>(out textureResource, true);
			textureResource.graphicsResource = this.m_CurrentBackbuffer;
			textureResource.imported = true;
			return new TextureHandle(handle, false);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000B428 File Offset: 0x00009628
		internal TextureHandle CreateTexture(in TextureDesc desc, int transientPassIndex = -1)
		{
			this.ValidateTextureDesc(desc);
			TextureResource textureResource;
			int handle = this.m_RenderGraphResources[0].AddNewRenderGraphResource<TextureResource>(out textureResource, true);
			textureResource.desc = desc;
			textureResource.transientPassIndex = transientPassIndex;
			textureResource.requestFallBack = desc.fallBackToBlackTexture;
			return new TextureHandle(handle, false);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000B471 File Offset: 0x00009671
		internal int GetTextureResourceCount()
		{
			return this.m_RenderGraphResources[0].resourceArray.size;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000B485 File Offset: 0x00009685
		private unsafe TextureResource GetTextureResource(in ResourceHandle handle)
		{
			return (*this.m_RenderGraphResources[0].resourceArray[handle]) as TextureResource;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000B4AA File Offset: 0x000096AA
		internal unsafe TextureDesc GetTextureResourceDesc(in ResourceHandle handle)
		{
			return ((*this.m_RenderGraphResources[0].resourceArray[handle]) as TextureResource).desc;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000B4D4 File Offset: 0x000096D4
		internal void ForceTextureClear(in ResourceHandle handle, Color clearColor)
		{
			this.GetTextureResource(handle).desc.clearBuffer = true;
			this.GetTextureResource(handle).desc.clearColor = clearColor;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000B4FC File Offset: 0x000096FC
		internal RendererListHandle CreateRendererList(in RendererListDesc desc)
		{
			this.ValidateRendererListDesc(desc);
			DynamicArray<RendererListResource> rendererListResources = this.m_RendererListResources;
			RendererListResource rendererListResource = new RendererListResource(ref desc);
			return new RendererListHandle(rendererListResources.Add(rendererListResource));
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000B52C File Offset: 0x0000972C
		internal ComputeBufferHandle ImportComputeBuffer(ComputeBuffer computeBuffer)
		{
			ComputeBufferResource computeBufferResource;
			int handle = this.m_RenderGraphResources[1].AddNewRenderGraphResource<ComputeBufferResource>(out computeBufferResource, true);
			computeBufferResource.graphicsResource = computeBuffer;
			computeBufferResource.imported = true;
			return new ComputeBufferHandle(handle, false);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000B560 File Offset: 0x00009760
		internal ComputeBufferHandle CreateComputeBuffer(in ComputeBufferDesc desc, int transientPassIndex = -1)
		{
			this.ValidateComputeBufferDesc(desc);
			ComputeBufferResource computeBufferResource;
			int handle = this.m_RenderGraphResources[1].AddNewRenderGraphResource<ComputeBufferResource>(out computeBufferResource, true);
			computeBufferResource.desc = desc;
			computeBufferResource.transientPassIndex = transientPassIndex;
			return new ComputeBufferHandle(handle, false);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000B59D File Offset: 0x0000979D
		internal unsafe ComputeBufferDesc GetComputeBufferResourceDesc(in ResourceHandle handle)
		{
			return ((*this.m_RenderGraphResources[1].resourceArray[handle]) as ComputeBufferResource).desc;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000B5C7 File Offset: 0x000097C7
		internal int GetComputeBufferResourceCount()
		{
			return this.m_RenderGraphResources[1].resourceArray.size;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000B5DB File Offset: 0x000097DB
		private unsafe ComputeBufferResource GetComputeBufferResource(in ResourceHandle handle)
		{
			return (*this.m_RenderGraphResources[1].resourceArray[handle]) as ComputeBufferResource;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000B600 File Offset: 0x00009800
		internal unsafe void UpdateSharedResourceLastFrameIndex(int type, int index)
		{
			this.m_RenderGraphResources[type].resourceArray[index]->sharedResourceLastFrameUsed = this.m_ExecutionCount;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000B624 File Offset: 0x00009824
		private unsafe void ManageSharedRenderGraphResources()
		{
			for (int i = 0; i < 2; i++)
			{
				RenderGraphResourceRegistry.RenderGraphResourcesData renderGraphResourcesData = this.m_RenderGraphResources[i];
				for (int j = 0; j < renderGraphResourcesData.sharedResourcesCount; j++)
				{
					IRenderGraphResource renderGraphResource = *this.m_RenderGraphResources[i].resourceArray[j];
					bool flag = renderGraphResource.IsCreated();
					if (renderGraphResource.sharedResourceLastFrameUsed == this.m_ExecutionCount && !flag)
					{
						renderGraphResource.CreateGraphicsResource(renderGraphResource.GetName());
					}
					else if (flag && !renderGraphResource.sharedExplicitRelease && renderGraphResource.sharedResourceLastFrameUsed + 30 < this.m_ExecutionCount)
					{
						renderGraphResource.ReleaseGraphicsResource();
					}
				}
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000B6BC File Offset: 0x000098BC
		internal unsafe bool CreatePooledResource(RenderGraphContext rgContext, int type, int index)
		{
			bool? flag = new bool?(false);
			IRenderGraphResource renderGraphResource = *this.m_RenderGraphResources[type].resourceArray[index];
			if (!renderGraphResource.imported)
			{
				renderGraphResource.CreatePooledGraphicsResource();
				if (this.m_RenderGraphDebug.enableLogging)
				{
					renderGraphResource.LogCreation(this.m_FrameInformationLogger);
				}
				RenderGraphResourceRegistry.ResourceCreateCallback createResourceCallback = this.m_RenderGraphResources[type].createResourceCallback;
				flag = ((createResourceCallback != null) ? new bool?(createResourceCallback(rgContext, renderGraphResource)) : null);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000B740 File Offset: 0x00009940
		private bool CreateTextureCallback(RenderGraphContext rgContext, IRenderGraphResource res)
		{
			TextureResource textureResource = res as TextureResource;
			FastMemoryDesc fastMemoryDesc = textureResource.desc.fastMemoryDesc;
			if (fastMemoryDesc.inFastMemory)
			{
				textureResource.graphicsResource.SwitchToFastMemory(rgContext.cmd, fastMemoryDesc.residencyFraction, fastMemoryDesc.flags, false);
			}
			bool result = false;
			if (textureResource.desc.clearBuffer || this.m_RenderGraphDebug.clearRenderTargetsAtCreation)
			{
				bool flag = this.m_RenderGraphDebug.clearRenderTargetsAtCreation && !textureResource.desc.clearBuffer;
				using (new ProfilingScope(rgContext.cmd, ProfilingSampler.Get<RenderGraphProfileId>(flag ? RenderGraphProfileId.RenderGraphClearDebug : RenderGraphProfileId.RenderGraphClear)))
				{
					ClearFlag clearFlag = (textureResource.desc.depthBufferBits != DepthBits.None) ? ClearFlag.DepthStencil : ClearFlag.Color;
					Color clearColor = flag ? Color.magenta : textureResource.desc.clearColor;
					CoreUtils.SetRenderTarget(rgContext.cmd, textureResource.graphicsResource, clearFlag, clearColor, 0, CubemapFace.Unknown, -1);
				}
				result = true;
			}
			return result;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000B844 File Offset: 0x00009A44
		internal unsafe void ReleasePooledResource(RenderGraphContext rgContext, int type, int index)
		{
			IRenderGraphResource renderGraphResource = *this.m_RenderGraphResources[type].resourceArray[index];
			if (!renderGraphResource.imported)
			{
				RenderGraphResourceRegistry.ResourceCallback releaseResourceCallback = this.m_RenderGraphResources[type].releaseResourceCallback;
				if (releaseResourceCallback != null)
				{
					releaseResourceCallback(rgContext, renderGraphResource);
				}
				if (this.m_RenderGraphDebug.enableLogging)
				{
					renderGraphResource.LogRelease(this.m_FrameInformationLogger);
				}
				renderGraphResource.ReleasePooledGraphicsResource(this.m_CurrentFrameIndex);
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000B8B0 File Offset: 0x00009AB0
		private void ReleaseTextureCallback(RenderGraphContext rgContext, IRenderGraphResource res)
		{
			TextureResource textureResource = res as TextureResource;
			if (this.m_RenderGraphDebug.clearRenderTargetsAtRelease)
			{
				using (new ProfilingScope(rgContext.cmd, ProfilingSampler.Get<RenderGraphProfileId>(RenderGraphProfileId.RenderGraphClearDebug)))
				{
					ClearFlag clearFlag = (textureResource.desc.depthBufferBits != DepthBits.None) ? ClearFlag.DepthStencil : ClearFlag.Color;
					CoreUtils.SetRenderTarget(rgContext.cmd, textureResource.graphicsResource, clearFlag, Color.magenta, 0, CubemapFace.Unknown, -1);
				}
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000B930 File Offset: 0x00009B30
		private void ValidateTextureDesc(in TextureDesc desc)
		{
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000B932 File Offset: 0x00009B32
		private void ValidateRendererListDesc(in RendererListDesc desc)
		{
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000B934 File Offset: 0x00009B34
		private void ValidateComputeBufferDesc(in ComputeBufferDesc desc)
		{
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000B938 File Offset: 0x00009B38
		internal void CreateRendererLists(List<RendererListHandle> rendererLists, ScriptableRenderContext context, bool manualDispatch = false)
		{
			this.m_ActiveRendererLists.Clear();
			foreach (RendererListHandle handle in rendererLists)
			{
				ref RendererListResource ptr = ref this.m_RendererListResources[handle];
				ref RendererListDesc ptr2 = ref ptr.desc;
				ptr.rendererList = context.CreateRendererList(ptr2);
				this.m_ActiveRendererLists.Add(ptr.rendererList);
			}
			if (manualDispatch)
			{
				context.PrepareRendererListsAsync(this.m_ActiveRendererLists);
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000B9D8 File Offset: 0x00009BD8
		internal void Clear(bool onException)
		{
			this.LogResources();
			for (int i = 0; i < 2; i++)
			{
				this.m_RenderGraphResources[i].Clear(onException, this.m_CurrentFrameIndex);
			}
			this.m_RendererListResources.Clear();
			this.m_ActiveRendererLists.Clear();
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000BA24 File Offset: 0x00009C24
		internal void PurgeUnusedGraphicsResources()
		{
			for (int i = 0; i < 2; i++)
			{
				this.m_RenderGraphResources[i].PurgeUnusedGraphicsResources(this.m_CurrentFrameIndex);
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000BA50 File Offset: 0x00009C50
		internal void Cleanup()
		{
			for (int i = 0; i < 2; i++)
			{
				this.m_RenderGraphResources[i].Cleanup();
			}
			RTHandles.Release(this.m_CurrentBackbuffer);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000BA81 File Offset: 0x00009C81
		internal void FlushLogs()
		{
			Debug.Log(this.m_ResourceLogger.GetAllLogs());
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000BA94 File Offset: 0x00009C94
		private void LogResources()
		{
			if (this.m_RenderGraphDebug.enableLogging)
			{
				this.m_ResourceLogger.LogLine("==== Allocated Resources ====\n", Array.Empty<object>());
				for (int i = 0; i < 2; i++)
				{
					this.m_RenderGraphResources[i].pool.LogResources(this.m_ResourceLogger);
					this.m_ResourceLogger.LogLine("", Array.Empty<object>());
				}
			}
		}

		// Token: 0x04000139 RID: 313
		private const int kSharedResourceLifetime = 30;

		// Token: 0x0400013A RID: 314
		private static RenderGraphResourceRegistry m_CurrentRegistry;

		// Token: 0x0400013B RID: 315
		private RenderGraphResourceRegistry.RenderGraphResourcesData[] m_RenderGraphResources = new RenderGraphResourceRegistry.RenderGraphResourcesData[2];

		// Token: 0x0400013C RID: 316
		private DynamicArray<RendererListResource> m_RendererListResources = new DynamicArray<RendererListResource>();

		// Token: 0x0400013D RID: 317
		private RenderGraphDebugParams m_RenderGraphDebug;

		// Token: 0x0400013E RID: 318
		private RenderGraphLogger m_ResourceLogger = new RenderGraphLogger();

		// Token: 0x0400013F RID: 319
		private RenderGraphLogger m_FrameInformationLogger;

		// Token: 0x04000140 RID: 320
		private int m_CurrentFrameIndex;

		// Token: 0x04000141 RID: 321
		private int m_ExecutionCount;

		// Token: 0x04000142 RID: 322
		private RTHandle m_CurrentBackbuffer;

		// Token: 0x04000143 RID: 323
		private const int kInitialRendererListCount = 256;

		// Token: 0x04000144 RID: 324
		private List<RendererList> m_ActiveRendererLists = new List<RendererList>(256);

		// Token: 0x02000136 RID: 310
		// (Invoke) Token: 0x0600082D RID: 2093
		private delegate bool ResourceCreateCallback(RenderGraphContext rgContext, IRenderGraphResource res);

		// Token: 0x02000137 RID: 311
		// (Invoke) Token: 0x06000831 RID: 2097
		private delegate void ResourceCallback(RenderGraphContext rgContext, IRenderGraphResource res);

		// Token: 0x02000138 RID: 312
		private class RenderGraphResourcesData
		{
			// Token: 0x06000834 RID: 2100 RVA: 0x00022A62 File Offset: 0x00020C62
			public void Clear(bool onException, int frameIndex)
			{
				this.resourceArray.Resize(this.sharedResourcesCount, false);
				this.pool.CheckFrameAllocation(onException, frameIndex);
			}

			// Token: 0x06000835 RID: 2101 RVA: 0x00022A84 File Offset: 0x00020C84
			public unsafe void Cleanup()
			{
				for (int i = 0; i < this.sharedResourcesCount; i++)
				{
					IRenderGraphResource renderGraphResource = *this.resourceArray[i];
					if (renderGraphResource != null)
					{
						renderGraphResource.ReleaseGraphicsResource();
					}
				}
				this.pool.Cleanup();
			}

			// Token: 0x06000836 RID: 2102 RVA: 0x00022AC4 File Offset: 0x00020CC4
			public void PurgeUnusedGraphicsResources(int frameIndex)
			{
				this.pool.PurgeUnusedResources(frameIndex);
			}

			// Token: 0x06000837 RID: 2103 RVA: 0x00022AD4 File Offset: 0x00020CD4
			public unsafe int AddNewRenderGraphResource<ResType>(out ResType outRes, bool pooledResource = true) where ResType : IRenderGraphResource, new()
			{
				int size = this.resourceArray.size;
				this.resourceArray.Resize(this.resourceArray.size + 1, true);
				if (*this.resourceArray[size] == null)
				{
					*this.resourceArray[size] = Activator.CreateInstance<ResType>();
				}
				outRes = ((*this.resourceArray[size]) as ResType);
				outRes.Reset(pooledResource ? this.pool : null);
				return size;
			}

			// Token: 0x06000838 RID: 2104 RVA: 0x00022B62 File Offset: 0x00020D62
			public RenderGraphResourcesData()
			{
			}

			// Token: 0x040004F6 RID: 1270
			public DynamicArray<IRenderGraphResource> resourceArray = new DynamicArray<IRenderGraphResource>();

			// Token: 0x040004F7 RID: 1271
			public int sharedResourcesCount;

			// Token: 0x040004F8 RID: 1272
			public IRenderGraphResourcePool pool;

			// Token: 0x040004F9 RID: 1273
			public RenderGraphResourceRegistry.ResourceCreateCallback createResourceCallback;

			// Token: 0x040004FA RID: 1274
			public RenderGraphResourceRegistry.ResourceCallback releaseResourceCallback;
		}
	}
}
