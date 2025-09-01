using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace UnityEngine.UIElements
{
	// Token: 0x02000197 RID: 407
	public class TwoPaneSplitView : VisualElement
	{
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x00037730 File Offset: 0x00035930
		public VisualElement fixedPane
		{
			get
			{
				return this.m_FixedPane;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x00037738 File Offset: 0x00035938
		public VisualElement flexedPane
		{
			get
			{
				return this.m_FlexedPane;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x00037740 File Offset: 0x00035940
		// (set) Token: 0x06000D4E RID: 3406 RVA: 0x00037748 File Offset: 0x00035948
		public int fixedPaneIndex
		{
			get
			{
				return this.m_FixedPaneIndex;
			}
			set
			{
				bool flag = value == this.m_FixedPaneIndex;
				if (!flag)
				{
					this.Init(value, this.m_FixedPaneInitialDimension, this.m_Orientation);
				}
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x00037779 File Offset: 0x00035979
		// (set) Token: 0x06000D50 RID: 3408 RVA: 0x00037784 File Offset: 0x00035984
		public float fixedPaneInitialDimension
		{
			get
			{
				return this.m_FixedPaneInitialDimension;
			}
			set
			{
				bool flag = value == this.m_FixedPaneInitialDimension;
				if (!flag)
				{
					this.Init(this.m_FixedPaneIndex, value, this.m_Orientation);
				}
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000D51 RID: 3409 RVA: 0x000377B5 File Offset: 0x000359B5
		// (set) Token: 0x06000D52 RID: 3410 RVA: 0x000377C0 File Offset: 0x000359C0
		public TwoPaneSplitViewOrientation orientation
		{
			get
			{
				return this.m_Orientation;
			}
			set
			{
				bool flag = value == this.m_Orientation;
				if (!flag)
				{
					this.Init(this.m_FixedPaneIndex, this.m_FixedPaneInitialDimension, value);
				}
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x000377F1 File Offset: 0x000359F1
		// (set) Token: 0x06000D54 RID: 3412 RVA: 0x00037810 File Offset: 0x00035A10
		internal float fixedPaneDimension
		{
			get
			{
				return string.IsNullOrEmpty(base.viewDataKey) ? this.m_FixedPaneInitialDimension : this.m_FixedPaneDimension;
			}
			set
			{
				bool flag = value == this.m_FixedPaneDimension;
				if (!flag)
				{
					this.m_FixedPaneDimension = value;
					base.SaveViewData();
				}
			}
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0003783C File Offset: 0x00035A3C
		public TwoPaneSplitView()
		{
			base.AddToClassList(TwoPaneSplitView.s_UssClassName);
			this.m_Content = new VisualElement();
			this.m_Content.name = "unity-content-container";
			this.m_Content.AddToClassList(TwoPaneSplitView.s_ContentContainerClassName);
			base.hierarchy.Add(this.m_Content);
			this.m_DragLineAnchor = new VisualElement();
			this.m_DragLineAnchor.name = "unity-dragline-anchor";
			this.m_DragLineAnchor.AddToClassList(TwoPaneSplitView.s_HandleDragLineAnchorClassName);
			base.hierarchy.Add(this.m_DragLineAnchor);
			this.m_DragLine = new VisualElement();
			this.m_DragLine.name = "unity-dragline";
			this.m_DragLine.AddToClassList(TwoPaneSplitView.s_HandleDragLineClassName);
			this.m_DragLineAnchor.Add(this.m_DragLine);
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0003792B File Offset: 0x00035B2B
		public TwoPaneSplitView(int fixedPaneIndex, float fixedPaneStartDimension, TwoPaneSplitViewOrientation orientation) : this()
		{
			this.Init(fixedPaneIndex, fixedPaneStartDimension, orientation);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00037940 File Offset: 0x00035B40
		public void CollapseChild(int index)
		{
			bool flag = this.m_LeftPane == null;
			if (!flag)
			{
				this.m_DragLine.style.display = DisplayStyle.None;
				this.m_DragLineAnchor.style.display = DisplayStyle.None;
				bool flag2 = index == 0;
				if (flag2)
				{
					this.m_RightPane.style.width = StyleKeyword.Initial;
					this.m_RightPane.style.height = StyleKeyword.Initial;
					this.m_RightPane.style.flexGrow = 1f;
					this.m_LeftPane.style.display = DisplayStyle.None;
				}
				else
				{
					this.m_LeftPane.style.width = StyleKeyword.Initial;
					this.m_LeftPane.style.height = StyleKeyword.Initial;
					this.m_LeftPane.style.flexGrow = 1f;
					this.m_RightPane.style.display = DisplayStyle.None;
				}
				this.m_CollapseMode = true;
			}
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00037A64 File Offset: 0x00035C64
		public void UnCollapse()
		{
			bool flag = this.m_LeftPane == null;
			if (!flag)
			{
				this.m_LeftPane.style.display = DisplayStyle.Flex;
				this.m_RightPane.style.display = DisplayStyle.Flex;
				this.m_DragLine.style.display = DisplayStyle.Flex;
				this.m_DragLineAnchor.style.display = DisplayStyle.Flex;
				this.m_LeftPane.style.flexGrow = 0f;
				this.m_RightPane.style.flexGrow = 0f;
				this.m_CollapseMode = false;
				this.Init(this.m_FixedPaneIndex, this.m_FixedPaneInitialDimension, this.m_Orientation);
			}
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00037B38 File Offset: 0x00035D38
		internal void Init(int fixedPaneIndex, float fixedPaneInitialDimension, TwoPaneSplitViewOrientation orientation)
		{
			this.m_Orientation = orientation;
			this.m_FixedPaneIndex = fixedPaneIndex;
			this.m_FixedPaneInitialDimension = fixedPaneInitialDimension;
			this.m_Content.RemoveFromClassList(TwoPaneSplitView.s_HorizontalClassName);
			this.m_Content.RemoveFromClassList(TwoPaneSplitView.s_VerticalClassName);
			bool flag = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
			if (flag)
			{
				this.m_Content.AddToClassList(TwoPaneSplitView.s_HorizontalClassName);
			}
			else
			{
				this.m_Content.AddToClassList(TwoPaneSplitView.s_VerticalClassName);
			}
			this.m_DragLineAnchor.RemoveFromClassList(TwoPaneSplitView.s_HandleDragLineAnchorHorizontalClassName);
			this.m_DragLineAnchor.RemoveFromClassList(TwoPaneSplitView.s_HandleDragLineAnchorVerticalClassName);
			bool flag2 = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
			if (flag2)
			{
				this.m_DragLineAnchor.AddToClassList(TwoPaneSplitView.s_HandleDragLineAnchorHorizontalClassName);
			}
			else
			{
				this.m_DragLineAnchor.AddToClassList(TwoPaneSplitView.s_HandleDragLineAnchorVerticalClassName);
			}
			this.m_DragLine.RemoveFromClassList(TwoPaneSplitView.s_HandleDragLineHorizontalClassName);
			this.m_DragLine.RemoveFromClassList(TwoPaneSplitView.s_HandleDragLineVerticalClassName);
			bool flag3 = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
			if (flag3)
			{
				this.m_DragLine.AddToClassList(TwoPaneSplitView.s_HandleDragLineHorizontalClassName);
			}
			else
			{
				this.m_DragLine.AddToClassList(TwoPaneSplitView.s_HandleDragLineVerticalClassName);
			}
			bool flag4 = this.m_Resizer != null;
			if (flag4)
			{
				this.m_DragLineAnchor.RemoveManipulator(this.m_Resizer);
				this.m_Resizer = null;
			}
			bool flag5 = this.m_Content.childCount != 2;
			if (flag5)
			{
				base.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnPostDisplaySetup), TrickleDown.NoTrickleDown);
			}
			else
			{
				this.PostDisplaySetup();
			}
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00037CB0 File Offset: 0x00035EB0
		private void OnPostDisplaySetup(GeometryChangedEvent evt)
		{
			bool flag = this.m_Content.childCount != 2;
			if (flag)
			{
				Debug.LogError("TwoPaneSplitView needs exactly 2 children.");
			}
			else
			{
				this.PostDisplaySetup();
				base.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnPostDisplaySetup), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x00037CFC File Offset: 0x00035EFC
		private void PostDisplaySetup()
		{
			bool flag = this.m_Content.childCount != 2;
			if (flag)
			{
				Debug.LogError("TwoPaneSplitView needs exactly 2 children.");
			}
			else
			{
				bool flag2 = this.fixedPaneDimension < 0f;
				if (flag2)
				{
					this.fixedPaneDimension = this.m_FixedPaneInitialDimension;
				}
				float fixedPaneDimension = this.fixedPaneDimension;
				this.m_LeftPane = this.m_Content[0];
				bool flag3 = this.m_FixedPaneIndex == 0;
				if (flag3)
				{
					this.m_FixedPane = this.m_LeftPane;
				}
				else
				{
					this.m_FlexedPane = this.m_LeftPane;
				}
				this.m_RightPane = this.m_Content[1];
				bool flag4 = this.m_FixedPaneIndex == 1;
				if (flag4)
				{
					this.m_FixedPane = this.m_RightPane;
				}
				else
				{
					this.m_FlexedPane = this.m_RightPane;
				}
				this.m_FixedPane.style.flexBasis = StyleKeyword.Null;
				this.m_FixedPane.style.flexShrink = StyleKeyword.Null;
				this.m_FixedPane.style.flexGrow = StyleKeyword.Null;
				this.m_FlexedPane.style.flexGrow = StyleKeyword.Null;
				this.m_FlexedPane.style.flexShrink = StyleKeyword.Null;
				this.m_FlexedPane.style.flexBasis = StyleKeyword.Null;
				this.m_FixedPane.style.width = StyleKeyword.Null;
				this.m_FixedPane.style.height = StyleKeyword.Null;
				this.m_FlexedPane.style.width = StyleKeyword.Null;
				this.m_FlexedPane.style.height = StyleKeyword.Null;
				bool flag5 = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
				if (flag5)
				{
					this.m_FixedPane.style.width = fixedPaneDimension;
					this.m_FixedPane.style.height = StyleKeyword.Null;
				}
				else
				{
					this.m_FixedPane.style.width = StyleKeyword.Null;
					this.m_FixedPane.style.height = fixedPaneDimension;
				}
				this.m_FixedPane.style.flexShrink = 0f;
				this.m_FixedPane.style.flexGrow = 0f;
				this.m_FlexedPane.style.flexGrow = 1f;
				this.m_FlexedPane.style.flexShrink = 0f;
				this.m_FlexedPane.style.flexBasis = 0f;
				bool flag6 = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
				if (flag6)
				{
					bool flag7 = this.m_FixedPaneIndex == 0;
					if (flag7)
					{
						this.m_DragLineAnchor.style.left = fixedPaneDimension;
					}
					else
					{
						this.m_DragLineAnchor.style.left = base.resolvedStyle.width - fixedPaneDimension;
					}
				}
				else
				{
					bool flag8 = this.m_FixedPaneIndex == 0;
					if (flag8)
					{
						this.m_DragLineAnchor.style.top = fixedPaneDimension;
					}
					else
					{
						this.m_DragLineAnchor.style.top = base.resolvedStyle.height - fixedPaneDimension;
					}
				}
				bool flag9 = this.m_FixedPaneIndex == 0;
				int dir;
				if (flag9)
				{
					dir = 1;
				}
				else
				{
					dir = -1;
				}
				bool flag10 = this.m_FixedPaneIndex == 0;
				if (flag10)
				{
					this.m_Resizer = new TwoPaneSplitViewResizer(this, dir, this.m_Orientation);
				}
				else
				{
					this.m_Resizer = new TwoPaneSplitViewResizer(this, dir, this.m_Orientation);
				}
				this.m_DragLineAnchor.AddManipulator(this.m_Resizer);
				base.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnSizeChange), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x000380C8 File Offset: 0x000362C8
		private void OnSizeChange(GeometryChangedEvent evt)
		{
			this.OnSizeChange();
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x000380D4 File Offset: 0x000362D4
		private void OnSizeChange()
		{
			bool collapseMode = this.m_CollapseMode;
			if (!collapseMode)
			{
				bool flag = base.resolvedStyle.display == DisplayStyle.None || base.resolvedStyle.visibility == Visibility.Hidden;
				if (!flag)
				{
					float num = base.resolvedStyle.width;
					float num2 = this.m_FixedPane.resolvedStyle.width;
					float value = this.m_FixedPane.resolvedStyle.minWidth.value;
					float value2 = this.m_FlexedPane.resolvedStyle.minWidth.value;
					bool flag2 = this.m_Orientation == TwoPaneSplitViewOrientation.Vertical;
					if (flag2)
					{
						num = base.resolvedStyle.height;
						num2 = this.m_FixedPane.resolvedStyle.height;
						value = this.m_FixedPane.resolvedStyle.minHeight.value;
						value2 = this.m_FlexedPane.resolvedStyle.minHeight.value;
					}
					bool flag3 = num >= num2 + value2;
					if (flag3)
					{
						this.SetDragLineOffset((this.m_FixedPaneIndex == 0) ? num2 : (num - num2));
					}
					else
					{
						bool flag4 = num >= value + value2;
						if (flag4)
						{
							float num3 = num - value2;
							this.SetFixedPaneDimension(num3);
							this.SetDragLineOffset((this.m_FixedPaneIndex == 0) ? num3 : value2);
						}
						else
						{
							this.SetFixedPaneDimension(value);
							this.SetDragLineOffset((this.m_FixedPaneIndex == 0) ? value : value2);
						}
					}
				}
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x0003824C File Offset: 0x0003644C
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_Content;
			}
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00038264 File Offset: 0x00036464
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
			base.OverwriteFromViewData(this, fullHierarchicalViewDataKey);
			this.PostDisplaySetup();
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00038290 File Offset: 0x00036490
		private void SetDragLineOffset(float offset)
		{
			bool flag = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
			if (flag)
			{
				this.m_DragLineAnchor.style.left = offset;
			}
			else
			{
				this.m_DragLineAnchor.style.top = offset;
			}
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x000382DC File Offset: 0x000364DC
		private void SetFixedPaneDimension(float dimension)
		{
			bool flag = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
			if (flag)
			{
				this.m_FixedPane.style.width = dimension;
			}
			else
			{
				this.m_FixedPane.style.height = dimension;
			}
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x00038328 File Offset: 0x00036528
		// Note: this type is marked as 'beforefieldinit'.
		static TwoPaneSplitView()
		{
		}

		// Token: 0x0400062D RID: 1581
		private static readonly string s_UssClassName = "unity-two-pane-split-view";

		// Token: 0x0400062E RID: 1582
		private static readonly string s_ContentContainerClassName = "unity-two-pane-split-view__content-container";

		// Token: 0x0400062F RID: 1583
		private static readonly string s_HandleDragLineClassName = "unity-two-pane-split-view__dragline";

		// Token: 0x04000630 RID: 1584
		private static readonly string s_HandleDragLineVerticalClassName = TwoPaneSplitView.s_HandleDragLineClassName + "--vertical";

		// Token: 0x04000631 RID: 1585
		private static readonly string s_HandleDragLineHorizontalClassName = TwoPaneSplitView.s_HandleDragLineClassName + "--horizontal";

		// Token: 0x04000632 RID: 1586
		private static readonly string s_HandleDragLineAnchorClassName = "unity-two-pane-split-view__dragline-anchor";

		// Token: 0x04000633 RID: 1587
		private static readonly string s_HandleDragLineAnchorVerticalClassName = TwoPaneSplitView.s_HandleDragLineAnchorClassName + "--vertical";

		// Token: 0x04000634 RID: 1588
		private static readonly string s_HandleDragLineAnchorHorizontalClassName = TwoPaneSplitView.s_HandleDragLineAnchorClassName + "--horizontal";

		// Token: 0x04000635 RID: 1589
		private static readonly string s_VerticalClassName = "unity-two-pane-split-view--vertical";

		// Token: 0x04000636 RID: 1590
		private static readonly string s_HorizontalClassName = "unity-two-pane-split-view--horizontal";

		// Token: 0x04000637 RID: 1591
		private VisualElement m_LeftPane;

		// Token: 0x04000638 RID: 1592
		private VisualElement m_RightPane;

		// Token: 0x04000639 RID: 1593
		private VisualElement m_FixedPane;

		// Token: 0x0400063A RID: 1594
		private VisualElement m_FlexedPane;

		// Token: 0x0400063B RID: 1595
		[SerializeField]
		private float m_FixedPaneDimension = -1f;

		// Token: 0x0400063C RID: 1596
		private VisualElement m_DragLine;

		// Token: 0x0400063D RID: 1597
		private VisualElement m_DragLineAnchor;

		// Token: 0x0400063E RID: 1598
		private bool m_CollapseMode;

		// Token: 0x0400063F RID: 1599
		private VisualElement m_Content;

		// Token: 0x04000640 RID: 1600
		private TwoPaneSplitViewOrientation m_Orientation;

		// Token: 0x04000641 RID: 1601
		private int m_FixedPaneIndex;

		// Token: 0x04000642 RID: 1602
		private float m_FixedPaneInitialDimension;

		// Token: 0x04000643 RID: 1603
		internal TwoPaneSplitViewResizer m_Resizer;

		// Token: 0x02000198 RID: 408
		public new class UxmlFactory : UxmlFactory<TwoPaneSplitView, TwoPaneSplitView.UxmlTraits>
		{
			// Token: 0x06000D63 RID: 3427 RVA: 0x000383C1 File Offset: 0x000365C1
			public UxmlFactory()
			{
			}
		}

		// Token: 0x02000199 RID: 409
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x170002BB RID: 699
			// (get) Token: 0x06000D64 RID: 3428 RVA: 0x000383CC File Offset: 0x000365CC
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}

			// Token: 0x06000D65 RID: 3429 RVA: 0x000383EC File Offset: 0x000365EC
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				int valueFromBag = this.m_FixedPaneIndex.GetValueFromBag(bag, cc);
				int valueFromBag2 = this.m_FixedPaneInitialDimension.GetValueFromBag(bag, cc);
				TwoPaneSplitViewOrientation valueFromBag3 = this.m_Orientation.GetValueFromBag(bag, cc);
				((TwoPaneSplitView)ve).Init(valueFromBag, (float)valueFromBag2, valueFromBag3);
			}

			// Token: 0x06000D66 RID: 3430 RVA: 0x00038440 File Offset: 0x00036640
			public UxmlTraits()
			{
			}

			// Token: 0x04000644 RID: 1604
			private UxmlIntAttributeDescription m_FixedPaneIndex = new UxmlIntAttributeDescription
			{
				name = "fixed-pane-index",
				defaultValue = 0
			};

			// Token: 0x04000645 RID: 1605
			private UxmlIntAttributeDescription m_FixedPaneInitialDimension = new UxmlIntAttributeDescription
			{
				name = "fixed-pane-initial-dimension",
				defaultValue = 100
			};

			// Token: 0x04000646 RID: 1606
			private UxmlEnumAttributeDescription<TwoPaneSplitViewOrientation> m_Orientation = new UxmlEnumAttributeDescription<TwoPaneSplitViewOrientation>
			{
				name = "orientation",
				defaultValue = TwoPaneSplitViewOrientation.Horizontal
			};

			// Token: 0x0200019A RID: 410
			[CompilerGenerated]
			private sealed class <get_uxmlChildElementsDescription>d__4 : IEnumerable<UxmlChildElementDescription>, IEnumerable, IEnumerator<UxmlChildElementDescription>, IEnumerator, IDisposable
			{
				// Token: 0x06000D67 RID: 3431 RVA: 0x000384B2 File Offset: 0x000366B2
				[DebuggerHidden]
				public <get_uxmlChildElementsDescription>d__4(int <>1__state)
				{
					this.<>1__state = <>1__state;
					this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
				}

				// Token: 0x06000D68 RID: 3432 RVA: 0x000080DB File Offset: 0x000062DB
				[DebuggerHidden]
				void IDisposable.Dispose()
				{
				}

				// Token: 0x06000D69 RID: 3433 RVA: 0x000384D4 File Offset: 0x000366D4
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

				// Token: 0x170002BC RID: 700
				// (get) Token: 0x06000D6A RID: 3434 RVA: 0x000384FA File Offset: 0x000366FA
				UxmlChildElementDescription IEnumerator<UxmlChildElementDescription>.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x06000D6B RID: 3435 RVA: 0x0000810E File Offset: 0x0000630E
				[DebuggerHidden]
				void IEnumerator.Reset()
				{
					throw new NotSupportedException();
				}

				// Token: 0x170002BD RID: 701
				// (get) Token: 0x06000D6C RID: 3436 RVA: 0x000384FA File Offset: 0x000366FA
				object IEnumerator.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x06000D6D RID: 3437 RVA: 0x00038504 File Offset: 0x00036704
				[DebuggerHidden]
				IEnumerator<UxmlChildElementDescription> IEnumerable<UxmlChildElementDescription>.GetEnumerator()
				{
					TwoPaneSplitView.UxmlTraits.<get_uxmlChildElementsDescription>d__4 <get_uxmlChildElementsDescription>d__;
					if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
					{
						this.<>1__state = 0;
						<get_uxmlChildElementsDescription>d__ = this;
					}
					else
					{
						<get_uxmlChildElementsDescription>d__ = new TwoPaneSplitView.UxmlTraits.<get_uxmlChildElementsDescription>d__4(0);
						<get_uxmlChildElementsDescription>d__.<>4__this = this;
					}
					return <get_uxmlChildElementsDescription>d__;
				}

				// Token: 0x06000D6E RID: 3438 RVA: 0x0003854C File Offset: 0x0003674C
				[DebuggerHidden]
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.System.Collections.Generic.IEnumerable<UnityEngine.UIElements.UxmlChildElementDescription>.GetEnumerator();
				}

				// Token: 0x04000647 RID: 1607
				private int <>1__state;

				// Token: 0x04000648 RID: 1608
				private UxmlChildElementDescription <>2__current;

				// Token: 0x04000649 RID: 1609
				private int <>l__initialThreadId;

				// Token: 0x0400064A RID: 1610
				public TwoPaneSplitView.UxmlTraits <>4__this;
			}
		}
	}
}
