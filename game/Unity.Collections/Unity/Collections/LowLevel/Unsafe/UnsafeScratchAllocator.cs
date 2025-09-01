using System;
using System.Diagnostics;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000149 RID: 329
	[BurstCompatible]
	public struct UnsafeScratchAllocator
	{
		// Token: 0x06000BE1 RID: 3041 RVA: 0x000236F8 File Offset: 0x000218F8
		public unsafe UnsafeScratchAllocator(void* ptr, int capacityInBytes)
		{
			this.m_Pointer = ptr;
			this.m_LengthInBytes = 0;
			this.m_CapacityInBytes = capacityInBytes;
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0002370F File Offset: 0x0002190F
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckAllocationDoesNotExceedCapacity(ulong requestedSize)
		{
			if (requestedSize > (ulong)((long)this.m_CapacityInBytes))
			{
				throw new ArgumentException(string.Format("Cannot allocate more than provided size in UnsafeScratchAllocator. Requested: {0} Size: {1} Capacity: {2}", requestedSize, this.m_LengthInBytes, this.m_CapacityInBytes));
			}
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00023748 File Offset: 0x00021948
		public unsafe void* Allocate(int sizeInBytes, int alignmentInBytes)
		{
			if (sizeInBytes == 0)
			{
				return null;
			}
			ulong num = (ulong)((long)(alignmentInBytes - 1));
			long value = (long)((IntPtr)this.m_Pointer) + (long)this.m_LengthInBytes + (long)num & (long)(~(long)num);
			long num2 = (long)((byte*)((void*)((IntPtr)value)) - (byte*)this.m_Pointer);
			num2 += (long)sizeInBytes;
			this.m_LengthInBytes = (int)num2;
			return (void*)((IntPtr)value);
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x000237AA File Offset: 0x000219AA
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public unsafe void* Allocate<T>(int count = 1) where T : struct
		{
			return this.Allocate(UnsafeUtility.SizeOf<T>() * count, UnsafeUtility.AlignOf<T>());
		}

		// Token: 0x040003D2 RID: 978
		private unsafe void* m_Pointer;

		// Token: 0x040003D3 RID: 979
		private int m_LengthInBytes;

		// Token: 0x040003D4 RID: 980
		private readonly int m_CapacityInBytes;
	}
}
