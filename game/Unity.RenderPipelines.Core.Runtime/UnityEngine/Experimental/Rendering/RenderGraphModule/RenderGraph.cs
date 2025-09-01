using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RendererUtils;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000022 RID: 34
	public class RenderGraph
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00007303 File Offset: 0x00005503
		// (set) Token: 0x060000EF RID: 239 RVA: 0x0000730B File Offset: 0x0000550B
		public string name
		{
			[CompilerGenerated]
			get
			{
				return this.<name>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<name>k__BackingField = value;
			}
		} = "RenderGraph";

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00007314 File Offset: 0x00005514
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x0000731B File Offset: 0x0000551B
		internal static bool requireDebugData
		{
			[CompilerGenerated]
			get
			{
				return RenderGraph.<requireDebugData>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				RenderGraph.<requireDebugData>k__BackingField = value;
			}
		} = false;

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00007323 File Offset: 0x00005523
		public RenderGraphDefaultResources defaultResources
		{
			get
			{
				return this.m_DefaultResources;
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000732C File Offset: 0x0000552C
		public RenderGraph(string name = "RenderGraph")
		{
			this.name = name;
			this.m_Resources = new RenderGraphResourceRegistry(this.m_DebugParameters, this.m_FrameInformationLogger);
			for (int i = 0; i < 2; i++)
			{
				this.m_CompiledResourcesInfos[i] = new DynamicArray<RenderGraph.CompiledResourceInfo>();
			}
			RenderGraph.s_RegisteredGraphs.Add(this);
			RenderGraph.OnGraphRegisteredDelegate onGraphRegisteredDelegate = RenderGraph.onGraphRegistered;
			if (onGraphRegisteredDelegate == null)
			{
				return;
			}
			onGraphRegisteredDelegate(this);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00007431 File Offset: 0x00005631
		public void Cleanup()
		{
			this.m_Resources.Cleanup();
			this.m_DefaultResources.Cleanup();
			this.m_RenderGraphPool.Cleanup();
			RenderGraph.s_RegisteredGraphs.Remove(this);
			RenderGraph.OnGraphRegisteredDelegate onGraphRegisteredDelegate = RenderGraph.onGraphUnregistered;
			if (onGraphRegisteredDelegate == null)
			{
				return;
			}
			onGraphRegisteredDelegate(this);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00007470 File Offset: 0x00005670
		public void RegisterDebug(DebugUI.Panel panel = null)
		{
			this.m_DebugParameters.RegisterDebug(this.name, panel);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00007484 File Offset: 0x00005684
		public void UnRegisterDebug()
		{
			this.m_DebugParameters.UnRegisterDebug(this.name);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00007497 File Offset: 0x00005697
		public static List<RenderGraph> GetRegisteredRenderGraphs()
		{
			return RenderGraph.s_RegisteredGraphs;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000074A0 File Offset: 0x000056A0
		internal RenderGraphDebugData GetDebugData(string executionName)
		{
			RenderGraphDebugData result;
			if (this.m_DebugData.TryGetValue(executionName, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000074C0 File Offset: 0x000056C0
		public void EndFrame()
		{
			this.m_Resources.PurgeUnusedGraphicsResources();
			if (this.m_DebugParameters.logFrameInformation)
			{
				Debug.Log(this.m_FrameInformationLogger.GetAllLogs());
				this.m_DebugParameters.logFrameInformation = false;
			}
			if (this.m_DebugParameters.logResources)
			{
				this.m_Resources.FlushLogs();
				this.m_DebugParameters.logResources = false;
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00007525 File Offset: 0x00005725
		public TextureHandle ImportTexture(RTHandle rt)
		{
			return this.m_Resources.ImportTexture(rt);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00007533 File Offset: 0x00005733
		public TextureHandle ImportBackbuffer(RenderTargetIdentifier rt)
		{
			return this.m_Resources.ImportBackbuffer(rt);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00007541 File Offset: 0x00005741
		public TextureHandle CreateTexture(in TextureDesc desc)
		{
			return this.m_Resources.CreateTexture(desc, -1);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00007550 File Offset: 0x00005750
		public TextureHandle CreateSharedTexture(in TextureDesc desc, bool explicitRelease = false)
		{
			if (this.m_HasRenderGraphBegun)
			{
				throw new InvalidOperationException("A shared texture can only be created outside of render graph execution.");
			}
			return this.m_Resources.CreateSharedTexture(desc, explicitRelease);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00007572 File Offset: 0x00005772
		public void ReleaseSharedTexture(TextureHandle texture)
		{
			if (this.m_HasRenderGraphBegun)
			{
				throw new InvalidOperationException("A shared texture can only be release outside of render graph execution.");
			}
			this.m_Resources.ReleaseSharedTexture(texture);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00007594 File Offset: 0x00005794
		public TextureHandle CreateTexture(TextureHandle texture)
		{
			RenderGraphResourceRegistry resources = this.m_Resources;
			TextureDesc textureResourceDesc = this.m_Resources.GetTextureResourceDesc(texture.handle);
			return resources.CreateTexture(textureResourceDesc, -1);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000075C2 File Offset: 0x000057C2
		public void CreateTextureIfInvalid(in TextureDesc desc, ref TextureHandle texture)
		{
			if (!texture.IsValid())
			{
				texture = this.m_Resources.CreateTexture(desc, -1);
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000075DF File Offset: 0x000057DF
		public TextureDesc GetTextureDesc(TextureHandle texture)
		{
			return this.m_Resources.GetTextureResourceDesc(texture.handle);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000075F3 File Offset: 0x000057F3
		public RendererListHandle CreateRendererList(in RendererListDesc desc)
		{
			return this.m_Resources.CreateRendererList(desc);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00007601 File Offset: 0x00005801
		public ComputeBufferHandle ImportComputeBuffer(ComputeBuffer computeBuffer)
		{
			return this.m_Resources.ImportComputeBuffer(computeBuffer);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000760F File Offset: 0x0000580F
		public ComputeBufferHandle CreateComputeBuffer(in ComputeBufferDesc desc)
		{
			return this.m_Resources.CreateComputeBuffer(desc, -1);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00007620 File Offset: 0x00005820
		public ComputeBufferHandle CreateComputeBuffer(in ComputeBufferHandle computeBuffer)
		{
			RenderGraphResourceRegistry resources = this.m_Resources;
			ComputeBufferDesc computeBufferResourceDesc = this.m_Resources.GetComputeBufferResourceDesc(computeBuffer.handle);
			return resources.CreateComputeBuffer(computeBufferResourceDesc, -1);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000764D File Offset: 0x0000584D
		public ComputeBufferDesc GetComputeBufferDesc(in ComputeBufferHandle computeBuffer)
		{
			return this.m_Resources.GetComputeBufferResourceDesc(computeBuffer.handle);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00007660 File Offset: 0x00005860
		public RenderGraphBuilder AddRenderPass<PassData>(string passName, out PassData passData, ProfilingSampler sampler) where PassData : class, new()
		{
			RenderGraphPass<PassData> renderGraphPass = this.m_RenderGraphPool.Get<RenderGraphPass<PassData>>();
			renderGraphPass.Initialize(this.m_RenderPasses.Count, this.m_RenderGraphPool.Get<PassData>(), passName, sampler);
			passData = renderGraphPass.data;
			this.m_RenderPasses.Add(renderGraphPass);
			return new RenderGraphBuilder(renderGraphPass, this.m_Resources, this);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000076BC File Offset: 0x000058BC
		public RenderGraphBuilder AddRenderPass<PassData>(string passName, out PassData passData) where PassData : class, new()
		{
			return this.AddRenderPass<PassData>(passName, out passData, this.GetDefaultProfilingSampler(passName));
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000076D0 File Offset: 0x000058D0
		public RenderGraphExecution RecordAndExecute(in RenderGraphParameters parameters)
		{
			this.m_CurrentFrameIndex = parameters.currentFrameIndex;
			this.m_CurrentExecutionName = ((parameters.executionName != null) ? parameters.executionName : "RenderGraphExecution");
			this.m_HasRenderGraphBegun = true;
			RenderGraphResourceRegistry resources = this.m_Resources;
			int executionCount = this.m_ExecutionCount;
			this.m_ExecutionCount = executionCount + 1;
			resources.BeginRenderGraph(executionCount);
			if (this.m_DebugParameters.enableLogging)
			{
				this.m_FrameInformationLogger.Initialize(this.m_CurrentExecutionName);
			}
			this.m_DefaultResources.InitializeForRendering(this);
			this.m_RenderGraphContext.cmd = parameters.commandBuffer;
			this.m_RenderGraphContext.renderContext = parameters.scriptableRenderContext;
			this.m_RenderGraphContext.renderGraphPool = this.m_RenderGraphPool;
			this.m_RenderGraphContext.defaultResources = this.m_DefaultResources;
			if (this.m_DebugParameters.immediateMode)
			{
				this.LogFrameInformation();
				this.m_CompiledPassInfos.Resize(this.m_CompiledPassInfos.capacity, false);
				this.m_CurrentImmediatePassIndex = 0;
				for (int i = 0; i < 2; i++)
				{
					if (this.m_ImmediateModeResourceList[i] == null)
					{
						this.m_ImmediateModeResourceList[i] = new List<int>();
					}
					this.m_ImmediateModeResourceList[i].Clear();
				}
				this.m_Resources.BeginExecute(this.m_CurrentFrameIndex);
			}
			return new RenderGraphExecution(this);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00007810 File Offset: 0x00005A10
		internal void Execute()
		{
			this.m_ExecutionExceptionWasRaised = false;
			try
			{
				if (this.m_RenderGraphContext.cmd == null)
				{
					throw new InvalidOperationException("RenderGraph.RecordAndExecute was not called before executing the render graph.");
				}
				if (!this.m_DebugParameters.immediateMode)
				{
					this.LogFrameInformation();
					this.CompileRenderGraph();
					this.m_Resources.BeginExecute(this.m_CurrentFrameIndex);
					this.ExecuteRenderGraph();
				}
			}
			catch (Exception exception)
			{
				Debug.LogError("Render Graph Execution error");
				if (!this.m_ExecutionExceptionWasRaised)
				{
					Debug.LogException(exception);
				}
				this.m_ExecutionExceptionWasRaised = true;
			}
			finally
			{
				this.GenerateDebugData();
				if (this.m_DebugParameters.immediateMode)
				{
					this.ReleaseImmediateModeResources();
				}
				this.ClearCompiledGraph();
				this.m_Resources.EndExecute();
				this.InvalidateContext();
				this.m_HasRenderGraphBegun = false;
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000078E4 File Offset: 0x00005AE4
		public void BeginProfilingSampler(ProfilingSampler sampler)
		{
			RenderGraph.ProfilingScopePassData profilingScopePassData;
			using (RenderGraphBuilder renderGraphBuilder = this.AddRenderPass<RenderGraph.ProfilingScopePassData>("BeginProfile", out profilingScopePassData, null))
			{
				profilingScopePassData.sampler = sampler;
				renderGraphBuilder.AllowPassCulling(false);
				renderGraphBuilder.GenerateDebugData(false);
				renderGraphBuilder.SetRenderFunc<RenderGraph.ProfilingScopePassData>(delegate(RenderGraph.ProfilingScopePassData data, RenderGraphContext ctx)
				{
					data.sampler.Begin(ctx.cmd);
				});
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00007960 File Offset: 0x00005B60
		public void EndProfilingSampler(ProfilingSampler sampler)
		{
			RenderGraph.ProfilingScopePassData profilingScopePassData;
			using (RenderGraphBuilder renderGraphBuilder = this.AddRenderPass<RenderGraph.ProfilingScopePassData>("EndProfile", out profilingScopePassData, null))
			{
				profilingScopePassData.sampler = sampler;
				renderGraphBuilder.AllowPassCulling(false);
				renderGraphBuilder.GenerateDebugData(false);
				renderGraphBuilder.SetRenderFunc<RenderGraph.ProfilingScopePassData>(delegate(RenderGraph.ProfilingScopePassData data, RenderGraphContext ctx)
				{
					data.sampler.End(ctx.cmd);
				});
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000079DC File Offset: 0x00005BDC
		internal DynamicArray<RenderGraph.CompiledPassInfo> GetCompiledPassInfos()
		{
			return this.m_CompiledPassInfos;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000079E4 File Offset: 0x00005BE4
		internal void ClearCompiledGraph()
		{
			this.ClearRenderPasses();
			this.m_Resources.Clear(this.m_ExecutionExceptionWasRaised);
			this.m_RendererLists.Clear();
			for (int i = 0; i < 2; i++)
			{
				this.m_CompiledResourcesInfos[i].Clear();
			}
			this.m_CompiledPassInfos.Clear();
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00007A37 File Offset: 0x00005C37
		private void InvalidateContext()
		{
			this.m_RenderGraphContext.cmd = null;
			this.m_RenderGraphContext.renderGraphPool = null;
			this.m_RenderGraphContext.defaultResources = null;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00007A5D File Offset: 0x00005C5D
		internal void OnPassAdded(RenderGraphPass pass)
		{
			if (this.m_DebugParameters.immediateMode)
			{
				this.ExecutePassImmediatly(pass);
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000111 RID: 273 RVA: 0x00007A74 File Offset: 0x00005C74
		// (remove) Token: 0x06000112 RID: 274 RVA: 0x00007AA8 File Offset: 0x00005CA8
		internal static event RenderGraph.OnGraphRegisteredDelegate onGraphRegistered
		{
			[CompilerGenerated]
			add
			{
				RenderGraph.OnGraphRegisteredDelegate onGraphRegisteredDelegate = RenderGraph.onGraphRegistered;
				RenderGraph.OnGraphRegisteredDelegate onGraphRegisteredDelegate2;
				do
				{
					onGraphRegisteredDelegate2 = onGraphRegisteredDelegate;
					RenderGraph.OnGraphRegisteredDelegate value2 = (RenderGraph.OnGraphRegisteredDelegate)Delegate.Combine(onGraphRegisteredDelegate2, value);
					onGraphRegisteredDelegate = Interlocked.CompareExchange<RenderGraph.OnGraphRegisteredDelegate>(ref RenderGraph.onGraphRegistered, value2, onGraphRegisteredDelegate2);
				}
				while (onGraphRegisteredDelegate != onGraphRegisteredDelegate2);
			}
			[CompilerGenerated]
			remove
			{
				RenderGraph.OnGraphRegisteredDelegate onGraphRegisteredDelegate = RenderGraph.onGraphRegistered;
				RenderGraph.OnGraphRegisteredDelegate onGraphRegisteredDelegate2;
				do
				{
					onGraphRegisteredDelegate2 = onGraphRegisteredDelegate;
					RenderGraph.OnGraphRegisteredDelegate value2 = (RenderGraph.OnGraphRegisteredDelegate)Delegate.Remove(onGraphRegisteredDelegate2, value);
					onGraphRegisteredDelegate = Interlocked.CompareExchange<RenderGraph.OnGraphRegisteredDelegate>(ref RenderGraph.onGraphRegistered, value2, onGraphRegisteredDelegate2);
				}
				while (onGraphRegisteredDelegate != onGraphRegisteredDelegate2);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000113 RID: 275 RVA: 0x00007ADC File Offset: 0x00005CDC
		// (remove) Token: 0x06000114 RID: 276 RVA: 0x00007B10 File Offset: 0x00005D10
		internal static event RenderGraph.OnGraphRegisteredDelegate onGraphUnregistered
		{
			[CompilerGenerated]
			add
			{
				RenderGraph.OnGraphRegisteredDelegate onGraphRegisteredDelegate = RenderGraph.onGraphUnregistered;
				RenderGraph.OnGraphRegisteredDelegate onGraphRegisteredDelegate2;
				do
				{
					onGraphRegisteredDelegate2 = onGraphRegisteredDelegate;
					RenderGraph.OnGraphRegisteredDelegate value2 = (RenderGraph.OnGraphRegisteredDelegate)Delegate.Combine(onGraphRegisteredDelegate2, value);
					onGraphRegisteredDelegate = Interlocked.CompareExchange<RenderGraph.OnGraphRegisteredDelegate>(ref RenderGraph.onGraphUnregistered, value2, onGraphRegisteredDelegate2);
				}
				while (onGraphRegisteredDelegate != onGraphRegisteredDelegate2);
			}
			[CompilerGenerated]
			remove
			{
				RenderGraph.OnGraphRegisteredDelegate onGraphRegisteredDelegate = RenderGraph.onGraphUnregistered;
				RenderGraph.OnGraphRegisteredDelegate onGraphRegisteredDelegate2;
				do
				{
					onGraphRegisteredDelegate2 = onGraphRegisteredDelegate;
					RenderGraph.OnGraphRegisteredDelegate value2 = (RenderGraph.OnGraphRegisteredDelegate)Delegate.Remove(onGraphRegisteredDelegate2, value);
					onGraphRegisteredDelegate = Interlocked.CompareExchange<RenderGraph.OnGraphRegisteredDelegate>(ref RenderGraph.onGraphUnregistered, value2, onGraphRegisteredDelegate2);
				}
				while (onGraphRegisteredDelegate != onGraphRegisteredDelegate2);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000115 RID: 277 RVA: 0x00007B44 File Offset: 0x00005D44
		// (remove) Token: 0x06000116 RID: 278 RVA: 0x00007B78 File Offset: 0x00005D78
		internal static event RenderGraph.OnExecutionRegisteredDelegate onExecutionRegistered
		{
			[CompilerGenerated]
			add
			{
				RenderGraph.OnExecutionRegisteredDelegate onExecutionRegisteredDelegate = RenderGraph.onExecutionRegistered;
				RenderGraph.OnExecutionRegisteredDelegate onExecutionRegisteredDelegate2;
				do
				{
					onExecutionRegisteredDelegate2 = onExecutionRegisteredDelegate;
					RenderGraph.OnExecutionRegisteredDelegate value2 = (RenderGraph.OnExecutionRegisteredDelegate)Delegate.Combine(onExecutionRegisteredDelegate2, value);
					onExecutionRegisteredDelegate = Interlocked.CompareExchange<RenderGraph.OnExecutionRegisteredDelegate>(ref RenderGraph.onExecutionRegistered, value2, onExecutionRegisteredDelegate2);
				}
				while (onExecutionRegisteredDelegate != onExecutionRegisteredDelegate2);
			}
			[CompilerGenerated]
			remove
			{
				RenderGraph.OnExecutionRegisteredDelegate onExecutionRegisteredDelegate = RenderGraph.onExecutionRegistered;
				RenderGraph.OnExecutionRegisteredDelegate onExecutionRegisteredDelegate2;
				do
				{
					onExecutionRegisteredDelegate2 = onExecutionRegisteredDelegate;
					RenderGraph.OnExecutionRegisteredDelegate value2 = (RenderGraph.OnExecutionRegisteredDelegate)Delegate.Remove(onExecutionRegisteredDelegate2, value);
					onExecutionRegisteredDelegate = Interlocked.CompareExchange<RenderGraph.OnExecutionRegisteredDelegate>(ref RenderGraph.onExecutionRegistered, value2, onExecutionRegisteredDelegate2);
				}
				while (onExecutionRegisteredDelegate != onExecutionRegisteredDelegate2);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000117 RID: 279 RVA: 0x00007BAC File Offset: 0x00005DAC
		// (remove) Token: 0x06000118 RID: 280 RVA: 0x00007BE0 File Offset: 0x00005DE0
		internal static event RenderGraph.OnExecutionRegisteredDelegate onExecutionUnregistered
		{
			[CompilerGenerated]
			add
			{
				RenderGraph.OnExecutionRegisteredDelegate onExecutionRegisteredDelegate = RenderGraph.onExecutionUnregistered;
				RenderGraph.OnExecutionRegisteredDelegate onExecutionRegisteredDelegate2;
				do
				{
					onExecutionRegisteredDelegate2 = onExecutionRegisteredDelegate;
					RenderGraph.OnExecutionRegisteredDelegate value2 = (RenderGraph.OnExecutionRegisteredDelegate)Delegate.Combine(onExecutionRegisteredDelegate2, value);
					onExecutionRegisteredDelegate = Interlocked.CompareExchange<RenderGraph.OnExecutionRegisteredDelegate>(ref RenderGraph.onExecutionUnregistered, value2, onExecutionRegisteredDelegate2);
				}
				while (onExecutionRegisteredDelegate != onExecutionRegisteredDelegate2);
			}
			[CompilerGenerated]
			remove
			{
				RenderGraph.OnExecutionRegisteredDelegate onExecutionRegisteredDelegate = RenderGraph.onExecutionUnregistered;
				RenderGraph.OnExecutionRegisteredDelegate onExecutionRegisteredDelegate2;
				do
				{
					onExecutionRegisteredDelegate2 = onExecutionRegisteredDelegate;
					RenderGraph.OnExecutionRegisteredDelegate value2 = (RenderGraph.OnExecutionRegisteredDelegate)Delegate.Remove(onExecutionRegisteredDelegate2, value);
					onExecutionRegisteredDelegate = Interlocked.CompareExchange<RenderGraph.OnExecutionRegisteredDelegate>(ref RenderGraph.onExecutionUnregistered, value2, onExecutionRegisteredDelegate2);
				}
				while (onExecutionRegisteredDelegate != onExecutionRegisteredDelegate2);
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00007C14 File Offset: 0x00005E14
		private void InitResourceInfosData(DynamicArray<RenderGraph.CompiledResourceInfo> resourceInfos, int count)
		{
			resourceInfos.Resize(count, false);
			for (int i = 0; i < resourceInfos.size; i++)
			{
				resourceInfos[i].Reset();
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00007C48 File Offset: 0x00005E48
		private void InitializeCompilationData()
		{
			this.InitResourceInfosData(this.m_CompiledResourcesInfos[0], this.m_Resources.GetTextureResourceCount());
			this.InitResourceInfosData(this.m_CompiledResourcesInfos[1], this.m_Resources.GetComputeBufferResourceCount());
			this.m_CompiledPassInfos.Resize(this.m_RenderPasses.Count, false);
			for (int i = 0; i < this.m_CompiledPassInfos.size; i++)
			{
				this.m_CompiledPassInfos[i].Reset(this.m_RenderPasses[i]);
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00007CD4 File Offset: 0x00005ED4
		private void CountReferences()
		{
			for (int i = 0; i < this.m_CompiledPassInfos.size; i++)
			{
				ref RenderGraph.CompiledPassInfo ptr = ref this.m_CompiledPassInfos[i];
				for (int j = 0; j < 2; j++)
				{
					foreach (ResourceHandle handle in ptr.pass.resourceReadLists[j])
					{
						ref RenderGraph.CompiledResourceInfo ptr2 = ref this.m_CompiledResourcesInfos[j][handle];
						ptr2.imported = this.m_Resources.IsRenderGraphResourceImported(handle);
						ptr2.consumers.Add(i);
						ptr2.refCount++;
					}
					foreach (ResourceHandle handle2 in ptr.pass.resourceWriteLists[j])
					{
						ref RenderGraph.CompiledResourceInfo ptr3 = ref this.m_CompiledResourcesInfos[j][handle2];
						ptr3.imported = this.m_Resources.IsRenderGraphResourceImported(handle2);
						ptr3.producers.Add(i);
						ptr.hasSideEffect = ptr3.imported;
						ptr.refCount++;
					}
					foreach (ResourceHandle handle3 in ptr.pass.transientResourceList[j])
					{
						int index = handle3;
						ref RenderGraph.CompiledResourceInfo ptr4 = ref this.m_CompiledResourcesInfos[j][index];
						ptr4.refCount++;
						ptr4.consumers.Add(i);
						ptr4.producers.Add(i);
					}
				}
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00007EAC File Offset: 0x000060AC
		private unsafe void CullUnusedPasses()
		{
			if (this.m_DebugParameters.disablePassCulling)
			{
				if (this.m_DebugParameters.enableLogging)
				{
					this.m_FrameInformationLogger.LogLine("- Pass Culling Disabled -\n", Array.Empty<object>());
				}
				return;
			}
			for (int i = 0; i < 2; i++)
			{
				DynamicArray<RenderGraph.CompiledResourceInfo> dynamicArray = this.m_CompiledResourcesInfos[i];
				this.m_CullingStack.Clear();
				for (int j = 0; j < dynamicArray.size; j++)
				{
					if (dynamicArray[j].refCount == 0)
					{
						this.m_CullingStack.Push(j);
					}
				}
				while (this.m_CullingStack.Count != 0)
				{
					foreach (int index in dynamicArray[this.m_CullingStack.Pop()]->producers)
					{
						ref RenderGraph.CompiledPassInfo ptr = ref this.m_CompiledPassInfos[index];
						ptr.refCount--;
						if (ptr.refCount == 0 && !ptr.hasSideEffect && ptr.allowPassCulling)
						{
							ptr.culled = true;
							foreach (ResourceHandle handle in ptr.pass.resourceReadLists[i])
							{
								ref RenderGraph.CompiledResourceInfo ptr2 = ref dynamicArray[handle];
								ptr2.refCount--;
								if (ptr2.refCount == 0)
								{
									this.m_CullingStack.Push(handle);
								}
							}
						}
					}
				}
			}
			this.LogCulledPasses();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00008068 File Offset: 0x00006268
		private void UpdatePassSynchronization(ref RenderGraph.CompiledPassInfo currentPassInfo, ref RenderGraph.CompiledPassInfo producerPassInfo, int currentPassIndex, int lastProducer, ref int intLastSyncIndex)
		{
			currentPassInfo.syncToPassIndex = lastProducer;
			intLastSyncIndex = lastProducer;
			producerPassInfo.needGraphicsFence = true;
			if (producerPassInfo.syncFromPassIndex == -1)
			{
				producerPassInfo.syncFromPassIndex = currentPassIndex;
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00008090 File Offset: 0x00006290
		private void UpdateResourceSynchronization(ref int lastGraphicsPipeSync, ref int lastComputePipeSync, int currentPassIndex, in RenderGraph.CompiledResourceInfo resource)
		{
			int latestProducerIndex = this.GetLatestProducerIndex(currentPassIndex, resource);
			if (latestProducerIndex != -1)
			{
				ref RenderGraph.CompiledPassInfo ptr = ref this.m_CompiledPassInfos[currentPassIndex];
				if (this.m_CompiledPassInfos[latestProducerIndex].enableAsyncCompute != ptr.enableAsyncCompute)
				{
					if (ptr.enableAsyncCompute)
					{
						if (latestProducerIndex > lastGraphicsPipeSync)
						{
							this.UpdatePassSynchronization(ref ptr, this.m_CompiledPassInfos[latestProducerIndex], currentPassIndex, latestProducerIndex, ref lastGraphicsPipeSync);
							return;
						}
					}
					else if (latestProducerIndex > lastComputePipeSync)
					{
						this.UpdatePassSynchronization(ref ptr, this.m_CompiledPassInfos[latestProducerIndex], currentPassIndex, latestProducerIndex, ref lastComputePipeSync);
					}
				}
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00008110 File Offset: 0x00006310
		private int GetLatestProducerIndex(int passIndex, in RenderGraph.CompiledResourceInfo info)
		{
			int result = -1;
			foreach (int num in info.producers)
			{
				if (num >= passIndex)
				{
					return result;
				}
				result = num;
			}
			return result;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00008170 File Offset: 0x00006370
		private int GetLatestValidReadIndex(in RenderGraph.CompiledResourceInfo info)
		{
			if (info.consumers.Count == 0)
			{
				return -1;
			}
			List<int> consumers = info.consumers;
			for (int i = consumers.Count - 1; i >= 0; i--)
			{
				if (!this.m_CompiledPassInfos[consumers[i]].culled)
				{
					return consumers[i];
				}
			}
			return -1;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000081C8 File Offset: 0x000063C8
		private int GetFirstValidWriteIndex(in RenderGraph.CompiledResourceInfo info)
		{
			if (info.producers.Count == 0)
			{
				return -1;
			}
			List<int> producers = info.producers;
			for (int i = 0; i < producers.Count; i++)
			{
				if (!this.m_CompiledPassInfos[producers[i]].culled)
				{
					return producers[i];
				}
			}
			return -1;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00008220 File Offset: 0x00006420
		private int GetLatestValidWriteIndex(in RenderGraph.CompiledResourceInfo info)
		{
			if (info.producers.Count == 0)
			{
				return -1;
			}
			List<int> producers = info.producers;
			for (int i = producers.Count - 1; i >= 0; i--)
			{
				if (!this.m_CompiledPassInfos[producers[i]].culled)
				{
					return producers[i];
				}
			}
			return -1;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00008278 File Offset: 0x00006478
		private void CreateRendererLists()
		{
			for (int i = 0; i < this.m_CompiledPassInfos.size; i++)
			{
				ref RenderGraph.CompiledPassInfo ptr = ref this.m_CompiledPassInfos[i];
				if (!ptr.culled)
				{
					this.m_RendererLists.AddRange(ptr.pass.usedRendererListList);
				}
			}
			this.m_Resources.CreateRendererLists(this.m_RendererLists, this.m_RenderGraphContext.renderContext, this.m_RendererListCulling);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000082E8 File Offset: 0x000064E8
		private unsafe void UpdateResourceAllocationAndSynchronization()
		{
			int num = -1;
			int num2 = -1;
			for (int i = 0; i < this.m_CompiledPassInfos.size; i++)
			{
				ref RenderGraph.CompiledPassInfo ptr = ref this.m_CompiledPassInfos[i];
				if (!ptr.culled)
				{
					for (int j = 0; j < 2; j++)
					{
						DynamicArray<RenderGraph.CompiledResourceInfo> dynamicArray = this.m_CompiledResourcesInfos[j];
						foreach (ResourceHandle handle in ptr.pass.resourceReadLists[j])
						{
							int index = handle;
							this.UpdateResourceSynchronization(ref num, ref num2, i, dynamicArray[index]);
						}
						foreach (ResourceHandle handle2 in ptr.pass.resourceWriteLists[j])
						{
							int index2 = handle2;
							this.UpdateResourceSynchronization(ref num, ref num2, i, dynamicArray[index2]);
						}
					}
				}
			}
			for (int k = 0; k < 2; k++)
			{
				DynamicArray<RenderGraph.CompiledResourceInfo> dynamicArray2 = this.m_CompiledResourcesInfos[k];
				for (int l = 0; l < dynamicArray2.size; l++)
				{
					RenderGraph.CompiledResourceInfo compiledResourceInfo = *dynamicArray2[l];
					bool flag = this.m_Resources.IsRenderGraphResourceShared((RenderGraphResourceType)k, l);
					if (!compiledResourceInfo.imported || flag)
					{
						int firstValidWriteIndex = this.GetFirstValidWriteIndex(compiledResourceInfo);
						if (firstValidWriteIndex != -1)
						{
							this.m_CompiledPassInfos[firstValidWriteIndex].resourceCreateList[k].Add(l);
						}
						int latestValidReadIndex = this.GetLatestValidReadIndex(compiledResourceInfo);
						int latestValidWriteIndex = this.GetLatestValidWriteIndex(compiledResourceInfo);
						int num3 = (firstValidWriteIndex != -1 || compiledResourceInfo.imported) ? Math.Max(latestValidWriteIndex, latestValidReadIndex) : -1;
						if (num3 != -1)
						{
							if (this.m_CompiledPassInfos[num3].enableAsyncCompute)
							{
								int num4 = num3;
								int num5 = this.m_CompiledPassInfos[num4].syncFromPassIndex;
								while (num5 == -1 && num4++ < this.m_CompiledPassInfos.size - 1)
								{
									if (this.m_CompiledPassInfos[num4].enableAsyncCompute)
									{
										num5 = this.m_CompiledPassInfos[num4].syncFromPassIndex;
									}
								}
								if (num4 == this.m_CompiledPassInfos.size)
								{
									if (!this.m_CompiledPassInfos[num3].hasSideEffect)
									{
										RenderGraphPass renderGraphPass = this.m_RenderPasses[num3];
										string arg = "<unknown>";
										throw new InvalidOperationException(string.Format("{0} resource '{1}' in asynchronous pass '{2}' is missing synchronization on the graphics pipeline.", (RenderGraphResourceType)k, arg, renderGraphPass.name));
									}
									num5 = num4;
								}
								int num6 = Math.Max(0, num5 - 1);
								while (this.m_CompiledPassInfos[num6].culled)
								{
									num6 = Math.Max(0, num6 - 1);
								}
								this.m_CompiledPassInfos[num6].resourceReleaseList[k].Add(l);
							}
							else
							{
								this.m_CompiledPassInfos[num3].resourceReleaseList[k].Add(l);
							}
						}
						if (flag && (firstValidWriteIndex != -1 || num3 != -1))
						{
							this.m_Resources.UpdateSharedResourceLastFrameIndex(k, l);
						}
					}
				}
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000863C File Offset: 0x0000683C
		private bool AreRendererListsEmpty(List<RendererListHandle> rendererLists)
		{
			foreach (RendererListHandle rendererListHandle in rendererLists)
			{
				RendererList rendererList = this.m_Resources.GetRendererList(rendererListHandle);
				if (this.m_RenderGraphContext.renderContext.QueryRendererListStatus(rendererList) == RendererListStatus.kRendererListPopulated)
				{
					return false;
				}
			}
			return rendererLists.Count > 0;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000086B8 File Offset: 0x000068B8
		private void TryCullPassAtIndex(int passIndex)
		{
			RenderGraphPass pass = this.m_CompiledPassInfos[passIndex].pass;
			if (!this.m_CompiledPassInfos[passIndex].culled && pass.allowPassCulling && pass.allowRendererListCulling && !this.m_CompiledPassInfos[passIndex].hasSideEffect && (this.AreRendererListsEmpty(pass.usedRendererListList) || this.AreRendererListsEmpty(pass.dependsOnRendererListList)))
			{
				this.m_CompiledPassInfos[passIndex].culled = true;
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000873C File Offset: 0x0000693C
		private void CullRendererLists()
		{
			for (int i = 0; i < this.m_CompiledPassInfos.size; i++)
			{
				if (!this.m_CompiledPassInfos[i].culled && !this.m_CompiledPassInfos[i].hasSideEffect)
				{
					RenderGraphPass pass = this.m_CompiledPassInfos[i].pass;
					if (pass.usedRendererListList.Count > 0 || pass.dependsOnRendererListList.Count > 0)
					{
						this.TryCullPassAtIndex(i);
					}
				}
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000087BC File Offset: 0x000069BC
		internal void CompileRenderGraph()
		{
			using (new ProfilingScope(this.m_RenderGraphContext.cmd, ProfilingSampler.Get<RenderGraphProfileId>(RenderGraphProfileId.CompileRenderGraph)))
			{
				this.InitializeCompilationData();
				this.CountReferences();
				this.CullUnusedPasses();
				this.CreateRendererLists();
				if (this.m_RendererListCulling)
				{
					this.CullRendererLists();
				}
				this.UpdateResourceAllocationAndSynchronization();
				this.LogRendererListsCreation();
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00008834 File Offset: 0x00006A34
		private ref RenderGraph.CompiledPassInfo CompilePassImmediatly(RenderGraphPass pass)
		{
			if (this.m_CurrentImmediatePassIndex >= this.m_CompiledPassInfos.size)
			{
				this.m_CompiledPassInfos.Resize(this.m_CompiledPassInfos.size * 2, false);
			}
			DynamicArray<RenderGraph.CompiledPassInfo> compiledPassInfos = this.m_CompiledPassInfos;
			int currentImmediatePassIndex = this.m_CurrentImmediatePassIndex;
			this.m_CurrentImmediatePassIndex = currentImmediatePassIndex + 1;
			ref RenderGraph.CompiledPassInfo ptr = ref compiledPassInfos[currentImmediatePassIndex];
			ptr.Reset(pass);
			ptr.enableAsyncCompute = false;
			for (int i = 0; i < 2; i++)
			{
				foreach (ResourceHandle handle in pass.resourceWriteLists[i])
				{
					if (!this.m_Resources.IsGraphicsResourceCreated(handle))
					{
						ptr.resourceCreateList[i].Add(handle);
						this.m_ImmediateModeResourceList[i].Add(handle);
					}
				}
				foreach (ResourceHandle handle2 in pass.transientResourceList[i])
				{
					ptr.resourceCreateList[i].Add(handle2);
					ptr.resourceReleaseList[i].Add(handle2);
				}
				foreach (ResourceHandle resourceHandle in pass.resourceReadLists[i])
				{
				}
			}
			foreach (RendererListHandle item in pass.usedRendererListList)
			{
				if (!this.m_Resources.IsRendererListCreated(item))
				{
					this.m_RendererLists.Add(item);
				}
			}
			this.m_Resources.CreateRendererLists(this.m_RendererLists, this.m_RenderGraphContext.renderContext, false);
			this.m_RendererLists.Clear();
			return ref ptr;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00008A48 File Offset: 0x00006C48
		private void ExecutePassImmediatly(RenderGraphPass pass)
		{
			this.ExecuteCompiledPass(this.CompilePassImmediatly(pass), this.m_CurrentImmediatePassIndex - 1);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00008A60 File Offset: 0x00006C60
		private void ExecuteCompiledPass(ref RenderGraph.CompiledPassInfo passInfo, int passIndex)
		{
			if (passInfo.culled)
			{
				return;
			}
			if (!passInfo.pass.HasRenderFunc())
			{
				throw new InvalidOperationException(string.Format("RenderPass {0} was not provided with an execute function.", passInfo.pass.name));
			}
			try
			{
				using (new ProfilingScope(this.m_RenderGraphContext.cmd, passInfo.pass.customSampler))
				{
					this.LogRenderPassBegin(passInfo);
					using (new RenderGraphLogIndent(this.m_FrameInformationLogger, 1))
					{
						this.PreRenderPassExecute(passInfo, this.m_RenderGraphContext);
						passInfo.pass.Execute(this.m_RenderGraphContext);
						this.PostRenderPassExecute(ref passInfo, this.m_RenderGraphContext);
					}
				}
			}
			catch (Exception exception)
			{
				this.m_ExecutionExceptionWasRaised = true;
				Debug.LogError(string.Format("Render Graph Execution error at pass {0} ({1})", passInfo.pass.name, passIndex));
				Debug.LogException(exception);
				throw;
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00008B74 File Offset: 0x00006D74
		private void ExecuteRenderGraph()
		{
			using (new ProfilingScope(this.m_RenderGraphContext.cmd, ProfilingSampler.Get<RenderGraphProfileId>(RenderGraphProfileId.ExecuteRenderGraph)))
			{
				for (int i = 0; i < this.m_CompiledPassInfos.size; i++)
				{
					this.ExecuteCompiledPass(this.m_CompiledPassInfos[i], i);
				}
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00008BE4 File Offset: 0x00006DE4
		private void PreRenderPassSetRenderTargets(in RenderGraph.CompiledPassInfo passInfo, RenderGraphContext rgContext)
		{
			RenderGraphPass pass = passInfo.pass;
			TextureHandle depthBuffer = pass.depthBuffer;
			if (depthBuffer.IsValid() || pass.colorBufferMaxIndex != -1)
			{
				RenderTargetIdentifier[] tempArray = rgContext.renderGraphPool.GetTempArray<RenderTargetIdentifier>(pass.colorBufferMaxIndex + 1);
				TextureHandle[] colorBuffers = pass.colorBuffers;
				if (pass.colorBufferMaxIndex > 0)
				{
					for (int i = 0; i <= pass.colorBufferMaxIndex; i++)
					{
						if (!colorBuffers[i].IsValid())
						{
							throw new InvalidOperationException("MRT setup is invalid. Some indices are not used.");
						}
						tempArray[i] = this.m_Resources.GetTexture(colorBuffers[i]);
					}
					depthBuffer = pass.depthBuffer;
					if (depthBuffer.IsValid())
					{
						CommandBuffer cmd = rgContext.cmd;
						RenderTargetIdentifier[] colorBuffers2 = tempArray;
						RenderGraphResourceRegistry resources = this.m_Resources;
						depthBuffer = pass.depthBuffer;
						CoreUtils.SetRenderTarget(cmd, colorBuffers2, resources.GetTexture(depthBuffer));
						return;
					}
					throw new InvalidOperationException("Setting MRTs without a depth buffer is not supported.");
				}
				else
				{
					depthBuffer = pass.depthBuffer;
					if (depthBuffer.IsValid())
					{
						if (pass.colorBufferMaxIndex > -1)
						{
							CommandBuffer cmd2 = rgContext.cmd;
							RTHandle texture = this.m_Resources.GetTexture(pass.colorBuffers[0]);
							RenderGraphResourceRegistry resources2 = this.m_Resources;
							depthBuffer = pass.depthBuffer;
							CoreUtils.SetRenderTarget(cmd2, texture, resources2.GetTexture(depthBuffer), 0, CubemapFace.Unknown, -1);
							return;
						}
						CommandBuffer cmd3 = rgContext.cmd;
						RenderGraphResourceRegistry resources3 = this.m_Resources;
						depthBuffer = pass.depthBuffer;
						CoreUtils.SetRenderTarget(cmd3, resources3.GetTexture(depthBuffer), ClearFlag.None, 0, CubemapFace.Unknown, -1);
						return;
					}
					else
					{
						CoreUtils.SetRenderTarget(rgContext.cmd, this.m_Resources.GetTexture(pass.colorBuffers[0]), ClearFlag.None, 0, CubemapFace.Unknown, -1);
					}
				}
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00008D64 File Offset: 0x00006F64
		private void PreRenderPassExecute(in RenderGraph.CompiledPassInfo passInfo, RenderGraphContext rgContext)
		{
			RenderGraphPass pass = passInfo.pass;
			this.m_PreviousCommandBuffer = rgContext.cmd;
			bool flag = false;
			for (int i = 0; i < 2; i++)
			{
				foreach (int index in passInfo.resourceCreateList[i])
				{
					flag |= this.m_Resources.CreatePooledResource(rgContext, i, index);
				}
			}
			this.PreRenderPassSetRenderTargets(passInfo, rgContext);
			if (passInfo.enableAsyncCompute)
			{
				GraphicsFence fence = default(GraphicsFence);
				if (flag)
				{
					fence = rgContext.cmd.CreateGraphicsFence(GraphicsFenceType.AsyncQueueSynchronisation, SynchronisationStageFlags.AllGPUOperations);
				}
				rgContext.renderContext.ExecuteCommandBuffer(rgContext.cmd);
				rgContext.cmd.Clear();
				CommandBuffer commandBuffer = CommandBufferPool.Get(pass.name);
				commandBuffer.SetExecutionFlags(CommandBufferExecutionFlags.AsyncCompute);
				rgContext.cmd = commandBuffer;
				if (flag)
				{
					rgContext.cmd.WaitOnAsyncGraphicsFence(fence);
				}
			}
			if (passInfo.syncToPassIndex != -1)
			{
				rgContext.cmd.WaitOnAsyncGraphicsFence(this.m_CompiledPassInfos[passInfo.syncToPassIndex].fence);
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00008E84 File Offset: 0x00007084
		private void PostRenderPassExecute(ref RenderGraph.CompiledPassInfo passInfo, RenderGraphContext rgContext)
		{
			if (passInfo.needGraphicsFence)
			{
				passInfo.fence = rgContext.cmd.CreateAsyncGraphicsFence();
			}
			if (passInfo.enableAsyncCompute)
			{
				rgContext.renderContext.ExecuteCommandBufferAsync(rgContext.cmd, ComputeQueueType.Background);
				CommandBufferPool.Release(rgContext.cmd);
				rgContext.cmd = this.m_PreviousCommandBuffer;
			}
			this.m_RenderGraphPool.ReleaseAllTempAlloc();
			for (int i = 0; i < 2; i++)
			{
				foreach (int index in passInfo.resourceReleaseList[i])
				{
					this.m_Resources.ReleasePooledResource(rgContext, i, index);
				}
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00008F44 File Offset: 0x00007144
		private void ClearRenderPasses()
		{
			foreach (RenderGraphPass renderGraphPass in this.m_RenderPasses)
			{
				renderGraphPass.Release(this.m_RenderGraphPool);
			}
			this.m_RenderPasses.Clear();
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00008FA8 File Offset: 0x000071A8
		private void ReleaseImmediateModeResources()
		{
			for (int i = 0; i < 2; i++)
			{
				foreach (int index in this.m_ImmediateModeResourceList[i])
				{
					this.m_Resources.ReleasePooledResource(this.m_RenderGraphContext, i, index);
				}
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00009018 File Offset: 0x00007218
		private void LogFrameInformation()
		{
			if (this.m_DebugParameters.enableLogging)
			{
				this.m_FrameInformationLogger.LogLine("==== Staring render graph frame for: " + this.m_CurrentExecutionName + " ====", Array.Empty<object>());
				if (!this.m_DebugParameters.immediateMode)
				{
					this.m_FrameInformationLogger.LogLine("Number of passes declared: {0}\n", new object[]
					{
						this.m_RenderPasses.Count
					});
				}
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000908D File Offset: 0x0000728D
		private void LogRendererListsCreation()
		{
			if (this.m_DebugParameters.enableLogging)
			{
				this.m_FrameInformationLogger.LogLine("Number of renderer lists created: {0}\n", new object[]
				{
					this.m_RendererLists.Count
				});
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000090C8 File Offset: 0x000072C8
		private void LogRenderPassBegin(in RenderGraph.CompiledPassInfo passInfo)
		{
			if (this.m_DebugParameters.enableLogging)
			{
				RenderGraphPass pass = passInfo.pass;
				this.m_FrameInformationLogger.LogLine("[{0}][{1}] \"{2}\"", new object[]
				{
					pass.index,
					pass.enableAsyncCompute ? "Compute" : "Graphics",
					pass.name
				});
				using (new RenderGraphLogIndent(this.m_FrameInformationLogger, 1))
				{
					if (passInfo.syncToPassIndex != -1)
					{
						this.m_FrameInformationLogger.LogLine("Synchronize with [{0}]", new object[]
						{
							passInfo.syncToPassIndex
						});
					}
				}
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000918C File Offset: 0x0000738C
		private void LogCulledPasses()
		{
			if (this.m_DebugParameters.enableLogging)
			{
				this.m_FrameInformationLogger.LogLine("Pass Culling Report:", Array.Empty<object>());
				using (new RenderGraphLogIndent(this.m_FrameInformationLogger, 1))
				{
					for (int i = 0; i < this.m_CompiledPassInfos.size; i++)
					{
						if (this.m_CompiledPassInfos[i].culled)
						{
							RenderGraphPass renderGraphPass = this.m_RenderPasses[i];
							this.m_FrameInformationLogger.LogLine("[{0}] {1}", new object[]
							{
								renderGraphPass.index,
								renderGraphPass.name
							});
						}
					}
					this.m_FrameInformationLogger.LogLine("\n", Array.Empty<object>());
				}
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00009264 File Offset: 0x00007464
		private ProfilingSampler GetDefaultProfilingSampler(string name)
		{
			int hashCode = name.GetHashCode();
			ProfilingSampler profilingSampler;
			if (!this.m_DefaultProfilingSamplers.TryGetValue(hashCode, out profilingSampler))
			{
				profilingSampler = new ProfilingSampler(name);
				this.m_DefaultProfilingSamplers.Add(hashCode, profilingSampler);
			}
			return profilingSampler;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000092A0 File Offset: 0x000074A0
		private void UpdateImportedResourceLifeTime(ref RenderGraphDebugData.ResourceDebugData data, List<int> passList)
		{
			foreach (int num in passList)
			{
				if (data.creationPassIndex == -1)
				{
					data.creationPassIndex = num;
				}
				else
				{
					data.creationPassIndex = Math.Min(data.creationPassIndex, num);
				}
				if (data.releasePassIndex == -1)
				{
					data.releasePassIndex = num;
				}
				else
				{
					data.releasePassIndex = Math.Max(data.releasePassIndex, num);
				}
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00009330 File Offset: 0x00007530
		private void GenerateDebugData()
		{
			if (this.m_ExecutionExceptionWasRaised)
			{
				return;
			}
			if (!RenderGraph.requireDebugData)
			{
				this.CleanupDebugData();
				return;
			}
			RenderGraphDebugData renderGraphDebugData;
			if (!this.m_DebugData.TryGetValue(this.m_CurrentExecutionName, out renderGraphDebugData))
			{
				RenderGraph.OnExecutionRegisteredDelegate onExecutionRegisteredDelegate = RenderGraph.onExecutionRegistered;
				if (onExecutionRegisteredDelegate != null)
				{
					onExecutionRegisteredDelegate(this, this.m_CurrentExecutionName);
				}
				renderGraphDebugData = new RenderGraphDebugData();
				this.m_DebugData.Add(this.m_CurrentExecutionName, renderGraphDebugData);
			}
			renderGraphDebugData.Clear();
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < this.m_CompiledResourcesInfos[i].size; j++)
				{
					ref RenderGraph.CompiledResourceInfo ptr = ref this.m_CompiledResourcesInfos[i][j];
					RenderGraphDebugData.ResourceDebugData resourceDebugData = new RenderGraphDebugData.ResourceDebugData
					{
						name = this.m_Resources.GetRenderGraphResourceName((RenderGraphResourceType)i, j),
						imported = this.m_Resources.IsRenderGraphResourceImported((RenderGraphResourceType)i, j),
						creationPassIndex = -1,
						releasePassIndex = -1,
						consumerList = new List<int>(ptr.consumers),
						producerList = new List<int>(ptr.producers)
					};
					if (resourceDebugData.imported)
					{
						this.UpdateImportedResourceLifeTime(ref resourceDebugData, resourceDebugData.consumerList);
						this.UpdateImportedResourceLifeTime(ref resourceDebugData, resourceDebugData.producerList);
					}
					renderGraphDebugData.resourceLists[i].Add(resourceDebugData);
				}
			}
			for (int k = 0; k < this.m_CompiledPassInfos.size; k++)
			{
				ref RenderGraph.CompiledPassInfo ptr2 = ref this.m_CompiledPassInfos[k];
				RenderGraphDebugData.PassDebugData passDebugData = default(RenderGraphDebugData.PassDebugData);
				passDebugData.name = ptr2.pass.name;
				passDebugData.culled = ptr2.culled;
				passDebugData.generateDebugData = ptr2.pass.generateDebugData;
				passDebugData.resourceReadLists = new List<int>[2];
				passDebugData.resourceWriteLists = new List<int>[2];
				for (int l = 0; l < 2; l++)
				{
					passDebugData.resourceReadLists[l] = new List<int>();
					passDebugData.resourceWriteLists[l] = new List<int>();
					foreach (ResourceHandle handle in ptr2.pass.resourceReadLists[l])
					{
						passDebugData.resourceReadLists[l].Add(handle);
					}
					foreach (ResourceHandle handle2 in ptr2.pass.resourceWriteLists[l])
					{
						passDebugData.resourceWriteLists[l].Add(handle2);
					}
					foreach (int index in ptr2.resourceCreateList[l])
					{
						RenderGraphDebugData.ResourceDebugData resourceDebugData2 = renderGraphDebugData.resourceLists[l][index];
						if (!resourceDebugData2.imported)
						{
							resourceDebugData2.creationPassIndex = k;
							renderGraphDebugData.resourceLists[l][index] = resourceDebugData2;
						}
					}
					foreach (int index2 in ptr2.resourceReleaseList[l])
					{
						RenderGraphDebugData.ResourceDebugData resourceDebugData3 = renderGraphDebugData.resourceLists[l][index2];
						if (!resourceDebugData3.imported)
						{
							resourceDebugData3.releasePassIndex = k;
							renderGraphDebugData.resourceLists[l][index2] = resourceDebugData3;
						}
					}
				}
				renderGraphDebugData.passList.Add(passDebugData);
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000096E8 File Offset: 0x000078E8
		private void CleanupDebugData()
		{
			foreach (KeyValuePair<string, RenderGraphDebugData> keyValuePair in this.m_DebugData)
			{
				RenderGraph.OnExecutionRegisteredDelegate onExecutionRegisteredDelegate = RenderGraph.onExecutionUnregistered;
				if (onExecutionRegisteredDelegate != null)
				{
					onExecutionRegisteredDelegate(this, keyValuePair.Key);
				}
			}
			this.m_DebugData.Clear();
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00009758 File Offset: 0x00007958
		// Note: this type is marked as 'beforefieldinit'.
		static RenderGraph()
		{
		}

		// Token: 0x040000DB RID: 219
		public static readonly int kMaxMRTCount = 8;

		// Token: 0x040000DC RID: 220
		private RenderGraphResourceRegistry m_Resources;

		// Token: 0x040000DD RID: 221
		private RenderGraphObjectPool m_RenderGraphPool = new RenderGraphObjectPool();

		// Token: 0x040000DE RID: 222
		private List<RenderGraphPass> m_RenderPasses = new List<RenderGraphPass>(64);

		// Token: 0x040000DF RID: 223
		private List<RendererListHandle> m_RendererLists = new List<RendererListHandle>(32);

		// Token: 0x040000E0 RID: 224
		private RenderGraphDebugParams m_DebugParameters = new RenderGraphDebugParams();

		// Token: 0x040000E1 RID: 225
		private RenderGraphLogger m_FrameInformationLogger = new RenderGraphLogger();

		// Token: 0x040000E2 RID: 226
		private RenderGraphDefaultResources m_DefaultResources = new RenderGraphDefaultResources();

		// Token: 0x040000E3 RID: 227
		private Dictionary<int, ProfilingSampler> m_DefaultProfilingSamplers = new Dictionary<int, ProfilingSampler>();

		// Token: 0x040000E4 RID: 228
		private bool m_ExecutionExceptionWasRaised;

		// Token: 0x040000E5 RID: 229
		private RenderGraphContext m_RenderGraphContext = new RenderGraphContext();

		// Token: 0x040000E6 RID: 230
		private CommandBuffer m_PreviousCommandBuffer;

		// Token: 0x040000E7 RID: 231
		private int m_CurrentImmediatePassIndex;

		// Token: 0x040000E8 RID: 232
		private List<int>[] m_ImmediateModeResourceList = new List<int>[2];

		// Token: 0x040000E9 RID: 233
		private DynamicArray<RenderGraph.CompiledResourceInfo>[] m_CompiledResourcesInfos = new DynamicArray<RenderGraph.CompiledResourceInfo>[2];

		// Token: 0x040000EA RID: 234
		private DynamicArray<RenderGraph.CompiledPassInfo> m_CompiledPassInfos = new DynamicArray<RenderGraph.CompiledPassInfo>();

		// Token: 0x040000EB RID: 235
		private Stack<int> m_CullingStack = new Stack<int>();

		// Token: 0x040000EC RID: 236
		private int m_ExecutionCount;

		// Token: 0x040000ED RID: 237
		private int m_CurrentFrameIndex;

		// Token: 0x040000EE RID: 238
		private bool m_HasRenderGraphBegun;

		// Token: 0x040000EF RID: 239
		private string m_CurrentExecutionName;

		// Token: 0x040000F0 RID: 240
		private bool m_RendererListCulling;

		// Token: 0x040000F1 RID: 241
		private Dictionary<string, RenderGraphDebugData> m_DebugData = new Dictionary<string, RenderGraphDebugData>();

		// Token: 0x040000F2 RID: 242
		private static List<RenderGraph> s_RegisteredGraphs = new List<RenderGraph>();

		// Token: 0x040000F3 RID: 243
		[CompilerGenerated]
		private string <name>k__BackingField;

		// Token: 0x040000F4 RID: 244
		[CompilerGenerated]
		private static bool <requireDebugData>k__BackingField;

		// Token: 0x040000F5 RID: 245
		[CompilerGenerated]
		private static RenderGraph.OnGraphRegisteredDelegate onGraphRegistered;

		// Token: 0x040000F6 RID: 246
		[CompilerGenerated]
		private static RenderGraph.OnGraphRegisteredDelegate onGraphUnregistered;

		// Token: 0x040000F7 RID: 247
		[CompilerGenerated]
		private static RenderGraph.OnExecutionRegisteredDelegate onExecutionRegistered;

		// Token: 0x040000F8 RID: 248
		[CompilerGenerated]
		private static RenderGraph.OnExecutionRegisteredDelegate onExecutionUnregistered;

		// Token: 0x0200012C RID: 300
		internal struct CompiledResourceInfo
		{
			// Token: 0x0600080E RID: 2062 RVA: 0x000227E8 File Offset: 0x000209E8
			public void Reset()
			{
				if (this.producers == null)
				{
					this.producers = new List<int>();
				}
				if (this.consumers == null)
				{
					this.consumers = new List<int>();
				}
				this.producers.Clear();
				this.consumers.Clear();
				this.refCount = 0;
				this.imported = false;
			}

			// Token: 0x040004DC RID: 1244
			public List<int> producers;

			// Token: 0x040004DD RID: 1245
			public List<int> consumers;

			// Token: 0x040004DE RID: 1246
			public int refCount;

			// Token: 0x040004DF RID: 1247
			public bool imported;
		}

		// Token: 0x0200012D RID: 301
		[DebuggerDisplay("RenderPass: {pass.name} (Index:{pass.index} Async:{enableAsyncCompute})")]
		internal struct CompiledPassInfo
		{
			// Token: 0x170000F0 RID: 240
			// (get) Token: 0x0600080F RID: 2063 RVA: 0x0002283F File Offset: 0x00020A3F
			public bool allowPassCulling
			{
				get
				{
					return this.pass.allowPassCulling;
				}
			}

			// Token: 0x06000810 RID: 2064 RVA: 0x0002284C File Offset: 0x00020A4C
			public void Reset(RenderGraphPass pass)
			{
				this.pass = pass;
				this.enableAsyncCompute = pass.enableAsyncCompute;
				if (this.resourceCreateList == null)
				{
					this.resourceCreateList = new List<int>[2];
					this.resourceReleaseList = new List<int>[2];
					for (int i = 0; i < 2; i++)
					{
						this.resourceCreateList[i] = new List<int>();
						this.resourceReleaseList[i] = new List<int>();
					}
				}
				for (int j = 0; j < 2; j++)
				{
					this.resourceCreateList[j].Clear();
					this.resourceReleaseList[j].Clear();
				}
				this.refCount = 0;
				this.culled = false;
				this.hasSideEffect = false;
				this.syncToPassIndex = -1;
				this.syncFromPassIndex = -1;
				this.needGraphicsFence = false;
			}

			// Token: 0x040004E0 RID: 1248
			public RenderGraphPass pass;

			// Token: 0x040004E1 RID: 1249
			public List<int>[] resourceCreateList;

			// Token: 0x040004E2 RID: 1250
			public List<int>[] resourceReleaseList;

			// Token: 0x040004E3 RID: 1251
			public int refCount;

			// Token: 0x040004E4 RID: 1252
			public bool culled;

			// Token: 0x040004E5 RID: 1253
			public bool hasSideEffect;

			// Token: 0x040004E6 RID: 1254
			public int syncToPassIndex;

			// Token: 0x040004E7 RID: 1255
			public int syncFromPassIndex;

			// Token: 0x040004E8 RID: 1256
			public bool needGraphicsFence;

			// Token: 0x040004E9 RID: 1257
			public GraphicsFence fence;

			// Token: 0x040004EA RID: 1258
			public bool enableAsyncCompute;
		}

		// Token: 0x0200012E RID: 302
		private class ProfilingScopePassData
		{
			// Token: 0x06000811 RID: 2065 RVA: 0x00022902 File Offset: 0x00020B02
			public ProfilingScopePassData()
			{
			}

			// Token: 0x040004EB RID: 1259
			public ProfilingSampler sampler;
		}

		// Token: 0x0200012F RID: 303
		// (Invoke) Token: 0x06000813 RID: 2067
		internal delegate void OnGraphRegisteredDelegate(RenderGraph graph);

		// Token: 0x02000130 RID: 304
		// (Invoke) Token: 0x06000817 RID: 2071
		internal delegate void OnExecutionRegisteredDelegate(RenderGraph graph, string executionName);

		// Token: 0x02000131 RID: 305
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600081A RID: 2074 RVA: 0x0002290A File Offset: 0x00020B0A
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600081B RID: 2075 RVA: 0x00022916 File Offset: 0x00020B16
			public <>c()
			{
			}

			// Token: 0x0600081C RID: 2076 RVA: 0x0002291E File Offset: 0x00020B1E
			internal void <BeginProfilingSampler>b__61_0(RenderGraph.ProfilingScopePassData data, RenderGraphContext ctx)
			{
				data.sampler.Begin(ctx.cmd);
			}

			// Token: 0x0600081D RID: 2077 RVA: 0x00022931 File Offset: 0x00020B31
			internal void <EndProfilingSampler>b__62_0(RenderGraph.ProfilingScopePassData data, RenderGraphContext ctx)
			{
				data.sampler.End(ctx.cmd);
			}

			// Token: 0x040004EC RID: 1260
			public static readonly RenderGraph.<>c <>9 = new RenderGraph.<>c();

			// Token: 0x040004ED RID: 1261
			public static RenderFunc<RenderGraph.ProfilingScopePassData> <>9__61_0;

			// Token: 0x040004EE RID: 1262
			public static RenderFunc<RenderGraph.ProfilingScopePassData> <>9__62_0;
		}
	}
}
