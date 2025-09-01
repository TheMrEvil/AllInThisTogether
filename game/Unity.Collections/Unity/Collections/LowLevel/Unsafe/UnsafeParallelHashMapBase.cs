using System;
using System.Diagnostics;
using System.Threading;
using Unity.Mathematics;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000133 RID: 307
	[BurstCompatible(GenericTypeArguments = new Type[]
	{
		typeof(int),
		typeof(int)
	})]
	internal struct UnsafeParallelHashMapBase<TKey, TValue> where TKey : struct, IEquatable<TKey> where TValue : struct
	{
		// Token: 0x06000B27 RID: 2855 RVA: 0x00021034 File Offset: 0x0001F234
		internal unsafe static void Clear(UnsafeParallelHashMapData* data)
		{
			UnsafeUtility.MemSet((void*)data->buckets, byte.MaxValue, (long)((data->bucketCapacityMask + 1) * 4));
			UnsafeUtility.MemSet((void*)data->next, byte.MaxValue, (long)(data->keyCapacity * 4));
			for (int i = 0; i < 128; i++)
			{
				*(ref data->firstFreeTLS.FixedElementField + (IntPtr)(i * 16) * 4) = -1;
			}
			data->allocatedIndexLength = 0;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x000210A4 File Offset: 0x0001F2A4
		internal unsafe static int AllocEntry(UnsafeParallelHashMapData* data, int threadIndex)
		{
			int* next = (int*)data->next;
			int num;
			for (;;)
			{
				num = *(ref data->firstFreeTLS.FixedElementField + (IntPtr)(threadIndex * 16) * 4);
				if (num < 0)
				{
					Interlocked.Exchange(ref data->firstFreeTLS.FixedElementField + (IntPtr)(threadIndex * 16) * 4, -2);
					if (data->allocatedIndexLength < data->keyCapacity)
					{
						num = Interlocked.Add(ref data->allocatedIndexLength, 16) - 16;
						if (num < data->keyCapacity - 1)
						{
							break;
						}
						if (num == data->keyCapacity - 1)
						{
							goto Block_5;
						}
					}
					Interlocked.Exchange(ref data->firstFreeTLS.FixedElementField + (IntPtr)(threadIndex * 16) * 4, -1);
					bool flag = true;
					while (flag)
					{
						flag = false;
						for (int num2 = (threadIndex + 1) % 128; num2 != threadIndex; num2 = (num2 + 1) % 128)
						{
							do
							{
								num = *(ref data->firstFreeTLS.FixedElementField + (IntPtr)(num2 * 16) * 4);
							}
							while (num >= 0 && Interlocked.CompareExchange(ref data->firstFreeTLS.FixedElementField + (IntPtr)(num2 * 16) * 4, next[num], num) != num);
							if (num == -2)
							{
								flag = true;
							}
							else if (num >= 0)
							{
								goto Block_8;
							}
						}
					}
				}
				if (Interlocked.CompareExchange(ref data->firstFreeTLS.FixedElementField + (IntPtr)(threadIndex * 16) * 4, next[num], num) == num)
				{
					goto Block_9;
				}
			}
			int num3 = math.min(16, data->keyCapacity - num);
			for (int i = 1; i < num3; i++)
			{
				next[num + i] = num + i + 1;
			}
			next[num + num3 - 1] = -1;
			next[num] = -1;
			Interlocked.Exchange(ref data->firstFreeTLS.FixedElementField + (IntPtr)(threadIndex * 16) * 4, num + 1);
			return num;
			Block_5:
			Interlocked.Exchange(ref data->firstFreeTLS.FixedElementField + (IntPtr)(threadIndex * 16) * 4, -1);
			return num;
			Block_8:
			next[num] = -1;
			return num;
			Block_9:
			next[num] = -1;
			return num;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0002126C File Offset: 0x0001F46C
		internal unsafe static void FreeEntry(UnsafeParallelHashMapData* data, int idx, int threadIndex)
		{
			int* next = (int*)data->next;
			int num;
			do
			{
				num = *(ref data->firstFreeTLS.FixedElementField + (IntPtr)(threadIndex * 16) * 4);
				next[idx] = num;
			}
			while (Interlocked.CompareExchange(ref data->firstFreeTLS.FixedElementField + (IntPtr)(threadIndex * 16) * 4, idx, num) != num);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x000212BC File Offset: 0x0001F4BC
		internal unsafe static bool TryAddAtomic(UnsafeParallelHashMapData* data, TKey key, TValue item, int threadIndex)
		{
			TValue tvalue;
			NativeParallelMultiHashMapIterator<TKey> nativeParallelMultiHashMapIterator;
			if (UnsafeParallelHashMapBase<TKey, TValue>.TryGetFirstValueAtomic(data, key, out tvalue, out nativeParallelMultiHashMapIterator))
			{
				return false;
			}
			int num = UnsafeParallelHashMapBase<TKey, TValue>.AllocEntry(data, threadIndex);
			UnsafeUtility.WriteArrayElement<TKey>((void*)data->keys, num, key);
			UnsafeUtility.WriteArrayElement<TValue>((void*)data->values, num, item);
			int num2 = key.GetHashCode() & data->bucketCapacityMask;
			int* buckets = (int*)data->buckets;
			if (Interlocked.CompareExchange(ref buckets[num2], num, -1) != -1)
			{
				int* next = (int*)data->next;
				for (;;)
				{
					int num3 = buckets[num2];
					next[num] = num3;
					if (UnsafeParallelHashMapBase<TKey, TValue>.TryGetFirstValueAtomic(data, key, out tvalue, out nativeParallelMultiHashMapIterator))
					{
						break;
					}
					if (Interlocked.CompareExchange(ref buckets[num2], num, num3) == num3)
					{
						return true;
					}
				}
				UnsafeParallelHashMapBase<TKey, TValue>.FreeEntry(data, num, threadIndex);
				return false;
			}
			return true;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00021374 File Offset: 0x0001F574
		internal unsafe static void AddAtomicMulti(UnsafeParallelHashMapData* data, TKey key, TValue item, int threadIndex)
		{
			int num = UnsafeParallelHashMapBase<TKey, TValue>.AllocEntry(data, threadIndex);
			UnsafeUtility.WriteArrayElement<TKey>((void*)data->keys, num, key);
			UnsafeUtility.WriteArrayElement<TValue>((void*)data->values, num, item);
			int num2 = key.GetHashCode() & data->bucketCapacityMask;
			int* buckets = (int*)data->buckets;
			int* next = (int*)data->next;
			int num3;
			do
			{
				num3 = buckets[num2];
				next[num] = num3;
			}
			while (Interlocked.CompareExchange(ref buckets[num2], num, num3) != num3);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x000213E8 File Offset: 0x0001F5E8
		internal unsafe static bool TryAdd(UnsafeParallelHashMapData* data, TKey key, TValue item, bool isMultiHashMap, AllocatorManager.AllocatorHandle allocation)
		{
			TValue tvalue;
			NativeParallelMultiHashMapIterator<TKey> nativeParallelMultiHashMapIterator;
			if (!isMultiHashMap && UnsafeParallelHashMapBase<TKey, TValue>.TryGetFirstValueAtomic(data, key, out tvalue, out nativeParallelMultiHashMapIterator))
			{
				return false;
			}
			int num;
			int* next;
			if (data->allocatedIndexLength >= data->keyCapacity && data->firstFreeTLS.FixedElementField < 0)
			{
				for (int i = 1; i < 128; i++)
				{
					if (*(ref data->firstFreeTLS.FixedElementField + (IntPtr)(i * 16) * 4) >= 0)
					{
						num = *(ref data->firstFreeTLS.FixedElementField + (IntPtr)(i * 16) * 4);
						next = (int*)data->next;
						*(ref data->firstFreeTLS.FixedElementField + (IntPtr)(i * 16) * 4) = next[num];
						next[num] = -1;
						data->firstFreeTLS.FixedElementField = num;
						break;
					}
				}
				if (data->firstFreeTLS.FixedElementField < 0)
				{
					int num2 = UnsafeParallelHashMapData.GrowCapacity(data->keyCapacity);
					UnsafeParallelHashMapData.ReallocateHashMap<TKey, TValue>(data, num2, UnsafeParallelHashMapData.GetBucketSize(num2), allocation);
				}
			}
			num = data->firstFreeTLS.FixedElementField;
			if (num >= 0)
			{
				data->firstFreeTLS.FixedElementField = *(int*)(data->next + (IntPtr)num * 4);
			}
			else
			{
				int allocatedIndexLength = data->allocatedIndexLength;
				data->allocatedIndexLength = allocatedIndexLength + 1;
				num = allocatedIndexLength;
			}
			UnsafeUtility.WriteArrayElement<TKey>((void*)data->keys, num, key);
			UnsafeUtility.WriteArrayElement<TValue>((void*)data->values, num, item);
			int num3 = key.GetHashCode() & data->bucketCapacityMask;
			int* buckets = (int*)data->buckets;
			next = (int*)data->next;
			next[num] = buckets[num3];
			buckets[num3] = num;
			return true;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0002156C File Offset: 0x0001F76C
		internal unsafe static int Remove(UnsafeParallelHashMapData* data, TKey key, bool isMultiHashMap)
		{
			if (data->keyCapacity == 0)
			{
				return 0;
			}
			int num = 0;
			int* buckets = (int*)data->buckets;
			int* next = (int*)data->next;
			int num2 = key.GetHashCode() & data->bucketCapacityMask;
			int num3 = -1;
			int num4 = buckets[num2];
			while (num4 >= 0 && num4 < data->keyCapacity)
			{
				TKey tkey = UnsafeUtility.ReadArrayElement<TKey>((void*)data->keys, num4);
				if (tkey.Equals(key))
				{
					num++;
					if (num3 < 0)
					{
						buckets[num2] = next[num4];
					}
					else
					{
						next[num3] = next[num4];
					}
					int num5 = next[num4];
					next[num4] = data->firstFreeTLS.FixedElementField;
					data->firstFreeTLS.FixedElementField = num4;
					num4 = num5;
					if (!isMultiHashMap)
					{
						break;
					}
				}
				else
				{
					num3 = num4;
					num4 = next[num4];
				}
			}
			return num;
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00021654 File Offset: 0x0001F854
		internal unsafe static void Remove(UnsafeParallelHashMapData* data, NativeParallelMultiHashMapIterator<TKey> it)
		{
			int* buckets = (int*)data->buckets;
			int* next = (int*)data->next;
			int num = it.key.GetHashCode() & data->bucketCapacityMask;
			int num2 = buckets[num];
			if (num2 == it.EntryIndex)
			{
				buckets[num] = next[num2];
			}
			else
			{
				while (num2 >= 0 && next[num2] != it.EntryIndex)
				{
					num2 = next[num2];
				}
				next[num2] = next[it.EntryIndex];
			}
			next[it.EntryIndex] = data->firstFreeTLS.FixedElementField;
			data->firstFreeTLS.FixedElementField = it.EntryIndex;
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00021708 File Offset: 0x0001F908
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		internal unsafe static void RemoveKeyValue<TValueEQ>(UnsafeParallelHashMapData* data, TKey key, TValueEQ value) where TValueEQ : struct, IEquatable<TValueEQ>
		{
			if (data->keyCapacity == 0)
			{
				return;
			}
			int* buckets = (int*)data->buckets;
			uint keyCapacity = (uint)data->keyCapacity;
			int* ptr = buckets + (key.GetHashCode() & data->bucketCapacityMask);
			int num = *ptr;
			if (num >= (int)keyCapacity)
			{
				return;
			}
			int* next = (int*)data->next;
			byte* keys = data->keys;
			byte* values = data->values;
			int* ptr2 = &data->firstFreeTLS.FixedElementField;
			for (;;)
			{
				TKey tkey = UnsafeUtility.ReadArrayElement<TKey>((void*)keys, num);
				if (!tkey.Equals(key))
				{
					goto IL_B4;
				}
				TValueEQ tvalueEQ = UnsafeUtility.ReadArrayElement<TValueEQ>((void*)values, num);
				if (!tvalueEQ.Equals(value))
				{
					goto IL_B4;
				}
				int num2 = next[num];
				next[num] = *ptr2;
				*ptr2 = num;
				num = (*ptr = num2);
				IL_BF:
				if (num >= (int)keyCapacity)
				{
					break;
				}
				continue;
				IL_B4:
				ptr = next + num;
				num = *ptr;
				goto IL_BF;
			}
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x000217D8 File Offset: 0x0001F9D8
		internal unsafe static bool TryGetFirstValueAtomic(UnsafeParallelHashMapData* data, TKey key, out TValue item, out NativeParallelMultiHashMapIterator<TKey> it)
		{
			it.key = key;
			if (data->allocatedIndexLength <= 0)
			{
				it.EntryIndex = (it.NextEntryIndex = -1);
				item = default(TValue);
				return false;
			}
			int* buckets = (int*)data->buckets;
			int num = key.GetHashCode() & data->bucketCapacityMask;
			it.EntryIndex = (it.NextEntryIndex = buckets[num]);
			return UnsafeParallelHashMapBase<TKey, TValue>.TryGetNextValueAtomic(data, out item, ref it);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00021848 File Offset: 0x0001FA48
		internal unsafe static bool TryGetNextValueAtomic(UnsafeParallelHashMapData* data, out TValue item, ref NativeParallelMultiHashMapIterator<TKey> it)
		{
			int num = it.NextEntryIndex;
			it.NextEntryIndex = -1;
			it.EntryIndex = -1;
			item = default(TValue);
			if (num < 0 || num >= data->keyCapacity)
			{
				return false;
			}
			int* next = (int*)data->next;
			do
			{
				TKey tkey = UnsafeUtility.ReadArrayElement<TKey>((void*)data->keys, num);
				if (tkey.Equals(it.key))
				{
					goto Block_3;
				}
				num = next[num];
			}
			while (num >= 0 && num < data->keyCapacity);
			return false;
			Block_3:
			it.NextEntryIndex = next[num];
			it.EntryIndex = num;
			item = UnsafeUtility.ReadArrayElement<TValue>((void*)data->values, num);
			return true;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x000218EC File Offset: 0x0001FAEC
		internal unsafe static bool SetValue(UnsafeParallelHashMapData* data, ref NativeParallelMultiHashMapIterator<TKey> it, ref TValue item)
		{
			int entryIndex = it.EntryIndex;
			if (entryIndex < 0 || entryIndex >= data->keyCapacity)
			{
				return false;
			}
			UnsafeUtility.WriteArrayElement<TValue>((void*)data->values, entryIndex, item);
			return true;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00021922 File Offset: 0x0001FB22
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckOutOfCapacity(int idx, int keyCapacity)
		{
			if (idx >= keyCapacity)
			{
				throw new InvalidOperationException(string.Format("nextPtr idx {0} beyond capacity {1}", idx, keyCapacity));
			}
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00021944 File Offset: 0x0001FB44
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private unsafe static void CheckIndexOutOfBounds(UnsafeParallelHashMapData* data, int idx)
		{
			if (idx < 0 || idx >= data->keyCapacity)
			{
				throw new InvalidOperationException("Internal HashMap error");
			}
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0002195E File Offset: 0x0001FB5E
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void ThrowFull()
		{
			throw new InvalidOperationException("HashMap is full");
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0002196A File Offset: 0x0001FB6A
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void ThrowInvalidIterator()
		{
			throw new InvalidOperationException("Invalid iterator passed to HashMap remove");
		}
	}
}
