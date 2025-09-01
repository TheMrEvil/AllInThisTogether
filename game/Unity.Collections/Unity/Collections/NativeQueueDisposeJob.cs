using System;
using Unity.Burst;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000D0 RID: 208
	[BurstCompile]
	internal struct NativeQueueDisposeJob : IJob
	{
		// Token: 0x060007BD RID: 1981 RVA: 0x00017CB5 File Offset: 0x00015EB5
		public void Execute()
		{
			this.Data.Dispose();
		}

		// Token: 0x040002B1 RID: 689
		public NativeQueueDispose Data;
	}
}
