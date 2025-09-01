using System;

namespace System.Xml.Schema
{
	// Token: 0x020004F9 RID: 1273
	internal class LeafNode : SyntaxTreeNode
	{
		// Token: 0x0600340F RID: 13327 RVA: 0x001276AC File Offset: 0x001258AC
		public LeafNode(int pos)
		{
			this.pos = pos;
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06003410 RID: 13328 RVA: 0x001276BB File Offset: 0x001258BB
		// (set) Token: 0x06003411 RID: 13329 RVA: 0x001276C3 File Offset: 0x001258C3
		public int Pos
		{
			get
			{
				return this.pos;
			}
			set
			{
				this.pos = value;
			}
		}

		// Token: 0x06003412 RID: 13330 RVA: 0x0000B528 File Offset: 0x00009728
		public override void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
		}

		// Token: 0x06003413 RID: 13331 RVA: 0x001276CC File Offset: 0x001258CC
		public override SyntaxTreeNode Clone(Positions positions)
		{
			return new LeafNode(positions.Add(positions[this.pos].symbol, positions[this.pos].particle));
		}

		// Token: 0x06003414 RID: 13332 RVA: 0x001276FB File Offset: 0x001258FB
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			firstpos.Set(this.pos);
			lastpos.Set(this.pos);
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06003415 RID: 13333 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override bool IsNullable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040026D4 RID: 9940
		private int pos;
	}
}
