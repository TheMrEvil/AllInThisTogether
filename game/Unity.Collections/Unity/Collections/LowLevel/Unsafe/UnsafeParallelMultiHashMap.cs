using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000141 RID: 321
	[DebuggerTypeProxy(typeof(UnsafeParallelMultiHashMapDebuggerTypeProxy<, >))]
	[BurstCompatible(GenericTypeArguments = new Type[]
	{
		typeof(int),
		typeof(int)
	})]
	public struct UnsafeParallelMultiHashMap<TKey, TValue> : INativeDisposable, IDisposable, IEnumerable<KeyValue<!0, !1>>, IEnumerable where TKey : struct, IEquatable<TKey> where TValue : struct
	{
		// Token: 0x06000B9F RID: 2975 RVA: 0x00022EA0 File Offset: 0x000210A0
		public UnsafeParallelMultiHashMap(int capacity, AllocatorManager.AllocatorHandle allocator)
		{
			this.m_AllocatorLabel = allocator;
			UnsafeParallelHashMapData.AllocateHashMap<TKey, TValue>(capacity, capacity * 2, allocator, out this.m_Buffer);
			this.Clear();
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x00022EBF File Offset: 0x000210BF
		public bool IsEmpty
		{
			get
			{
				return !this.IsCreated || UnsafeParallelHashMapData.IsEmpty(this.m_Buffer);
			}
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00022ED6 File Offset: 0x000210D6
		public unsafe int Count()
		{
			if (this.m_Buffer->allocatedIndexLength <= 0)
			{
				return 0;
			}
			return UnsafeParallelHashMapData.GetCount(this.m_Buffer);
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x00022EF3 File Offset: 0x000210F3
		// (set) Token: 0x06000BA3 RID: 2979 RVA: 0x00022F00 File Offset: 0x00021100
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

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00022F1A File Offset: 0x0002111A
		public void Clear()
		{
			UnsafeParallelHashMapBase<TKey, TValue>.Clear(this.m_Buffer);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00022F27 File Offset: 0x00021127
		public void Add(TKey key, TValue item)
		{
			UnsafeParallelHashMapBase<TKey, TValue>.TryAdd(this.m_Buffer, key, item, true, this.m_AllocatorLabel);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00022F3E File Offset: 0x0002113E
		public int Remove(TKey key)
		{
			return UnsafeParallelHashMapBase<TKey, TValue>.Remove(this.m_Buffer, key, true);
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00022F4D File Offset: 0x0002114D
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public void Remove<TValueEQ>(TKey key, TValueEQ value) where TValueEQ : struct, IEquatable<TValueEQ>
		{
			UnsafeParallelHashMapBase<TKey, TValueEQ>.RemoveKeyValue<TValueEQ>(this.m_Buffer, key, value);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00022F5C File Offset: 0x0002115C
		public void Remove(NativeParallelMultiHashMapIterator<TKey> it)
		{
			UnsafeParallelHashMapBase<TKey, TValue>.Remove(this.m_Buffer, it);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00022F6A File Offset: 0x0002116A
		public bool TryGetFirstValue(TKey key, out TValue item, out NativeParallelMultiHashMapIterator<TKey> it)
		{
			return UnsafeParallelHashMapBase<TKey, TValue>.TryGetFirstValueAtomic(this.m_Buffer, key, out item, out it);
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00022F7A File Offset: 0x0002117A
		public bool TryGetNextValue(out TValue item, ref NativeParallelMultiHashMapIterator<TKey> it)
		{
			return UnsafeParallelHashMapBase<TKey, TValue>.TryGetNextValueAtomic(this.m_Buffer, out item, ref it);
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00022F8C File Offset: 0x0002118C
		public bool ContainsKey(TKey key)
		{
			TValue tvalue;
			NativeParallelMultiHashMapIterator<TKey> nativeParallelMultiHashMapIterator;
			return this.TryGetFirstValue(key, out tvalue, out nativeParallelMultiHashMapIterator);
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00022FA4 File Offset: 0x000211A4
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

		// Token: 0x06000BAD RID: 2989 RVA: 0x00022FD5 File Offset: 0x000211D5
		public bool SetValue(TValue item, NativeParallelMultiHashMapIterator<TKey> it)
		{
			return UnsafeParallelHashMapBase<TKey, TValue>.SetValue(this.m_Buffer, ref it, ref item);
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x00022FE6 File Offset: 0x000211E6
		public bool IsCreated
		{
			get
			{
				return this.m_Buffer != null;
			}
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00022FF5 File Offset: 0x000211F5
		public void Dispose()
		{
			UnsafeParallelHashMapData.DeallocateHashMap(this.m_Buffer, this.m_AllocatorLabel);
			this.m_Buffer = null;
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00023010 File Offset: 0x00021210
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

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00023050 File Offset: 0x00021250
		public NativeArray<TKey> GetKeyArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<TKey> result = CollectionHelper.CreateNativeArray<TKey>(this.Count(), allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeParallelHashMapData.GetKeyArray<TKey>(this.m_Buffer, result);
			return result;
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00023078 File Offset: 0x00021278
		public NativeArray<TValue> GetValueArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<TValue> result = CollectionHelper.CreateNativeArray<TValue>(this.Count(), allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeParallelHashMapData.GetValueArray<TValue>(this.m_Buffer, result);
			return result;
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x000230A0 File Offset: 0x000212A0
		public NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(AllocatorManager.AllocatorHandle allocator)
		{
			NativeKeyValueArrays<TKey, TValue> result = new NativeKeyValueArrays<TKey, TValue>(this.Count(), allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeParallelHashMapData.GetKeyValueArrays<TKey, TValue>(this.m_Buffer, result);
			return result;
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x000230CC File Offset: 0x000212CC
		public UnsafeParallelMultiHashMap<TKey, TValue>.Enumerator GetValuesForKey(TKey key)
		{
			return new UnsafeParallelMultiHashMap<TKey, TValue>.Enumerator
			{
				hashmap = this,
				key = key,
				isFirst = true
			};
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00023100 File Offset: 0x00021300
		public UnsafeParallelMultiHashMap<TKey, TValue>.ParallelWriter AsParallelWriter()
		{
			UnsafeParallelMultiHashMap<TKey, TValue>.ParallelWriter result;
			result.m_ThreadIndex = 0;
			result.m_Buffer = this.m_Buffer;
			return result;
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00023124 File Offset: 0x00021324
		public UnsafeParallelMultiHashMap<TKey, TValue>.KeyValueEnumerator GetEnumerator()
		{
			return new UnsafeParallelMultiHashMap<TKey, TValue>.KeyValueEnumerator
			{
				m_Enumerator = new UnsafeParallelHashMapDataEnumerator(this.m_Buffer)
			};
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator<KeyValue<TKey, TValue>> IEnumerable<KeyValue<!0, !1>>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040003BF RID: 959
		[NativeDisableUnsafePtrRestriction]
		internal unsafe UnsafeParallelHashMapData* m_Buffer;

		// Token: 0x040003C0 RID: 960
		internal AllocatorManager.AllocatorHandle m_AllocatorLabel;

		// Token: 0x02000142 RID: 322
		public struct Enumerator : IEnumerator<TValue>, IEnumerator, IDisposable
		{
			// Token: 0x06000BB9 RID: 3001 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x06000BBA RID: 3002 RVA: 0x0002314C File Offset: 0x0002134C
			public bool MoveNext()
			{
				if (this.isFirst)
				{
					this.isFirst = false;
					return this.hashmap.TryGetFirstValue(this.key, out this.value, out this.iterator);
				}
				return this.hashmap.TryGetNextValue(out this.value, ref this.iterator);
			}

			// Token: 0x06000BBB RID: 3003 RVA: 0x0002319D File Offset: 0x0002139D
			public void Reset()
			{
				this.isFirst = true;
			}

			// Token: 0x1700014B RID: 331
			// (get) Token: 0x06000BBC RID: 3004 RVA: 0x000231A6 File Offset: 0x000213A6
			public TValue Current
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x1700014C RID: 332
			// (get) Token: 0x06000BBD RID: 3005 RVA: 0x000231AE File Offset: 0x000213AE
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000BBE RID: 3006 RVA: 0x000231BB File Offset: 0x000213BB
			public UnsafeParallelMultiHashMap<TKey, TValue>.Enumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x040003C1 RID: 961
			internal UnsafeParallelMultiHashMap<TKey, TValue> hashmap;

			// Token: 0x040003C2 RID: 962
			internal TKey key;

			// Token: 0x040003C3 RID: 963
			internal bool isFirst;

			// Token: 0x040003C4 RID: 964
			private TValue value;

			// Token: 0x040003C5 RID: 965
			private NativeParallelMultiHashMapIterator<TKey> iterator;
		}

		// Token: 0x02000143 RID: 323
		[NativeContainerIsAtomicWriteOnly]
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public struct ParallelWriter
		{
			// Token: 0x1700014D RID: 333
			// (get) Token: 0x06000BBF RID: 3007 RVA: 0x000231C3 File Offset: 0x000213C3
			public unsafe int Capacity
			{
				get
				{
					return this.m_Buffer->keyCapacity;
				}
			}

			// Token: 0x06000BC0 RID: 3008 RVA: 0x000231D0 File Offset: 0x000213D0
			public void Add(TKey key, TValue item)
			{
				UnsafeParallelHashMapBase<TKey, TValue>.AddAtomicMulti(this.m_Buffer, key, item, this.m_ThreadIndex);
			}

			// Token: 0x040003C6 RID: 966
			[NativeDisableUnsafePtrRestriction]
			internal unsafe UnsafeParallelHashMapData* m_Buffer;

			// Token: 0x040003C7 RID: 967
			[NativeSetThreadIndex]
			internal int m_ThreadIndex;
		}

		// Token: 0x02000144 RID: 324
		public struct KeyValueEnumerator : IEnumerator<KeyValue<TKey, TValue>>, IEnumerator, IDisposable
		{
			// Token: 0x06000BC1 RID: 3009 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x06000BC2 RID: 3010 RVA: 0x000231E5 File Offset: 0x000213E5
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x06000BC3 RID: 3011 RVA: 0x000231F2 File Offset: 0x000213F2
			public void Reset()
			{
				this.m_Enumerator.Reset();
			}

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x000231FF File Offset: 0x000213FF
			public KeyValue<TKey, TValue> Current
			{
				get
				{
					return this.m_Enumerator.GetCurrent<TKey, TValue>();
				}
			}

			// Token: 0x1700014F RID: 335
			// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x0002320C File Offset: 0x0002140C
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040003C8 RID: 968
			internal UnsafeParallelHashMapDataEnumerator m_Enumerator;
		}
	}
}
