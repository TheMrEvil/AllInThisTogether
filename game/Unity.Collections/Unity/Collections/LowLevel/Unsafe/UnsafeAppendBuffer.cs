using System;
using System.Diagnostics;
using Unity.Collections.LowLevel.Unsafe.NotBurstCompatible;
using Unity.Jobs;
using Unity.Mathematics;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200011B RID: 283
	[BurstCompatible]
	public struct UnsafeAppendBuffer : INativeDisposable, IDisposable
	{
		// Token: 0x06000A5A RID: 2650 RVA: 0x0001E98E File Offset: 0x0001CB8E
		public UnsafeAppendBuffer(int initialCapacity, int alignment, AllocatorManager.AllocatorHandle allocator)
		{
			this.Alignment = alignment;
			this.Allocator = allocator;
			this.Ptr = null;
			this.Length = 0;
			this.Capacity = 0;
			this.SetCapacity(initialCapacity);
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0001E9BB File Offset: 0x0001CBBB
		public unsafe UnsafeAppendBuffer(void* ptr, int length)
		{
			this.Alignment = 0;
			this.Allocator = AllocatorManager.None;
			this.Ptr = (byte*)ptr;
			this.Length = 0;
			this.Capacity = length;
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x0001E9E4 File Offset: 0x0001CBE4
		public bool IsEmpty
		{
			get
			{
				return this.Length == 0;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x0001E9EF File Offset: 0x0001CBEF
		public bool IsCreated
		{
			get
			{
				return this.Ptr != null;
			}
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0001EA00 File Offset: 0x0001CC00
		public void Dispose()
		{
			if (CollectionHelper.ShouldDeallocate(this.Allocator))
			{
				Memory.Unmanaged.Free<byte>(this.Ptr, this.Allocator);
				this.Allocator = AllocatorManager.Invalid;
			}
			this.Ptr = null;
			this.Length = 0;
			this.Capacity = 0;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0001EA4C File Offset: 0x0001CC4C
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

		// Token: 0x06000A60 RID: 2656 RVA: 0x0001EAAC File Offset: 0x0001CCAC
		public void Reset()
		{
			this.Length = 0;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0001EAB8 File Offset: 0x0001CCB8
		public unsafe void SetCapacity(int capacity)
		{
			if (capacity <= this.Capacity)
			{
				return;
			}
			capacity = math.max(64, math.ceilpow2(capacity));
			byte* ptr = (byte*)Memory.Unmanaged.Allocate((long)capacity, this.Alignment, this.Allocator);
			if (this.Ptr != null)
			{
				UnsafeUtility.MemCpy((void*)ptr, (void*)this.Ptr, (long)this.Length);
				Memory.Unmanaged.Free<byte>(this.Ptr, this.Allocator);
			}
			this.Ptr = ptr;
			this.Capacity = capacity;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0001EB2E File Offset: 0x0001CD2E
		public void ResizeUninitialized(int length)
		{
			this.SetCapacity(length);
			this.Length = length;
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0001EB40 File Offset: 0x0001CD40
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public unsafe void Add<T>(T value) where T : struct
		{
			int num = UnsafeUtility.SizeOf<T>();
			this.SetCapacity(this.Length + num);
			UnsafeUtility.CopyStructureToPtr<T>(ref value, (void*)(this.Ptr + this.Length));
			this.Length += num;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0001EB83 File Offset: 0x0001CD83
		public unsafe void Add(void* ptr, int structSize)
		{
			this.SetCapacity(this.Length + structSize);
			UnsafeUtility.MemCpy((void*)(this.Ptr + this.Length), ptr, (long)structSize);
			this.Length += structSize;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0001EBB6 File Offset: 0x0001CDB6
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public unsafe void AddArray<T>(void* ptr, int length) where T : struct
		{
			this.Add<int>(length);
			if (length != 0)
			{
				this.Add(ptr, length * UnsafeUtility.SizeOf<T>());
			}
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0001EBD0 File Offset: 0x0001CDD0
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public void Add<T>(NativeArray<T> value) where T : struct
		{
			this.Add<int>(value.Length);
			this.Add(value.GetUnsafeReadOnlyPtr<T>(), UnsafeUtility.SizeOf<T>() * value.Length);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0001EBF8 File Offset: 0x0001CDF8
		[NotBurstCompatible]
		[Obsolete("Please use `AddNBC` from `Unity.Collections.LowLevel.Unsafe.NotBurstCompatible` namespace instead. (RemovedAfter 2021-06-22)", false)]
		public void Add(string value)
		{
			ref this.AddNBC(value);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0001EC04 File Offset: 0x0001CE04
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public unsafe T Pop<T>() where T : struct
		{
			int num = UnsafeUtility.SizeOf<T>();
			byte* ptr = this.Ptr;
			long num2 = (long)this.Length;
			T result = UnsafeUtility.ReadArrayElement<T>((void*)((byte*)((byte*)ptr + num2) - (long)num), 0);
			this.Length -= num;
			return result;
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0001EC40 File Offset: 0x0001CE40
		public unsafe void Pop(void* ptr, int structSize)
		{
			long num = this.Ptr;
			long num2 = (long)this.Length;
			long num3 = num + num2 - (long)structSize;
			UnsafeUtility.MemCpy(ptr, num3, (long)structSize);
			this.Length -= structSize;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0001EC7A File Offset: 0x0001CE7A
		[NotBurstCompatible]
		[Obsolete("Please use `ToBytesNBC` from `Unity.Collections.LowLevel.Unsafe.NotBurstCompatible` namespace instead. (RemovedAfter 2021-06-22)", false)]
		public byte[] ToBytes()
		{
			return ref this.ToBytesNBC();
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0001EC82 File Offset: 0x0001CE82
		public UnsafeAppendBuffer.Reader AsReader()
		{
			return new UnsafeAppendBuffer.Reader(ref this);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0001EC8C File Offset: 0x0001CE8C
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckAlignment(int alignment)
		{
			int num = (alignment == 0) ? 1 : 0;
			bool flag = (alignment - 1 & alignment) == 0;
			if (num != 0 || !flag)
			{
				throw new ArgumentException(string.Format("Specified alignment must be non-zero positive power of two. Requested: {0}", alignment));
			}
		}

		// Token: 0x0400036A RID: 874
		[NativeDisableUnsafePtrRestriction]
		public unsafe byte* Ptr;

		// Token: 0x0400036B RID: 875
		public int Length;

		// Token: 0x0400036C RID: 876
		public int Capacity;

		// Token: 0x0400036D RID: 877
		public AllocatorManager.AllocatorHandle Allocator;

		// Token: 0x0400036E RID: 878
		public readonly int Alignment;

		// Token: 0x0200011C RID: 284
		[BurstCompatible]
		public struct Reader
		{
			// Token: 0x06000A6D RID: 2669 RVA: 0x0001ECC3 File Offset: 0x0001CEC3
			public Reader(ref UnsafeAppendBuffer buffer)
			{
				this.Ptr = buffer.Ptr;
				this.Size = buffer.Length;
				this.Offset = 0;
			}

			// Token: 0x06000A6E RID: 2670 RVA: 0x0001ECE4 File Offset: 0x0001CEE4
			public unsafe Reader(void* ptr, int length)
			{
				this.Ptr = (byte*)ptr;
				this.Size = length;
				this.Offset = 0;
			}

			// Token: 0x17000124 RID: 292
			// (get) Token: 0x06000A6F RID: 2671 RVA: 0x0001ECFB File Offset: 0x0001CEFB
			public bool EndOfBuffer
			{
				get
				{
					return this.Offset == this.Size;
				}
			}

			// Token: 0x06000A70 RID: 2672 RVA: 0x0001ED0C File Offset: 0x0001CF0C
			[BurstCompatible(GenericTypeArguments = new Type[]
			{
				typeof(int)
			})]
			public unsafe void ReadNext<T>(out T value) where T : struct
			{
				int num = UnsafeUtility.SizeOf<T>();
				UnsafeUtility.CopyPtrToStructure<T>((void*)(this.Ptr + this.Offset), out value);
				this.Offset += num;
			}

			// Token: 0x06000A71 RID: 2673 RVA: 0x0001ED40 File Offset: 0x0001CF40
			[BurstCompatible(GenericTypeArguments = new Type[]
			{
				typeof(int)
			})]
			public unsafe T ReadNext<T>() where T : struct
			{
				int num = UnsafeUtility.SizeOf<T>();
				T result = UnsafeUtility.ReadArrayElement<T>((void*)(this.Ptr + this.Offset), 0);
				this.Offset += num;
				return result;
			}

			// Token: 0x06000A72 RID: 2674 RVA: 0x0001ED74 File Offset: 0x0001CF74
			public unsafe void* ReadNext(int structSize)
			{
				void* result = (void*)((IntPtr)((void*)this.Ptr) + this.Offset);
				this.Offset += structSize;
				return result;
			}

			// Token: 0x06000A73 RID: 2675 RVA: 0x0001EDA0 File Offset: 0x0001CFA0
			[BurstCompatible(GenericTypeArguments = new Type[]
			{
				typeof(int)
			})]
			public unsafe void ReadNext<T>(out NativeArray<T> value, AllocatorManager.AllocatorHandle allocator) where T : struct
			{
				int num = this.ReadNext<int>();
				value = CollectionHelper.CreateNativeArray<T>(num, allocator, NativeArrayOptions.ClearMemory);
				int num2 = num * UnsafeUtility.SizeOf<T>();
				if (num2 > 0)
				{
					void* source = this.ReadNext(num2);
					UnsafeUtility.MemCpy(value.GetUnsafePtr<T>(), source, (long)num2);
				}
			}

			// Token: 0x06000A74 RID: 2676 RVA: 0x0001EDE9 File Offset: 0x0001CFE9
			[BurstCompatible(GenericTypeArguments = new Type[]
			{
				typeof(int)
			})]
			public unsafe void* ReadNextArray<T>(out int length) where T : struct
			{
				length = this.ReadNext<int>();
				if (length != 0)
				{
					return this.ReadNext(length * UnsafeUtility.SizeOf<T>());
				}
				return null;
			}

			// Token: 0x06000A75 RID: 2677 RVA: 0x0001EE08 File Offset: 0x0001D008
			[NotBurstCompatible]
			[Obsolete("Please use `ReadNextNBC` from `Unity.Collections.LowLevel.Unsafe.NotBurstCompatible` namespace instead. (RemovedAfter 2021-06-22)", false)]
			public void ReadNext(out string value)
			{
				ref this.ReadNextNBC(out value);
			}

			// Token: 0x06000A76 RID: 2678 RVA: 0x0001EE11 File Offset: 0x0001D011
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private void CheckBounds(int structSize)
			{
				if (this.Offset + structSize > this.Size)
				{
					throw new ArgumentException(string.Format("Requested value outside bounds of UnsafeAppendOnlyBuffer. Remaining bytes: {0} Requested: {1}", this.Size - this.Offset, structSize));
				}
			}

			// Token: 0x0400036F RID: 879
			public unsafe readonly byte* Ptr;

			// Token: 0x04000370 RID: 880
			public readonly int Size;

			// Token: 0x04000371 RID: 881
			public int Offset;
		}
	}
}
