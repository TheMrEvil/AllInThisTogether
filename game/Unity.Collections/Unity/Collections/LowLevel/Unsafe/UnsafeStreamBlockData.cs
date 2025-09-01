using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200014D RID: 333
	[BurstCompatible]
	internal struct UnsafeStreamBlockData
	{
		// Token: 0x06000BE5 RID: 3045 RVA: 0x000237C0 File Offset: 0x000219C0
		internal unsafe UnsafeStreamBlock* Allocate(UnsafeStreamBlock* oldBlock, int threadIndex)
		{
			UnsafeStreamBlock* ptr = (UnsafeStreamBlock*)Memory.Unmanaged.Allocate(4096L, 16, this.Allocator);
			ptr->Next = null;
			if (oldBlock == null)
			{
				ptr->Next = *(IntPtr*)(this.Blocks + (IntPtr)threadIndex * (IntPtr)sizeof(UnsafeStreamBlock*) / (IntPtr)sizeof(UnsafeStreamBlock*));
				*(IntPtr*)(this.Blocks + (IntPtr)threadIndex * (IntPtr)sizeof(UnsafeStreamBlock*) / (IntPtr)sizeof(UnsafeStreamBlock*)) = ptr;
			}
			else
			{
				oldBlock->Next = ptr;
			}
			return ptr;
		}

		// Token: 0x040003DD RID: 989
		internal const int AllocationSize = 4096;

		// Token: 0x040003DE RID: 990
		internal AllocatorManager.AllocatorHandle Allocator;

		// Token: 0x040003DF RID: 991
		internal unsafe UnsafeStreamBlock** Blocks;

		// Token: 0x040003E0 RID: 992
		internal int BlockCount;

		// Token: 0x040003E1 RID: 993
		internal unsafe UnsafeStreamBlock* Free;

		// Token: 0x040003E2 RID: 994
		internal unsafe UnsafeStreamRange* Ranges;

		// Token: 0x040003E3 RID: 995
		internal int RangeCount;
	}
}
