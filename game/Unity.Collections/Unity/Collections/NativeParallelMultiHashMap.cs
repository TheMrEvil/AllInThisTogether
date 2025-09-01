using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000C3 RID: 195
	[NativeContainer]
	[DebuggerTypeProxy(typeof(NativeParallelMultiHashMapDebuggerTypeProxy<, >))]
	[BurstCompatible(GenericTypeArguments = new Type[]
	{
		typeof(int),
		typeof(int)
	})]
	public struct NativeParallelMultiHashMap<TKey, TValue> : INativeDisposable, IDisposable, IEnumerable<KeyValue<!0, !1>>, IEnumerable where TKey : struct, IEquatable<TKey> where TValue : struct
	{
		// Token: 0x0600076D RID: 1901 RVA: 0x0001701C File Offset: 0x0001521C
		public NativeParallelMultiHashMap(int capacity, AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeParallelMultiHashMap<TKey, TValue>(capacity, allocator, 2);
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00017027 File Offset: 0x00015227
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(AllocatorManager.AllocatorHandle)
		})]
		internal void Initialize<[IsUnmanaged] U>(int capacity, ref U allocator, int disposeSentinelStackDepth) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			this.m_MultiHashMapData = new UnsafeParallelMultiHashMap<TKey, TValue>(capacity, allocator.Handle);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00017041 File Offset: 0x00015241
		private NativeParallelMultiHashMap(int capacity, AllocatorManager.AllocatorHandle allocator, int disposeSentinelStackDepth)
		{
			this = default(NativeParallelMultiHashMap<TKey, TValue>);
			this.Initialize<AllocatorManager.AllocatorHandle>(capacity, ref allocator, disposeSentinelStackDepth);
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x00017054 File Offset: 0x00015254
		public bool IsEmpty
		{
			get
			{
				return this.m_MultiHashMapData.IsEmpty;
			}
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00017061 File Offset: 0x00015261
		public int Count()
		{
			return this.m_MultiHashMapData.Count();
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x0001706E File Offset: 0x0001526E
		// (set) Token: 0x06000773 RID: 1907 RVA: 0x0001707B File Offset: 0x0001527B
		public int Capacity
		{
			get
			{
				return this.m_MultiHashMapData.Capacity;
			}
			set
			{
				this.m_MultiHashMapData.Capacity = value;
			}
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00017089 File Offset: 0x00015289
		public void Clear()
		{
			this.m_MultiHashMapData.Clear();
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00017096 File Offset: 0x00015296
		public void Add(TKey key, TValue item)
		{
			this.m_MultiHashMapData.Add(key, item);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x000170A5 File Offset: 0x000152A5
		public int Remove(TKey key)
		{
			return this.m_MultiHashMapData.Remove(key);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x000170B3 File Offset: 0x000152B3
		public void Remove(NativeParallelMultiHashMapIterator<TKey> it)
		{
			this.m_MultiHashMapData.Remove(it);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x000170C1 File Offset: 0x000152C1
		public bool TryGetFirstValue(TKey key, out TValue item, out NativeParallelMultiHashMapIterator<TKey> it)
		{
			return this.m_MultiHashMapData.TryGetFirstValue(key, out item, out it);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x000170D1 File Offset: 0x000152D1
		public bool TryGetNextValue(out TValue item, ref NativeParallelMultiHashMapIterator<TKey> it)
		{
			return this.m_MultiHashMapData.TryGetNextValue(out item, ref it);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x000170E0 File Offset: 0x000152E0
		public bool ContainsKey(TKey key)
		{
			TValue tvalue;
			NativeParallelMultiHashMapIterator<TKey> nativeParallelMultiHashMapIterator;
			return this.TryGetFirstValue(key, out tvalue, out nativeParallelMultiHashMapIterator);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x000170F8 File Offset: 0x000152F8
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

		// Token: 0x0600077C RID: 1916 RVA: 0x00017129 File Offset: 0x00015329
		public bool SetValue(TValue item, NativeParallelMultiHashMapIterator<TKey> it)
		{
			return this.m_MultiHashMapData.SetValue(item, it);
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x00017138 File Offset: 0x00015338
		public bool IsCreated
		{
			get
			{
				return this.m_MultiHashMapData.IsCreated;
			}
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00017145 File Offset: 0x00015345
		public void Dispose()
		{
			this.m_MultiHashMapData.Dispose();
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00017154 File Offset: 0x00015354
		[NotBurstCompatible]
		public JobHandle Dispose(JobHandle inputDeps)
		{
			JobHandle result = new UnsafeParallelHashMapDataDisposeJob
			{
				Data = new UnsafeParallelHashMapDataDispose
				{
					m_Buffer = this.m_MultiHashMapData.m_Buffer,
					m_AllocatorLabel = this.m_MultiHashMapData.m_AllocatorLabel
				}
			}.Schedule(inputDeps);
			this.m_MultiHashMapData.m_Buffer = null;
			return result;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x000171B1 File Offset: 0x000153B1
		public NativeArray<TKey> GetKeyArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_MultiHashMapData.GetKeyArray(allocator);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x000171BF File Offset: 0x000153BF
		public NativeArray<TValue> GetValueArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_MultiHashMapData.GetValueArray(allocator);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x000171CD File Offset: 0x000153CD
		public NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_MultiHashMapData.GetKeyValueArrays(allocator);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x000171DC File Offset: 0x000153DC
		public NativeParallelMultiHashMap<TKey, TValue>.ParallelWriter AsParallelWriter()
		{
			NativeParallelMultiHashMap<TKey, TValue>.ParallelWriter result;
			result.m_Writer = this.m_MultiHashMapData.AsParallelWriter();
			return result;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x000171FC File Offset: 0x000153FC
		public NativeParallelMultiHashMap<TKey, TValue>.Enumerator GetValuesForKey(TKey key)
		{
			return new NativeParallelMultiHashMap<TKey, TValue>.Enumerator
			{
				hashmap = this,
				key = key,
				isFirst = true
			};
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00017230 File Offset: 0x00015430
		public NativeParallelMultiHashMap<TKey, TValue>.KeyValueEnumerator GetEnumerator()
		{
			return new NativeParallelMultiHashMap<TKey, TValue>.KeyValueEnumerator
			{
				m_Enumerator = new UnsafeParallelHashMapDataEnumerator(this.m_MultiHashMapData.m_Buffer)
			};
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator<KeyValue<TKey, TValue>> IEnumerable<KeyValue<!0, !1>>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckRead()
		{
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckWrite()
		{
		}

		// Token: 0x04000292 RID: 658
		internal UnsafeParallelMultiHashMap<TKey, TValue> m_MultiHashMapData;

		// Token: 0x020000C4 RID: 196
		[NativeContainer]
		[NativeContainerIsAtomicWriteOnly]
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public struct ParallelWriter
		{
			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x0600078A RID: 1930 RVA: 0x0001725D File Offset: 0x0001545D
			public int m_ThreadIndex
			{
				get
				{
					return this.m_Writer.m_ThreadIndex;
				}
			}

			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x0600078B RID: 1931 RVA: 0x0001726A File Offset: 0x0001546A
			public int Capacity
			{
				get
				{
					return this.m_Writer.Capacity;
				}
			}

			// Token: 0x0600078C RID: 1932 RVA: 0x00017277 File Offset: 0x00015477
			public void Add(TKey key, TValue item)
			{
				this.m_Writer.Add(key, item);
			}

			// Token: 0x04000293 RID: 659
			internal UnsafeParallelMultiHashMap<TKey, TValue>.ParallelWriter m_Writer;
		}

		// Token: 0x020000C5 RID: 197
		public struct Enumerator : IEnumerator<TValue>, IEnumerator, IDisposable
		{
			// Token: 0x0600078D RID: 1933 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x0600078E RID: 1934 RVA: 0x00017288 File Offset: 0x00015488
			public bool MoveNext()
			{
				if (this.isFirst)
				{
					this.isFirst = false;
					return this.hashmap.TryGetFirstValue(this.key, out this.value, out this.iterator);
				}
				return this.hashmap.TryGetNextValue(out this.value, ref this.iterator);
			}

			// Token: 0x0600078F RID: 1935 RVA: 0x000172D9 File Offset: 0x000154D9
			public void Reset()
			{
				this.isFirst = true;
			}

			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x06000790 RID: 1936 RVA: 0x000172E2 File Offset: 0x000154E2
			public TValue Current
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x06000791 RID: 1937 RVA: 0x000172EA File Offset: 0x000154EA
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000792 RID: 1938 RVA: 0x000172F7 File Offset: 0x000154F7
			public NativeParallelMultiHashMap<TKey, TValue>.Enumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x04000294 RID: 660
			internal NativeParallelMultiHashMap<TKey, TValue> hashmap;

			// Token: 0x04000295 RID: 661
			internal TKey key;

			// Token: 0x04000296 RID: 662
			internal bool isFirst;

			// Token: 0x04000297 RID: 663
			private TValue value;

			// Token: 0x04000298 RID: 664
			private NativeParallelMultiHashMapIterator<TKey> iterator;
		}

		// Token: 0x020000C6 RID: 198
		[NativeContainer]
		[NativeContainerIsReadOnly]
		public struct KeyValueEnumerator : IEnumerator<KeyValue<TKey, TValue>>, IEnumerator, IDisposable
		{
			// Token: 0x06000793 RID: 1939 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x06000794 RID: 1940 RVA: 0x000172FF File Offset: 0x000154FF
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x06000795 RID: 1941 RVA: 0x0001730C File Offset: 0x0001550C
			public void Reset()
			{
				this.m_Enumerator.Reset();
			}

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x06000796 RID: 1942 RVA: 0x00017319 File Offset: 0x00015519
			public KeyValue<TKey, TValue> Current
			{
				get
				{
					return this.m_Enumerator.GetCurrent<TKey, TValue>();
				}
			}

			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x06000797 RID: 1943 RVA: 0x00017326 File Offset: 0x00015526
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000299 RID: 665
			internal UnsafeParallelHashMapDataEnumerator m_Enumerator;
		}
	}
}
