using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Jobs;
using Unity.Mathematics;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x020000FC RID: 252
	[DebuggerDisplay("Length = {Length}, Capacity = {Capacity}, IsCreated = {IsCreated}, IsEmpty = {IsEmpty}")]
	[Obsolete("Untyped UnsafeList is deprecated, please use UnsafeList<T> instead. (RemovedAfter 2021-05-18)", false)]
	public struct UnsafeList : INativeDisposable, IDisposable
	{
		// Token: 0x06000960 RID: 2400 RVA: 0x0001CAE5 File Offset: 0x0001ACE5
		public UnsafeList(Allocator allocator)
		{
			this = default(UnsafeList);
			this.Ptr = null;
			this.Length = 0;
			this.Capacity = 0;
			this.Allocator = allocator;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0001CB10 File Offset: 0x0001AD10
		public unsafe UnsafeList(void* ptr, int length)
		{
			this = default(UnsafeList);
			this.Ptr = ptr;
			this.Length = length;
			this.Capacity = length;
			this.Allocator = Unity.Collections.Allocator.None;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0001CB3C File Offset: 0x0001AD3C
		internal void Initialize<[IsUnmanaged] U>(int sizeOf, int alignOf, int initialCapacity, ref U allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			this.Allocator = allocator.Handle;
			this.Ptr = null;
			this.Length = 0;
			this.Capacity = 0;
			if (initialCapacity != 0)
			{
				this.SetCapacity<U>(ref allocator, sizeOf, alignOf, initialCapacity);
			}
			if (options == NativeArrayOptions.ClearMemory && this.Ptr != null)
			{
				UnsafeUtility.MemClear(this.Ptr, (long)(this.Capacity * sizeOf));
			}
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0001CBA4 File Offset: 0x0001ADA4
		internal static UnsafeList New<[IsUnmanaged] U>(int sizeOf, int alignOf, int initialCapacity, ref U allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			UnsafeList result = default(UnsafeList);
			result.Initialize<U>(sizeOf, alignOf, initialCapacity, ref allocator, options);
			return result;
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0001CBC7 File Offset: 0x0001ADC7
		public UnsafeList(int sizeOf, int alignOf, int initialCapacity, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			this = default(UnsafeList);
			this = default(UnsafeList);
			this.Initialize<AllocatorManager.AllocatorHandle>(sizeOf, alignOf, initialCapacity, ref allocator, options);
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0001CBE4 File Offset: 0x0001ADE4
		public UnsafeList(int sizeOf, int alignOf, int initialCapacity, Allocator allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			this = default(UnsafeList);
			this.Allocator = allocator;
			this.Ptr = null;
			this.Length = 0;
			this.Capacity = 0;
			if (initialCapacity != 0)
			{
				this.SetCapacity(sizeOf, alignOf, initialCapacity);
			}
			if (options == NativeArrayOptions.ClearMemory && this.Ptr != null)
			{
				UnsafeUtility.MemClear(this.Ptr, (long)(this.Capacity * sizeOf));
			}
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0001CC4C File Offset: 0x0001AE4C
		public unsafe static UnsafeList* Create(int sizeOf, int alignOf, int initialCapacity, Allocator allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			UnsafeList* ptr = AllocatorManager.Allocate<UnsafeList>(allocator, 1);
			UnsafeUtility.MemClear((void*)ptr, (long)UnsafeUtility.SizeOf<UnsafeList>());
			ptr->Allocator = allocator;
			if (initialCapacity != 0)
			{
				ptr->SetCapacity(sizeOf, alignOf, initialCapacity);
			}
			if (options == NativeArrayOptions.ClearMemory && ptr->Ptr != null)
			{
				UnsafeUtility.MemClear(ptr->Ptr, (long)(ptr->Capacity * sizeOf));
			}
			return ptr;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0001CCB0 File Offset: 0x0001AEB0
		internal unsafe static UnsafeList* Create<[IsUnmanaged] U>(int sizeOf, int alignOf, int initialCapacity, ref U allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			UnsafeList* ptr = ref allocator.Allocate(default(UnsafeList), 1);
			UnsafeUtility.MemClear((void*)ptr, (long)UnsafeUtility.SizeOf<UnsafeList>());
			ptr->Allocator = allocator.Handle;
			if (initialCapacity != 0)
			{
				ptr->SetCapacity<U>(ref allocator, sizeOf, alignOf, initialCapacity);
			}
			if (options == NativeArrayOptions.ClearMemory && ptr->Ptr != null)
			{
				UnsafeUtility.MemClear(ptr->Ptr, (long)(ptr->Capacity * sizeOf));
			}
			return ptr;
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0001CD1D File Offset: 0x0001AF1D
		internal unsafe static void Destroy<[IsUnmanaged] U>(UnsafeList* listData, ref U allocator, int sizeOf, int alignOf) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			listData->Dispose<U>(ref allocator, sizeOf, alignOf);
			ref allocator.Free((void*)listData, UnsafeUtility.SizeOf<UnsafeList>(), UnsafeUtility.AlignOf<UnsafeList>(), 1);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0001CD3A File Offset: 0x0001AF3A
		public unsafe static void Destroy(UnsafeList* listData)
		{
			AllocatorManager.AllocatorHandle allocator = listData->Allocator;
			listData->Dispose();
			AllocatorManager.Free<UnsafeList>(allocator, listData, 1);
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x0001CD4F File Offset: 0x0001AF4F
		public bool IsEmpty
		{
			get
			{
				return !this.IsCreated || this.Length == 0;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x0001CD64 File Offset: 0x0001AF64
		public bool IsCreated
		{
			get
			{
				return this.Ptr != null;
			}
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0001CD74 File Offset: 0x0001AF74
		public void Dispose()
		{
			if (CollectionHelper.ShouldDeallocate(this.Allocator))
			{
				AllocatorManager.Free(this.Allocator, this.Ptr);
				this.Allocator = AllocatorManager.Invalid;
			}
			this.Ptr = null;
			this.Length = 0;
			this.Capacity = 0;
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0001CDC0 File Offset: 0x0001AFC0
		internal void Dispose<[IsUnmanaged] U>(ref U allocator, int sizeOf, int alignOf) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			ref allocator.Free(this.Ptr, sizeOf, alignOf, this.Length);
			this.Ptr = null;
			this.Length = 0;
			this.Capacity = 0;
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0001CDEC File Offset: 0x0001AFEC
		[NotBurstCompatible]
		public JobHandle Dispose(JobHandle inputDeps)
		{
			if (CollectionHelper.ShouldDeallocate(this.Allocator))
			{
				JobHandle result = new UnsafeDisposeJob
				{
					Ptr = this.Ptr,
					Allocator = (Allocator)this.Allocator.Value
				}.Schedule(inputDeps);
				this.Ptr = null;
				this.Allocator = AllocatorManager.Invalid;
				return result;
			}
			this.Ptr = null;
			return inputDeps;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0001CE56 File Offset: 0x0001B056
		public void Clear()
		{
			this.Length = 0;
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0001CE60 File Offset: 0x0001B060
		public unsafe void Resize(int sizeOf, int alignOf, int length, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			int length2 = this.Length;
			if (length > this.Capacity)
			{
				this.SetCapacity(sizeOf, alignOf, length);
			}
			this.Length = length;
			if (options == NativeArrayOptions.ClearMemory && length2 < length)
			{
				int num = length - length2;
				byte* ptr = (byte*)this.Ptr;
				UnsafeUtility.MemClear((void*)(ptr + length2 * sizeOf), (long)(num * sizeOf));
			}
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0001CEAF File Offset: 0x0001B0AF
		public void Resize<T>(int length, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory) where T : struct
		{
			this.Resize(UnsafeUtility.SizeOf<T>(), UnsafeUtility.AlignOf<T>(), length, options);
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0001CEC4 File Offset: 0x0001B0C4
		private unsafe void Realloc<[IsUnmanaged] U>(ref U allocator, int sizeOf, int alignOf, int capacity) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			void* ptr = null;
			if (capacity > 0)
			{
				ptr = ref allocator.Allocate(sizeOf, alignOf, capacity);
				if (this.Capacity > 0)
				{
					int num = math.min(capacity, this.Capacity) * sizeOf;
					UnsafeUtility.MemCpy(ptr, this.Ptr, (long)num);
				}
			}
			ref allocator.Free(this.Ptr, sizeOf, alignOf, this.Capacity);
			this.Ptr = ptr;
			this.Capacity = capacity;
			this.Length = math.min(this.Length, capacity);
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0001CF41 File Offset: 0x0001B141
		private void Realloc(int sizeOf, int alignOf, int capacity)
		{
			this.Realloc<AllocatorManager.AllocatorHandle>(ref this.Allocator, sizeOf, alignOf, capacity);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0001CF54 File Offset: 0x0001B154
		private void SetCapacity<[IsUnmanaged] U>(ref U allocator, int sizeOf, int alignOf, int capacity) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			int num = math.max(capacity, 64 / sizeOf);
			num = math.ceilpow2(num);
			if (num == this.Capacity)
			{
				return;
			}
			this.Realloc<U>(ref allocator, sizeOf, alignOf, num);
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0001CF88 File Offset: 0x0001B188
		private void SetCapacity(int sizeOf, int alignOf, int capacity)
		{
			this.SetCapacity<AllocatorManager.AllocatorHandle>(ref this.Allocator, sizeOf, alignOf, capacity);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0001CF99 File Offset: 0x0001B199
		public void SetCapacity<T>(int capacity) where T : struct
		{
			this.SetCapacity(UnsafeUtility.SizeOf<T>(), UnsafeUtility.AlignOf<T>(), capacity);
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0001CFAC File Offset: 0x0001B1AC
		public void TrimExcess<T>() where T : struct
		{
			if (this.Capacity != this.Length)
			{
				this.Realloc(UnsafeUtility.SizeOf<T>(), UnsafeUtility.AlignOf<T>(), this.Length);
			}
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0001CFD2 File Offset: 0x0001B1D2
		public int IndexOf<T>(T value) where T : struct, IEquatable<T>
		{
			return NativeArrayExtensions.IndexOf<T, T>(this.Ptr, this.Length, value);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0001CFE6 File Offset: 0x0001B1E6
		public bool Contains<T>(T value) where T : struct, IEquatable<T>
		{
			return this.IndexOf<T>(value) != -1;
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0001CFF5 File Offset: 0x0001B1F5
		public void AddNoResize<T>(T value) where T : struct
		{
			UnsafeUtility.WriteArrayElement<T>(this.Ptr, this.Length, value);
			this.Length++;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0001D018 File Offset: 0x0001B218
		private unsafe void AddRangeNoResize(int sizeOf, void* ptr, int length)
		{
			void* destination = (void*)((byte*)this.Ptr + this.Length * sizeOf);
			UnsafeUtility.MemCpy(destination, ptr, (long)(length * sizeOf));
			this.Length += length;
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0001D04E File Offset: 0x0001B24E
		public unsafe void AddRangeNoResize<T>(void* ptr, int length) where T : struct
		{
			this.AddRangeNoResize(UnsafeUtility.SizeOf<T>(), ptr, length);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0001D05D File Offset: 0x0001B25D
		public void AddRangeNoResize<T>(UnsafeList list) where T : struct
		{
			this.AddRangeNoResize(UnsafeUtility.SizeOf<T>(), list.Ptr, CollectionHelper.AssumePositive(list.Length));
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0001D07C File Offset: 0x0001B27C
		public void Add<T>(T value) where T : struct
		{
			int length = this.Length;
			if (this.Length + 1 > this.Capacity)
			{
				this.Resize<T>(length + 1, NativeArrayOptions.UninitializedMemory);
			}
			else
			{
				this.Length++;
			}
			UnsafeUtility.WriteArrayElement<T>(this.Ptr, length, value);
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0001D0C8 File Offset: 0x0001B2C8
		private unsafe void AddRange(int sizeOf, int alignOf, void* ptr, int length)
		{
			int length2 = this.Length;
			if (this.Length + length > this.Capacity)
			{
				this.Resize(sizeOf, alignOf, this.Length + length, NativeArrayOptions.UninitializedMemory);
			}
			else
			{
				this.Length += length;
			}
			void* destination = (void*)((byte*)this.Ptr + length2 * sizeOf);
			UnsafeUtility.MemCpy(destination, ptr, (long)(length * sizeOf));
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0001D127 File Offset: 0x0001B327
		public unsafe void AddRange<T>(void* ptr, int length) where T : struct
		{
			this.AddRange(UnsafeUtility.SizeOf<T>(), UnsafeUtility.AlignOf<T>(), ptr, length);
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0001D13B File Offset: 0x0001B33B
		public void AddRange<T>(UnsafeList list) where T : struct
		{
			this.AddRange(UnsafeUtility.SizeOf<T>(), UnsafeUtility.AlignOf<T>(), list.Ptr, list.Length);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0001D15C File Offset: 0x0001B35C
		private unsafe void InsertRangeWithBeginEnd(int sizeOf, int alignOf, int begin, int end)
		{
			int num = end - begin;
			if (num < 1)
			{
				return;
			}
			int length = this.Length;
			if (this.Length + num > this.Capacity)
			{
				this.Resize(sizeOf, alignOf, this.Length + num, NativeArrayOptions.UninitializedMemory);
			}
			else
			{
				this.Length += num;
			}
			int num2 = length - begin;
			if (num2 < 1)
			{
				return;
			}
			int num3 = num2 * sizeOf;
			byte* ptr = (byte*)this.Ptr;
			void* destination = (void*)(ptr + end * sizeOf);
			byte* source = ptr + begin * sizeOf;
			UnsafeUtility.MemMove(destination, (void*)source, (long)num3);
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0001D1D3 File Offset: 0x0001B3D3
		public void InsertRangeWithBeginEnd<T>(int begin, int end) where T : struct
		{
			this.InsertRangeWithBeginEnd(UnsafeUtility.SizeOf<T>(), UnsafeUtility.AlignOf<T>(), begin, end);
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0001D1E8 File Offset: 0x0001B3E8
		private unsafe void RemoveRangeSwapBackWithBeginEnd(int sizeOf, int begin, int end)
		{
			int num = end - begin;
			if (num > 0)
			{
				int num2 = math.max(this.Length - num, end);
				void* destination = (void*)((byte*)this.Ptr + begin * sizeOf);
				void* source = (void*)((byte*)this.Ptr + num2 * sizeOf);
				UnsafeUtility.MemCpy(destination, source, (long)((this.Length - num2) * sizeOf));
				this.Length -= num;
			}
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0001D242 File Offset: 0x0001B442
		public void RemoveAtSwapBack<T>(int index) where T : struct
		{
			this.RemoveRangeSwapBackWithBeginEnd<T>(index, index + 1);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0001D24E File Offset: 0x0001B44E
		public void RemoveRangeSwapBackWithBeginEnd<T>(int begin, int end) where T : struct
		{
			this.RemoveRangeSwapBackWithBeginEnd(UnsafeUtility.SizeOf<T>(), begin, end);
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0001D260 File Offset: 0x0001B460
		private unsafe void RemoveRangeWithBeginEnd(int sizeOf, int begin, int end)
		{
			int num = end - begin;
			if (num > 0)
			{
				int num2 = math.min(begin + num, this.Length);
				void* destination = (void*)((byte*)this.Ptr + begin * sizeOf);
				void* source = (void*)((byte*)this.Ptr + num2 * sizeOf);
				UnsafeUtility.MemCpy(destination, source, (long)((this.Length - num2) * sizeOf));
				this.Length -= num;
			}
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0001D2BA File Offset: 0x0001B4BA
		public void RemoveAt<T>(int index) where T : struct
		{
			this.RemoveRangeWithBeginEnd<T>(index, index + 1);
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0001D2C6 File Offset: 0x0001B4C6
		public void RemoveRangeWithBeginEnd<T>(int begin, int end) where T : struct
		{
			this.RemoveRangeWithBeginEnd(UnsafeUtility.SizeOf<T>(), begin, end);
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0001D2D5 File Offset: 0x0001B4D5
		public UnsafeList.ParallelReader AsParallelReader()
		{
			return new UnsafeList.ParallelReader(this.Ptr, this.Length);
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0001D2E8 File Offset: 0x0001B4E8
		public unsafe UnsafeList.ParallelWriter AsParallelWriter()
		{
			return new UnsafeList.ParallelWriter(this.Ptr, (UnsafeList*)UnsafeUtility.AddressOf<UnsafeList>(ref this));
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0001D2FB File Offset: 0x0001B4FB
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		internal unsafe static void CheckNull(void* listData)
		{
			if (listData == null)
			{
				throw new Exception("UnsafeList has yet to be created or has been destroyed!");
			}
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0001D30D File Offset: 0x0001B50D
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckAllocator(Allocator a)
		{
			if (!CollectionHelper.ShouldDeallocate(a))
			{
				throw new Exception("UnsafeList is not initialized, it must be initialized with allocator before use.");
			}
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0001D327 File Offset: 0x0001B527
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckAllocator(AllocatorManager.AllocatorHandle a)
		{
			if (!CollectionHelper.ShouldDeallocate(a))
			{
				throw new Exception("UnsafeList is not initialized, it must be initialized with allocator before use.");
			}
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0001D33C File Offset: 0x0001B53C
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckBeginEnd(int begin, int end)
		{
			if (begin > end)
			{
				throw new ArgumentException(string.Format("Value for begin {0} index must less or equal to end {1}.", begin, end));
			}
			if (begin < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("Value for begin {0} must be positive.", begin));
			}
			if (begin > this.Length)
			{
				throw new ArgumentOutOfRangeException(string.Format("Value for begin {0} is out of bounds.", begin));
			}
			if (end > this.Length)
			{
				throw new ArgumentOutOfRangeException(string.Format("Value for end {0} is out of bounds.", end));
			}
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckNoResizeHasEnoughCapacity(int length)
		{
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0001D3C1 File Offset: 0x0001B5C1
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckNoResizeHasEnoughCapacity(int length, int index)
		{
			if (this.Capacity < index + length)
			{
				throw new Exception(string.Format("AddNoResize assumes that list capacity is sufficient (Capacity {0}, Length {1}), requested length {2}!", this.Capacity, this.Length, length));
			}
		}

		// Token: 0x04000328 RID: 808
		[NativeDisableUnsafePtrRestriction]
		public unsafe void* Ptr;

		// Token: 0x04000329 RID: 809
		public int Length;

		// Token: 0x0400032A RID: 810
		public readonly int unused;

		// Token: 0x0400032B RID: 811
		public int Capacity;

		// Token: 0x0400032C RID: 812
		public AllocatorManager.AllocatorHandle Allocator;

		// Token: 0x020000FD RID: 253
		public struct ParallelReader
		{
			// Token: 0x06000992 RID: 2450 RVA: 0x0001D3FA File Offset: 0x0001B5FA
			internal unsafe ParallelReader(void* ptr, int length)
			{
				this.Ptr = ptr;
				this.Length = length;
			}

			// Token: 0x06000993 RID: 2451 RVA: 0x0001D40A File Offset: 0x0001B60A
			public int IndexOf<T>(T value) where T : struct, IEquatable<T>
			{
				return NativeArrayExtensions.IndexOf<T, T>(this.Ptr, this.Length, value);
			}

			// Token: 0x06000994 RID: 2452 RVA: 0x0001D41E File Offset: 0x0001B61E
			public bool Contains<T>(T value) where T : struct, IEquatable<T>
			{
				return this.IndexOf<T>(value) != -1;
			}

			// Token: 0x0400032D RID: 813
			[NativeDisableUnsafePtrRestriction]
			public unsafe readonly void* Ptr;

			// Token: 0x0400032E RID: 814
			public readonly int Length;
		}

		// Token: 0x020000FE RID: 254
		public struct ParallelWriter
		{
			// Token: 0x06000995 RID: 2453 RVA: 0x0001D42D File Offset: 0x0001B62D
			internal unsafe ParallelWriter(void* ptr, UnsafeList* listData)
			{
				this.Ptr = ptr;
				this.ListData = listData;
			}

			// Token: 0x06000996 RID: 2454 RVA: 0x0001D440 File Offset: 0x0001B640
			public unsafe void AddNoResize<T>(T value) where T : struct
			{
				int index = Interlocked.Increment(ref this.ListData->Length) - 1;
				UnsafeUtility.WriteArrayElement<T>(this.Ptr, index, value);
			}

			// Token: 0x06000997 RID: 2455 RVA: 0x0001D470 File Offset: 0x0001B670
			private unsafe void AddRangeNoResize(int sizeOf, int alignOf, void* ptr, int length)
			{
				int num = Interlocked.Add(ref this.ListData->Length, length) - length;
				void* destination = (void*)((byte*)this.Ptr + num * sizeOf);
				UnsafeUtility.MemCpy(destination, ptr, (long)(length * sizeOf));
			}

			// Token: 0x06000998 RID: 2456 RVA: 0x0001D4AA File Offset: 0x0001B6AA
			public unsafe void AddRangeNoResize<T>(void* ptr, int length) where T : struct
			{
				this.AddRangeNoResize(UnsafeUtility.SizeOf<T>(), UnsafeUtility.AlignOf<T>(), ptr, length);
			}

			// Token: 0x06000999 RID: 2457 RVA: 0x0001D4BE File Offset: 0x0001B6BE
			public void AddRangeNoResize<T>(UnsafeList list) where T : struct
			{
				this.AddRangeNoResize(UnsafeUtility.SizeOf<T>(), UnsafeUtility.AlignOf<T>(), list.Ptr, list.Length);
			}

			// Token: 0x0400032F RID: 815
			[NativeDisableUnsafePtrRestriction]
			public unsafe readonly void* Ptr;

			// Token: 0x04000330 RID: 816
			[NativeDisableUnsafePtrRestriction]
			public unsafe UnsafeList* ListData;
		}
	}
}
