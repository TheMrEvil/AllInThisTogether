using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Data
{
	// Token: 0x02000119 RID: 281
	internal abstract class RBTree<K> : IEnumerable
	{
		// Token: 0x06000FA3 RID: 4003
		protected abstract int CompareNode(K record1, K record2);

		// Token: 0x06000FA4 RID: 4004
		protected abstract int CompareSateliteTreeNode(K record1, K record2);

		// Token: 0x06000FA5 RID: 4005 RVA: 0x0003FE81 File Offset: 0x0003E081
		protected RBTree(TreeAccessMethod accessMethod)
		{
			this._accessMethod = accessMethod;
			this.InitTree();
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0003FE98 File Offset: 0x0003E098
		private void InitTree()
		{
			this.root = 0;
			this._pageTable = new RBTree<K>.TreePage[32];
			this._pageTableMap = new int[(this._pageTable.Length + 32 - 1) / 32];
			this._inUsePageCount = 0;
			this._nextFreePageLine = 0;
			this.AllocPage(32);
			this._pageTable[0]._slots[0]._nodeColor = RBTree<K>.NodeColor.black;
			this._pageTable[0]._slotMap[0] = 1;
			this._pageTable[0].InUseCount = 1;
			this._inUseNodeCount = 1;
			this._inUseSatelliteTreeCount = 0;
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0003FF30 File Offset: 0x0003E130
		private void FreePage(RBTree<K>.TreePage page)
		{
			this.MarkPageFree(page);
			this._pageTable[page.PageId] = null;
			this._inUsePageCount--;
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0003FF58 File Offset: 0x0003E158
		private RBTree<K>.TreePage AllocPage(int size)
		{
			int num = this.GetIndexOfPageWithFreeSlot(false);
			if (num != -1)
			{
				this._pageTable[num] = new RBTree<K>.TreePage(size);
				this._nextFreePageLine = num / 32;
			}
			else
			{
				RBTree<K>.TreePage[] array = new RBTree<K>.TreePage[this._pageTable.Length * 2];
				Array.Copy(this._pageTable, 0, array, 0, this._pageTable.Length);
				int[] array2 = new int[(array.Length + 32 - 1) / 32];
				Array.Copy(this._pageTableMap, 0, array2, 0, this._pageTableMap.Length);
				this._nextFreePageLine = this._pageTableMap.Length;
				num = this._pageTable.Length;
				this._pageTable = array;
				this._pageTableMap = array2;
				this._pageTable[num] = new RBTree<K>.TreePage(size);
			}
			this._pageTable[num].PageId = num;
			this._inUsePageCount++;
			return this._pageTable[num];
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x00040032 File Offset: 0x0003E232
		private void MarkPageFull(RBTree<K>.TreePage page)
		{
			this._pageTableMap[page.PageId / 32] |= 1 << page.PageId % 32;
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0004005A File Offset: 0x0003E25A
		private void MarkPageFree(RBTree<K>.TreePage page)
		{
			this._pageTableMap[page.PageId / 32] &= ~(1 << page.PageId % 32);
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x00040084 File Offset: 0x0003E284
		private static int GetIntValueFromBitMap(uint bitMap)
		{
			int num = 0;
			if ((bitMap & 4294901760U) != 0U)
			{
				num += 16;
				bitMap >>= 16;
			}
			if ((bitMap & 65280U) != 0U)
			{
				num += 8;
				bitMap >>= 8;
			}
			if ((bitMap & 240U) != 0U)
			{
				num += 4;
				bitMap >>= 4;
			}
			if ((bitMap & 12U) != 0U)
			{
				num += 2;
				bitMap >>= 2;
			}
			if ((bitMap & 2U) != 0U)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x000400E4 File Offset: 0x0003E2E4
		private void FreeNode(int nodeId)
		{
			RBTree<K>.TreePage treePage = this._pageTable[nodeId >> 16];
			int num = nodeId & 65535;
			treePage._slots[num] = default(RBTree<K>.Node);
			treePage._slotMap[num / 32] &= ~(1 << num % 32);
			RBTree<K>.TreePage treePage2 = treePage;
			int inUseCount = treePage2.InUseCount;
			treePage2.InUseCount = inUseCount - 1;
			this._inUseNodeCount--;
			if (treePage.InUseCount == 0)
			{
				this.FreePage(treePage);
				return;
			}
			if (treePage.InUseCount == treePage._slots.Length - 1)
			{
				this.MarkPageFree(treePage);
			}
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0004017C File Offset: 0x0003E37C
		private int GetIndexOfPageWithFreeSlot(bool allocatedPage)
		{
			int i = this._nextFreePageLine;
			int num = -1;
			while (i < this._pageTableMap.Length)
			{
				if (this._pageTableMap[i] < -1)
				{
					uint num2 = (uint)this._pageTableMap[i];
					while ((num2 ^ 4294967295U) != 0U)
					{
						uint num3 = ~num2 & num2 + 1U;
						if (((long)this._pageTableMap[i] & (long)((ulong)num3)) != 0L)
						{
							throw ExceptionBuilder.InternalRBTreeError(RBTreeError.PagePositionInSlotInUse);
						}
						num = i * 32 + RBTree<K>.GetIntValueFromBitMap(num3);
						if (allocatedPage)
						{
							if (this._pageTable[num] != null)
							{
								return num;
							}
						}
						else if (this._pageTable[num] == null)
						{
							return num;
						}
						num = -1;
						num2 |= num3;
					}
				}
				i++;
			}
			if (this._nextFreePageLine != 0)
			{
				this._nextFreePageLine = 0;
				num = this.GetIndexOfPageWithFreeSlot(allocatedPage);
			}
			return num;
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000FAE RID: 4014 RVA: 0x0004021F File Offset: 0x0003E41F
		public int Count
		{
			get
			{
				return this._inUseNodeCount - 1;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x00040229 File Offset: 0x0003E429
		public bool HasDuplicates
		{
			get
			{
				return this._inUseSatelliteTreeCount != 0;
			}
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x00040234 File Offset: 0x0003E434
		private int GetNewNode(K key)
		{
			int indexOfPageWithFreeSlot = this.GetIndexOfPageWithFreeSlot(true);
			RBTree<K>.TreePage treePage;
			if (indexOfPageWithFreeSlot != -1)
			{
				treePage = this._pageTable[indexOfPageWithFreeSlot];
			}
			else if (this._inUsePageCount < 4)
			{
				treePage = this.AllocPage(32);
			}
			else if (this._inUsePageCount < 32)
			{
				treePage = this.AllocPage(256);
			}
			else if (this._inUsePageCount < 128)
			{
				treePage = this.AllocPage(1024);
			}
			else if (this._inUsePageCount < 4096)
			{
				treePage = this.AllocPage(4096);
			}
			else if (this._inUsePageCount < 32768)
			{
				treePage = this.AllocPage(8192);
			}
			else
			{
				treePage = this.AllocPage(65536);
			}
			int num = treePage.AllocSlot(this);
			if (num == -1)
			{
				throw ExceptionBuilder.InternalRBTreeError(RBTreeError.NoFreeSlots);
			}
			treePage._slots[num]._selfId = (treePage.PageId << 16 | num);
			treePage._slots[num]._subTreeSize = 1;
			treePage._slots[num]._keyOfNode = key;
			return treePage._slots[num]._selfId;
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0004034C File Offset: 0x0003E54C
		private int Successor(int x_id)
		{
			if (this.Right(x_id) != 0)
			{
				return this.Minimum(this.Right(x_id));
			}
			int num = this.Parent(x_id);
			while (num != 0 && x_id == this.Right(num))
			{
				x_id = num;
				num = this.Parent(num);
			}
			return num;
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x00040394 File Offset: 0x0003E594
		private bool Successor(ref int nodeId, ref int mainTreeNodeId)
		{
			if (nodeId == 0)
			{
				nodeId = this.Minimum(mainTreeNodeId);
				mainTreeNodeId = 0;
			}
			else
			{
				nodeId = this.Successor(nodeId);
				if (nodeId == 0 && mainTreeNodeId != 0)
				{
					nodeId = this.Successor(mainTreeNodeId);
					mainTreeNodeId = 0;
				}
			}
			if (nodeId != 0)
			{
				if (this.Next(nodeId) != 0)
				{
					if (mainTreeNodeId != 0)
					{
						throw ExceptionBuilder.InternalRBTreeError(RBTreeError.NestedSatelliteTreeEnumerator);
					}
					mainTreeNodeId = nodeId;
					nodeId = this.Minimum(this.Next(nodeId));
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x00040404 File Offset: 0x0003E604
		private int Minimum(int x_id)
		{
			while (this.Left(x_id) != 0)
			{
				x_id = this.Left(x_id);
			}
			return x_id;
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0004041C File Offset: 0x0003E61C
		private int LeftRotate(int root_id, int x_id, int mainTreeNode)
		{
			int num = this.Right(x_id);
			this.SetRight(x_id, this.Left(num));
			if (this.Left(num) != 0)
			{
				this.SetParent(this.Left(num), x_id);
			}
			this.SetParent(num, this.Parent(x_id));
			if (this.Parent(x_id) == 0)
			{
				if (root_id == 0)
				{
					this.root = num;
				}
				else
				{
					this.SetNext(mainTreeNode, num);
					this.SetKey(mainTreeNode, this.Key(num));
					root_id = num;
				}
			}
			else if (x_id == this.Left(this.Parent(x_id)))
			{
				this.SetLeft(this.Parent(x_id), num);
			}
			else
			{
				this.SetRight(this.Parent(x_id), num);
			}
			this.SetLeft(num, x_id);
			this.SetParent(x_id, num);
			if (x_id != 0)
			{
				this.SetSubTreeSize(x_id, this.SubTreeSize(this.Left(x_id)) + this.SubTreeSize(this.Right(x_id)) + ((this.Next(x_id) == 0) ? 1 : this.SubTreeSize(this.Next(x_id))));
			}
			if (num != 0)
			{
				this.SetSubTreeSize(num, this.SubTreeSize(this.Left(num)) + this.SubTreeSize(this.Right(num)) + ((this.Next(num) == 0) ? 1 : this.SubTreeSize(this.Next(num))));
			}
			return root_id;
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x00040554 File Offset: 0x0003E754
		private int RightRotate(int root_id, int x_id, int mainTreeNode)
		{
			int num = this.Left(x_id);
			this.SetLeft(x_id, this.Right(num));
			if (this.Right(num) != 0)
			{
				this.SetParent(this.Right(num), x_id);
			}
			this.SetParent(num, this.Parent(x_id));
			if (this.Parent(x_id) == 0)
			{
				if (root_id == 0)
				{
					this.root = num;
				}
				else
				{
					this.SetNext(mainTreeNode, num);
					this.SetKey(mainTreeNode, this.Key(num));
					root_id = num;
				}
			}
			else if (x_id == this.Left(this.Parent(x_id)))
			{
				this.SetLeft(this.Parent(x_id), num);
			}
			else
			{
				this.SetRight(this.Parent(x_id), num);
			}
			this.SetRight(num, x_id);
			this.SetParent(x_id, num);
			if (x_id != 0)
			{
				this.SetSubTreeSize(x_id, this.SubTreeSize(this.Left(x_id)) + this.SubTreeSize(this.Right(x_id)) + ((this.Next(x_id) == 0) ? 1 : this.SubTreeSize(this.Next(x_id))));
			}
			if (num != 0)
			{
				this.SetSubTreeSize(num, this.SubTreeSize(this.Left(num)) + this.SubTreeSize(this.Right(num)) + ((this.Next(num) == 0) ? 1 : this.SubTreeSize(this.Next(num))));
			}
			return root_id;
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0004068C File Offset: 0x0003E88C
		private int RBInsert(int root_id, int x_id, int mainTreeNodeID, int position, bool append)
		{
			this._version++;
			int num = 0;
			int num2 = (root_id == 0) ? this.root : root_id;
			if (this._accessMethod == TreeAccessMethod.KEY_SEARCH_AND_INDEX && !append)
			{
				while (num2 != 0)
				{
					this.IncreaseSize(num2);
					num = num2;
					int num3 = (root_id == 0) ? this.CompareNode(this.Key(x_id), this.Key(num2)) : this.CompareSateliteTreeNode(this.Key(x_id), this.Key(num2));
					if (num3 < 0)
					{
						num2 = this.Left(num2);
					}
					else if (num3 > 0)
					{
						num2 = this.Right(num2);
					}
					else
					{
						if (root_id != 0)
						{
							throw ExceptionBuilder.InternalRBTreeError(RBTreeError.InvalidStateinInsert);
						}
						if (this.Next(num2) != 0)
						{
							root_id = this.RBInsert(this.Next(num2), x_id, num2, -1, false);
							this.SetKey(num2, this.Key(this.Next(num2)));
						}
						else
						{
							int newNode = this.GetNewNode(this.Key(num2));
							this._inUseSatelliteTreeCount++;
							this.SetNext(newNode, num2);
							this.SetColor(newNode, this.color(num2));
							this.SetParent(newNode, this.Parent(num2));
							this.SetLeft(newNode, this.Left(num2));
							this.SetRight(newNode, this.Right(num2));
							if (this.Left(this.Parent(num2)) == num2)
							{
								this.SetLeft(this.Parent(num2), newNode);
							}
							else if (this.Right(this.Parent(num2)) == num2)
							{
								this.SetRight(this.Parent(num2), newNode);
							}
							if (this.Left(num2) != 0)
							{
								this.SetParent(this.Left(num2), newNode);
							}
							if (this.Right(num2) != 0)
							{
								this.SetParent(this.Right(num2), newNode);
							}
							if (this.root == num2)
							{
								this.root = newNode;
							}
							this.SetColor(num2, RBTree<K>.NodeColor.black);
							this.SetParent(num2, 0);
							this.SetLeft(num2, 0);
							this.SetRight(num2, 0);
							int size = this.SubTreeSize(num2);
							this.SetSubTreeSize(num2, 1);
							root_id = this.RBInsert(num2, x_id, newNode, -1, false);
							this.SetSubTreeSize(newNode, size);
						}
						return root_id;
					}
				}
			}
			else
			{
				if (this._accessMethod != TreeAccessMethod.INDEX_ONLY && !append)
				{
					throw ExceptionBuilder.InternalRBTreeError(RBTreeError.UnsupportedAccessMethod1);
				}
				if (position == -1)
				{
					position = this.SubTreeSize(this.root);
				}
				while (num2 != 0)
				{
					this.IncreaseSize(num2);
					num = num2;
					int num4 = position - this.SubTreeSize(this.Left(num));
					if (num4 <= 0)
					{
						num2 = this.Left(num2);
					}
					else
					{
						num2 = this.Right(num2);
						if (num2 != 0)
						{
							position = num4 - 1;
						}
					}
				}
			}
			this.SetParent(x_id, num);
			if (num == 0)
			{
				if (root_id == 0)
				{
					this.root = x_id;
				}
				else
				{
					this.SetNext(mainTreeNodeID, x_id);
					this.SetKey(mainTreeNodeID, this.Key(x_id));
					root_id = x_id;
				}
			}
			else
			{
				int num5;
				if (this._accessMethod == TreeAccessMethod.KEY_SEARCH_AND_INDEX)
				{
					num5 = ((root_id == 0) ? this.CompareNode(this.Key(x_id), this.Key(num)) : this.CompareSateliteTreeNode(this.Key(x_id), this.Key(num)));
				}
				else
				{
					if (this._accessMethod != TreeAccessMethod.INDEX_ONLY)
					{
						throw ExceptionBuilder.InternalRBTreeError(RBTreeError.UnsupportedAccessMethod2);
					}
					num5 = ((position <= 0) ? -1 : 1);
				}
				if (num5 < 0)
				{
					this.SetLeft(num, x_id);
				}
				else
				{
					this.SetRight(num, x_id);
				}
			}
			this.SetLeft(x_id, 0);
			this.SetRight(x_id, 0);
			this.SetColor(x_id, RBTree<K>.NodeColor.red);
			while (this.color(this.Parent(x_id)) == RBTree<K>.NodeColor.red)
			{
				if (this.Parent(x_id) == this.Left(this.Parent(this.Parent(x_id))))
				{
					num = this.Right(this.Parent(this.Parent(x_id)));
					if (this.color(num) == RBTree<K>.NodeColor.red)
					{
						this.SetColor(this.Parent(x_id), RBTree<K>.NodeColor.black);
						this.SetColor(num, RBTree<K>.NodeColor.black);
						this.SetColor(this.Parent(this.Parent(x_id)), RBTree<K>.NodeColor.red);
						x_id = this.Parent(this.Parent(x_id));
					}
					else
					{
						if (x_id == this.Right(this.Parent(x_id)))
						{
							x_id = this.Parent(x_id);
							root_id = this.LeftRotate(root_id, x_id, mainTreeNodeID);
						}
						this.SetColor(this.Parent(x_id), RBTree<K>.NodeColor.black);
						this.SetColor(this.Parent(this.Parent(x_id)), RBTree<K>.NodeColor.red);
						root_id = this.RightRotate(root_id, this.Parent(this.Parent(x_id)), mainTreeNodeID);
					}
				}
				else
				{
					num = this.Left(this.Parent(this.Parent(x_id)));
					if (this.color(num) == RBTree<K>.NodeColor.red)
					{
						this.SetColor(this.Parent(x_id), RBTree<K>.NodeColor.black);
						this.SetColor(num, RBTree<K>.NodeColor.black);
						this.SetColor(this.Parent(this.Parent(x_id)), RBTree<K>.NodeColor.red);
						x_id = this.Parent(this.Parent(x_id));
					}
					else
					{
						if (x_id == this.Left(this.Parent(x_id)))
						{
							x_id = this.Parent(x_id);
							root_id = this.RightRotate(root_id, x_id, mainTreeNodeID);
						}
						this.SetColor(this.Parent(x_id), RBTree<K>.NodeColor.black);
						this.SetColor(this.Parent(this.Parent(x_id)), RBTree<K>.NodeColor.red);
						root_id = this.LeftRotate(root_id, this.Parent(this.Parent(x_id)), mainTreeNodeID);
					}
				}
			}
			if (root_id == 0)
			{
				this.SetColor(this.root, RBTree<K>.NodeColor.black);
			}
			else
			{
				this.SetColor(root_id, RBTree<K>.NodeColor.black);
			}
			return root_id;
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x00040B80 File Offset: 0x0003ED80
		public void UpdateNodeKey(K currentKey, K newKey)
		{
			RBTree<K>.NodePath nodeByKey = this.GetNodeByKey(currentKey);
			if (this.Parent(nodeByKey._nodeID) == 0 && nodeByKey._nodeID != this.root)
			{
				this.SetKey(nodeByKey._mainTreeNodeID, newKey);
			}
			this.SetKey(nodeByKey._nodeID, newKey);
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00040BCC File Offset: 0x0003EDCC
		public K DeleteByIndex(int i)
		{
			RBTree<K>.NodePath nodeByIndex = this.GetNodeByIndex(i);
			K result = this.Key(nodeByIndex._nodeID);
			this.RBDeleteX(0, nodeByIndex._nodeID, nodeByIndex._mainTreeNodeID);
			return result;
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x00040C01 File Offset: 0x0003EE01
		public int RBDelete(int z_id)
		{
			return this.RBDeleteX(0, z_id, 0);
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x00040C0C File Offset: 0x0003EE0C
		private int RBDeleteX(int root_id, int z_id, int mainTreeNodeID)
		{
			if (this.Next(z_id) != 0)
			{
				return this.RBDeleteX(this.Next(z_id), this.Next(z_id), z_id);
			}
			bool flag = false;
			int num = (this._accessMethod == TreeAccessMethod.KEY_SEARCH_AND_INDEX) ? mainTreeNodeID : z_id;
			if (this.Next(num) != 0)
			{
				root_id = this.Next(num);
			}
			if (this.SubTreeSize(this.Next(num)) == 2)
			{
				flag = true;
			}
			else if (this.SubTreeSize(this.Next(num)) == 1)
			{
				throw ExceptionBuilder.InternalRBTreeError(RBTreeError.InvalidNextSizeInDelete);
			}
			int num2;
			if (this.Left(z_id) == 0 || this.Right(z_id) == 0)
			{
				num2 = z_id;
			}
			else
			{
				num2 = this.Successor(z_id);
			}
			int num3;
			if (this.Left(num2) != 0)
			{
				num3 = this.Left(num2);
			}
			else
			{
				num3 = this.Right(num2);
			}
			int num4 = this.Parent(num2);
			if (num3 != 0)
			{
				this.SetParent(num3, num4);
			}
			if (num4 == 0)
			{
				if (root_id == 0)
				{
					this.root = num3;
				}
				else
				{
					root_id = num3;
				}
			}
			else if (num2 == this.Left(num4))
			{
				this.SetLeft(num4, num3);
			}
			else
			{
				this.SetRight(num4, num3);
			}
			if (num2 != z_id)
			{
				this.SetKey(z_id, this.Key(num2));
				this.SetNext(z_id, this.Next(num2));
			}
			if (this.Next(num) != 0)
			{
				if (root_id == 0 && z_id != num)
				{
					throw ExceptionBuilder.InternalRBTreeError(RBTreeError.InvalidStateinDelete);
				}
				if (root_id != 0)
				{
					this.SetNext(num, root_id);
					this.SetKey(num, this.Key(root_id));
				}
			}
			for (int nodeId = num4; nodeId != 0; nodeId = this.Parent(nodeId))
			{
				this.RecomputeSize(nodeId);
			}
			if (root_id != 0)
			{
				for (int nodeId2 = num; nodeId2 != 0; nodeId2 = this.Parent(nodeId2))
				{
					this.DecreaseSize(nodeId2);
				}
			}
			if (this.color(num2) == RBTree<K>.NodeColor.black)
			{
				root_id = this.RBDeleteFixup(root_id, num3, num4, mainTreeNodeID);
			}
			if (flag)
			{
				if (num == 0 || this.SubTreeSize(this.Next(num)) != 1)
				{
					throw ExceptionBuilder.InternalRBTreeError(RBTreeError.InvalidNodeSizeinDelete);
				}
				this._inUseSatelliteTreeCount--;
				int num5 = this.Next(num);
				this.SetLeft(num5, this.Left(num));
				this.SetRight(num5, this.Right(num));
				this.SetSubTreeSize(num5, this.SubTreeSize(num));
				this.SetColor(num5, this.color(num));
				if (this.Parent(num) != 0)
				{
					this.SetParent(num5, this.Parent(num));
					if (this.Left(this.Parent(num)) == num)
					{
						this.SetLeft(this.Parent(num), num5);
					}
					else
					{
						this.SetRight(this.Parent(num), num5);
					}
				}
				if (this.Left(num) != 0)
				{
					this.SetParent(this.Left(num), num5);
				}
				if (this.Right(num) != 0)
				{
					this.SetParent(this.Right(num), num5);
				}
				if (this.root == num)
				{
					this.root = num5;
				}
				this.FreeNode(num);
				num = 0;
			}
			else if (this.Next(num) != 0)
			{
				if (root_id == 0 && z_id != num)
				{
					throw ExceptionBuilder.InternalRBTreeError(RBTreeError.InvalidStateinEndDelete);
				}
				if (root_id != 0)
				{
					this.SetNext(num, root_id);
					this.SetKey(num, this.Key(root_id));
				}
			}
			if (num2 != z_id)
			{
				this.SetLeft(num2, this.Left(z_id));
				this.SetRight(num2, this.Right(z_id));
				this.SetColor(num2, this.color(z_id));
				this.SetSubTreeSize(num2, this.SubTreeSize(z_id));
				if (this.Parent(z_id) != 0)
				{
					this.SetParent(num2, this.Parent(z_id));
					if (this.Left(this.Parent(z_id)) == z_id)
					{
						this.SetLeft(this.Parent(z_id), num2);
					}
					else
					{
						this.SetRight(this.Parent(z_id), num2);
					}
				}
				else
				{
					this.SetParent(num2, 0);
				}
				if (this.Left(z_id) != 0)
				{
					this.SetParent(this.Left(z_id), num2);
				}
				if (this.Right(z_id) != 0)
				{
					this.SetParent(this.Right(z_id), num2);
				}
				if (this.root == z_id)
				{
					this.root = num2;
				}
				else if (root_id == z_id)
				{
					root_id = num2;
				}
				if (num != 0 && this.Next(num) == z_id)
				{
					this.SetNext(num, num2);
				}
			}
			this.FreeNode(z_id);
			this._version++;
			return z_id;
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x00041000 File Offset: 0x0003F200
		private int RBDeleteFixup(int root_id, int x_id, int px_id, int mainTreeNodeID)
		{
			if (x_id == 0 && px_id == 0)
			{
				return 0;
			}
			while (((root_id == 0) ? this.root : root_id) != x_id && this.color(x_id) == RBTree<K>.NodeColor.black)
			{
				if ((x_id != 0 && x_id == this.Left(this.Parent(x_id))) || (x_id == 0 && this.Left(px_id) == 0))
				{
					int num = (x_id == 0) ? this.Right(px_id) : this.Right(this.Parent(x_id));
					if (num == 0)
					{
						throw ExceptionBuilder.InternalRBTreeError(RBTreeError.RBDeleteFixup);
					}
					if (this.color(num) == RBTree<K>.NodeColor.red)
					{
						this.SetColor(num, RBTree<K>.NodeColor.black);
						this.SetColor(px_id, RBTree<K>.NodeColor.red);
						root_id = this.LeftRotate(root_id, px_id, mainTreeNodeID);
						num = ((x_id == 0) ? this.Right(px_id) : this.Right(this.Parent(x_id)));
					}
					if (this.color(this.Left(num)) == RBTree<K>.NodeColor.black && this.color(this.Right(num)) == RBTree<K>.NodeColor.black)
					{
						this.SetColor(num, RBTree<K>.NodeColor.red);
						x_id = px_id;
						px_id = this.Parent(px_id);
					}
					else
					{
						if (this.color(this.Right(num)) == RBTree<K>.NodeColor.black)
						{
							this.SetColor(this.Left(num), RBTree<K>.NodeColor.black);
							this.SetColor(num, RBTree<K>.NodeColor.red);
							root_id = this.RightRotate(root_id, num, mainTreeNodeID);
							num = ((x_id == 0) ? this.Right(px_id) : this.Right(this.Parent(x_id)));
						}
						this.SetColor(num, this.color(px_id));
						this.SetColor(px_id, RBTree<K>.NodeColor.black);
						this.SetColor(this.Right(num), RBTree<K>.NodeColor.black);
						root_id = this.LeftRotate(root_id, px_id, mainTreeNodeID);
						x_id = ((root_id == 0) ? this.root : root_id);
						px_id = this.Parent(x_id);
					}
				}
				else
				{
					int num = this.Left(px_id);
					if (this.color(num) == RBTree<K>.NodeColor.red)
					{
						this.SetColor(num, RBTree<K>.NodeColor.black);
						if (x_id != 0)
						{
							this.SetColor(px_id, RBTree<K>.NodeColor.red);
							root_id = this.RightRotate(root_id, px_id, mainTreeNodeID);
							num = ((x_id == 0) ? this.Left(px_id) : this.Left(this.Parent(x_id)));
						}
						else
						{
							this.SetColor(px_id, RBTree<K>.NodeColor.red);
							root_id = this.RightRotate(root_id, px_id, mainTreeNodeID);
							num = ((x_id == 0) ? this.Left(px_id) : this.Left(this.Parent(x_id)));
							if (num == 0)
							{
								throw ExceptionBuilder.InternalRBTreeError(RBTreeError.CannotRotateInvalidsuccessorNodeinDelete);
							}
						}
					}
					if (this.color(this.Right(num)) == RBTree<K>.NodeColor.black && this.color(this.Left(num)) == RBTree<K>.NodeColor.black)
					{
						this.SetColor(num, RBTree<K>.NodeColor.red);
						x_id = px_id;
						px_id = this.Parent(px_id);
					}
					else
					{
						if (this.color(this.Left(num)) == RBTree<K>.NodeColor.black)
						{
							this.SetColor(this.Right(num), RBTree<K>.NodeColor.black);
							this.SetColor(num, RBTree<K>.NodeColor.red);
							root_id = this.LeftRotate(root_id, num, mainTreeNodeID);
							num = ((x_id == 0) ? this.Left(px_id) : this.Left(this.Parent(x_id)));
						}
						if (x_id != 0)
						{
							this.SetColor(num, this.color(px_id));
							this.SetColor(px_id, RBTree<K>.NodeColor.black);
							this.SetColor(this.Left(num), RBTree<K>.NodeColor.black);
							root_id = this.RightRotate(root_id, px_id, mainTreeNodeID);
							x_id = ((root_id == 0) ? this.root : root_id);
							px_id = this.Parent(x_id);
						}
						else
						{
							this.SetColor(num, this.color(px_id));
							this.SetColor(px_id, RBTree<K>.NodeColor.black);
							this.SetColor(this.Left(num), RBTree<K>.NodeColor.black);
							root_id = this.RightRotate(root_id, px_id, mainTreeNodeID);
							x_id = ((root_id == 0) ? this.root : root_id);
							px_id = this.Parent(x_id);
						}
					}
				}
			}
			this.SetColor(x_id, RBTree<K>.NodeColor.black);
			return root_id;
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x00041338 File Offset: 0x0003F538
		private int SearchSubTree(int root_id, K key)
		{
			if (root_id != 0 && this._accessMethod != TreeAccessMethod.KEY_SEARCH_AND_INDEX)
			{
				throw ExceptionBuilder.InternalRBTreeError(RBTreeError.UnsupportedAccessMethodInNonNillRootSubtree);
			}
			int num = (root_id == 0) ? this.root : root_id;
			while (num != 0)
			{
				int num2 = (root_id == 0) ? this.CompareNode(key, this.Key(num)) : this.CompareSateliteTreeNode(key, this.Key(num));
				if (num2 == 0)
				{
					break;
				}
				if (num2 < 0)
				{
					num = this.Left(num);
				}
				else
				{
					num = this.Right(num);
				}
			}
			return num;
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x000413A8 File Offset: 0x0003F5A8
		public int Search(K key)
		{
			int num = this.root;
			while (num != 0)
			{
				int num2 = this.CompareNode(key, this.Key(num));
				if (num2 == 0)
				{
					break;
				}
				if (num2 < 0)
				{
					num = this.Left(num);
				}
				else
				{
					num = this.Right(num);
				}
			}
			return num;
		}

		// Token: 0x170002BD RID: 701
		public K this[int index]
		{
			get
			{
				return this.Key(this.GetNodeByIndex(index)._nodeID);
			}
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x00041400 File Offset: 0x0003F600
		private RBTree<K>.NodePath GetNodeByKey(K key)
		{
			int num = this.SearchSubTree(0, key);
			if (this.Next(num) != 0)
			{
				return new RBTree<K>.NodePath(this.SearchSubTree(this.Next(num), key), num);
			}
			K k = this.Key(num);
			if (!k.Equals(key))
			{
				num = 0;
			}
			return new RBTree<K>.NodePath(num, 0);
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0004145C File Offset: 0x0003F65C
		public int GetIndexByKey(K key)
		{
			int result = -1;
			RBTree<K>.NodePath nodeByKey = this.GetNodeByKey(key);
			if (nodeByKey._nodeID != 0)
			{
				result = this.GetIndexByNodePath(nodeByKey);
			}
			return result;
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x00041484 File Offset: 0x0003F684
		public int GetIndexByNode(int node)
		{
			if (this._inUseSatelliteTreeCount == 0)
			{
				return this.ComputeIndexByNode(node);
			}
			if (this.Next(node) != 0)
			{
				return this.ComputeIndexWithSatelliteByNode(node);
			}
			int num = this.SearchSubTree(0, this.Key(node));
			if (num == node)
			{
				return this.ComputeIndexWithSatelliteByNode(node);
			}
			return this.ComputeIndexWithSatelliteByNode(num) + this.ComputeIndexByNode(node);
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x000414DC File Offset: 0x0003F6DC
		private int GetIndexByNodePath(RBTree<K>.NodePath path)
		{
			if (this._inUseSatelliteTreeCount == 0)
			{
				return this.ComputeIndexByNode(path._nodeID);
			}
			if (path._mainTreeNodeID == 0)
			{
				return this.ComputeIndexWithSatelliteByNode(path._nodeID);
			}
			return this.ComputeIndexWithSatelliteByNode(path._mainTreeNodeID) + this.ComputeIndexByNode(path._nodeID);
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0004152C File Offset: 0x0003F72C
		private int ComputeIndexByNode(int nodeId)
		{
			int num = this.SubTreeSize(this.Left(nodeId));
			while (nodeId != 0)
			{
				int num2 = this.Parent(nodeId);
				if (nodeId == this.Right(num2))
				{
					num += this.SubTreeSize(this.Left(num2)) + 1;
				}
				nodeId = num2;
			}
			return num;
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x00041574 File Offset: 0x0003F774
		private int ComputeIndexWithSatelliteByNode(int nodeId)
		{
			int num = this.SubTreeSize(this.Left(nodeId));
			while (nodeId != 0)
			{
				int num2 = this.Parent(nodeId);
				if (nodeId == this.Right(num2))
				{
					num += this.SubTreeSize(this.Left(num2)) + ((this.Next(num2) == 0) ? 1 : this.SubTreeSize(this.Next(num2)));
				}
				nodeId = num2;
			}
			return num;
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x000415D4 File Offset: 0x0003F7D4
		private RBTree<K>.NodePath GetNodeByIndex(int userIndex)
		{
			int num;
			int mainTreeNodeID;
			if (this._inUseSatelliteTreeCount == 0)
			{
				num = this.ComputeNodeByIndex(this.root, userIndex + 1);
				mainTreeNodeID = 0;
			}
			else
			{
				num = this.ComputeNodeByIndex(userIndex, out mainTreeNodeID);
			}
			if (num != 0)
			{
				return new RBTree<K>.NodePath(num, mainTreeNodeID);
			}
			if (TreeAccessMethod.INDEX_ONLY == this._accessMethod)
			{
				throw ExceptionBuilder.RowOutOfRange(userIndex);
			}
			throw ExceptionBuilder.InternalRBTreeError(RBTreeError.IndexOutOFRangeinGetNodeByIndex);
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0004162C File Offset: 0x0003F82C
		private int ComputeNodeByIndex(int index, out int satelliteRootId)
		{
			index++;
			satelliteRootId = 0;
			int num = this.root;
			int num2;
			while (num != 0 && ((num2 = this.SubTreeSize(this.Left(num)) + 1) != index || this.Next(num) != 0))
			{
				if (index < num2)
				{
					num = this.Left(num);
				}
				else
				{
					if (this.Next(num) != 0 && index >= num2 && index <= num2 + this.SubTreeSize(this.Next(num)) - 1)
					{
						satelliteRootId = num;
						index = index - num2 + 1;
						return this.ComputeNodeByIndex(this.Next(num), index);
					}
					if (this.Next(num) == 0)
					{
						index -= num2;
					}
					else
					{
						index -= num2 + this.SubTreeSize(this.Next(num)) - 1;
					}
					num = this.Right(num);
				}
			}
			return num;
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x000416E8 File Offset: 0x0003F8E8
		private int ComputeNodeByIndex(int x_id, int index)
		{
			while (x_id != 0)
			{
				int num = this.Left(x_id);
				int num2 = this.SubTreeSize(num) + 1;
				if (index < num2)
				{
					x_id = num;
				}
				else
				{
					if (num2 >= index)
					{
						break;
					}
					x_id = this.Right(x_id);
					index -= num2;
				}
			}
			return x_id;
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x00041728 File Offset: 0x0003F928
		public int Insert(K item)
		{
			int newNode = this.GetNewNode(item);
			this.RBInsert(0, newNode, 0, -1, false);
			return newNode;
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x0004174C File Offset: 0x0003F94C
		public int Add(K item)
		{
			int newNode = this.GetNewNode(item);
			this.RBInsert(0, newNode, 0, -1, false);
			return newNode;
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0004176E File Offset: 0x0003F96E
		public IEnumerator GetEnumerator()
		{
			return new RBTree<K>.RBTreeEnumerator(this);
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x0004177C File Offset: 0x0003F97C
		public int IndexOf(int nodeId, K item)
		{
			int result = -1;
			if (nodeId == 0)
			{
				return result;
			}
			if (this.Key(nodeId) == item)
			{
				return this.GetIndexByNode(nodeId);
			}
			if ((result = this.IndexOf(this.Left(nodeId), item)) != -1)
			{
				return result;
			}
			return this.IndexOf(this.Right(nodeId), item);
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x000417D5 File Offset: 0x0003F9D5
		public int Insert(int position, K item)
		{
			return this.InsertAt(position, item, false);
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x000417E0 File Offset: 0x0003F9E0
		public int InsertAt(int position, K item, bool append)
		{
			int newNode = this.GetNewNode(item);
			this.RBInsert(0, newNode, 0, position, append);
			return newNode;
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x00041802 File Offset: 0x0003FA02
		public void RemoveAt(int position)
		{
			this.DeleteByIndex(position);
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0004180C File Offset: 0x0003FA0C
		public void Clear()
		{
			this.InitTree();
			this._version++;
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x00041824 File Offset: 0x0003FA24
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw ExceptionBuilder.ArgumentNull("array");
			}
			if (index < 0)
			{
				throw ExceptionBuilder.ArgumentOutOfRange("index");
			}
			int count = this.Count;
			if (array.Length - index < this.Count)
			{
				throw ExceptionBuilder.InvalidOffsetLength();
			}
			int num = this.Minimum(this.root);
			for (int i = 0; i < count; i++)
			{
				array.SetValue(this.Key(num), index + i);
				num = this.Successor(num);
			}
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x000418A4 File Offset: 0x0003FAA4
		public void CopyTo(K[] array, int index)
		{
			if (array == null)
			{
				throw ExceptionBuilder.ArgumentNull("array");
			}
			if (index < 0)
			{
				throw ExceptionBuilder.ArgumentOutOfRange("index");
			}
			int count = this.Count;
			if (array.Length - index < this.Count)
			{
				throw ExceptionBuilder.InvalidOffsetLength();
			}
			int num = this.Minimum(this.root);
			for (int i = 0; i < count; i++)
			{
				array[index + i] = this.Key(num);
				num = this.Successor(num);
			}
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x00041919 File Offset: 0x0003FB19
		private void SetRight(int nodeId, int rightNodeId)
		{
			this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._rightId = rightNodeId;
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0004193D File Offset: 0x0003FB3D
		private void SetLeft(int nodeId, int leftNodeId)
		{
			this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._leftId = leftNodeId;
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x00041961 File Offset: 0x0003FB61
		private void SetParent(int nodeId, int parentNodeId)
		{
			this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._parentId = parentNodeId;
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x00041985 File Offset: 0x0003FB85
		private void SetColor(int nodeId, RBTree<K>.NodeColor color)
		{
			this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._nodeColor = color;
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x000419A9 File Offset: 0x0003FBA9
		private void SetKey(int nodeId, K key)
		{
			this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._keyOfNode = key;
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x000419CD File Offset: 0x0003FBCD
		private void SetNext(int nodeId, int nextNodeId)
		{
			this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._nextId = nextNodeId;
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x000419F1 File Offset: 0x0003FBF1
		private void SetSubTreeSize(int nodeId, int size)
		{
			this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._subTreeSize = size;
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x00041A15 File Offset: 0x0003FC15
		private void IncreaseSize(int nodeId)
		{
			RBTree<K>.Node[] slots = this._pageTable[nodeId >> 16]._slots;
			int num = nodeId & 65535;
			slots[num]._subTreeSize = slots[num]._subTreeSize + 1;
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x00041A40 File Offset: 0x0003FC40
		private void RecomputeSize(int nodeId)
		{
			int subTreeSize = this.SubTreeSize(this.Left(nodeId)) + this.SubTreeSize(this.Right(nodeId)) + ((this.Next(nodeId) == 0) ? 1 : this.SubTreeSize(this.Next(nodeId)));
			this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._subTreeSize = subTreeSize;
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x00041AA5 File Offset: 0x0003FCA5
		private void DecreaseSize(int nodeId)
		{
			RBTree<K>.Node[] slots = this._pageTable[nodeId >> 16]._slots;
			int num = nodeId & 65535;
			slots[num]._subTreeSize = slots[num]._subTreeSize - 1;
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x00041ACD File Offset: 0x0003FCCD
		[Conditional("DEBUG")]
		private void VerifySize(int nodeId, int size)
		{
			this.SubTreeSize(this.Left(nodeId));
			this.SubTreeSize(this.Right(nodeId));
			if (this.Next(nodeId) != 0)
			{
				this.SubTreeSize(this.Next(nodeId));
			}
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x00041B02 File Offset: 0x0003FD02
		public int Right(int nodeId)
		{
			return this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._rightId;
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x00041B25 File Offset: 0x0003FD25
		public int Left(int nodeId)
		{
			return this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._leftId;
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x00041B48 File Offset: 0x0003FD48
		public int Parent(int nodeId)
		{
			return this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._parentId;
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x00041B6B File Offset: 0x0003FD6B
		private RBTree<K>.NodeColor color(int nodeId)
		{
			return this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._nodeColor;
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x00041B8E File Offset: 0x0003FD8E
		public int Next(int nodeId)
		{
			return this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._nextId;
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x00041BB1 File Offset: 0x0003FDB1
		public int SubTreeSize(int nodeId)
		{
			return this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._subTreeSize;
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x00041BD4 File Offset: 0x0003FDD4
		public K Key(int nodeId)
		{
			return this._pageTable[nodeId >> 16]._slots[nodeId & 65535]._keyOfNode;
		}

		// Token: 0x040009AF RID: 2479
		internal const int DefaultPageSize = 32;

		// Token: 0x040009B0 RID: 2480
		internal const int NIL = 0;

		// Token: 0x040009B1 RID: 2481
		private RBTree<K>.TreePage[] _pageTable;

		// Token: 0x040009B2 RID: 2482
		private int[] _pageTableMap;

		// Token: 0x040009B3 RID: 2483
		private int _inUsePageCount;

		// Token: 0x040009B4 RID: 2484
		private int _nextFreePageLine;

		// Token: 0x040009B5 RID: 2485
		public int root;

		// Token: 0x040009B6 RID: 2486
		private int _version;

		// Token: 0x040009B7 RID: 2487
		private int _inUseNodeCount;

		// Token: 0x040009B8 RID: 2488
		private int _inUseSatelliteTreeCount;

		// Token: 0x040009B9 RID: 2489
		private readonly TreeAccessMethod _accessMethod;

		// Token: 0x0200011A RID: 282
		private enum NodeColor
		{
			// Token: 0x040009BB RID: 2491
			red,
			// Token: 0x040009BC RID: 2492
			black
		}

		// Token: 0x0200011B RID: 283
		private struct Node
		{
			// Token: 0x040009BD RID: 2493
			internal int _selfId;

			// Token: 0x040009BE RID: 2494
			internal int _leftId;

			// Token: 0x040009BF RID: 2495
			internal int _rightId;

			// Token: 0x040009C0 RID: 2496
			internal int _parentId;

			// Token: 0x040009C1 RID: 2497
			internal int _nextId;

			// Token: 0x040009C2 RID: 2498
			internal int _subTreeSize;

			// Token: 0x040009C3 RID: 2499
			internal K _keyOfNode;

			// Token: 0x040009C4 RID: 2500
			internal RBTree<K>.NodeColor _nodeColor;
		}

		// Token: 0x0200011C RID: 284
		private readonly struct NodePath
		{
			// Token: 0x06000FE4 RID: 4068 RVA: 0x00041BF7 File Offset: 0x0003FDF7
			internal NodePath(int nodeID, int mainTreeNodeID)
			{
				this._nodeID = nodeID;
				this._mainTreeNodeID = mainTreeNodeID;
			}

			// Token: 0x040009C5 RID: 2501
			internal readonly int _nodeID;

			// Token: 0x040009C6 RID: 2502
			internal readonly int _mainTreeNodeID;
		}

		// Token: 0x0200011D RID: 285
		private sealed class TreePage
		{
			// Token: 0x06000FE5 RID: 4069 RVA: 0x00041C07 File Offset: 0x0003FE07
			internal TreePage(int size)
			{
				if (size > 65536)
				{
					throw ExceptionBuilder.InternalRBTreeError(RBTreeError.InvalidPageSize);
				}
				this._slots = new RBTree<K>.Node[size];
				this._slotMap = new int[(size + 32 - 1) / 32];
			}

			// Token: 0x06000FE6 RID: 4070 RVA: 0x00041C40 File Offset: 0x0003FE40
			internal int AllocSlot(RBTree<K> tree)
			{
				int num = -1;
				if (this._inUseCount < this._slots.Length)
				{
					for (int i = this._nextFreeSlotLine; i < this._slotMap.Length; i++)
					{
						if (this._slotMap[i] < -1)
						{
							int num2 = ~this._slotMap[i] & this._slotMap[i] + 1;
							this._slotMap[i] |= num2;
							this._inUseCount++;
							if (this._inUseCount == this._slots.Length)
							{
								tree.MarkPageFull(this);
							}
							tree._inUseNodeCount++;
							num = RBTree<K>.GetIntValueFromBitMap((uint)num2);
							this._nextFreeSlotLine = i;
							num = i * 32 + num;
							break;
						}
					}
					if (num == -1 && this._nextFreeSlotLine != 0)
					{
						this._nextFreeSlotLine = 0;
						num = this.AllocSlot(tree);
					}
				}
				return num;
			}

			// Token: 0x170002BE RID: 702
			// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x00041D1D File Offset: 0x0003FF1D
			// (set) Token: 0x06000FE8 RID: 4072 RVA: 0x00041D25 File Offset: 0x0003FF25
			internal int InUseCount
			{
				get
				{
					return this._inUseCount;
				}
				set
				{
					this._inUseCount = value;
				}
			}

			// Token: 0x170002BF RID: 703
			// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x00041D2E File Offset: 0x0003FF2E
			// (set) Token: 0x06000FEA RID: 4074 RVA: 0x00041D36 File Offset: 0x0003FF36
			internal int PageId
			{
				get
				{
					return this._pageId;
				}
				set
				{
					this._pageId = value;
				}
			}

			// Token: 0x040009C7 RID: 2503
			public const int slotLineSize = 32;

			// Token: 0x040009C8 RID: 2504
			internal readonly RBTree<K>.Node[] _slots;

			// Token: 0x040009C9 RID: 2505
			internal readonly int[] _slotMap;

			// Token: 0x040009CA RID: 2506
			private int _inUseCount;

			// Token: 0x040009CB RID: 2507
			private int _pageId;

			// Token: 0x040009CC RID: 2508
			private int _nextFreeSlotLine;
		}

		// Token: 0x0200011E RID: 286
		internal struct RBTreeEnumerator : IEnumerator<K>, IDisposable, IEnumerator
		{
			// Token: 0x06000FEB RID: 4075 RVA: 0x00041D3F File Offset: 0x0003FF3F
			internal RBTreeEnumerator(RBTree<K> tree)
			{
				this._tree = tree;
				this._version = tree._version;
				this._index = 0;
				this._mainTreeNodeId = tree.root;
				this._current = default(K);
			}

			// Token: 0x06000FEC RID: 4076 RVA: 0x00041D74 File Offset: 0x0003FF74
			internal RBTreeEnumerator(RBTree<K> tree, int position)
			{
				this._tree = tree;
				this._version = tree._version;
				if (position == 0)
				{
					this._index = 0;
					this._mainTreeNodeId = tree.root;
				}
				else
				{
					this._index = tree.ComputeNodeByIndex(position - 1, out this._mainTreeNodeId);
					if (this._index == 0)
					{
						throw ExceptionBuilder.InternalRBTreeError(RBTreeError.IndexOutOFRangeinGetNodeByIndex);
					}
				}
				this._current = default(K);
			}

			// Token: 0x06000FED RID: 4077 RVA: 0x00007EED File Offset: 0x000060ED
			public void Dispose()
			{
			}

			// Token: 0x06000FEE RID: 4078 RVA: 0x00041DE0 File Offset: 0x0003FFE0
			public bool MoveNext()
			{
				if (this._version != this._tree._version)
				{
					throw ExceptionBuilder.EnumeratorModified();
				}
				bool result = this._tree.Successor(ref this._index, ref this._mainTreeNodeId);
				this._current = this._tree.Key(this._index);
				return result;
			}

			// Token: 0x170002C0 RID: 704
			// (get) Token: 0x06000FEF RID: 4079 RVA: 0x00041E34 File Offset: 0x00040034
			public K Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x170002C1 RID: 705
			// (get) Token: 0x06000FF0 RID: 4080 RVA: 0x00041E3C File Offset: 0x0004003C
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000FF1 RID: 4081 RVA: 0x00041E49 File Offset: 0x00040049
			void IEnumerator.Reset()
			{
				if (this._version != this._tree._version)
				{
					throw ExceptionBuilder.EnumeratorModified();
				}
				this._index = 0;
				this._mainTreeNodeId = this._tree.root;
				this._current = default(K);
			}

			// Token: 0x040009CD RID: 2509
			private readonly RBTree<K> _tree;

			// Token: 0x040009CE RID: 2510
			private readonly int _version;

			// Token: 0x040009CF RID: 2511
			private int _index;

			// Token: 0x040009D0 RID: 2512
			private int _mainTreeNodeId;

			// Token: 0x040009D1 RID: 2513
			private K _current;
		}
	}
}
