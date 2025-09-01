using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000CF RID: 207
	[NativeContainer]
	[BurstCompatible]
	internal struct NativeQueueDispose
	{
		// Token: 0x060007BC RID: 1980 RVA: 0x00017C9C File Offset: 0x00015E9C
		public void Dispose()
		{
			NativeQueueData.DeallocateQueue(this.m_Buffer, this.m_QueuePool, this.m_AllocatorLabel);
		}

		// Token: 0x040002AE RID: 686
		[NativeDisableUnsafePtrRestriction]
		internal unsafe NativeQueueData* m_Buffer;

		// Token: 0x040002AF RID: 687
		[NativeDisableUnsafePtrRestriction]
		internal unsafe NativeQueueBlockPoolData* m_QueuePool;

		// Token: 0x040002B0 RID: 688
		internal AllocatorManager.AllocatorHandle m_AllocatorLabel;
	}
}
