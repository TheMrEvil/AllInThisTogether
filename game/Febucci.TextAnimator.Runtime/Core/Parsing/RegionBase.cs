using System;
using UnityEngine;

namespace Febucci.UI.Core.Parsing
{
	// Token: 0x0200004D RID: 77
	public abstract class RegionBase
	{
		// Token: 0x0600018C RID: 396 RVA: 0x00007727 File Offset: 0x00005927
		public RegionBase(string tagId)
		{
			this.tagId = tagId;
			this.ranges = Array.Empty<TagRange>();
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007741 File Offset: 0x00005941
		public RegionBase(string tagId, params TagRange[] ranges)
		{
			this.tagId = tagId;
			this.ranges = ranges;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00007758 File Offset: 0x00005958
		public RegionBase(string tagId, params Vector2Int[] ranges)
		{
			this.tagId = tagId;
			int length = tagId.Length;
			this.ranges = new TagRange[ranges.Length];
			for (int i = 0; i < this.ranges.Length; i++)
			{
				this.ranges[i] = new TagRange(ranges[i], Array.Empty<ModifierInfo>());
			}
		}

		// Token: 0x04000113 RID: 275
		public readonly string tagId;

		// Token: 0x04000114 RID: 276
		public TagRange[] ranges;
	}
}
