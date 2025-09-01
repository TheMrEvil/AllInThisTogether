using System;

namespace System.Xml.Schema
{
	// Token: 0x020004F8 RID: 1272
	internal abstract class SyntaxTreeNode
	{
		// Token: 0x06003409 RID: 13321
		public abstract void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions);

		// Token: 0x0600340A RID: 13322
		public abstract SyntaxTreeNode Clone(Positions positions);

		// Token: 0x0600340B RID: 13323
		public abstract void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos);

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x0600340C RID: 13324
		public abstract bool IsNullable { get; }

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x0600340D RID: 13325 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public virtual bool IsRangeNode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600340E RID: 13326 RVA: 0x0000216B File Offset: 0x0000036B
		protected SyntaxTreeNode()
		{
		}
	}
}
