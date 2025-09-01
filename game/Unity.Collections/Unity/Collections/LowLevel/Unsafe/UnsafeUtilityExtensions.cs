using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000156 RID: 342
	[BurstCompatible]
	public static class UnsafeUtilityExtensions
	{
		// Token: 0x06000C1C RID: 3100 RVA: 0x00024260 File Offset: 0x00022460
		internal unsafe static void MemSwap(void* ptr, void* otherPtr, long size)
		{
			byte* ptr2 = (byte*)ptr;
			byte* ptr3 = (byte*)otherPtr;
			byte* ptr4 = stackalloc byte[(UIntPtr)1024];
			while (size > 0L)
			{
				long num = math.min(size, 1024L);
				UnsafeUtility.MemCpy((void*)ptr4, (void*)ptr2, num);
				UnsafeUtility.MemCpy((void*)ptr2, (void*)ptr3, num);
				UnsafeUtility.MemCpy((void*)ptr3, (void*)ptr4, num);
				size -= num;
				ptr3 += num;
				ptr2 += num;
			}
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x000242B5 File Offset: 0x000224B5
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public unsafe static T ReadArrayElementBoundsChecked<T>(void* source, int index, int capacity)
		{
			return UnsafeUtility.ReadArrayElement<T>(source, index);
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x000242BE File Offset: 0x000224BE
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public unsafe static void WriteArrayElementBoundsChecked<T>(void* destination, int index, T value, int capacity)
		{
			UnsafeUtility.WriteArrayElement<T>(destination, index, value);
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x000242C8 File Offset: 0x000224C8
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void* AddressOf<T>(in T value) where T : struct
		{
			return ILSupport.AddressOf<T>(value);
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x000242D0 File Offset: 0x000224D0
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref T AsRef<T>(in T value) where T : struct
		{
			return ILSupport.AsRef<T>(value);
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x000242D8 File Offset: 0x000224D8
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private unsafe static void CheckMemSwapOverlap(byte* dst, byte* src, long size)
		{
			if (dst + size != src && src + size != dst)
			{
				throw new InvalidOperationException("MemSwap memory blocks are overlapped.");
			}
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x000242F3 File Offset: 0x000224F3
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckIndexRange(int index, int capacity)
		{
			if (index > capacity - 1 || index < 0)
			{
				throw new IndexOutOfRangeException(string.Format("Attempt to read or write from array index {0}, which is out of bounds. Array capacity is {1}. ", index, capacity) + "This may lead to a crash, data corruption, or reading invalid data.");
			}
		}
	}
}
