using System;
using System.Threading;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000CC RID: 204
	[BurstCompatible]
	internal struct NativeQueueData
	{
		// Token: 0x060007A2 RID: 1954 RVA: 0x00017674 File Offset: 0x00015874
		internal unsafe NativeQueueBlockHeader* GetCurrentWriteBlockTLS(int threadIndex)
		{
			NativeQueueBlockHeader** ptr = (NativeQueueBlockHeader**)(this.m_CurrentWriteBlockTLS + threadIndex * 64);
			return *(IntPtr*)ptr;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00017690 File Offset: 0x00015890
		internal unsafe void SetCurrentWriteBlockTLS(int threadIndex, NativeQueueBlockHeader* currentWriteBlock)
		{
			NativeQueueBlockHeader** ptr = (NativeQueueBlockHeader**)(this.m_CurrentWriteBlockTLS + threadIndex * 64);
			*(IntPtr*)ptr = currentWriteBlock;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x000176B0 File Offset: 0x000158B0
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public unsafe static NativeQueueBlockHeader* AllocateWriteBlockMT<T>(NativeQueueData* data, NativeQueueBlockPoolData* pool, int threadIndex) where T : struct
		{
			NativeQueueBlockHeader* ptr = data->GetCurrentWriteBlockTLS(threadIndex);
			if (ptr != null && ptr->m_NumItems == data->m_MaxItems)
			{
				ptr = null;
			}
			if (ptr == null)
			{
				ptr = pool->AllocateBlock();
				ptr->m_NextBlock = null;
				ptr->m_NumItems = 0;
				NativeQueueBlockHeader* ptr2 = (NativeQueueBlockHeader*)((void*)Interlocked.Exchange(ref data->m_LastBlock, (IntPtr)((void*)ptr)));
				if (ptr2 == null)
				{
					data->m_FirstBlock = (IntPtr)((void*)ptr);
				}
				else
				{
					ptr2->m_NextBlock = ptr;
				}
				data->SetCurrentWriteBlockTLS(threadIndex, ptr);
			}
			return ptr;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00017730 File Offset: 0x00015930
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public unsafe static void AllocateQueue<T>(AllocatorManager.AllocatorHandle label, out NativeQueueData* outBuf) where T : struct
		{
			int num = CollectionHelper.Align(UnsafeUtility.SizeOf<NativeQueueData>(), 64);
			NativeQueueData* ptr = (NativeQueueData*)Memory.Unmanaged.Allocate((long)(num + 8192), 64, label);
			ptr->m_CurrentWriteBlockTLS = (byte*)(ptr + num / sizeof(NativeQueueData));
			ptr->m_FirstBlock = IntPtr.Zero;
			ptr->m_LastBlock = IntPtr.Zero;
			ptr->m_MaxItems = (16384 - UnsafeUtility.SizeOf<NativeQueueBlockHeader>()) / UnsafeUtility.SizeOf<T>();
			ptr->m_CurrentRead = 0;
			for (int i = 0; i < 128; i++)
			{
				ptr->SetCurrentWriteBlockTLS(i, null);
			}
			outBuf = ptr;
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x000177B4 File Offset: 0x000159B4
		public unsafe static void DeallocateQueue(NativeQueueData* data, NativeQueueBlockPoolData* pool, AllocatorManager.AllocatorHandle allocation)
		{
			NativeQueueBlockHeader* nextBlock;
			for (NativeQueueBlockHeader* ptr = (NativeQueueBlockHeader*)((void*)data->m_FirstBlock); ptr != null; ptr = nextBlock)
			{
				nextBlock = ptr->m_NextBlock;
				pool->FreeBlock(ptr);
			}
			Memory.Unmanaged.Free<NativeQueueData>(data, allocation);
		}

		// Token: 0x040002A3 RID: 675
		public IntPtr m_FirstBlock;

		// Token: 0x040002A4 RID: 676
		public IntPtr m_LastBlock;

		// Token: 0x040002A5 RID: 677
		public int m_MaxItems;

		// Token: 0x040002A6 RID: 678
		public int m_CurrentRead;

		// Token: 0x040002A7 RID: 679
		public unsafe byte* m_CurrentWriteBlockTLS;
	}
}
