using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Collections.NotBurstCompatible;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000B2 RID: 178
	[NativeContainer]
	[DebuggerDisplay("Length = {Length}")]
	[DebuggerTypeProxy(typeof(NativeListDebugView<>))]
	[BurstCompatible(GenericTypeArguments = new Type[]
	{
		typeof(int)
	})]
	public struct NativeList<[IsUnmanaged] T> : INativeDisposable, IDisposable, INativeList<T>, IIndexable<T>, IEnumerable<!0>, IEnumerable where T : struct, ValueType
	{
		// Token: 0x060006C8 RID: 1736 RVA: 0x00015A8C File Offset: 0x00013C8C
		public NativeList(AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeList<T>(1, allocator, 2);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00015A97 File Offset: 0x00013C97
		public NativeList(int initialCapacity, AllocatorManager.AllocatorHandle allocator)
		{
			this = new NativeList<T>(initialCapacity, allocator, 2);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00015AA2 File Offset: 0x00013CA2
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(AllocatorManager.AllocatorHandle)
		})]
		internal void Initialize<[IsUnmanaged] U>(int initialCapacity, ref U allocator, int disposeSentinelStackDepth) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			this.m_ListData = UnsafeList<T>.Create<U>(initialCapacity, ref allocator, NativeArrayOptions.UninitializedMemory);
			this.m_DeprecatedAllocator = allocator.Handle;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00015AC4 File Offset: 0x00013CC4
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(AllocatorManager.AllocatorHandle)
		})]
		internal static NativeList<T> New<[IsUnmanaged] U>(int initialCapacity, ref U allocator, int disposeSentinelStackDepth) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			NativeList<T> result = default(NativeList<T>);
			result.Initialize<U>(initialCapacity, ref allocator, disposeSentinelStackDepth);
			return result;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00015AE4 File Offset: 0x00013CE4
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(AllocatorManager.AllocatorHandle)
		})]
		internal static NativeList<T> New<[IsUnmanaged] U>(int initialCapacity, ref U allocator) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			return NativeList<T>.New<U>(initialCapacity, ref allocator, 2);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00015AF0 File Offset: 0x00013CF0
		private NativeList(int initialCapacity, AllocatorManager.AllocatorHandle allocator, int disposeSentinelStackDepth)
		{
			this = default(NativeList<T>);
			AllocatorManager.AllocatorHandle allocatorHandle = allocator;
			this.Initialize<AllocatorManager.AllocatorHandle>(initialCapacity, ref allocatorHandle, disposeSentinelStackDepth);
		}

		// Token: 0x170000B7 RID: 183
		public unsafe T this[int index]
		{
			get
			{
				return (*this.m_ListData)[index];
			}
			set
			{
				(*this.m_ListData)[index] = value;
			}
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00015B2D File Offset: 0x00013D2D
		public unsafe ref T ElementAt(int index)
		{
			return this.m_ListData->ElementAt(index);
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x00015B3B File Offset: 0x00013D3B
		// (set) Token: 0x060006D2 RID: 1746 RVA: 0x00015B4D File Offset: 0x00013D4D
		public unsafe int Length
		{
			get
			{
				return CollectionHelper.AssumePositive(this.m_ListData->Length);
			}
			set
			{
				this.m_ListData->Resize(value, NativeArrayOptions.ClearMemory);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x00015B5C File Offset: 0x00013D5C
		// (set) Token: 0x060006D4 RID: 1748 RVA: 0x00015B69 File Offset: 0x00013D69
		public unsafe int Capacity
		{
			get
			{
				return this.m_ListData->Capacity;
			}
			set
			{
				this.m_ListData->Capacity = value;
			}
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00015B77 File Offset: 0x00013D77
		public unsafe UnsafeList<T>* GetUnsafeList()
		{
			return this.m_ListData;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00015B7F File Offset: 0x00013D7F
		public unsafe void AddNoResize(T value)
		{
			this.m_ListData->AddNoResize(value);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00015B8D File Offset: 0x00013D8D
		public unsafe void AddRangeNoResize(void* ptr, int count)
		{
			this.m_ListData->AddRangeNoResize(ptr, count);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00015B9C File Offset: 0x00013D9C
		public unsafe void AddRangeNoResize(NativeList<T> list)
		{
			this.m_ListData->AddRangeNoResize(*list.m_ListData);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00015BB4 File Offset: 0x00013DB4
		public unsafe void Add(in T value)
		{
			this.m_ListData->Add(value);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00015BC2 File Offset: 0x00013DC2
		public void AddRange(NativeArray<T> array)
		{
			this.AddRange(array.GetUnsafeReadOnlyPtr<T>(), array.Length);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00015BD7 File Offset: 0x00013DD7
		public unsafe void AddRange(void* ptr, int count)
		{
			this.m_ListData->AddRange(ptr, CollectionHelper.AssumePositive(count));
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00015BEB File Offset: 0x00013DEB
		public unsafe void InsertRangeWithBeginEnd(int begin, int end)
		{
			this.m_ListData->InsertRangeWithBeginEnd(CollectionHelper.AssumePositive(begin), CollectionHelper.AssumePositive(end));
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00015C04 File Offset: 0x00013E04
		public unsafe void RemoveAtSwapBack(int index)
		{
			this.m_ListData->RemoveAtSwapBack(CollectionHelper.AssumePositive(index));
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00015C17 File Offset: 0x00013E17
		public unsafe void RemoveRangeSwapBack(int index, int count)
		{
			this.m_ListData->RemoveRangeSwapBack(CollectionHelper.AssumePositive(index), CollectionHelper.AssumePositive(count));
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00015C30 File Offset: 0x00013E30
		[Obsolete("RemoveRangeSwapBackWithBeginEnd(begin, end) is deprecated, use RemoveRangeSwapBack(index, count) instead. (RemovedAfter 2021-06-02)", false)]
		public unsafe void RemoveRangeSwapBackWithBeginEnd(int begin, int end)
		{
			this.m_ListData->RemoveRangeSwapBackWithBeginEnd(CollectionHelper.AssumePositive(begin), CollectionHelper.AssumePositive(end));
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00015C49 File Offset: 0x00013E49
		public unsafe void RemoveAt(int index)
		{
			this.m_ListData->RemoveAt(CollectionHelper.AssumePositive(index));
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00015C5C File Offset: 0x00013E5C
		public unsafe void RemoveRange(int index, int count)
		{
			this.m_ListData->RemoveRange(index, count);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00015C6B File Offset: 0x00013E6B
		[Obsolete("RemoveRangeWithBeginEnd(begin, end) is deprecated, use RemoveRange(index, count) instead. (RemovedAfter 2021-06-02)", false)]
		public unsafe void RemoveRangeWithBeginEnd(int begin, int end)
		{
			this.m_ListData->RemoveRangeWithBeginEnd(begin, end);
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x00015C7A File Offset: 0x00013E7A
		public bool IsEmpty
		{
			get
			{
				return !this.IsCreated || this.Length == 0;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x00015C8F File Offset: 0x00013E8F
		public bool IsCreated
		{
			get
			{
				return this.m_ListData != null;
			}
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00015C9E File Offset: 0x00013E9E
		public void Dispose()
		{
			UnsafeList<T>.Destroy(this.m_ListData);
			this.m_ListData = null;
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00015CB3 File Offset: 0x00013EB3
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(AllocatorManager.AllocatorHandle)
		})]
		internal void Dispose<[IsUnmanaged] U>(ref U allocator) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			UnsafeList<T>.Destroy<U>(this.m_ListData, ref allocator);
			this.m_ListData = null;
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00015CCC File Offset: 0x00013ECC
		[NotBurstCompatible]
		public unsafe JobHandle Dispose(JobHandle inputDeps)
		{
			JobHandle result = new NativeListDisposeJob
			{
				Data = new NativeListDispose
				{
					m_ListData = (UntypedUnsafeList*)this.m_ListData
				}
			}.Schedule(inputDeps);
			this.m_ListData = null;
			return result;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00015D0D File Offset: 0x00013F0D
		public unsafe void Clear()
		{
			this.m_ListData->Clear();
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00015D1A File Offset: 0x00013F1A
		public static implicit operator NativeArray<T>(NativeList<T> nativeList)
		{
			return nativeList.AsArray();
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00015D23 File Offset: 0x00013F23
		public unsafe NativeArray<T> AsArray()
		{
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)this.m_ListData->Ptr, this.m_ListData->Length, Allocator.None);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00015D44 File Offset: 0x00013F44
		public unsafe NativeArray<T> AsDeferredJobArray()
		{
			byte* ptr = (byte*)this.m_ListData;
			ptr++;
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)ptr, 0, Allocator.Invalid);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00015D64 File Offset: 0x00013F64
		[NotBurstCompatible]
		public T[] ToArray()
		{
			return this.ToArrayNBC<T>();
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00015D74 File Offset: 0x00013F74
		public NativeArray<T> ToArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<T> result = CollectionHelper.CreateNativeArray<T>(this.Length, allocator, NativeArrayOptions.UninitializedMemory);
			result.CopyFrom(this);
			return result;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00015DA4 File Offset: 0x00013FA4
		public NativeArray<T>.Enumerator GetEnumerator()
		{
			NativeArray<T> nativeArray = this.AsArray();
			return new NativeArray<T>.Enumerator(ref nativeArray);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00015DBF File Offset: 0x00013FBF
		[NotBurstCompatible]
		[Obsolete("Please use `CopyFromNBC` from `Unity.Collections.NotBurstCompatible` namespace instead. (RemovedAfter 2021-06-22)", false)]
		public void CopyFrom(T[] array)
		{
			this.CopyFromNBC(array);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00015DD0 File Offset: 0x00013FD0
		public void CopyFrom(NativeArray<T> array)
		{
			this.Clear();
			this.Resize(array.Length, NativeArrayOptions.UninitializedMemory);
			this.AsArray().CopyFrom(array);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00015E00 File Offset: 0x00014000
		public unsafe void Resize(int length, NativeArrayOptions options)
		{
			this.m_ListData->Resize(length, options);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00015E0F File Offset: 0x0001400F
		public void ResizeUninitialized(int length)
		{
			this.Resize(length, NativeArrayOptions.UninitializedMemory);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00015E19 File Offset: 0x00014019
		public unsafe void SetCapacity(int capacity)
		{
			this.m_ListData->SetCapacity(capacity);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00015E27 File Offset: 0x00014027
		public unsafe void TrimExcess()
		{
			this.m_ListData->TrimExcess();
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00015E34 File Offset: 0x00014034
		public unsafe NativeArray<T>.ReadOnly AsParallelReader()
		{
			return new NativeArray<T>.ReadOnly((void*)this.m_ListData->Ptr, this.m_ListData->Length);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00015E51 File Offset: 0x00014051
		public NativeList<T>.ParallelWriter AsParallelWriter()
		{
			return new NativeList<T>.ParallelWriter(this.m_ListData);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00015E5E File Offset: 0x0001405E
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckInitialCapacity(int initialCapacity)
		{
			if (initialCapacity < 0)
			{
				throw new ArgumentOutOfRangeException("initialCapacity", "Capacity must be >= 0");
			}
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00015E74 File Offset: 0x00014074
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckTotalSize(int initialCapacity, long totalSize)
		{
			if (totalSize > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("initialCapacity", string.Format("Capacity * sizeof(T) cannot exceed {0} bytes", int.MaxValue));
			}
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00015E9E File Offset: 0x0001409E
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckSufficientCapacity(int capacity, int length)
		{
			if (capacity < length)
			{
				throw new Exception(string.Format("Length {0} exceeds capacity Capacity {1}", length, capacity));
			}
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00015EC0 File Offset: 0x000140C0
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckIndexInRange(int value, int length)
		{
			if (value < 0)
			{
				throw new IndexOutOfRangeException(string.Format("Value {0} must be positive.", value));
			}
			if (value >= length)
			{
				throw new IndexOutOfRangeException(string.Format("Value {0} is out of range in NativeList of '{1}' Length.", value, length));
			}
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00015EFC File Offset: 0x000140FC
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckArgPositive(int value)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("Value {0} must be positive.", value));
			}
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00015F18 File Offset: 0x00014118
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private unsafe void CheckHandleMatches(AllocatorManager.AllocatorHandle handle)
		{
			if (this.m_ListData == null)
			{
				throw new ArgumentOutOfRangeException(string.Format("Allocator handle {0} can't match because container is not initialized.", handle));
			}
			if (this.m_ListData->Allocator.Index != handle.Index)
			{
				throw new ArgumentOutOfRangeException(string.Format("Allocator handle {0} can't match because container handle index doesn't match.", handle));
			}
			if (this.m_ListData->Allocator.Version != handle.Version)
			{
				throw new ArgumentOutOfRangeException(string.Format("Allocator handle {0} matches container handle index, but has different version.", handle));
			}
		}

		// Token: 0x0400027F RID: 639
		[NativeDisableUnsafePtrRestriction]
		internal unsafe UnsafeList<T>* m_ListData;

		// Token: 0x04000280 RID: 640
		internal AllocatorManager.AllocatorHandle m_DeprecatedAllocator;

		// Token: 0x020000B3 RID: 179
		[NativeContainer]
		[NativeContainerIsAtomicWriteOnly]
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public struct ParallelWriter
		{
			// Token: 0x170000BC RID: 188
			// (get) Token: 0x060006FF RID: 1791 RVA: 0x00015FA1 File Offset: 0x000141A1
			public unsafe readonly void* Ptr
			{
				get
				{
					return (void*)this.ListData->Ptr;
				}
			}

			// Token: 0x06000700 RID: 1792 RVA: 0x00015FAE File Offset: 0x000141AE
			internal unsafe ParallelWriter(UnsafeList<T>* listData)
			{
				this.ListData = listData;
			}

			// Token: 0x06000701 RID: 1793 RVA: 0x00015FB8 File Offset: 0x000141B8
			public unsafe void AddNoResize(T value)
			{
				int index = Interlocked.Increment(ref this.ListData->m_length) - 1;
				UnsafeUtility.WriteArrayElement<T>((void*)this.ListData->Ptr, index, value);
			}

			// Token: 0x06000702 RID: 1794 RVA: 0x00015FEC File Offset: 0x000141EC
			public unsafe void AddRangeNoResize(void* ptr, int count)
			{
				int num = Interlocked.Add(ref this.ListData->m_length, count) - count;
				int num2 = sizeof(T);
				void* destination = (void*)(this.ListData->Ptr + num * num2 / sizeof(T));
				UnsafeUtility.MemCpy(destination, ptr, (long)(count * num2));
			}

			// Token: 0x06000703 RID: 1795 RVA: 0x0001602F File Offset: 0x0001422F
			public unsafe void AddRangeNoResize(UnsafeList<T> list)
			{
				this.AddRangeNoResize((void*)list.Ptr, list.Length);
			}

			// Token: 0x06000704 RID: 1796 RVA: 0x00016044 File Offset: 0x00014244
			public unsafe void AddRangeNoResize(NativeList<T> list)
			{
				this.AddRangeNoResize(*list.m_ListData);
			}

			// Token: 0x04000281 RID: 641
			[NativeDisableUnsafePtrRestriction]
			public unsafe UnsafeList<T>* ListData;
		}
	}
}
