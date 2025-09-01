using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020004FA RID: 1274
	internal class NamespaceListNode : SyntaxTreeNode
	{
		// Token: 0x06003416 RID: 13334 RVA: 0x00127715 File Offset: 0x00125915
		public NamespaceListNode(NamespaceList namespaceList, object particle)
		{
			this.namespaceList = namespaceList;
			this.particle = particle;
		}

		// Token: 0x06003417 RID: 13335 RVA: 0x0005BE99 File Offset: 0x0005A099
		public override SyntaxTreeNode Clone(Positions positions)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x0012772B File Offset: 0x0012592B
		public virtual ICollection GetResolvedSymbols(SymbolsDictionary symbols)
		{
			return symbols.GetNamespaceListSymbols(this.namespaceList);
		}

		// Token: 0x06003419 RID: 13337 RVA: 0x0012773C File Offset: 0x0012593C
		public override void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
			SyntaxTreeNode syntaxTreeNode = null;
			foreach (object obj in this.GetResolvedSymbols(symbols))
			{
				int symbol = (int)obj;
				if (symbols.GetParticle(symbol) != this.particle)
				{
					symbols.IsUpaEnforced = false;
				}
				LeafNode leafNode = new LeafNode(positions.Add(symbol, this.particle));
				if (syntaxTreeNode == null)
				{
					syntaxTreeNode = leafNode;
				}
				else
				{
					syntaxTreeNode = new ChoiceNode
					{
						LeftChild = syntaxTreeNode,
						RightChild = leafNode
					};
				}
			}
			if (parent.LeftChild == this)
			{
				parent.LeftChild = syntaxTreeNode;
				return;
			}
			parent.RightChild = syntaxTreeNode;
		}

		// Token: 0x0600341A RID: 13338 RVA: 0x0005BE99 File Offset: 0x0005A099
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x0600341B RID: 13339 RVA: 0x0005BE99 File Offset: 0x0005A099
		public override bool IsNullable
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x040026D5 RID: 9941
		protected NamespaceList namespaceList;

		// Token: 0x040026D6 RID: 9942
		protected object particle;
	}
}
