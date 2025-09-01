using System;

namespace System.Xml.Schema
{
	// Token: 0x020004FF RID: 1279
	internal sealed class PlusNode : InteriorNode
	{
		// Token: 0x0600342E RID: 13358 RVA: 0x00127BE8 File Offset: 0x00125DE8
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			base.LeftChild.ConstructPos(firstpos, lastpos, followpos);
			for (int num = lastpos.NextSet(-1); num != -1; num = lastpos.NextSet(num))
			{
				followpos[num].Or(firstpos);
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x0600342F RID: 13359 RVA: 0x00127C22 File Offset: 0x00125E22
		public override bool IsNullable
		{
			get
			{
				return base.LeftChild.IsNullable;
			}
		}

		// Token: 0x06003430 RID: 13360 RVA: 0x00127AE4 File Offset: 0x00125CE4
		public PlusNode()
		{
		}
	}
}
