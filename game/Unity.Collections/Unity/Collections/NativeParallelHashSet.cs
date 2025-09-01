using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000BD RID: 189
	[DebuggerTypeProxy(typeof(NativeHashSetDebuggerTypeProxy<>))]
	[BurstCompatible(GenericTypeArguments = new Type[]
	{
		typeof(int)
	})]
	public struct NativeParallelHashSet<[IsUnmanaged] T> : INativeDisposable, IDisposable, IEnumerable<!0>, IEnumerable where T : struct, ValueType, IEquatable<T>
	{
		// Token: 0x0600073A RID: 1850 RVA: 0x000164C4 File Offset: 0x000146C4
		public NativeParallelHashSet(int capacity, AllocatorManager.AllocatorHandle allocator)
		{
			this.m_Data = new NativeParallelHashMap<T, bool>(capacity, allocator);
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x000164D3 File Offset: 0x000146D3
		public bool IsEmpty
		{
			get
			{
				return this.m_Data.IsEmpty;
			}
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x000164E0 File Offset: 0x000146E0
		public int Count()
		{
			return this.m_Data.Count();
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x000164ED File Offset: 0x000146ED
		// (set) Token: 0x0600073E RID: 1854 RVA: 0x000164FA File Offset: 0x000146FA
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

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x00016508 File Offset: 0x00014708
		public bool IsCreated
		{
			get
			{
				return this.m_Data.IsCreated;
			}
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00016515 File Offset: 0x00014715
		public void Dispose()
		{
			this.m_Data.Dispose();
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00016522 File Offset: 0x00014722
		[NotBurstCompatible]
		public JobHandle Dispose(JobHandle inputDeps)
		{
			return this.m_Data.Dispose(inputDeps);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00016530 File Offset: 0x00014730
		public void Clear()
		{
			this.m_Data.Clear();
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001653D File Offset: 0x0001473D
		public bool Add(T item)
		{
			return this.m_Data.TryAdd(item, false);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001654C File Offset: 0x0001474C
		public bool Remove(T item)
		{
			return this.m_Data.Remove(item);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001655A File Offset: 0x0001475A
		public bool Contains(T item)
		{
			return this.m_Data.ContainsKey(item);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00016568 File Offset: 0x00014768
		public NativeArray<T> ToNativeArray(AllocatorManager.AllocatorHandle allocator)
		{
			return this.m_Data.GetKeyArray(allocator);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00016578 File Offset: 0x00014778
		public NativeParallelHashSet<T>.ParallelWriter AsParallelWriter()
		{
			NativeParallelHashSet<T>.ParallelWriter result;
			result.m_Data = this.m_Data.AsParallelWriter();
			return result;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00016598 File Offset: 0x00014798
		public NativeParallelHashSet<T>.Enumerator GetEnumerator()
		{
			return new NativeParallelHashSet<T>.Enumerator
			{
				m_Enumerator = new UnsafeParallelHashMapDataEnumerator(this.m_Data.m_HashMapData.m_Buffer)
			};
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400028B RID: 651
		internal NativeParallelHashMap<T, bool> m_Data;

		// Token: 0x020000BE RID: 190
		[NativeContainerIsAtomicWriteOnly]
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public struct ParallelWriter
		{
			// Token: 0x170000CB RID: 203
			// (get) Token: 0x0600074B RID: 1867 RVA: 0x000165CA File Offset: 0x000147CA
			public int Capacity
			{
				get
				{
					return this.m_Data.Capacity;
				}
			}

			// Token: 0x0600074C RID: 1868 RVA: 0x000165D7 File Offset: 0x000147D7
			public bool Add(T item)
			{
				return this.m_Data.TryAdd(item, false);
			}

			// Token: 0x0400028C RID: 652
			internal NativeParallelHashMap<T, bool>.ParallelWriter m_Data;
		}

		// Token: 0x020000BF RID: 191
		[NativeContainer]
		[NativeContainerIsReadOnly]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x0600074D RID: 1869 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x0600074E RID: 1870 RVA: 0x000165E6 File Offset: 0x000147E6
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x0600074F RID: 1871 RVA: 0x000165F3 File Offset: 0x000147F3
			public void Reset()
			{
				this.m_Enumerator.Reset();
			}

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x06000750 RID: 1872 RVA: 0x00016600 File Offset: 0x00014800
			public T Current
			{
				get
				{
					return this.m_Enumerator.GetCurrentKey<T>();
				}
			}

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x06000751 RID: 1873 RVA: 0x0001660D File Offset: 0x0001480D
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0400028D RID: 653
			internal UnsafeParallelHashMapDataEnumerator m_Enumerator;
		}
	}
}
