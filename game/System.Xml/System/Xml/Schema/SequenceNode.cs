using System;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	// Token: 0x020004FC RID: 1276
	internal sealed class SequenceNode : InteriorNode
	{
		// Token: 0x06003424 RID: 13348 RVA: 0x00127904 File Offset: 0x00125B04
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			Stack<SequenceNode.SequenceConstructPosContext> stack = new Stack<SequenceNode.SequenceConstructPosContext>();
			SequenceNode.SequenceConstructPosContext sequenceConstructPosContext = new SequenceNode.SequenceConstructPosContext(this, firstpos, lastpos);
			SequenceNode this_;
			for (;;)
			{
				this_ = sequenceConstructPosContext.this_;
				sequenceConstructPosContext.lastposLeft = new BitSet(lastpos.Count);
				if (!(this_.LeftChild is SequenceNode))
				{
					break;
				}
				stack.Push(sequenceConstructPosContext);
				sequenceConstructPosContext = new SequenceNode.SequenceConstructPosContext((SequenceNode)this_.LeftChild, sequenceConstructPosContext.firstpos, sequenceConstructPosContext.lastposLeft);
			}
			this_.LeftChild.ConstructPos(sequenceConstructPosContext.firstpos, sequenceConstructPosContext.lastposLeft, followpos);
			for (;;)
			{
				sequenceConstructPosContext.firstposRight = new BitSet(firstpos.Count);
				this_.RightChild.ConstructPos(sequenceConstructPosContext.firstposRight, sequenceConstructPosContext.lastpos, followpos);
				if (this_.LeftChild.IsNullable && !this_.RightChild.IsRangeNode)
				{
					sequenceConstructPosContext.firstpos.Or(sequenceConstructPosContext.firstposRight);
				}
				if (this_.RightChild.IsNullable)
				{
					sequenceConstructPosContext.lastpos.Or(sequenceConstructPosContext.lastposLeft);
				}
				for (int num = sequenceConstructPosContext.lastposLeft.NextSet(-1); num != -1; num = sequenceConstructPosContext.lastposLeft.NextSet(num))
				{
					followpos[num].Or(sequenceConstructPosContext.firstposRight);
				}
				if (this_.RightChild.IsRangeNode)
				{
					((LeafRangeNode)this_.RightChild).NextIteration = sequenceConstructPosContext.firstpos.Clone();
				}
				if (stack.Count == 0)
				{
					break;
				}
				sequenceConstructPosContext = stack.Pop();
				this_ = sequenceConstructPosContext.this_;
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06003425 RID: 13349 RVA: 0x00127A6C File Offset: 0x00125C6C
		public override bool IsNullable
		{
			get
			{
				SequenceNode sequenceNode = this;
				while (!sequenceNode.RightChild.IsRangeNode || !(((LeafRangeNode)sequenceNode.RightChild).Min == 0m))
				{
					if (!sequenceNode.RightChild.IsNullable && !sequenceNode.RightChild.IsRangeNode)
					{
						return false;
					}
					SyntaxTreeNode leftChild = sequenceNode.LeftChild;
					sequenceNode = (leftChild as SequenceNode);
					if (sequenceNode == null)
					{
						return leftChild.IsNullable;
					}
				}
				return true;
			}
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x00127AD9 File Offset: 0x00125CD9
		public override void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
			base.ExpandTreeNoRecursive(parent, symbols, positions);
		}

		// Token: 0x06003427 RID: 13351 RVA: 0x00127AE4 File Offset: 0x00125CE4
		public SequenceNode()
		{
		}

		// Token: 0x020004FD RID: 1277
		private struct SequenceConstructPosContext
		{
			// Token: 0x06003428 RID: 13352 RVA: 0x00127AEC File Offset: 0x00125CEC
			public SequenceConstructPosContext(SequenceNode node, BitSet firstpos, BitSet lastpos)
			{
				this.this_ = node;
				this.firstpos = firstpos;
				this.lastpos = lastpos;
				this.lastposLeft = null;
				this.firstposRight = null;
			}

			// Token: 0x040026D9 RID: 9945
			public SequenceNode this_;

			// Token: 0x040026DA RID: 9946
			public BitSet firstpos;

			// Token: 0x040026DB RID: 9947
			public BitSet lastpos;

			// Token: 0x040026DC RID: 9948
			public BitSet lastposLeft;

			// Token: 0x040026DD RID: 9949
			public BitSet firstposRight;
		}
	}
}
