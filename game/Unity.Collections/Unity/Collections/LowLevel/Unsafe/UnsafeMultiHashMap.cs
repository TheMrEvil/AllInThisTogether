using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000111 RID: 273
	[Obsolete("UnsafeMultiHashMap is renamed to UnsafeParallelMultiHashMap. (UnityUpgradable) -> UnsafeParallelMultiHashMap<TKey, TValue>", false)]
	public struct UnsafeMultiHashMap<TKey, TValue> : INativeDisposable, IDisposable, IEnumerable<KeyValue<!0, !1>>, IEnumerable where TKey : struct, IEquatable<TKey> where TValue : struct
	{
		// Token: 0x06000A14 RID: 2580 RVA: 0x0001E47A File Offset: 0x0001C67A
		public UnsafeMultiHashMap(int capacity, AllocatorManager.AllocatorHandle allocator)
		{
			this.m_AllocatorLabel = allocator;
			UnsafeParallelHashMapData.AllocateHashMap<TKey, TValue>(capacity, capacity * 2, allocator, out this.m_Buffer);
			this.Clear();
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x0001E499 File Offset: 0x0001C699
		public bool IsEmpty
		{
			get
			{
				return !this.IsCreated || UnsafeParallelHashMapData.IsEmpty(this.m_Buffer);
			}
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0001E4B0 File Offset: 0x0001C6B0
		public unsafe int Count()
		{
			if (this.m_Buffer->allocatedIndexLength <= 0)
			{
				return 0;
			}
			return UnsafeParallelHashMapData.GetCount(this.m_Buffer);
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x0001E4CD File Offset: 0x0001C6CD
		// (set) Token: 0x06000A18 RID: 2584 RVA: 0x0001E4DA File Offset: 0x0001C6DA
		public unsafe int Capacity
		{
			get
			{
				return this.m_Buffer->keyCapacity;
			}
			set
			{
				UnsafeParallelHashMapData.ReallocateHashMap<TKey, TValue>(this.m_Buffer, value, UnsafeParallelHashMapData.GetBucketSize(value), this.m_AllocatorLabel);
			}
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0001E4F4 File Offset: 0x0001C6F4
		public void Clear()
		{
			UnsafeParallelHashMapBase<TKey, TValue>.Clear(this.m_Buffer);
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0001E501 File Offset: 0x0001C701
		public void Add(TKey key, TValue item)
		{
			UnsafeParallelHashMapBase<TKey, TValue>.TryAdd(this.m_Buffer, key, item, true, this.m_AllocatorLabel);
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0001E518 File Offset: 0x0001C718
		public int Remove(TKey key)
		{
			return UnsafeParallelHashMapBase<TKey, TValue>.Remove(this.m_Buffer, key, true);
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0001E527 File Offset: 0x0001C727
		public void Remove<TValueEQ>(TKey key, TValueEQ value) where TValueEQ : struct, IEquatable<TValueEQ>
		{
			UnsafeParallelHashMapBase<TKey, TValueEQ>.RemoveKeyValue<TValueEQ>(this.m_Buffer, key, value);
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0001E536 File Offset: 0x0001C736
		public void Remove(NativeParallelMultiHashMapIterator<TKey> it)
		{
			UnsafeParallelHashMapBase<TKey, TValue>.Remove(this.m_Buffer, it);
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0001E544 File Offset: 0x0001C744
		public bool TryGetFirstValue(TKey key, out TValue item, out NativeParallelMultiHashMapIterator<TKey> it)
		{
			return UnsafeParallelHashMapBase<TKey, TValue>.TryGetFirstValueAtomic(this.m_Buffer, key, out item, out it);
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0001E554 File Offset: 0x0001C754
		public bool TryGetNextValue(out TValue item, ref NativeParallelMultiHashMapIterator<TKey> it)
		{
			return UnsafeParallelHashMapBase<TKey, TValue>.TryGetNextValueAtomic(this.m_Buffer, out item, ref it);
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0001E564 File Offset: 0x0001C764
		public bool ContainsKey(TKey key)
		{
			TValue tvalue;
			NativeParallelMultiHashMapIterator<TKey> nativeParallelMultiHashMapIterator;
			return this.TryGetFirstValue(key, out tvalue, out nativeParallelMultiHashMapIterator);
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0001E57C File Offset: 0x0001C77C
		public int CountValuesForKey(TKey key)
		{
			TValue tvalue;
			NativeParallelMultiHashMapIterator<TKey> nativeParallelMultiHashMapIterator;
			if (!this.TryGetFirstValue(key, out tvalue, out nativeParallelMultiHashMapIterator))
			{
				return 0;
			}
			int num = 1;
			while (this.TryGetNextValue(out tvalue, ref nativeParallelMultiHashMapIterator))
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0001E5AD File Offset: 0x0001C7AD
		public bool SetValue(TValue item, NativeParallelMultiHashMapIterator<TKey> it)
		{
			return UnsafeParallelHashMapBase<TKey, TValue>.SetValue(this.m_Buffer, ref it, ref item);
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x0001E5BE File Offset: 0x0001C7BE
		public bool IsCreated
		{
			get
			{
				return this.m_Buffer != null;
			}
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0001E5CD File Offset: 0x0001C7CD
		public void Dispose()
		{
			UnsafeParallelHashMapData.DeallocateHashMap(this.m_Buffer, this.m_AllocatorLabel);
			this.m_Buffer = null;
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0001E5E8 File Offset: 0x0001C7E8
		[NotBurstCompatible]
		public JobHandle Dispose(JobHandle inputDeps)
		{
			JobHandle result = new UnsafeParallelHashMapDisposeJob
			{
				Data = this.m_Buffer,
				Allocator = this.m_AllocatorLabel
			}.Schedule(inputDeps);
			this.m_Buffer = null;
			return result;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0001E628 File Offset: 0x0001C828
		public NativeArray<TKey> GetKeyArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<TKey> result = CollectionHelper.CreateNativeArray<TKey>(this.Count(), allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeParallelHashMapData.GetKeyArray<TKey>(this.m_Buffer, result);
			return result;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0001E650 File Offset: 0x0001C850
		public NativeArray<TValue> GetValueArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<TValue> result = CollectionHelper.CreateNativeArray<TValue>(this.Count(), allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeParallelHashMapData.GetValueArray<TValue>(this.m_Buffer, result);
			return result;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0001E678 File Offset: 0x0001C878
		public NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(AllocatorManager.AllocatorHandle allocator)
		{
			NativeKeyValueArrays<TKey, TValue> result = new NativeKeyValueArrays<TKey, TValue>(this.Count(), allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeParallelHashMapData.GetKeyValueArrays<TKey, TValue>(this.m_Buffer, result);
			return result;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0001E6A4 File Offset: 0x0001C8A4
		public UnsafeMultiHashMap<TKey, TValue>.Enumerator GetValuesForKey(TKey key)
		{
			return new UnsafeMultiHashMap<TKey, TValue>.Enumerator
			{
				hashmap = this,
				key = key,
				isFirst = true
			};
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0001E6D8 File Offset: 0x0001C8D8
		public UnsafeMultiHashMap<TKey, TValue>.ParallelWriter AsParallelWriter()
		{
			UnsafeMultiHashMap<TKey, TValue>.ParallelWriter result;
			result.m_ThreadIndex = 0;
			result.m_Buffer = this.m_Buffer;
			return result;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0001E6FC File Offset: 0x0001C8FC
		public UnsafeMultiHashMap<TKey, TValue>.KeyValueEnumerator GetEnumerator()
		{
			return new UnsafeMultiHashMap<TKey, TValue>.KeyValueEnumerator
			{
				m_Enumerator = new UnsafeParallelHashMapDataEnumerator(this.m_Buffer)
			};
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator<KeyValue<TKey, TValue>> IEnumerable<KeyValue<!0, !1>>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400035D RID: 861
		[NativeDisableUnsafePtrRestriction]
		internal unsafe UnsafeParallelHashMapData* m_Buffer;

		// Token: 0x0400035E RID: 862
		internal AllocatorManager.AllocatorHandle m_AllocatorLabel;

		// Token: 0x02000112 RID: 274
		public struct Enumerator : IEnumerator<TValue>, IEnumerator, IDisposable
		{
			// Token: 0x06000A2E RID: 2606 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x06000A2F RID: 2607 RVA: 0x0001E724 File Offset: 0x0001C924
			public bool MoveNext()
			{
				if (this.isFirst)
				{
					this.isFirst = false;
					return this.hashmap.TryGetFirstValue(this.key, out this.value, out this.iterator);
				}
				return this.hashmap.TryGetNextValue(out this.value, ref this.iterator);
			}

			// Token: 0x06000A30 RID: 2608 RVA: 0x0001E775 File Offset: 0x0001C975
			public void Reset()
			{
				this.isFirst = true;
			}

			// Token: 0x17000117 RID: 279
			// (get) Token: 0x06000A31 RID: 2609 RVA: 0x0001E77E File Offset: 0x0001C97E
			public TValue Current
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x06000A32 RID: 2610 RVA: 0x0001E786 File Offset: 0x0001C986
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000A33 RID: 2611 RVA: 0x0001E793 File Offset: 0x0001C993
			public UnsafeMultiHashMap<TKey, TValue>.Enumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x0400035F RID: 863
			internal UnsafeMultiHashMap<TKey, TValue> hashmap;

			// Token: 0x04000360 RID: 864
			internal TKey key;

			// Token: 0x04000361 RID: 865
			internal bool isFirst;

			// Token: 0x04000362 RID: 866
			private TValue value;

			// Token: 0x04000363 RID: 867
			private NativeParallelMultiHashMapIterator<TKey> iterator;
		}

		// Token: 0x02000113 RID: 275
		[NativeContainerIsAtomicWriteOnly]
		public struct ParallelWriter
		{
			// Token: 0x17000119 RID: 281
			// (get) Token: 0x06000A34 RID: 2612 RVA: 0x0001E79B File Offset: 0x0001C99B
			public unsafe int Capacity
			{
				get
				{
					return this.m_Buffer->keyCapacity;
				}
			}

			// Token: 0x06000A35 RID: 2613 RVA: 0x0001E7A8 File Offset: 0x0001C9A8
			public void Add(TKey key, TValue item)
			{
				UnsafeParallelHashMapBase<TKey, TValue>.AddAtomicMulti(this.m_Buffer, key, item, this.m_ThreadIndex);
			}

			// Token: 0x04000364 RID: 868
			[NativeDisableUnsafePtrRestriction]
			internal unsafe UnsafeParallelHashMapData* m_Buffer;

			// Token: 0x04000365 RID: 869
			[NativeSetThreadIndex]
			internal int m_ThreadIndex;
		}

		// Token: 0x02000114 RID: 276
		public struct KeyValueEnumerator : IEnumerator<KeyValue<TKey, TValue>>, IEnumerator, IDisposable
		{
			// Token: 0x06000A36 RID: 2614 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x06000A37 RID: 2615 RVA: 0x0001E7BD File Offset: 0x0001C9BD
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x06000A38 RID: 2616 RVA: 0x0001E7CA File Offset: 0x0001C9CA
			public void Reset()
			{
				this.m_Enumerator.Reset();
			}

			// Token: 0x1700011A RID: 282
			// (get) Token: 0x06000A39 RID: 2617 RVA: 0x0001E7D7 File Offset: 0x0001C9D7
			public KeyValue<TKey, TValue> Current
			{
				get
				{
					return this.m_Enumerator.GetCurrent<TKey, TValue>();
				}
			}

			// Token: 0x1700011B RID: 283
			// (get) Token: 0x06000A3A RID: 2618 RVA: 0x0001E7E4 File Offset: 0x0001C9E4
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000366 RID: 870
			internal UnsafeParallelHashMapDataEnumerator m_Enumerator;
		}
	}
}
