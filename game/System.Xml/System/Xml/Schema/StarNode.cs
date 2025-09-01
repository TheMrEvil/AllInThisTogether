using System;

namespace System.Xml.Schema
{
	// Token: 0x02000501 RID: 1281
	internal sealed class StarNode : InteriorNode
	{
		// Token: 0x06003434 RID: 13364 RVA: 0x00127C40 File Offset: 0x00125E40
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			base.LeftChild.ConstructPos(firstpos, lastpos, followpos);
			for (int num = lastpos.NextSet(-1); num != -1; num = lastpos.NextSet(num))
			{
				followpos[num].Or(firstpos);
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06003435 RID: 13365 RVA: 0x0001222F File Offset: 0x0001042F
		public override bool IsNullable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003436 RID: 13366 RVA: 0x00127AE4 File Offset: 0x00125CE4
		public StarNode()
		{
		}
	}
}
