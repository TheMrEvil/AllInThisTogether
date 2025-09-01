using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000115 RID: 277
	internal class ReusableListViewItem : ReusableCollectionItem
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00022E44 File Offset: 0x00021044
		public override VisualElement rootElement
		{
			get
			{
				return this.m_Container ?? base.bindableElement;
			}
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00022E56 File Offset: 0x00021056
		public void Init(VisualElement item, bool usesAnimatedDragger)
		{
			base.Init(item);
			this.UpdateHierarchy(usesAnimatedDragger);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x00022E6C File Offset: 0x0002106C
		private void UpdateHierarchy(bool usesAnimatedDragger)
		{
			if (usesAnimatedDragger)
			{
				bool flag = this.m_Container != null;
				if (!flag)
				{
					this.m_Container = new VisualElement
					{
						name = ListView.reorderableItemUssClassName
					};
					this.m_Container.AddToClassList(ListView.reorderableItemUssClassName);
					this.m_DragHandle = new VisualElement
					{
						name = ListView.reorderableItemHandleUssClassName
					};
					this.m_DragHandle.AddToClassList(ListView.reorderableItemHandleUssClassName);
					VisualElement visualElement = new VisualElement
					{
						name = ListView.reorderableItemHandleBarUssClassName
					};
					visualElement.AddToClassList(ListView.reorderableItemHandleBarUssClassName);
					this.m_DragHandle.Add(visualElement);
					VisualElement visualElement2 = new VisualElement
					{
						name = ListView.reorderableItemHandleBarUssClassName
					};
					visualElement2.AddToClassList(ListView.reorderableItemHandleBarUssClassName);
					this.m_DragHandle.Add(visualElement2);
					this.m_ItemContainer = new VisualElement
					{
						name = ListView.reorderableItemContainerUssClassName
					};
					this.m_ItemContainer.AddToClassList(ListView.reorderableItemContainerUssClassName);
					this.m_ItemContainer.Add(base.bindableElement);
					this.m_Container.Add(this.m_DragHandle);
					this.m_Container.Add(this.m_ItemContainer);
				}
			}
			else
			{
				bool flag2 = this.m_Container == null;
				if (!flag2)
				{
					this.m_Container.RemoveFromHierarchy();
					this.m_Container = null;
				}
			}
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00022FC4 File Offset: 0x000211C4
		public void UpdateDragHandle(bool needsDragHandle)
		{
			if (needsDragHandle)
			{
				bool flag = this.m_DragHandle.parent == null;
				if (flag)
				{
					this.rootElement.Insert(0, this.m_DragHandle);
					this.rootElement.AddToClassList(ListView.reorderableItemUssClassName);
				}
			}
			else
			{
				VisualElement dragHandle = this.m_DragHandle;
				bool flag2 = ((dragHandle != null) ? dragHandle.parent : null) != null;
				if (flag2)
				{
					this.m_DragHandle.RemoveFromHierarchy();
					this.rootElement.RemoveFromClassList(ListView.reorderableItemUssClassName);
				}
			}
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0002304D File Offset: 0x0002124D
		public override void PreAttachElement()
		{
			base.PreAttachElement();
			this.rootElement.AddToClassList(ListView.itemUssClassName);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00023068 File Offset: 0x00021268
		public override void DetachElement()
		{
			base.DetachElement();
			this.rootElement.RemoveFromClassList(ListView.itemUssClassName);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00023084 File Offset: 0x00021284
		public override void SetDragGhost(bool dragGhost)
		{
			base.SetDragGhost(dragGhost);
			bool flag = this.m_DragHandle != null;
			if (flag)
			{
				this.m_DragHandle.style.display = (base.isDragGhost ? DisplayStyle.None : DisplayStyle.Flex);
			}
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x000230CB File Offset: 0x000212CB
		public ReusableListViewItem()
		{
		}

		// Token: 0x040003AE RID: 942
		private VisualElement m_Container;

		// Token: 0x040003AF RID: 943
		private VisualElement m_DragHandle;

		// Token: 0x040003B0 RID: 944
		private VisualElement m_ItemContainer;
	}
}
