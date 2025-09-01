using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Pool;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	// Token: 0x020001BA RID: 442
	internal class TreeViewReorderableDragAndDropController : BaseReorderableDragAndDropController
	{
		// Token: 0x06000E73 RID: 3699 RVA: 0x0003CB52 File Offset: 0x0003AD52
		public TreeViewReorderableDragAndDropController(TreeView view) : base(view)
		{
			this.m_TreeView = view;
			this.enableReordering = true;
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x0003CB78 File Offset: 0x0003AD78
		protected override int CompareId(int id1, int id2)
		{
			bool flag = id1 == id2;
			int result;
			if (flag)
			{
				result = id1.CompareTo(id2);
			}
			else
			{
				int num = id1;
				int num2 = id2;
				List<int> list;
				using (CollectionPool<List<int>, int>.Get(out list))
				{
					while (num != -1)
					{
						list.Add(num);
						num = this.m_TreeView.viewController.GetParentId(num);
					}
					List<int> list2;
					using (CollectionPool<List<int>, int>.Get(out list2))
					{
						while (num2 != -1)
						{
							list2.Add(num2);
							num2 = this.m_TreeView.viewController.GetParentId(num2);
						}
						list.Add(-1);
						list2.Add(-1);
						int i = 0;
						while (i < list.Count)
						{
							int item = list[i];
							int num3 = list2.IndexOf(item);
							bool flag2 = num3 >= 0;
							if (flag2)
							{
								bool flag3 = i == 0;
								if (flag3)
								{
									return -1;
								}
								int id3 = (i > 0) ? list[i - 1] : id1;
								int id4 = (num3 > 0) ? list2[num3 - 1] : id2;
								int childIndexForId = this.m_TreeView.viewController.GetChildIndexForId(id3);
								int childIndexForId2 = this.m_TreeView.viewController.GetChildIndexForId(id4);
								return childIndexForId.CompareTo(childIndexForId2);
							}
							else
							{
								i++;
							}
						}
						throw new ArgumentOutOfRangeException("[UI Toolkit] Trying to reorder ids that are not in the same tree.");
					}
				}
			}
			return result;
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0003CD34 File Offset: 0x0003AF34
		public override StartDragArgs SetupDragAndDrop(IEnumerable<int> itemIds, bool skipText = false)
		{
			StartDragArgs result = base.SetupDragAndDrop(itemIds, skipText);
			this.m_DropData.draggedIds = base.GetSortedSelectedIds().ToArray<int>();
			return result;
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0003CD68 File Offset: 0x0003AF68
		public override DragVisualMode HandleDragAndDrop(IListDragAndDropArgs args)
		{
			bool flag = !this.enableReordering;
			DragVisualMode result;
			if (flag)
			{
				result = DragVisualMode.Rejected;
			}
			else
			{
				result = ((args.dragAndDropData.userData == this.m_TreeView) ? DragVisualMode.Move : DragVisualMode.Rejected);
			}
			return result;
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0003CDA4 File Offset: 0x0003AFA4
		public override void OnDrop(IListDragAndDropArgs args)
		{
			int parentId = args.parentId;
			int childIndex = args.childIndex;
			int num = 0;
			bool flag = args.dragAndDropPosition == DragAndDropPosition.OverItem || (parentId == -1 && childIndex == -1);
			List<TreeViewReorderableDragAndDropController.TreeItemState> list;
			using (CollectionPool<List<TreeViewReorderableDragAndDropController.TreeItemState>, TreeViewReorderableDragAndDropController.TreeItemState>.Get(out list))
			{
				foreach (int id in this.m_DropData.draggedIds)
				{
					int parentId2 = this.m_TreeView.viewController.GetParentId(id);
					int childIndexForId = this.m_TreeView.viewController.GetChildIndexForId(id);
					list.Add(new TreeViewReorderableDragAndDropController.TreeItemState(parentId2, childIndexForId));
					bool flag2 = flag;
					if (flag2)
					{
						this.m_TreeView.viewController.Move(id, parentId, -1, false);
					}
					else
					{
						int childIndex2 = childIndex + num;
						bool flag3 = parentId2 != parentId || childIndexForId >= childIndex;
						if (flag3)
						{
							num++;
						}
						this.m_TreeView.viewController.Move(id, parentId, childIndex2, false);
					}
				}
				bool flag4 = args.dragAndDropPosition == DragAndDropPosition.OverItem;
				if (flag4)
				{
					this.m_TreeView.viewController.ExpandItem(parentId, false, false);
				}
				this.m_TreeView.viewController.RebuildTree();
				this.m_TreeView.RefreshItems();
				for (int j = 0; j < this.m_DropData.draggedIds.Length; j++)
				{
					int id2 = this.m_DropData.draggedIds[j];
					TreeViewReorderableDragAndDropController.TreeItemState treeItemState = list[j];
					int parentId3 = this.m_TreeView.viewController.GetParentId(id2);
					int childIndexForId2 = this.m_TreeView.viewController.GetChildIndexForId(id2);
					bool flag5 = treeItemState.parentId == parentId3 && treeItemState.childIndex == childIndexForId2;
					if (!flag5)
					{
						this.m_TreeView.viewController.RaiseItemParentChanged(id2, parentId);
					}
				}
			}
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0003CFB4 File Offset: 0x0003B1B4
		public override void DragCleanup()
		{
			bool flag = this.m_DropData != null;
			if (flag)
			{
				this.m_DropData = new TreeViewReorderableDragAndDropController.DropData();
			}
		}

		// Token: 0x040006BA RID: 1722
		protected TreeViewReorderableDragAndDropController.DropData m_DropData = new TreeViewReorderableDragAndDropController.DropData();

		// Token: 0x040006BB RID: 1723
		protected readonly TreeView m_TreeView;

		// Token: 0x020001BB RID: 443
		protected class DropData
		{
			// Token: 0x06000E79 RID: 3705 RVA: 0x000020C2 File Offset: 0x000002C2
			public DropData()
			{
			}

			// Token: 0x040006BC RID: 1724
			public int[] draggedIds;
		}

		// Token: 0x020001BC RID: 444
		private struct TreeItemState
		{
			// Token: 0x06000E7A RID: 3706 RVA: 0x0003CFDC File Offset: 0x0003B1DC
			public TreeItemState(int parentId, int childIndex)
			{
				this.parentId = parentId;
				this.childIndex = childIndex;
			}

			// Token: 0x040006BD RID: 1725
			public int parentId;

			// Token: 0x040006BE RID: 1726
			public int childIndex;
		}
	}
}
