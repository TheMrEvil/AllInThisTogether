using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x020000BF RID: 191
	internal static class UIElementsRuntimeUtility
	{
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000649 RID: 1609 RVA: 0x00017990 File Offset: 0x00015B90
		// (remove) Token: 0x0600064A RID: 1610 RVA: 0x000179C4 File Offset: 0x00015BC4
		private static event Action s_onRepaintOverlayPanels
		{
			[CompilerGenerated]
			add
			{
				Action action = UIElementsRuntimeUtility.s_onRepaintOverlayPanels;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref UIElementsRuntimeUtility.s_onRepaintOverlayPanels, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = UIElementsRuntimeUtility.s_onRepaintOverlayPanels;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref UIElementsRuntimeUtility.s_onRepaintOverlayPanels, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x0600064B RID: 1611 RVA: 0x000179F8 File Offset: 0x00015BF8
		// (remove) Token: 0x0600064C RID: 1612 RVA: 0x00017A24 File Offset: 0x00015C24
		internal static event Action onRepaintOverlayPanels
		{
			add
			{
				bool flag = UIElementsRuntimeUtility.s_onRepaintOverlayPanels == null;
				if (flag)
				{
					UIElementsRuntimeUtility.RegisterPlayerloopCallback();
				}
				UIElementsRuntimeUtility.s_onRepaintOverlayPanels += value;
			}
			remove
			{
				UIElementsRuntimeUtility.s_onRepaintOverlayPanels -= value;
				bool flag = UIElementsRuntimeUtility.s_onRepaintOverlayPanels == null;
				if (flag)
				{
					UIElementsRuntimeUtility.UnregisterPlayerloopCallback();
				}
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600064D RID: 1613 RVA: 0x00017A50 File Offset: 0x00015C50
		// (remove) Token: 0x0600064E RID: 1614 RVA: 0x00017A84 File Offset: 0x00015C84
		public static event Action<BaseRuntimePanel> onCreatePanel
		{
			[CompilerGenerated]
			add
			{
				Action<BaseRuntimePanel> action = UIElementsRuntimeUtility.onCreatePanel;
				Action<BaseRuntimePanel> action2;
				do
				{
					action2 = action;
					Action<BaseRuntimePanel> value2 = (Action<BaseRuntimePanel>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<BaseRuntimePanel>>(ref UIElementsRuntimeUtility.onCreatePanel, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<BaseRuntimePanel> action = UIElementsRuntimeUtility.onCreatePanel;
				Action<BaseRuntimePanel> action2;
				do
				{
					action2 = action;
					Action<BaseRuntimePanel> value2 = (Action<BaseRuntimePanel>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<BaseRuntimePanel>>(ref UIElementsRuntimeUtility.onCreatePanel, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00017AB8 File Offset: 0x00015CB8
		static UIElementsRuntimeUtility()
		{
			UIElementsRuntimeUtilityNative.RepaintOverlayPanelsCallback = delegate()
			{
			};
			UIElementsRuntimeUtilityNative.RepaintOffscreenPanelsCallback = new Action(UIElementsRuntimeUtility.RepaintOffscreenPanels);
			Canvas.externBeginRenderOverlays = new Action<int>(UIElementsRuntimeUtility.BeginRenderOverlays);
			Canvas.externRenderOverlaysBefore = delegate(int displayIndex, int sortOrder)
			{
				UIElementsRuntimeUtility.RenderOverlaysBeforePriority(displayIndex, (float)sortOrder);
			};
			Canvas.externEndRenderOverlays = new Action<int>(UIElementsRuntimeUtility.EndRenderOverlays);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00017B68 File Offset: 0x00015D68
		public static EventBase CreateEvent(Event systemEvent)
		{
			return UIElementsUtility.CreateEvent(systemEvent, systemEvent.rawType);
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00017B88 File Offset: 0x00015D88
		public static BaseRuntimePanel FindOrCreateRuntimePanel(ScriptableObject ownerObject, UIElementsRuntimeUtility.CreateRuntimePanelDelegate createDelegate)
		{
			Panel panel;
			bool flag = UIElementsUtility.TryGetPanel(ownerObject.GetInstanceID(), out panel);
			if (flag)
			{
				BaseRuntimePanel baseRuntimePanel = panel as BaseRuntimePanel;
				bool flag2 = baseRuntimePanel != null;
				if (flag2)
				{
					return baseRuntimePanel;
				}
				UIElementsRuntimeUtility.RemoveCachedPanelInternal(ownerObject.GetInstanceID());
			}
			BaseRuntimePanel baseRuntimePanel2 = createDelegate(ownerObject);
			baseRuntimePanel2.IMGUIEventInterests = new EventInterests
			{
				wantsMouseMove = true,
				wantsMouseEnterLeaveWindow = true
			};
			UIElementsRuntimeUtility.RegisterCachedPanelInternal(ownerObject.GetInstanceID(), baseRuntimePanel2);
			Action<BaseRuntimePanel> action = UIElementsRuntimeUtility.onCreatePanel;
			if (action != null)
			{
				action(baseRuntimePanel2);
			}
			return baseRuntimePanel2;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00017C1C File Offset: 0x00015E1C
		public static void DisposeRuntimePanel(ScriptableObject ownerObject)
		{
			Panel panel;
			bool flag = UIElementsUtility.TryGetPanel(ownerObject.GetInstanceID(), out panel);
			if (flag)
			{
				panel.Dispose();
				UIElementsRuntimeUtility.RemoveCachedPanelInternal(ownerObject.GetInstanceID());
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00017C50 File Offset: 0x00015E50
		private static void RegisterCachedPanelInternal(int instanceID, IPanel panel)
		{
			UIElementsUtility.RegisterCachedPanel(instanceID, panel as Panel);
			UIElementsRuntimeUtility.s_PanelOrderingDirty = true;
			bool flag = !UIElementsRuntimeUtility.s_RegisteredPlayerloopCallback;
			if (flag)
			{
				UIElementsRuntimeUtility.s_RegisteredPlayerloopCallback = true;
				UIElementsRuntimeUtility.RegisterPlayerloopCallback();
				Canvas.SetExternalCanvasEnabled(true);
			}
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00017C94 File Offset: 0x00015E94
		private static void RemoveCachedPanelInternal(int instanceID)
		{
			UIElementsUtility.RemoveCachedPanel(instanceID);
			UIElementsRuntimeUtility.s_PanelOrderingDirty = true;
			UIElementsRuntimeUtility.s_SortedRuntimePanels.Clear();
			UIElementsUtility.GetAllPanels(UIElementsRuntimeUtility.s_SortedRuntimePanels, ContextType.Player);
			bool flag = UIElementsRuntimeUtility.s_SortedRuntimePanels.Count == 0;
			if (flag)
			{
				UIElementsRuntimeUtility.s_RegisteredPlayerloopCallback = false;
				UIElementsRuntimeUtility.UnregisterPlayerloopCallback();
				Canvas.SetExternalCanvasEnabled(false);
			}
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00017CEC File Offset: 0x00015EEC
		public static void RepaintOverlayPanels()
		{
			foreach (Panel panel in UIElementsRuntimeUtility.GetSortedPlayerPanels())
			{
				BaseRuntimePanel baseRuntimePanel = (BaseRuntimePanel)panel;
				bool flag = !baseRuntimePanel.drawToCameras;
				if (flag)
				{
					UIElementsRuntimeUtility.RepaintOverlayPanel(baseRuntimePanel);
				}
			}
			bool flag2 = UIElementsRuntimeUtility.s_onRepaintOverlayPanels != null;
			if (flag2)
			{
				UIElementsRuntimeUtility.s_onRepaintOverlayPanels();
			}
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00017D70 File Offset: 0x00015F70
		public static void RepaintOffscreenPanels()
		{
			foreach (Panel panel in UIElementsRuntimeUtility.GetSortedPlayerPanels())
			{
				BaseRuntimePanel baseRuntimePanel = (BaseRuntimePanel)panel;
				bool flag = baseRuntimePanel.targetTexture != null;
				if (flag)
				{
					UIElementsRuntimeUtility.RepaintOverlayPanel(baseRuntimePanel);
				}
			}
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00017DE0 File Offset: 0x00015FE0
		public static void RepaintOverlayPanel(BaseRuntimePanel panel)
		{
			Camera current = Camera.current;
			RenderTexture active = RenderTexture.active;
			Camera.SetupCurrent(null);
			RenderTexture.active = null;
			using (UIElementsRuntimeUtility.s_RepaintProfilerMarker.Auto())
			{
				panel.Repaint(Event.current);
			}
			Camera.SetupCurrent(current);
			RenderTexture.active = active;
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00017E54 File Offset: 0x00016054
		internal static void BeginRenderOverlays(int displayIndex)
		{
			UIElementsRuntimeUtility.currentOverlayIndex = 0;
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00017E60 File Offset: 0x00016060
		internal static void RenderOverlaysBeforePriority(int displayIndex, float maxPriority)
		{
			bool flag = UIElementsRuntimeUtility.currentOverlayIndex < 0;
			if (!flag)
			{
				List<Panel> sortedPlayerPanels = UIElementsRuntimeUtility.GetSortedPlayerPanels();
				while (UIElementsRuntimeUtility.currentOverlayIndex < sortedPlayerPanels.Count)
				{
					BaseRuntimePanel baseRuntimePanel = sortedPlayerPanels[UIElementsRuntimeUtility.currentOverlayIndex] as BaseRuntimePanel;
					bool flag2 = baseRuntimePanel != null;
					if (flag2)
					{
						bool flag3 = baseRuntimePanel.sortingPriority >= maxPriority;
						if (flag3)
						{
							break;
						}
						bool flag4 = baseRuntimePanel.targetDisplay == displayIndex;
						if (flag4)
						{
							UIElementsRuntimeUtility.RepaintOverlayPanel(baseRuntimePanel);
						}
					}
					UIElementsRuntimeUtility.currentOverlayIndex++;
				}
			}
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00017EEC File Offset: 0x000160EC
		internal static void EndRenderOverlays(int displayIndex)
		{
			UIElementsRuntimeUtility.RenderOverlaysBeforePriority(displayIndex, float.MaxValue);
			UIElementsRuntimeUtility.currentOverlayIndex = -1;
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x00017F01 File Offset: 0x00016101
		// (set) Token: 0x0600065C RID: 1628 RVA: 0x00017F08 File Offset: 0x00016108
		internal static Object activeEventSystem
		{
			[CompilerGenerated]
			get
			{
				return UIElementsRuntimeUtility.<activeEventSystem>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				UIElementsRuntimeUtility.<activeEventSystem>k__BackingField = value;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x00017F10 File Offset: 0x00016110
		internal static bool useDefaultEventSystem
		{
			get
			{
				return UIElementsRuntimeUtility.activeEventSystem == null;
			}
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00017F20 File Offset: 0x00016120
		public static void RegisterEventSystem(Object eventSystem)
		{
			bool flag = UIElementsRuntimeUtility.activeEventSystem != null && UIElementsRuntimeUtility.activeEventSystem != eventSystem && eventSystem.GetType().Name == "EventSystem";
			if (flag)
			{
				Debug.LogWarning("There can be only one active Event System.");
			}
			UIElementsRuntimeUtility.activeEventSystem = eventSystem;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00017F78 File Offset: 0x00016178
		public static void UnregisterEventSystem(Object eventSystem)
		{
			bool flag = UIElementsRuntimeUtility.activeEventSystem == eventSystem;
			if (flag)
			{
				UIElementsRuntimeUtility.activeEventSystem = null;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x00017F9C File Offset: 0x0001619C
		internal static DefaultEventSystem defaultEventSystem
		{
			get
			{
				DefaultEventSystem result;
				if ((result = UIElementsRuntimeUtility.s_DefaultEventSystem) == null)
				{
					result = (UIElementsRuntimeUtility.s_DefaultEventSystem = new DefaultEventSystem());
				}
				return result;
			}
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00017FB4 File Offset: 0x000161B4
		public static void UpdateRuntimePanels()
		{
			UIElementsRuntimeUtility.RemoveUnusedPanels();
			foreach (Panel panel in UIElementsRuntimeUtility.GetSortedPlayerPanels())
			{
				BaseRuntimePanel baseRuntimePanel = (BaseRuntimePanel)panel;
				baseRuntimePanel.Update();
			}
			bool flag = Application.isPlaying && UIElementsRuntimeUtility.useDefaultEventSystem;
			if (flag)
			{
				UIElementsRuntimeUtility.defaultEventSystem.Update(DefaultEventSystem.UpdateMode.IgnoreIfAppNotFocused);
			}
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00018038 File Offset: 0x00016238
		internal static void MarkPotentiallyEmpty(PanelSettings settings)
		{
			bool flag = !UIElementsRuntimeUtility.s_PotentiallyEmptyPanelSettings.Contains(settings);
			if (flag)
			{
				UIElementsRuntimeUtility.s_PotentiallyEmptyPanelSettings.Add(settings);
			}
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00018064 File Offset: 0x00016264
		internal static void RemoveUnusedPanels()
		{
			foreach (PanelSettings panelSettings in UIElementsRuntimeUtility.s_PotentiallyEmptyPanelSettings)
			{
				UIDocumentList attachedUIDocumentsList = panelSettings.m_AttachedUIDocumentsList;
				bool flag = attachedUIDocumentsList == null || attachedUIDocumentsList.m_AttachedUIDocuments.Count == 0;
				if (flag)
				{
					panelSettings.DisposePanel();
				}
			}
			UIElementsRuntimeUtility.s_PotentiallyEmptyPanelSettings.Clear();
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x000180E8 File Offset: 0x000162E8
		public static void RegisterPlayerloopCallback()
		{
			UIElementsRuntimeUtilityNative.RegisterPlayerloopCallback();
			UIElementsRuntimeUtilityNative.UpdateRuntimePanelsCallback = new Action(UIElementsRuntimeUtility.UpdateRuntimePanels);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00018102 File Offset: 0x00016302
		public static void UnregisterPlayerloopCallback()
		{
			UIElementsRuntimeUtilityNative.UnregisterPlayerloopCallback();
			UIElementsRuntimeUtilityNative.UpdateRuntimePanelsCallback = null;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00018111 File Offset: 0x00016311
		internal static void SetPanelOrderingDirty()
		{
			UIElementsRuntimeUtility.s_PanelOrderingDirty = true;
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001811C File Offset: 0x0001631C
		internal static List<Panel> GetSortedPlayerPanels()
		{
			bool flag = UIElementsRuntimeUtility.s_PanelOrderingDirty;
			if (flag)
			{
				UIElementsRuntimeUtility.SortPanels();
			}
			return UIElementsRuntimeUtility.s_SortedRuntimePanels;
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00018144 File Offset: 0x00016344
		private static void SortPanels()
		{
			UIElementsRuntimeUtility.s_SortedRuntimePanels.Clear();
			UIElementsUtility.GetAllPanels(UIElementsRuntimeUtility.s_SortedRuntimePanels, ContextType.Player);
			UIElementsRuntimeUtility.s_SortedRuntimePanels.Sort(delegate(Panel a, Panel b)
			{
				BaseRuntimePanel baseRuntimePanel = a as BaseRuntimePanel;
				BaseRuntimePanel baseRuntimePanel2 = b as BaseRuntimePanel;
				bool flag = baseRuntimePanel == null || baseRuntimePanel2 == null;
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					float num = baseRuntimePanel.sortingPriority - baseRuntimePanel2.sortingPriority;
					bool flag2 = Mathf.Approximately(0f, num);
					if (flag2)
					{
						result = baseRuntimePanel.m_RuntimePanelCreationIndex.CompareTo(baseRuntimePanel2.m_RuntimePanelCreationIndex);
					}
					else
					{
						result = ((num < 0f) ? -1 : 1);
					}
				}
				return result;
			});
			UIElementsRuntimeUtility.s_PanelOrderingDirty = false;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0001819C File Offset: 0x0001639C
		internal static Vector2 MultiDisplayBottomLeftToPanelPosition(Vector2 position, out int? targetDisplay)
		{
			Vector2 position2 = UIElementsRuntimeUtility.MultiDisplayToLocalScreenPosition(position, out targetDisplay);
			return UIElementsRuntimeUtility.ScreenBottomLeftToPanelPosition(position2, targetDisplay.GetValueOrDefault());
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x000181C4 File Offset: 0x000163C4
		internal static Vector2 MultiDisplayToLocalScreenPosition(Vector2 position, out int? targetDisplay)
		{
			Vector3 vector = Display.RelativeMouseAt(position);
			bool flag = vector != Vector3.zero;
			Vector2 result;
			if (flag)
			{
				targetDisplay = new int?((int)vector.z);
				result = vector;
			}
			else
			{
				targetDisplay = null;
				result = position;
			}
			return result;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00018218 File Offset: 0x00016418
		internal static Vector2 ScreenBottomLeftToPanelPosition(Vector2 position, int targetDisplay)
		{
			int num = Screen.height;
			bool flag = targetDisplay > 0 && targetDisplay < Display.displays.Length;
			if (flag)
			{
				num = Display.displays[targetDisplay].systemHeight;
			}
			position.y = (float)num - position.y;
			return position;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00018264 File Offset: 0x00016464
		internal static Vector2 ScreenBottomLeftToPanelDelta(Vector2 delta)
		{
			delta.y = -delta.y;
			return delta;
		}

		// Token: 0x04000283 RID: 643
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action s_onRepaintOverlayPanels;

		// Token: 0x04000284 RID: 644
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<BaseRuntimePanel> onCreatePanel;

		// Token: 0x04000285 RID: 645
		private static bool s_RegisteredPlayerloopCallback = false;

		// Token: 0x04000286 RID: 646
		private static List<Panel> s_SortedRuntimePanels = new List<Panel>();

		// Token: 0x04000287 RID: 647
		private static bool s_PanelOrderingDirty = true;

		// Token: 0x04000288 RID: 648
		internal static readonly string s_RepaintProfilerMarkerName = "UIElementsRuntimeUtility.DoDispatch(Repaint Event)";

		// Token: 0x04000289 RID: 649
		private static readonly ProfilerMarker s_RepaintProfilerMarker = new ProfilerMarker(UIElementsRuntimeUtility.s_RepaintProfilerMarkerName);

		// Token: 0x0400028A RID: 650
		private static int currentOverlayIndex = -1;

		// Token: 0x0400028B RID: 651
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Object <activeEventSystem>k__BackingField;

		// Token: 0x0400028C RID: 652
		private static DefaultEventSystem s_DefaultEventSystem;

		// Token: 0x0400028D RID: 653
		private static List<PanelSettings> s_PotentiallyEmptyPanelSettings = new List<PanelSettings>();

		// Token: 0x020000C0 RID: 192
		// (Invoke) Token: 0x0600066E RID: 1646
		public delegate BaseRuntimePanel CreateRuntimePanelDelegate(ScriptableObject ownerObject);

		// Token: 0x020000C1 RID: 193
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000671 RID: 1649 RVA: 0x00018285 File Offset: 0x00016485
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000672 RID: 1650 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c()
			{
			}

			// Token: 0x06000673 RID: 1651 RVA: 0x00002166 File Offset: 0x00000366
			internal void <.cctor>b__9_0()
			{
			}

			// Token: 0x06000674 RID: 1652 RVA: 0x00018291 File Offset: 0x00016491
			internal void <.cctor>b__9_1(int displayIndex, int sortOrder)
			{
				UIElementsRuntimeUtility.RenderOverlaysBeforePriority(displayIndex, (float)sortOrder);
			}

			// Token: 0x06000675 RID: 1653 RVA: 0x0001829C File Offset: 0x0001649C
			internal int <SortPanels>b__47_0(Panel a, Panel b)
			{
				BaseRuntimePanel baseRuntimePanel = a as BaseRuntimePanel;
				BaseRuntimePanel baseRuntimePanel2 = b as BaseRuntimePanel;
				bool flag = baseRuntimePanel == null || baseRuntimePanel2 == null;
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					float num = baseRuntimePanel.sortingPriority - baseRuntimePanel2.sortingPriority;
					bool flag2 = Mathf.Approximately(0f, num);
					if (flag2)
					{
						result = baseRuntimePanel.m_RuntimePanelCreationIndex.CompareTo(baseRuntimePanel2.m_RuntimePanelCreationIndex);
					}
					else
					{
						result = ((num < 0f) ? -1 : 1);
					}
				}
				return result;
			}

			// Token: 0x0400028E RID: 654
			public static readonly UIElementsRuntimeUtility.<>c <>9 = new UIElementsRuntimeUtility.<>c();

			// Token: 0x0400028F RID: 655
			public static Comparison<Panel> <>9__47_0;
		}
	}
}
