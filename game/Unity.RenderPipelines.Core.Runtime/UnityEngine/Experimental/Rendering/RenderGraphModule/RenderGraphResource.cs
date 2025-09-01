using System;
using System.Diagnostics;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000038 RID: 56
	[DebuggerDisplay("Resource ({GetType().Name}:{GetName()})")]
	internal abstract class RenderGraphResource<DescType, ResType> : IRenderGraphResource where DescType : struct where ResType : class
	{
		// Token: 0x06000223 RID: 547 RVA: 0x0000BCF2 File Offset: 0x00009EF2
		protected RenderGraphResource()
		{
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000BCFA File Offset: 0x00009EFA
		public override void Reset(IRenderGraphResourcePool pool)
		{
			base.Reset(pool);
			this.graphicsResource = default(ResType);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000BD0F File Offset: 0x00009F0F
		public override bool IsCreated()
		{
			return this.graphicsResource != null;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000BD1F File Offset: 0x00009F1F
		public override void ReleaseGraphicsResource()
		{
			this.graphicsResource = default(ResType);
		}

		// Token: 0x0400015C RID: 348
		public DescType desc;

		// Token: 0x0400015D RID: 349
		public ResType graphicsResource;
	}
}
