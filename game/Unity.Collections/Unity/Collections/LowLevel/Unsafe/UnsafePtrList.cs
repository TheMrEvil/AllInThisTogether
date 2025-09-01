using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000100 RID: 256
	[DebuggerDisplay("Length = {Length}, Capacity = {Capacity}, IsCreated = {IsCreated}, IsEmpty = {IsEmpty}")]
	[DebuggerTypeProxy(typeof(UnsafePtrListDebugView))]
	[Obsolete("Untyped UnsafePtrList is deprecated, please use UnsafePtrList<T> instead. (RemovedAfter 2021-05-18)", false)]
	public struct UnsafePtrList : INativeDisposable, IDisposable, INativeList<IntPtr>, IIndexable<IntPtr>, IEnumerable<IntPtr>, IEnumerable
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x0001D5B6 File Offset: 0x0001B7B6
		// (set) Token: 0x060009A4 RID: 2468 RVA: 0x00002C2B File Offset: 0x00000E2B
		public int Length
		{
			get
			{
				return this.length;
			}
			set
			{
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0001D5BE File Offset: 0x0001B7BE
		// (set) Token: 0x060009A6 RID: 2470 RVA: 0x00002C2B File Offset: 0x00000E2B
		public int Capacity
		{
			get
			{
				return this.capacity;
			}
			set
			{
			}
		}

		// Token: 0x17000103 RID: 259
		public unsafe IntPtr this[int index]
		{
			get
			{
				return new IntPtr(*(IntPtr*)(this.Ptr + (IntPtr)index * (IntPtr)sizeof(void*) / (IntPtr)sizeof(void*)));
			}
			set
			{
				*(IntPtr*)(this.Ptr + (IntPtr)index * (IntPtr)sizeof(void*) / (IntPtr)sizeof(void*)) = (void*)value;
			}
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0001D5F7 File Offset: 0x0001B7F7
		public unsafe ref IntPtr ElementAt(int index)
		{
			return ref *(IntPtr*)(this.Ptr + (IntPtr)index * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(void*));
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0001D609 File Offset: 0x0001B809
		public unsafe UnsafePtrList(void** ptr, int length)
		{
			this = default(UnsafePtrList);
			this.Ptr = ptr;
			this.length = length;
			this.capacity = length;
			this.Allocator = AllocatorManager.None;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0001D634 File Offset: 0x0001B834
		public unsafe UnsafePtrList(int initialCapacity, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			this = default(UnsafePtrList);
			this.Ptr = null;
			this.length = 0;
			this.capacity = 0;
			this.Allocator = AllocatorManager.None;
			int size = IntPtr.Size;
			*ref this.ListData() = new UnsafeList(size, size, initialCapacity, allocator, options);
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0001D684 File Offset: 0x0001B884
		public unsafe UnsafePtrList(int initialCapacity, Allocator allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			this = default(UnsafePtrList);
			this.Ptr = null;
			this.length = 0;
			this.capacity = 0;
			this.Allocator = AllocatorManager.None;
			int size = IntPtr.Size;
			*ref this.ListData() = new UnsafeList(size, size, initialCapacity, allocator, options);
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0001D6D4 File Offset: 0x0001B8D4
		public unsafe static UnsafePtrList* Create(void** ptr, int length)
		{
			UnsafePtrList* ptr2 = AllocatorManager.Allocate<UnsafePtrList>(AllocatorManager.Persistent, 1);
			*ptr2 = new UnsafePtrList(ptr, length);
			return ptr2;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0001D6EE File Offset: 0x0001B8EE
		public unsafe static UnsafePtrList* Create(int initialCapacity, Allocator allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			UnsafePtrList* ptr = AllocatorManager.Allocate<UnsafePtrList>(allocator, 1);
			*ptr = new UnsafePtrList(initialCapacity, allocator, options);
			return ptr;
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0001D70C File Offset: 0x0001B90C
		public unsafe static void Destroy(UnsafePtrList* listData)
		{
			AllocatorManager.AllocatorHandle handle = (ref *listData.ListData().Allocator.Value == AllocatorManager.Invalid.Value) ? AllocatorManager.Persistent : ref *listData.ListData().Allocator;
			listData->Dispose();
			AllocatorManager.Free<UnsafePtrList>(handle, listData, 1);
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x0001D757 File Offset: 0x0001B957
		public bool IsEmpty
		{
			get
			{
				return !this.IsCreated || this.Length == 0;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x0001D76C File Offset: 0x0001B96C
		public bool IsCreated
		{
			get
			{
				return this.Ptr != null;
			}
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0001D77B File Offset: 0x0001B97B
		public void Dispose()
		{
			ref this.ListData().Dispose();
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0001D788 File Offset: 0x0001B988
		[NotBurstCompatible]
		public JobHandle Dispose(JobHandle inputDeps)
		{
			return ref this.ListData().Dispose(inputDeps);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0001D796 File Offset: 0x0001B996
		public void Clear()
		{
			ref this.ListData().Clear();
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0001D7A3 File Offset: 0x0001B9A3
		public void Resize(int length, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			ref this.ListData().Resize<IntPtr>(length, options);
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0001D7B2 File Offset: 0x0001B9B2
		public void SetCapacity(int capacity)
		{
			ref this.ListData().SetCapacity<IntPtr>(capacity);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0001D7C0 File Offset: 0x0001B9C0
		public void TrimExcess()
		{
			ref this.ListData().TrimExcess<IntPtr>();
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0001D7D0 File Offset: 0x0001B9D0
		public unsafe int IndexOf(void* value)
		{
			for (int i = 0; i < this.Length; i++)
			{
				if (*(IntPtr*)(this.Ptr + (IntPtr)i * (IntPtr)sizeof(void*) / (IntPtr)sizeof(void*)) == value)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0001D805 File Offset: 0x0001BA05
		public unsafe bool Contains(void* value)
		{
			return this.IndexOf(value) != -1;
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0001D814 File Offset: 0x0001BA14
		public unsafe void AddNoResize(void* value)
		{
			ref this.ListData().AddNoResize<IntPtr>((IntPtr)value);
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0001D827 File Offset: 0x0001BA27
		public unsafe void AddRangeNoResize(void** ptr, int length)
		{
			ref this.ListData().AddRangeNoResize<IntPtr>((void*)ptr, length);
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0001D836 File Offset: 0x0001BA36
		public unsafe void AddRangeNoResize(UnsafePtrList list)
		{
			ref this.ListData().AddRangeNoResize<IntPtr>((void*)list.Ptr, list.Length);
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0001D850 File Offset: 0x0001BA50
		public void Add(in IntPtr value)
		{
			ref this.ListData().Add<IntPtr>(value);
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0001D85F File Offset: 0x0001BA5F
		public unsafe void Add(void* value)
		{
			ref this.ListData().Add<IntPtr>((IntPtr)value);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0001D872 File Offset: 0x0001BA72
		public unsafe void AddRange(void* ptr, int length)
		{
			ref this.ListData().AddRange<IntPtr>(ptr, length);
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0001D881 File Offset: 0x0001BA81
		public unsafe void AddRange(UnsafePtrList list)
		{
			ref this.ListData().AddRange<IntPtr>(*ref list.ListData());
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0001D89A File Offset: 0x0001BA9A
		public void InsertRangeWithBeginEnd(int begin, int end)
		{
			ref this.ListData().InsertRangeWithBeginEnd<IntPtr>(begin, end);
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0001D8A9 File Offset: 0x0001BAA9
		public void RemoveAtSwapBack(int index)
		{
			ref this.ListData().RemoveAtSwapBack<IntPtr>(index);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0001D8B7 File Offset: 0x0001BAB7
		public void RemoveRangeSwapBackWithBeginEnd(int begin, int end)
		{
			ref this.ListData().RemoveRangeSwapBackWithBeginEnd<IntPtr>(begin, end);
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0001D8C6 File Offset: 0x0001BAC6
		public void RemoveAt(int index)
		{
			ref this.ListData().RemoveAt<IntPtr>(index);
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0001D8D4 File Offset: 0x0001BAD4
		public void RemoveRangeWithBeginEnd(int begin, int end)
		{
			ref this.ListData().RemoveRangeWithBeginEnd<IntPtr>(begin, end);
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator<IntPtr> IEnumerable<IntPtr>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0001D8E3 File Offset: 0x0001BAE3
		public UnsafePtrList.ParallelReader AsParallelReader()
		{
			return new UnsafePtrList.ParallelReader(this.Ptr, this.Length);
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0001D8F6 File Offset: 0x0001BAF6
		public unsafe UnsafePtrList.ParallelWriter AsParallelWriter()
		{
			return new UnsafePtrList.ParallelWriter((void*)this.Ptr, (UnsafeList*)UnsafeUtility.AddressOf<UnsafePtrList>(ref this));
		}

		// Token: 0x04000331 RID: 817
		[NativeDisableUnsafePtrRestriction]
		public unsafe readonly void** Ptr;

		// Token: 0x04000332 RID: 818
		public readonly int length;

		// Token: 0x04000333 RID: 819
		public readonly int unused;

		// Token: 0x04000334 RID: 820
		public readonly int capacity;

		// Token: 0x04000335 RID: 821
		public readonly AllocatorManager.AllocatorHandle Allocator;

		// Token: 0x02000101 RID: 257
		public struct ParallelReader
		{
			// Token: 0x060009CA RID: 2506 RVA: 0x0001D909 File Offset: 0x0001BB09
			internal unsafe ParallelReader(void** ptr, int length)
			{
				this.Ptr = ptr;
				this.Length = length;
			}

			// Token: 0x060009CB RID: 2507 RVA: 0x0001D91C File Offset: 0x0001BB1C
			public unsafe int IndexOf(void* value)
			{
				for (int i = 0; i < this.Length; i++)
				{
					if (*(IntPtr*)(this.Ptr + (IntPtr)i * (IntPtr)sizeof(void*) / (IntPtr)sizeof(void*)) == value)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x060009CC RID: 2508 RVA: 0x0001D951 File Offset: 0x0001BB51
			public unsafe bool Contains(void* value)
			{
				return this.IndexOf(value) != -1;
			}

			// Token: 0x04000336 RID: 822
			[NativeDisableUnsafePtrRestriction]
			public unsafe readonly void** Ptr;

			// Token: 0x04000337 RID: 823
			public readonly int Length;
		}

		// Token: 0x02000102 RID: 258
		public struct ParallelWriter
		{
			// Token: 0x060009CD RID: 2509 RVA: 0x0001D960 File Offset: 0x0001BB60
			internal unsafe ParallelWriter(void* ptr, UnsafeList* listData)
			{
				this.Ptr = ptr;
				this.ListData = listData;
			}

			// Token: 0x060009CE RID: 2510 RVA: 0x0001D970 File Offset: 0x0001BB70
			public unsafe void AddNoResize(void* value)
			{
				this.ListData->AddNoResize<IntPtr>((IntPtr)value);
			}

			// Token: 0x060009CF RID: 2511 RVA: 0x0001D983 File Offset: 0x0001BB83
			public unsafe void AddRangeNoResize(void** ptr, int length)
			{
				this.ListData->AddRangeNoResize<IntPtr>((void*)ptr, length);
			}

			// Token: 0x060009D0 RID: 2512 RVA: 0x0001D992 File Offset: 0x0001BB92
			public unsafe void AddRangeNoResize(UnsafePtrList list)
			{
				this.ListData->AddRangeNoResize<IntPtr>((void*)list.Ptr, list.Length);
			}

			// Token: 0x04000338 RID: 824
			[NativeDisableUnsafePtrRestriction]
			public unsafe readonly void* Ptr;

			// Token: 0x04000339 RID: 825
			[NativeDisableUnsafePtrRestriction]
			public unsafe UnsafeList* ListData;
		}
	}
}
