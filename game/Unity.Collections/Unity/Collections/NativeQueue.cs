using System;
using System.Diagnostics;
using System.Threading;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x020000CD RID: 205
	[NativeContainer]
	[BurstCompatible(GenericTypeArguments = new Type[]
	{
		typeof(int)
	})]
	public struct NativeQueue<T> : INativeDisposable, IDisposable where T : struct
	{
		// Token: 0x060007A7 RID: 1959 RVA: 0x000177E9 File Offset: 0x000159E9
		public NativeQueue(AllocatorManager.AllocatorHandle allocator)
		{
			this.m_QueuePool = NativeQueueBlockPool.GetQueueBlockPool();
			this.m_AllocatorLabel = allocator;
			NativeQueueData.AllocateQueue<T>(allocator, out this.m_Buffer);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0001780C File Offset: 0x00015A0C
		public unsafe bool IsEmpty()
		{
			if (!this.IsCreated)
			{
				return true;
			}
			int num = 0;
			int currentRead = this.m_Buffer->m_CurrentRead;
			for (NativeQueueBlockHeader* ptr = (NativeQueueBlockHeader*)((void*)this.m_Buffer->m_FirstBlock); ptr != null; ptr = ptr->m_NextBlock)
			{
				num += ptr->m_NumItems;
				if (num > currentRead)
				{
					return false;
				}
			}
			return num == currentRead;
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x00017864 File Offset: 0x00015A64
		public unsafe int Count
		{
			get
			{
				int num = 0;
				for (NativeQueueBlockHeader* ptr = (NativeQueueBlockHeader*)((void*)this.m_Buffer->m_FirstBlock); ptr != null; ptr = ptr->m_NextBlock)
				{
					num += ptr->m_NumItems;
				}
				return num - this.m_Buffer->m_CurrentRead;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x000178A8 File Offset: 0x00015AA8
		// (set) Token: 0x060007AB RID: 1963 RVA: 0x000178B4 File Offset: 0x00015AB4
		internal unsafe static int PersistentMemoryBlockCount
		{
			get
			{
				return NativeQueueBlockPool.GetQueueBlockPool()->m_MaxBlocks;
			}
			set
			{
				Interlocked.Exchange(ref NativeQueueBlockPool.GetQueueBlockPool()->m_MaxBlocks, value);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x000178C7 File Offset: 0x00015AC7
		internal static int MemoryBlockSize
		{
			get
			{
				return 16384;
			}
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x000178D0 File Offset: 0x00015AD0
		public unsafe T Peek()
		{
			NativeQueueBlockHeader* ptr = (NativeQueueBlockHeader*)((void*)this.m_Buffer->m_FirstBlock);
			return UnsafeUtility.ReadArrayElement<T>((void*)(ptr + 1), this.m_Buffer->m_CurrentRead);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00017908 File Offset: 0x00015B08
		public unsafe void Enqueue(T value)
		{
			NativeQueueBlockHeader* ptr = NativeQueueData.AllocateWriteBlockMT<T>(this.m_Buffer, this.m_QueuePool, 0);
			UnsafeUtility.WriteArrayElement<T>((void*)(ptr + 1), ptr->m_NumItems, value);
			ptr->m_NumItems++;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00017948 File Offset: 0x00015B48
		public T Dequeue()
		{
			T result;
			this.TryDequeue(out result);
			return result;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00017960 File Offset: 0x00015B60
		public unsafe bool TryDequeue(out T item)
		{
			NativeQueueBlockHeader* ptr = (NativeQueueBlockHeader*)((void*)this.m_Buffer->m_FirstBlock);
			if (ptr == null)
			{
				item = default(T);
				return false;
			}
			NativeQueueData* buffer = this.m_Buffer;
			int currentRead = buffer->m_CurrentRead;
			buffer->m_CurrentRead = currentRead + 1;
			int index = currentRead;
			item = UnsafeUtility.ReadArrayElement<T>((void*)(ptr + 1), index);
			if (this.m_Buffer->m_CurrentRead >= ptr->m_NumItems)
			{
				this.m_Buffer->m_CurrentRead = 0;
				this.m_Buffer->m_FirstBlock = (IntPtr)((void*)ptr->m_NextBlock);
				if (this.m_Buffer->m_FirstBlock == IntPtr.Zero)
				{
					this.m_Buffer->m_LastBlock = IntPtr.Zero;
				}
				for (int i = 0; i < 128; i++)
				{
					if (this.m_Buffer->GetCurrentWriteBlockTLS(i) == ptr)
					{
						this.m_Buffer->SetCurrentWriteBlockTLS(i, null);
					}
				}
				this.m_QueuePool->FreeBlock(ptr);
			}
			return true;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00017A50 File Offset: 0x00015C50
		public unsafe NativeArray<T> ToArray(AllocatorManager.AllocatorHandle allocator)
		{
			NativeQueueBlockHeader* ptr = (NativeQueueBlockHeader*)((void*)this.m_Buffer->m_FirstBlock);
			NativeArray<T> nativeArray = CollectionHelper.CreateNativeArray<T>(this.Count, allocator, NativeArrayOptions.ClearMemory);
			NativeQueueBlockHeader* ptr2 = ptr;
			byte* unsafePtr = (byte*)nativeArray.GetUnsafePtr<T>();
			int num = UnsafeUtility.SizeOf<T>();
			int num2 = 0;
			int num3 = this.m_Buffer->m_CurrentRead * num;
			int num4 = this.m_Buffer->m_CurrentRead;
			while (ptr2 != null)
			{
				int num5 = (ptr2->m_NumItems - num4) * num;
				UnsafeUtility.MemCpy((void*)(unsafePtr + num2), (void*)(ptr2 + 1 + num3 / sizeof(NativeQueueBlockHeader)), (long)num5);
				num4 = (num3 = 0);
				num2 += num5;
				ptr2 = ptr2->m_NextBlock;
			}
			return nativeArray;
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00017AF0 File Offset: 0x00015CF0
		public unsafe void Clear()
		{
			NativeQueueBlockHeader* nextBlock;
			for (NativeQueueBlockHeader* ptr = (NativeQueueBlockHeader*)((void*)this.m_Buffer->m_FirstBlock); ptr != null; ptr = nextBlock)
			{
				nextBlock = ptr->m_NextBlock;
				this.m_QueuePool->FreeBlock(ptr);
			}
			this.m_Buffer->m_FirstBlock = IntPtr.Zero;
			this.m_Buffer->m_LastBlock = IntPtr.Zero;
			this.m_Buffer->m_CurrentRead = 0;
			for (int i = 0; i < 128; i++)
			{
				this.m_Buffer->SetCurrentWriteBlockTLS(i, null);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x00017B72 File Offset: 0x00015D72
		public bool IsCreated
		{
			get
			{
				return this.m_Buffer != null;
			}
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00017B81 File Offset: 0x00015D81
		public void Dispose()
		{
			NativeQueueData.DeallocateQueue(this.m_Buffer, this.m_QueuePool, this.m_AllocatorLabel);
			this.m_Buffer = null;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00017BA4 File Offset: 0x00015DA4
		[NotBurstCompatible]
		public JobHandle Dispose(JobHandle inputDeps)
		{
			JobHandle result = new NativeQueueDisposeJob
			{
				Data = new NativeQueueDispose
				{
					m_Buffer = this.m_Buffer,
					m_QueuePool = this.m_QueuePool,
					m_AllocatorLabel = this.m_AllocatorLabel
				}
			}.Schedule(inputDeps);
			this.m_Buffer = null;
			return result;
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00017C00 File Offset: 0x00015E00
		public NativeQueue<T>.ParallelWriter AsParallelWriter()
		{
			NativeQueue<T>.ParallelWriter result;
			result.m_Buffer = this.m_Buffer;
			result.m_QueuePool = this.m_QueuePool;
			result.m_ThreadIndex = 0;
			return result;
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckRead()
		{
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00017C30 File Offset: 0x00015E30
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private unsafe void CheckReadNotEmpty()
		{
			this.m_Buffer->m_FirstBlock == (IntPtr)0;
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00002C2B File Offset: 0x00000E2B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckWrite()
		{
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00017C49 File Offset: 0x00015E49
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void ThrowEmpty()
		{
			throw new InvalidOperationException("Trying to read from an empty queue.");
		}

		// Token: 0x040002A8 RID: 680
		[NativeDisableUnsafePtrRestriction]
		private unsafe NativeQueueData* m_Buffer;

		// Token: 0x040002A9 RID: 681
		[NativeDisableUnsafePtrRestriction]
		private unsafe NativeQueueBlockPoolData* m_QueuePool;

		// Token: 0x040002AA RID: 682
		private AllocatorManager.AllocatorHandle m_AllocatorLabel;

		// Token: 0x020000CE RID: 206
		[NativeContainer]
		[NativeContainerIsAtomicWriteOnly]
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public struct ParallelWriter
		{
			// Token: 0x060007BB RID: 1979 RVA: 0x00017C58 File Offset: 0x00015E58
			public unsafe void Enqueue(T value)
			{
				NativeQueueBlockHeader* ptr = NativeQueueData.AllocateWriteBlockMT<T>(this.m_Buffer, this.m_QueuePool, this.m_ThreadIndex);
				UnsafeUtility.WriteArrayElement<T>((void*)(ptr + 1), ptr->m_NumItems, value);
				ptr->m_NumItems++;
			}

			// Token: 0x040002AB RID: 683
			[NativeDisableUnsafePtrRestriction]
			internal unsafe NativeQueueData* m_Buffer;

			// Token: 0x040002AC RID: 684
			[NativeDisableUnsafePtrRestriction]
			internal unsafe NativeQueueBlockPoolData* m_QueuePool;

			// Token: 0x040002AD RID: 685
			[NativeSetThreadIndex]
			internal int m_ThreadIndex;
		}
	}
}
