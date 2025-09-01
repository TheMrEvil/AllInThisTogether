using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x0200004F RID: 79
	[Obsolete("NativeHashSet is renamed to NativeParallelHashSet. (UnityUpgradable) -> NativeParallelHashSet<T>", false)]
	public struct NativeHashSet<[IsUnmanaged] T> : INativeDisposable, IDisposable, IEnumerable<!0>, IEnumerable where T : struct, ValueType, IEquatable<T>
	{
		// Token: 0x0600018C RID: 396 RVA: 0x00004E17 File Offset: 0x00003017
		public NativeHashSet(int capacity, AllocatorManager.AllocatorHandle allocator)
		{
			this.m_Data = new NativeParallelHashMap<T, bool>(capacity, allocator);
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00004E26 File Offset: 0x00003026
		public bool IsEmpty
		{
			get
			{
				return this.m_Data.IsEmpty;
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00004E33 File Offset: 0x00003033
		public int Count()
		{
			return this.m_Data.Count();
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00004E40 File Offset: 0x00003040
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00004E4D File Offset: 0x0000304D
		public int Capacity
		{
			get
			{
				return this.m_Data.Capacity;
			}
			set
			{
				this.m_Data.Capacity = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00004E5B File Offset: 0x0000305B
		public bool IsCreated
		{
			get
			{
				return this.m_Data.IsCreated;
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00004E68 File Offset: 0x00003068
		public void Dispose()
		{
			this.m_Data.Dispose();
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00004E75 File Offset: 0x00003075
		[NotBurstCompatible]
		public JobHandle Dispose(JobHandle inputDeps)
		{
			return this.m_Data.Dispose(inputDeps);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00004E83 File Offset: 0x00003083
		public void Clear()
		{
			this.m_Data.Clear();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00004E90 File Offset: 0x00003090
		public bool Add(T item)
		{
			return this.m_Data.TryAdd(item, false);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00004E9F File Offset: 0x0000309F
		public bool Remove(T item)
		{
			return this.m_Data.Remove(item);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00004EAD File Offset: 0x000030AD
		public bool Contains(T item)
		{
			return this.m_Data.ContainsKey(item);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00004EBB File Offset: 0x000030BB
		public NativeArray<T> ToNativeArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_Data.GetKeyArray(allocator);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00004ECC File Offset: 0x000030CC
		public NativeHashSet<T>.ParallelWriter AsParallelWriter()
		{
			NativeHashSet<T>.ParallelWriter result;
			result.m_Data = this.m_Data.AsParallelWriter();
			return result;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00004EEC File Offset: 0x000030EC
		public NativeHashSet<T>.Enumerator GetEnumerator()
		{
			return new NativeHashSet<T>.Enumerator
			{
				m_Enumerator = new UnsafeParallelHashMapDataEnumerator(this.m_Data.m_HashMapData.m_Buffer)
			};
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040000AC RID: 172
		internal NativeParallelHashMap<T, bool> m_Data;

		// Token: 0x02000050 RID: 80
		[NativeContainerIsAtomicWriteOnly]
		public struct ParallelWriter
		{
			// Token: 0x17000035 RID: 53
			// (get) Token: 0x0600019D RID: 413 RVA: 0x00004F1E File Offset: 0x0000311E
			public int Capacity
			{
				get
				{
					return this.m_Data.Capacity;
				}
			}

			// Token: 0x0600019E RID: 414 RVA: 0x00004F2B File Offset: 0x0000312B
			public bool Add(T item)
			{
				return this.m_Data.TryAdd(item, false);
			}

			// Token: 0x040000AD RID: 173
			internal NativeParallelHashMap<T, bool>.ParallelWriter m_Data;
		}

		// Token: 0x02000051 RID: 81
		[NativeContainer]
		[NativeContainerIsReadOnly]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x0600019F RID: 415 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x060001A0 RID: 416 RVA: 0x00004F3A File Offset: 0x0000313A
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x060001A1 RID: 417 RVA: 0x00004F47 File Offset: 0x00003147
			public void Reset()
			{
				this.m_Enumerator.Reset();
			}

			// Token: 0x17000036 RID: 54
			// (get) Token: 0x060001A2 RID: 418 RVA: 0x00004F54 File Offset: 0x00003154
			public T Current
			{
				get
				{
					return this.m_Enumerator.GetCurrentKey<T>();
				}
			}

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x060001A3 RID: 419 RVA: 0x00004F61 File Offset: 0x00003161
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040000AE RID: 174
			internal UnsafeParallelHashMapDataEnumerator m_Enumerator;
		}
	}
}
