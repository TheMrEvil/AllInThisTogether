using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x0200015B RID: 347
	public class CloneContext
	{
		// Token: 0x06001153 RID: 4435 RVA: 0x00047A8B File Offset: 0x00045C8B
		public void AddBlockMap(Block from, Block to)
		{
			this.block_map.Add(from, to);
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x00047A9C File Offset: 0x00045C9C
		public Block LookupBlock(Block from)
		{
			Block result;
			if (!this.block_map.TryGetValue(from, out result))
			{
				result = (Block)from.Clone(this);
			}
			return result;
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00047AC8 File Offset: 0x00045CC8
		public Block RemapBlockCopy(Block from)
		{
			Block result;
			if (!this.block_map.TryGetValue(from, out result))
			{
				return from;
			}
			return result;
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x00047AE8 File Offset: 0x00045CE8
		public CloneContext()
		{
		}

		// Token: 0x04000760 RID: 1888
		private Dictionary<Block, Block> block_map = new Dictionary<Block, Block>();
	}
}
