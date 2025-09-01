using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000129 RID: 297
	[DebuggerDisplay("Length = {Length}, Capacity = {Capacity}, IsCreated = {IsCreated}, IsEmpty = {IsEmpty}")]
	[DebuggerTypeProxy(typeof(UnsafePtrListTDebugView<>))]
	[BurstCompatible(GenericTypeArguments = new Type[]
	{
		typeof(int)
	})]
	public struct UnsafePtrList<[IsUnmanaged] T> : INativeDisposable, IDisposable, IEnumerable<IntPtr>, IEnumerable where T : struct, ValueType
	{
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x00020602 File Offset: 0x0001E802
		// (set) Token: 0x06000AE5 RID: 2789 RVA: 0x0002060F File Offset: 0x0001E80F
		public int Length
		{
			get
			{
				return ref this.ListData<T>().Length;
			}
			set
			{
				ref this.ListData<T>().Length = value;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x0002061D File Offset: 0x0001E81D
		// (set) Token: 0x06000AE7 RID: 2791 RVA: 0x0002062A File Offset: 0x0001E82A
		public int Capacity
		{
			get
			{
				return ref this.ListData<T>().Capacity;
			}
			set
			{
				ref this.ListData<T>().Capacity = value;
			}
		}

		// Token: 0x17000132 RID: 306
		public unsafe T* this[int index]
		{
			get
			{
				return *(IntPtr*)(this.Ptr + (IntPtr)CollectionHelper.AssumePositive(index) * (IntPtr)sizeof(T*) / (IntPtr)sizeof(T*));
			}
			set
			{
				*(IntPtr*)(this.Ptr + (IntPtr)CollectionHelper.AssumePositive(index) * (IntPtr)sizeof(T*) / (IntPtr)sizeof(T*)) = value;
			}
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00020669 File Offset: 0x0001E869
		public unsafe ref T* ElementAt(int index)
		{
			return ref this.Ptr[(IntPtr)CollectionHelper.AssumePositive(index) * (IntPtr)sizeof(T*) / (IntPtr)sizeof(T*)];
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00020680 File Offset: 0x0001E880
		public unsafe UnsafePtrList(T** ptr, int length)
		{
			this = default(UnsafePtrList<T>);
			this.Ptr = ptr;
			this.m_length = length;
			this.m_capacity = length;
			this.Allocator = AllocatorManager.None;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x000206A9 File Offset: 0x0001E8A9
		public unsafe UnsafePtrList(int initialCapacity, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			this = default(UnsafePtrList<T>);
			this.Ptr = null;
			this.m_length = 0;
			this.m_capacity = 0;
			this.Allocator = AllocatorManager.None;
			*ref this.ListData<T>() = new UnsafeList<IntPtr>(initialCapacity, allocator, options);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x000206E6 File Offset: 0x0001E8E6
		public unsafe static UnsafePtrList<T>* Create(T** ptr, int length)
		{
			UnsafePtrList<T>* ptr2 = AllocatorManager.Allocate<UnsafePtrList<T>>(AllocatorManager.Persistent, 1);
			*ptr2 = new UnsafePtrList<T>(ptr, length);
			return ptr2;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00020700 File Offset: 0x0001E900
		public unsafe static UnsafePtrList<T>* Create(int initialCapacity, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			UnsafePtrList<T>* ptr = AllocatorManager.Allocate<UnsafePtrList<T>>(allocator, 1);
			*ptr = new UnsafePtrList<T>(initialCapacity, allocator, options);
			return ptr;
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00020718 File Offset: 0x0001E918
		public unsafe static void Destroy(UnsafePtrList<T>* listData)
		{
			AllocatorManager.AllocatorHandle handle = (ref *listData.ListData<T>().Allocator.Value == AllocatorManager.Invalid.Value) ? AllocatorManager.Persistent : ref *listData.ListData<T>().Allocator;
			listData->Dispose();
			AllocatorManager.Free<UnsafePtrList<T>>(handle, listData, 1);
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x00020763 File Offset: 0x0001E963
		public bool IsEmpty
		{
			get
			{
				return !this.IsCreated || this.Length == 0;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x00020778 File Offset: 0x0001E978
		public bool IsCreated
		{
			get
			{
				return this.Ptr != null;
			}
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00020787 File Offset: 0x0001E987
		public void Dispose()
		{
			ref this.ListData<T>().Dispose();
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00020794 File Offset: 0x0001E994
		[NotBurstCompatible]
		public JobHandle Dispose(JobHandle inputDeps)
		{
			return ref this.ListData<T>().Dispose(inputDeps);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x000207A2 File Offset: 0x0001E9A2
		public void Clear()
		{
			ref this.ListData<T>().Clear();
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x000207AF File Offset: 0x0001E9AF
		public void Resize(int length, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			ref this.ListData<T>().Resize(length, options);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x000207BE File Offset: 0x0001E9BE
		public void SetCapacity(int capacity)
		{
			ref this.ListData<T>().SetCapacity(capacity);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x000207CC File Offset: 0x0001E9CC
		public void TrimExcess()
		{
			ref this.ListData<T>().TrimExcess();
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x000207DC File Offset: 0x0001E9DC
		public unsafe int IndexOf(void* ptr)
		{
			for (int i = 0; i < this.Length; i++)
			{
				if (*(IntPtr*)(this.Ptr + (IntPtr)i * (IntPtr)sizeof(T*) / (IntPtr)sizeof(T*)) == ptr)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00020811 File Offset: 0x0001EA11
		public unsafe bool Contains(void* ptr)
		{
			return this.IndexOf(ptr) != -1;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00020820 File Offset: 0x0001EA20
		public unsafe void AddNoResize(void* value)
		{
			ref this.ListData<T>().AddNoResize((IntPtr)value);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00020833 File Offset: 0x0001EA33
		public unsafe void AddRangeNoResize(void** ptr, int count)
		{
			ref this.ListData<T>().AddRangeNoResize((void*)ptr, count);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00020842 File Offset: 0x0001EA42
		public unsafe void AddRangeNoResize(UnsafePtrList<T> list)
		{
			ref this.ListData<T>().AddRangeNoResize((void*)list.Ptr, list.Length);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0002085C File Offset: 0x0001EA5C
		public void Add(in IntPtr value)
		{
			ref this.ListData<T>().Add(value);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0002086C File Offset: 0x0001EA6C
		public unsafe void Add(void* value)
		{
			ref UnsafeList<IntPtr> ptr = ref ref this.ListData<T>();
			IntPtr intPtr = (IntPtr)value;
			ptr.Add(intPtr);
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0002088D File Offset: 0x0001EA8D
		public unsafe void AddRange(void* ptr, int length)
		{
			ref this.ListData<T>().AddRange(ptr, length);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0002089C File Offset: 0x0001EA9C
		public unsafe void AddRange(UnsafePtrList<T> list)
		{
			ref this.ListData<T>().AddRange(*ref list.ListData<T>());
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x000208B5 File Offset: 0x0001EAB5
		public void InsertRangeWithBeginEnd(int begin, int end)
		{
			ref this.ListData<T>().InsertRangeWithBeginEnd(begin, end);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x000208C4 File Offset: 0x0001EAC4
		public void RemoveAtSwapBack(int index)
		{
			ref this.ListData<T>().RemoveAtSwapBack(index);
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x000208D2 File Offset: 0x0001EAD2
		public void RemoveRangeSwapBack(int index, int count)
		{
			ref this.ListData<T>().RemoveRangeSwapBack(index, count);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x000208E1 File Offset: 0x0001EAE1
		[Obsolete("RemoveRangeSwapBackWithBeginEnd(begin, end) is deprecated, use RemoveRangeSwapBack(index, count) instead. (RemovedAfter 2021-06-02)", false)]
		public void RemoveRangeSwapBackWithBeginEnd(int begin, int end)
		{
			ref this.ListData<T>().RemoveRangeSwapBackWithBeginEnd(begin, end);
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x000208F0 File Offset: 0x0001EAF0
		public void RemoveAt(int index)
		{
			ref this.ListData<T>().RemoveAt(index);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x000208FE File Offset: 0x0001EAFE
		public void RemoveRange(int index, int count)
		{
			ref this.ListData<T>().RemoveRange(index, count);
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0002090D File Offset: 0x0001EB0D
		[Obsolete("RemoveRangeWithBeginEnd(begin, end) is deprecated, use RemoveRange(index, count) instead. (RemovedAfter 2021-06-02)", false)]
		public void RemoveRangeWithBeginEnd(int begin, int end)
		{
			ref this.ListData<T>().RemoveRangeWithBeginEnd(begin, end);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator<IntPtr> IEnumerable<IntPtr>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0002091C File Offset: 0x0001EB1C
		public UnsafePtrList<T>.ParallelReader AsParallelReader()
		{
			return new UnsafePtrList<T>.ParallelReader(this.Ptr, this.Length);
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0002092F File Offset: 0x0001EB2F
		public unsafe UnsafePtrList<T>.ParallelWriter AsParallelWriter()
		{
			return new UnsafePtrList<T>.ParallelWriter(this.Ptr, (UnsafeList<IntPtr>*)UnsafeUtility.AddressOf<UnsafePtrList<T>>(ref this));
		}

		// Token: 0x0400038D RID: 909
		[NativeDisableUnsafePtrRestriction]
		public unsafe readonly T** Ptr;

		// Token: 0x0400038E RID: 910
		public readonly int m_length;

		// Token: 0x0400038F RID: 911
		public readonly int m_capacity;

		// Token: 0x04000390 RID: 912
		public readonly AllocatorManager.AllocatorHandle Allocator;

		// Token: 0x04000391 RID: 913
		[Obsolete("Use Length property (UnityUpgradable) -> Length", true)]
		public int length;

		// Token: 0x04000392 RID: 914
		[Obsolete("Use Capacity property (UnityUpgradable) -> Capacity", true)]
		public int capacity;

		// Token: 0x0200012A RID: 298
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public struct ParallelReader
		{
			// Token: 0x06000B0C RID: 2828 RVA: 0x00020942 File Offset: 0x0001EB42
			internal unsafe ParallelReader(T** ptr, int length)
			{
				this.Ptr = ptr;
				this.Length = length;
			}

			// Token: 0x06000B0D RID: 2829 RVA: 0x00020954 File Offset: 0x0001EB54
			public unsafe int IndexOf(void* ptr)
			{
				for (int i = 0; i < this.Length; i++)
				{
					if (*(IntPtr*)(this.Ptr + (IntPtr)i * (IntPtr)sizeof(T*) / (IntPtr)sizeof(T*)) == ptr)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06000B0E RID: 2830 RVA: 0x00020989 File Offset: 0x0001EB89
			public unsafe bool Contains(void* ptr)
			{
				return this.IndexOf(ptr) != -1;
			}

			// Token: 0x04000393 RID: 915
			[NativeDisableUnsafePtrRestriction]
			public unsafe readonly T** Ptr;

			// Token: 0x04000394 RID: 916
			public readonly int Length;
		}

		// Token: 0x0200012B RID: 299
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public struct ParallelWriter
		{
			// Token: 0x06000B0F RID: 2831 RVA: 0x00020998 File Offset: 0x0001EB98
			internal unsafe ParallelWriter(T** ptr, UnsafeList<IntPtr>* listData)
			{
				this.Ptr = ptr;
				this.ListData = listData;
			}

			// Token: 0x06000B10 RID: 2832 RVA: 0x000209A8 File Offset: 0x0001EBA8
			public unsafe void AddNoResize(T* value)
			{
				this.ListData->AddNoResize((IntPtr)((void*)value));
			}

			// Token: 0x06000B11 RID: 2833 RVA: 0x000209BB File Offset: 0x0001EBBB
			public unsafe void AddRangeNoResize(T** ptr, int count)
			{
				this.ListData->AddRangeNoResize((void*)ptr, count);
			}

			// Token: 0x06000B12 RID: 2834 RVA: 0x000209CA File Offset: 0x0001EBCA
			public unsafe void AddRangeNoResize(UnsafePtrList<T> list)
			{
				this.ListData->AddRangeNoResize((void*)list.Ptr, list.Length);
			}

			// Token: 0x04000395 RID: 917
			[NativeDisableUnsafePtrRestriction]
			public unsafe readonly T** Ptr;

			// Token: 0x04000396 RID: 918
			[NativeDisableUnsafePtrRestriction]
			public unsafe UnsafeList<IntPtr>* ListData;
		}
	}
}
