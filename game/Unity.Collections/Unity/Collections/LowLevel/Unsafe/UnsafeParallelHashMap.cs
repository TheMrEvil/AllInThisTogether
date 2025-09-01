using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000136 RID: 310
	[DebuggerDisplay("Count = {Count()}, Capacity = {Capacity}, IsCreated = {IsCreated}, IsEmpty = {IsEmpty}")]
	[DebuggerTypeProxy(typeof(UnsafeParallelHashMapDebuggerTypeProxy<, >))]
	[BurstCompatible(GenericTypeArguments = new Type[]
	{
		typeof(int),
		typeof(int)
	})]
	public struct UnsafeParallelHashMap<TKey, TValue> : INativeDisposable, IDisposable, IEnumerable<KeyValue<!0, !1>>, IEnumerable where TKey : struct, IEquatable<TKey> where TValue : struct
	{
		// Token: 0x06000B40 RID: 2880 RVA: 0x00021B0A File Offset: 0x0001FD0A
		public UnsafeParallelHashMap(int capacity, AllocatorManager.AllocatorHandle allocator)
		{
			this.m_AllocatorLabel = allocator;
			UnsafeParallelHashMapData.AllocateHashMap<TKey, TValue>(capacity, capacity * 2, allocator, out this.m_Buffer);
			this.Clear();
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x00021B29 File Offset: 0x0001FD29
		public bool IsEmpty
		{
			get
			{
				return !this.IsCreated || UnsafeParallelHashMapData.IsEmpty(this.m_Buffer);
			}
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00021B40 File Offset: 0x0001FD40
		public int Count()
		{
			return UnsafeParallelHashMapData.GetCount(this.m_Buffer);
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x00021B4D File Offset: 0x0001FD4D
		// (set) Token: 0x06000B44 RID: 2884 RVA: 0x00021B5A File Offset: 0x0001FD5A
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

		// Token: 0x06000B45 RID: 2885 RVA: 0x00021B74 File Offset: 0x0001FD74
		public void Clear()
		{
			UnsafeParallelHashMapBase<TKey, TValue>.Clear(this.m_Buffer);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00021B81 File Offset: 0x0001FD81
		public bool TryAdd(TKey key, TValue item)
		{
			return UnsafeParallelHashMapBase<TKey, TValue>.TryAdd(this.m_Buffer, key, item, false, this.m_AllocatorLabel);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00021B97 File Offset: 0x0001FD97
		public void Add(TKey key, TValue item)
		{
			this.TryAdd(key, item);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00021BA2 File Offset: 0x0001FDA2
		public bool Remove(TKey key)
		{
			return UnsafeParallelHashMapBase<TKey, TValue>.Remove(this.m_Buffer, key, false) != 0;
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00021BB4 File Offset: 0x0001FDB4
		public bool TryGetValue(TKey key, out TValue item)
		{
			NativeParallelMultiHashMapIterator<TKey> nativeParallelMultiHashMapIterator;
			return UnsafeParallelHashMapBase<TKey, TValue>.TryGetFirstValueAtomic(this.m_Buffer, key, out item, out nativeParallelMultiHashMapIterator);
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00021BD0 File Offset: 0x0001FDD0
		public bool ContainsKey(TKey key)
		{
			TValue tvalue;
			NativeParallelMultiHashMapIterator<TKey> nativeParallelMultiHashMapIterator;
			return UnsafeParallelHashMapBase<TKey, TValue>.TryGetFirstValueAtomic(this.m_Buffer, key, out tvalue, out nativeParallelMultiHashMapIterator);
		}

		// Token: 0x1700013B RID: 315
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

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x00021C4D File Offset: 0x0001FE4D
		public bool IsCreated
		{
			get
			{
				return this.m_Buffer != null;
			}
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00021C5C File Offset: 0x0001FE5C
		public void Dispose()
		{
			UnsafeParallelHashMapData.DeallocateHashMap(this.m_Buffer, this.m_AllocatorLabel);
			this.m_Buffer = null;
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00021C78 File Offset: 0x0001FE78
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

		// Token: 0x06000B50 RID: 2896 RVA: 0x00021CB8 File Offset: 0x0001FEB8
		public NativeArray<TKey> GetKeyArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<TKey> result = CollectionHelper.CreateNativeArray<TKey>(UnsafeParallelHashMapData.GetCount(this.m_Buffer), allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeParallelHashMapData.GetKeyArray<TKey>(this.m_Buffer, result);
			return result;
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x00021CE8 File Offset: 0x0001FEE8
		public NativeArray<TValue> GetValueArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<TValue> result = CollectionHelper.CreateNativeArray<TValue>(UnsafeParallelHashMapData.GetCount(this.m_Buffer), allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeParallelHashMapData.GetValueArray<TValue>(this.m_Buffer, result);
			return result;
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00021D18 File Offset: 0x0001FF18
		public NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(AllocatorManager.AllocatorHandle allocator)
		{
			NativeKeyValueArrays<TKey, TValue> result = new NativeKeyValueArrays<TKey, TValue>(UnsafeParallelHashMapData.GetCount(this.m_Buffer), allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeParallelHashMapData.GetKeyValueArrays<TKey, TValue>(this.m_Buffer, result);
			return result;
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x00021D48 File Offset: 0x0001FF48
		public UnsafeParallelHashMap<TKey, TValue>.ParallelWriter AsParallelWriter()
		{
			UnsafeParallelHashMap<TKey, TValue>.ParallelWriter result;
			result.m_ThreadIndex = 0;
			result.m_Buffer = this.m_Buffer;
			return result;
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00021D6C File Offset: 0x0001FF6C
		public UnsafeParallelHashMap<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new UnsafeParallelHashMap<TKey, TValue>.Enumerator
			{
				m_Enumerator = new UnsafeParallelHashMapDataEnumerator(this.m_Buffer)
			};
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator<KeyValue<TKey, TValue>> IEnumerable<KeyValue<!0, !1>>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040003B1 RID: 945
		[NativeDisableUnsafePtrRestriction]
		internal unsafe UnsafeParallelHashMapData* m_Buffer;

		// Token: 0x040003B2 RID: 946
		internal AllocatorManager.AllocatorHandle m_AllocatorLabel;

		// Token: 0x02000137 RID: 311
		[NativeContainerIsAtomicWriteOnly]
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public struct ParallelWriter
		{
			// Token: 0x1700013D RID: 317
			// (get) Token: 0x06000B57 RID: 2903 RVA: 0x00021D94 File Offset: 0x0001FF94
			public unsafe int Capacity
			{
				get
				{
					return this.m_Buffer->keyCapacity;
				}
			}

			// Token: 0x06000B58 RID: 2904 RVA: 0x00021DA1 File Offset: 0x0001FFA1
			public bool TryAdd(TKey key, TValue item)
			{
				return UnsafeParallelHashMapBase<TKey, TValue>.TryAddAtomic(this.m_Buffer, key, item, this.m_ThreadIndex);
			}

			// Token: 0x040003B3 RID: 947
			[NativeDisableUnsafePtrRestriction]
			internal unsafe UnsafeParallelHashMapData* m_Buffer;

			// Token: 0x040003B4 RID: 948
			[NativeSetThreadIndex]
			internal int m_ThreadIndex;
		}

		// Token: 0x02000138 RID: 312
		public struct Enumerator : IEnumerator<KeyValue<TKey, TValue>>, IEnumerator, IDisposable
		{
			// Token: 0x06000B59 RID: 2905 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x06000B5A RID: 2906 RVA: 0x00021DB6 File Offset: 0x0001FFB6
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x06000B5B RID: 2907 RVA: 0x00021DC3 File Offset: 0x0001FFC3
			public void Reset()
			{
				this.m_Enumerator.Reset();
			}

			// Token: 0x1700013E RID: 318
			// (get) Token: 0x06000B5C RID: 2908 RVA: 0x00021DD0 File Offset: 0x0001FFD0
			public KeyValue<TKey, TValue> Current
			{
				get
				{
					return this.m_Enumerator.GetCurrent<TKey, TValue>();
				}
			}

			// Token: 0x1700013F RID: 319
			// (get) Token: 0x06000B5D RID: 2909 RVA: 0x00021DDD File Offset: 0x0001FFDD
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040003B5 RID: 949
			internal UnsafeParallelHashMapDataEnumerator m_Enumerator;
		}
	}
}
