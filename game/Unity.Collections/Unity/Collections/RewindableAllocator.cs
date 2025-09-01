using System;
using System.Runtime.CompilerServices;
using System.Threading;
using AOT;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x020000E6 RID: 230
	[BurstCompile]
	public struct RewindableAllocator : AllocatorManager.IAllocator, IDisposable
	{
		// Token: 0x060008C0 RID: 2240 RVA: 0x00019EA4 File Offset: 0x000180A4
		public unsafe void Initialize(int initialSizeInBytes, bool enableBlockFree = false)
		{
			this.m_spinner = default(Spinner);
			this.m_block = new UnmanagedArray<RewindableAllocator.MemoryBlock>(64, Allocator.Persistent);
			*this.m_block[0] = new RewindableAllocator.MemoryBlock((long)initialSizeInBytes);
			this.m_last = (this.m_used = (this.m_best = 0));
			this.m_enableBlockFree = enableBlockFree;
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x00019F08 File Offset: 0x00018108
		// (set) Token: 0x060008C2 RID: 2242 RVA: 0x00019F10 File Offset: 0x00018110
		public bool EnableBlockFree
		{
			get
			{
				return this.m_enableBlockFree;
			}
			set
			{
				this.m_enableBlockFree = value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x00019F19 File Offset: 0x00018119
		public int BlocksAllocated
		{
			get
			{
				return this.m_last + 1;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x00019F23 File Offset: 0x00018123
		public int InitialSizeInBytes
		{
			get
			{
				return (int)this.m_block[0].m_bytes;
			}
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00019F38 File Offset: 0x00018138
		public void Rewind()
		{
			if (JobsUtility.IsExecutingJob)
			{
				throw new InvalidOperationException("You cannot Rewind a RewindableAllocator from a Job.");
			}
			this.m_handle.Rewind();
			while (this.m_last > this.m_used)
			{
				int num = this.m_last;
				this.m_last = num - 1;
				this.m_block[num].Dispose();
			}
			while (this.m_used > 0)
			{
				int num = this.m_used;
				this.m_used = num - 1;
				this.m_block[num].Rewind();
			}
			this.m_block[0].Rewind();
			this.m_best = 0;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00019FD8 File Offset: 0x000181D8
		public void Dispose()
		{
			if (JobsUtility.IsExecutingJob)
			{
				throw new InvalidOperationException("You cannot Dispose a RewindableAllocator from a Job.");
			}
			this.m_used = 0;
			this.Rewind();
			this.m_block[0].Dispose();
			this.m_block.Dispose();
			this.m_last = (this.m_used = (this.m_best = 0));
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x0001A039 File Offset: 0x00018239
		[NotBurstCompatible]
		public AllocatorManager.TryFunction Function
		{
			get
			{
				return new AllocatorManager.TryFunction(RewindableAllocator.Try);
			}
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0001A048 File Offset: 0x00018248
		public unsafe int Try(ref AllocatorManager.Block block)
		{
			if (block.Range.Pointer == IntPtr.Zero)
			{
				int num = this.m_block[this.m_best].TryAllocate(ref block);
				if (num == 0)
				{
					return num;
				}
				this.m_spinner.Lock();
				int i;
				for (i = 0; i <= this.m_last; i++)
				{
					num = this.m_block[i].TryAllocate(ref block);
					if (num == 0)
					{
						this.m_used = ((i > this.m_used) ? i : this.m_used);
						this.m_best = i;
						this.m_spinner.Unlock();
						return num;
					}
				}
				long bytes = math.max(this.m_block[0].m_bytes << i, math.ceilpow2(block.Bytes));
				*this.m_block[i] = new RewindableAllocator.MemoryBlock(bytes);
				num = this.m_block[i].TryAllocate(ref block);
				this.m_best = i;
				this.m_used = i;
				this.m_last = i;
				this.m_spinner.Unlock();
				return num;
			}
			else
			{
				if (block.Range.Items == 0)
				{
					if (this.m_enableBlockFree)
					{
						this.m_spinner.Lock();
						if (this.m_block[this.m_best].Contains(block.Range.Pointer) && Interlocked.Decrement(ref this.m_block[this.m_best].m_allocations) == 0L)
						{
							this.m_block[this.m_best].Rewind();
						}
						this.m_spinner.Unlock();
					}
					return 0;
				}
				return -1;
			}
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0001A1E1 File Offset: 0x000183E1
		[BurstCompile]
		[MonoPInvokeCallback(typeof(AllocatorManager.TryFunction))]
		internal static int Try(IntPtr state, ref AllocatorManager.Block block)
		{
			return RewindableAllocator.Try_00000756$BurstDirectCall.Invoke(state, ref block);
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x0001A1EA File Offset: 0x000183EA
		// (set) Token: 0x060008CB RID: 2251 RVA: 0x0001A1F2 File Offset: 0x000183F2
		public AllocatorManager.AllocatorHandle Handle
		{
			get
			{
				return this.m_handle;
			}
			set
			{
				this.m_handle = value;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x0001A1FB File Offset: 0x000183FB
		public Allocator ToAllocator
		{
			get
			{
				return this.m_handle.ToAllocator;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x0001A208 File Offset: 0x00018408
		public bool IsCustomAllocator
		{
			get
			{
				return this.m_handle.IsCustomAllocator;
			}
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0001A218 File Offset: 0x00018418
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public NativeArray<T> AllocateNativeArray<T>(int length) where T : struct
		{
			return new NativeArray<T>
			{
				m_Buffer = ref this.AllocateStruct(default(T), length),
				m_Length = length,
				m_AllocatorLabel = Allocator.None
			};
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0001A258 File Offset: 0x00018458
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public unsafe NativeList<T> AllocateNativeList<[IsUnmanaged] T>(int capacity) where T : struct, ValueType
		{
			NativeList<T> nativeList = default(NativeList<T>);
			nativeList.m_ListData = ref this.Allocate(default(UnsafeList<T>), 1);
			nativeList.m_ListData->Ptr = ref this.Allocate(default(T), capacity);
			nativeList.m_ListData->m_capacity = capacity;
			nativeList.m_ListData->m_length = 0;
			nativeList.m_ListData->Allocator = Allocator.None;
			nativeList.m_DeprecatedAllocator = Allocator.None;
			return nativeList;
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0001A2D6 File Offset: 0x000184D6
		[BurstCompile]
		[MonoPInvokeCallback(typeof(AllocatorManager.TryFunction))]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int Try$BurstManaged(IntPtr state, ref AllocatorManager.Block block)
		{
			return ((RewindableAllocator*)((void*)state))->Try(ref block);
		}

		// Token: 0x040002DC RID: 732
		private Spinner m_spinner;

		// Token: 0x040002DD RID: 733
		private AllocatorManager.AllocatorHandle m_handle;

		// Token: 0x040002DE RID: 734
		private UnmanagedArray<RewindableAllocator.MemoryBlock> m_block;

		// Token: 0x040002DF RID: 735
		private int m_best;

		// Token: 0x040002E0 RID: 736
		private int m_last;

		// Token: 0x040002E1 RID: 737
		private int m_used;

		// Token: 0x040002E2 RID: 738
		private bool m_enableBlockFree;

		// Token: 0x020000E7 RID: 231
		[BurstCompatible]
		internal struct MemoryBlock : IDisposable
		{
			// Token: 0x060008D1 RID: 2257 RVA: 0x0001A2E4 File Offset: 0x000184E4
			public unsafe MemoryBlock(long bytes)
			{
				this.m_pointer = (byte*)Memory.Unmanaged.Allocate(bytes, 16384, Allocator.Persistent);
				this.m_bytes = bytes;
				this.m_current = 0L;
				this.m_allocations = 0L;
			}

			// Token: 0x060008D2 RID: 2258 RVA: 0x0001A314 File Offset: 0x00018514
			public void Rewind()
			{
				this.m_current = 0L;
				this.m_allocations = 0L;
			}

			// Token: 0x060008D3 RID: 2259 RVA: 0x0001A326 File Offset: 0x00018526
			public void Dispose()
			{
				Memory.Unmanaged.Free<byte>(this.m_pointer, Allocator.Persistent);
				this.m_pointer = null;
				this.m_bytes = 0L;
				this.m_current = 0L;
				this.m_allocations = 0L;
			}

			// Token: 0x060008D4 RID: 2260 RVA: 0x0001A35C File Offset: 0x0001855C
			public unsafe int TryAllocate(ref AllocatorManager.Block block)
			{
				int num = math.max(64, block.Alignment);
				int num2 = (num != 64) ? 1 : 0;
				int num3 = 63;
				if (num2 == 1)
				{
					num = (num + num3 & ~num3);
				}
				long num4 = (long)num - 1L;
				long num5 = block.Bytes + (long)(num2 * num) + num4 & ~num4;
				long num6 = Interlocked.Read(ref this.m_current);
				long num7;
				for (;;)
				{
					long value = num6 + num5;
					num7 = (num6 + num4 & ~num4);
					if (num7 + block.Bytes > this.m_bytes)
					{
						break;
					}
					long num8 = num6;
					num6 = Interlocked.CompareExchange(ref this.m_current, value, num8);
					if (num6 == num8)
					{
						goto Block_4;
					}
				}
				return -1;
				Block_4:
				block.Range.Pointer = (IntPtr)((void*)(this.m_pointer + num7));
				block.AllocatedItems = block.Range.Items;
				Interlocked.Increment(ref this.m_allocations);
				return 0;
			}

			// Token: 0x060008D5 RID: 2261 RVA: 0x0001A42C File Offset: 0x0001862C
			public unsafe bool Contains(IntPtr ptr)
			{
				void* ptr2 = (void*)ptr;
				return ptr2 >= (void*)this.m_pointer && ptr2 < (void*)(this.m_pointer + this.m_current);
			}

			// Token: 0x040002E3 RID: 739
			public const int kMaximumAlignment = 16384;

			// Token: 0x040002E4 RID: 740
			public unsafe byte* m_pointer;

			// Token: 0x040002E5 RID: 741
			public long m_bytes;

			// Token: 0x040002E6 RID: 742
			public long m_current;

			// Token: 0x040002E7 RID: 743
			public long m_allocations;
		}

		// Token: 0x020000E8 RID: 232
		// (Invoke) Token: 0x060008D7 RID: 2263
		public delegate int Try_00000756$PostfixBurstDelegate(IntPtr state, ref AllocatorManager.Block block);

		// Token: 0x020000E9 RID: 233
		internal static class Try_00000756$BurstDirectCall
		{
			// Token: 0x060008DA RID: 2266 RVA: 0x0001A45C File Offset: 0x0001865C
			[BurstDiscard]
			private unsafe static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (RewindableAllocator.Try_00000756$BurstDirectCall.Pointer == 0)
				{
					RewindableAllocator.Try_00000756$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(RewindableAllocator.Try_00000756$BurstDirectCall.DeferredCompilation, methodof(RewindableAllocator.Try$BurstManaged(IntPtr, AllocatorManager.Block*)).MethodHandle, typeof(RewindableAllocator.Try_00000756$PostfixBurstDelegate).TypeHandle);
				}
				A_0 = RewindableAllocator.Try_00000756$BurstDirectCall.Pointer;
			}

			// Token: 0x060008DB RID: 2267 RVA: 0x0001A488 File Offset: 0x00018688
			private static IntPtr GetFunctionPointer()
			{
				IntPtr result = (IntPtr)0;
				RewindableAllocator.Try_00000756$BurstDirectCall.GetFunctionPointerDiscard(ref result);
				return result;
			}

			// Token: 0x060008DC RID: 2268 RVA: 0x0001A4A0 File Offset: 0x000186A0
			public unsafe static void Constructor()
			{
				RewindableAllocator.Try_00000756$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(RewindableAllocator.Try(IntPtr, AllocatorManager.Block*)).MethodHandle);
			}

			// Token: 0x060008DD RID: 2269 RVA: 0x00002C2B File Offset: 0x00000E2B
			public static void Initialize()
			{
			}

			// Token: 0x060008DE RID: 2270 RVA: 0x0001A4B1 File Offset: 0x000186B1
			// Note: this type is marked as 'beforefieldinit'.
			static Try_00000756$BurstDirectCall()
			{
				RewindableAllocator.Try_00000756$BurstDirectCall.Constructor();
			}

			// Token: 0x060008DF RID: 2271 RVA: 0x0001A4B8 File Offset: 0x000186B8
			public static int Invoke(IntPtr state, ref AllocatorManager.Block block)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = RewindableAllocator.Try_00000756$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						return calli(System.Int32(System.IntPtr,Unity.Collections.AllocatorManager/Block&), state, ref block, functionPointer);
					}
				}
				return RewindableAllocator.Try$BurstManaged(state, ref block);
			}

			// Token: 0x040002E8 RID: 744
			private static IntPtr Pointer;

			// Token: 0x040002E9 RID: 745
			private static IntPtr DeferredCompilation;
		}
	}
}
