using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000B8 RID: 184
	[NativeContainer]
	[DebuggerDisplay("Count = {m_HashMapData.Count()}, Capacity = {m_HashMapData.Capacity}, IsCreated = {m_HashMapData.IsCreated}, IsEmpty = {IsEmpty}")]
	[DebuggerTypeProxy(typeof(NativeParallelHashMapDebuggerTypeProxy<, >))]
	[BurstCompatible(GenericTypeArguments = new Type[]
	{
		typeof(int),
		typeof(int)
	})]
	public struct NativeParallelHashMap<TKey, TValue> : INativeDisposable, IDisposable, IEnumerable<KeyValue<!0, !1>>, IEnumerable where TKey : struct, IEquatable<TKey> where TValue : struct
	{
		// Token: 0x0600070D RID: 1805 RVA: 0x0001610C File Offset: 0x0001430C
		public NativeParallelHashMap(int capacity, AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeParallelHashMap<TKey, TValue>(capacity, allocator, 2);
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00016117 File Offset: 0x00014317
		private NativeParallelHashMap(int capacity, AllocatorManager.AllocatorHandle allocator, int disposeSentinelStackDepth)
		{
			this.m_HashMapData = new UnsafeParallelHashMap<TKey, TValue>(capacity, allocator);
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x00016126 File Offset: 0x00014326
		public bool IsEmpty
		{
			get
			{
				return !this.IsCreated || this.m_HashMapData.IsEmpty;
			}
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001613D File Offset: 0x0001433D
		public int Count()
		{
			return this.m_HashMapData.Count();
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x0001614A File Offset: 0x0001434A
		// (set) Token: 0x06000712 RID: 1810 RVA: 0x00016157 File Offset: 0x00014357
		public int Capacity
		{
			get
			{
				return this.m_HashMapData.Capacity;
			}
			set
			{
				this.m_HashMapData.Capacity = value;
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00016165 File Offset: 0x00014365
		public void Clear()
		{
			this.m_HashMapData.Clear();
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00016172 File Offset: 0x00014372
		public bool TryAdd(TKey key, TValue item)
		{
			return this.m_HashMapData.TryAdd(key, item);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00016181 File Offset: 0x00014381
		public void Add(TKey key, TValue item)
		{
			this.TryAdd(key, item);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0001618C File Offset: 0x0001438C
		public bool Remove(TKey key)
		{
			return this.m_HashMapData.Remove(key);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0001619A File Offset: 0x0001439A
		public bool TryGetValue(TKey key, out TValue item)
		{
			return this.m_HashMapData.TryGetValue(key, out item);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x000161A9 File Offset: 0x000143A9
		public bool ContainsKey(TKey key)
		{
			return this.m_HashMapData.ContainsKey(key);
		}

		// Token: 0x170000C1 RID: 193
		public TValue this[TKey key]
		{
			get
			{
				TValue result;
				if (this.m_HashMapData.TryGetValue(key, out result))
				{
					return result;
				}
				return default(TValue);
			}
			set
			{
				this.m_HashMapData[key] = value;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x000161EF File Offset: 0x000143EF
		public bool IsCreated
		{
			get
			{
				return this.m_HashMapData.IsCreated;
			}
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x000161FC File Offset: 0x000143FC
		public void Dispose()
		{
			this.m_HashMapData.Dispose();
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001620C File Offset: 0x0001440C
		[NotBurstCompatible]
		public JobHandle Dispose(JobHandle inputDeps)
		{
			JobHandle result = new UnsafeParallelHashMapDataDisposeJob
			{
				Data = new UnsafeParallelHashMapDataDispose
				{
					m_Buffer = this.m_HashMapData.m_Buffer,
					m_AllocatorLabel = this.m_HashMapData.m_AllocatorLabel
				}
			}.Schedule(inputDeps);
			this.m_HashMapData.m_Buffer = null;
			return result;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00016269 File Offset: 0x00014469
		public NativeArray<TKey> GetKeyArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_HashMapData.GetKeyArray(allocator);
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00016277 File Offset: 0x00014477
		public NativeArray<TValue> GetValueArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_HashMapData.GetValueArray(allocator);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00016285 File Offset: 0x00014485
		public NativeKeyValueArrays<TKey, TValue> GetKeyValueArrays(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_HashMapData.GetKeyValueArrays(allocator);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00016294 File Offset: 0x00014494
		public NativeParallelHashMap<TKey, TValue>.ParallelWriter AsParallelWriter()
		{
			NativeParallelHashMap<TKey, TValue>.ParallelWriter result;
			result.m_Writer = this.m_HashMapData.AsParallelWriter();
			return result;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x000162B4 File Offset: 0x000144B4
		public NativeParallelHashMap<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new NativeParallelHashMap<TKey, TValue>.Enumerator
			{
				m_Enumerator = new UnsafeParallelHashMapDataEnumerator(this.m_HashMapData.m_Buffer)
			};
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator<KeyValue<TKey, TValue>> IEnumerable<KeyValue<!0, !1>>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckRead()
		{
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckWrite()
		{
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00004A78 File Offset: 0x00002C78
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void ThrowKeyNotPresent(TKey key)
		{
			throw new ArgumentException(string.Format("Key: {0} is not present in the NativeParallelHashMap.", key));
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00004A8F File Offset: 0x00002C8F
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void ThrowKeyAlreadyAdded(TKey key)
		{
			throw new ArgumentException("An item with the same key has already been added", "key");
		}

		// Token: 0x04000287 RID: 647
		internal UnsafeParallelHashMap<TKey, TValue> m_HashMapData;

		// Token: 0x020000B9 RID: 185
		[NativeContainer]
		[NativeContainerIsAtomicWriteOnly]
		[DebuggerDisplay("Capacity = {m_Writer.Capacity}")]
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public struct ParallelWriter
		{
			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x06000729 RID: 1833 RVA: 0x000162E1 File Offset: 0x000144E1
			public int m_ThreadIndex
			{
				get
				{
					return this.m_Writer.m_ThreadIndex;
				}
			}

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x0600072A RID: 1834 RVA: 0x000162EE File Offset: 0x000144EE
			public int Capacity
			{
				get
				{
					return this.m_Writer.Capacity;
				}
			}

			// Token: 0x0600072B RID: 1835 RVA: 0x000162FB File Offset: 0x000144FB
			public bool TryAdd(TKey key, TValue item)
			{
				return this.m_Writer.TryAdd(key, item);
			}

			// Token: 0x04000288 RID: 648
			internal UnsafeParallelHashMap<TKey, TValue>.ParallelWriter m_Writer;
		}

		// Token: 0x020000BA RID: 186
		[NativeContainer]
		[NativeContainerIsReadOnly]
		public struct Enumerator : IEnumerator<KeyValue<TKey, TValue>>, IEnumerator, IDisposable
		{
			// Token: 0x0600072C RID: 1836 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x0600072D RID: 1837 RVA: 0x0001630A File Offset: 0x0001450A
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x0600072E RID: 1838 RVA: 0x00016317 File Offset: 0x00014517
			public void Reset()
			{
				this.m_Enumerator.Reset();
			}

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x0600072F RID: 1839 RVA: 0x00016324 File Offset: 0x00014524
			public KeyValue<TKey, TValue> Current
			{
				get
				{
					return this.m_Enumerator.GetCurrent<TKey, TValue>();
				}
			}

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x06000730 RID: 1840 RVA: 0x00016331 File Offset: 0x00014531
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000289 RID: 649
			internal UnsafeParallelHashMapDataEnumerator m_Enumerator;
		}
	}
}
