using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace UnityEngine.UIElements
{
	// Token: 0x02000124 RID: 292
	public abstract class BaseVerticalCollectionView : BindableElement, ISerializationCallbackReceiver
	{
		// Token: 0x1400001A RID: 26
		// (add) Token: 0x0600098F RID: 2447 RVA: 0x000264B0 File Offset: 0x000246B0
		// (remove) Token: 0x06000990 RID: 2448 RVA: 0x000264E8 File Offset: 0x000246E8
		[Obsolete("onItemChosen is deprecated, use onItemsChosen instead", true)]
		public event Action<object> onItemChosen
		{
			[CompilerGenerated]
			add
			{
				Action<object> action = this.onItemChosen;
				Action<object> action2;
				do
				{
					action2 = action;
					Action<object> value2 = (Action<object>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<object>>(ref this.onItemChosen, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<object> action = this.onItemChosen;
				Action<object> action2;
				do
				{
					action2 = action;
					Action<object> value2 = (Action<object>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<object>>(ref this.onItemChosen, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000991 RID: 2449 RVA: 0x00026520 File Offset: 0x00024720
		// (remove) Token: 0x06000992 RID: 2450 RVA: 0x00026558 File Offset: 0x00024758
		public event Action<IEnumerable<object>> onItemsChosen
		{
			[CompilerGenerated]
			add
			{
				Action<IEnumerable<object>> action = this.onItemsChosen;
				Action<IEnumerable<object>> action2;
				do
				{
					action2 = action;
					Action<IEnumerable<object>> value2 = (Action<IEnumerable<object>>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<IEnumerable<object>>>(ref this.onItemsChosen, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<IEnumerable<object>> action = this.onItemsChosen;
				Action<IEnumerable<object>> action2;
				do
				{
					action2 = action;
					Action<IEnumerable<object>> value2 = (Action<IEnumerable<object>>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<IEnumerable<object>>>(ref this.onItemsChosen, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000993 RID: 2451 RVA: 0x00026590 File Offset: 0x00024790
		// (remove) Token: 0x06000994 RID: 2452 RVA: 0x000265C8 File Offset: 0x000247C8
		[Obsolete("onSelectionChanged is deprecated, use onSelectionChange instead", true)]
		public event Action<List<object>> onSelectionChanged
		{
			[CompilerGenerated]
			add
			{
				Action<List<object>> action = this.onSelectionChanged;
				Action<List<object>> action2;
				do
				{
					action2 = action;
					Action<List<object>> value2 = (Action<List<object>>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<List<object>>>(ref this.onSelectionChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<List<object>> action = this.onSelectionChanged;
				Action<List<object>> action2;
				do
				{
					action2 = action;
					Action<List<object>> value2 = (Action<List<object>>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<List<object>>>(ref this.onSelectionChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000995 RID: 2453 RVA: 0x00026600 File Offset: 0x00024800
		// (remove) Token: 0x06000996 RID: 2454 RVA: 0x00026638 File Offset: 0x00024838
		public event Action<IEnumerable<object>> onSelectionChange
		{
			[CompilerGenerated]
			add
			{
				Action<IEnumerable<object>> action = this.onSelectionChange;
				Action<IEnumerable<object>> action2;
				do
				{
					action2 = action;
					Action<IEnumerable<object>> value2 = (Action<IEnumerable<object>>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<IEnumerable<object>>>(ref this.onSelectionChange, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<IEnumerable<object>> action = this.onSelectionChange;
				Action<IEnumerable<object>> action2;
				do
				{
					action2 = action;
					Action<IEnumerable<object>> value2 = (Action<IEnumerable<object>>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<IEnumerable<object>>>(ref this.onSelectionChange, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000997 RID: 2455 RVA: 0x00026670 File Offset: 0x00024870
		// (remove) Token: 0x06000998 RID: 2456 RVA: 0x000266A8 File Offset: 0x000248A8
		public event Action<IEnumerable<int>> onSelectedIndicesChange
		{
			[CompilerGenerated]
			add
			{
				Action<IEnumerable<int>> action = this.onSelectedIndicesChange;
				Action<IEnumerable<int>> action2;
				do
				{
					action2 = action;
					Action<IEnumerable<int>> value2 = (Action<IEnumerable<int>>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<IEnumerable<int>>>(ref this.onSelectedIndicesChange, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<IEnumerable<int>> action = this.onSelectedIndicesChange;
				Action<IEnumerable<int>> action2;
				do
				{
					action2 = action;
					Action<IEnumerable<int>> value2 = (Action<IEnumerable<int>>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<IEnumerable<int>>>(ref this.onSelectedIndicesChange, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000999 RID: 2457 RVA: 0x000266E0 File Offset: 0x000248E0
		// (remove) Token: 0x0600099A RID: 2458 RVA: 0x00026718 File Offset: 0x00024918
		public event Action<int, int> itemIndexChanged
		{
			[CompilerGenerated]
			add
			{
				Action<int, int> action = this.itemIndexChanged;
				Action<int, int> action2;
				do
				{
					action2 = action;
					Action<int, int> value2 = (Action<int, int>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<int, int>>(ref this.itemIndexChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<int, int> action = this.itemIndexChanged;
				Action<int, int> action2;
				do
				{
					action2 = action;
					Action<int, int> value2 = (Action<int, int>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<int, int>>(ref this.itemIndexChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x0600099B RID: 2459 RVA: 0x00026750 File Offset: 0x00024950
		// (remove) Token: 0x0600099C RID: 2460 RVA: 0x00026788 File Offset: 0x00024988
		public event Action itemsSourceChanged
		{
			[CompilerGenerated]
			add
			{
				Action action = this.itemsSourceChanged;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.itemsSourceChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.itemsSourceChanged;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.itemsSourceChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x000267BD File Offset: 0x000249BD
		// (set) Token: 0x0600099E RID: 2462 RVA: 0x000267C5 File Offset: 0x000249C5
		internal Func<int, int> getItemId
		{
			get
			{
				return this.m_GetItemId;
			}
			set
			{
				this.m_GetItemId = value;
				this.RefreshItems();
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x000267D6 File Offset: 0x000249D6
		// (set) Token: 0x060009A0 RID: 2464 RVA: 0x000267EA File Offset: 0x000249EA
		public IList itemsSource
		{
			get
			{
				CollectionViewController viewController = this.viewController;
				return (viewController != null) ? viewController.itemsSource : null;
			}
			set
			{
				this.GetOrCreateViewController().itemsSource = value;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x000267F9 File Offset: 0x000249F9
		// (set) Token: 0x060009A2 RID: 2466 RVA: 0x00026804 File Offset: 0x00024A04
		public Func<VisualElement> makeItem
		{
			get
			{
				return this.m_MakeItem;
			}
			set
			{
				bool flag = value != this.m_MakeItem;
				if (flag)
				{
					this.m_MakeItem = value;
					this.Rebuild();
				}
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x00026832 File Offset: 0x00024A32
		// (set) Token: 0x060009A4 RID: 2468 RVA: 0x0002683C File Offset: 0x00024A3C
		public Action<VisualElement, int> bindItem
		{
			get
			{
				return this.m_BindItem;
			}
			set
			{
				bool flag = value != this.m_BindItem;
				if (flag)
				{
					this.m_BindItem = value;
					this.RefreshItems();
				}
			}
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0002686A File Offset: 0x00024A6A
		internal void SetMakeItemWithoutNotify(Func<VisualElement> func)
		{
			this.m_MakeItem = func;
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00026874 File Offset: 0x00024A74
		internal void SetBindItemWithoutNotify(Action<VisualElement, int> callback)
		{
			this.m_BindItem = callback;
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x0002687E File Offset: 0x00024A7E
		// (set) Token: 0x060009A8 RID: 2472 RVA: 0x00026886 File Offset: 0x00024A86
		public Action<VisualElement, int> unbindItem
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

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x0002688F File Offset: 0x00024A8F
		// (set) Token: 0x060009AA RID: 2474 RVA: 0x00026897 File Offset: 0x00024A97
		public Action<VisualElement> destroyItem
		{
			[CompilerGenerated]
			get
			{
				return this.<destroyItem>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<destroyItem>k__BackingField = value;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x000268A0 File Offset: 0x00024AA0
		public override VisualElement contentContainer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x000268A4 File Offset: 0x00024AA4
		// (set) Token: 0x060009AD RID: 2477 RVA: 0x000268BC File Offset: 0x00024ABC
		public SelectionType selectionType
		{
			get
			{
				return this.m_SelectionType;
			}
			set
			{
				this.m_SelectionType = value;
				bool flag = this.m_SelectionType == SelectionType.None;
				if (flag)
				{
					this.ClearSelection();
				}
				else
				{
					bool flag2 = this.m_SelectionType == SelectionType.Single;
					if (flag2)
					{
						bool flag3 = this.m_SelectedIndices.Count > 1;
						if (flag3)
						{
							this.SetSelection(this.m_SelectedIndices.First<int>());
						}
					}
				}
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x0002691E File Offset: 0x00024B1E
		public object selectedItem
		{
			get
			{
				return (this.m_SelectedItems.Count == 0) ? null : this.m_SelectedItems.First<object>();
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x0002693B File Offset: 0x00024B3B
		public IEnumerable<object> selectedItems
		{
			get
			{
				return this.m_SelectedItems;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x00026944 File Offset: 0x00024B44
		// (set) Token: 0x060009B1 RID: 2481 RVA: 0x00026971 File Offset: 0x00024B71
		public int selectedIndex
		{
			get
			{
				return (this.m_SelectedIndices.Count == 0) ? -1 : this.m_SelectedIndices.First<int>();
			}
			set
			{
				this.SetSelection(value);
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x0002697C File Offset: 0x00024B7C
		public IEnumerable<int> selectedIndices
		{
			get
			{
				return this.m_SelectedIndices;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x00026984 File Offset: 0x00024B84
		internal List<int> selectedIds
		{
			get
			{
				return this.m_SelectedIds;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x0002698C File Offset: 0x00024B8C
		internal IEnumerable<ReusableCollectionItem> activeItems
		{
			get
			{
				CollectionVirtualizationController virtualizationController = this.m_VirtualizationController;
				return ((virtualizationController != null) ? virtualizationController.activeItems : null) ?? BaseVerticalCollectionView.k_EmptyItems;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x000269A9 File Offset: 0x00024BA9
		internal ScrollView scrollView
		{
			get
			{
				return this.m_ScrollView;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x000269B1 File Offset: 0x00024BB1
		internal ListViewDragger dragger
		{
			get
			{
				return this.m_Dragger;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x000269B9 File Offset: 0x00024BB9
		internal CollectionViewController viewController
		{
			get
			{
				return this.m_ViewController;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x000269C1 File Offset: 0x00024BC1
		internal CollectionVirtualizationController virtualizationController
		{
			get
			{
				return this.GetOrCreateVirtualizationController();
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x000269C9 File Offset: 0x00024BC9
		[Obsolete("resolvedItemHeight is deprecated and will be removed from the API.", false)]
		public float resolvedItemHeight
		{
			get
			{
				return this.ResolveItemHeight(-1f);
			}
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x000269D8 File Offset: 0x00024BD8
		internal float ResolveItemHeight(float height = -1f)
		{
			float scaledPixelsPerPoint = base.scaledPixelsPerPoint;
			height = ((height < 0f) ? this.fixedItemHeight : height);
			return Mathf.Round(height * scaledPixelsPerPoint) / scaledPixelsPerPoint;
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x00026A0E File Offset: 0x00024C0E
		// (set) Token: 0x060009BC RID: 2492 RVA: 0x00026A20 File Offset: 0x00024C20
		public bool showBorder
		{
			get
			{
				return this.m_ScrollView.ClassListContains(BaseVerticalCollectionView.borderUssClassName);
			}
			set
			{
				this.m_ScrollView.EnableInClassList(BaseVerticalCollectionView.borderUssClassName, value);
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x00026A34 File Offset: 0x00024C34
		// (set) Token: 0x060009BE RID: 2494 RVA: 0x00026A7C File Offset: 0x00024C7C
		public bool reorderable
		{
			get
			{
				ListViewDragger dragger = this.m_Dragger;
				bool? flag;
				if (dragger == null)
				{
					flag = null;
				}
				else
				{
					ICollectionDragAndDropController dragAndDropController = dragger.dragAndDropController;
					flag = ((dragAndDropController != null) ? new bool?(dragAndDropController.enableReordering) : null);
				}
				bool? flag2 = flag;
				return flag2.GetValueOrDefault();
			}
			set
			{
				ICollectionDragAndDropController dragAndDropController = this.m_Dragger.dragAndDropController;
				bool flag = dragAndDropController != null && dragAndDropController.enableReordering != value;
				if (flag)
				{
					dragAndDropController.enableReordering = value;
					this.Rebuild();
				}
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x00026AC0 File Offset: 0x00024CC0
		// (set) Token: 0x060009C0 RID: 2496 RVA: 0x00026AD8 File Offset: 0x00024CD8
		public bool horizontalScrollingEnabled
		{
			get
			{
				return this.m_HorizontalScrollingEnabled;
			}
			set
			{
				bool flag = this.m_HorizontalScrollingEnabled == value;
				if (!flag)
				{
					this.m_HorizontalScrollingEnabled = value;
					this.m_ScrollView.mode = (value ? ScrollViewMode.VerticalAndHorizontal : ScrollViewMode.Vertical);
				}
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x00026B10 File Offset: 0x00024D10
		// (set) Token: 0x060009C2 RID: 2498 RVA: 0x00026B28 File Offset: 0x00024D28
		public AlternatingRowBackground showAlternatingRowBackgrounds
		{
			get
			{
				return this.m_ShowAlternatingRowBackgrounds;
			}
			set
			{
				bool flag = this.m_ShowAlternatingRowBackgrounds == value;
				if (!flag)
				{
					this.m_ShowAlternatingRowBackgrounds = value;
					this.RefreshItems();
				}
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x00026B53 File Offset: 0x00024D53
		// (set) Token: 0x060009C4 RID: 2500 RVA: 0x00026B5C File Offset: 0x00024D5C
		public CollectionVirtualizationMethod virtualizationMethod
		{
			get
			{
				return this.m_VirtualizationMethod;
			}
			set
			{
				CollectionVirtualizationMethod virtualizationMethod = this.m_VirtualizationMethod;
				this.m_VirtualizationMethod = value;
				bool flag = virtualizationMethod != value;
				if (flag)
				{
					this.CreateVirtualizationController();
					this.Rebuild();
				}
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00026B93 File Offset: 0x00024D93
		// (set) Token: 0x060009C6 RID: 2502 RVA: 0x00026B9C File Offset: 0x00024D9C
		[Obsolete("itemHeight is deprecated, use fixedItemHeight instead.", false)]
		public int itemHeight
		{
			get
			{
				return (int)this.fixedItemHeight;
			}
			set
			{
				this.fixedItemHeight = (float)value;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x00026BA7 File Offset: 0x00024DA7
		// (set) Token: 0x060009C8 RID: 2504 RVA: 0x00026BB0 File Offset: 0x00024DB0
		public float fixedItemHeight
		{
			get
			{
				return this.m_FixedItemHeight;
			}
			set
			{
				bool flag = value < 0f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("fixedItemHeight", "Value needs to be positive for virtualization.");
				}
				this.m_ItemHeightIsInline = true;
				bool flag2 = Math.Abs(this.m_FixedItemHeight - value) > float.Epsilon;
				if (flag2)
				{
					this.m_FixedItemHeight = value;
					this.RefreshItems();
				}
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x00026C09 File Offset: 0x00024E09
		internal float lastHeight
		{
			get
			{
				return this.m_LastHeight;
			}
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00026C11 File Offset: 0x00024E11
		private protected virtual void CreateVirtualizationController()
		{
			this.CreateVirtualizationController<ReusableCollectionItem>();
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00026C1C File Offset: 0x00024E1C
		internal CollectionVirtualizationController GetOrCreateVirtualizationController()
		{
			bool flag = this.m_VirtualizationController == null;
			if (flag)
			{
				this.CreateVirtualizationController();
			}
			return this.m_VirtualizationController;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00026C48 File Offset: 0x00024E48
		internal void CreateVirtualizationController<T>() where T : ReusableCollectionItem, new()
		{
			CollectionVirtualizationMethod virtualizationMethod = this.virtualizationMethod;
			CollectionVirtualizationMethod collectionVirtualizationMethod = virtualizationMethod;
			if (collectionVirtualizationMethod != CollectionVirtualizationMethod.FixedHeight)
			{
				if (collectionVirtualizationMethod != CollectionVirtualizationMethod.DynamicHeight)
				{
					throw new ArgumentOutOfRangeException("virtualizationMethod", this.virtualizationMethod, "Unsupported virtualizationMethod virtualization");
				}
				this.m_VirtualizationController = new DynamicHeightVirtualizationController<T>(this);
			}
			else
			{
				this.m_VirtualizationController = new FixedHeightVirtualizationController<T>(this);
			}
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00026CA4 File Offset: 0x00024EA4
		internal CollectionViewController GetOrCreateViewController()
		{
			bool flag = this.m_ViewController == null;
			if (flag)
			{
				this.CreateViewController();
			}
			return this.m_ViewController;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00026CD2 File Offset: 0x00024ED2
		private protected virtual void CreateViewController()
		{
			this.SetViewController(new CollectionViewController());
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00026CE4 File Offset: 0x00024EE4
		internal void SetViewController(CollectionViewController controller)
		{
			bool flag = this.m_ViewController != null;
			if (flag)
			{
				this.m_ViewController.itemIndexChanged -= this.m_ItemIndexChangedCallback;
				this.m_ViewController.itemsSourceChanged -= this.m_ItemsSourceChangedCallback;
			}
			this.m_ViewController = controller;
			bool flag2 = this.m_ViewController != null;
			if (flag2)
			{
				this.m_ViewController.SetView(this);
				this.m_ViewController.itemIndexChanged += this.m_ItemIndexChangedCallback;
				this.m_ViewController.itemsSourceChanged += this.m_ItemsSourceChangedCallback;
			}
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00026D6C File Offset: 0x00024F6C
		internal virtual ListViewDragger CreateDragger()
		{
			return new ListViewDragger(this);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00026D84 File Offset: 0x00024F84
		internal void InitializeDragAndDropController(bool enableReordering)
		{
			bool flag = this.m_Dragger != null;
			if (flag)
			{
				this.m_Dragger.UnregisterCallbacksFromTarget(true);
				this.m_Dragger.dragAndDropController = null;
				this.m_Dragger = null;
			}
			this.m_Dragger = this.CreateDragger();
			this.m_Dragger.dragAndDropController = this.CreateDragAndDropController();
			bool flag2 = this.m_Dragger.dragAndDropController == null;
			if (!flag2)
			{
				this.m_Dragger.dragAndDropController.enableReordering = enableReordering;
			}
		}

		// Token: 0x060009D2 RID: 2514
		internal abstract ICollectionDragAndDropController CreateDragAndDropController();

		// Token: 0x060009D3 RID: 2515 RVA: 0x00026E06 File Offset: 0x00025006
		internal void SetDragAndDropController(ICollectionDragAndDropController dragAndDropController)
		{
			if (this.m_Dragger == null)
			{
				this.m_Dragger = this.CreateDragger();
			}
			this.m_Dragger.dragAndDropController = dragAndDropController;
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00026E2C File Offset: 0x0002502C
		internal ICollectionDragAndDropController GetDragAndDropController()
		{
			ListViewDragger dragger = this.m_Dragger;
			return (dragger != null) ? dragger.dragAndDropController : null;
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00026E50 File Offset: 0x00025050
		public BaseVerticalCollectionView()
		{
			base.AddToClassList(BaseVerticalCollectionView.ussClassName);
			this.selectionType = SelectionType.Single;
			this.m_ScrollOffset = Vector2.zero;
			this.m_ScrollView = new ScrollView();
			this.m_ScrollView.AddToClassList(BaseVerticalCollectionView.listScrollViewUssClassName);
			this.m_ScrollView.verticalScroller.valueChanged += delegate(float v)
			{
				this.OnScroll(new Vector2(0f, v));
			};
			this.m_ScrollView.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnSizeChanged), TrickleDown.NoTrickleDown);
			base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), TrickleDown.NoTrickleDown);
			this.m_ScrollView.contentContainer.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
			this.m_ScrollView.contentContainer.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnDetachFromPanel), TrickleDown.NoTrickleDown);
			base.hierarchy.Add(this.m_ScrollView);
			this.m_ScrollView.contentContainer.focusable = true;
			this.m_ScrollView.contentContainer.usageHints &= ~UsageHints.GroupTransform;
			this.m_ScrollView.viewDataKey = "unity-vertical-collection-scroll-view";
			this.m_ScrollView.verticalScroller.viewDataKey = null;
			this.m_ScrollView.horizontalScroller.viewDataKey = null;
			base.focusable = true;
			base.isCompositeRoot = true;
			base.delegatesFocus = true;
			this.m_ItemIndexChangedCallback = new Action<int, int>(this.OnItemIndexChanged);
			this.m_ItemsSourceChangedCallback = new Action(this.OnItemsSourceChanged);
			this.InitializeDragAndDropController(false);
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00027020 File Offset: 0x00025220
		public BaseVerticalCollectionView(IList itemsSource, float itemHeight = -1f, Func<VisualElement> makeItem = null, Action<VisualElement, int> bindItem = null) : this()
		{
			bool flag = Math.Abs(itemHeight - -1f) > float.Epsilon;
			if (flag)
			{
				this.m_FixedItemHeight = itemHeight;
				this.m_ItemHeightIsInline = true;
			}
			this.itemsSource = itemsSource;
			this.makeItem = makeItem;
			this.bindItem = bindItem;
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00027078 File Offset: 0x00025278
		public VisualElement GetRootElementForId(int id)
		{
			ReusableCollectionItem reusableCollectionItem = this.activeItems.FirstOrDefault((ReusableCollectionItem t) => t.id == id);
			return (reusableCollectionItem != null) ? reusableCollectionItem.rootElement : null;
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x000270BC File Offset: 0x000252BC
		public VisualElement GetRootElementForIndex(int index)
		{
			return this.GetRootElementForId(this.viewController.GetIdForIndex(index));
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x000270E0 File Offset: 0x000252E0
		internal bool HasValidDataAndBindings()
		{
			return this.m_ViewController != null && this.itemsSource != null && this.makeItem != null == (this.bindItem != null);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x00027119 File Offset: 0x00025319
		private void OnItemIndexChanged(int srcIndex, int dstIndex)
		{
			Action<int, int> action = this.itemIndexChanged;
			if (action != null)
			{
				action(srcIndex, dstIndex);
			}
			this.RefreshItems();
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00027137 File Offset: 0x00025337
		private void OnItemsSourceChanged()
		{
			Action action = this.itemsSourceChanged;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0002714C File Offset: 0x0002534C
		public void RefreshItem(int index)
		{
			foreach (ReusableCollectionItem reusableCollectionItem in this.activeItems)
			{
				bool flag = reusableCollectionItem.index == index;
				if (flag)
				{
					this.viewController.InvokeUnbindItem(reusableCollectionItem, reusableCollectionItem.index);
					this.viewController.InvokeBindItem(reusableCollectionItem, reusableCollectionItem.index);
					break;
				}
			}
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x000271D0 File Offset: 0x000253D0
		public void RefreshItems()
		{
			bool flag = this.m_ViewController == null;
			if (!flag)
			{
				this.RefreshSelection();
				this.virtualizationController.Refresh(false);
				this.PostRefresh();
			}
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00027208 File Offset: 0x00025408
		[Obsolete("Refresh() has been deprecated. Use Rebuild() instead. (UnityUpgradable) -> Rebuild()", false)]
		public void Refresh()
		{
			this.Rebuild();
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00027214 File Offset: 0x00025414
		public void Rebuild()
		{
			bool flag = this.m_ViewController == null;
			if (!flag)
			{
				this.RefreshSelection();
				this.virtualizationController.Refresh(true);
				this.PostRefresh();
			}
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0002724C File Offset: 0x0002544C
		private void RefreshSelection()
		{
			this.m_SelectedIndices.Clear();
			this.m_SelectedItems.Clear();
			CollectionViewController viewController = this.viewController;
			bool flag = ((viewController != null) ? viewController.itemsSource : null) == null;
			if (!flag)
			{
				bool flag2 = this.m_SelectedIds.Count > 0;
				if (flag2)
				{
					int count = this.viewController.itemsSource.Count;
					for (int i = 0; i < count; i++)
					{
						bool flag3 = !this.m_SelectedIds.Contains(this.viewController.GetIdForIndex(i));
						if (!flag3)
						{
							this.m_SelectedIndices.Add(i);
							this.m_SelectedItems.Add(this.viewController.GetItemForIndex(i));
						}
					}
				}
			}
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00027310 File Offset: 0x00025510
		private protected virtual void PostRefresh()
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				this.m_LastHeight = this.m_ScrollView.layout.height;
				bool flag2 = float.IsNaN(this.m_ScrollView.layout.height);
				if (!flag2)
				{
					this.Resize(this.m_ScrollView.layout.size);
				}
			}
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0002737E File Offset: 0x0002557E
		public void ScrollTo(VisualElement visualElement)
		{
			this.m_ScrollView.ScrollTo(visualElement);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00027390 File Offset: 0x00025590
		public void ScrollToItem(int index)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				this.virtualizationController.ScrollToItem(index);
			}
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x000273BC File Offset: 0x000255BC
		public void ScrollToId(int id)
		{
			int indexForId = this.viewController.GetIndexForId(id);
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				this.virtualizationController.ScrollToItem(indexForId);
			}
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x000273F4 File Offset: 0x000255F4
		private void OnScroll(Vector2 offset)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				this.virtualizationController.OnScroll(offset);
			}
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0002741E File Offset: 0x0002561E
		private void Resize(Vector2 size)
		{
			this.virtualizationController.Resize(size);
			this.m_LastHeight = size.y;
			this.virtualizationController.UpdateBackground();
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00027448 File Offset: 0x00025648
		private void OnAttachToPanel(AttachToPanelEvent evt)
		{
			bool flag = evt.destinationPanel == null;
			if (!flag)
			{
				this.m_ScrollView.contentContainer.AddManipulator(this.m_NavigationManipulator = new KeyboardNavigationManipulator(new Action<KeyboardNavigationOperation, EventBase>(this.Apply)));
				this.m_ScrollView.contentContainer.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
				this.m_ScrollView.contentContainer.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
				this.m_ScrollView.contentContainer.RegisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), TrickleDown.NoTrickleDown);
				this.m_ScrollView.contentContainer.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0002750C File Offset: 0x0002570C
		private void OnDetachFromPanel(DetachFromPanelEvent evt)
		{
			bool flag = evt.originPanel == null;
			if (!flag)
			{
				this.m_ScrollView.contentContainer.RemoveManipulator(this.m_NavigationManipulator);
				this.m_ScrollView.contentContainer.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
				this.m_ScrollView.contentContainer.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
				this.m_ScrollView.contentContainer.UnregisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), TrickleDown.NoTrickleDown);
				this.m_ScrollView.contentContainer.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x000275BB File Offset: 0x000257BB
		[Obsolete("OnKeyDown is obsolete and will be removed from ListView. Use the event system instead, i.e. SendEvent(EventBase e).", false)]
		public void OnKeyDown(KeyDownEvent evt)
		{
			this.m_NavigationManipulator.OnKeyDown(evt);
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x000275CC File Offset: 0x000257CC
		private bool Apply(KeyboardNavigationOperation op, bool shiftKey)
		{
			BaseVerticalCollectionView.<>c__DisplayClass165_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.shiftKey = shiftKey;
			bool flag = this.selectionType == SelectionType.None || !this.HasValidDataAndBindings();
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				switch (op)
				{
				case KeyboardNavigationOperation.SelectAll:
					this.SelectAll();
					return true;
				case KeyboardNavigationOperation.Cancel:
					this.ClearSelection();
					return true;
				case KeyboardNavigationOperation.Submit:
				{
					Action<IEnumerable<object>> action = this.onItemsChosen;
					if (action != null)
					{
						action(this.m_SelectedItems);
					}
					this.ScrollToItem(this.selectedIndex);
					return true;
				}
				case KeyboardNavigationOperation.Previous:
				{
					bool flag2 = this.selectedIndex > 0;
					if (flag2)
					{
						this.<Apply>g__HandleSelectionAndScroll|165_0(this.selectedIndex - 1, ref CS$<>8__locals1);
						return true;
					}
					break;
				}
				case KeyboardNavigationOperation.Next:
				{
					bool flag3 = this.selectedIndex + 1 < this.m_ViewController.itemsSource.Count;
					if (flag3)
					{
						this.<Apply>g__HandleSelectionAndScroll|165_0(this.selectedIndex + 1, ref CS$<>8__locals1);
						return true;
					}
					break;
				}
				case KeyboardNavigationOperation.PageUp:
				{
					bool flag4 = this.m_SelectedIndices.Count > 0;
					if (flag4)
					{
						int num = this.m_IsRangeSelectionDirectionUp ? this.m_SelectedIndices.Min() : this.m_SelectedIndices.Max();
						this.<Apply>g__HandleSelectionAndScroll|165_0(Mathf.Max(0, num - (this.virtualizationController.visibleItemCount - 1)), ref CS$<>8__locals1);
					}
					return true;
				}
				case KeyboardNavigationOperation.PageDown:
				{
					bool flag5 = this.m_SelectedIndices.Count > 0;
					if (flag5)
					{
						int num2 = this.m_IsRangeSelectionDirectionUp ? this.m_SelectedIndices.Min() : this.m_SelectedIndices.Max();
						this.<Apply>g__HandleSelectionAndScroll|165_0(Mathf.Min(this.viewController.itemsSource.Count - 1, num2 + (this.virtualizationController.visibleItemCount - 1)), ref CS$<>8__locals1);
					}
					return true;
				}
				case KeyboardNavigationOperation.Begin:
					this.<Apply>g__HandleSelectionAndScroll|165_0(0, ref CS$<>8__locals1);
					return true;
				case KeyboardNavigationOperation.End:
					this.<Apply>g__HandleSelectionAndScroll|165_0(this.m_ViewController.itemsSource.Count - 1, ref CS$<>8__locals1);
					return true;
				}
				result = false;
			}
			return result;
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x000277F4 File Offset: 0x000259F4
		private void Apply(KeyboardNavigationOperation op, EventBase sourceEvent)
		{
			KeyDownEvent keyDownEvent = sourceEvent as KeyDownEvent;
			bool shiftKey = keyDownEvent != null && keyDownEvent.shiftKey;
			bool flag = this.Apply(op, shiftKey);
			if (flag)
			{
				sourceEvent.StopPropagation();
				sourceEvent.PreventDefault();
			}
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00027834 File Offset: 0x00025A34
		private void OnPointerMove(PointerMoveEvent evt)
		{
			bool flag = evt.button == 0;
			if (flag)
			{
				bool flag2 = (evt.pressedButtons & 1) == 0;
				if (flag2)
				{
					this.ProcessPointerUp(evt);
				}
				else
				{
					this.ProcessPointerDown(evt);
				}
			}
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00027878 File Offset: 0x00025A78
		private void OnPointerDown(PointerDownEvent evt)
		{
			bool flag = evt.pointerType != PointerType.mouse;
			if (flag)
			{
				this.ProcessPointerDown(evt);
				base.panel.PreventCompatibilityMouseEvents(evt.pointerId);
			}
			else
			{
				this.ProcessPointerDown(evt);
			}
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x000278C4 File Offset: 0x00025AC4
		private void OnPointerCancel(PointerCancelEvent evt)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = !evt.isPrimary;
				if (!flag2)
				{
					this.ClearSelection();
				}
			}
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x000278F8 File Offset: 0x00025AF8
		private void OnPointerUp(PointerUpEvent evt)
		{
			bool flag = evt.pointerType != PointerType.mouse;
			if (flag)
			{
				this.ProcessPointerUp(evt);
				base.panel.PreventCompatibilityMouseEvents(evt.pointerId);
			}
			else
			{
				this.ProcessPointerUp(evt);
			}
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00027944 File Offset: 0x00025B44
		private void ProcessPointerDown(IPointerEvent evt)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = !evt.isPrimary;
				if (!flag2)
				{
					bool flag3 = evt.button != 0;
					if (!flag3)
					{
						bool flag4 = evt.pointerType != PointerType.mouse;
						if (flag4)
						{
							this.m_TouchDownPosition = evt.position;
						}
						else
						{
							this.DoSelect(evt.localPosition, evt.clickCount, evt.actionKey, evt.shiftKey);
						}
					}
				}
			}
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x000279C8 File Offset: 0x00025BC8
		private void ProcessPointerUp(IPointerEvent evt)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = !evt.isPrimary;
				if (!flag2)
				{
					bool flag3 = evt.button != 0;
					if (!flag3)
					{
						bool flag4 = evt.pointerType != PointerType.mouse;
						if (flag4)
						{
							bool flag5 = (evt.position - this.m_TouchDownPosition).sqrMagnitude <= 100f;
							if (flag5)
							{
								this.DoSelect(evt.localPosition, evt.clickCount, evt.actionKey, evt.shiftKey);
							}
						}
						else
						{
							int indexFromPosition = this.virtualizationController.GetIndexFromPosition(evt.localPosition);
							bool flag6 = this.selectionType == SelectionType.Multiple && !evt.shiftKey && !evt.actionKey && this.m_SelectedIndices.Count > 1 && this.m_SelectedIndices.Contains(indexFromPosition);
							if (flag6)
							{
								this.ProcessSingleClick(indexFromPosition);
							}
						}
					}
				}
			}
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00027AD8 File Offset: 0x00025CD8
		private void DoSelect(Vector2 localPosition, int clickCount, bool actionKey, bool shiftKey)
		{
			int indexFromPosition = this.virtualizationController.GetIndexFromPosition(localPosition);
			bool flag = indexFromPosition > this.viewController.itemsSource.Count - 1;
			if (!flag)
			{
				bool flag2 = this.selectionType == SelectionType.None;
				if (!flag2)
				{
					int idForIndex = this.viewController.GetIdForIndex(indexFromPosition);
					if (clickCount != 1)
					{
						if (clickCount == 2)
						{
							bool flag3 = this.onItemsChosen == null;
							if (!flag3)
							{
								bool flag4 = false;
								foreach (int num in this.selectedIndices)
								{
									bool flag5 = indexFromPosition == num;
									if (flag5)
									{
										flag4 = true;
										break;
									}
								}
								this.ProcessSingleClick(indexFromPosition);
								bool flag6 = !flag4;
								if (!flag6)
								{
									Action<IEnumerable<object>> action = this.onItemsChosen;
									if (action != null)
									{
										action(this.m_SelectedItems);
									}
								}
							}
						}
					}
					else
					{
						bool flag7 = this.selectionType == SelectionType.Multiple && actionKey;
						if (flag7)
						{
							bool flag8 = this.m_SelectedIds.Contains(idForIndex);
							if (flag8)
							{
								this.RemoveFromSelection(indexFromPosition);
							}
							else
							{
								this.AddToSelection(indexFromPosition);
							}
						}
						else
						{
							bool flag9 = this.selectionType == SelectionType.Multiple && shiftKey;
							if (flag9)
							{
								bool flag10 = this.m_SelectedIndices.Count == 0;
								if (flag10)
								{
									this.SetSelection(indexFromPosition);
								}
								else
								{
									this.DoRangeSelection(indexFromPosition);
								}
							}
							else
							{
								bool flag11 = this.selectionType == SelectionType.Multiple && this.m_SelectedIndices.Contains(indexFromPosition);
								if (!flag11)
								{
									this.SetSelection(indexFromPosition);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00027C8C File Offset: 0x00025E8C
		private void DoRangeSelection(int rangeSelectionFinalIndex)
		{
			int num = this.m_IsRangeSelectionDirectionUp ? this.m_SelectedIndices.Max() : this.m_SelectedIndices.Min();
			this.ClearSelectionWithoutValidation();
			List<int> list = new List<int>();
			this.m_IsRangeSelectionDirectionUp = (rangeSelectionFinalIndex < num);
			bool isRangeSelectionDirectionUp = this.m_IsRangeSelectionDirectionUp;
			if (isRangeSelectionDirectionUp)
			{
				for (int i = rangeSelectionFinalIndex; i <= num; i++)
				{
					list.Add(i);
				}
			}
			else
			{
				for (int j = rangeSelectionFinalIndex; j >= num; j--)
				{
					list.Add(j);
				}
			}
			this.AddToSelection(list);
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00026971 File Offset: 0x00024B71
		private void ProcessSingleClick(int clickedIndex)
		{
			this.SetSelection(clickedIndex);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00027D2C File Offset: 0x00025F2C
		internal void SelectAll()
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = this.selectionType != SelectionType.Multiple;
				if (!flag2)
				{
					for (int i = 0; i < this.m_ViewController.itemsSource.Count; i++)
					{
						int idForIndex = this.viewController.GetIdForIndex(i);
						object itemForIndex = this.viewController.GetItemForIndex(i);
						foreach (ReusableCollectionItem reusableCollectionItem in this.activeItems)
						{
							bool flag3 = reusableCollectionItem.id == idForIndex;
							if (flag3)
							{
								reusableCollectionItem.SetSelected(true);
							}
						}
						bool flag4 = !this.m_SelectedIds.Contains(idForIndex);
						if (flag4)
						{
							this.m_SelectedIds.Add(idForIndex);
							this.m_SelectedIndices.Add(i);
							this.m_SelectedItems.Add(itemForIndex);
						}
					}
					this.NotifyOfSelectionChange();
					base.SaveViewData();
				}
			}
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00027E50 File Offset: 0x00026050
		public void AddToSelection(int index)
		{
			this.AddToSelection(new int[]
			{
				index
			});
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00027E64 File Offset: 0x00026064
		internal void AddToSelection(IList<int> indexes)
		{
			bool flag = !this.HasValidDataAndBindings() || indexes == null || indexes.Count == 0;
			if (!flag)
			{
				foreach (int index in indexes)
				{
					this.AddToSelectionWithoutValidation(index);
				}
				this.NotifyOfSelectionChange();
				base.SaveViewData();
			}
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00027EE0 File Offset: 0x000260E0
		private void AddToSelectionWithoutValidation(int index)
		{
			bool flag = this.m_SelectedIndices.Contains(index);
			if (!flag)
			{
				int idForIndex = this.viewController.GetIdForIndex(index);
				object itemForIndex = this.viewController.GetItemForIndex(index);
				foreach (ReusableCollectionItem reusableCollectionItem in this.activeItems)
				{
					bool flag2 = reusableCollectionItem.id == idForIndex;
					if (flag2)
					{
						reusableCollectionItem.SetSelected(true);
					}
				}
				this.m_SelectedIds.Add(idForIndex);
				this.m_SelectedIndices.Add(index);
				this.m_SelectedItems.Add(itemForIndex);
			}
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00027F9C File Offset: 0x0002619C
		public void RemoveFromSelection(int index)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				this.RemoveFromSelectionWithoutValidation(index);
				this.NotifyOfSelectionChange();
				base.SaveViewData();
			}
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00027FD0 File Offset: 0x000261D0
		private void RemoveFromSelectionWithoutValidation(int index)
		{
			bool flag = !this.m_SelectedIndices.Contains(index);
			if (!flag)
			{
				int idForIndex = this.viewController.GetIdForIndex(index);
				object itemForIndex = this.viewController.GetItemForIndex(index);
				foreach (ReusableCollectionItem reusableCollectionItem in this.activeItems)
				{
					bool flag2 = reusableCollectionItem.id == idForIndex;
					if (flag2)
					{
						reusableCollectionItem.SetSelected(false);
					}
				}
				this.m_SelectedIds.Remove(idForIndex);
				this.m_SelectedIndices.Remove(index);
				this.m_SelectedItems.Remove(itemForIndex);
			}
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0002808C File Offset: 0x0002628C
		public void SetSelection(int index)
		{
			bool flag = index < 0;
			if (flag)
			{
				this.ClearSelection();
			}
			else
			{
				this.SetSelection(new int[]
				{
					index
				});
			}
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x000280BD File Offset: 0x000262BD
		public void SetSelection(IEnumerable<int> indices)
		{
			this.SetSelectionInternal(indices, true);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x000280C9 File Offset: 0x000262C9
		public void SetSelectionWithoutNotify(IEnumerable<int> indices)
		{
			this.SetSelectionInternal(indices, false);
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x000280D8 File Offset: 0x000262D8
		internal void SetSelectionInternal(IEnumerable<int> indices, bool sendNotification)
		{
			bool flag = !this.HasValidDataAndBindings() || indices == null;
			if (!flag)
			{
				this.ClearSelectionWithoutValidation();
				foreach (int index in indices)
				{
					this.AddToSelectionWithoutValidation(index);
				}
				if (sendNotification)
				{
					this.NotifyOfSelectionChange();
				}
				base.SaveViewData();
			}
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00028154 File Offset: 0x00026354
		private void NotifyOfSelectionChange()
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				Action<IEnumerable<object>> action = this.onSelectionChange;
				if (action != null)
				{
					action(this.m_SelectedItems);
				}
				Action<IEnumerable<int>> action2 = this.onSelectedIndicesChange;
				if (action2 != null)
				{
					action2(this.m_SelectedIndices);
				}
			}
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x000281A4 File Offset: 0x000263A4
		public void ClearSelection()
		{
			bool flag = !this.HasValidDataAndBindings() || this.m_SelectedIds.Count == 0;
			if (!flag)
			{
				this.ClearSelectionWithoutValidation();
				this.NotifyOfSelectionChange();
			}
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x000281E0 File Offset: 0x000263E0
		private void ClearSelectionWithoutValidation()
		{
			foreach (ReusableCollectionItem reusableCollectionItem in this.activeItems)
			{
				reusableCollectionItem.SetSelected(false);
			}
			this.m_SelectedIds.Clear();
			this.m_SelectedIndices.Clear();
			this.m_SelectedItems.Clear();
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00028258 File Offset: 0x00026458
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
			base.OverwriteFromViewData(this, fullHierarchicalViewDataKey);
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00028280 File Offset: 0x00026480
		protected override void ExecuteDefaultAction(EventBase evt)
		{
			base.ExecuteDefaultAction(evt);
			bool flag = evt.eventTypeId == EventBase<PointerUpEvent>.TypeId();
			if (flag)
			{
				ListViewDragger dragger = this.m_Dragger;
				if (dragger != null)
				{
					dragger.OnPointerUpEvent((PointerUpEvent)evt);
				}
			}
			else
			{
				bool flag2 = evt.eventTypeId == EventBase<FocusEvent>.TypeId();
				if (flag2)
				{
					CollectionVirtualizationController virtualizationController = this.m_VirtualizationController;
					if (virtualizationController != null)
					{
						virtualizationController.OnFocus(evt.leafTarget as VisualElement);
					}
				}
				else
				{
					bool flag3 = evt.eventTypeId == EventBase<BlurEvent>.TypeId();
					if (flag3)
					{
						BlurEvent blurEvent = evt as BlurEvent;
						CollectionVirtualizationController virtualizationController2 = this.m_VirtualizationController;
						if (virtualizationController2 != null)
						{
							virtualizationController2.OnBlur(((blurEvent != null) ? blurEvent.relatedTarget : null) as VisualElement);
						}
					}
					else
					{
						bool flag4 = evt.eventTypeId == EventBase<NavigationSubmitEvent>.TypeId();
						if (flag4)
						{
							bool flag5 = evt.target == this;
							if (flag5)
							{
								this.m_ScrollView.contentContainer.Focus();
							}
						}
					}
				}
			}
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00028370 File Offset: 0x00026570
		private void OnSizeChanged(GeometryChangedEvent evt)
		{
			bool flag = !this.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = Mathf.Approximately(evt.newRect.width, evt.oldRect.width) && Mathf.Approximately(evt.newRect.height, evt.oldRect.height);
				if (!flag2)
				{
					this.Resize(evt.newRect.size);
				}
			}
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x000283F0 File Offset: 0x000265F0
		private void OnCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			int num;
			bool flag = !this.m_ItemHeightIsInline && e.customStyle.TryGetValue(BaseVerticalCollectionView.s_ItemHeightProperty, out num);
			if (flag)
			{
				bool flag2 = Math.Abs(this.m_FixedItemHeight - (float)num) > float.Epsilon;
				if (flag2)
				{
					this.m_FixedItemHeight = (float)num;
					this.RefreshItems();
				}
			}
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00002166 File Offset: 0x00000366
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0002844B File Offset: 0x0002664B
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.RefreshItems();
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00028458 File Offset: 0x00026658
		// Note: this type is marked as 'beforefieldinit'.
		static BaseVerticalCollectionView()
		{
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00028543 File Offset: 0x00026743
		[CompilerGenerated]
		private void <.ctor>b__144_0(float v)
		{
			this.OnScroll(new Vector2(0f, v));
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00028558 File Offset: 0x00026758
		[CompilerGenerated]
		private void <Apply>g__HandleSelectionAndScroll|165_0(int index, ref BaseVerticalCollectionView.<>c__DisplayClass165_0 A_2)
		{
			bool flag = index < 0 || index >= this.m_ViewController.itemsSource.Count;
			if (!flag)
			{
				bool flag2 = (this.selectionType == SelectionType.Multiple & A_2.shiftKey) && this.m_SelectedIndices.Count != 0;
				if (flag2)
				{
					this.DoRangeSelection(index);
				}
				else
				{
					this.selectedIndex = index;
				}
				this.ScrollToItem(index);
			}
		}

		// Token: 0x0400041A RID: 1050
		internal const string internalBindingKey = "__unity-collection-view-internal-binding";

		// Token: 0x0400041B RID: 1051
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<object> onItemChosen;

		// Token: 0x0400041C RID: 1052
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<IEnumerable<object>> onItemsChosen;

		// Token: 0x0400041D RID: 1053
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<List<object>> onSelectionChanged;

		// Token: 0x0400041E RID: 1054
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<IEnumerable<object>> onSelectionChange;

		// Token: 0x0400041F RID: 1055
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<IEnumerable<int>> onSelectedIndicesChange;

		// Token: 0x04000420 RID: 1056
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<int, int> itemIndexChanged;

		// Token: 0x04000421 RID: 1057
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action itemsSourceChanged;

		// Token: 0x04000422 RID: 1058
		private Func<int, int> m_GetItemId;

		// Token: 0x04000423 RID: 1059
		private Func<VisualElement> m_MakeItem;

		// Token: 0x04000424 RID: 1060
		private Action<VisualElement, int> m_BindItem;

		// Token: 0x04000425 RID: 1061
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<VisualElement, int> <unbindItem>k__BackingField;

		// Token: 0x04000426 RID: 1062
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<VisualElement> <destroyItem>k__BackingField;

		// Token: 0x04000427 RID: 1063
		private SelectionType m_SelectionType;

		// Token: 0x04000428 RID: 1064
		[SerializeField]
		internal SerializedVirtualizationData serializedVirtualizationData = new SerializedVirtualizationData();

		// Token: 0x04000429 RID: 1065
		private static readonly List<ReusableCollectionItem> k_EmptyItems = new List<ReusableCollectionItem>();

		// Token: 0x0400042A RID: 1066
		private bool m_HorizontalScrollingEnabled;

		// Token: 0x0400042B RID: 1067
		[SerializeField]
		private AlternatingRowBackground m_ShowAlternatingRowBackgrounds = AlternatingRowBackground.None;

		// Token: 0x0400042C RID: 1068
		internal static readonly int s_DefaultItemHeight = 22;

		// Token: 0x0400042D RID: 1069
		internal float m_FixedItemHeight = (float)BaseVerticalCollectionView.s_DefaultItemHeight;

		// Token: 0x0400042E RID: 1070
		internal bool m_ItemHeightIsInline;

		// Token: 0x0400042F RID: 1071
		private CollectionVirtualizationMethod m_VirtualizationMethod;

		// Token: 0x04000430 RID: 1072
		private readonly ScrollView m_ScrollView;

		// Token: 0x04000431 RID: 1073
		private CollectionViewController m_ViewController;

		// Token: 0x04000432 RID: 1074
		private CollectionVirtualizationController m_VirtualizationController;

		// Token: 0x04000433 RID: 1075
		private KeyboardNavigationManipulator m_NavigationManipulator;

		// Token: 0x04000434 RID: 1076
		[SerializeField]
		internal Vector2 m_ScrollOffset;

		// Token: 0x04000435 RID: 1077
		[SerializeField]
		private readonly List<int> m_SelectedIds = new List<int>();

		// Token: 0x04000436 RID: 1078
		private readonly List<int> m_SelectedIndices = new List<int>();

		// Token: 0x04000437 RID: 1079
		private readonly List<object> m_SelectedItems = new List<object>();

		// Token: 0x04000438 RID: 1080
		private float m_LastHeight;

		// Token: 0x04000439 RID: 1081
		private bool m_IsRangeSelectionDirectionUp;

		// Token: 0x0400043A RID: 1082
		private ListViewDragger m_Dragger;

		// Token: 0x0400043B RID: 1083
		internal const float ItemHeightUnset = -1f;

		// Token: 0x0400043C RID: 1084
		internal static CustomStyleProperty<int> s_ItemHeightProperty = new CustomStyleProperty<int>("--unity-item-height");

		// Token: 0x0400043D RID: 1085
		private Action<int, int> m_ItemIndexChangedCallback;

		// Token: 0x0400043E RID: 1086
		private Action m_ItemsSourceChangedCallback;

		// Token: 0x0400043F RID: 1087
		public static readonly string ussClassName = "unity-collection-view";

		// Token: 0x04000440 RID: 1088
		public static readonly string borderUssClassName = BaseVerticalCollectionView.ussClassName + "--with-border";

		// Token: 0x04000441 RID: 1089
		public static readonly string itemUssClassName = BaseVerticalCollectionView.ussClassName + "__item";

		// Token: 0x04000442 RID: 1090
		public static readonly string dragHoverBarUssClassName = BaseVerticalCollectionView.ussClassName + "__drag-hover-bar";

		// Token: 0x04000443 RID: 1091
		public static readonly string dragHoverMarkerUssClassName = BaseVerticalCollectionView.ussClassName + "__drag-hover-marker";

		// Token: 0x04000444 RID: 1092
		public static readonly string itemDragHoverUssClassName = BaseVerticalCollectionView.itemUssClassName + "--drag-hover";

		// Token: 0x04000445 RID: 1093
		public static readonly string itemSelectedVariantUssClassName = BaseVerticalCollectionView.itemUssClassName + "--selected";

		// Token: 0x04000446 RID: 1094
		public static readonly string itemAlternativeBackgroundUssClassName = BaseVerticalCollectionView.itemUssClassName + "--alternative-background";

		// Token: 0x04000447 RID: 1095
		public static readonly string listScrollViewUssClassName = BaseVerticalCollectionView.ussClassName + "__scroll-view";

		// Token: 0x04000448 RID: 1096
		internal static readonly string backgroundFillUssClassName = BaseVerticalCollectionView.ussClassName + "__background-fill";

		// Token: 0x04000449 RID: 1097
		private Vector3 m_TouchDownPosition;

		// Token: 0x02000125 RID: 293
		[CompilerGenerated]
		private sealed class <>c__DisplayClass146_0
		{
			// Token: 0x06000A0B RID: 2571 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c__DisplayClass146_0()
			{
			}

			// Token: 0x06000A0C RID: 2572 RVA: 0x000285CE File Offset: 0x000267CE
			internal bool <GetRootElementForId>b__0(ReusableCollectionItem t)
			{
				return t.id == this.id;
			}

			// Token: 0x0400044A RID: 1098
			public int id;
		}

		// Token: 0x02000126 RID: 294
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <>c__DisplayClass165_0
		{
			// Token: 0x0400044B RID: 1099
			public BaseVerticalCollectionView <>4__this;

			// Token: 0x0400044C RID: 1100
			public bool shiftKey;
		}
	}
}
