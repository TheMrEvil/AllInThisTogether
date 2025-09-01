using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200010E RID: 270
	[Obsolete("UnsafeHashMap is renamed to UnsafeParallelHashMap. (UnityUpgradable) -> UnsafeParallelHashMap<TKey, TValue>", false)]
	public struct UnsafeHashMap<TKey, TValue> : INativeDisposable, IDisposable, IEnumerable<KeyValue<!0, !1>>, IEnumerable where TKey : struct, IEquatable<TKey> where TValue : struct
	{
		// Token: 0x060009F6 RID: 2550 RVA: 0x0001E198 File Offset: 0x0001C398
		public UnsafeHashMap(int capacity, AllocatorManager.AllocatorHandle allocator)
		{
			this.m_AllocatorLabel = allocator;
			UnsafeParallelHashMapData.AllocateHashMap<TKey, TValue>(capacity, capacity * 2, allocator, out this.m_Buffer);
			this.Clear();
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x0001E1B7 File Offset: 0x0001C3B7
		public bool IsEmpty
		{
			get
			{
				return !this.IsCreated || UnsafeParallelHashMapData.IsEmpty(this.m_Buffer);
			}
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0001E1CE File Offset: 0x0001C3CE
		public int Count()
		{
			return UnsafeParallelHashMapData.GetCount(this.m_Buffer);
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x0001E1DB File Offset: 0x0001C3DB
		// (set) Token: 0x060009FA RID: 2554 RVA: 0x0001E1E8 File Offset: 0x0001C3E8
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

		// Token: 0x060009FB RID: 2555 RVA: 0x0001E202 File Offset: 0x0001C402
		public void Clear()
		{
			UnsafeParallelHashMapBase<TKey, TValue>.Clear(this.m_Buffer);
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0001E20F File Offset: 0x0001C40F
		public bool TryAdd(TKey key, TValue item)
		{
			return UnsafeParallelHashMapBase<TKey, TValue>.TryAdd(this.m_Buffer, key, item, false, this.m_AllocatorLabel);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0001E225 File Offset: 0x0001C425
		public void Add(TKey key, TValue item)
		{
			this.TryAdd(key, item);
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0001E230 File Offset: 0x0001C430
		public bool Remove(TKey key)
		{
			return UnsafeParallelHashMapBase<TKey, TValue>.Remove(this.m_Buffer, key, false) != 0;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0001E244 File Offset: 0x0001C444
		public bool TryGetValue(TKey key, out TValue item)
		{
			NativeParallelMultiHashMapIterator<TKey> nativeParallelMultiHashMapIterator;
			return UnsafeParallelHashMapBase<TKey, TValue>.TryGetFirstValueAtomic(this.m_Buffer, key, out item, out nativeParallelMultiHashMapIterator);
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0001E260 File Offset: 0x0001C460
		public bool ContainsKey(TKey key)
		{
			TValue tvalue;
			NativeParallelMultiHashMapIterator<TKey> nativeParallelMultiHashMapIterator;
			return UnsafeParallelHashMapBase<TKey, TValue>.TryGetFirstValueAtomic(this.m_Buffer, key, out tvalue, out nativeParallelMultiHashMapIterator);
		}

		// Token: 0x1700010F RID: 271
		public TValue this[TKey key]
		{
			get
			{
				TValue result;
				this.TryGetValue(key, out result);
				return result;
			}
			set
			{
				TValue tvalue;
				NativeParallelMultiHashMapIterator<TKey> nativeParallelMultiHashMapIterator;
				if (UnsafeParallelHashMapBase<TKey, TValue>.TryGetFirstValueAtomic(this.m_Buffer, key, out tvalue, out nativeParallelMultiHashMapIterator))
				{
					UnsafeParallelHashMapBase<TKey, TValue>.SetValue(this.m_Buffer, ref nativeParallelMultiHashMapIterator, ref value);
					return;
				}
				UnsafeParallelHashMapBase<TKey, TValue>.TryAdd(this.m_Buffer, key, value, false, this.m_AllocatorLabel);
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x0001E2DD File Offset: 0x0001C4DD
		public bool IsCreated
		{
			get
			{
				return this.m_Buffer != null;
			}
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0001E2EC File Offset: 0x0001C4EC
		public void Dispose()
		{
			UnsafeParallelHashMapData.DeallocateHashMap(this.m_Buffer, this.m_AllocatorLabel);
			this.m_Buffer = null;
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0001E308 File Offset: 0x0001C508
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

		// Token: 0x06000A06 RID: 2566 RVA: 0x0001E348 File Offset: 0x0001C548
		public NativeArray<TKey> GetKeyArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<TKey> result = CollectionHelper.CreateNativeArray<TKey>(UnsafeParallelHashMapData.GetCount(this.m_Buffer), allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeParallelHashMapData.GetKeyArray<TKey>(this.m_Buffer, result);
			return result;
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0001E378 File Offset: 0x0001C578
		public NativeArray<TValue> GetValueArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<TValue> result = CollectionHelper.CreateNativeArray<TValue>(UnsafeParallelHashMapData.GetCount(this.m_Buffer), allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeParallelHashMapData.GetValueArray<TValue>(this.m_Buffer, result);
			return result;
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0001E3A8 File Offset: 0x0001C5A8
		public NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(AllocatorManager.AllocatorHandle allocator)
		{
			NativeKeyValueArrays<TKey, TValue> result = new NativeKeyValueArrays<TKey, TValue>(UnsafeParallelHashMapData.GetCount(this.m_Buffer), allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeParallelHashMapData.GetKeyValueArrays<TKey, TValue>(this.m_Buffer, result);
			return result;
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0001E3D8 File Offset: 0x0001C5D8
		public UnsafeHashMap<TKey, TValue>.ParallelWriter AsParallelWriter()
		{
			UnsafeHashMap<TKey, TValue>.ParallelWriter result;
			result.m_ThreadIndex = 0;
			result.m_Buffer = this.m_Buffer;
			return result;
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0001E3FC File Offset: 0x0001C5FC
		public UnsafeHashMap<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new UnsafeHashMap<TKey, TValue>.Enumerator
			{
				m_Enumerator = new UnsafeParallelHashMapDataEnumerator(this.m_Buffer)
			};
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator<KeyValue<TKey, TValue>> IEnumerable<KeyValue<!0, !1>>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000358 RID: 856
		[NativeDisableUnsafePtrRestriction]
		internal unsafe UnsafeParallelHashMapData* m_Buffer;

		// Token: 0x04000359 RID: 857
		internal AllocatorManager.AllocatorHandle m_AllocatorLabel;

		// Token: 0x0200010F RID: 271
		[NativeContainerIsAtomicWriteOnly]
		public struct ParallelWriter
		{
			// Token: 0x17000111 RID: 273
			// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0001E424 File Offset: 0x0001C624
			public unsafe int Capacity
			{
				get
				{
					return this.m_Buffer->keyCapacity;
				}
			}

			// Token: 0x06000A0E RID: 2574 RVA: 0x0001E431 File Offset: 0x0001C631
			public bool TryAdd(TKey key, TValue item)
			{
				return UnsafeParallelHashMapBase<TKey, TValue>.TryAddAtomic(this.m_Buffer, key, item, this.m_ThreadIndex);
			}

			// Token: 0x0400035A RID: 858
			[NativeDisableUnsafePtrRestriction]
			internal unsafe UnsafeParallelHashMapData* m_Buffer;

			// Token: 0x0400035B RID: 859
			[NativeSetThreadIndex]
			internal int m_ThreadIndex;
		}

		// Token: 0x02000110 RID: 272
		public struct Enumerator : IEnumerator<KeyValue<TKey, TValue>>, IEnumerator, IDisposable
		{
			// Token: 0x06000A0F RID: 2575 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x06000A10 RID: 2576 RVA: 0x0001E446 File Offset: 0x0001C646
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x06000A11 RID: 2577 RVA: 0x0001E453 File Offset: 0x0001C653
			public void Reset()
			{
				this.m_Enumerator.Reset();
			}

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x06000A12 RID: 2578 RVA: 0x0001E460 File Offset: 0x0001C660
			public KeyValue<TKey, TValue> Current
			{
				get
				{
					return this.m_Enumerator.GetCurrent<TKey, TValue>();
				}
			}

			// Token: 0x17000113 RID: 275
			// (get) Token: 0x06000A13 RID: 2579 RVA: 0x0001E46D File Offset: 0x0001C66D
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0400035C RID: 860
			internal UnsafeParallelHashMapDataEnumerator m_Enumerator;
		}
	}
}
