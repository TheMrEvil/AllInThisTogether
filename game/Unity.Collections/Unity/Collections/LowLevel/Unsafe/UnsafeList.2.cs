using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Jobs;
using Unity.Mathematics;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000123 RID: 291
	[DebuggerDisplay("Length = {Length}, Capacity = {Capacity}, IsCreated = {IsCreated}, IsEmpty = {IsEmpty}")]
	[DebuggerTypeProxy(typeof(UnsafeListTDebugView<>))]
	[BurstCompatible(GenericTypeArguments = new Type[]
	{
		typeof(int)
	})]
	public struct UnsafeList<[IsUnmanaged] T> : INativeDisposable, IDisposable, INativeList<T>, IIndexable<T>, IEnumerable<!0>, IEnumerable where T : struct, ValueType
	{
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x0001FA57 File Offset: 0x0001DC57
		// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x0001FA64 File Offset: 0x0001DC64
		public int Length
		{
			get
			{
				return CollectionHelper.AssumePositive(this.m_length);
			}
			set
			{
				if (value > this.Capacity)
				{
					this.Resize(value, NativeArrayOptions.UninitializedMemory);
					return;
				}
				this.m_length = value;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x0001FA7F File Offset: 0x0001DC7F
		// (set) Token: 0x06000AA2 RID: 2722 RVA: 0x0001FA8C File Offset: 0x0001DC8C
		public int Capacity
		{
			get
			{
				return CollectionHelper.AssumePositive(this.m_capacity);
			}
			set
			{
				this.SetCapacity(value);
			}
		}

		// Token: 0x17000129 RID: 297
		public unsafe T this[int index]
		{
			get
			{
				return this.Ptr[(IntPtr)CollectionHelper.AssumePositive(index) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
			}
			set
			{
				this.Ptr[(IntPtr)CollectionHelper.AssumePositive(index) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = value;
			}
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0001FACE File Offset: 0x0001DCCE
		public unsafe ref T ElementAt(int index)
		{
			return ref this.Ptr[(IntPtr)CollectionHelper.AssumePositive(index) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0001FAE5 File Offset: 0x0001DCE5
		public unsafe UnsafeList(T* ptr, int length)
		{
			this = default(UnsafeList<T>);
			this.Ptr = ptr;
			this.m_length = length;
			this.m_capacity = 0;
			this.Allocator = AllocatorManager.None;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0001FB10 File Offset: 0x0001DD10
		public unsafe UnsafeList(int initialCapacity, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			this = default(UnsafeList<T>);
			this.Ptr = null;
			this.m_length = 0;
			this.m_capacity = 0;
			this.Allocator = allocator;
			if (initialCapacity != 0)
			{
				this.SetCapacity(initialCapacity);
			}
			if (options == NativeArrayOptions.ClearMemory && this.Ptr != null)
			{
				int num = sizeof(T);
				UnsafeUtility.MemClear((void*)this.Ptr, (long)(this.Capacity * num));
			}
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0001FB74 File Offset: 0x0001DD74
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(AllocatorManager.AllocatorHandle)
		})]
		internal void Initialize<[IsUnmanaged] U>(int initialCapacity, ref U allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			this.Ptr = null;
			this.m_length = 0;
			this.m_capacity = 0;
			this.Allocator = AllocatorManager.None;
			this.Initialize<U>(initialCapacity, ref allocator, options);
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0001FBA0 File Offset: 0x0001DDA0
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(AllocatorManager.AllocatorHandle)
		})]
		internal static UnsafeList<T> New<[IsUnmanaged] U>(int initialCapacity, ref U allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			UnsafeList<T> result = default(UnsafeList<T>);
			result.Initialize<U>(initialCapacity, ref allocator, options);
			return result;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0001FBC0 File Offset: 0x0001DDC0
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(AllocatorManager.AllocatorHandle)
		})]
		internal unsafe static UnsafeList<T>* Create<[IsUnmanaged] U>(int initialCapacity, ref U allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			UnsafeList<T>* ptr = ref allocator.Allocate(default(UnsafeList<T>), 1);
			UnsafeUtility.MemClear((void*)ptr, (long)sizeof(UnsafeList<T>));
			ptr->Allocator = allocator.Handle;
			if (initialCapacity != 0)
			{
				ptr->SetCapacity<U>(ref allocator, initialCapacity);
			}
			if (options == NativeArrayOptions.ClearMemory && ptr->Ptr != null)
			{
				int num = sizeof(T);
				UnsafeUtility.MemClear((void*)ptr->Ptr, (long)(ptr->Capacity * num));
			}
			return ptr;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0001FC32 File Offset: 0x0001DE32
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(AllocatorManager.AllocatorHandle)
		})]
		internal unsafe static void Destroy<[IsUnmanaged] U>(UnsafeList<T>* listData, ref U allocator) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			listData->Dispose<U>(ref allocator);
			ref allocator.Free((void*)listData, sizeof(UnsafeList<T>), UnsafeUtility.AlignOf<UnsafeList<T>>(), 1);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0001FC4E File Offset: 0x0001DE4E
		public unsafe static UnsafeList<T>* Create(int initialCapacity, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			UnsafeList<T>* ptr = AllocatorManager.Allocate<UnsafeList<T>>(allocator, 1);
			*ptr = new UnsafeList<T>(initialCapacity, allocator, options);
			return ptr;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0001FC65 File Offset: 0x0001DE65
		public unsafe static void Destroy(UnsafeList<T>* listData)
		{
			AllocatorManager.AllocatorHandle allocator = listData->Allocator;
			listData->Dispose();
			AllocatorManager.Free<UnsafeList<T>>(allocator, listData, 1);
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x0001FC7A File Offset: 0x0001DE7A
		public bool IsEmpty
		{
			get
			{
				return !this.IsCreated || this.m_length == 0;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x0001FC8F File Offset: 0x0001DE8F
		public bool IsCreated
		{
			get
			{
				return this.Ptr != null;
			}
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0001FC9E File Offset: 0x0001DE9E
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(AllocatorManager.AllocatorHandle)
		})]
		internal void Dispose<[IsUnmanaged] U>(ref U allocator) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			ref allocator.Free(this.Ptr, this.m_length);
			this.Ptr = null;
			this.m_length = 0;
			this.m_capacity = 0;
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0001FCC8 File Offset: 0x0001DEC8
		public void Dispose()
		{
			if (CollectionHelper.ShouldDeallocate(this.Allocator))
			{
				AllocatorManager.Free<T>(this.Allocator, this.Ptr, 1);
				this.Allocator = AllocatorManager.Invalid;
			}
			this.Ptr = null;
			this.m_length = 0;
			this.m_capacity = 0;
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0001FD18 File Offset: 0x0001DF18
		[NotBurstCompatible]
		public unsafe JobHandle Dispose(JobHandle inputDeps)
		{
			if (CollectionHelper.ShouldDeallocate(this.Allocator))
			{
				JobHandle result = new UnsafeDisposeJob
				{
					Ptr = (void*)this.Ptr,
					Allocator = this.Allocator
				}.Schedule(inputDeps);
				this.Ptr = null;
				this.Allocator = AllocatorManager.Invalid;
				return result;
			}
			this.Ptr = null;
			return inputDeps;
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0001FD78 File Offset: 0x0001DF78
		public void Clear()
		{
			this.m_length = 0;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0001FD84 File Offset: 0x0001DF84
		public unsafe void Resize(int length, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory)
		{
			int num = this.m_length;
			if (length > this.Capacity)
			{
				this.SetCapacity(length);
			}
			this.m_length = length;
			if (options == NativeArrayOptions.ClearMemory && num < length)
			{
				int num2 = length - num;
				byte* ptr = (byte*)this.Ptr;
				int num3 = sizeof(T);
				UnsafeUtility.MemClear((void*)(ptr + num * num3), (long)(num2 * num3));
			}
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0001FDD8 File Offset: 0x0001DFD8
		private unsafe void Realloc<[IsUnmanaged] U>(ref U allocator, int newCapacity) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			T* ptr = null;
			int alignOf = UnsafeUtility.AlignOf<T>();
			int num = sizeof(T);
			if (newCapacity > 0)
			{
				ptr = (T*)ref allocator.Allocate(num, alignOf, newCapacity);
				if (this.m_capacity > 0)
				{
					int num2 = math.min(newCapacity, this.Capacity) * num;
					UnsafeUtility.MemCpy((void*)ptr, (void*)this.Ptr, (long)num2);
				}
			}
			ref allocator.Free(this.Ptr, this.Capacity);
			this.Ptr = ptr;
			this.m_capacity = newCapacity;
			this.m_length = math.min(this.m_length, newCapacity);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0001FE5B File Offset: 0x0001E05B
		private void Realloc(int capacity)
		{
			this.Realloc<AllocatorManager.AllocatorHandle>(ref this.Allocator, capacity);
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0001FE6C File Offset: 0x0001E06C
		private void SetCapacity<[IsUnmanaged] U>(ref U allocator, int capacity) where U : struct, ValueType, AllocatorManager.IAllocator
		{
			int num = sizeof(T);
			int num2 = math.max(capacity, 64 / num);
			num2 = math.ceilpow2(num2);
			if (num2 == this.Capacity)
			{
				return;
			}
			this.Realloc<U>(ref allocator, num2);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0001FEA4 File Offset: 0x0001E0A4
		public void SetCapacity(int capacity)
		{
			this.SetCapacity<AllocatorManager.AllocatorHandle>(ref this.Allocator, capacity);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0001FEB3 File Offset: 0x0001E0B3
		public void TrimExcess()
		{
			if (this.Capacity != this.m_length)
			{
				this.Realloc(this.m_length);
			}
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0001FECF File Offset: 0x0001E0CF
		public unsafe void AddNoResize(T value)
		{
			UnsafeUtility.WriteArrayElement<T>((void*)this.Ptr, this.m_length, value);
			this.m_length++;
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0001FEF4 File Offset: 0x0001E0F4
		public unsafe void AddRangeNoResize(void* ptr, int count)
		{
			int num = sizeof(T);
			void* destination = (void*)(this.Ptr + this.m_length * num / sizeof(T));
			UnsafeUtility.MemCpy(destination, ptr, (long)(count * num));
			this.m_length += count;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0001FF31 File Offset: 0x0001E131
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public unsafe void AddRangeNoResize(UnsafeList<T> list)
		{
			this.AddRangeNoResize((void*)list.Ptr, CollectionHelper.AssumePositive(list.m_length));
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0001FF4C File Offset: 0x0001E14C
		public unsafe void Add(in T value)
		{
			int num = this.m_length;
			if (this.m_length + 1 > this.Capacity)
			{
				this.Resize(num + 1, NativeArrayOptions.UninitializedMemory);
			}
			else
			{
				this.m_length++;
			}
			UnsafeUtility.WriteArrayElement<T>((void*)this.Ptr, num, value);
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0001FF9C File Offset: 0x0001E19C
		public unsafe void AddRange(void* ptr, int count)
		{
			int num = this.m_length;
			if (this.m_length + count > this.Capacity)
			{
				this.Resize(this.m_length + count, NativeArrayOptions.UninitializedMemory);
			}
			else
			{
				this.m_length += count;
			}
			int num2 = sizeof(T);
			void* destination = (void*)(this.Ptr + num * num2 / sizeof(T));
			UnsafeUtility.MemCpy(destination, ptr, (long)(count * num2));
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0001FFFC File Offset: 0x0001E1FC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public unsafe void AddRange(UnsafeList<T> list)
		{
			this.AddRange((void*)list.Ptr, list.Length);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00020014 File Offset: 0x0001E214
		public unsafe void InsertRangeWithBeginEnd(int begin, int end)
		{
			int num = end - begin;
			if (num < 1)
			{
				return;
			}
			int num2 = this.m_length;
			if (this.m_length + num > this.Capacity)
			{
				this.Resize(this.m_length + num, NativeArrayOptions.UninitializedMemory);
			}
			else
			{
				this.m_length += num;
			}
			int num3 = num2 - begin;
			if (num3 < 1)
			{
				return;
			}
			int num4 = sizeof(T);
			int num5 = num3 * num4;
			byte* ptr = (byte*)this.Ptr;
			void* destination = (void*)(ptr + end * num4);
			byte* source = ptr + begin * num4;
			UnsafeUtility.MemMove(destination, (void*)source, (long)num5);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00020091 File Offset: 0x0001E291
		public void RemoveAtSwapBack(int index)
		{
			this.RemoveRangeSwapBack(index, 1);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0002009C File Offset: 0x0001E29C
		public unsafe void RemoveRangeSwapBack(int index, int count)
		{
			if (count > 0)
			{
				int num = math.max(this.m_length - count, index + count);
				int num2 = sizeof(T);
				void* destination = (void*)(this.Ptr + index * num2 / sizeof(T));
				void* source = (void*)(this.Ptr + num * num2 / sizeof(T));
				UnsafeUtility.MemCpy(destination, source, (long)((this.m_length - num) * num2));
				this.m_length -= count;
			}
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x000200FC File Offset: 0x0001E2FC
		[Obsolete("RemoveRangeSwapBackWithBeginEnd(begin, end) is deprecated, use RemoveRangeSwapBack(index, count) instead. (RemovedAfter 2021-06-02)", false)]
		public unsafe void RemoveRangeSwapBackWithBeginEnd(int begin, int end)
		{
			int num = end - begin;
			if (num > 0)
			{
				int num2 = math.max(this.m_length - num, end);
				int num3 = sizeof(T);
				void* destination = (void*)(this.Ptr + begin * num3 / sizeof(T));
				void* source = (void*)(this.Ptr + num2 * num3 / sizeof(T));
				UnsafeUtility.MemCpy(destination, source, (long)((this.m_length - num2) * num3));
				this.m_length -= num;
			}
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0002015F File Offset: 0x0001E35F
		public void RemoveAt(int index)
		{
			this.RemoveRange(index, 1);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0002016C File Offset: 0x0001E36C
		public unsafe void RemoveRange(int index, int count)
		{
			if (count > 0)
			{
				int num = math.min(index + count, this.m_length);
				int num2 = sizeof(T);
				void* destination = (void*)(this.Ptr + index * num2 / sizeof(T));
				void* source = (void*)(this.Ptr + num * num2 / sizeof(T));
				UnsafeUtility.MemCpy(destination, source, (long)((this.m_length - num) * num2));
				this.m_length -= count;
			}
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x000201CC File Offset: 0x0001E3CC
		[Obsolete("RemoveRangeWithBeginEnd(begin, end) is deprecated, use RemoveRange(index, count) instead. (RemovedAfter 2021-06-02)", false)]
		public unsafe void RemoveRangeWithBeginEnd(int begin, int end)
		{
			int num = end - begin;
			if (num > 0)
			{
				int num2 = math.min(begin + num, this.m_length);
				int num3 = sizeof(T);
				void* destination = (void*)(this.Ptr + begin * num3 / sizeof(T));
				void* source = (void*)(this.Ptr + num2 * num3 / sizeof(T));
				UnsafeUtility.MemCpy(destination, source, (long)((this.m_length - num2) * num3));
				this.m_length -= num;
			}
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0002022F File Offset: 0x0001E42F
		public UnsafeList<T>.ParallelReader AsParallelReader()
		{
			return new UnsafeList<T>.ParallelReader(this.Ptr, this.Length);
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00020242 File Offset: 0x0001E442
		public unsafe UnsafeList<T>.ParallelWriter AsParallelWriter()
		{
			return new UnsafeList<T>.ParallelWriter((UnsafeList<T>*)UnsafeUtility.AddressOf<UnsafeList<T>>(ref this));
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0002024F File Offset: 0x0001E44F
		public unsafe void CopyFrom(UnsafeList<T> array)
		{
			this.Resize(array.Length, NativeArrayOptions.UninitializedMemory);
			UnsafeUtility.MemCpy((void*)this.Ptr, (void*)array.Ptr, (long)(UnsafeUtility.SizeOf<T>() * this.Length));
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00020280 File Offset: 0x0001E480
		public UnsafeList<T>.Enumerator GetEnumerator()
		{
			return new UnsafeList<T>.Enumerator
			{
				m_Ptr = this.Ptr,
				m_Length = this.Length,
				m_Index = -1
			};
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00004A71 File Offset: 0x00002C71
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0001D2FB File Offset: 0x0001B4FB
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		internal unsafe static void CheckNull(void* listData)
		{
			if (listData == null)
			{
				throw new Exception("UnsafeList has yet to be created or has been destroyed!");
			}
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x000202B8 File Offset: 0x0001E4B8
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckIndexCount(int index, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("Value for cound {0} must be positive.", count));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("Value for index {0} must be positive.", index));
			}
			if (index > this.Length)
			{
				throw new ArgumentOutOfRangeException(string.Format("Value for index {0} is out of bounds.", index));
			}
			if (index + count > this.Length)
			{
				throw new ArgumentOutOfRangeException(string.Format("Value for count {0} is out of bounds.", count));
			}
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0002033C File Offset: 0x0001E53C
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

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckNoResizeHasEnoughCapacity(int length)
		{
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x000203C1 File Offset: 0x0001E5C1
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckNoResizeHasEnoughCapacity(int length, int index)
		{
			if (this.Capacity < index + length)
			{
				throw new Exception(string.Format("AddNoResize assumes that list capacity is sufficient (Capacity {0}, Length {1}), requested length {2}!", this.Capacity, this.Length, length));
			}
		}

		// Token: 0x04000380 RID: 896
		[NativeDisableUnsafePtrRestriction]
		public unsafe T* Ptr;

		// Token: 0x04000381 RID: 897
		public int m_length;

		// Token: 0x04000382 RID: 898
		public int m_capacity;

		// Token: 0x04000383 RID: 899
		public AllocatorManager.AllocatorHandle Allocator;

		// Token: 0x04000384 RID: 900
		[Obsolete("Use Length property (UnityUpgradable) -> Length", true)]
		public int length;

		// Token: 0x04000385 RID: 901
		[Obsolete("Use Capacity property (UnityUpgradable) -> Capacity", true)]
		public int capacity;

		// Token: 0x02000124 RID: 292
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public struct ParallelReader
		{
			// Token: 0x06000AD2 RID: 2770 RVA: 0x000203FA File Offset: 0x0001E5FA
			internal unsafe ParallelReader(T* ptr, int length)
			{
				this.Ptr = ptr;
				this.Length = length;
			}

			// Token: 0x04000386 RID: 902
			[NativeDisableUnsafePtrRestriction]
			public unsafe readonly T* Ptr;

			// Token: 0x04000387 RID: 903
			public readonly int Length;
		}

		// Token: 0x02000125 RID: 293
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public struct ParallelWriter
		{
			// Token: 0x1700012C RID: 300
			// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0002040A File Offset: 0x0001E60A
			public unsafe readonly void* Ptr
			{
				get
				{
					return (void*)this.ListData->Ptr;
				}
			}

			// Token: 0x06000AD4 RID: 2772 RVA: 0x00020417 File Offset: 0x0001E617
			internal unsafe ParallelWriter(UnsafeList<T>* listData)
			{
				this.ListData = listData;
			}

			// Token: 0x06000AD5 RID: 2773 RVA: 0x00020420 File Offset: 0x0001E620
			[BurstCompatible(GenericTypeArguments = new Type[]
			{
				typeof(int)
			})]
			public unsafe void AddNoResize(T value)
			{
				int index = Interlocked.Increment(ref this.ListData->m_length) - 1;
				UnsafeUtility.WriteArrayElement<T>((void*)this.ListData->Ptr, index, value);
			}

			// Token: 0x06000AD6 RID: 2774 RVA: 0x00020454 File Offset: 0x0001E654
			[BurstCompatible(GenericTypeArguments = new Type[]
			{
				typeof(int)
			})]
			public unsafe void AddRangeNoResize(void* ptr, int count)
			{
				int num = Interlocked.Add(ref this.ListData->m_length, count) - count;
				void* destination = (void*)(this.ListData->Ptr + num * sizeof(T) / sizeof(T));
				UnsafeUtility.MemCpy(destination, ptr, (long)(count * sizeof(T)));
			}

			// Token: 0x06000AD7 RID: 2775 RVA: 0x0002049A File Offset: 0x0001E69A
			[BurstCompatible(GenericTypeArguments = new Type[]
			{
				typeof(int)
			})]
			public unsafe void AddRangeNoResize(UnsafeList<T> list)
			{
				this.AddRangeNoResize((void*)list.Ptr, list.Length);
			}

			// Token: 0x04000388 RID: 904
			[NativeDisableUnsafePtrRestriction]
			public unsafe UnsafeList<T>* ListData;
		}

		// Token: 0x02000126 RID: 294
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06000AD8 RID: 2776 RVA: 0x00002C2B File Offset: 0x00000E2B
			public void Dispose()
			{
			}

			// Token: 0x06000AD9 RID: 2777 RVA: 0x000204B0 File Offset: 0x0001E6B0
			public bool MoveNext()
			{
				int num = this.m_Index + 1;
				this.m_Index = num;
				return num < this.m_Length;
			}

			// Token: 0x06000ADA RID: 2778 RVA: 0x000204D6 File Offset: 0x0001E6D6
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x06000ADB RID: 2779 RVA: 0x000204DF File Offset: 0x0001E6DF
			public unsafe T Current
			{
				get
				{
					return this.m_Ptr[(IntPtr)this.m_Index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
				}
			}

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x06000ADC RID: 2780 RVA: 0x000204FB File Offset: 0x0001E6FB
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04000389 RID: 905
			internal unsafe T* m_Ptr;

			// Token: 0x0400038A RID: 906
			internal int m_Length;

			// Token: 0x0400038B RID: 907
			internal int m_Index;
		}
	}
}
