using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	// Token: 0x020001B5 RID: 437
	internal class ListViewDragger : DragEventsProcessor
	{
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x0003AEC8 File Offset: 0x000390C8
		protected BaseVerticalCollectionView targetView
		{
			get
			{
				return this.m_Target as BaseVerticalCollectionView;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x0003AEE8 File Offset: 0x000390E8
		protected ScrollView targetScrollView
		{
			get
			{
				return this.targetView.scrollView;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x0003AF05 File Offset: 0x00039105
		// (set) Token: 0x06000E4C RID: 3660 RVA: 0x0003AF0D File Offset: 0x0003910D
		public ICollectionDragAndDropController dragAndDropController
		{
			[CompilerGenerated]
			get
			{
				return this.<dragAndDropController>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<dragAndDropController>k__BackingField = value;
			}
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0003AF16 File Offset: 0x00039116
		public ListViewDragger(BaseVerticalCollectionView listView) : base(listView)
		{
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0003AF38 File Offset: 0x00039138
		protected override bool CanStartDrag(Vector3 pointerPosition)
		{
			bool flag = this.dragAndDropController == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = !this.targetScrollView.contentContainer.worldBound.Contains(pointerPosition);
				if (flag2)
				{
					result = false;
				}
				else
				{
					ReusableCollectionItem recycledItem = this.GetRecycledItem(pointerPosition);
					bool flag3 = this.targetView.selectedIds.Any<int>();
					if (flag3)
					{
						result = this.dragAndDropController.CanStartDrag(this.targetView.selectedIds);
					}
					else
					{
						result = (recycledItem != null && this.dragAndDropController.CanStartDrag(new int[]
						{
							recycledItem.id
						}));
					}
				}
			}
			return result;
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0003AFDC File Offset: 0x000391DC
		protected internal override StartDragArgs StartDrag(Vector3 pointerPosition)
		{
			ReusableCollectionItem recycledItem = this.GetRecycledItem(pointerPosition);
			bool flag = recycledItem != null;
			IEnumerable<int> itemIndices;
			if (flag)
			{
				bool flag2 = !this.targetView.selectedIndices.Contains(recycledItem.index);
				if (flag2)
				{
					this.targetView.SetSelection(recycledItem.index);
				}
				itemIndices = this.targetView.selectedIds;
			}
			else
			{
				IEnumerable<int> enumerable;
				if (!this.targetView.selectedIds.Any<int>())
				{
					enumerable = Enumerable.Empty<int>();
				}
				else
				{
					IEnumerable<int> selectedIds = this.targetView.selectedIds;
					enumerable = selectedIds;
				}
				itemIndices = enumerable;
			}
			StartDragArgs result = this.dragAndDropController.SetupDragAndDrop(itemIndices, false);
			result.SetGenericData("__unity-drag-and-drop__source-view", this.targetView);
			return result;
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x0003B090 File Offset: 0x00039290
		protected internal override void UpdateDrag(Vector3 pointerPosition)
		{
			ListViewDragger.DragPosition dragPosition = default(ListViewDragger.DragPosition);
			DragVisualMode visualMode = this.GetVisualMode(pointerPosition, ref dragPosition);
			bool flag = visualMode == DragVisualMode.Rejected;
			if (flag)
			{
				this.ClearDragAndDropUI(false);
			}
			else
			{
				this.HandleDragAndScroll(pointerPosition);
				this.ApplyDragAndDropUI(dragPosition);
			}
			base.dragAndDrop.SetVisualMode(visualMode);
			base.dragAndDrop.UpdateDrag(pointerPosition);
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x0003B0F8 File Offset: 0x000392F8
		private DragVisualMode GetVisualMode(Vector3 pointerPosition, ref ListViewDragger.DragPosition dragPosition)
		{
			bool flag = this.dragAndDropController == null;
			DragVisualMode result;
			if (flag)
			{
				result = DragVisualMode.Rejected;
			}
			else
			{
				bool flag2 = this.TryGetDragPosition(pointerPosition, ref dragPosition);
				DragAndDropArgs dragAndDropArgs = this.MakeDragAndDropArgs(dragPosition);
				result = (flag2 ? this.dragAndDropController.HandleDragAndDrop(dragAndDropArgs) : DragVisualMode.Rejected);
			}
			return result;
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x0003B150 File Offset: 0x00039350
		protected internal override void OnDrop(Vector3 pointerPosition)
		{
			ListViewDragger.DragPosition dragPosition = default(ListViewDragger.DragPosition);
			bool flag = !this.TryGetDragPosition(pointerPosition, ref dragPosition);
			if (!flag)
			{
				DragAndDropArgs dragAndDropArgs = this.MakeDragAndDropArgs(dragPosition);
				bool flag2 = this.dragAndDropController.HandleDragAndDrop(dragAndDropArgs) != DragVisualMode.Rejected;
				if (flag2)
				{
					this.dragAndDropController.OnDrop(dragAndDropArgs);
					base.dragAndDrop.AcceptDrag();
				}
				else
				{
					this.dragAndDropController.DragCleanup();
				}
			}
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0003B1D0 File Offset: 0x000393D0
		internal void HandleDragAndScroll(Vector2 pointerPosition)
		{
			bool flag = pointerPosition.y < this.targetScrollView.worldBound.yMin + 5f;
			bool flag2 = pointerPosition.y > this.targetScrollView.worldBound.yMax - 5f;
			bool flag3 = flag || flag2;
			if (flag3)
			{
				Vector2 vector = this.targetScrollView.scrollOffset + (flag ? Vector2.down : Vector2.up) * 20f;
				vector.y = Mathf.Clamp(vector.y, 0f, Mathf.Max(0f, this.targetScrollView.contentContainer.worldBound.height - this.targetScrollView.contentViewport.worldBound.height));
				this.targetScrollView.scrollOffset = vector;
			}
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x0003B2BC File Offset: 0x000394BC
		protected void ApplyDragAndDropUI(ListViewDragger.DragPosition dragPosition)
		{
			bool flag = this.m_LastDragPosition.Equals(dragPosition);
			if (!flag)
			{
				bool flag2 = this.m_DragHoverBar == null;
				if (flag2)
				{
					this.m_DragHoverBar = new VisualElement();
					this.m_DragHoverBar.AddToClassList(BaseVerticalCollectionView.dragHoverBarUssClassName);
					this.m_DragHoverBar.style.width = this.targetView.localBound.width;
					this.m_DragHoverBar.style.visibility = Visibility.Hidden;
					this.m_DragHoverBar.pickingMode = PickingMode.Ignore;
					this.targetView.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.<ApplyDragAndDropUI>g__GeometryChangedCallback|26_0), TrickleDown.NoTrickleDown);
					this.targetScrollView.contentViewport.Add(this.m_DragHoverBar);
				}
				bool flag3 = this.m_DragHoverItemMarker == null && this.targetView is TreeView;
				if (flag3)
				{
					this.m_DragHoverItemMarker = new VisualElement();
					this.m_DragHoverItemMarker.AddToClassList(BaseVerticalCollectionView.dragHoverMarkerUssClassName);
					this.m_DragHoverItemMarker.style.visibility = Visibility.Hidden;
					this.m_DragHoverItemMarker.pickingMode = PickingMode.Ignore;
					this.m_DragHoverBar.Add(this.m_DragHoverItemMarker);
					this.m_DragHoverSiblingMarker = new VisualElement();
					this.m_DragHoverSiblingMarker.AddToClassList(BaseVerticalCollectionView.dragHoverMarkerUssClassName);
					this.m_DragHoverSiblingMarker.style.visibility = Visibility.Hidden;
					this.m_DragHoverSiblingMarker.pickingMode = PickingMode.Ignore;
					this.targetScrollView.contentViewport.Add(this.m_DragHoverSiblingMarker);
				}
				this.ClearDragAndDropUI(false);
				this.m_LastDragPosition = dragPosition;
				switch (dragPosition.dropPosition)
				{
				case DragAndDropPosition.OverItem:
					dragPosition.recycledItem.rootElement.AddToClassList(BaseVerticalCollectionView.itemDragHoverUssClassName);
					break;
				case DragAndDropPosition.BetweenItems:
				{
					bool flag4 = dragPosition.insertAtIndex == 0;
					if (flag4)
					{
						this.PlaceHoverBarAt(0f, -1f, -1f);
					}
					else
					{
						ReusableCollectionItem recycledItemFromIndex = this.targetView.GetRecycledItemFromIndex(dragPosition.insertAtIndex - 1);
						ReusableCollectionItem recycledItemFromIndex2 = this.targetView.GetRecycledItemFromIndex(dragPosition.insertAtIndex);
						this.PlaceHoverBarAtElement(recycledItemFromIndex ?? recycledItemFromIndex2);
					}
					break;
				}
				case DragAndDropPosition.OutsideItems:
				{
					ReusableCollectionItem recycledItemFromIndex3 = this.targetView.GetRecycledItemFromIndex(this.targetView.itemsSource.Count - 1);
					bool flag5 = recycledItemFromIndex3 != null;
					if (flag5)
					{
						this.PlaceHoverBarAtElement(recycledItemFromIndex3);
					}
					else
					{
						this.PlaceHoverBarAt(0f, -1f, -1f);
					}
					break;
				}
				default:
					throw new ArgumentOutOfRangeException("dropPosition", dragPosition.dropPosition, "Unsupported dropPosition value");
				}
			}
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0003B570 File Offset: 0x00039770
		protected virtual bool TryGetDragPosition(Vector2 pointerPosition, ref ListViewDragger.DragPosition dragPosition)
		{
			ReusableCollectionItem recycledItem = this.GetRecycledItem(pointerPosition);
			bool flag = recycledItem == null;
			bool result;
			if (flag)
			{
				bool flag2 = !this.targetView.worldBound.Contains(pointerPosition);
				if (flag2)
				{
					result = false;
				}
				else
				{
					dragPosition.dropPosition = DragAndDropPosition.OutsideItems;
					bool flag3 = pointerPosition.y >= this.targetScrollView.contentContainer.worldBound.yMax;
					if (flag3)
					{
						dragPosition.insertAtIndex = this.targetView.itemsSource.Count;
					}
					else
					{
						dragPosition.insertAtIndex = 0;
					}
					this.HandleTreePosition(pointerPosition, ref dragPosition);
					result = true;
				}
			}
			else
			{
				bool flag4 = recycledItem.rootElement.worldBound.yMax - pointerPosition.y < 5f;
				if (flag4)
				{
					dragPosition.insertAtIndex = recycledItem.index + 1;
					dragPosition.dropPosition = DragAndDropPosition.BetweenItems;
				}
				else
				{
					bool flag5 = pointerPosition.y - recycledItem.rootElement.worldBound.yMin > 5f;
					if (flag5)
					{
						Vector2 scrollOffset = this.targetScrollView.scrollOffset;
						this.targetScrollView.ScrollTo(recycledItem.rootElement);
						bool flag6 = scrollOffset != this.targetScrollView.scrollOffset;
						if (flag6)
						{
							return this.TryGetDragPosition(pointerPosition, ref dragPosition);
						}
						dragPosition.recycledItem = recycledItem;
						dragPosition.insertAtIndex = recycledItem.index;
						dragPosition.dropPosition = DragAndDropPosition.OverItem;
					}
					else
					{
						dragPosition.insertAtIndex = recycledItem.index;
						dragPosition.dropPosition = DragAndDropPosition.BetweenItems;
					}
				}
				this.HandleTreePosition(pointerPosition, ref dragPosition);
				result = true;
			}
			return result;
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x0003B70C File Offset: 0x0003990C
		private void HandleTreePosition(Vector2 pointerPosition, ref ListViewDragger.DragPosition dragPosition)
		{
			dragPosition.parentId = -1;
			dragPosition.childIndex = -1;
			this.m_LeftIndentation = -1f;
			this.m_SiblingBottom = -1f;
			TreeView treeView = this.targetView as TreeView;
			bool flag = treeView == null;
			if (!flag)
			{
				bool flag2 = dragPosition.insertAtIndex < 0;
				if (!flag2)
				{
					TreeViewController viewController = treeView.viewController;
					bool flag3 = dragPosition.dropPosition == DragAndDropPosition.OverItem;
					if (flag3)
					{
						dragPosition.parentId = viewController.GetIdForIndex(dragPosition.insertAtIndex);
						dragPosition.childIndex = -1;
					}
					else
					{
						bool flag4 = dragPosition.insertAtIndex <= 0;
						if (flag4)
						{
							dragPosition.childIndex = 0;
						}
						else
						{
							this.HandleSiblingInsertionAtAvailableDepthsAndChangeTargetIfNeeded(ref dragPosition, pointerPosition);
						}
					}
				}
			}
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x0003B7C0 File Offset: 0x000399C0
		private void HandleSiblingInsertionAtAvailableDepthsAndChangeTargetIfNeeded(ref ListViewDragger.DragPosition dragPosition, Vector2 pointerPosition)
		{
			TreeView treeView = this.targetView as TreeView;
			bool flag = treeView == null;
			if (!flag)
			{
				TreeViewController viewController = treeView.viewController;
				int insertAtIndex = dragPosition.insertAtIndex;
				int idForIndex = viewController.GetIdForIndex(insertAtIndex);
				int num;
				int num2;
				this.GetPreviousAndNextItemsIgnoringDraggedItems(dragPosition.insertAtIndex, out num, out num2);
				bool flag2 = num == -1;
				if (!flag2)
				{
					bool flag3 = viewController.HasChildren(num) && treeView.IsExpanded(num);
					int indentationDepth = viewController.GetIndentationDepth(num);
					int indentationDepth2 = viewController.GetIndentationDepth(num2);
					int num3 = (num2 != -1) ? indentationDepth2 : 0;
					int num4 = viewController.GetIndentationDepth(num) + (flag3 ? 1 : 0);
					int num5 = num;
					float num6 = 15f;
					float num7 = 15f;
					bool flag4 = indentationDepth > 0;
					if (flag4)
					{
						VisualElement rootElementForId = treeView.GetRootElementForId(num);
						VisualElement visualElement = rootElementForId.Q(TreeView.itemIndentUssClassName, null);
						VisualElement visualElement2 = rootElementForId.Q(TreeView.itemToggleUssClassName, null);
						num6 = visualElement2.layout.width;
						num7 = visualElement.layout.width / (float)indentationDepth;
					}
					else
					{
						int indentationDepth3 = treeView.viewController.GetIndentationDepth(idForIndex);
						bool flag5 = indentationDepth3 > 0;
						if (flag5)
						{
							VisualElement rootElementForId2 = treeView.GetRootElementForId(idForIndex);
							VisualElement visualElement3 = rootElementForId2.Q(TreeView.itemIndentUssClassName, null);
							VisualElement visualElement4 = rootElementForId2.Q(TreeView.itemToggleUssClassName, null);
							num6 = visualElement4.layout.width;
							num7 = visualElement3.layout.width / (float)indentationDepth3;
						}
					}
					bool flag6 = num4 <= num3;
					if (flag6)
					{
						this.m_LeftIndentation = num6 + num7 * (float)num3;
						bool flag7 = flag3;
						if (flag7)
						{
							dragPosition.parentId = num;
							dragPosition.childIndex = 0;
						}
						else
						{
							dragPosition.parentId = viewController.GetParentId(num);
							dragPosition.childIndex = viewController.GetChildIndexForId(num2);
						}
					}
					else
					{
						Vector2 vector = treeView.scrollView.contentContainer.WorldToLocal(pointerPosition);
						int num8 = Mathf.FloorToInt((vector.x - num6) / num7);
						bool flag8 = num8 >= num4;
						if (flag8)
						{
							this.m_LeftIndentation = num6 + num7 * (float)num4;
							bool flag9 = flag3;
							if (flag9)
							{
								dragPosition.parentId = num;
								dragPosition.childIndex = 0;
							}
							else
							{
								dragPosition.parentId = viewController.GetParentId(num);
								dragPosition.childIndex = viewController.GetChildIndexForId(num) + 1;
							}
						}
						else
						{
							int i;
							for (i = viewController.GetIndentationDepth(num5); i > num3; i--)
							{
								bool flag10 = i == num8;
								if (flag10)
								{
									break;
								}
								num5 = viewController.GetParentId(num5);
							}
							bool flag11 = num5 != idForIndex;
							bool flag12 = flag11;
							if (flag12)
							{
								VisualElement rootElementForId3 = treeView.GetRootElementForId(num5);
								bool flag13 = rootElementForId3 != null;
								if (flag13)
								{
									VisualElement contentViewport = this.targetScrollView.contentViewport;
									Rect rect = contentViewport.WorldToLocal(rootElementForId3.worldBound);
									bool flag14 = contentViewport.localBound.yMin < rect.yMax && rect.yMax < contentViewport.localBound.yMax;
									if (flag14)
									{
										this.m_SiblingBottom = rect.yMax;
									}
								}
							}
							dragPosition.parentId = viewController.GetParentId(num5);
							dragPosition.childIndex = viewController.GetChildIndexForId(num5) + 1;
							this.m_LeftIndentation = num6 + num7 * (float)i;
						}
					}
				}
			}
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x0003BB28 File Offset: 0x00039D28
		private void GetPreviousAndNextItemsIgnoringDraggedItems(int insertAtIndex, out int previousItemId, out int nextItemId)
		{
			previousItemId = (nextItemId = -1);
			int i = insertAtIndex - 1;
			int j = insertAtIndex;
			while (i >= 0)
			{
				int idForIndex = this.targetView.viewController.GetIdForIndex(i);
				bool flag = !this.dragAndDropController.GetSortedSelectedIds().Contains(idForIndex);
				if (flag)
				{
					previousItemId = idForIndex;
					break;
				}
				i--;
			}
			while (j < this.targetView.itemsSource.Count)
			{
				int idForIndex2 = this.targetView.viewController.GetIdForIndex(j);
				bool flag2 = !this.dragAndDropController.GetSortedSelectedIds().Contains(idForIndex2);
				if (flag2)
				{
					nextItemId = idForIndex2;
					break;
				}
				j++;
			}
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x0003BBE4 File Offset: 0x00039DE4
		protected DragAndDropArgs MakeDragAndDropArgs(ListViewDragger.DragPosition dragPosition)
		{
			object target = null;
			ReusableCollectionItem recycledItem = dragPosition.recycledItem;
			bool flag = recycledItem != null;
			if (flag)
			{
				target = this.targetView.viewController.GetItemForIndex(recycledItem.index);
			}
			return new DragAndDropArgs
			{
				target = target,
				insertAtIndex = dragPosition.insertAtIndex,
				parentId = dragPosition.parentId,
				childIndex = dragPosition.childIndex,
				dragAndDropPosition = dragPosition.dropPosition,
				dragAndDropData = base.dragAndDrop.data
			};
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x0003BC80 File Offset: 0x00039E80
		private float GetHoverBarTopPosition(ReusableCollectionItem item)
		{
			VisualElement contentViewport = this.targetScrollView.contentViewport;
			return Mathf.Min(contentViewport.WorldToLocal(item.rootElement.worldBound).yMax, contentViewport.localBound.yMax - 2f);
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0003BCD4 File Offset: 0x00039ED4
		private void PlaceHoverBarAtElement(ReusableCollectionItem item)
		{
			this.PlaceHoverBarAt(this.GetHoverBarTopPosition(item), this.m_LeftIndentation, this.m_SiblingBottom);
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x0003BCF4 File Offset: 0x00039EF4
		private void PlaceHoverBarAt(float top, float indentationPadding = -1f, float siblingBottom = -1f)
		{
			this.m_DragHoverBar.style.top = top;
			this.m_DragHoverBar.style.visibility = Visibility.Visible;
			bool flag = this.m_DragHoverItemMarker != null;
			if (flag)
			{
				this.m_DragHoverItemMarker.style.visibility = Visibility.Visible;
			}
			bool flag2 = indentationPadding >= 0f;
			if (flag2)
			{
				this.m_DragHoverBar.style.marginLeft = indentationPadding;
				this.m_DragHoverBar.style.width = this.targetView.localBound.width - indentationPadding;
				bool flag3 = siblingBottom > 0f && this.m_DragHoverSiblingMarker != null;
				if (flag3)
				{
					this.m_DragHoverSiblingMarker.style.top = siblingBottom;
					this.m_DragHoverSiblingMarker.style.visibility = Visibility.Visible;
					this.m_DragHoverSiblingMarker.style.marginLeft = indentationPadding;
				}
			}
			else
			{
				this.m_DragHoverBar.style.marginLeft = 0f;
				this.m_DragHoverBar.style.width = this.targetView.localBound.width;
			}
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x0003BE54 File Offset: 0x0003A054
		protected override void ClearDragAndDropUI(bool dragCancelled)
		{
			if (dragCancelled)
			{
				this.dragAndDropController.DragCleanup();
			}
			this.targetView.elementPanel.cursorManager.ResetCursor();
			this.m_LastDragPosition = default(ListViewDragger.DragPosition);
			foreach (ReusableCollectionItem reusableCollectionItem in this.targetView.activeItems)
			{
				reusableCollectionItem.rootElement.RemoveFromClassList(BaseVerticalCollectionView.itemDragHoverUssClassName);
			}
			bool flag = this.m_DragHoverBar != null;
			if (flag)
			{
				this.m_DragHoverBar.style.visibility = Visibility.Hidden;
			}
			bool flag2 = this.m_DragHoverItemMarker != null;
			if (flag2)
			{
				this.m_DragHoverItemMarker.style.visibility = Visibility.Hidden;
			}
			bool flag3 = this.m_DragHoverSiblingMarker != null;
			if (flag3)
			{
				this.m_DragHoverSiblingMarker.style.visibility = Visibility.Hidden;
			}
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x0003BF5C File Offset: 0x0003A15C
		protected ReusableCollectionItem GetRecycledItem(Vector3 pointerPosition)
		{
			foreach (ReusableCollectionItem reusableCollectionItem in this.targetView.activeItems)
			{
				bool flag = reusableCollectionItem.rootElement.worldBound.Contains(pointerPosition);
				if (flag)
				{
					return reusableCollectionItem;
				}
			}
			return null;
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x0003BFD0 File Offset: 0x0003A1D0
		[CompilerGenerated]
		private void <ApplyDragAndDropUI>g__GeometryChangedCallback|26_0(GeometryChangedEvent e)
		{
			this.m_DragHoverBar.style.width = this.targetView.localBound.width;
		}

		// Token: 0x040006A1 RID: 1697
		private ListViewDragger.DragPosition m_LastDragPosition;

		// Token: 0x040006A2 RID: 1698
		private VisualElement m_DragHoverBar;

		// Token: 0x040006A3 RID: 1699
		private VisualElement m_DragHoverItemMarker;

		// Token: 0x040006A4 RID: 1700
		private VisualElement m_DragHoverSiblingMarker;

		// Token: 0x040006A5 RID: 1701
		private float m_LeftIndentation = -1f;

		// Token: 0x040006A6 RID: 1702
		private float m_SiblingBottom = -1f;

		// Token: 0x040006A7 RID: 1703
		private const int k_AutoScrollAreaSize = 5;

		// Token: 0x040006A8 RID: 1704
		private const int k_BetweenElementsAreaSize = 5;

		// Token: 0x040006A9 RID: 1705
		private const int k_PanSpeed = 20;

		// Token: 0x040006AA RID: 1706
		private const int k_DragHoverBarHeight = 2;

		// Token: 0x040006AB RID: 1707
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ICollectionDragAndDropController <dragAndDropController>k__BackingField;

		// Token: 0x020001B6 RID: 438
		internal struct DragPosition : IEquatable<ListViewDragger.DragPosition>
		{
			// Token: 0x06000E60 RID: 3680 RVA: 0x0003C008 File Offset: 0x0003A208
			public bool Equals(ListViewDragger.DragPosition other)
			{
				return this.insertAtIndex == other.insertAtIndex && this.parentId == other.parentId && this.childIndex == other.childIndex && object.Equals(this.recycledItem, other.recycledItem) && this.dropPosition == other.dropPosition;
			}

			// Token: 0x06000E61 RID: 3681 RVA: 0x0003C068 File Offset: 0x0003A268
			public override bool Equals(object obj)
			{
				bool result;
				if (obj is ListViewDragger.DragPosition)
				{
					ListViewDragger.DragPosition other = (ListViewDragger.DragPosition)obj;
					result = this.Equals(other);
				}
				else
				{
					result = false;
				}
				return result;
			}

			// Token: 0x06000E62 RID: 3682 RVA: 0x0003C094 File Offset: 0x0003A294
			public override int GetHashCode()
			{
				int num = this.insertAtIndex;
				num = (num * 397 ^ this.parentId);
				num = (num * 397 ^ this.childIndex);
				int num2 = num * 397;
				ReusableCollectionItem reusableCollectionItem = this.recycledItem;
				num = (num2 ^ ((reusableCollectionItem != null) ? reusableCollectionItem.GetHashCode() : 0));
				return num * 397 ^ (int)this.dropPosition;
			}

			// Token: 0x040006AC RID: 1708
			public int insertAtIndex;

			// Token: 0x040006AD RID: 1709
			public int parentId;

			// Token: 0x040006AE RID: 1710
			public int childIndex;

			// Token: 0x040006AF RID: 1711
			public ReusableCollectionItem recycledItem;

			// Token: 0x040006B0 RID: 1712
			public DragAndDropPosition dropPosition;
		}
	}
}
