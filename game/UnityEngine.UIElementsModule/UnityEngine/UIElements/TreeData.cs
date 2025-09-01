using System;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x02000193 RID: 403
	internal readonly struct TreeData<T>
	{
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x00036DF8 File Offset: 0x00034FF8
		public IEnumerable<int> rootItemIds
		{
			get
			{
				return this.m_RootItemIds;
			}
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00036E00 File Offset: 0x00035000
		public TreeData(IList<TreeViewItemData<T>> rootItems)
		{
			this.m_RootItemIds = new List<int>();
			this.m_Tree = new Dictionary<int, TreeViewItemData<T>>();
			this.m_ParentIds = new Dictionary<int, int>();
			this.m_ChildrenIds = new Dictionary<int, List<int>>();
			this.RefreshTree(rootItems);
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00036E38 File Offset: 0x00035038
		public TreeViewItemData<T> GetDataForId(int id)
		{
			return this.m_Tree[id];
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x00036E58 File Offset: 0x00035058
		public int GetParentId(int id)
		{
			int num;
			bool flag = this.m_ParentIds.TryGetValue(id, out num);
			int result;
			if (flag)
			{
				result = num;
			}
			else
			{
				result = -1;
			}
			return result;
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00036E84 File Offset: 0x00035084
		public void AddItem(TreeViewItemData<T> item, int parentId, int childIndex)
		{
			List<TreeViewItemData<T>> list = CollectionPool<List<TreeViewItemData<T>>, TreeViewItemData<T>>.Get();
			list.Add(item);
			this.BuildTree(list, false);
			this.AddItemToParent(item, parentId, childIndex);
			CollectionPool<List<TreeViewItemData<T>>, TreeViewItemData<T>>.Release(list);
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x00036EBC File Offset: 0x000350BC
		public bool TryRemove(int id)
		{
			int parentId;
			bool flag = this.m_ParentIds.TryGetValue(id, out parentId);
			if (flag)
			{
				this.RemoveFromParent(id, parentId);
			}
			else
			{
				this.m_RootItemIds.Remove(id);
			}
			return this.TryRemoveChildrenIds(id);
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x00036F04 File Offset: 0x00035104
		public void Move(int id, int newParentId, int childIndex)
		{
			TreeViewItemData<T> item;
			bool flag = !this.m_Tree.TryGetValue(id, out item);
			if (!flag)
			{
				int num;
				bool flag2 = this.m_ParentIds.TryGetValue(id, out num);
				if (flag2)
				{
					bool flag3 = num == newParentId;
					if (flag3)
					{
						int childIndex2 = this.m_Tree[num].GetChildIndex(id);
						bool flag4 = childIndex2 < childIndex;
						if (flag4)
						{
							childIndex--;
						}
					}
					this.RemoveFromParent(item.id, num);
				}
				else
				{
					int num2 = this.m_RootItemIds.IndexOf(id);
					bool flag5 = newParentId == -1 && num2 < childIndex;
					if (flag5)
					{
						childIndex--;
					}
					this.m_RootItemIds.Remove(id);
				}
				this.AddItemToParent(item, newParentId, childIndex);
			}
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x00036FC8 File Offset: 0x000351C8
		private void AddItemToParent(TreeViewItemData<T> item, int parentId, int childIndex)
		{
			bool flag = parentId == -1;
			if (flag)
			{
				this.m_ParentIds.Remove(item.id);
				bool flag2 = childIndex < 0 || childIndex >= this.m_RootItemIds.Count;
				if (flag2)
				{
					this.m_RootItemIds.Add(item.id);
				}
				else
				{
					this.m_RootItemIds.Insert(childIndex, item.id);
				}
			}
			else
			{
				TreeViewItemData<T> treeViewItemData = this.m_Tree[parentId];
				treeViewItemData.InsertChild(item, childIndex);
				this.m_Tree[parentId] = treeViewItemData;
				this.m_ParentIds[item.id] = parentId;
				this.UpdateParentTree(treeViewItemData);
			}
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0003707C File Offset: 0x0003527C
		private void RemoveFromParent(int id, int parentId)
		{
			TreeViewItemData<T> treeViewItemData = this.m_Tree[parentId];
			treeViewItemData.RemoveChild(id);
			this.m_Tree[parentId] = treeViewItemData;
			List<int> list;
			bool flag = this.m_ChildrenIds.TryGetValue(parentId, out list);
			if (flag)
			{
				list.Remove(id);
			}
			this.UpdateParentTree(treeViewItemData);
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x000370D0 File Offset: 0x000352D0
		private void UpdateParentTree(TreeViewItemData<T> current)
		{
			for (;;)
			{
				int key;
				bool flag = this.m_ParentIds.TryGetValue(current.id, out key);
				if (!flag)
				{
					break;
				}
				TreeViewItemData<T> treeViewItemData = this.m_Tree[key];
				treeViewItemData.ReplaceChild(current);
				this.m_Tree[key] = treeViewItemData;
				current = treeViewItemData;
			}
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x00037124 File Offset: 0x00035324
		private bool TryRemoveChildrenIds(int id)
		{
			TreeViewItemData<T> treeViewItemData;
			bool flag = this.m_Tree.TryGetValue(id, out treeViewItemData) && treeViewItemData.children != null;
			if (flag)
			{
				foreach (TreeViewItemData<T> treeViewItemData2 in treeViewItemData.children)
				{
					this.TryRemoveChildrenIds(treeViewItemData2.id);
				}
			}
			List<int> toRelease;
			bool flag2 = this.m_ChildrenIds.TryGetValue(id, out toRelease);
			if (flag2)
			{
				CollectionPool<List<int>, int>.Release(toRelease);
			}
			bool flag3 = false;
			flag3 |= this.m_RootItemIds.Remove(id);
			flag3 |= this.m_ChildrenIds.Remove(id);
			flag3 |= this.m_ParentIds.Remove(id);
			flag3 |= this.m_Tree.Remove(id);
			return flag3 | this.m_RootItemIds.Remove(id);
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x00037218 File Offset: 0x00035418
		private void RefreshTree(IList<TreeViewItemData<T>> rootItems)
		{
			this.m_Tree.Clear();
			this.m_ParentIds.Clear();
			this.m_ChildrenIds.Clear();
			this.m_RootItemIds.Clear();
			this.BuildTree(rootItems, true);
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00037254 File Offset: 0x00035454
		private void BuildTree(IEnumerable<TreeViewItemData<T>> items, bool isRoot)
		{
			bool flag = items == null;
			if (!flag)
			{
				foreach (TreeViewItemData<T> value in items)
				{
					this.m_Tree.Add(value.id, value);
					if (isRoot)
					{
						this.m_RootItemIds.Add(value.id);
					}
					bool flag2 = value.children != null;
					if (flag2)
					{
						List<int> list;
						bool flag3 = !this.m_ChildrenIds.TryGetValue(value.id, out list);
						if (flag3)
						{
							this.m_ChildrenIds.Add(value.id, list = CollectionPool<List<int>, int>.Get());
						}
						foreach (TreeViewItemData<T> treeViewItemData in value.children)
						{
							this.m_ParentIds.Add(treeViewItemData.id, value.id);
							list.Add(treeViewItemData.id);
						}
						this.BuildTree(value.children, false);
					}
				}
			}
		}

		// Token: 0x04000620 RID: 1568
		private readonly IList<int> m_RootItemIds;

		// Token: 0x04000621 RID: 1569
		private readonly Dictionary<int, TreeViewItemData<T>> m_Tree;

		// Token: 0x04000622 RID: 1570
		private readonly Dictionary<int, int> m_ParentIds;

		// Token: 0x04000623 RID: 1571
		private readonly Dictionary<int, List<int>> m_ChildrenIds;
	}
}
