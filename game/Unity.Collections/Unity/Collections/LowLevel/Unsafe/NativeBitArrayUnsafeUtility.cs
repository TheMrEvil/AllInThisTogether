using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000118 RID: 280
	[BurstCompatible]
	public static class NativeBitArrayUnsafeUtility
	{
		// Token: 0x06000A53 RID: 2643 RVA: 0x0001E94C File Offset: 0x0001CB4C
		public unsafe static NativeBitArray ConvertExistingDataToNativeBitArray(void* ptr, int sizeInBytes, AllocatorManager.AllocatorHandle allocator)
		{
			return new NativeBitArray
			{
				m_BitArray = new UnsafeBitArray(ptr, sizeInBytes, allocator)
			};
		}
	}
}
