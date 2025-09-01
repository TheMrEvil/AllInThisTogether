using System;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	// Token: 0x020004FB RID: 1275
	internal abstract class InteriorNode : SyntaxTreeNode
	{
		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x0600341C RID: 13340 RVA: 0x001277F0 File Offset: 0x001259F0
		// (set) Token: 0x0600341D RID: 13341 RVA: 0x001277F8 File Offset: 0x001259F8
		public SyntaxTreeNode LeftChild
		{
			get
			{
				return this.leftChild;
			}
			set
			{
				this.leftChild = value;
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x0600341E RID: 13342 RVA: 0x00127801 File Offset: 0x00125A01
		// (set) Token: 0x0600341F RID: 13343 RVA: 0x00127809 File Offset: 0x00125A09
		public SyntaxTreeNode RightChild
		{
			get
			{
				return this.rightChild;
			}
			set
			{
				this.rightChild = value;
			}
		}

		// Token: 0x06003420 RID: 13344 RVA: 0x00127814 File Offset: 0x00125A14
		public override SyntaxTreeNode Clone(Positions positions)
		{
			InteriorNode interiorNode = (InteriorNode)base.MemberwiseClone();
			interiorNode.LeftChild = this.leftChild.Clone(positions);
			if (this.rightChild != null)
			{
				interiorNode.RightChild = this.rightChild.Clone(positions);
			}
			return interiorNode;
		}

		// Token: 0x06003421 RID: 13345 RVA: 0x0012785C File Offset: 0x00125A5C
		protected void ExpandTreeNoRecursive(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
			Stack<InteriorNode> stack = new Stack<InteriorNode>();
			InteriorNode interiorNode = this;
			while (interiorNode.leftChild is ChoiceNode || interiorNode.leftChild is SequenceNode)
			{
				stack.Push(interiorNode);
				interiorNode = (InteriorNode)interiorNode.leftChild;
			}
			interiorNode.leftChild.ExpandTree(interiorNode, symbols, positions);
			for (;;)
			{
				if (interiorNode.rightChild != null)
				{
					interiorNode.rightChild.ExpandTree(interiorNode, symbols, positions);
				}
				if (stack.Count == 0)
				{
					break;
				}
				interiorNode = stack.Pop();
			}
		}

		// Token: 0x06003422 RID: 13346 RVA: 0x001278D5 File Offset: 0x00125AD5
		public override void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
			this.leftChild.ExpandTree(this, symbols, positions);
			if (this.rightChild != null)
			{
				this.rightChild.ExpandTree(this, symbols, positions);
			}
		}

		// Token: 0x06003423 RID: 13347 RVA: 0x001278FB File Offset: 0x00125AFB
		protected InteriorNode()
		{
		}

		// Token: 0x040026D7 RID: 9943
		private SyntaxTreeNode leftChild;

		// Token: 0x040026D8 RID: 9944
		private SyntaxTreeNode rightChild;
	}
}
