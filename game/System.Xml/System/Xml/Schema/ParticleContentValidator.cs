using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000504 RID: 1284
	internal sealed class ParticleContentValidator : ContentValidator
	{
		// Token: 0x0600344F RID: 13391 RVA: 0x00127E68 File Offset: 0x00126068
		public ParticleContentValidator(XmlSchemaContentType contentType) : this(contentType, true)
		{
		}

		// Token: 0x06003450 RID: 13392 RVA: 0x00127E72 File Offset: 0x00126072
		public ParticleContentValidator(XmlSchemaContentType contentType, bool enableUpaCheck) : base(contentType)
		{
			this.enableUpaCheck = enableUpaCheck;
		}

		// Token: 0x06003451 RID: 13393 RVA: 0x0005BE99 File Offset: 0x0005A099
		public override void InitValidation(ValidationState context)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06003452 RID: 13394 RVA: 0x0005BE99 File Offset: 0x0005A099
		public override object ValidateElement(XmlQualifiedName name, ValidationState context, out int errorCode)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x0005BE99 File Offset: 0x0005A099
		public override bool CompleteValidation(ValidationState context)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06003454 RID: 13396 RVA: 0x00127E82 File Offset: 0x00126082
		public void Start()
		{
			this.symbols = new SymbolsDictionary();
			this.positions = new Positions();
			this.stack = new Stack();
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x00127EA5 File Offset: 0x001260A5
		public void OpenGroup()
		{
			this.stack.Push(null);
		}

		// Token: 0x06003456 RID: 13398 RVA: 0x00127EB4 File Offset: 0x001260B4
		public void CloseGroup()
		{
			SyntaxTreeNode syntaxTreeNode = (SyntaxTreeNode)this.stack.Pop();
			if (syntaxTreeNode == null)
			{
				return;
			}
			if (this.stack.Count == 0)
			{
				this.contentNode = syntaxTreeNode;
				this.isPartial = false;
				return;
			}
			InteriorNode interiorNode = (InteriorNode)this.stack.Pop();
			if (interiorNode != null)
			{
				interiorNode.RightChild = syntaxTreeNode;
				syntaxTreeNode = interiorNode;
				this.isPartial = true;
			}
			else
			{
				this.isPartial = false;
			}
			this.stack.Push(syntaxTreeNode);
		}

		// Token: 0x06003457 RID: 13399 RVA: 0x00127F2B File Offset: 0x0012612B
		public bool Exists(XmlQualifiedName name)
		{
			return this.symbols.Exists(name);
		}

		// Token: 0x06003458 RID: 13400 RVA: 0x00127F3E File Offset: 0x0012613E
		public void AddName(XmlQualifiedName name, object particle)
		{
			this.AddLeafNode(new LeafNode(this.positions.Add(this.symbols.AddName(name, particle), particle)));
		}

		// Token: 0x06003459 RID: 13401 RVA: 0x00127F64 File Offset: 0x00126164
		public void AddNamespaceList(NamespaceList namespaceList, object particle)
		{
			this.symbols.AddNamespaceList(namespaceList, particle, false);
			this.AddLeafNode(new NamespaceListNode(namespaceList, particle));
		}

		// Token: 0x0600345A RID: 13402 RVA: 0x00127F84 File Offset: 0x00126184
		private void AddLeafNode(SyntaxTreeNode node)
		{
			if (this.stack.Count > 0)
			{
				InteriorNode interiorNode = (InteriorNode)this.stack.Pop();
				if (interiorNode != null)
				{
					interiorNode.RightChild = node;
					node = interiorNode;
				}
			}
			this.stack.Push(node);
			this.isPartial = true;
		}

		// Token: 0x0600345B RID: 13403 RVA: 0x00127FD0 File Offset: 0x001261D0
		public void AddChoice()
		{
			SyntaxTreeNode leftChild = (SyntaxTreeNode)this.stack.Pop();
			InteriorNode interiorNode = new ChoiceNode();
			interiorNode.LeftChild = leftChild;
			this.stack.Push(interiorNode);
		}

		// Token: 0x0600345C RID: 13404 RVA: 0x00128008 File Offset: 0x00126208
		public void AddSequence()
		{
			SyntaxTreeNode leftChild = (SyntaxTreeNode)this.stack.Pop();
			InteriorNode interiorNode = new SequenceNode();
			interiorNode.LeftChild = leftChild;
			this.stack.Push(interiorNode);
		}

		// Token: 0x0600345D RID: 13405 RVA: 0x0012803F File Offset: 0x0012623F
		public void AddStar()
		{
			this.Closure(new StarNode());
		}

		// Token: 0x0600345E RID: 13406 RVA: 0x0012804C File Offset: 0x0012624C
		public void AddPlus()
		{
			this.Closure(new PlusNode());
		}

		// Token: 0x0600345F RID: 13407 RVA: 0x00128059 File Offset: 0x00126259
		public void AddQMark()
		{
			this.Closure(new QmarkNode());
		}

		// Token: 0x06003460 RID: 13408 RVA: 0x00128068 File Offset: 0x00126268
		public void AddLeafRange(decimal min, decimal max)
		{
			LeafRangeNode leafRangeNode = new LeafRangeNode(min, max);
			int pos = this.positions.Add(-2, leafRangeNode);
			leafRangeNode.Pos = pos;
			this.Closure(new SequenceNode
			{
				RightChild = leafRangeNode
			});
			this.minMaxNodesCount++;
		}

		// Token: 0x06003461 RID: 13409 RVA: 0x001280B8 File Offset: 0x001262B8
		private void Closure(InteriorNode node)
		{
			if (this.stack.Count > 0)
			{
				SyntaxTreeNode syntaxTreeNode = (SyntaxTreeNode)this.stack.Pop();
				InteriorNode interiorNode = syntaxTreeNode as InteriorNode;
				if (this.isPartial && interiorNode != null)
				{
					node.LeftChild = interiorNode.RightChild;
					interiorNode.RightChild = node;
				}
				else
				{
					node.LeftChild = syntaxTreeNode;
					syntaxTreeNode = node;
				}
				this.stack.Push(syntaxTreeNode);
				return;
			}
			if (this.contentNode != null)
			{
				node.LeftChild = this.contentNode;
				this.contentNode = node;
			}
		}

		// Token: 0x06003462 RID: 13410 RVA: 0x0012813C File Offset: 0x0012633C
		public ContentValidator Finish()
		{
			return this.Finish(true);
		}

		// Token: 0x06003463 RID: 13411 RVA: 0x00128148 File Offset: 0x00126348
		public ContentValidator Finish(bool useDFA)
		{
			if (this.contentNode == null)
			{
				if (base.ContentType != XmlSchemaContentType.Mixed)
				{
					return ContentValidator.Empty;
				}
				bool isOpen = base.IsOpen;
				if (!base.IsOpen)
				{
					return ContentValidator.TextOnly;
				}
				return ContentValidator.Any;
			}
			else
			{
				InteriorNode interiorNode = new SequenceNode();
				interiorNode.LeftChild = this.contentNode;
				LeafNode leafNode = new LeafNode(this.positions.Add(this.symbols.AddName(XmlQualifiedName.Empty, null), null));
				interiorNode.RightChild = leafNode;
				this.contentNode.ExpandTree(interiorNode, this.symbols, this.positions);
				int count = this.symbols.Count;
				int count2 = this.positions.Count;
				BitSet bitSet = new BitSet(count2);
				BitSet lastpos = new BitSet(count2);
				BitSet[] array = new BitSet[count2];
				for (int i = 0; i < count2; i++)
				{
					array[i] = new BitSet(count2);
				}
				interiorNode.ConstructPos(bitSet, lastpos, array);
				if (this.minMaxNodesCount > 0)
				{
					BitSet bitSet2;
					BitSet[] minmaxFollowPos = this.CalculateTotalFollowposForRangeNodes(bitSet, array, out bitSet2);
					if (this.enableUpaCheck)
					{
						this.CheckCMUPAWithLeafRangeNodes(this.GetApplicableMinMaxFollowPos(bitSet, bitSet2, minmaxFollowPos));
						for (int j = 0; j < count2; j++)
						{
							this.CheckCMUPAWithLeafRangeNodes(this.GetApplicableMinMaxFollowPos(array[j], bitSet2, minmaxFollowPos));
						}
					}
					return new RangeContentValidator(bitSet, array, this.symbols, this.positions, leafNode.Pos, base.ContentType, interiorNode.LeftChild.IsNullable, bitSet2, this.minMaxNodesCount);
				}
				int[][] array2 = null;
				if (!this.symbols.IsUpaEnforced)
				{
					if (this.enableUpaCheck)
					{
						this.CheckUniqueParticleAttribution(bitSet, array);
					}
				}
				else if (useDFA)
				{
					array2 = this.BuildTransitionTable(bitSet, array, leafNode.Pos);
				}
				if (array2 != null)
				{
					return new DfaContentValidator(array2, this.symbols, base.ContentType, base.IsOpen, interiorNode.LeftChild.IsNullable);
				}
				return new NfaContentValidator(bitSet, array, this.symbols, this.positions, leafNode.Pos, base.ContentType, base.IsOpen, interiorNode.LeftChild.IsNullable);
			}
		}

		// Token: 0x06003464 RID: 13412 RVA: 0x0012834C File Offset: 0x0012654C
		private BitSet[] CalculateTotalFollowposForRangeNodes(BitSet firstpos, BitSet[] followpos, out BitSet posWithRangeTerminals)
		{
			int count = this.positions.Count;
			posWithRangeTerminals = new BitSet(count);
			BitSet[] array = new BitSet[this.minMaxNodesCount];
			int num = 0;
			for (int i = count - 1; i >= 0; i--)
			{
				Position position = this.positions[i];
				if (position.symbol == -2)
				{
					LeafRangeNode leafRangeNode = position.particle as LeafRangeNode;
					BitSet bitSet = new BitSet(count);
					bitSet.Clear();
					bitSet.Or(followpos[i]);
					if (leafRangeNode.Min != leafRangeNode.Max)
					{
						bitSet.Or(leafRangeNode.NextIteration);
					}
					for (int num2 = bitSet.NextSet(-1); num2 != -1; num2 = bitSet.NextSet(num2))
					{
						if (num2 > i)
						{
							Position position2 = this.positions[num2];
							if (position2.symbol == -2)
							{
								LeafRangeNode leafRangeNode2 = position2.particle as LeafRangeNode;
								bitSet.Or(array[leafRangeNode2.Pos]);
							}
						}
					}
					array[num] = bitSet;
					leafRangeNode.Pos = num++;
					posWithRangeTerminals.Set(i);
				}
			}
			return array;
		}

		// Token: 0x06003465 RID: 13413 RVA: 0x00128468 File Offset: 0x00126668
		private void CheckCMUPAWithLeafRangeNodes(BitSet curpos)
		{
			object[] array = new object[this.symbols.Count];
			for (int num = curpos.NextSet(-1); num != -1; num = curpos.NextSet(num))
			{
				Position position = this.positions[num];
				int symbol = position.symbol;
				if (symbol >= 0)
				{
					if (array[symbol] != null)
					{
						throw new UpaException(array[symbol], position.particle);
					}
					array[symbol] = position.particle;
				}
			}
		}

		// Token: 0x06003466 RID: 13414 RVA: 0x001284D4 File Offset: 0x001266D4
		private BitSet GetApplicableMinMaxFollowPos(BitSet curpos, BitSet posWithRangeTerminals, BitSet[] minmaxFollowPos)
		{
			if (curpos.Intersects(posWithRangeTerminals))
			{
				BitSet bitSet = new BitSet(this.positions.Count);
				bitSet.Or(curpos);
				bitSet.And(posWithRangeTerminals);
				curpos = curpos.Clone();
				for (int num = bitSet.NextSet(-1); num != -1; num = bitSet.NextSet(num))
				{
					LeafRangeNode leafRangeNode = this.positions[num].particle as LeafRangeNode;
					curpos.Or(minmaxFollowPos[leafRangeNode.Pos]);
				}
			}
			return curpos;
		}

		// Token: 0x06003467 RID: 13415 RVA: 0x00128550 File Offset: 0x00126750
		private void CheckUniqueParticleAttribution(BitSet firstpos, BitSet[] followpos)
		{
			this.CheckUniqueParticleAttribution(firstpos);
			for (int i = 0; i < this.positions.Count; i++)
			{
				this.CheckUniqueParticleAttribution(followpos[i]);
			}
		}

		// Token: 0x06003468 RID: 13416 RVA: 0x00128584 File Offset: 0x00126784
		private void CheckUniqueParticleAttribution(BitSet curpos)
		{
			object[] array = new object[this.symbols.Count];
			for (int num = curpos.NextSet(-1); num != -1; num = curpos.NextSet(num))
			{
				int symbol = this.positions[num].symbol;
				if (array[symbol] == null)
				{
					array[symbol] = this.positions[num].particle;
				}
				else if (array[symbol] != this.positions[num].particle)
				{
					throw new UpaException(array[symbol], this.positions[num].particle);
				}
			}
		}

		// Token: 0x06003469 RID: 13417 RVA: 0x00128618 File Offset: 0x00126818
		private int[][] BuildTransitionTable(BitSet firstpos, BitSet[] followpos, int endMarkerPos)
		{
			int count = this.positions.Count;
			int num = 8192 / count;
			int count2 = this.symbols.Count;
			ArrayList arrayList = new ArrayList();
			Hashtable hashtable = new Hashtable();
			hashtable.Add(new BitSet(count), -1);
			Queue queue = new Queue();
			int num2 = 0;
			queue.Enqueue(firstpos);
			hashtable.Add(firstpos, 0);
			arrayList.Add(new int[count2 + 1]);
			while (queue.Count > 0)
			{
				BitSet bitSet = (BitSet)queue.Dequeue();
				int[] array = (int[])arrayList[num2];
				if (bitSet[endMarkerPos])
				{
					array[count2] = 1;
				}
				for (int i = 0; i < count2; i++)
				{
					BitSet bitSet2 = new BitSet(count);
					for (int num3 = bitSet.NextSet(-1); num3 != -1; num3 = bitSet.NextSet(num3))
					{
						if (i == this.positions[num3].symbol)
						{
							bitSet2.Or(followpos[num3]);
						}
					}
					object obj = hashtable[bitSet2];
					if (obj != null)
					{
						array[i] = (int)obj;
					}
					else
					{
						int num4 = hashtable.Count - 1;
						if (num4 >= num)
						{
							return null;
						}
						queue.Enqueue(bitSet2);
						hashtable.Add(bitSet2, num4);
						arrayList.Add(new int[count2 + 1]);
						array[i] = num4;
					}
				}
				num2++;
			}
			return (int[][])arrayList.ToArray(typeof(int[]));
		}

		// Token: 0x040026E8 RID: 9960
		private SymbolsDictionary symbols;

		// Token: 0x040026E9 RID: 9961
		private Positions positions;

		// Token: 0x040026EA RID: 9962
		private Stack stack;

		// Token: 0x040026EB RID: 9963
		private SyntaxTreeNode contentNode;

		// Token: 0x040026EC RID: 9964
		private bool isPartial;

		// Token: 0x040026ED RID: 9965
		private int minMaxNodesCount;

		// Token: 0x040026EE RID: 9966
		private bool enableUpaCheck;
	}
}
