using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000341 RID: 833
	internal class BestFitAllocator
	{
		// Token: 0x06001ADE RID: 6878 RVA: 0x00076E58 File Offset: 0x00075058
		public BestFitAllocator(uint size)
		{
			this.totalSize = size;
			this.m_FirstBlock = (this.m_FirstAvailableBlock = this.m_BlockPool.Get());
			this.m_FirstAvailableBlock.end = size;
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001ADF RID: 6879 RVA: 0x00076EA5 File Offset: 0x000750A5
		public uint totalSize
		{
			[CompilerGenerated]
			get
			{
				return this.<totalSize>k__BackingField;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001AE0 RID: 6880 RVA: 0x00076EB0 File Offset: 0x000750B0
		public uint highWatermark
		{
			get
			{
				return this.m_HighWatermark;
			}
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x00076EC8 File Offset: 0x000750C8
		public Alloc Allocate(uint size)
		{
			BestFitAllocator.Block block = this.BestFitFindAvailableBlock(size);
			bool flag = block == null;
			Alloc result;
			if (flag)
			{
				result = default(Alloc);
			}
			else
			{
				Debug.Assert(block.size >= size);
				Debug.Assert(!block.allocated);
				bool flag2 = size != block.size;
				if (flag2)
				{
					this.SplitBlock(block, size);
				}
				Debug.Assert(block.size == size);
				bool flag3 = block.end > this.m_HighWatermark;
				if (flag3)
				{
					this.m_HighWatermark = block.end;
				}
				bool flag4 = block == this.m_FirstAvailableBlock;
				if (flag4)
				{
					this.m_FirstAvailableBlock = this.m_FirstAvailableBlock.nextAvailable;
				}
				bool flag5 = block.prevAvailable != null;
				if (flag5)
				{
					block.prevAvailable.nextAvailable = block.nextAvailable;
				}
				bool flag6 = block.nextAvailable != null;
				if (flag6)
				{
					block.nextAvailable.prevAvailable = block.prevAvailable;
				}
				block.allocated = true;
				block.prevAvailable = (block.nextAvailable = null);
				result = new Alloc
				{
					start = block.start,
					size = block.size,
					handle = block
				};
			}
			return result;
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x00077008 File Offset: 0x00075208
		public void Free(Alloc alloc)
		{
			BestFitAllocator.Block block = (BestFitAllocator.Block)alloc.handle;
			bool flag = !block.allocated;
			if (flag)
			{
				Debug.Assert(false, "Severe error: UIR allocation double-free");
			}
			else
			{
				Debug.Assert(block.allocated);
				Debug.Assert(block.start == alloc.start);
				Debug.Assert(block.size == alloc.size);
				bool flag2 = block.end == this.m_HighWatermark;
				if (flag2)
				{
					bool flag3 = block.prev != null;
					if (flag3)
					{
						this.m_HighWatermark = (block.prev.allocated ? block.prev.end : block.prev.start);
					}
					else
					{
						this.m_HighWatermark = 0U;
					}
				}
				block.allocated = false;
				BestFitAllocator.Block block2 = this.m_FirstAvailableBlock;
				BestFitAllocator.Block block3 = null;
				while (block2 != null && block2.start < block.start)
				{
					block3 = block2;
					block2 = block2.nextAvailable;
				}
				bool flag4 = block3 == null;
				if (flag4)
				{
					Debug.Assert(block.prevAvailable == null);
					block.nextAvailable = this.m_FirstAvailableBlock;
					this.m_FirstAvailableBlock = block;
				}
				else
				{
					block.prevAvailable = block3;
					block.nextAvailable = block3.nextAvailable;
					block3.nextAvailable = block;
				}
				bool flag5 = block.nextAvailable != null;
				if (flag5)
				{
					block.nextAvailable.prevAvailable = block;
				}
				bool flag6 = block.prevAvailable == block.prev && block.prev != null;
				if (flag6)
				{
					block = this.CoalesceBlockWithPrevious(block);
				}
				bool flag7 = block.nextAvailable == block.next && block.next != null;
				if (flag7)
				{
					block = this.CoalesceBlockWithPrevious(block.next);
				}
			}
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x000771C4 File Offset: 0x000753C4
		private BestFitAllocator.Block CoalesceBlockWithPrevious(BestFitAllocator.Block block)
		{
			Debug.Assert(block.prevAvailable.end == block.start);
			Debug.Assert(block.prev.nextAvailable == block);
			BestFitAllocator.Block prev = block.prev;
			prev.next = block.next;
			bool flag = block.next != null;
			if (flag)
			{
				block.next.prev = prev;
			}
			prev.nextAvailable = block.nextAvailable;
			bool flag2 = block.nextAvailable != null;
			if (flag2)
			{
				block.nextAvailable.prevAvailable = block.prevAvailable;
			}
			prev.end = block.end;
			this.m_BlockPool.Return(block);
			return prev;
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x00077274 File Offset: 0x00075474
		internal HeapStatistics GatherStatistics()
		{
			HeapStatistics heapStatistics = default(HeapStatistics);
			for (BestFitAllocator.Block block = this.m_FirstBlock; block != null; block = block.next)
			{
				bool allocated = block.allocated;
				if (allocated)
				{
					heapStatistics.numAllocs += 1U;
					heapStatistics.allocatedSize += block.size;
				}
				else
				{
					heapStatistics.freeSize += block.size;
					heapStatistics.availableBlocksCount += 1U;
					heapStatistics.largestAvailableBlock = Math.Max(heapStatistics.largestAvailableBlock, block.size);
				}
				heapStatistics.blockCount += 1U;
			}
			heapStatistics.totalSize = this.totalSize;
			heapStatistics.highWatermark = this.m_HighWatermark;
			bool flag = heapStatistics.freeSize > 0U;
			if (flag)
			{
				heapStatistics.fragmentation = (float)((heapStatistics.freeSize - heapStatistics.largestAvailableBlock) / heapStatistics.freeSize) * 100f;
			}
			return heapStatistics;
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x00077368 File Offset: 0x00075568
		private BestFitAllocator.Block BestFitFindAvailableBlock(uint size)
		{
			BestFitAllocator.Block block = this.m_FirstAvailableBlock;
			BestFitAllocator.Block result = null;
			uint num = uint.MaxValue;
			while (block != null)
			{
				bool flag = block.size >= size && num > block.size;
				if (flag)
				{
					result = block;
					num = block.size;
				}
				block = block.nextAvailable;
			}
			return result;
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x000773C4 File Offset: 0x000755C4
		private void SplitBlock(BestFitAllocator.Block block, uint size)
		{
			Debug.Assert(block.size > size);
			BestFitAllocator.Block block2 = this.m_BlockPool.Get();
			block2.next = block.next;
			block2.nextAvailable = block.nextAvailable;
			block2.prev = block;
			block2.prevAvailable = block;
			block2.start = block.start + size;
			block2.end = block.end;
			bool flag = block2.next != null;
			if (flag)
			{
				block2.next.prev = block2;
			}
			bool flag2 = block2.nextAvailable != null;
			if (flag2)
			{
				block2.nextAvailable.prevAvailable = block2;
			}
			block.next = block2;
			block.nextAvailable = block2;
			block.end = block2.start;
		}

		// Token: 0x04000CC8 RID: 3272
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly uint <totalSize>k__BackingField;

		// Token: 0x04000CC9 RID: 3273
		private BestFitAllocator.Block m_FirstBlock;

		// Token: 0x04000CCA RID: 3274
		private BestFitAllocator.Block m_FirstAvailableBlock;

		// Token: 0x04000CCB RID: 3275
		private BestFitAllocator.BlockPool m_BlockPool = new BestFitAllocator.BlockPool();

		// Token: 0x04000CCC RID: 3276
		private uint m_HighWatermark;

		// Token: 0x02000342 RID: 834
		private class BlockPool : LinkedPool<BestFitAllocator.Block>
		{
			// Token: 0x06001AE7 RID: 6887 RVA: 0x0007747C File Offset: 0x0007567C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static BestFitAllocator.Block CreateBlock()
			{
				return new BestFitAllocator.Block();
			}

			// Token: 0x06001AE8 RID: 6888 RVA: 0x00002166 File Offset: 0x00000366
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static void ResetBlock(BestFitAllocator.Block block)
			{
			}

			// Token: 0x06001AE9 RID: 6889 RVA: 0x00077493 File Offset: 0x00075693
			public BlockPool() : base(new Func<BestFitAllocator.Block>(BestFitAllocator.BlockPool.CreateBlock), new Action<BestFitAllocator.Block>(BestFitAllocator.BlockPool.ResetBlock), 10000)
			{
			}
		}

		// Token: 0x02000343 RID: 835
		private class Block : LinkedPoolItem<BestFitAllocator.Block>
		{
			// Token: 0x1700066A RID: 1642
			// (get) Token: 0x06001AEA RID: 6890 RVA: 0x000774BC File Offset: 0x000756BC
			public uint size
			{
				get
				{
					return this.end - this.start;
				}
			}

			// Token: 0x06001AEB RID: 6891 RVA: 0x000774DB File Offset: 0x000756DB
			public Block()
			{
			}

			// Token: 0x04000CCD RID: 3277
			public uint start;

			// Token: 0x04000CCE RID: 3278
			public uint end;

			// Token: 0x04000CCF RID: 3279
			public BestFitAllocator.Block prev;

			// Token: 0x04000CD0 RID: 3280
			public BestFitAllocator.Block next;

			// Token: 0x04000CD1 RID: 3281
			public BestFitAllocator.Block prevAvailable;

			// Token: 0x04000CD2 RID: 3282
			public BestFitAllocator.Block nextAvailable;

			// Token: 0x04000CD3 RID: 3283
			public bool allocated;
		}
	}
}
