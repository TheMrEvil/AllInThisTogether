using System;
using System.Diagnostics;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x0200002A RID: 42
	[DebuggerDisplay("RenderPass: {name} (Index:{index} Async:{enableAsyncCompute})")]
	internal sealed class RenderGraphPass<PassData> : RenderGraphPass where PassData : class, new()
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x0000A50C File Offset: 0x0000870C
		public override void Execute(RenderGraphContext renderGraphContext)
		{
			base.GetExecuteDelegate<PassData>()(this.data, renderGraphContext);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000A520 File Offset: 0x00008720
		public void Initialize(int passIndex, PassData passData, string passName, ProfilingSampler sampler)
		{
			base.Clear();
			base.index = passIndex;
			this.data = passData;
			base.name = passName;
			base.customSampler = sampler;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000A545 File Offset: 0x00008745
		public override void Release(RenderGraphObjectPool pool)
		{
			base.Clear();
			pool.Release<PassData>(this.data);
			this.data = default(PassData);
			this.renderFunc = null;
			pool.Release<RenderGraphPass<PassData>>(this);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000A573 File Offset: 0x00008773
		public override bool HasRenderFunc()
		{
			return this.renderFunc != null;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000A57E File Offset: 0x0000877E
		public RenderGraphPass()
		{
		}

		// Token: 0x04000127 RID: 295
		internal PassData data;

		// Token: 0x04000128 RID: 296
		internal RenderFunc<PassData> renderFunc;
	}
}
