using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	// Token: 0x020001B8 RID: 440
	internal class ListViewDraggerAnimated : ListViewDragger
	{
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x0003C1D0 File Offset: 0x0003A3D0
		// (set) Token: 0x06000E66 RID: 3686 RVA: 0x0003C1D8 File Offset: 0x0003A3D8
		public bool isDragging
		{
			[CompilerGenerated]
			get
			{
				return this.<isDragging>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<isDragging>k__BackingField = value;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000E67 RID: 3687 RVA: 0x0003C1E1 File Offset: 0x0003A3E1
		public ReusableCollectionItem draggedItem
		{
			get
			{
				return this.m_Item;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x00004E8A File Offset: 0x0000308A
		protected override bool supportsDragEvents
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0003C1E9 File Offset: 0x0003A3E9
		public ListViewDraggerAnimated(BaseVerticalCollectionView listView) : base(listView)
		{
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x0003C1F4 File Offset: 0x0003A3F4
		protected internal override StartDragArgs StartDrag(Vector3 pointerPosition)
		{
			base.targetView.ClearSelection();
			ReusableCollectionItem recycledItem = base.GetRecycledItem(pointerPosition);
			bool flag = recycledItem == null;
			StartDragArgs result;
			if (flag)
			{
				result = default(StartDragArgs);
			}
			else
			{
				base.targetView.SetSelection(recycledItem.index);
				this.isDragging = true;
				this.m_Item = recycledItem;
				base.targetView.virtualizationController.StartDragItem(this.m_Item);
				float y = this.m_Item.rootElement.layout.y;
				this.m_SelectionHeight = this.m_Item.rootElement.layout.height;
				this.m_Item.rootElement.style.position = Position.Absolute;
				this.m_Item.rootElement.style.height = this.m_Item.rootElement.layout.height;
				this.m_Item.rootElement.style.width = this.m_Item.rootElement.layout.width;
				this.m_Item.rootElement.style.top = y;
				this.m_DragStartIndex = this.m_Item.index;
				this.m_CurrentIndex = this.m_DragStartIndex;
				this.m_CurrentPointerPosition = pointerPosition;
				this.m_LocalOffsetOnStart = base.targetScrollView.contentContainer.WorldToLocal(pointerPosition).y - y;
				ReusableCollectionItem recycledItemFromIndex = base.targetView.GetRecycledItemFromIndex(this.m_CurrentIndex + 1);
				bool flag2 = recycledItemFromIndex != null;
				if (flag2)
				{
					this.m_OffsetItem = recycledItemFromIndex;
					this.Animate(this.m_OffsetItem, this.m_SelectionHeight);
					this.m_OffsetItem.rootElement.style.paddingTop = this.m_SelectionHeight;
					bool flag3 = base.targetView.virtualizationMethod == CollectionVirtualizationMethod.FixedHeight;
					if (flag3)
					{
						this.m_OffsetItem.rootElement.style.height = base.targetView.fixedItemHeight + this.m_SelectionHeight;
					}
				}
				result = base.dragAndDropController.SetupDragAndDrop(new int[]
				{
					this.m_Item.index
				}, true);
			}
			return result;
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x0003C44C File Offset: 0x0003A64C
		protected internal override void UpdateDrag(Vector3 pointerPosition)
		{
			bool flag = this.m_Item == null;
			if (!flag)
			{
				base.HandleDragAndScroll(pointerPosition);
				this.m_CurrentPointerPosition = pointerPosition;
				Vector2 vector = base.targetScrollView.contentContainer.WorldToLocal(this.m_CurrentPointerPosition);
				Rect layout = this.m_Item.rootElement.layout;
				float height = base.targetScrollView.contentContainer.layout.height;
				layout.y = Mathf.Clamp(vector.y - this.m_LocalOffsetOnStart, 0f, height - this.m_SelectionHeight);
				float num = base.targetScrollView.contentContainer.resolvedStyle.paddingTop;
				this.m_CurrentIndex = -1;
				foreach (ReusableCollectionItem reusableCollectionItem in base.targetView.activeItems)
				{
					bool flag2 = reusableCollectionItem.index < 0 || (reusableCollectionItem.rootElement.style.display == DisplayStyle.None && !reusableCollectionItem.isDragGhost);
					if (!flag2)
					{
						bool flag3 = reusableCollectionItem.index == this.m_Item.index && reusableCollectionItem.index < base.targetView.itemsSource.Count - 1;
						if (flag3)
						{
							float expectedItemHeight = base.targetView.virtualizationController.GetExpectedItemHeight(reusableCollectionItem.index + 1);
							bool flag4 = layout.y <= num + expectedItemHeight * 0.5f;
							if (flag4)
							{
								this.m_CurrentIndex = reusableCollectionItem.index;
							}
						}
						else
						{
							float expectedItemHeight2 = base.targetView.virtualizationController.GetExpectedItemHeight(reusableCollectionItem.index);
							bool flag5 = layout.y <= num + expectedItemHeight2 * 0.5f;
							if (flag5)
							{
								bool flag6 = this.m_CurrentIndex == -1;
								if (flag6)
								{
									this.m_CurrentIndex = reusableCollectionItem.index;
								}
								bool flag7 = this.m_OffsetItem == reusableCollectionItem;
								if (flag7)
								{
									break;
								}
								this.Animate(this.m_OffsetItem, 0f);
								this.Animate(reusableCollectionItem, this.m_SelectionHeight);
								this.m_OffsetItem = reusableCollectionItem;
								break;
							}
							else
							{
								num += expectedItemHeight2;
							}
						}
					}
				}
				bool flag8 = this.m_CurrentIndex == -1;
				if (flag8)
				{
					this.m_CurrentIndex = base.targetView.itemsSource.Count;
					this.Animate(this.m_OffsetItem, 0f);
					this.m_OffsetItem = null;
				}
				this.m_Item.rootElement.layout = layout;
				this.m_Item.rootElement.BringToFront();
			}
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x0003C728 File Offset: 0x0003A928
		private void Animate(ReusableCollectionItem element, float paddingTop)
		{
			bool flag = element == null;
			if (!flag)
			{
				bool flag2 = element.animator != null;
				if (flag2)
				{
					bool flag3 = (element.animator.isRunning && element.animator.to.paddingTop == paddingTop) || (!element.animator.isRunning && element.rootElement.style.paddingTop == paddingTop);
					if (flag3)
					{
						return;
					}
				}
				ValueAnimation<StyleValues> animator = element.animator;
				if (animator != null)
				{
					animator.Stop();
				}
				ValueAnimation<StyleValues> animator2 = element.animator;
				if (animator2 != null)
				{
					animator2.Recycle();
				}
				StyleValues to = (base.targetView.virtualizationMethod == CollectionVirtualizationMethod.FixedHeight) ? new StyleValues
				{
					paddingTop = paddingTop,
					height = base.targetView.ResolveItemHeight(-1f) + paddingTop
				} : new StyleValues
				{
					paddingTop = paddingTop
				};
				element.animator = element.rootElement.experimental.animation.Start(to, 500);
				element.animator.KeepAlive();
			}
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x0003C850 File Offset: 0x0003AA50
		protected internal override void OnDrop(Vector3 pointerPosition)
		{
			this.isDragging = false;
			this.m_Item.rootElement.ClearManualLayout();
			base.targetView.virtualizationController.EndDrag(this.m_CurrentIndex);
			bool flag = this.m_OffsetItem != null;
			if (flag)
			{
				ValueAnimation<StyleValues> animator = this.m_OffsetItem.animator;
				if (animator != null)
				{
					animator.Stop();
				}
				ValueAnimation<StyleValues> animator2 = this.m_OffsetItem.animator;
				if (animator2 != null)
				{
					animator2.Recycle();
				}
				this.m_OffsetItem.animator = null;
				this.m_OffsetItem.rootElement.style.paddingTop = 0f;
				bool flag2 = base.targetView.virtualizationMethod == CollectionVirtualizationMethod.FixedHeight;
				if (flag2)
				{
					this.m_OffsetItem.rootElement.style.height = base.targetView.ResolveItemHeight(-1f);
				}
			}
			ListViewDragger.DragPosition dragPosition = new ListViewDragger.DragPosition
			{
				recycledItem = this.m_Item,
				insertAtIndex = this.m_CurrentIndex,
				dropPosition = DragAndDropPosition.BetweenItems
			};
			DragAndDropArgs dragAndDropArgs = base.MakeDragAndDropArgs(dragPosition);
			base.dragAndDropController.OnDrop(dragAndDropArgs);
			base.dragAndDrop.AcceptDrag();
			this.m_Item = null;
			this.m_OffsetItem = null;
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x00002166 File Offset: 0x00000366
		protected override void ClearDragAndDropUI(bool dragCancelled)
		{
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0003C99C File Offset: 0x0003AB9C
		protected override bool TryGetDragPosition(Vector2 pointerPosition, ref ListViewDragger.DragPosition dragPosition)
		{
			dragPosition.recycledItem = this.m_Item;
			dragPosition.insertAtIndex = this.m_CurrentIndex;
			dragPosition.dropPosition = DragAndDropPosition.BetweenItems;
			return true;
		}

		// Token: 0x040006B1 RID: 1713
		private int m_DragStartIndex;

		// Token: 0x040006B2 RID: 1714
		private int m_CurrentIndex;

		// Token: 0x040006B3 RID: 1715
		private float m_SelectionHeight;

		// Token: 0x040006B4 RID: 1716
		private float m_LocalOffsetOnStart;

		// Token: 0x040006B5 RID: 1717
		private Vector3 m_CurrentPointerPosition;

		// Token: 0x040006B6 RID: 1718
		private ReusableCollectionItem m_Item;

		// Token: 0x040006B7 RID: 1719
		private ReusableCollectionItem m_OffsetItem;

		// Token: 0x040006B8 RID: 1720
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <isDragging>k__BackingField;
	}
}
