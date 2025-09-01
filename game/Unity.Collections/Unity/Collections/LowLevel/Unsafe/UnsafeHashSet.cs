using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000115 RID: 277
	[Obsolete("UnsafeHashSet is renamed to UnsafeParallelHashSet. (UnityUpgradable) -> UnsafeParallelHashSet<T>", false)]
	public struct UnsafeHashSet<[IsUnmanaged] T> : INativeDisposable, IDisposable, IEnumerable<!0>, IEnumerable where T : struct, ValueType, IEquatable<T>
	{
		// Token: 0x06000A3B RID: 2619 RVA: 0x0001E7F1 File Offset: 0x0001C9F1
		public UnsafeHashSet(int capacity, AllocatorManager.AllocatorHandle allocator)
		{
			this.m_Data = new UnsafeParallelHashMap<T, bool>(capacity, allocator);
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x0001E800 File Offset: 0x0001CA00
		public bool IsEmpty
		{
			get
			{
				return this.m_Data.IsEmpty;
			}
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0001E80D File Offset: 0x0001CA0D
		public int Count()
		{
			return this.m_Data.Count();
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x0001E81A File Offset: 0x0001CA1A
		// (set) Token: 0x06000A3F RID: 2623 RVA: 0x0001E827 File Offset: 0x0001CA27
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

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x0001E835 File Offset: 0x0001CA35
		public bool IsCreated
		{
			get
			{
				return this.m_Data.IsCreated;
			}
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0001E842 File Offset: 0x0001CA42
		public void Dispose()
		{
			this.m_Data.Dispose();
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0001E84F File Offset: 0x0001CA4F
		[NotBurstCompatible]
		public JobHandle Dispose(JobHandle inputDeps)
		{
			return this.m_Data.Dispose(inputDeps);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0001E85D File Offset: 0x0001CA5D
		public void Clear()
		{
			this.m_Data.Clear();
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0001E86A File Offset: 0x0001CA6A
		public bool Add(T item)
		{
			return this.m_Data.TryAdd(item, false);
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0001E879 File Offset: 0x0001CA79
		public bool Remove(T item)
		{
			return this.m_Data.Remove(item);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0001E887 File Offset: 0x0001CA87
		public bool Contains(T item)
		{
			return this.m_Data.ContainsKey(item);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0001E895 File Offset: 0x0001CA95
		public NativeArray<T> ToNativeArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_Data.GetKeyArray(allocator);
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0001E8A4 File Offset: 0x0001CAA4
		public UnsafeHashSet<T>.ParallelWriter AsParallelWriter()
		{
			return new UnsafeHashSet<T>.ParallelWriter
			{
				m_Data = this.m_Data.AsParallelWriter()
			};
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0001E8CC File Offset: 0x0001CACC
		public UnsafeHashSet<T>.Enumerator GetEnumerator()
		{
			return new UnsafeHashSet<T>.Enumerator
			{
				m_Enumerator = new UnsafeParallelHashMapDataEnumerator(this.m_Data.m_Buffer)
			};
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000367 RID: 871
		internal UnsafeParallelHashMap<T, bool> m_Data;

		// Token: 0x02000116 RID: 278
		[NativeContainerIsAtomicWriteOnly]
		public struct ParallelWriter
		{
			// Token: 0x1700011F RID: 287
			// (get) Token: 0x06000A4C RID: 2636 RVA: 0x0001E8F9 File Offset: 0x0001CAF9
			public int Capacity
			{
				get
				{
					return this.m_Data.Capacity;
				}
			}

			// Token: 0x06000A4D RID: 2637 RVA: 0x0001E906 File Offset: 0x0001CB06
			public bool Add(T item)
			{
				return this.m_Data.TryAdd(item, false);
			}

			// Token: 0x04000368 RID: 872
			internal UnsafeParallelHashMap<T, bool>.ParallelWriter m_Data;
		}

		// Token: 0x02000117 RID: 279
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000A4E RID: 2638 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x06000A4F RID: 2639 RVA: 0x0001E915 File Offset: 0x0001CB15
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x06000A50 RID: 2640 RVA: 0x0001E922 File Offset: 0x0001CB22
			public void Reset()
			{
				this.m_Enumerator.Reset();
			}

			// Token: 0x17000120 RID: 288
			// (get) Token: 0x06000A51 RID: 2641 RVA: 0x0001E92F File Offset: 0x0001CB2F
			public T Current
			{
				get
				{
					return this.m_Enumerator.GetCurrentKey<T>();
				}
			}

			// Token: 0x17000121 RID: 289
			// (get) Token: 0x06000A52 RID: 2642 RVA: 0x0001E93C File Offset: 0x0001CB3C
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000369 RID: 873
			internal UnsafeParallelHashMapDataEnumerator m_Enumerator;
		}
	}
}
