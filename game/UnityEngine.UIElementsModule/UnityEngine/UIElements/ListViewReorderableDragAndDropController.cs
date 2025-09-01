using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001B9 RID: 441
	internal class ListViewReorderableDragAndDropController : BaseReorderableDragAndDropController
	{
		// Token: 0x06000E70 RID: 3696 RVA: 0x0003C9CE File Offset: 0x0003ABCE
		public ListViewReorderableDragAndDropController(ListView view) : base(view)
		{
			this.m_ListView = view;
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x0003C9E0 File Offset: 0x0003ABE0
		public override DragVisualMode HandleDragAndDrop(IListDragAndDropArgs args)
		{
			bool flag = args.dragAndDropPosition == DragAndDropPosition.OverItem || !this.enableReordering;
			DragVisualMode result;
			if (flag)
			{
				result = DragVisualMode.Rejected;
			}
			else
			{
				result = ((args.dragAndDropData.userData == this.m_ListView) ? DragVisualMode.Move : DragVisualMode.Rejected);
			}
			return result;
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x0003CA28 File Offset: 0x0003AC28
		public override void OnDrop(IListDragAndDropArgs args)
		{
			int insertAtIndex = args.insertAtIndex;
			int num = 0;
			int num2 = 0;
			for (int i = this.m_SortedSelectedIds.Count - 1; i >= 0; i--)
			{
				int id = this.m_SortedSelectedIds[i];
				int num3 = this.m_View.viewController.GetIndexForId(id);
				bool flag = num3 < 0;
				if (!flag)
				{
					int num4 = insertAtIndex - num;
					bool flag2 = num3 >= insertAtIndex;
					if (flag2)
					{
						num3 += num2;
						num2++;
					}
					else
					{
						bool flag3 = num3 < num4;
						if (flag3)
						{
							num++;
							num4--;
						}
					}
					this.m_ListView.viewController.Move(num3, num4);
				}
			}
			bool flag4 = this.m_ListView.selectionType > SelectionType.None;
			if (flag4)
			{
				List<int> list = new List<int>();
				for (int j = 0; j < this.m_SortedSelectedIds.Count; j++)
				{
					list.Add(insertAtIndex - num + j);
				}
				this.m_ListView.SetSelectionWithoutNotify(list);
			}
			else
			{
				this.m_ListView.ClearSelection();
			}
		}

		// Token: 0x040006B9 RID: 1721
		protected readonly ListView m_ListView;
	}
}
