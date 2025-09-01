using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000029 RID: 41
	[DebuggerDisplay("RenderPass: {name} (Index:{index} Async:{enableAsyncCompute})")]
	internal abstract class RenderGraphPass
	{
		// Token: 0x0600017C RID: 380 RVA: 0x0000A1B9 File Offset: 0x000083B9
		public RenderFunc<PassData> GetExecuteDelegate<PassData>() where PassData : class, new()
		{
			return ((RenderGraphPass<PassData>)this).renderFunc;
		}

		// Token: 0x0600017D RID: 381
		public abstract void Execute(RenderGraphContext renderGraphContext);

		// Token: 0x0600017E RID: 382
		public abstract void Release(RenderGraphObjectPool pool);

		// Token: 0x0600017F RID: 383
		public abstract bool HasRenderFunc();

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000A1C6 File Offset: 0x000083C6
		// (set) Token: 0x06000181 RID: 385 RVA: 0x0000A1CE File Offset: 0x000083CE
		public string name
		{
			[CompilerGenerated]
			get
			{
				return this.<name>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<name>k__BackingField = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000182 RID: 386 RVA: 0x0000A1D7 File Offset: 0x000083D7
		// (set) Token: 0x06000183 RID: 387 RVA: 0x0000A1DF File Offset: 0x000083DF
		public int index
		{
			[CompilerGenerated]
			get
			{
				return this.<index>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<index>k__BackingField = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0000A1E8 File Offset: 0x000083E8
		// (set) Token: 0x06000185 RID: 389 RVA: 0x0000A1F0 File Offset: 0x000083F0
		public ProfilingSampler customSampler
		{
			[CompilerGenerated]
			get
			{
				return this.<customSampler>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<customSampler>k__BackingField = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000A1F9 File Offset: 0x000083F9
		// (set) Token: 0x06000187 RID: 391 RVA: 0x0000A201 File Offset: 0x00008401
		public bool enableAsyncCompute
		{
			[CompilerGenerated]
			get
			{
				return this.<enableAsyncCompute>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<enableAsyncCompute>k__BackingField = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000A20A File Offset: 0x0000840A
		// (set) Token: 0x06000189 RID: 393 RVA: 0x0000A212 File Offset: 0x00008412
		public bool allowPassCulling
		{
			[CompilerGenerated]
			get
			{
				return this.<allowPassCulling>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<allowPassCulling>k__BackingField = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000A21B File Offset: 0x0000841B
		// (set) Token: 0x0600018B RID: 395 RVA: 0x0000A223 File Offset: 0x00008423
		public TextureHandle depthBuffer
		{
			[CompilerGenerated]
			get
			{
				return this.<depthBuffer>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<depthBuffer>k__BackingField = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000A22C File Offset: 0x0000842C
		// (set) Token: 0x0600018D RID: 397 RVA: 0x0000A234 File Offset: 0x00008434
		public TextureHandle[] colorBuffers
		{
			[CompilerGenerated]
			get
			{
				return this.<colorBuffers>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<colorBuffers>k__BackingField = value;
			}
		} = new TextureHandle[RenderGraph.kMaxMRTCount];

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600018E RID: 398 RVA: 0x0000A23D File Offset: 0x0000843D
		// (set) Token: 0x0600018F RID: 399 RVA: 0x0000A245 File Offset: 0x00008445
		public int colorBufferMaxIndex
		{
			[CompilerGenerated]
			get
			{
				return this.<colorBufferMaxIndex>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<colorBufferMaxIndex>k__BackingField = value;
			}
		} = -1;

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000190 RID: 400 RVA: 0x0000A24E File Offset: 0x0000844E
		// (set) Token: 0x06000191 RID: 401 RVA: 0x0000A256 File Offset: 0x00008456
		public int refCount
		{
			[CompilerGenerated]
			get
			{
				return this.<refCount>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<refCount>k__BackingField = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000192 RID: 402 RVA: 0x0000A25F File Offset: 0x0000845F
		// (set) Token: 0x06000193 RID: 403 RVA: 0x0000A267 File Offset: 0x00008467
		public bool generateDebugData
		{
			[CompilerGenerated]
			get
			{
				return this.<generateDebugData>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<generateDebugData>k__BackingField = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000A270 File Offset: 0x00008470
		// (set) Token: 0x06000195 RID: 405 RVA: 0x0000A278 File Offset: 0x00008478
		public bool allowRendererListCulling
		{
			[CompilerGenerated]
			get
			{
				return this.<allowRendererListCulling>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<allowRendererListCulling>k__BackingField = value;
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000A284 File Offset: 0x00008484
		public RenderGraphPass()
		{
			for (int i = 0; i < 2; i++)
			{
				this.resourceReadLists[i] = new List<ResourceHandle>();
				this.resourceWriteLists[i] = new List<ResourceHandle>();
				this.transientResourceList[i] = new List<ResourceHandle>();
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000A31C File Offset: 0x0000851C
		public void Clear()
		{
			this.name = "";
			this.index = -1;
			this.customSampler = null;
			for (int i = 0; i < 2; i++)
			{
				this.resourceReadLists[i].Clear();
				this.resourceWriteLists[i].Clear();
				this.transientResourceList[i].Clear();
			}
			this.usedRendererListList.Clear();
			this.dependsOnRendererListList.Clear();
			this.enableAsyncCompute = false;
			this.allowPassCulling = true;
			this.allowRendererListCulling = true;
			this.generateDebugData = true;
			this.refCount = 0;
			this.colorBufferMaxIndex = -1;
			this.depthBuffer = TextureHandle.nullHandle;
			for (int j = 0; j < RenderGraph.kMaxMRTCount; j++)
			{
				this.colorBuffers[j] = TextureHandle.nullHandle;
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000A3E4 File Offset: 0x000085E4
		public void AddResourceWrite(in ResourceHandle res)
		{
			List<ResourceHandle>[] array = this.resourceWriteLists;
			ResourceHandle resourceHandle = res;
			array[resourceHandle.iType].Add(res);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000A414 File Offset: 0x00008614
		public void AddResourceRead(in ResourceHandle res)
		{
			List<ResourceHandle>[] array = this.resourceReadLists;
			ResourceHandle resourceHandle = res;
			array[resourceHandle.iType].Add(res);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000A444 File Offset: 0x00008644
		public void AddTransientResource(in ResourceHandle res)
		{
			List<ResourceHandle>[] array = this.transientResourceList;
			ResourceHandle resourceHandle = res;
			array[resourceHandle.iType].Add(res);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000A471 File Offset: 0x00008671
		public void UseRendererList(RendererListHandle rendererList)
		{
			this.usedRendererListList.Add(rendererList);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000A47F File Offset: 0x0000867F
		public void DependsOnRendererList(RendererListHandle rendererList)
		{
			this.dependsOnRendererListList.Add(rendererList);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000A48D File Offset: 0x0000868D
		public void EnableAsyncCompute(bool value)
		{
			this.enableAsyncCompute = value;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000A496 File Offset: 0x00008696
		public void AllowPassCulling(bool value)
		{
			this.allowPassCulling = value;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000A49F File Offset: 0x0000869F
		public void AllowRendererListCulling(bool value)
		{
			this.allowRendererListCulling = value;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000A4A8 File Offset: 0x000086A8
		public void GenerateDebugData(bool value)
		{
			this.generateDebugData = value;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000A4B1 File Offset: 0x000086B1
		public void SetColorBuffer(TextureHandle resource, int index)
		{
			this.colorBufferMaxIndex = Math.Max(this.colorBufferMaxIndex, index);
			this.colorBuffers[index] = resource;
			this.AddResourceWrite(resource.handle);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000A4DF File Offset: 0x000086DF
		public void SetDepthBuffer(TextureHandle resource, DepthAccess flags)
		{
			this.depthBuffer = resource;
			if ((flags & DepthAccess.Read) != (DepthAccess)0)
			{
				this.AddResourceRead(resource.handle);
			}
			if ((flags & DepthAccess.Write) != (DepthAccess)0)
			{
				this.AddResourceWrite(resource.handle);
			}
		}

		// Token: 0x04000117 RID: 279
		[CompilerGenerated]
		private string <name>k__BackingField;

		// Token: 0x04000118 RID: 280
		[CompilerGenerated]
		private int <index>k__BackingField;

		// Token: 0x04000119 RID: 281
		[CompilerGenerated]
		private ProfilingSampler <customSampler>k__BackingField;

		// Token: 0x0400011A RID: 282
		[CompilerGenerated]
		private bool <enableAsyncCompute>k__BackingField;

		// Token: 0x0400011B RID: 283
		[CompilerGenerated]
		private bool <allowPassCulling>k__BackingField;

		// Token: 0x0400011C RID: 284
		[CompilerGenerated]
		private TextureHandle <depthBuffer>k__BackingField;

		// Token: 0x0400011D RID: 285
		[CompilerGenerated]
		private TextureHandle[] <colorBuffers>k__BackingField;

		// Token: 0x0400011E RID: 286
		[CompilerGenerated]
		private int <colorBufferMaxIndex>k__BackingField;

		// Token: 0x0400011F RID: 287
		[CompilerGenerated]
		private int <refCount>k__BackingField;

		// Token: 0x04000120 RID: 288
		[CompilerGenerated]
		private bool <generateDebugData>k__BackingField;

		// Token: 0x04000121 RID: 289
		[CompilerGenerated]
		private bool <allowRendererListCulling>k__BackingField;

		// Token: 0x04000122 RID: 290
		public List<ResourceHandle>[] resourceReadLists = new List<ResourceHandle>[2];

		// Token: 0x04000123 RID: 291
		public List<ResourceHandle>[] resourceWriteLists = new List<ResourceHandle>[2];

		// Token: 0x04000124 RID: 292
		public List<ResourceHandle>[] transientResourceList = new List<ResourceHandle>[2];

		// Token: 0x04000125 RID: 293
		public List<RendererListHandle> usedRendererListList = new List<RendererListHandle>();

		// Token: 0x04000126 RID: 294
		public List<RendererListHandle> dependsOnRendererListList = new List<RendererListHandle>();
	}
}
