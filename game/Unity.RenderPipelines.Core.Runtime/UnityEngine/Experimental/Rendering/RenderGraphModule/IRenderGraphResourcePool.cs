using System;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000030 RID: 48
	internal abstract class IRenderGraphResourcePool
	{
		// Token: 0x060001BF RID: 447
		public abstract void PurgeUnusedResources(int currentFrameIndex);

		// Token: 0x060001C0 RID: 448
		public abstract void Cleanup();

		// Token: 0x060001C1 RID: 449
		public abstract void CheckFrameAllocation(bool onException, int frameIndex);

		// Token: 0x060001C2 RID: 450
		public abstract void LogResources(RenderGraphLogger logger);

		// Token: 0x060001C3 RID: 451 RVA: 0x0000A968 File Offset: 0x00008B68
		protected IRenderGraphResourcePool()
		{
		}
	}
}
