using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	// Token: 0x0200019D RID: 413
	internal class InternalTreeView : VisualElement
	{
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x000389F4 File Offset: 0x00036BF4
		// (set) Token: 0x06000D7B RID: 3451 RVA: 0x00038A0C File Offset: 0x00036C0C
		public Func<VisualElement> makeItem
		{
			get
			{
				return this.m_MakeItem;
			}
			set
			{
				bool flag = this.m_MakeItem == value;
				if (!flag)
				{
					this.m_MakeItem = value;
					this.m_ListView.Rebuild();
				}
			}
		}

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06000D7C RID: 3452 RVA: 0x00038A40 File Offset: 0x00036C40
		// (remove) Token: 0x06000D7D RID: 3453 RVA: 0x00038A78 File Offset: 0x00036C78
		public event Action<IEnumerable<ITreeViewItem>> onItemsChosen
		{
			[CompilerGenerated]
			add
			{
				Action<IEnumerable<ITreeViewItem>> action = this.onItemsChosen;
				Action<IEnumerable<ITreeViewItem>> action2;
				do
				{
					action2 = action;
					Action<IEnumerable<ITreeViewItem>> value2 = (Action<IEnumerable<ITreeViewItem>>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<IEnumerable<ITreeViewItem>>>(ref this.onItemsChosen, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<IEnumerable<ITreeViewItem>> action = this.onItemsChosen;
				Action<IEnumerable<ITreeViewItem>> action2;
				do
				{
					action2 = action;
					Action<IEnumerable<ITreeViewItem>> value2 = (Action<IEnumerable<ITreeViewItem>>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<IEnumerable<ITreeViewItem>>>(ref this.onItemsChosen, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06000D7E RID: 3454 RVA: 0x00038AB0 File Offset: 0x00036CB0
		// (remove) Token: 0x06000D7F RID: 3455 RVA: 0x00038AE8 File Offset: 0x00036CE8
		public event Action<IEnumerable<ITreeViewItem>> onSelectionChange
		{
			[CompilerGenerated]
			add
			{
				Action<IEnumerable<ITreeViewItem>> action = this.onSelectionChange;
				Action<IEnumerable<ITreeViewItem>> action2;
				do
				{
					action2 = action;
					Action<IEnumerable<ITreeViewItem>> value2 = (Action<IEnumerable<ITreeViewItem>>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<IEnumerable<ITreeViewItem>>>(ref this.onSelectionChange, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<IEnumerable<ITreeViewItem>> action = this.onSelectionChange;
				Action<IEnumerable<ITreeViewItem>> action2;
				do
				{
					action2 = action;
					Action<IEnumerable<ITreeViewItem>> value2 = (Action<IEnumerable<ITreeViewItem>>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<IEnumerable<ITreeViewItem>>>(ref this.onSelectionChange, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x00038B1D File Offset: 0x00036D1D
		public ITreeViewItem selectedItem
		{
			get
			{
				return (this.m_SelectedItems.Count == 0) ? null : this.m_SelectedItems.First<ITreeViewItem>();
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x00038B3C File Offset: 0x00036D3C
		public IEnumerable<ITreeViewItem> selectedItems
		{
			get
			{
				bool flag = this.m_SelectedItems != null;
				IEnumerable<ITreeViewItem> selectedItems;
				if (flag)
				{
					selectedItems = this.m_SelectedItems;
				}
				else
				{
					this.m_SelectedItems = new List<ITreeViewItem>();
					foreach (ITreeViewItem treeViewItem in this.items)
					{
						foreach (int num in this.m_ListView.selectedIds)
						{
							bool flag2 = treeViewItem.id == num;
							if (flag2)
							{
								this.m_SelectedItems.Add(treeViewItem);
							}
						}
					}
					selectedItems = this.m_SelectedItems;
				}
				return selectedItems;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000D82 RID: 3458 RVA: 0x00038C18 File Offset: 0x00036E18
		// (set) Token: 0x06000D83 RID: 3459 RVA: 0x00038C30 File Offset: 0x00036E30
		public Action<VisualElement, ITreeViewItem> bindItem
		{
			get
			{
				return this.m_BindItem;
			}
			set
			{
				this.m_BindItem = value;
				this.ListViewRefresh();
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x00038C41 File Offset: 0x00036E41
		// (set) Token: 0x06000D85 RID: 3461 RVA: 0x00038C49 File Offset: 0x00036E49
		public Action<VisualElement, ITreeViewItem> unbindItem
		{
			[CompilerGenerated]
			get
			{
				return this.<unbindItem>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<unbindItem>k__BackingField = value;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x00038C54 File Offset: 0x00036E54
		// (set) Token: 0x06000D87 RID: 3463 RVA: 0x00038C6C File Offset: 0x00036E6C
		public IList<ITreeViewItem> rootItems
		{
			get
			{
				return this.m_RootItems;
			}
			set
			{
				this.m_RootItems = value;
				this.Rebuild();
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x00038C7D File Offset: 0x00036E7D
		public IEnumerable<ITreeViewItem> items
		{
			get
			{
				return InternalTreeView.GetAllItems(this.m_RootItems);
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x00038C8A File Offset: 0x00036E8A
		public float resolvedItemHeight
		{
			get
			{
				return this.m_ListView.ResolveItemHeight(-1f);
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x00038C9C File Offset: 0x00036E9C
		// (set) Token: 0x06000D8B RID: 3467 RVA: 0x00038CBA File Offset: 0x00036EBA
		public int itemHeight
		{
			get
			{
				return (int)this.m_ListView.fixedItemHeight;
			}
			set
			{
				this.m_ListView.fixedItemHeight = (float)value;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x00038CCC File Offset: 0x00036ECC
		// (set) Token: 0x06000D8D RID: 3469 RVA: 0x00038CE9 File Offset: 0x00036EE9
		public bool horizontalScrollingEnabled
		{
			get
			{
				return this.m_ListView.horizontalScrollingEnabled;
			}
			set
			{
				this.m_ListView.horizontalScrollingEnabled = value;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x00038CFC File Offset: 0x00036EFC
		// (set) Token: 0x06000D8F RID: 3471 RVA: 0x00038D19 File Offset: 0x00036F19
		public bool showBorder
		{
			get
			{
				return this.m_ListView.showBorder;
			}
			set
			{
				this.m_ListView.showBorder = value;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x00038D2C File Offset: 0x00036F2C
		// (set) Token: 0x06000D91 RID: 3473 RVA: 0x00038D49 File Offset: 0x00036F49
		public SelectionType selectionType
		{
			get
			{
				return this.m_ListView.selectionType;
			}
			set
			{
				this.m_ListView.selectionType = value;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x00038D5C File Offset: 0x00036F5C
		// (set) Token: 0x06000D93 RID: 3475 RVA: 0x00038D79 File Offset: 0x00036F79
		public AlternatingRowBackground showAlternatingRowBackgrounds
		{
			get
			{
				return this.m_ListView.showAlternatingRowBackgrounds;
			}
			set
			{
				this.m_ListView.showAlternatingRowBackgrounds = value;
			}
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00038D8C File Offset: 0x00036F8C
		public InternalTreeView()
		{
			this.m_SelectedItems = null;
			this.m_ExpandedItemIds = new List<int>();
			this.m_ItemWrappers = new List<InternalTreeView.TreeViewItemWrapper>();
			this.m_ListView = new ListView();
			this.m_ListView.name = InternalTreeView.s_ListViewName;
			this.m_ListView.itemsSource = this.m_ItemWrappers;
			this.m_ListView.viewDataKey = InternalTreeView.s_ListViewName;
			this.m_ListView.AddToClassList(InternalTreeView.s_ListViewName);
			base.hierarchy.Add(this.m_ListView);
			this.m_ListView.makeItem = new Func<VisualElement>(this.MakeTreeItem);
			this.m_ListView.bindItem = new Action<VisualElement, int>(this.BindTreeItem);
			this.m_ListView.unbindItem = new Action<VisualElement, int>(this.UnbindTreeItem);
			this.m_ListView.getItemId = new Func<int, int>(this.GetItemId);
			this.m_ListView.onItemsChosen += this.OnItemsChosen;
			this.m_ListView.onSelectionChange += this.OnSelectionChange;
			this.m_ScrollView = this.m_ListView.scrollView;
			this.m_ScrollView.contentContainer.RegisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
			base.RegisterCallback<MouseUpEvent>(new EventCallback<MouseUpEvent>(this.OnTreeViewMouseUp), TrickleDown.TrickleDown);
			base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00038F0A File Offset: 0x0003710A
		public InternalTreeView(IList<ITreeViewItem> items, int fixedItemHeight, Func<VisualElement> makeItem, Action<VisualElement, ITreeViewItem> bindItem) : this()
		{
			this.m_ListView.fixedItemHeight = (float)fixedItemHeight;
			this.m_MakeItem = makeItem;
			this.m_BindItem = bindItem;
			this.m_RootItems = items;
			this.Rebuild();
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00038F3F File Offset: 0x0003713F
		public void RefreshItems()
		{
			this.RegenerateWrappers();
			this.ListViewRefresh();
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00038F50 File Offset: 0x00037150
		public void Rebuild()
		{
			this.RegenerateWrappers();
			this.m_ListView.Rebuild();
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00038F68 File Offset: 0x00037168
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
			base.OverwriteFromViewData(this, fullHierarchicalViewDataKey);
			this.Rebuild();
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00038F94 File Offset: 0x00037194
		public static IEnumerable<ITreeViewItem> GetAllItems(IEnumerable<ITreeViewItem> rootItems)
		{
			bool flag = rootItems == null;
			if (flag)
			{
				yield break;
			}
			Stack<IEnumerator<ITreeViewItem>> iteratorStack = new Stack<IEnumerator<ITreeViewItem>>();
			IEnumerator<ITreeViewItem> currentIterator = rootItems.GetEnumerator();
			for (;;)
			{
				bool hasNext = currentIterator.MoveNext();
				bool flag2 = !hasNext;
				if (flag2)
				{
					bool flag3 = iteratorStack.Count > 0;
					if (!flag3)
					{
						break;
					}
					currentIterator = iteratorStack.Pop();
				}
				else
				{
					ITreeViewItem currentItem = currentIterator.Current;
					yield return currentItem;
					bool hasChildren = currentItem.hasChildren;
					if (hasChildren)
					{
						iteratorStack.Push(currentIterator);
						currentIterator = currentItem.children.GetEnumerator();
					}
					currentItem = null;
				}
			}
			yield break;
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00038FA4 File Offset: 0x000371A4
		public void OnKeyDown(KeyDownEvent evt)
		{
			int selectedIndex = this.m_ListView.selectedIndex;
			bool flag = true;
			KeyCode keyCode = evt.keyCode;
			KeyCode keyCode2 = keyCode;
			if (keyCode2 != KeyCode.RightArrow)
			{
				if (keyCode2 != KeyCode.LeftArrow)
				{
					flag = false;
				}
				else
				{
					bool flag2 = this.IsExpandedByIndex(selectedIndex);
					if (flag2)
					{
						this.CollapseItemByIndex(selectedIndex);
					}
				}
			}
			else
			{
				bool flag3 = !this.IsExpandedByIndex(selectedIndex);
				if (flag3)
				{
					this.ExpandItemByIndex(selectedIndex);
				}
			}
			bool flag4 = flag;
			if (flag4)
			{
				evt.StopPropagation();
			}
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x00039020 File Offset: 0x00037220
		public void SetSelection(int id)
		{
			this.SetSelection(new int[]
			{
				id
			});
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00039034 File Offset: 0x00037234
		public void SetSelection(IEnumerable<int> ids)
		{
			this.SetSelectionInternal(ids, true);
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00039040 File Offset: 0x00037240
		public void SetSelectionWithoutNotify(IEnumerable<int> ids)
		{
			this.SetSelectionInternal(ids, false);
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x0003904C File Offset: 0x0003724C
		internal void SetSelectionInternal(IEnumerable<int> ids, bool sendNotification)
		{
			bool flag = ids == null;
			if (!flag)
			{
				List<int> indices = (from id in ids
				select this.GetItemIndex(id, true)).ToList<int>();
				this.ListViewRefresh();
				this.m_ListView.SetSelectionInternal(indices, sendNotification);
			}
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00039094 File Offset: 0x00037294
		internal void SetSelectionByIndices(IEnumerable<int> indexes, bool sendNotification)
		{
			bool flag = indexes == null;
			if (!flag)
			{
				this.ListViewRefresh();
				this.m_ListView.SetSelectionInternal(indexes, sendNotification);
			}
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x000390C4 File Offset: 0x000372C4
		public void AddToSelection(int id)
		{
			int itemIndex = this.GetItemIndex(id, true);
			this.ListViewRefresh();
			this.m_ListView.AddToSelection(itemIndex);
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x000390F0 File Offset: 0x000372F0
		public void RemoveFromSelection(int id)
		{
			int itemIndex = this.GetItemIndex(id, false);
			this.m_ListView.RemoveFromSelection(itemIndex);
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00039114 File Offset: 0x00037314
		internal int GetItemIndex(int id, bool expand = false)
		{
			ITreeViewItem treeViewItem = this.FindItem(id);
			bool flag = treeViewItem == null;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("id", id, "InternalTreeView: Item id not found.");
			}
			if (expand)
			{
				bool flag2 = false;
				for (ITreeViewItem parent = treeViewItem.parent; parent != null; parent = parent.parent)
				{
					bool flag3 = !this.m_ExpandedItemIds.Contains(parent.id);
					if (flag3)
					{
						this.m_ExpandedItemIds.Add(parent.id);
						flag2 = true;
					}
				}
				bool flag4 = flag2;
				if (flag4)
				{
					this.RegenerateWrappers();
				}
			}
			int i;
			for (i = 0; i < this.m_ItemWrappers.Count; i++)
			{
				bool flag5 = this.m_ItemWrappers[i].id == id;
				if (flag5)
				{
					break;
				}
			}
			return i;
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x000391F7 File Offset: 0x000373F7
		public void ClearSelection()
		{
			this.m_ListView.ClearSelection();
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00039206 File Offset: 0x00037406
		public void ScrollTo(VisualElement visualElement)
		{
			this.m_ListView.ScrollTo(visualElement);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00039218 File Offset: 0x00037418
		public void ScrollToItem(int id)
		{
			int itemIndex = this.GetItemIndex(id, true);
			this.RefreshItems();
			this.m_ListView.ScrollToItem(itemIndex);
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00039244 File Offset: 0x00037444
		internal void CopyExpandedStates(ITreeViewItem source, ITreeViewItem target)
		{
			bool flag = this.IsExpanded(source.id);
			if (flag)
			{
				this.ExpandItem(target.id);
				bool flag2 = source.children != null && source.children.Count<ITreeViewItem>() > 0;
				if (flag2)
				{
					bool flag3 = target.children == null || source.children.Count<ITreeViewItem>() != target.children.Count<ITreeViewItem>();
					if (flag3)
					{
						Debug.LogWarning("Source and target hierarchies are not the same");
					}
					else
					{
						for (int i = 0; i < source.children.Count<ITreeViewItem>(); i++)
						{
							ITreeViewItem source2 = source.children.ElementAt(i);
							ITreeViewItem target2 = target.children.ElementAt(i);
							this.CopyExpandedStates(source2, target2);
						}
					}
				}
			}
			else
			{
				this.CollapseItem(target.id);
			}
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00039324 File Offset: 0x00037524
		public bool IsExpanded(int id)
		{
			return this.m_ExpandedItemIds.Contains(id);
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00039344 File Offset: 0x00037544
		public void CollapseItem(int id)
		{
			bool flag = this.FindItem(id) == null;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("id", id, "InternalTreeView: Item id not found.");
			}
			for (int i = 0; i < this.m_ItemWrappers.Count; i++)
			{
				bool flag2 = this.m_ItemWrappers[i].item.id == id;
				if (flag2)
				{
					bool flag3 = this.IsExpandedByIndex(i);
					if (flag3)
					{
						this.CollapseItemByIndex(i);
						return;
					}
				}
			}
			bool flag4 = !this.m_ExpandedItemIds.Contains(id);
			if (flag4)
			{
				return;
			}
			this.m_ExpandedItemIds.Remove(id);
			this.RefreshItems();
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x000393F0 File Offset: 0x000375F0
		public void ExpandItem(int id)
		{
			bool flag = this.FindItem(id) == null;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("id", id, "InternalTreeView: Item id not found.");
			}
			for (int i = 0; i < this.m_ItemWrappers.Count; i++)
			{
				bool flag2 = this.m_ItemWrappers[i].item.id == id;
				if (flag2)
				{
					bool flag3 = !this.IsExpandedByIndex(i);
					if (flag3)
					{
						this.ExpandItemByIndex(i);
						return;
					}
				}
			}
			bool flag4 = this.m_ExpandedItemIds.Contains(id);
			if (flag4)
			{
				return;
			}
			this.m_ExpandedItemIds.Add(id);
			this.RefreshItems();
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x0003949C File Offset: 0x0003769C
		public ITreeViewItem FindItem(int id)
		{
			foreach (ITreeViewItem treeViewItem in this.items)
			{
				bool flag = treeViewItem.id == id;
				if (flag)
				{
					return treeViewItem;
				}
			}
			return null;
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x000394FC File Offset: 0x000376FC
		private void ListViewRefresh()
		{
			this.m_ListView.RefreshItems();
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x0003950C File Offset: 0x0003770C
		private void OnItemsChosen(IEnumerable<object> chosenItems)
		{
			bool flag = this.onItemsChosen == null;
			if (!flag)
			{
				List<ITreeViewItem> list = new List<ITreeViewItem>();
				foreach (object obj in chosenItems)
				{
					InternalTreeView.TreeViewItemWrapper treeViewItemWrapper = (InternalTreeView.TreeViewItemWrapper)obj;
					list.Add(treeViewItemWrapper.item);
				}
				this.onItemsChosen(list);
			}
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0003958C File Offset: 0x0003778C
		private void OnSelectionChange(IEnumerable<object> selectedListItems)
		{
			bool flag = this.m_SelectedItems == null;
			if (flag)
			{
				this.m_SelectedItems = new List<ITreeViewItem>();
			}
			this.m_SelectedItems.Clear();
			foreach (object obj in selectedListItems)
			{
				this.m_SelectedItems.Add(((InternalTreeView.TreeViewItemWrapper)obj).item);
			}
			Action<IEnumerable<ITreeViewItem>> action = this.onSelectionChange;
			if (action != null)
			{
				action(this.m_SelectedItems);
			}
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00039624 File Offset: 0x00037824
		private void OnTreeViewMouseUp(MouseUpEvent evt)
		{
			this.m_ScrollView.contentContainer.Focus();
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x00039638 File Offset: 0x00037838
		private void OnItemMouseUp(MouseUpEvent evt)
		{
			bool flag = (evt.modifiers & EventModifiers.Alt) == EventModifiers.None;
			if (!flag)
			{
				VisualElement e = evt.currentTarget as VisualElement;
				Toggle toggle = e.Q(InternalTreeView.s_ItemToggleName, null);
				int index = (int)toggle.userData;
				ITreeViewItem item = this.m_ItemWrappers[index].item;
				bool flag2 = this.IsExpandedByIndex(index);
				bool flag3 = !item.hasChildren;
				if (!flag3)
				{
					HashSet<int> hashSet = new HashSet<int>(this.m_ExpandedItemIds);
					bool flag4 = flag2;
					if (flag4)
					{
						hashSet.Remove(item.id);
					}
					else
					{
						hashSet.Add(item.id);
					}
					foreach (ITreeViewItem treeViewItem in InternalTreeView.GetAllItems(item.children))
					{
						bool hasChildren = treeViewItem.hasChildren;
						if (hasChildren)
						{
							bool flag5 = flag2;
							if (flag5)
							{
								hashSet.Remove(treeViewItem.id);
							}
							else
							{
								hashSet.Add(treeViewItem.id);
							}
						}
					}
					this.m_ExpandedItemIds = hashSet.ToList<int>();
					this.RefreshItems();
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x00039780 File Offset: 0x00037980
		private VisualElement MakeTreeItem()
		{
			VisualElement visualElement = new VisualElement
			{
				name = InternalTreeView.itemUssClassName,
				style = 
				{
					flexDirection = FlexDirection.Row
				}
			};
			visualElement.AddToClassList(InternalTreeView.itemUssClassName);
			visualElement.RegisterCallback<MouseUpEvent>(new EventCallback<MouseUpEvent>(this.OnItemMouseUp), TrickleDown.NoTrickleDown);
			VisualElement visualElement2 = new VisualElement
			{
				name = InternalTreeView.s_ItemIndentsContainerName,
				style = 
				{
					flexDirection = FlexDirection.Row
				}
			};
			visualElement2.AddToClassList(InternalTreeView.s_ItemIndentsContainerName);
			visualElement.hierarchy.Add(visualElement2);
			Toggle toggle = new Toggle
			{
				name = InternalTreeView.s_ItemToggleName
			};
			toggle.AddToClassList(Foldout.toggleUssClassName);
			toggle.RegisterValueChangedCallback(new EventCallback<ChangeEvent<bool>>(this.ToggleExpandedState));
			visualElement.hierarchy.Add(toggle);
			VisualElement visualElement3 = new VisualElement
			{
				name = InternalTreeView.s_ItemContentContainerName,
				style = 
				{
					flexGrow = 1f
				}
			};
			visualElement3.AddToClassList(InternalTreeView.s_ItemContentContainerName);
			visualElement.Add(visualElement3);
			bool flag = this.m_MakeItem != null;
			if (flag)
			{
				visualElement3.Add(this.m_MakeItem());
			}
			return visualElement;
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x000398BC File Offset: 0x00037ABC
		private void UnbindTreeItem(VisualElement element, int index)
		{
			bool flag = this.unbindItem == null;
			if (!flag)
			{
				ITreeViewItem arg = (this.m_ItemWrappers.Count > index) ? this.m_ItemWrappers[index].item : null;
				VisualElement arg2 = element.Q(InternalTreeView.s_ItemContentContainerName, null).ElementAt(0);
				this.unbindItem(arg2, arg);
			}
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x00039920 File Offset: 0x00037B20
		private void BindTreeItem(VisualElement element, int index)
		{
			ITreeViewItem item = this.m_ItemWrappers[index].item;
			VisualElement visualElement = element.Q(InternalTreeView.s_ItemIndentsContainerName, null);
			visualElement.Clear();
			for (int i = 0; i < this.m_ItemWrappers[index].depth; i++)
			{
				VisualElement visualElement2 = new VisualElement();
				visualElement2.AddToClassList(InternalTreeView.s_ItemIndentName);
				visualElement.Add(visualElement2);
			}
			Toggle toggle = element.Q(InternalTreeView.s_ItemToggleName, null);
			toggle.SetValueWithoutNotify(this.IsExpandedByIndex(index));
			toggle.userData = index;
			bool hasChildren = item.hasChildren;
			if (hasChildren)
			{
				toggle.visible = true;
			}
			else
			{
				toggle.visible = false;
			}
			bool flag = this.m_BindItem == null;
			if (!flag)
			{
				VisualElement arg = element.Q(InternalTreeView.s_ItemContentContainerName, null).ElementAt(0);
				this.m_BindItem(arg, item);
			}
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x00039A14 File Offset: 0x00037C14
		internal int GetItemId(int index)
		{
			return this.m_ItemWrappers[index].id;
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x00039A3C File Offset: 0x00037C3C
		private bool IsExpandedByIndex(int index)
		{
			return this.m_ExpandedItemIds.Contains(this.m_ItemWrappers[index].id);
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x00039A70 File Offset: 0x00037C70
		private void CollapseItemByIndex(int index)
		{
			bool flag = !this.m_ItemWrappers[index].item.hasChildren;
			if (!flag)
			{
				this.m_ExpandedItemIds.Remove(this.m_ItemWrappers[index].item.id);
				int num = 0;
				int num2 = index + 1;
				int depth = this.m_ItemWrappers[index].depth;
				while (num2 < this.m_ItemWrappers.Count && this.m_ItemWrappers[num2].depth > depth)
				{
					num++;
					num2++;
				}
				this.m_ItemWrappers.RemoveRange(index + 1, num);
				this.ListViewRefresh();
				base.SaveViewData();
			}
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x00039B30 File Offset: 0x00037D30
		private void ExpandItemByIndex(int index)
		{
			bool flag = !this.m_ItemWrappers[index].item.hasChildren;
			if (!flag)
			{
				List<InternalTreeView.TreeViewItemWrapper> collection = new List<InternalTreeView.TreeViewItemWrapper>();
				this.CreateWrappers(this.m_ItemWrappers[index].item.children, this.m_ItemWrappers[index].depth + 1, ref collection);
				this.m_ItemWrappers.InsertRange(index + 1, collection);
				this.m_ExpandedItemIds.Add(this.m_ItemWrappers[index].item.id);
				this.ListViewRefresh();
				base.SaveViewData();
			}
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x00039BD8 File Offset: 0x00037DD8
		private void ToggleExpandedState(ChangeEvent<bool> evt)
		{
			Toggle toggle = evt.target as Toggle;
			int index = (int)toggle.userData;
			bool flag = this.IsExpandedByIndex(index);
			Assert.AreNotEqual<bool>(flag, evt.newValue);
			bool flag2 = flag;
			if (flag2)
			{
				this.CollapseItemByIndex(index);
			}
			else
			{
				this.ExpandItemByIndex(index);
			}
			this.m_ScrollView.contentContainer.Focus();
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x00039C3C File Offset: 0x00037E3C
		private void CreateWrappers(IEnumerable<ITreeViewItem> treeViewItems, int depth, ref List<InternalTreeView.TreeViewItemWrapper> wrappers)
		{
			foreach (ITreeViewItem treeViewItem in treeViewItems)
			{
				InternalTreeView.TreeViewItemWrapper item = new InternalTreeView.TreeViewItemWrapper
				{
					depth = depth,
					item = treeViewItem
				};
				wrappers.Add(item);
				bool flag = this.m_ExpandedItemIds.Contains(treeViewItem.id) && treeViewItem.hasChildren;
				if (flag)
				{
					this.CreateWrappers(treeViewItem.children, depth + 1, ref wrappers);
				}
			}
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x00039CD8 File Offset: 0x00037ED8
		public void CollapseAll()
		{
			bool flag = this.m_ExpandedItemIds.Count == 0;
			if (!flag)
			{
				this.m_ExpandedItemIds.Clear();
				this.RegenerateWrappers();
				this.RefreshItems();
			}
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00039D14 File Offset: 0x00037F14
		private void RegenerateWrappers()
		{
			this.m_ItemWrappers.Clear();
			bool flag = this.m_RootItems == null;
			if (!flag)
			{
				this.CreateWrappers(this.m_RootItems, 0, ref this.m_ItemWrappers);
			}
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x00039D54 File Offset: 0x00037F54
		private void OnCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			float fixedItemHeight = this.m_ListView.fixedItemHeight;
			int num;
			bool flag = !this.m_ListView.m_ItemHeightIsInline && e.customStyle.TryGetValue(BaseVerticalCollectionView.s_ItemHeightProperty, out num);
			if (flag)
			{
				this.m_ListView.m_FixedItemHeight = (float)num;
			}
			bool flag2 = this.m_ListView.m_FixedItemHeight != fixedItemHeight;
			if (flag2)
			{
				this.m_ListView.RefreshItems();
			}
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00039DC2 File Offset: 0x00037FC2
		// Note: this type is marked as 'beforefieldinit'.
		static InternalTreeView()
		{
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x00039E00 File Offset: 0x00038000
		[CompilerGenerated]
		private int <SetSelectionInternal>b__69_0(int id)
		{
			return this.GetItemIndex(id, true);
		}

		// Token: 0x04000653 RID: 1619
		private static readonly string s_ListViewName = "unity-tree-view__list-view";

		// Token: 0x04000654 RID: 1620
		private static readonly string s_ItemToggleName = "unity-tree-view__item-toggle";

		// Token: 0x04000655 RID: 1621
		private static readonly string s_ItemIndentsContainerName = "unity-tree-view__item-indents";

		// Token: 0x04000656 RID: 1622
		private static readonly string s_ItemIndentName = "unity-tree-view__item-indent";

		// Token: 0x04000657 RID: 1623
		private static readonly string s_ItemContentContainerName = "unity-tree-view__item-content";

		// Token: 0x04000658 RID: 1624
		public static readonly string itemUssClassName = "unity-tree-view__item";

		// Token: 0x04000659 RID: 1625
		private Func<VisualElement> m_MakeItem;

		// Token: 0x0400065A RID: 1626
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<IEnumerable<ITreeViewItem>> onItemsChosen;

		// Token: 0x0400065B RID: 1627
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<IEnumerable<ITreeViewItem>> onSelectionChange;

		// Token: 0x0400065C RID: 1628
		private List<ITreeViewItem> m_SelectedItems;

		// Token: 0x0400065D RID: 1629
		private Action<VisualElement, ITreeViewItem> m_BindItem;

		// Token: 0x0400065E RID: 1630
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<VisualElement, ITreeViewItem> <unbindItem>k__BackingField;

		// Token: 0x0400065F RID: 1631
		private IList<ITreeViewItem> m_RootItems;

		// Token: 0x04000660 RID: 1632
		[SerializeField]
		private List<int> m_ExpandedItemIds;

		// Token: 0x04000661 RID: 1633
		private List<InternalTreeView.TreeViewItemWrapper> m_ItemWrappers;

		// Token: 0x04000662 RID: 1634
		private readonly ListView m_ListView;

		// Token: 0x04000663 RID: 1635
		internal readonly ScrollView m_ScrollView;

		// Token: 0x0200019E RID: 414
		public new class UxmlFactory : UxmlFactory<InternalTreeView, InternalTreeView.UxmlTraits>
		{
			// Token: 0x06000DBE RID: 3518 RVA: 0x00039E0A File Offset: 0x0003800A
			public UxmlFactory()
			{
			}
		}

		// Token: 0x0200019F RID: 415
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x170002CF RID: 719
			// (get) Token: 0x06000DBF RID: 3519 RVA: 0x00039E14 File Offset: 0x00038014
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}

			// Token: 0x06000DC0 RID: 3520 RVA: 0x00039E34 File Offset: 0x00038034
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				int itemHeight = 0;
				bool flag = this.m_ItemHeight.TryGetValueFromBag(bag, cc, ref itemHeight);
				if (flag)
				{
					((InternalTreeView)ve).itemHeight = itemHeight;
				}
				((InternalTreeView)ve).showBorder = this.m_ShowBorder.GetValueFromBag(bag, cc);
				((InternalTreeView)ve).selectionType = this.m_SelectionType.GetValueFromBag(bag, cc);
				((InternalTreeView)ve).showAlternatingRowBackgrounds = this.m_ShowAlternatingRowBackgrounds.GetValueFromBag(bag, cc);
			}

			// Token: 0x06000DC1 RID: 3521 RVA: 0x00039EBC File Offset: 0x000380BC
			public UxmlTraits()
			{
			}

			// Token: 0x04000664 RID: 1636
			private readonly UxmlIntAttributeDescription m_ItemHeight = new UxmlIntAttributeDescription
			{
				name = "item-height",
				defaultValue = BaseVerticalCollectionView.s_DefaultItemHeight
			};

			// Token: 0x04000665 RID: 1637
			private readonly UxmlBoolAttributeDescription m_ShowBorder = new UxmlBoolAttributeDescription
			{
				name = "show-border",
				defaultValue = false
			};

			// Token: 0x04000666 RID: 1638
			private readonly UxmlEnumAttributeDescription<SelectionType> m_SelectionType = new UxmlEnumAttributeDescription<SelectionType>
			{
				name = "selection-type",
				defaultValue = SelectionType.Single
			};

			// Token: 0x04000667 RID: 1639
			private readonly UxmlEnumAttributeDescription<AlternatingRowBackground> m_ShowAlternatingRowBackgrounds = new UxmlEnumAttributeDescription<AlternatingRowBackground>
			{
				name = "show-alternating-row-backgrounds",
				defaultValue = AlternatingRowBackground.None
			};

			// Token: 0x020001A0 RID: 416
			[CompilerGenerated]
			private sealed class <get_uxmlChildElementsDescription>d__5 : IEnumerable<UxmlChildElementDescription>, IEnumerable, IEnumerator<UxmlChildElementDescription>, IEnumerator, IDisposable
			{
				// Token: 0x06000DC2 RID: 3522 RVA: 0x00039F50 File Offset: 0x00038150
				[DebuggerHidden]
				public <get_uxmlChildElementsDescription>d__5(int <>1__state)
				{
					this.<>1__state = <>1__state;
					this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
				}

				// Token: 0x06000DC3 RID: 3523 RVA: 0x000080DB File Offset: 0x000062DB
				[DebuggerHidden]
				void IDisposable.Dispose()
				{
				}

				// Token: 0x06000DC4 RID: 3524 RVA: 0x00039F70 File Offset: 0x00038170
				bool IEnumerator.MoveNext()
				{
					int num = this.<>1__state;
					if (num != 0)
					{
						return false;
					}
					this.<>1__state = -1;
					return false;
				}

				// Token: 0x170002D0 RID: 720
				// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x00039F96 File Offset: 0x00038196
				UxmlChildElementDescription IEnumerator<UxmlChildElementDescription>.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x06000DC6 RID: 3526 RVA: 0x0000810E File Offset: 0x0000630E
				[DebuggerHidden]
				void IEnumerator.Reset()
				{
					throw new NotSupportedException();
				}

				// Token: 0x170002D1 RID: 721
				// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x00039F96 File Offset: 0x00038196
				object IEnumerator.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x06000DC8 RID: 3528 RVA: 0x00039FA0 File Offset: 0x000381A0
				[DebuggerHidden]
				IEnumerator<UxmlChildElementDescription> IEnumerable<UxmlChildElementDescription>.GetEnumerator()
				{
					InternalTreeView.UxmlTraits.<get_uxmlChildElementsDescription>d__5 <get_uxmlChildElementsDescription>d__;
					if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
					{
						this.<>1__state = 0;
						<get_uxmlChildElementsDescription>d__ = this;
					}
					else
					{
						<get_uxmlChildElementsDescription>d__ = new InternalTreeView.UxmlTraits.<get_uxmlChildElementsDescription>d__5(0);
						<get_uxmlChildElementsDescription>d__.<>4__this = this;
					}
					return <get_uxmlChildElementsDescription>d__;
				}

				// Token: 0x06000DC9 RID: 3529 RVA: 0x00039FE8 File Offset: 0x000381E8
				[DebuggerHidden]
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.System.Collections.Generic.IEnumerable<UnityEngine.UIElements.UxmlChildElementDescription>.GetEnumerator();
				}

				// Token: 0x04000668 RID: 1640
				private int <>1__state;

				// Token: 0x04000669 RID: 1641
				private UxmlChildElementDescription <>2__current;

				// Token: 0x0400066A RID: 1642
				private int <>l__initialThreadId;

				// Token: 0x0400066B RID: 1643
				public InternalTreeView.UxmlTraits <>4__this;
			}
		}

		// Token: 0x020001A1 RID: 417
		private struct TreeViewItemWrapper
		{
			// Token: 0x170002D2 RID: 722
			// (get) Token: 0x06000DCA RID: 3530 RVA: 0x00039FF0 File Offset: 0x000381F0
			public int id
			{
				get
				{
					return this.item.id;
				}
			}

			// Token: 0x170002D3 RID: 723
			// (get) Token: 0x06000DCB RID: 3531 RVA: 0x00039FFD File Offset: 0x000381FD
			public bool hasChildren
			{
				get
				{
					return this.item.hasChildren;
				}
			}

			// Token: 0x0400066C RID: 1644
			public int depth;

			// Token: 0x0400066D RID: 1645
			public ITreeViewItem item;
		}

		// Token: 0x020001A2 RID: 418
		[CompilerGenerated]
		private sealed class <GetAllItems>d__64 : IEnumerable<ITreeViewItem>, IEnumerable, IEnumerator<ITreeViewItem>, IEnumerator, IDisposable
		{
			// Token: 0x06000DCC RID: 3532 RVA: 0x0003A00A File Offset: 0x0003820A
			[DebuggerHidden]
			public <GetAllItems>d__64(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06000DCD RID: 3533 RVA: 0x000080DB File Offset: 0x000062DB
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000DCE RID: 3534 RVA: 0x0003A02C File Offset: 0x0003822C
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					bool hasChildren = currentItem.hasChildren;
					if (hasChildren)
					{
						iteratorStack.Push(currentIterator);
						currentIterator = currentItem.children.GetEnumerator();
					}
					currentItem = null;
				}
				else
				{
					this.<>1__state = -1;
					bool flag = rootItems == null;
					if (flag)
					{
						return false;
					}
					iteratorStack = new Stack<IEnumerator<ITreeViewItem>>();
					currentIterator = rootItems.GetEnumerator();
				}
				for (;;)
				{
					hasNext = currentIterator.MoveNext();
					bool flag2 = !hasNext;
					if (!flag2)
					{
						goto IL_9B;
					}
					bool flag3 = iteratorStack.Count > 0;
					if (!flag3)
					{
						break;
					}
					currentIterator = iteratorStack.Pop();
				}
				return false;
				IL_9B:
				currentItem = currentIterator.Current;
				this.<>2__current = currentItem;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170002D4 RID: 724
			// (get) Token: 0x06000DCF RID: 3535 RVA: 0x0003A14D File Offset: 0x0003834D
			ITreeViewItem IEnumerator<ITreeViewItem>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000DD0 RID: 3536 RVA: 0x0000810E File Offset: 0x0000630E
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170002D5 RID: 725
			// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x0003A14D File Offset: 0x0003834D
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000DD2 RID: 3538 RVA: 0x0003A158 File Offset: 0x00038358
			[DebuggerHidden]
			IEnumerator<ITreeViewItem> IEnumerable<ITreeViewItem>.GetEnumerator()
			{
				InternalTreeView.<GetAllItems>d__64 <GetAllItems>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<GetAllItems>d__ = this;
				}
				else
				{
					<GetAllItems>d__ = new InternalTreeView.<GetAllItems>d__64(0);
				}
				<GetAllItems>d__.rootItems = rootItems;
				return <GetAllItems>d__;
			}

			// Token: 0x06000DD3 RID: 3539 RVA: 0x0003A1A0 File Offset: 0x000383A0
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<UnityEngine.UIElements.ITreeViewItem>.GetEnumerator();
			}

			// Token: 0x0400066E RID: 1646
			private int <>1__state;

			// Token: 0x0400066F RID: 1647
			private ITreeViewItem <>2__current;

			// Token: 0x04000670 RID: 1648
			private int <>l__initialThreadId;

			// Token: 0x04000671 RID: 1649
			private IEnumerable<ITreeViewItem> rootItems;

			// Token: 0x04000672 RID: 1650
			public IEnumerable<ITreeViewItem> <>3__rootItems;

			// Token: 0x04000673 RID: 1651
			private Stack<IEnumerator<ITreeViewItem>> <iteratorStack>5__1;

			// Token: 0x04000674 RID: 1652
			private IEnumerator<ITreeViewItem> <currentIterator>5__2;

			// Token: 0x04000675 RID: 1653
			private bool <hasNext>5__3;

			// Token: 0x04000676 RID: 1654
			private ITreeViewItem <currentItem>5__4;
		}
	}
}
