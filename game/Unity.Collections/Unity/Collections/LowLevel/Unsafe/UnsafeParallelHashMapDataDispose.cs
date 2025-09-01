using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000131 RID: 305
	[NativeContainer]
	[BurstCompatible]
	internal struct UnsafeParallelHashMapDataDispose
	{
		// Token: 0x06000B25 RID: 2853 RVA: 0x00021011 File Offset: 0x0001F211
		public void Dispose()
		{
			UnsafeParallelHashMapData.DeallocateHashMap(this.m_Buffer, this.m_AllocatorLabel);
		}

		// Token: 0x040003A7 RID: 935
		[NativeDisableUnsafePtrRestriction]
		internal unsafe UnsafeParallelHashMapData* m_Buffer;

		// Token: 0x040003A8 RID: 936
		internal AllocatorManager.AllocatorHandle m_AllocatorLabel;
	}
}
