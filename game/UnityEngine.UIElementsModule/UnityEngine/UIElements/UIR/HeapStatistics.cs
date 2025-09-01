using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000340 RID: 832
	internal struct HeapStatistics
	{
		// Token: 0x04000CBE RID: 3262
		public uint numAllocs;

		// Token: 0x04000CBF RID: 3263
		public uint totalSize;

		// Token: 0x04000CC0 RID: 3264
		public uint allocatedSize;

		// Token: 0x04000CC1 RID: 3265
		public uint freeSize;

		// Token: 0x04000CC2 RID: 3266
		public uint largestAvailableBlock;

		// Token: 0x04000CC3 RID: 3267
		public uint availableBlocksCount;

		// Token: 0x04000CC4 RID: 3268
		public uint blockCount;

		// Token: 0x04000CC5 RID: 3269
		public uint highWatermark;

		// Token: 0x04000CC6 RID: 3270
		public float fragmentation;

		// Token: 0x04000CC7 RID: 3271
		public HeapStatistics[] subAllocators;
	}
}
