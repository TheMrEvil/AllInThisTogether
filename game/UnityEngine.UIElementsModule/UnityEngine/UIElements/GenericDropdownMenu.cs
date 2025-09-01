using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements
{
	// Token: 0x0200013C RID: 316
	public class GenericDropdownMenu : IGenericMenu
	{
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x000297A8 File Offset: 0x000279A8
		internal List<GenericDropdownMenu.MenuItem> items
		{
			get
			{
				return this.m_Items;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x000297B0 File Offset: 0x000279B0
		internal VisualElement menuContainer
		{
			get
			{
				return this.m_MenuContainer;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x000297B8 File Offset: 0x000279B8
		public VisualElement contentContainer
		{
			get
			{
				return this.m_ScrollView.contentContainer;
			}
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x000297C8 File Offset: 0x000279C8
		public GenericDropdownMenu()
		{
			this.m_MenuContainer = new VisualElement();
			this.m_MenuContainer.AddToClassList(GenericDropdownMenu.ussClassName);
			this.m_OuterContainer = new VisualElement();
			this.m_OuterContainer.AddToClassList(GenericDropdownMenu.containerOuterUssClassName);
			this.m_MenuContainer.Add(this.m_OuterContainer);
			this.m_ScrollView = new ScrollView();
			this.m_ScrollView.AddToClassList(GenericDropdownMenu.containerInnerUssClassName);
			this.m_ScrollView.pickingMode = PickingMode.Position;
			this.m_ScrollView.contentContainer.focusable = true;
			this.m_ScrollView.touchScrollBehavior = ScrollView.TouchScrollBehavior.Clamped;
			this.m_OuterContainer.hierarchy.Add(this.m_ScrollView);
			this.m_MenuContainer.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
			this.m_MenuContainer.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnDetachFromPanel), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x000298C8 File Offset: 0x00027AC8
		private void OnAttachToPanel(AttachToPanelEvent evt)
		{
			bool flag = evt.destinationPanel == null;
			if (!flag)
			{
				this.contentContainer.AddManipulator(this.m_NavigationManipulator = new KeyboardNavigationManipulator(new Action<KeyboardNavigationOperation, EventBase>(this.Apply)));
				this.m_MenuContainer.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
				this.m_MenuContainer.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
				this.m_MenuContainer.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
				evt.destinationPanel.visualTree.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnParentResized), TrickleDown.NoTrickleDown);
				this.m_ScrollView.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnContainerGeometryChanged), TrickleDown.NoTrickleDown);
				this.m_ScrollView.RegisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.OnFocusOut), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x000299AC File Offset: 0x00027BAC
		private void OnDetachFromPanel(DetachFromPanelEvent evt)
		{
			bool flag = evt.originPanel == null;
			if (!flag)
			{
				this.contentContainer.RemoveManipulator(this.m_NavigationManipulator);
				this.m_MenuContainer.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
				this.m_MenuContainer.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
				this.m_MenuContainer.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
				evt.originPanel.visualTree.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnParentResized), TrickleDown.NoTrickleDown);
				this.m_ScrollView.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnContainerGeometryChanged), TrickleDown.NoTrickleDown);
				this.m_ScrollView.UnregisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.OnFocusOut), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00029A7C File Offset: 0x00027C7C
		private void Hide()
		{
			this.m_MenuContainer.RemoveFromHierarchy();
			bool flag = this.m_TargetElement != null;
			if (flag)
			{
				this.m_TargetElement.UnregisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnTargetElementDetachFromPanel), TrickleDown.NoTrickleDown);
				this.m_TargetElement.pseudoStates ^= PseudoStates.Active;
			}
			this.m_TargetElement = null;
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00029ADC File Offset: 0x00027CDC
		private void Apply(KeyboardNavigationOperation op, EventBase sourceEvent)
		{
			bool flag = this.Apply(op);
			if (flag)
			{
				sourceEvent.StopPropagation();
				sourceEvent.PreventDefault();
			}
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00029B08 File Offset: 0x00027D08
		private bool Apply(KeyboardNavigationOperation op)
		{
			GenericDropdownMenu.<>c__DisplayClass27_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.selectedIndex = this.GetSelectedIndex();
			bool result;
			switch (op)
			{
			case KeyboardNavigationOperation.Cancel:
				this.Hide();
				result = true;
				break;
			case KeyboardNavigationOperation.Submit:
			{
				GenericDropdownMenu.MenuItem menuItem = this.m_Items[CS$<>8__locals1.selectedIndex];
				bool flag = CS$<>8__locals1.selectedIndex >= 0 && menuItem.element.enabledSelf;
				if (flag)
				{
					Action action = menuItem.action;
					if (action != null)
					{
						action();
					}
					Action<object> actionUserData = menuItem.actionUserData;
					if (actionUserData != null)
					{
						actionUserData(menuItem.element.userData);
					}
				}
				this.Hide();
				result = true;
				break;
			}
			case KeyboardNavigationOperation.Previous:
				this.<Apply>g__UpdateSelectionUp|27_1((CS$<>8__locals1.selectedIndex < 0) ? (this.m_Items.Count - 1) : (CS$<>8__locals1.selectedIndex - 1), ref CS$<>8__locals1);
				result = true;
				break;
			case KeyboardNavigationOperation.Next:
				this.<Apply>g__UpdateSelectionDown|27_0(CS$<>8__locals1.selectedIndex + 1, ref CS$<>8__locals1);
				result = true;
				break;
			case KeyboardNavigationOperation.PageUp:
			case KeyboardNavigationOperation.Begin:
				this.<Apply>g__UpdateSelectionDown|27_0(0, ref CS$<>8__locals1);
				result = true;
				break;
			case KeyboardNavigationOperation.PageDown:
			case KeyboardNavigationOperation.End:
				this.<Apply>g__UpdateSelectionUp|27_1(this.m_Items.Count - 1, ref CS$<>8__locals1);
				result = true;
				break;
			default:
				result = false;
				break;
			}
			return result;
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00029C50 File Offset: 0x00027E50
		private void OnPointerDown(PointerDownEvent evt)
		{
			this.m_MousePosition = this.m_ScrollView.WorldToLocal(evt.position);
			this.UpdateSelection(evt.target as VisualElement);
			bool flag = evt.pointerId != PointerId.mousePointerId;
			if (flag)
			{
				this.m_MenuContainer.panel.PreventCompatibilityMouseEvents(evt.pointerId);
			}
			evt.StopPropagation();
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00029CC0 File Offset: 0x00027EC0
		private void OnPointerMove(PointerMoveEvent evt)
		{
			this.m_MousePosition = this.m_ScrollView.WorldToLocal(evt.position);
			this.UpdateSelection(evt.target as VisualElement);
			bool flag = evt.pointerId != PointerId.mousePointerId;
			if (flag)
			{
				this.m_MenuContainer.panel.PreventCompatibilityMouseEvents(evt.pointerId);
			}
			evt.StopPropagation();
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00029D30 File Offset: 0x00027F30
		private void OnPointerUp(PointerUpEvent evt)
		{
			int selectedIndex = this.GetSelectedIndex();
			bool flag = selectedIndex != -1;
			if (flag)
			{
				GenericDropdownMenu.MenuItem menuItem = this.m_Items[selectedIndex];
				Action action = menuItem.action;
				if (action != null)
				{
					action();
				}
				Action<object> actionUserData = menuItem.actionUserData;
				if (actionUserData != null)
				{
					actionUserData(menuItem.element.userData);
				}
				this.Hide();
			}
			bool flag2 = evt.pointerId != PointerId.mousePointerId;
			if (flag2)
			{
				this.m_MenuContainer.panel.PreventCompatibilityMouseEvents(evt.pointerId);
			}
			evt.StopPropagation();
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00029DCC File Offset: 0x00027FCC
		private void OnFocusOut(FocusOutEvent evt)
		{
			bool flag = !this.m_ScrollView.ContainsPoint(this.m_MousePosition);
			if (flag)
			{
				this.Hide();
			}
			else
			{
				this.m_MenuContainer.schedule.Execute(new Action(this.contentContainer.Focus));
			}
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00029E22 File Offset: 0x00028022
		private void OnParentResized(GeometryChangedEvent evt)
		{
			this.Hide();
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00029E2C File Offset: 0x0002802C
		private void UpdateSelection(VisualElement target)
		{
			bool flag = !this.m_ScrollView.ContainsPoint(this.m_MousePosition);
			if (flag)
			{
				int selectedIndex = this.GetSelectedIndex();
				bool flag2 = selectedIndex >= 0;
				if (flag2)
				{
					this.m_Items[selectedIndex].element.pseudoStates &= ~PseudoStates.Hover;
				}
			}
			else
			{
				bool flag3 = target == null;
				if (!flag3)
				{
					bool flag4 = (target.pseudoStates & PseudoStates.Hover) != PseudoStates.Hover;
					if (flag4)
					{
						int selectedIndex2 = this.GetSelectedIndex();
						bool flag5 = selectedIndex2 >= 0;
						if (flag5)
						{
							this.m_Items[selectedIndex2].element.pseudoStates &= ~PseudoStates.Hover;
						}
						target.pseudoStates |= PseudoStates.Hover;
					}
				}
			}
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00029EF0 File Offset: 0x000280F0
		private void ChangeSelectedIndex(int newIndex, int previousIndex)
		{
			bool flag = previousIndex >= 0 && previousIndex < this.m_Items.Count;
			if (flag)
			{
				this.m_Items[previousIndex].element.pseudoStates &= ~PseudoStates.Hover;
			}
			bool flag2 = newIndex >= 0 && newIndex < this.m_Items.Count;
			if (flag2)
			{
				this.m_Items[newIndex].element.pseudoStates |= PseudoStates.Hover;
				this.m_ScrollView.ScrollTo(this.m_Items[newIndex].element);
			}
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00029F90 File Offset: 0x00028190
		private int GetSelectedIndex()
		{
			for (int i = 0; i < this.m_Items.Count; i++)
			{
				bool flag = (this.m_Items[i].element.pseudoStates & PseudoStates.Hover) == PseudoStates.Hover;
				if (flag)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00029FE4 File Offset: 0x000281E4
		public void AddItem(string itemName, bool isChecked, Action action)
		{
			GenericDropdownMenu.MenuItem menuItem = this.AddItem(itemName, isChecked, true, null);
			bool flag = menuItem != null;
			if (flag)
			{
				menuItem.action = action;
			}
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0002A010 File Offset: 0x00028210
		public void AddItem(string itemName, bool isChecked, Action<object> action, object data)
		{
			GenericDropdownMenu.MenuItem menuItem = this.AddItem(itemName, isChecked, true, data);
			bool flag = menuItem != null;
			if (flag)
			{
				menuItem.actionUserData = action;
			}
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0002A03B File Offset: 0x0002823B
		public void AddDisabledItem(string itemName, bool isChecked)
		{
			this.AddItem(itemName, isChecked, false, null);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0002A04C File Offset: 0x0002824C
		public void AddSeparator(string path)
		{
			VisualElement visualElement = new VisualElement();
			visualElement.AddToClassList(GenericDropdownMenu.separatorUssClassName);
			visualElement.pickingMode = PickingMode.Ignore;
			this.m_ScrollView.Add(visualElement);
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0002A084 File Offset: 0x00028284
		private GenericDropdownMenu.MenuItem AddItem(string itemName, bool isChecked, bool isEnabled, object data = null)
		{
			bool flag = string.IsNullOrEmpty(itemName) || itemName.EndsWith("/");
			GenericDropdownMenu.MenuItem result;
			if (flag)
			{
				this.AddSeparator(itemName);
				result = null;
			}
			else
			{
				for (int i = 0; i < this.m_Items.Count; i++)
				{
					bool flag2 = itemName == this.m_Items[i].name;
					if (flag2)
					{
						return null;
					}
				}
				VisualElement visualElement = new VisualElement();
				visualElement.AddToClassList(GenericDropdownMenu.itemUssClassName);
				visualElement.SetEnabled(isEnabled);
				visualElement.userData = data;
				if (isChecked)
				{
					VisualElement visualElement2 = new VisualElement();
					visualElement2.AddToClassList(GenericDropdownMenu.checkmarkUssClassName);
					visualElement2.pickingMode = PickingMode.Ignore;
					visualElement.Add(visualElement2);
					visualElement.pseudoStates |= PseudoStates.Checked;
				}
				Label label = new Label(itemName);
				label.AddToClassList(GenericDropdownMenu.labelUssClassName);
				label.pickingMode = PickingMode.Ignore;
				visualElement.Add(label);
				this.m_ScrollView.Add(visualElement);
				GenericDropdownMenu.MenuItem menuItem = new GenericDropdownMenu.MenuItem
				{
					name = itemName,
					element = visualElement
				};
				this.m_Items.Add(menuItem);
				result = menuItem;
			}
			return result;
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0002A1BC File Offset: 0x000283BC
		public void DropDown(Rect position, VisualElement targetElement = null, bool anchored = false)
		{
			bool flag = targetElement == null;
			if (flag)
			{
				Debug.LogError("VisualElement Generic Menu needs a target to find a root to attach to.");
			}
			else
			{
				this.m_TargetElement = targetElement;
				this.m_TargetElement.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnTargetElementDetachFromPanel), TrickleDown.NoTrickleDown);
				this.m_PanelRootVisualContainer = this.m_TargetElement.GetRootVisualContainer();
				bool flag2 = this.m_PanelRootVisualContainer == null;
				if (flag2)
				{
					Debug.LogError("Could not find rootVisualContainer...");
				}
				else
				{
					this.m_PanelRootVisualContainer.Add(this.m_MenuContainer);
					this.m_MenuContainer.style.left = this.m_PanelRootVisualContainer.layout.x;
					this.m_MenuContainer.style.top = this.m_PanelRootVisualContainer.layout.y;
					this.m_MenuContainer.style.width = this.m_PanelRootVisualContainer.layout.width;
					this.m_MenuContainer.style.height = this.m_PanelRootVisualContainer.layout.height;
					Rect rect = this.m_PanelRootVisualContainer.WorldToLocal(position);
					this.m_OuterContainer.style.left = rect.x - this.m_PanelRootVisualContainer.layout.x;
					this.m_OuterContainer.style.top = rect.y + position.height - this.m_PanelRootVisualContainer.layout.y;
					this.m_DesiredRect = (anchored ? position : Rect.zero);
					this.m_MenuContainer.schedule.Execute(new Action(this.contentContainer.Focus));
					this.EnsureVisibilityInParent();
					bool flag3 = targetElement != null;
					if (flag3)
					{
						targetElement.pseudoStates |= PseudoStates.Active;
					}
				}
			}
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00029E22 File Offset: 0x00028022
		private void OnTargetElementDetachFromPanel(DetachFromPanelEvent evt)
		{
			this.Hide();
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0002A3B3 File Offset: 0x000285B3
		private void OnContainerGeometryChanged(GeometryChangedEvent evt)
		{
			this.EnsureVisibilityInParent();
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0002A3C0 File Offset: 0x000285C0
		private void EnsureVisibilityInParent()
		{
			bool flag = this.m_PanelRootVisualContainer != null && !float.IsNaN(this.m_OuterContainer.layout.width) && !float.IsNaN(this.m_OuterContainer.layout.height);
			if (flag)
			{
				bool flag2 = this.m_DesiredRect == Rect.zero;
				if (flag2)
				{
					float v = Mathf.Min(this.m_OuterContainer.layout.x, this.m_PanelRootVisualContainer.layout.width - this.m_OuterContainer.layout.width);
					float v2 = Mathf.Min(this.m_OuterContainer.layout.y, Mathf.Max(0f, this.m_PanelRootVisualContainer.layout.height - this.m_OuterContainer.layout.height));
					this.m_OuterContainer.style.left = v;
					this.m_OuterContainer.style.top = v2;
				}
				this.m_OuterContainer.style.height = Mathf.Min(this.m_MenuContainer.layout.height - this.m_MenuContainer.layout.y - this.m_OuterContainer.layout.y, this.m_ScrollView.layout.height + this.m_OuterContainer.resolvedStyle.borderBottomWidth + this.m_OuterContainer.resolvedStyle.borderTopWidth);
				bool flag3 = this.m_DesiredRect != Rect.zero;
				if (flag3)
				{
					this.m_OuterContainer.style.width = this.m_DesiredRect.width;
				}
			}
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0002A5B0 File Offset: 0x000287B0
		// Note: this type is marked as 'beforefieldinit'.
		static GenericDropdownMenu()
		{
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0002A640 File Offset: 0x00028840
		[CompilerGenerated]
		private void <Apply>g__UpdateSelectionDown|27_0(int newIndex, ref GenericDropdownMenu.<>c__DisplayClass27_0 A_2)
		{
			while (newIndex < this.m_Items.Count)
			{
				bool enabledSelf = this.m_Items[newIndex].element.enabledSelf;
				if (enabledSelf)
				{
					this.ChangeSelectedIndex(newIndex, A_2.selectedIndex);
					break;
				}
				newIndex++;
			}
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0002A694 File Offset: 0x00028894
		[CompilerGenerated]
		private void <Apply>g__UpdateSelectionUp|27_1(int newIndex, ref GenericDropdownMenu.<>c__DisplayClass27_0 A_2)
		{
			while (newIndex >= 0)
			{
				bool enabledSelf = this.m_Items[newIndex].element.enabledSelf;
				if (enabledSelf)
				{
					this.ChangeSelectedIndex(newIndex, A_2.selectedIndex);
					break;
				}
				newIndex--;
			}
		}

		// Token: 0x0400047B RID: 1147
		public static readonly string ussClassName = "unity-base-dropdown";

		// Token: 0x0400047C RID: 1148
		public static readonly string itemUssClassName = GenericDropdownMenu.ussClassName + "__item";

		// Token: 0x0400047D RID: 1149
		public static readonly string labelUssClassName = GenericDropdownMenu.ussClassName + "__label";

		// Token: 0x0400047E RID: 1150
		public static readonly string containerInnerUssClassName = GenericDropdownMenu.ussClassName + "__container-inner";

		// Token: 0x0400047F RID: 1151
		public static readonly string containerOuterUssClassName = GenericDropdownMenu.ussClassName + "__container-outer";

		// Token: 0x04000480 RID: 1152
		public static readonly string checkmarkUssClassName = GenericDropdownMenu.ussClassName + "__checkmark";

		// Token: 0x04000481 RID: 1153
		public static readonly string separatorUssClassName = GenericDropdownMenu.ussClassName + "__separator";

		// Token: 0x04000482 RID: 1154
		private List<GenericDropdownMenu.MenuItem> m_Items = new List<GenericDropdownMenu.MenuItem>();

		// Token: 0x04000483 RID: 1155
		private VisualElement m_MenuContainer;

		// Token: 0x04000484 RID: 1156
		private VisualElement m_OuterContainer;

		// Token: 0x04000485 RID: 1157
		private ScrollView m_ScrollView;

		// Token: 0x04000486 RID: 1158
		private VisualElement m_PanelRootVisualContainer;

		// Token: 0x04000487 RID: 1159
		private VisualElement m_TargetElement;

		// Token: 0x04000488 RID: 1160
		private Rect m_DesiredRect;

		// Token: 0x04000489 RID: 1161
		private KeyboardNavigationManipulator m_NavigationManipulator;

		// Token: 0x0400048A RID: 1162
		private Vector2 m_MousePosition;

		// Token: 0x0200013D RID: 317
		internal class MenuItem
		{
			// Token: 0x06000A88 RID: 2696 RVA: 0x000020C2 File Offset: 0x000002C2
			public MenuItem()
			{
			}

			// Token: 0x0400048B RID: 1163
			public string name;

			// Token: 0x0400048C RID: 1164
			public VisualElement element;

			// Token: 0x0400048D RID: 1165
			public Action action;

			// Token: 0x0400048E RID: 1166
			public Action<object> actionUserData;
		}

		// Token: 0x0200013E RID: 318
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <>c__DisplayClass27_0
		{
			// Token: 0x0400048F RID: 1167
			public GenericDropdownMenu <>4__this;

			// Token: 0x04000490 RID: 1168
			public int selectedIndex;
		}
	}
}
