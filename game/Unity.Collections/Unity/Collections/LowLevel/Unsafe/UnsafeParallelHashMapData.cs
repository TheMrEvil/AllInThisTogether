using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Mathematics;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200012F RID: 303
	[BurstCompatible]
	[StructLayout(LayoutKind.Explicit)]
	internal struct UnsafeParallelHashMapData
	{
		// Token: 0x06000B17 RID: 2839 RVA: 0x00020A69 File Offset: 0x0001EC69
		internal static int GetBucketSize(int capacity)
		{
			return capacity * 2;
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00020A6E File Offset: 0x0001EC6E
		internal static int GrowCapacity(int capacity)
		{
			if (capacity == 0)
			{
				return 1;
			}
			return capacity * 2;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00020A78 File Offset: 0x0001EC78
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		internal unsafe static void AllocateHashMap<TKey, TValue>(int length, int bucketLength, AllocatorManager.AllocatorHandle label, out UnsafeParallelHashMapData* outBuf) where TKey : struct where TValue : struct
		{
			UnsafeParallelHashMapData* ptr = (UnsafeParallelHashMapData*)Memory.Unmanaged.Allocate((long)sizeof(UnsafeParallelHashMapData), UnsafeUtility.AlignOf<UnsafeParallelHashMapData>(), label);
			bucketLength = math.ceilpow2(bucketLength);
			ptr->keyCapacity = length;
			ptr->bucketCapacityMask = bucketLength - 1;
			int num2;
			int num3;
			int num4;
			int num = UnsafeParallelHashMapData.CalculateDataSize<TKey, TValue>(length, bucketLength, out num2, out num3, out num4);
			ptr->values = (byte*)Memory.Unmanaged.Allocate((long)num, 64, label);
			ptr->keys = ptr->values + num2;
			ptr->next = ptr->values + num3;
			ptr->buckets = ptr->values + num4;
			outBuf = ptr;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00020B00 File Offset: 0x0001ED00
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		internal unsafe static void ReallocateHashMap<TKey, TValue>(UnsafeParallelHashMapData* data, int newCapacity, int newBucketCapacity, AllocatorManager.AllocatorHandle label) where TKey : struct where TValue : struct
		{
			newBucketCapacity = math.ceilpow2(newBucketCapacity);
			if (data->keyCapacity == newCapacity && data->bucketCapacityMask + 1 == newBucketCapacity)
			{
				return;
			}
			int num;
			int num2;
			int num3;
			byte* ptr = (byte*)Memory.Unmanaged.Allocate((long)UnsafeParallelHashMapData.CalculateDataSize<TKey, TValue>(newCapacity, newBucketCapacity, out num, out num2, out num3), 64, label);
			byte* destination = ptr + num;
			byte* ptr2 = ptr + num2;
			byte* ptr3 = ptr + num3;
			UnsafeUtility.MemCpy((void*)ptr, (void*)data->values, (long)(data->keyCapacity * UnsafeUtility.SizeOf<TValue>()));
			UnsafeUtility.MemCpy((void*)destination, (void*)data->keys, (long)(data->keyCapacity * UnsafeUtility.SizeOf<TKey>()));
			UnsafeUtility.MemCpy((void*)ptr2, (void*)data->next, (long)(data->keyCapacity * UnsafeUtility.SizeOf<int>()));
			for (int i = data->keyCapacity; i < newCapacity; i++)
			{
				*(int*)(ptr2 + (IntPtr)i * 4) = -1;
			}
			for (int j = 0; j < newBucketCapacity; j++)
			{
				*(int*)(ptr3 + (IntPtr)j * 4) = -1;
			}
			for (int k = 0; k <= data->bucketCapacityMask; k++)
			{
				int* ptr4 = (int*)data->buckets;
				int* ptr5 = (int*)ptr2;
				while (ptr4[k] >= 0)
				{
					int num4 = ptr4[k];
					ptr4[k] = ptr5[num4];
					TKey tkey = UnsafeUtility.ReadArrayElement<TKey>((void*)data->keys, num4);
					int num5 = tkey.GetHashCode() & newBucketCapacity - 1;
					ptr5[num4] = *(int*)(ptr3 + (IntPtr)num5 * 4);
					*(int*)(ptr3 + (IntPtr)num5 * 4) = num4;
				}
			}
			Memory.Unmanaged.Free<byte>(data->values, label);
			if (data->allocatedIndexLength > data->keyCapacity)
			{
				data->allocatedIndexLength = data->keyCapacity;
			}
			data->values = ptr;
			data->keys = destination;
			data->next = ptr2;
			data->buckets = ptr3;
			data->keyCapacity = newCapacity;
			data->bucketCapacityMask = newBucketCapacity - 1;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00020CB9 File Offset: 0x0001EEB9
		internal unsafe static void DeallocateHashMap(UnsafeParallelHashMapData* data, AllocatorManager.AllocatorHandle allocator)
		{
			Memory.Unmanaged.Free<byte>(data->values, allocator);
			Memory.Unmanaged.Free<UnsafeParallelHashMapData>(data, allocator);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00020CD0 File Offset: 0x0001EED0
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		internal static int CalculateDataSize<TKey, TValue>(int length, int bucketLength, out int keyOffset, out int nextOffset, out int bucketOffset) where TKey : struct where TValue : struct
		{
			int num = UnsafeUtility.SizeOf<TValue>();
			int num2 = UnsafeUtility.SizeOf<TKey>();
			int num3 = UnsafeUtility.SizeOf<int>();
			int num4 = CollectionHelper.Align(num * length, 64);
			int num5 = CollectionHelper.Align(num2 * length, 64);
			int num6 = CollectionHelper.Align(num3 * length, 64);
			int num7 = CollectionHelper.Align(num3 * bucketLength, 64);
			int result = num4 + num5 + num6 + num7;
			keyOffset = num4;
			nextOffset = keyOffset + num5;
			bucketOffset = nextOffset + num6;
			return result;
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00020D38 File Offset: 0x0001EF38
		internal unsafe static bool IsEmpty(UnsafeParallelHashMapData* data)
		{
			if (data->allocatedIndexLength <= 0)
			{
				return true;
			}
			int* ptr = (int*)data->buckets;
			int* ptr2 = (int*)data->next;
			int num = data->bucketCapacityMask;
			for (int i = 0; i <= num; i++)
			{
				if (ptr[i] != -1)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00020D80 File Offset: 0x0001EF80
		internal unsafe static int GetCount(UnsafeParallelHashMapData* data)
		{
			if (data->allocatedIndexLength <= 0)
			{
				return 0;
			}
			int* ptr = (int*)data->next;
			int num = 0;
			for (int i = 0; i < 128; i++)
			{
				for (int j = *(ref data->firstFreeTLS.FixedElementField + (IntPtr)(i * 16) * 4); j >= 0; j = ptr[j])
				{
					num++;
				}
			}
			return math.min(data->keyCapacity, data->allocatedIndexLength) - num;
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00020DEC File Offset: 0x0001EFEC
		internal unsafe static bool MoveNext(UnsafeParallelHashMapData* data, ref int bucketIndex, ref int nextIndex, out int index)
		{
			int* ptr = (int*)data->buckets;
			int* ptr2 = (int*)data->next;
			int num = data->bucketCapacityMask;
			if (nextIndex != -1)
			{
				index = nextIndex;
				nextIndex = ptr2[nextIndex];
				return true;
			}
			for (int i = bucketIndex; i <= num; i++)
			{
				int num2 = ptr[i];
				if (num2 != -1)
				{
					index = num2;
					bucketIndex = i + 1;
					nextIndex = ptr2[num2];
					return true;
				}
			}
			index = -1;
			bucketIndex = num + 1;
			nextIndex = -1;
			return false;
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00020E60 File Offset: 0x0001F060
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		internal unsafe static void GetKeyArray<TKey>(UnsafeParallelHashMapData* data, NativeArray<TKey> result) where TKey : struct
		{
			int* ptr = (int*)data->buckets;
			int* ptr2 = (int*)data->next;
			int num = 0;
			int num2 = 0;
			int length = result.Length;
			while (num <= data->bucketCapacityMask && num2 < length)
			{
				for (int num3 = ptr[num]; num3 != -1; num3 = ptr2[num3])
				{
					result[num2++] = UnsafeUtility.ReadArrayElement<TKey>((void*)data->keys, num3);
				}
				num++;
			}
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00020ED0 File Offset: 0x0001F0D0
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		internal unsafe static void GetValueArray<TValue>(UnsafeParallelHashMapData* data, NativeArray<TValue> result) where TValue : struct
		{
			int* ptr = (int*)data->buckets;
			int* ptr2 = (int*)data->next;
			int num = 0;
			int num2 = 0;
			int length = result.Length;
			int num3 = data->bucketCapacityMask;
			while (num <= num3 && num2 < length)
			{
				for (int num4 = ptr[num]; num4 != -1; num4 = ptr2[num4])
				{
					result[num2++] = UnsafeUtility.ReadArrayElement<TValue>((void*)data->values, num4);
				}
				num++;
			}
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00020F44 File Offset: 0x0001F144
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		internal unsafe static void GetKeyValueArrays<TKey, TValue>(UnsafeParallelHashMapData* data, NativeKeyValueArrays<TKey, TValue> result) where TKey : struct where TValue : struct
		{
			int* ptr = (int*)data->buckets;
			int* ptr2 = (int*)data->next;
			int num = 0;
			int num2 = 0;
			int length = result.Length;
			int num3 = data->bucketCapacityMask;
			while (num <= num3 && num2 < length)
			{
				for (int num4 = ptr[num]; num4 != -1; num4 = ptr2[num4])
				{
					result.Keys[num2] = UnsafeUtility.ReadArrayElement<TKey>((void*)data->keys, num4);
					result.Values[num2] = UnsafeUtility.ReadArrayElement<TValue>((void*)data->values, num4);
					num2++;
				}
				num++;
			}
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00020FD6 File Offset: 0x0001F1D6
		internal UnsafeHashMapBucketData GetBucketData()
		{
			return new UnsafeHashMapBucketData(this.values, this.keys, this.next, this.buckets, this.bucketCapacityMask);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00020FFB File Offset: 0x0001F1FB
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private unsafe static void CheckHashMapReallocateDoesNotShrink(UnsafeParallelHashMapData* data, int newCapacity)
		{
			if (data->keyCapacity > newCapacity)
			{
				throw new Exception("Shrinking a hash map is not supported");
			}
		}

		// Token: 0x0400039D RID: 925
		[FieldOffset(0)]
		internal unsafe byte* values;

		// Token: 0x0400039E RID: 926
		[FieldOffset(8)]
		internal unsafe byte* keys;

		// Token: 0x0400039F RID: 927
		[FieldOffset(16)]
		internal unsafe byte* next;

		// Token: 0x040003A0 RID: 928
		[FieldOffset(24)]
		internal unsafe byte* buckets;

		// Token: 0x040003A1 RID: 929
		[FieldOffset(32)]
		internal int keyCapacity;

		// Token: 0x040003A2 RID: 930
		[FieldOffset(36)]
		internal int bucketCapacityMask;

		// Token: 0x040003A3 RID: 931
		[FieldOffset(40)]
		internal int allocatedIndexLength;

		// Token: 0x040003A4 RID: 932
		[FixedBuffer(typeof(int), 2048)]
		[FieldOffset(64)]
		internal UnsafeParallelHashMapData.<firstFreeTLS>e__FixedBuffer firstFreeTLS;

		// Token: 0x040003A5 RID: 933
		internal const int IntsPerCacheLine = 16;

		// Token: 0x02000130 RID: 304
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 8192)]
		public struct <firstFreeTLS>e__FixedBuffer
		{
			// Token: 0x040003A6 RID: 934
			public int FixedElementField;
		}
	}
}
