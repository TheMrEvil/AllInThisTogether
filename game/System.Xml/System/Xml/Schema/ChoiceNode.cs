using System;

namespace System.Xml.Schema
{
	// Token: 0x020004FE RID: 1278
	internal sealed class ChoiceNode : InteriorNode
	{
		// Token: 0x06003429 RID: 13353 RVA: 0x00127B14 File Offset: 0x00125D14
		private static void ConstructChildPos(SyntaxTreeNode child, BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			BitSet bitSet = new BitSet(firstpos.Count);
			BitSet bitSet2 = new BitSet(lastpos.Count);
			child.ConstructPos(bitSet, bitSet2, followpos);
			firstpos.Or(bitSet);
			lastpos.Or(bitSet2);
		}

		// Token: 0x0600342A RID: 13354 RVA: 0x00127B50 File Offset: 0x00125D50
		public override void ConstructPos(BitSet firstpos, BitSet lastpos, BitSet[] followpos)
		{
			BitSet bitSet = new BitSet(firstpos.Count);
			BitSet bitSet2 = new BitSet(lastpos.Count);
			ChoiceNode choiceNode = this;
			SyntaxTreeNode leftChild;
			do
			{
				ChoiceNode.ConstructChildPos(choiceNode.RightChild, bitSet, bitSet2, followpos);
				leftChild = choiceNode.LeftChild;
				choiceNode = (leftChild as ChoiceNode);
			}
			while (choiceNode != null);
			leftChild.ConstructPos(firstpos, lastpos, followpos);
			firstpos.Or(bitSet);
			lastpos.Or(bitSet2);
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x0600342B RID: 13355 RVA: 0x00127BB0 File Offset: 0x00125DB0
		public override bool IsNullable
		{
			get
			{
				ChoiceNode choiceNode = this;
				while (!choiceNode.RightChild.IsNullable)
				{
					SyntaxTreeNode leftChild = choiceNode.LeftChild;
					choiceNode = (leftChild as ChoiceNode);
					if (choiceNode == null)
					{
						return leftChild.IsNullable;
					}
				}
				return true;
			}
		}

		// Token: 0x0600342C RID: 13356 RVA: 0x00127AD9 File Offset: 0x00125CD9
		public override void ExpandTree(InteriorNode parent, SymbolsDictionary symbols, Positions positions)
		{
			base.ExpandTreeNoRecursive(parent, symbols, positions);
		}

		// Token: 0x0600342D RID: 13357 RVA: 0x00127AE4 File Offset: 0x00125CE4
		public ChoiceNode()
		{
		}
	}
}
