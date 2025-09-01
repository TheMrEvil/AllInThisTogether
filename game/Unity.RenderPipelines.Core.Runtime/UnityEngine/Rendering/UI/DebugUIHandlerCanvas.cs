using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000EE RID: 238
	public class DebugUIHandlerCanvas : MonoBehaviour
	{
		// Token: 0x060006F9 RID: 1785 RVA: 0x0001EC84 File Offset: 0x0001CE84
		private void OnEnable()
		{
			if (this.prefabs == null)
			{
				this.prefabs = new List<DebugUIPrefabBundle>();
			}
			if (this.m_PrefabsMap == null)
			{
				this.m_PrefabsMap = new Dictionary<Type, Transform>();
			}
			if (this.m_UIPanels == null)
			{
				this.m_UIPanels = new List<DebugUIHandlerPanel>();
			}
			DebugManager.instance.RegisterRootCanvas(this);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001ECD8 File Offset: 0x0001CED8
		private void Update()
		{
			int state = DebugManager.instance.GetState();
			if (this.m_DebugTreeState != state)
			{
				this.ResetAllHierarchy();
			}
			this.HandleInput();
			if (this.m_UIPanels != null && this.m_SelectedPanel < this.m_UIPanels.Count && this.m_UIPanels[this.m_SelectedPanel] != null)
			{
				this.m_UIPanels[this.m_SelectedPanel].UpdateScroll();
			}
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001ED4F File Offset: 0x0001CF4F
		internal void RequestHierarchyReset()
		{
			this.m_DebugTreeState = -1;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001ED58 File Offset: 0x0001CF58
		private void ResetAllHierarchy()
		{
			foreach (object obj in base.transform)
			{
				CoreUtils.Destroy(((Transform)obj).gameObject);
			}
			this.Rebuild();
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001EDBC File Offset: 0x0001CFBC
		private void Rebuild()
		{
			this.m_PrefabsMap.Clear();
			foreach (DebugUIPrefabBundle debugUIPrefabBundle in this.prefabs)
			{
				Type type = Type.GetType(debugUIPrefabBundle.type);
				if (type != null && debugUIPrefabBundle.prefab != null)
				{
					this.m_PrefabsMap.Add(type, debugUIPrefabBundle.prefab);
				}
			}
			this.m_UIPanels.Clear();
			this.m_DebugTreeState = DebugManager.instance.GetState();
			ReadOnlyCollection<DebugUI.Panel> panels = DebugManager.instance.panels;
			DebugUIHandlerWidget selectedWidget = null;
			foreach (DebugUI.Panel panel in panels)
			{
				if (!panel.isEditorOnly)
				{
					if (panel.children.Count((DebugUI.Widget x) => !x.isEditorOnly && !x.isHidden) != 0)
					{
						GameObject gameObject = Object.Instantiate<Transform>(this.panelPrefab, base.transform, false).gameObject;
						gameObject.name = panel.displayName;
						DebugUIHandlerPanel component = gameObject.GetComponent<DebugUIHandlerPanel>();
						component.SetPanel(panel);
						component.Canvas = this;
						this.m_UIPanels.Add(component);
						DebugUIHandlerContainer component2 = gameObject.GetComponent<DebugUIHandlerContainer>();
						DebugUIHandlerWidget debugUIHandlerWidget = null;
						this.Traverse(panel, component2.contentHolder, null, ref debugUIHandlerWidget);
						if (debugUIHandlerWidget != null && debugUIHandlerWidget.GetWidget().queryPath.Contains(panel.queryPath))
						{
							selectedWidget = debugUIHandlerWidget;
						}
					}
				}
			}
			this.ActivatePanel(this.m_SelectedPanel, selectedWidget);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001EF80 File Offset: 0x0001D180
		private void Traverse(DebugUI.IContainer container, Transform parentTransform, DebugUIHandlerWidget parentUIHandler, ref DebugUIHandlerWidget selectedHandler)
		{
			DebugUIHandlerWidget debugUIHandlerWidget = null;
			for (int i = 0; i < container.children.Count; i++)
			{
				DebugUI.Widget widget = container.children[i];
				if (!widget.isEditorOnly && !widget.isHidden)
				{
					Transform original;
					if (!this.m_PrefabsMap.TryGetValue(widget.GetType(), out original))
					{
						string str = "DebugUI widget doesn't have a prefab: ";
						Type type = widget.GetType();
						Debug.LogWarning(str + ((type != null) ? type.ToString() : null));
					}
					else
					{
						GameObject gameObject = Object.Instantiate<Transform>(original, parentTransform, false).gameObject;
						gameObject.name = widget.displayName;
						DebugUIHandlerWidget component = gameObject.GetComponent<DebugUIHandlerWidget>();
						if (component == null)
						{
							string str2 = "DebugUI prefab is missing a DebugUIHandler for: ";
							Type type2 = widget.GetType();
							Debug.LogWarning(str2 + ((type2 != null) ? type2.ToString() : null));
						}
						else
						{
							if (!string.IsNullOrEmpty(this.m_CurrentQueryPath) && widget.queryPath.Equals(this.m_CurrentQueryPath))
							{
								selectedHandler = component;
							}
							if (debugUIHandlerWidget != null)
							{
								debugUIHandlerWidget.nextUIHandler = component;
							}
							component.previousUIHandler = debugUIHandlerWidget;
							debugUIHandlerWidget = component;
							component.parentUIHandler = parentUIHandler;
							component.SetWidget(widget);
							DebugUIHandlerContainer component2 = gameObject.GetComponent<DebugUIHandlerContainer>();
							if (component2 != null)
							{
								DebugUI.IContainer container2 = widget as DebugUI.IContainer;
								if (container2 != null)
								{
									this.Traverse(container2, component2.contentHolder, component, ref selectedHandler);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001F0E4 File Offset: 0x0001D2E4
		private DebugUIHandlerWidget GetWidgetFromPath(string queryPath)
		{
			if (string.IsNullOrEmpty(queryPath))
			{
				return null;
			}
			return this.m_UIPanels[this.m_SelectedPanel].GetComponentsInChildren<DebugUIHandlerWidget>().FirstOrDefault((DebugUIHandlerWidget w) => w.GetWidget().queryPath == queryPath);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0001F134 File Offset: 0x0001D334
		private void ActivatePanel(int index, DebugUIHandlerWidget selectedWidget = null)
		{
			if (this.m_UIPanels.Count == 0)
			{
				return;
			}
			if (index >= this.m_UIPanels.Count)
			{
				index = this.m_UIPanels.Count - 1;
			}
			this.m_UIPanels.ForEach(delegate(DebugUIHandlerPanel p)
			{
				p.gameObject.SetActive(false);
			});
			this.m_UIPanels[index].gameObject.SetActive(true);
			this.m_SelectedPanel = index;
			if (selectedWidget == null)
			{
				selectedWidget = this.m_UIPanels[index].GetFirstItem();
			}
			this.ChangeSelection(selectedWidget, true);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0001F1D8 File Offset: 0x0001D3D8
		internal void ChangeSelection(DebugUIHandlerWidget widget, bool fromNext)
		{
			if (widget == null)
			{
				return;
			}
			if (this.m_SelectedWidget != null)
			{
				this.m_SelectedWidget.OnDeselection();
			}
			DebugUIHandlerWidget selectedWidget = this.m_SelectedWidget;
			this.m_SelectedWidget = widget;
			this.SetScrollTarget(widget);
			if (!this.m_SelectedWidget.OnSelection(fromNext, selectedWidget))
			{
				if (fromNext)
				{
					this.SelectNextItem();
					return;
				}
				this.SelectPreviousItem();
				return;
			}
			else
			{
				if (this.m_SelectedWidget == null || this.m_SelectedWidget.GetWidget() == null)
				{
					this.m_CurrentQueryPath = string.Empty;
					return;
				}
				this.m_CurrentQueryPath = this.m_SelectedWidget.GetWidget().queryPath;
				return;
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001F27C File Offset: 0x0001D47C
		internal void SelectPreviousItem()
		{
			if (this.m_SelectedWidget == null)
			{
				return;
			}
			DebugUIHandlerWidget debugUIHandlerWidget = this.m_SelectedWidget.Previous();
			if (debugUIHandlerWidget != null)
			{
				this.ChangeSelection(debugUIHandlerWidget, false);
			}
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001F2B8 File Offset: 0x0001D4B8
		internal void SelectNextPanel()
		{
			int num = this.m_SelectedPanel + 1;
			if (num >= this.m_UIPanels.Count)
			{
				num = 0;
			}
			num = Mathf.Clamp(num, 0, this.m_UIPanels.Count - 1);
			this.ActivatePanel(num, null);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0001F2FC File Offset: 0x0001D4FC
		internal void SelectPreviousPanel()
		{
			int num = this.m_SelectedPanel - 1;
			if (num < 0)
			{
				num = this.m_UIPanels.Count - 1;
			}
			num = Mathf.Clamp(num, 0, this.m_UIPanels.Count - 1);
			this.ActivatePanel(num, null);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0001F344 File Offset: 0x0001D544
		internal void SelectNextItem()
		{
			if (this.m_SelectedWidget == null)
			{
				return;
			}
			DebugUIHandlerWidget debugUIHandlerWidget = this.m_SelectedWidget.Next();
			if (debugUIHandlerWidget != null)
			{
				this.ChangeSelection(debugUIHandlerWidget, true);
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001F380 File Offset: 0x0001D580
		private void ChangeSelectionValue(float multiplier)
		{
			if (this.m_SelectedWidget == null)
			{
				return;
			}
			bool fast = DebugManager.instance.GetAction(DebugAction.Multiplier) != 0f;
			if (multiplier < 0f)
			{
				this.m_SelectedWidget.OnDecrement(fast);
				return;
			}
			this.m_SelectedWidget.OnIncrement(fast);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001F3D3 File Offset: 0x0001D5D3
		private void ActivateSelection()
		{
			if (this.m_SelectedWidget == null)
			{
				return;
			}
			this.m_SelectedWidget.OnAction();
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001F3F0 File Offset: 0x0001D5F0
		private void HandleInput()
		{
			if (DebugManager.instance.GetAction(DebugAction.PreviousDebugPanel) != 0f)
			{
				this.SelectPreviousPanel();
			}
			if (DebugManager.instance.GetAction(DebugAction.NextDebugPanel) != 0f)
			{
				this.SelectNextPanel();
			}
			if (DebugManager.instance.GetAction(DebugAction.Action) != 0f)
			{
				this.ActivateSelection();
			}
			if (DebugManager.instance.GetAction(DebugAction.MakePersistent) != 0f && this.m_SelectedWidget != null)
			{
				DebugManager.instance.TogglePersistent(this.m_SelectedWidget.GetWidget());
			}
			float action = DebugManager.instance.GetAction(DebugAction.MoveHorizontal);
			if (action != 0f)
			{
				this.ChangeSelectionValue(action);
			}
			float action2 = DebugManager.instance.GetAction(DebugAction.MoveVertical);
			if (action2 != 0f)
			{
				if (action2 < 0f)
				{
					this.SelectNextItem();
					return;
				}
				this.SelectPreviousItem();
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001F4C0 File Offset: 0x0001D6C0
		internal void SetScrollTarget(DebugUIHandlerWidget widget)
		{
			if (this.m_UIPanels != null && this.m_SelectedPanel < this.m_UIPanels.Count && this.m_UIPanels[this.m_SelectedPanel] != null)
			{
				this.m_UIPanels[this.m_SelectedPanel].SetScrollTarget(widget);
			}
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001F518 File Offset: 0x0001D718
		public DebugUIHandlerCanvas()
		{
		}

		// Token: 0x040003DE RID: 990
		private int m_DebugTreeState;

		// Token: 0x040003DF RID: 991
		private Dictionary<Type, Transform> m_PrefabsMap;

		// Token: 0x040003E0 RID: 992
		public Transform panelPrefab;

		// Token: 0x040003E1 RID: 993
		public List<DebugUIPrefabBundle> prefabs;

		// Token: 0x040003E2 RID: 994
		private List<DebugUIHandlerPanel> m_UIPanels;

		// Token: 0x040003E3 RID: 995
		private int m_SelectedPanel;

		// Token: 0x040003E4 RID: 996
		private DebugUIHandlerWidget m_SelectedWidget;

		// Token: 0x040003E5 RID: 997
		private string m_CurrentQueryPath;

		// Token: 0x02000183 RID: 387
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000927 RID: 2343 RVA: 0x00024AA5 File Offset: 0x00022CA5
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000928 RID: 2344 RVA: 0x00024AB1 File Offset: 0x00022CB1
			public <>c()
			{
			}

			// Token: 0x06000929 RID: 2345 RVA: 0x00024AB9 File Offset: 0x00022CB9
			internal bool <Rebuild>b__12_0(DebugUI.Widget x)
			{
				return !x.isEditorOnly && !x.isHidden;
			}

			// Token: 0x0600092A RID: 2346 RVA: 0x00024ACE File Offset: 0x00022CCE
			internal void <ActivatePanel>b__15_0(DebugUIHandlerPanel p)
			{
				p.gameObject.SetActive(false);
			}

			// Token: 0x040005CC RID: 1484
			public static readonly DebugUIHandlerCanvas.<>c <>9 = new DebugUIHandlerCanvas.<>c();

			// Token: 0x040005CD RID: 1485
			public static Func<DebugUI.Widget, bool> <>9__12_0;

			// Token: 0x040005CE RID: 1486
			public static Action<DebugUIHandlerPanel> <>9__15_0;
		}

		// Token: 0x02000184 RID: 388
		[CompilerGenerated]
		private sealed class <>c__DisplayClass14_0
		{
			// Token: 0x0600092B RID: 2347 RVA: 0x00024ADC File Offset: 0x00022CDC
			public <>c__DisplayClass14_0()
			{
			}

			// Token: 0x0600092C RID: 2348 RVA: 0x00024AE4 File Offset: 0x00022CE4
			internal bool <GetWidgetFromPath>b__0(DebugUIHandlerWidget w)
			{
				return w.GetWidget().queryPath == this.queryPath;
			}

			// Token: 0x040005CF RID: 1487
			public string queryPath;
		}
	}
}
