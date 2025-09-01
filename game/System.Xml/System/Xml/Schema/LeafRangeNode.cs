using System;

namespace System.Xml.Schema
{
	// Token: 0x02000502 RID: 1282
	internal sealed class LeafRangeNode : LeafNode
	{
		// Token: 0x06003437 RID: 13367 RVA: 0x00127C7A File Offset: 0x00125E7A
		public LeafRangeNode(decimal min, decimal max) : this(-1, min, max)
		{
		}

		// Token: 0x06003438 RID: 13368 RVA: 0x00127C85 File Offset: 0x00125E85
		public LeafRangeNode(int pos, decimal min, decimal max) : base(pos)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06003439 RID: 13369 RVA: 0x00127C9C File Offset: 0x00125E9C
		public decimal Max
		{
			get
			{
				return this.max;
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x0600343A RID: 13370 RVA: 0x00127CA4 File Offset: 0x00125EA4
		public decimal Min
		{
			get
			{
				return this.min;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x0600343B RID: 13371 RVA: 0x00127CAC File Offset: 0x00125EAC
		// (set) Token: 0x0600343C RID: 13372 RVA: 0x00127CB4 File Offset: 0x00125EB4
		public BitSet NextIteration
		{
			get
			{
				return this.nextIteration;
			}
			set
			{
				this.nextIteration = value;
			}
		}

		// Token: 0x0600343D RID: 13373 RVA: 0x00127CBD File Offset: 0x00125EBD
		public override SyntaxTreeNode Clone(Positions positions)
		{
			return new LeafRangeNode(base.Pos, this.min, this.max);
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x0600343E RID: 13374 RVA: 0x0001222F File Offset: 0x0001042F
		public override bool IsRangeNode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600343F RID: 13375 RVA: 0x00127CD6 File Offset: 0x00125ED6
		public override void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
			if (parent.LeftChild.IsNullable)
			{
				this.min = 0m;
			}
		}

		// Token: 0x040026DE RID: 9950
		private decimal min;

		// Token: 0x040026DF RID: 9951
		private decimal max;

		// Token: 0x040026E0 RID: 9952
		private BitSet nextIteration;
	}
}
