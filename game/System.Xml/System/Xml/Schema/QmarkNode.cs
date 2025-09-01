using System;

namespace System.Xml.Schema
{
	// Token: 0x02000500 RID: 1280
	internal sealed class QmarkNode : InteriorNode
	{
		// Token: 0x06003431 RID: 13361 RVA: 0x00127C2F File Offset: 0x00125E2F
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			base.LeftChild.ConstructPos(firstpos, lastpos, followpos);
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06003432 RID: 13362 RVA: 0x0001222F File Offset: 0x0001042F
		public override bool IsNullable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003433 RID: 13363 RVA: 0x00127AE4 File Offset: 0x00125CE4
		public QmarkNode()
		{
		}
	}
}
