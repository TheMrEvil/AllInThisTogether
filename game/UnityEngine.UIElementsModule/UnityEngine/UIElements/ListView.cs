using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace UnityEngine.UIElements
{
	// Token: 0x02000152 RID: 338
	public class ListView : BaseVerticalCollectionView
	{
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0002C3F9 File Offset: 0x0002A5F9
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x0002C404 File Offset: 0x0002A604
		public bool showBoundCollectionSize
		{
			get
			{
				return this.m_ShowBoundCollectionSize;
			}
			set
			{
				bool flag = this.m_ShowBoundCollectionSize == value;
				if (!flag)
				{
					this.m_ShowBoundCollectionSize = value;
					this.SetupArraySizeField();
				}
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0002C42F File Offset: 0x0002A62F
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x0002C438 File Offset: 0x0002A638
		public bool showFoldoutHeader
		{
			get
			{
				return this.m_ShowFoldoutHeader;
			}
			set
			{
				bool flag = this.m_ShowFoldoutHeader == value;
				if (!flag)
				{
					this.m_ShowFoldoutHeader = value;
					base.EnableInClassList(ListView.listViewWithHeaderUssClassName, value);
					bool showFoldoutHeader = this.m_ShowFoldoutHeader;
					if (showFoldoutHeader)
					{
						bool flag2 = this.m_Foldout != null;
						if (flag2)
						{
							return;
						}
						this.m_Foldout = new Foldout
						{
							name = ListView.foldoutHeaderUssClassName,
							text = this.m_HeaderTitle
						};
						this.m_Foldout.AddToClassList(ListView.foldoutHeaderUssClassName);
						this.m_Foldout.tabIndex = 1;
						base.hierarchy.Add(this.m_Foldout);
						this.m_Foldout.Add(base.scrollView);
					}
					else
					{
						bool flag3 = this.m_Foldout != null;
						if (flag3)
						{
							Foldout foldout = this.m_Foldout;
							if (foldout != null)
							{
								foldout.RemoveFromHierarchy();
							}
							this.m_Foldout = null;
							base.hierarchy.Add(base.scrollView);
						}
					}
					this.SetupArraySizeField();
					this.UpdateEmpty();
					bool showAddRemoveFooter = this.showAddRemoveFooter;
					if (showAddRemoveFooter)
					{
						this.EnableFooter(true);
					}
				}
			}
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0002C558 File Offset: 0x0002A758
		internal void SetupArraySizeField()
		{
			bool flag = !this.showBoundCollectionSize || (!this.showFoldoutHeader && base.GetProperty("__unity-collection-view-internal-binding") == null);
			if (flag)
			{
				TextField arraySizeField = this.m_ArraySizeField;
				if (arraySizeField != null)
				{
					arraySizeField.RemoveFromHierarchy();
				}
			}
			else
			{
				bool flag2 = this.m_ArraySizeField == null;
				if (flag2)
				{
					this.m_ArraySizeField = new TextField
					{
						name = ListView.arraySizeFieldUssClassName
					};
					this.m_ArraySizeField.AddToClassList(ListView.arraySizeFieldUssClassName);
					this.m_ArraySizeField.RegisterValueChangedCallback(new EventCallback<ChangeEvent<string>>(this.OnArraySizeFieldChanged));
					this.m_ArraySizeField.isDelayed = true;
					this.m_ArraySizeField.focusable = true;
				}
				this.m_ArraySizeField.EnableInClassList(ListView.arraySizeFieldWithFooterUssClassName, this.showAddRemoveFooter);
				this.m_ArraySizeField.EnableInClassList(ListView.arraySizeFieldWithHeaderUssClassName, this.showFoldoutHeader);
				bool showFoldoutHeader = this.showFoldoutHeader;
				if (showFoldoutHeader)
				{
					this.m_ArraySizeField.label = string.Empty;
					base.hierarchy.Add(this.m_ArraySizeField);
				}
				else
				{
					this.m_ArraySizeField.label = ListView.k_SizeFieldLabel;
					base.hierarchy.Insert(0, this.m_ArraySizeField);
				}
				this.UpdateArraySizeField();
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x0002C6A6 File Offset: 0x0002A8A6
		// (set) Token: 0x06000AE2 RID: 2786 RVA: 0x0002C6B0 File Offset: 0x0002A8B0
		public string headerTitle
		{
			get
			{
				return this.m_HeaderTitle;
			}
			set
			{
				this.m_HeaderTitle = value;
				bool flag = this.m_Foldout != null;
				if (flag)
				{
					this.m_Foldout.text = this.m_HeaderTitle;
				}
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x0002C6E4 File Offset: 0x0002A8E4
		// (set) Token: 0x06000AE4 RID: 2788 RVA: 0x0002C6EF File Offset: 0x0002A8EF
		public bool showAddRemoveFooter
		{
			get
			{
				return this.m_Footer != null;
			}
			set
			{
				this.EnableFooter(value);
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x0002C6F9 File Offset: 0x0002A8F9
		internal Foldout headerFoldout
		{
			get
			{
				return this.m_Foldout;
			}
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0002C704 File Offset: 0x0002A904
		private void EnableFooter(bool enabled)
		{
			base.EnableInClassList(ListView.listViewWithFooterUssClassName, enabled);
			base.scrollView.EnableInClassList(ListView.scrollViewWithFooterUssClassName, enabled);
			bool flag = this.m_ArraySizeField != null;
			if (flag)
			{
				this.m_ArraySizeField.EnableInClassList(ListView.arraySizeFieldWithFooterUssClassName, enabled);
			}
			if (enabled)
			{
				bool flag2 = this.m_Footer == null;
				if (flag2)
				{
					this.m_Footer = new VisualElement
					{
						name = ListView.footerUssClassName
					};
					this.m_Footer.AddToClassList(ListView.footerUssClassName);
					this.m_RemoveButton = new Button(new Action(this.OnRemoveClicked))
					{
						name = ListView.footerRemoveButtonName,
						text = "-"
					};
					this.m_Footer.Add(this.m_RemoveButton);
					this.m_AddButton = new Button(new Action(this.OnAddClicked))
					{
						name = ListView.footerAddButtonName,
						text = "+"
					};
					this.m_Footer.Add(this.m_AddButton);
				}
				bool flag3 = this.m_Foldout != null;
				if (flag3)
				{
					this.m_Foldout.contentContainer.Add(this.m_Footer);
				}
				else
				{
					base.hierarchy.Add(this.m_Footer);
				}
			}
			else
			{
				Button removeButton = this.m_RemoveButton;
				if (removeButton != null)
				{
					removeButton.RemoveFromHierarchy();
				}
				Button addButton = this.m_AddButton;
				if (addButton != null)
				{
					addButton.RemoveFromHierarchy();
				}
				VisualElement footer = this.m_Footer;
				if (footer != null)
				{
					footer.RemoveFromHierarchy();
				}
				this.m_RemoveButton = null;
				this.m_AddButton = null;
				this.m_Footer = null;
			}
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000AE7 RID: 2791 RVA: 0x0002C8A4 File Offset: 0x0002AAA4
		// (remove) Token: 0x06000AE8 RID: 2792 RVA: 0x0002C8DC File Offset: 0x0002AADC
		public event Action<IEnumerable<int>> itemsAdded
		{
			[CompilerGenerated]
			add
			{
				Action<IEnumerable<int>> action = this.itemsAdded;
				Action<IEnumerable<int>> action2;
				do
				{
					action2 = action;
					Action<IEnumerable<int>> value2 = (Action<IEnumerable<int>>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<IEnumerable<int>>>(ref this.itemsAdded, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<IEnumerable<int>> action = this.itemsAdded;
				Action<IEnumerable<int>> action2;
				do
				{
					action2 = action;
					Action<IEnumerable<int>> value2 = (Action<IEnumerable<int>>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<IEnumerable<int>>>(ref this.itemsAdded, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06000AE9 RID: 2793 RVA: 0x0002C914 File Offset: 0x0002AB14
		// (remove) Token: 0x06000AEA RID: 2794 RVA: 0x0002C94C File Offset: 0x0002AB4C
		public event Action<IEnumerable<int>> itemsRemoved
		{
			[CompilerGenerated]
			add
			{
				Action<IEnumerable<int>> action = this.itemsRemoved;
				Action<IEnumerable<int>> action2;
				do
				{
					action2 = action;
					Action<IEnumerable<int>> value2 = (Action<IEnumerable<int>>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<IEnumerable<int>>>(ref this.itemsRemoved, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<IEnumerable<int>> action = this.itemsRemoved;
				Action<IEnumerable<int>> action2;
				do
				{
					action2 = action;
					Action<IEnumerable<int>> value2 = (Action<IEnumerable<int>>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<IEnumerable<int>>>(ref this.itemsRemoved, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0002C981 File Offset: 0x0002AB81
		private void AddItems(int itemCount)
		{
			this.viewController.AddItems(itemCount);
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0002C991 File Offset: 0x0002AB91
		private void RemoveItems(List<int> indices)
		{
			this.viewController.RemoveItems(indices);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0002C9A4 File Offset: 0x0002ABA4
		private void OnArraySizeFieldChanged(ChangeEvent<string> evt)
		{
			int num;
			bool flag = !int.TryParse(evt.newValue, out num) || num < 0;
			if (flag)
			{
				this.m_ArraySizeField.SetValueWithoutNotify(evt.previousValue);
			}
			else
			{
				int itemsCount = this.viewController.GetItemsCount();
				bool flag2 = num > itemsCount;
				if (flag2)
				{
					this.viewController.AddItems(num - itemsCount);
				}
				else
				{
					bool flag3 = num < itemsCount;
					if (flag3)
					{
						this.viewController.RemoveItems(itemsCount - num);
					}
				}
			}
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0002CA24 File Offset: 0x0002AC24
		private void UpdateArraySizeField()
		{
			bool flag = !base.HasValidDataAndBindings();
			if (!flag)
			{
				TextField arraySizeField = this.m_ArraySizeField;
				if (arraySizeField != null)
				{
					arraySizeField.SetValueWithoutNotify(this.viewController.GetItemsCount().ToString());
				}
			}
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0002CA68 File Offset: 0x0002AC68
		private void UpdateEmpty()
		{
			bool flag = !base.HasValidDataAndBindings();
			if (!flag)
			{
				bool flag2 = base.itemsSource.Count == 0;
				if (flag2)
				{
					bool flag3 = this.m_EmptyListLabel != null;
					if (!flag3)
					{
						this.m_EmptyListLabel = new Label("List is Empty");
						this.m_EmptyListLabel.AddToClassList(ListView.emptyLabelUssClassName);
						base.scrollView.contentViewport.Add(this.m_EmptyListLabel);
					}
				}
				else
				{
					Label emptyListLabel = this.m_EmptyListLabel;
					if (emptyListLabel != null)
					{
						emptyListLabel.RemoveFromHierarchy();
					}
					this.m_EmptyListLabel = null;
				}
			}
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0002CB00 File Offset: 0x0002AD00
		private void OnAddClicked()
		{
			this.AddItems(1);
			bool flag = base.binding == null;
			if (flag)
			{
				base.SetSelection(base.itemsSource.Count - 1);
				base.ScrollToItem(-1);
			}
			else
			{
				base.schedule.Execute(delegate()
				{
					base.SetSelection(base.itemsSource.Count - 1);
					base.ScrollToItem(-1);
				}).ExecuteLater(100L);
			}
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0002CB68 File Offset: 0x0002AD68
		private void OnRemoveClicked()
		{
			bool flag = base.selectedIndices.Any<int>();
			if (flag)
			{
				this.viewController.RemoveItems(base.selectedIndices.ToList<int>());
				base.ClearSelection();
			}
			else
			{
				bool flag2 = base.itemsSource.Count > 0;
				if (flag2)
				{
					int index = base.itemsSource.Count - 1;
					this.viewController.RemoveItem(index);
				}
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x0002CBD6 File Offset: 0x0002ADD6
		internal new ListViewController viewController
		{
			get
			{
				return this.m_ListViewController;
			}
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0002CBDE File Offset: 0x0002ADDE
		private protected override void CreateVirtualizationController()
		{
			base.CreateVirtualizationController<ReusableListViewItem>();
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0002CBE8 File Offset: 0x0002ADE8
		private protected override void CreateViewController()
		{
			this.SetViewController(new ListViewController());
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0002CBF8 File Offset: 0x0002ADF8
		internal void SetViewController(ListViewController controller)
		{
			if (this.m_ItemAddedCallback == null)
			{
				this.m_ItemAddedCallback = new Action<IEnumerable<int>>(this.OnItemAdded);
			}
			if (this.m_ItemRemovedCallback == null)
			{
				this.m_ItemRemovedCallback = new Action<IEnumerable<int>>(this.OnItemsRemoved);
			}
			if (this.m_ItemsSourceSizeChangedCallback == null)
			{
				this.m_ItemsSourceSizeChangedCallback = new Action(this.OnItemsSourceSizeChanged);
			}
			bool flag = this.m_ListViewController != null;
			if (flag)
			{
				this.m_ListViewController.itemsAdded -= this.m_ItemAddedCallback;
				this.m_ListViewController.itemsRemoved -= this.m_ItemRemovedCallback;
				this.m_ListViewController.itemsSourceSizeChanged -= this.m_ItemsSourceSizeChangedCallback;
			}
			base.SetViewController(controller);
			this.m_ListViewController = controller;
			bool flag2 = this.m_ListViewController != null;
			if (flag2)
			{
				this.m_ListViewController.itemsAdded += this.m_ItemAddedCallback;
				this.m_ListViewController.itemsRemoved += this.m_ItemRemovedCallback;
				this.m_ListViewController.itemsSourceSizeChanged += this.m_ItemsSourceSizeChangedCallback;
			}
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0002CCED File Offset: 0x0002AEED
		private void OnItemAdded(IEnumerable<int> indices)
		{
			Action<IEnumerable<int>> action = this.itemsAdded;
			if (action != null)
			{
				action(indices);
			}
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0002CD03 File Offset: 0x0002AF03
		private void OnItemsRemoved(IEnumerable<int> indices)
		{
			Action<IEnumerable<int>> action = this.itemsRemoved;
			if (action != null)
			{
				action(indices);
			}
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0002844B File Offset: 0x0002664B
		private void OnItemsSourceSizeChanged()
		{
			base.RefreshItems();
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x0002CD19 File Offset: 0x0002AF19
		// (set) Token: 0x06000AFA RID: 2810 RVA: 0x0002CD24 File Offset: 0x0002AF24
		public ListViewReorderMode reorderMode
		{
			get
			{
				return this.m_ReorderMode;
			}
			set
			{
				bool flag = value != this.m_ReorderMode;
				if (flag)
				{
					this.m_ReorderMode = value;
					base.InitializeDragAndDropController(base.reorderable);
					base.Rebuild();
				}
			}
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0002CD60 File Offset: 0x0002AF60
		internal override ListViewDragger CreateDragger()
		{
			bool flag = this.m_ReorderMode == ListViewReorderMode.Simple;
			ListViewDragger result;
			if (flag)
			{
				result = new ListViewDragger(this);
			}
			else
			{
				result = new ListViewDraggerAnimated(this);
			}
			return result;
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0002CD8E File Offset: 0x0002AF8E
		internal override ICollectionDragAndDropController CreateDragAndDropController()
		{
			return new ListViewReorderableDragAndDropController(this);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0002CD96 File Offset: 0x0002AF96
		public ListView()
		{
			base.AddToClassList(ListView.ussClassName);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0002CDB3 File Offset: 0x0002AFB3
		public ListView(IList itemsSource, float itemHeight = -1f, Func<VisualElement> makeItem = null, Action<VisualElement, int> bindItem = null) : base(itemsSource, itemHeight, makeItem, bindItem)
		{
			base.AddToClassList(ListView.ussClassName);
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0002CDD5 File Offset: 0x0002AFD5
		private protected override void PostRefresh()
		{
			this.UpdateArraySizeField();
			this.UpdateEmpty();
			base.PostRefresh();
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0002CDF0 File Offset: 0x0002AFF0
		// Note: this type is marked as 'beforefieldinit'.
		static ListView()
		{
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0002CF65 File Offset: 0x0002B165
		[CompilerGenerated]
		private void <OnAddClicked>b__34_0()
		{
			base.SetSelection(base.itemsSource.Count - 1);
			base.ScrollToItem(-1);
		}

		// Token: 0x040004C6 RID: 1222
		private static readonly string k_SizeFieldLabel = "Size";

		// Token: 0x040004C7 RID: 1223
		private bool m_ShowBoundCollectionSize = true;

		// Token: 0x040004C8 RID: 1224
		private bool m_ShowFoldoutHeader;

		// Token: 0x040004C9 RID: 1225
		private string m_HeaderTitle;

		// Token: 0x040004CA RID: 1226
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<IEnumerable<int>> itemsAdded;

		// Token: 0x040004CB RID: 1227
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<IEnumerable<int>> itemsRemoved;

		// Token: 0x040004CC RID: 1228
		private Label m_EmptyListLabel;

		// Token: 0x040004CD RID: 1229
		private Foldout m_Foldout;

		// Token: 0x040004CE RID: 1230
		private TextField m_ArraySizeField;

		// Token: 0x040004CF RID: 1231
		private VisualElement m_Footer;

		// Token: 0x040004D0 RID: 1232
		private Button m_AddButton;

		// Token: 0x040004D1 RID: 1233
		private Button m_RemoveButton;

		// Token: 0x040004D2 RID: 1234
		private Action<IEnumerable<int>> m_ItemAddedCallback;

		// Token: 0x040004D3 RID: 1235
		private Action<IEnumerable<int>> m_ItemRemovedCallback;

		// Token: 0x040004D4 RID: 1236
		private Action m_ItemsSourceSizeChangedCallback;

		// Token: 0x040004D5 RID: 1237
		private ListViewController m_ListViewController;

		// Token: 0x040004D6 RID: 1238
		private ListViewReorderMode m_ReorderMode;

		// Token: 0x040004D7 RID: 1239
		public new static readonly string ussClassName = "unity-list-view";

		// Token: 0x040004D8 RID: 1240
		public new static readonly string itemUssClassName = ListView.ussClassName + "__item";

		// Token: 0x040004D9 RID: 1241
		public static readonly string emptyLabelUssClassName = ListView.ussClassName + "__empty-label";

		// Token: 0x040004DA RID: 1242
		public static readonly string reorderableUssClassName = ListView.ussClassName + "__reorderable";

		// Token: 0x040004DB RID: 1243
		public static readonly string reorderableItemUssClassName = ListView.reorderableUssClassName + "-item";

		// Token: 0x040004DC RID: 1244
		public static readonly string reorderableItemContainerUssClassName = ListView.reorderableItemUssClassName + "__container";

		// Token: 0x040004DD RID: 1245
		public static readonly string reorderableItemHandleUssClassName = ListView.reorderableUssClassName + "-handle";

		// Token: 0x040004DE RID: 1246
		public static readonly string reorderableItemHandleBarUssClassName = ListView.reorderableItemHandleUssClassName + "-bar";

		// Token: 0x040004DF RID: 1247
		public static readonly string footerUssClassName = ListView.ussClassName + "__footer";

		// Token: 0x040004E0 RID: 1248
		public static readonly string foldoutHeaderUssClassName = ListView.ussClassName + "__foldout-header";

		// Token: 0x040004E1 RID: 1249
		public static readonly string arraySizeFieldUssClassName = ListView.ussClassName + "__size-field";

		// Token: 0x040004E2 RID: 1250
		public static readonly string arraySizeFieldWithHeaderUssClassName = ListView.arraySizeFieldUssClassName + "--with-header";

		// Token: 0x040004E3 RID: 1251
		public static readonly string arraySizeFieldWithFooterUssClassName = ListView.arraySizeFieldUssClassName + "--with-footer";

		// Token: 0x040004E4 RID: 1252
		public static readonly string listViewWithHeaderUssClassName = ListView.ussClassName + "--with-header";

		// Token: 0x040004E5 RID: 1253
		public static readonly string listViewWithFooterUssClassName = ListView.ussClassName + "--with-footer";

		// Token: 0x040004E6 RID: 1254
		public static readonly string scrollViewWithFooterUssClassName = ListView.ussClassName + "__scroll-view--with-footer";

		// Token: 0x040004E7 RID: 1255
		internal static readonly string footerAddButtonName = ListView.ussClassName + "__add-button";

		// Token: 0x040004E8 RID: 1256
		internal static readonly string footerRemoveButtonName = ListView.ussClassName + "__remove-button";

		// Token: 0x02000153 RID: 339
		public new class UxmlFactory : UxmlFactory<ListView, ListView.UxmlTraits>
		{
			// Token: 0x06000B02 RID: 2818 RVA: 0x0002CF84 File Offset: 0x0002B184
			public UxmlFactory()
			{
			}
		}

		// Token: 0x02000154 RID: 340
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			// Token: 0x1700021F RID: 543
			// (get) Token: 0x06000B03 RID: 2819 RVA: 0x0002CF90 File Offset: 0x0002B190
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}

			// Token: 0x06000B04 RID: 2820 RVA: 0x0002CFB0 File Offset: 0x0002B1B0
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				int num = 0;
				ListView listView = (ListView)ve;
				listView.reorderable = this.m_Reorderable.GetValueFromBag(bag, cc);
				bool flag = this.m_FixedItemHeight.TryGetValueFromBag(bag, cc, ref num);
				if (flag)
				{
					listView.fixedItemHeight = (float)num;
				}
				listView.reorderMode = this.m_ReorderMode.GetValueFromBag(bag, cc);
				listView.virtualizationMethod = this.m_VirtualizationMethod.GetValueFromBag(bag, cc);
				listView.showBorder = this.m_ShowBorder.GetValueFromBag(bag, cc);
				listView.selectionType = this.m_SelectionType.GetValueFromBag(bag, cc);
				listView.showAlternatingRowBackgrounds = this.m_ShowAlternatingRowBackgrounds.GetValueFromBag(bag, cc);
				listView.showFoldoutHeader = this.m_ShowFoldoutHeader.GetValueFromBag(bag, cc);
				listView.headerTitle = this.m_HeaderTitle.GetValueFromBag(bag, cc);
				listView.showAddRemoveFooter = this.m_ShowAddRemoveFooter.GetValueFromBag(bag, cc);
				listView.showBoundCollectionSize = this.m_ShowBoundCollectionSize.GetValueFromBag(bag, cc);
				listView.horizontalScrollingEnabled = this.m_HorizontalScrollingEnabled.GetValueFromBag(bag, cc);
			}

			// Token: 0x06000B05 RID: 2821 RVA: 0x0002D0CC File Offset: 0x0002B2CC
			public UxmlTraits()
			{
			}

			// Token: 0x040004E9 RID: 1257
			private readonly UxmlIntAttributeDescription m_FixedItemHeight = new UxmlIntAttributeDescription
			{
				name = "fixed-item-height",
				obsoleteNames = new string[]
				{
					"itemHeight, item-height"
				},
				defaultValue = BaseVerticalCollectionView.s_DefaultItemHeight
			};

			// Token: 0x040004EA RID: 1258
			private readonly UxmlEnumAttributeDescription<CollectionVirtualizationMethod> m_VirtualizationMethod = new UxmlEnumAttributeDescription<CollectionVirtualizationMethod>
			{
				name = "virtualization-method",
				defaultValue = CollectionVirtualizationMethod.FixedHeight
			};

			// Token: 0x040004EB RID: 1259
			private readonly UxmlBoolAttributeDescription m_ShowBorder = new UxmlBoolAttributeDescription
			{
				name = "show-border",
				defaultValue = false
			};

			// Token: 0x040004EC RID: 1260
			private readonly UxmlEnumAttributeDescription<SelectionType> m_SelectionType = new UxmlEnumAttributeDescription<SelectionType>
			{
				name = "selection-type",
				defaultValue = SelectionType.Single
			};

			// Token: 0x040004ED RID: 1261
			private readonly UxmlEnumAttributeDescription<AlternatingRowBackground> m_ShowAlternatingRowBackgrounds = new UxmlEnumAttributeDescription<AlternatingRowBackground>
			{
				name = "show-alternating-row-backgrounds",
				defaultValue = AlternatingRowBackground.None
			};

			// Token: 0x040004EE RID: 1262
			private readonly UxmlBoolAttributeDescription m_ShowFoldoutHeader = new UxmlBoolAttributeDescription
			{
				name = "show-foldout-header",
				defaultValue = false
			};

			// Token: 0x040004EF RID: 1263
			private readonly UxmlStringAttributeDescription m_HeaderTitle = new UxmlStringAttributeDescription
			{
				name = "header-title",
				defaultValue = string.Empty
			};

			// Token: 0x040004F0 RID: 1264
			private readonly UxmlBoolAttributeDescription m_ShowAddRemoveFooter = new UxmlBoolAttributeDescription
			{
				name = "show-add-remove-footer",
				defaultValue = false
			};

			// Token: 0x040004F1 RID: 1265
			private readonly UxmlBoolAttributeDescription m_Reorderable = new UxmlBoolAttributeDescription
			{
				name = "reorderable",
				defaultValue = false
			};

			// Token: 0x040004F2 RID: 1266
			private readonly UxmlEnumAttributeDescription<ListViewReorderMode> m_ReorderMode = new UxmlEnumAttributeDescription<ListViewReorderMode>
			{
				name = "reorder-mode",
				defaultValue = ListViewReorderMode.Simple
			};

			// Token: 0x040004F3 RID: 1267
			private readonly UxmlBoolAttributeDescription m_ShowBoundCollectionSize = new UxmlBoolAttributeDescription
			{
				name = "show-bound-collection-size",
				defaultValue = true
			};

			// Token: 0x040004F4 RID: 1268
			private readonly UxmlBoolAttributeDescription m_HorizontalScrollingEnabled = new UxmlBoolAttributeDescription
			{
				name = "horizontal-scrolling",
				defaultValue = false
			};

			// Token: 0x02000155 RID: 341
			[CompilerGenerated]
			private sealed class <get_uxmlChildElementsDescription>d__13 : IEnumerable<UxmlChildElementDescription>, IEnumerable, IEnumerator<UxmlChildElementDescription>, IEnumerator, IDisposable
			{
				// Token: 0x06000B06 RID: 2822 RVA: 0x0002D273 File Offset: 0x0002B473
				[DebuggerHidden]
				public <get_uxmlChildElementsDescription>d__13(int <>1__state)
				{
					this.<>1__state = <>1__state;
					this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
				}

				// Token: 0x06000B07 RID: 2823 RVA: 0x000080DB File Offset: 0x000062DB
				[DebuggerHidden]
				void IDisposable.Dispose()
				{
				}

				// Token: 0x06000B08 RID: 2824 RVA: 0x0002D294 File Offset: 0x0002B494
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

				// Token: 0x17000220 RID: 544
				// (get) Token: 0x06000B09 RID: 2825 RVA: 0x0002D2BA File Offset: 0x0002B4BA
				UxmlChildElementDescription IEnumerator<UxmlChildElementDescription>.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x06000B0A RID: 2826 RVA: 0x0000810E File Offset: 0x0000630E
				[DebuggerHidden]
				void IEnumerator.Reset()
				{
					throw new NotSupportedException();
				}

				// Token: 0x17000221 RID: 545
				// (get) Token: 0x06000B0B RID: 2827 RVA: 0x0002D2BA File Offset: 0x0002B4BA
				object IEnumerator.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x06000B0C RID: 2828 RVA: 0x0002D2C4 File Offset: 0x0002B4C4
				[DebuggerHidden]
				IEnumerator<UxmlChildElementDescription> IEnumerable<UxmlChildElementDescription>.GetEnumerator()
				{
					ListView.UxmlTraits.<get_uxmlChildElementsDescription>d__13 <get_uxmlChildElementsDescription>d__;
					if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
					{
						this.<>1__state = 0;
						<get_uxmlChildElementsDescription>d__ = this;
					}
					else
					{
						<get_uxmlChildElementsDescription>d__ = new ListView.UxmlTraits.<get_uxmlChildElementsDescription>d__13(0);
						<get_uxmlChildElementsDescription>d__.<>4__this = this;
					}
					return <get_uxmlChildElementsDescription>d__;
				}

				// Token: 0x06000B0D RID: 2829 RVA: 0x0002D30C File Offset: 0x0002B50C
				[DebuggerHidden]
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.System.Collections.Generic.IEnumerable<UnityEngine.UIElements.UxmlChildElementDescription>.GetEnumerator();
				}

				// Token: 0x040004F5 RID: 1269
				private int <>1__state;

				// Token: 0x040004F6 RID: 1270
				private UxmlChildElementDescription <>2__current;

				// Token: 0x040004F7 RID: 1271
				private int <>l__initialThreadId;

				// Token: 0x040004F8 RID: 1272
				public ListView.UxmlTraits <>4__this;
			}
		}
	}
}
