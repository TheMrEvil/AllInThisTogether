using System;
using Unity.Burst;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000D4 RID: 212
	[BurstCompile]
	internal struct NativeReferenceDisposeJob : IJob
	{
		// Token: 0x060007D3 RID: 2003 RVA: 0x00017EF6 File Offset: 0x000160F6
		public void Execute()
		{
			this.Data.Dispose();
		}

		// Token: 0x040002B7 RID: 695
		internal NativeReferenceDispose Data;
	}
}
