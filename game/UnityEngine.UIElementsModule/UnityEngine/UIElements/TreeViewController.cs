using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Profiling;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	// Token: 0x02000109 RID: 265
	internal abstract class TreeViewController : CollectionViewController
	{
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x0001EF98 File Offset: 0x0001D198
		protected TreeView treeView
		{
			get
			{
				return base.view as TreeView;
			}
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0001EFA8 File Offset: 0x0001D1A8
		public void RebuildTree()
		{
			this.m_TreeItems.Clear();
			this.m_RootIndices.Clear();
			foreach (int num in this.GetAllItemIds(null))
			{
				int parentId = this.GetParentId(num);
				bool flag = parentId == -1;
				if (flag)
				{
					this.m_RootIndices.Add(num);
				}
				this.m_TreeItems.Add(num, new TreeItem(num, parentId, this.GetChildrenIds(num)));
			}
			this.RegenerateWrappers();
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0001F04C File Offset: 0x0001D24C
		public IEnumerable<int> GetRootItemIds()
		{
			return this.m_RootIndices;
		}

		// Token: 0x06000853 RID: 2131
		public abstract IEnumerable<int> GetAllItemIds(IEnumerable<int> rootIds = null);

		// Token: 0x06000854 RID: 2132
		public abstract int GetParentId(int id);

		// Token: 0x06000855 RID: 2133
		public abstract IEnumerable<int> GetChildrenIds(int id);

		// Token: 0x06000856 RID: 2134
		public abstract void Move(int id, int newParentId, int childIndex = -1, bool rebuildTree = true);

		// Token: 0x06000857 RID: 2135
		public abstract bool TryRemoveItem(int id, bool rebuildTree = true);

		// Token: 0x06000858 RID: 2136 RVA: 0x0001F064 File Offset: 0x0001D264
		internal override void InvokeMakeItem(ReusableCollectionItem reusableItem)
		{
			ReusableTreeViewItem treeItem = reusableItem as ReusableTreeViewItem;
			bool flag = treeItem != null;
			if (flag)
			{
				treeItem.Init(this.MakeItem());
				treeItem.onPointerUp += this.OnItemPointerUp;
				treeItem.onToggleValueChanged += this.ToggleExpandedState;
				bool autoExpand = this.treeView.autoExpand;
				if (autoExpand)
				{
					this.treeView.expandedItemIds.Remove(treeItem.id);
					this.treeView.schedule.Execute(delegate()
					{
						this.ExpandItem(treeItem.id, true, true);
					});
				}
			}
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0001F12C File Offset: 0x0001D32C
		internal override void InvokeBindItem(ReusableCollectionItem reusableItem, int index)
		{
			ReusableTreeViewItem reusableTreeViewItem = reusableItem as ReusableTreeViewItem;
			bool flag = reusableTreeViewItem != null;
			if (flag)
			{
				reusableTreeViewItem.Indent(this.GetIndentationDepthByIndex(index));
				reusableTreeViewItem.SetExpandedWithoutNotify(this.IsExpandedByIndex(index));
				reusableTreeViewItem.SetToggleVisibility(this.HasChildrenByIndex(index));
			}
			base.InvokeBindItem(reusableItem, index);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001F180 File Offset: 0x0001D380
		internal override void InvokeDestroyItem(ReusableCollectionItem reusableItem)
		{
			ReusableTreeViewItem reusableTreeViewItem = reusableItem as ReusableTreeViewItem;
			bool flag = reusableTreeViewItem != null;
			if (flag)
			{
				reusableTreeViewItem.onPointerUp -= this.OnItemPointerUp;
				reusableTreeViewItem.onToggleValueChanged -= this.ToggleExpandedState;
			}
			base.InvokeDestroyItem(reusableItem);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0001F1D0 File Offset: 0x0001D3D0
		protected override void BindItem(VisualElement element, int index)
		{
			bool flag = this.treeView.bindItem == null;
			if (flag)
			{
				bool flag2 = this.treeView.makeItem != null;
				bool flag3 = flag2;
				if (flag3)
				{
					throw new NotImplementedException("You must specify bindItem if makeItem is specified.");
				}
				Label label = (Label)element;
				object itemForIndex = this.GetItemForIndex(index);
				label.text = (((itemForIndex != null) ? itemForIndex.ToString() : null) ?? "null");
			}
			else
			{
				this.treeView.bindItem(element, index);
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0001F254 File Offset: 0x0001D454
		private void OnItemPointerUp(PointerUpEvent evt)
		{
			bool flag = (evt.modifiers & EventModifiers.Alt) == EventModifiers.None;
			if (!flag)
			{
				VisualElement e = evt.currentTarget as VisualElement;
				Toggle toggle = e.Q(TreeView.itemToggleUssClassName, null);
				int index = ((ReusableTreeViewItem)toggle.userData).index;
				int idForIndex = this.GetIdForIndex(index);
				bool flag2 = this.IsExpandedByIndex(index);
				bool flag3 = !this.HasChildrenByIndex(index);
				if (!flag3)
				{
					HashSet<int> hashSet = new HashSet<int>(this.treeView.expandedItemIds);
					bool flag4 = flag2;
					if (flag4)
					{
						hashSet.Remove(idForIndex);
					}
					else
					{
						hashSet.Add(idForIndex);
					}
					IEnumerable<int> childrenIdsByIndex = this.GetChildrenIdsByIndex(index);
					foreach (int num in this.GetAllItemIds(childrenIdsByIndex))
					{
						bool flag5 = this.HasChildren(num);
						if (flag5)
						{
							bool flag6 = flag2;
							if (flag6)
							{
								hashSet.Remove(num);
							}
							else
							{
								hashSet.Add(num);
							}
						}
					}
					this.treeView.expandedItemIds = hashSet.ToList<int>();
					this.RegenerateWrappers();
					this.treeView.RefreshItems();
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0001F3A0 File Offset: 0x0001D5A0
		private void ToggleExpandedState(ChangeEvent<bool> evt)
		{
			Toggle toggle = evt.target as Toggle;
			int index = ((ReusableTreeViewItem)toggle.userData).index;
			bool flag = this.IsExpandedByIndex(index);
			bool flag2 = flag;
			if (flag2)
			{
				this.CollapseItemByIndex(index, false);
			}
			else
			{
				this.ExpandItemByIndex(index, false, true);
			}
			this.treeView.scrollView.contentContainer.Focus();
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001F404 File Offset: 0x0001D604
		public override int GetItemsCount()
		{
			List<TreeViewItemWrapper> itemWrappers = this.m_ItemWrappers;
			return (itemWrappers != null) ? itemWrappers.Count : 0;
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0001F428 File Offset: 0x0001D628
		public virtual int GetTreeCount()
		{
			return this.m_TreeItems.Count;
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0001F448 File Offset: 0x0001D648
		public override int GetIndexForId(int id)
		{
			bool flag = this.m_TreeItemIdsWithItemWrappers.Contains(id);
			if (flag)
			{
				for (int i = 0; i < this.m_ItemWrappers.Count; i++)
				{
					bool flag2 = this.m_ItemWrappers[i].id == id;
					if (flag2)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0001F4B0 File Offset: 0x0001D6B0
		public override int GetIdForIndex(int index)
		{
			return this.IsIndexValid(index) ? this.m_ItemWrappers[index].id : -1;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0001F4E4 File Offset: 0x0001D6E4
		public bool Exists(int id)
		{
			return this.m_TreeItems.ContainsKey(id);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0001F504 File Offset: 0x0001D704
		public virtual bool HasChildren(int id)
		{
			TreeItem treeItem;
			bool flag = this.m_TreeItems.TryGetValue(id, out treeItem);
			return flag && treeItem.hasChildren;
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0001F534 File Offset: 0x0001D734
		public bool HasChildrenByIndex(int index)
		{
			return this.IsIndexValid(index) && this.m_ItemWrappers[index].hasChildren;
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0001F568 File Offset: 0x0001D768
		public IEnumerable<int> GetChildrenIdsByIndex(int index)
		{
			return this.IsIndexValid(index) ? this.m_ItemWrappers[index].childrenIds : null;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0001F59C File Offset: 0x0001D79C
		public int GetChildIndexForId(int id)
		{
			TreeItem treeItem;
			bool flag = !this.m_TreeItems.TryGetValue(id, out treeItem);
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				int num = 0;
				TreeItem treeItem2;
				IEnumerable<int> enumerable;
				if (!this.m_TreeItems.TryGetValue(treeItem.parentId, out treeItem2))
				{
					IEnumerable<int> rootIndices = this.m_RootIndices;
					enumerable = rootIndices;
				}
				else
				{
					enumerable = treeItem2.childrenIds;
				}
				IEnumerable<int> enumerable2 = enumerable;
				foreach (int num2 in enumerable2)
				{
					bool flag2 = num2 == id;
					if (flag2)
					{
						return num;
					}
					num++;
				}
				result = -1;
			}
			return result;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0001F64C File Offset: 0x0001D84C
		public int GetIndentationDepth(int id)
		{
			int num = 0;
			int parentId = this.GetParentId(id);
			while (parentId != -1)
			{
				parentId = this.GetParentId(parentId);
				num++;
			}
			return num;
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0001F684 File Offset: 0x0001D884
		public int GetIndentationDepthByIndex(int index)
		{
			int idForIndex = this.GetIdForIndex(index);
			return this.GetIndentationDepth(idForIndex);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0001F6A8 File Offset: 0x0001D8A8
		public bool IsExpanded(int id)
		{
			return this.treeView.expandedItemIds.Contains(id);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0001F6CC File Offset: 0x0001D8CC
		public bool IsExpandedByIndex(int index)
		{
			bool flag = !this.IsIndexValid(index);
			return !flag && this.IsExpanded(this.m_ItemWrappers[index].id);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0001F70C File Offset: 0x0001D90C
		public void ExpandItemByIndex(int index, bool expandAllChildren, bool refresh = true)
		{
			using (TreeViewController.K_ExpandItemByIndex.Auto())
			{
				bool flag = !this.HasChildrenByIndex(index);
				if (!flag)
				{
					int idForIndex = this.GetIdForIndex(index);
					bool flag2 = !this.treeView.expandedItemIds.Contains(idForIndex) || expandAllChildren;
					if (flag2)
					{
						IEnumerable<int> childrenIdsByIndex = this.GetChildrenIdsByIndex(index);
						List<int> list = new List<int>();
						foreach (int item in childrenIdsByIndex)
						{
							bool flag3 = !this.m_TreeItemIdsWithItemWrappers.Contains(item);
							if (flag3)
							{
								list.Add(item);
							}
						}
						this.CreateWrappers(list, this.GetIndentationDepth(idForIndex) + 1, ref this.m_WrapperInsertionList);
						this.m_ItemWrappers.InsertRange(index + 1, this.m_WrapperInsertionList);
						bool flag4 = !this.treeView.expandedItemIds.Contains(this.m_ItemWrappers[index].id);
						if (flag4)
						{
							this.treeView.expandedItemIds.Add(this.m_ItemWrappers[index].id);
						}
						this.m_WrapperInsertionList.Clear();
					}
					if (expandAllChildren)
					{
						IEnumerable<int> childrenIds = this.GetChildrenIds(idForIndex);
						foreach (int num in this.GetAllItemIds(childrenIds))
						{
							bool flag5 = !this.treeView.expandedItemIds.Contains(num);
							if (flag5)
							{
								this.ExpandItemByIndex(this.GetIndexForId(num), true, false);
							}
						}
					}
					if (refresh)
					{
						this.treeView.RefreshItems();
					}
				}
			}
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0001F934 File Offset: 0x0001DB34
		public void ExpandItem(int id, bool expandAllChildren, bool refresh = true)
		{
			bool flag = !this.HasChildren(id);
			if (!flag)
			{
				for (int i = 0; i < this.m_ItemWrappers.Count; i++)
				{
					bool flag2 = this.m_ItemWrappers[i].id == id;
					if (flag2)
					{
						bool flag3 = expandAllChildren || !this.IsExpandedByIndex(i);
						if (flag3)
						{
							this.ExpandItemByIndex(i, expandAllChildren, refresh);
							return;
						}
					}
				}
				bool flag4 = this.treeView.expandedItemIds.Contains(id);
				if (!flag4)
				{
					this.treeView.expandedItemIds.Add(id);
				}
			}
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0001F9DC File Offset: 0x0001DBDC
		public void CollapseItemByIndex(int index, bool collapseAllChildren)
		{
			bool flag = !this.HasChildrenByIndex(index);
			if (!flag)
			{
				if (collapseAllChildren)
				{
					int idForIndex = this.GetIdForIndex(index);
					IEnumerable<int> childrenIds = this.GetChildrenIds(idForIndex);
					foreach (int item in this.GetAllItemIds(childrenIds))
					{
						this.treeView.expandedItemIds.Remove(item);
					}
				}
				this.treeView.expandedItemIds.Remove(this.GetIdForIndex(index));
				int num = 0;
				int num2 = index + 1;
				int indentationDepthByIndex = this.GetIndentationDepthByIndex(index);
				while (num2 < this.m_ItemWrappers.Count && this.GetIndentationDepthByIndex(num2) > indentationDepthByIndex)
				{
					num++;
					num2++;
				}
				int num3 = index + 1 + num;
				for (int i = index + 1; i < num3; i++)
				{
					this.m_TreeItemIdsWithItemWrappers.Remove(this.m_ItemWrappers[i].id);
				}
				this.m_ItemWrappers.RemoveRange(index + 1, num);
				this.treeView.RefreshItems();
			}
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0001FB24 File Offset: 0x0001DD24
		public void CollapseItem(int id, bool collapseAllChildren)
		{
			for (int i = 0; i < this.m_ItemWrappers.Count; i++)
			{
				bool flag = this.m_ItemWrappers[i].id == id;
				if (flag)
				{
					bool flag2 = this.IsExpandedByIndex(i);
					if (flag2)
					{
						this.CollapseItemByIndex(i, collapseAllChildren);
						return;
					}
				}
			}
			bool flag3 = !this.treeView.expandedItemIds.Contains(id);
			if (flag3)
			{
				return;
			}
			this.treeView.expandedItemIds.Remove(id);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0001FBB0 File Offset: 0x0001DDB0
		public void ExpandAll()
		{
			foreach (int item in this.GetAllItemIds(null))
			{
				bool flag = !this.treeView.expandedItemIds.Contains(item);
				if (flag)
				{
					this.treeView.expandedItemIds.Add(item);
				}
			}
			this.RegenerateWrappers();
			this.treeView.RefreshItems();
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0001FC38 File Offset: 0x0001DE38
		public void CollapseAll()
		{
			bool flag = this.treeView.expandedItemIds.Count == 0;
			if (!flag)
			{
				this.treeView.expandedItemIds.Clear();
				this.RegenerateWrappers();
				this.treeView.RefreshItems();
			}
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0001FC84 File Offset: 0x0001DE84
		internal void RegenerateWrappers()
		{
			this.m_ItemWrappers.Clear();
			this.m_TreeItemIdsWithItemWrappers.Clear();
			IEnumerable<int> rootItemIds = this.GetRootItemIds();
			bool flag = rootItemIds == null;
			if (!flag)
			{
				this.CreateWrappers(rootItemIds, 0, ref this.m_ItemWrappers);
				base.SetItemsSourceWithoutNotify(this.m_ItemWrappers);
			}
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0001FCD8 File Offset: 0x0001DED8
		private void CreateWrappers(IEnumerable<int> treeViewItemIds, int depth, ref List<TreeViewItemWrapper> wrappers)
		{
			using (TreeViewController.k_CreateWrappers.Auto())
			{
				bool flag = treeViewItemIds == null || wrappers == null || this.m_TreeItemIdsWithItemWrappers == null;
				if (!flag)
				{
					foreach (int num in treeViewItemIds)
					{
						TreeItem item;
						bool flag2 = !this.m_TreeItems.TryGetValue(num, out item);
						if (!flag2)
						{
							TreeViewItemWrapper item2 = new TreeViewItemWrapper(item, depth);
							wrappers.Add(item2);
							this.m_TreeItemIdsWithItemWrappers.Add(num);
							TreeView treeView = this.treeView;
							bool flag3 = ((treeView != null) ? treeView.expandedItemIds : null) == null;
							if (!flag3)
							{
								bool flag4 = this.treeView.expandedItemIds.Contains(item2.id) && item2.hasChildren;
								if (flag4)
								{
									this.CreateWrappers(this.GetChildrenIds(item2.id), depth + 1, ref wrappers);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0001FE0C File Offset: 0x0001E00C
		private bool IsIndexValid(int index)
		{
			return index >= 0 && index < this.m_ItemWrappers.Count;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0001FE33 File Offset: 0x0001E033
		internal void RaiseItemParentChanged(int id, int newParentId)
		{
			base.RaiseItemIndexChanged(id, newParentId);
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0001FE3F File Offset: 0x0001E03F
		protected TreeViewController()
		{
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0001FE7F File Offset: 0x0001E07F
		// Note: this type is marked as 'beforefieldinit'.
		static TreeViewController()
		{
		}

		// Token: 0x0400036B RID: 875
		private Dictionary<int, TreeItem> m_TreeItems = new Dictionary<int, TreeItem>();

		// Token: 0x0400036C RID: 876
		private List<int> m_RootIndices = new List<int>();

		// Token: 0x0400036D RID: 877
		private List<TreeViewItemWrapper> m_ItemWrappers = new List<TreeViewItemWrapper>();

		// Token: 0x0400036E RID: 878
		private HashSet<int> m_TreeItemIdsWithItemWrappers = new HashSet<int>();

		// Token: 0x0400036F RID: 879
		private List<TreeViewItemWrapper> m_WrapperInsertionList = new List<TreeViewItemWrapper>();

		// Token: 0x04000370 RID: 880
		private static readonly ProfilerMarker K_ExpandItemByIndex = new ProfilerMarker(ProfilerCategory.Scripts, "BaseTreeViewController.ExpandItemByIndex");

		// Token: 0x04000371 RID: 881
		private static readonly ProfilerMarker k_CreateWrappers = new ProfilerMarker("BaseTreeViewController.CreateWrappers");

		// Token: 0x0200010A RID: 266
		[CompilerGenerated]
		private sealed class <>c__DisplayClass14_0
		{
			// Token: 0x06000877 RID: 2167 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c__DisplayClass14_0()
			{
			}

			// Token: 0x06000878 RID: 2168 RVA: 0x0001FEA4 File Offset: 0x0001E0A4
			internal void <InvokeMakeItem>b__0()
			{
				this.<>4__this.ExpandItem(this.treeItem.id, true, true);
			}

			// Token: 0x04000372 RID: 882
			public TreeViewController <>4__this;

			// Token: 0x04000373 RID: 883
			public ReusableTreeViewItem treeItem;
		}
	}
}
