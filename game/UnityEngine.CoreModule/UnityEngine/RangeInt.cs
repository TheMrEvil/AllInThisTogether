using System;

namespace UnityEngine
{
	// Token: 0x02000211 RID: 529
	public struct RangeInt
	{
		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001753 RID: 5971 RVA: 0x00025864 File Offset: 0x00023A64
		public int end
		{
			get
			{
				return this.start + this.length;
			}
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x00025883 File Offset: 0x00023A83
		public RangeInt(int start, int length)
		{
			this.start = start;
			this.length = length;
		}

		// Token: 0x040007FC RID: 2044
		public int start;

		// Token: 0x040007FD RID: 2045
		public int length;
	}
}
