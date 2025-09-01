using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000344 RID: 836
	internal class GPUBufferAllocator
	{
		// Token: 0x06001AEC RID: 6892 RVA: 0x000774E4 File Offset: 0x000756E4
		public GPUBufferAllocator(uint maxSize)
		{
			this.m_Low = new BestFitAllocator(maxSize);
			this.m_High = new BestFitAllocator(maxSize);
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x00077508 File Offset: 0x00075708
		public Alloc Allocate(uint size, bool shortLived)
		{
			bool flag = !shortLived;
			Alloc alloc;
			if (flag)
			{
				alloc = this.m_Low.Allocate(size);
			}
			else
			{
				alloc = this.m_High.Allocate(size);
				alloc.start = this.m_High.totalSize - alloc.start - alloc.size;
			}
			alloc.shortLived = shortLived;
			bool flag2 = this.HighLowCollide() && alloc.size > 0U;
			Alloc result;
			if (flag2)
			{
				this.Free(alloc);
				result = default(Alloc);
			}
			else
			{
				result = alloc;
			}
			return result;
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x0007759C File Offset: 0x0007579C
		public void Free(Alloc alloc)
		{
			bool flag = !alloc.shortLived;
			if (flag)
			{
				this.m_Low.Free(alloc);
			}
			else
			{
				alloc.start = this.m_High.totalSize - alloc.start - alloc.size;
				this.m_High.Free(alloc);
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001AEF RID: 6895 RVA: 0x000775F8 File Offset: 0x000757F8
		public bool isEmpty
		{
			get
			{
				return this.m_Low.highWatermark == 0U && this.m_High.highWatermark == 0U;
			}
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x00077628 File Offset: 0x00075828
		public HeapStatistics GatherStatistics()
		{
			HeapStatistics heapStatistics = default(HeapStatistics);
			heapStatistics.subAllocators = new HeapStatistics[]
			{
				this.m_Low.GatherStatistics(),
				this.m_High.GatherStatistics()
			};
			heapStatistics.largestAvailableBlock = uint.MaxValue;
			for (int i = 0; i < 2; i++)
			{
				heapStatistics.numAllocs += heapStatistics.subAllocators[i].numAllocs;
				heapStatistics.totalSize = Math.Max(heapStatistics.totalSize, heapStatistics.subAllocators[i].totalSize);
				heapStatistics.allocatedSize += heapStatistics.subAllocators[i].allocatedSize;
				heapStatistics.largestAvailableBlock = Math.Min(heapStatistics.largestAvailableBlock, heapStatistics.subAllocators[i].largestAvailableBlock);
				heapStatistics.availableBlocksCount += heapStatistics.subAllocators[i].availableBlocksCount;
				heapStatistics.blockCount += heapStatistics.subAllocators[i].blockCount;
				heapStatistics.highWatermark = Math.Max(heapStatistics.highWatermark, heapStatistics.subAllocators[i].highWatermark);
				heapStatistics.fragmentation = Math.Max(heapStatistics.fragmentation, heapStatistics.subAllocators[i].fragmentation);
			}
			heapStatistics.freeSize = heapStatistics.totalSize - heapStatistics.allocatedSize;
			return heapStatistics;
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x000777A4 File Offset: 0x000759A4
		private bool HighLowCollide()
		{
			return this.m_Low.highWatermark + this.m_High.highWatermark > this.m_Low.totalSize;
		}

		// Token: 0x04000CD4 RID: 3284
		private BestFitAllocator m_Low;

		// Token: 0x04000CD5 RID: 3285
		private BestFitAllocator m_High;
	}
}
