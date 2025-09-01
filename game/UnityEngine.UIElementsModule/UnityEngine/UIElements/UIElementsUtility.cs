using System;
using System.Collections.Generic;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x020000C5 RID: 197
	internal class UIElementsUtility : IUIElementsUtility
	{
		// Token: 0x06000690 RID: 1680 RVA: 0x00018761 File Offset: 0x00016961
		private UIElementsUtility()
		{
			UIEventRegistration.RegisterUIElementSystem(this);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00018774 File Offset: 0x00016974
		internal static IMGUIContainer GetCurrentIMGUIContainer()
		{
			bool flag = UIElementsUtility.s_ContainerStack.Count > 0;
			IMGUIContainer result;
			if (flag)
			{
				result = UIElementsUtility.s_ContainerStack.Peek();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x000187A8 File Offset: 0x000169A8
		bool IUIElementsUtility.MakeCurrentIMGUIContainerDirty()
		{
			bool flag = UIElementsUtility.s_ContainerStack.Count > 0;
			bool result;
			if (flag)
			{
				UIElementsUtility.s_ContainerStack.Peek().MarkDirtyLayout();
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x000187E4 File Offset: 0x000169E4
		bool IUIElementsUtility.TakeCapture()
		{
			bool flag = UIElementsUtility.s_ContainerStack.Count > 0;
			bool result;
			if (flag)
			{
				IMGUIContainer imguicontainer = UIElementsUtility.s_ContainerStack.Peek();
				IEventHandler capturingElement = imguicontainer.panel.GetCapturingElement(PointerId.mousePointerId);
				bool flag2 = capturingElement != null && capturingElement != imguicontainer;
				if (flag2)
				{
					Debug.Log("Should not grab hot control with an active capture");
				}
				imguicontainer.CaptureMouse();
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00018854 File Offset: 0x00016A54
		bool IUIElementsUtility.ReleaseCapture()
		{
			return false;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00018868 File Offset: 0x00016A68
		bool IUIElementsUtility.ProcessEvent(int instanceID, IntPtr nativeEventPtr, ref bool eventHandled)
		{
			Panel panel;
			bool flag = nativeEventPtr != IntPtr.Zero && UIElementsUtility.s_UIElementsCache.TryGetValue(instanceID, out panel);
			bool result;
			if (flag)
			{
				bool flag2 = panel.contextType == ContextType.Editor;
				if (flag2)
				{
					UIElementsUtility.s_EventInstance.CopyFromPtr(nativeEventPtr);
					eventHandled = UIElementsUtility.DoDispatch(panel);
				}
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x000188C4 File Offset: 0x00016AC4
		bool IUIElementsUtility.CleanupRoots()
		{
			UIElementsUtility.s_EventInstance = null;
			UIElementsUtility.s_UIElementsCache = null;
			UIElementsUtility.s_ContainerStack = null;
			return false;
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x000188EC File Offset: 0x00016AEC
		bool IUIElementsUtility.EndContainerGUIFromException(Exception exception)
		{
			bool flag = UIElementsUtility.s_ContainerStack.Count > 0;
			if (flag)
			{
				GUIUtility.EndContainer();
				UIElementsUtility.s_ContainerStack.Pop();
			}
			return false;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00018924 File Offset: 0x00016B24
		void IUIElementsUtility.UpdateSchedulers()
		{
			UIElementsUtility.s_PanelsIterationList.Clear();
			UIElementsUtility.GetAllPanels(UIElementsUtility.s_PanelsIterationList, ContextType.Editor);
			foreach (Panel panel in UIElementsUtility.s_PanelsIterationList)
			{
				panel.timerEventScheduler.UpdateScheduledEvents();
				panel.UpdateAnimations();
				panel.UpdateBindings();
			}
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x000189A8 File Offset: 0x00016BA8
		void IUIElementsUtility.RequestRepaintForPanels(Action<ScriptableObject> repaintCallback)
		{
			Dictionary<int, Panel>.Enumerator panelsIterator = UIElementsUtility.GetPanelsIterator();
			while (panelsIterator.MoveNext())
			{
				KeyValuePair<int, Panel> keyValuePair = panelsIterator.Current;
				Panel value = keyValuePair.Value;
				bool flag = value.contextType != ContextType.Editor;
				if (!flag)
				{
					bool isDirty = value.isDirty;
					if (isDirty)
					{
						repaintCallback(value.ownerObject);
					}
				}
			}
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00018A0A File Offset: 0x00016C0A
		public static void RegisterCachedPanel(int instanceID, Panel panel)
		{
			UIElementsUtility.s_UIElementsCache.Add(instanceID, panel);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00018A1A File Offset: 0x00016C1A
		public static void RemoveCachedPanel(int instanceID)
		{
			UIElementsUtility.s_UIElementsCache.Remove(instanceID);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00018A2C File Offset: 0x00016C2C
		public static bool TryGetPanel(int instanceID, out Panel panel)
		{
			return UIElementsUtility.s_UIElementsCache.TryGetValue(instanceID, out panel);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00018A4C File Offset: 0x00016C4C
		internal static void BeginContainerGUI(GUILayoutUtility.LayoutCache cache, Event evt, IMGUIContainer container)
		{
			bool useOwnerObjectGUIState = container.useOwnerObjectGUIState;
			if (useOwnerObjectGUIState)
			{
				GUIUtility.BeginContainerFromOwner(container.elementPanel.ownerObject);
			}
			else
			{
				GUIUtility.BeginContainer(container.guiState);
			}
			UIElementsUtility.s_ContainerStack.Push(container);
			GUIUtility.s_SkinMode = (int)container.contextType;
			GUIUtility.s_OriginalID = container.elementPanel.ownerObject.GetInstanceID();
			bool flag = Event.current == null;
			if (flag)
			{
				Event.current = evt;
			}
			else
			{
				Event.current.CopyFrom(evt);
			}
			GUI.enabled = container.enabledInHierarchy;
			GUILayoutUtility.BeginContainer(cache);
			GUIUtility.ResetGlobalState();
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00018AF4 File Offset: 0x00016CF4
		internal static void EndContainerGUI(Event evt, Rect layoutSize)
		{
			bool flag = Event.current.type == EventType.Layout && UIElementsUtility.s_ContainerStack.Count > 0;
			if (flag)
			{
				GUILayoutUtility.LayoutFromContainer(layoutSize.width, layoutSize.height);
			}
			GUILayoutUtility.SelectIDList(GUIUtility.s_OriginalID, false);
			GUIContent.ClearStaticCache();
			bool flag2 = UIElementsUtility.s_ContainerStack.Count > 0;
			if (flag2)
			{
			}
			evt.CopyFrom(Event.current);
			bool flag3 = UIElementsUtility.s_ContainerStack.Count > 0;
			if (flag3)
			{
				GUIUtility.EndContainer();
				UIElementsUtility.s_ContainerStack.Pop();
			}
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00018B90 File Offset: 0x00016D90
		internal static EventBase CreateEvent(Event systemEvent)
		{
			return UIElementsUtility.CreateEvent(systemEvent, systemEvent.rawType);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00018BB0 File Offset: 0x00016DB0
		internal static EventBase CreateEvent(Event systemEvent, EventType eventType)
		{
			switch (eventType)
			{
			case EventType.MouseDown:
			{
				bool flag = PointerDeviceState.HasAdditionalPressedButtons(PointerId.mousePointerId, systemEvent.button);
				if (flag)
				{
					return PointerEventBase<PointerMoveEvent>.GetPooled(systemEvent);
				}
				return PointerEventBase<PointerDownEvent>.GetPooled(systemEvent);
			}
			case EventType.MouseUp:
			{
				bool flag2 = PointerDeviceState.HasAdditionalPressedButtons(PointerId.mousePointerId, systemEvent.button);
				if (flag2)
				{
					return PointerEventBase<PointerMoveEvent>.GetPooled(systemEvent);
				}
				return PointerEventBase<PointerUpEvent>.GetPooled(systemEvent);
			}
			case EventType.MouseMove:
				return PointerEventBase<PointerMoveEvent>.GetPooled(systemEvent);
			case EventType.MouseDrag:
				return PointerEventBase<PointerMoveEvent>.GetPooled(systemEvent);
			case EventType.KeyDown:
				return KeyboardEventBase<KeyDownEvent>.GetPooled(systemEvent);
			case EventType.KeyUp:
				return KeyboardEventBase<KeyUpEvent>.GetPooled(systemEvent);
			case EventType.ScrollWheel:
				return WheelEvent.GetPooled(systemEvent);
			case EventType.ValidateCommand:
				return CommandEventBase<ValidateCommandEvent>.GetPooled(systemEvent);
			case EventType.ExecuteCommand:
				return CommandEventBase<ExecuteCommandEvent>.GetPooled(systemEvent);
			case EventType.ContextClick:
				return MouseEventBase<ContextClickEvent>.GetPooled(systemEvent);
			case EventType.MouseEnterWindow:
				return MouseEventBase<MouseEnterWindowEvent>.GetPooled(systemEvent);
			case EventType.MouseLeaveWindow:
				return MouseLeaveWindowEvent.GetPooled(systemEvent);
			}
			return IMGUIEvent.GetPooled(systemEvent);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00018CE4 File Offset: 0x00016EE4
		private static bool DoDispatch(BaseVisualElementPanel panel)
		{
			Debug.Assert(panel.contextType == ContextType.Editor);
			bool result = false;
			bool flag = UIElementsUtility.s_EventInstance.type == EventType.Repaint;
			if (flag)
			{
				Camera current = Camera.current;
				RenderTexture active = RenderTexture.active;
				Camera.SetupCurrent(null);
				RenderTexture.active = null;
				using (UIElementsUtility.s_RepaintProfilerMarker.Auto())
				{
					panel.Repaint(UIElementsUtility.s_EventInstance);
				}
				result = (panel.IMGUIContainersCount > 0);
				Camera.SetupCurrent(current);
				RenderTexture.active = active;
			}
			else
			{
				panel.ValidateLayout();
				using (EventBase eventBase = UIElementsUtility.CreateEvent(UIElementsUtility.s_EventInstance))
				{
					bool flag2 = UIElementsUtility.s_EventInstance.type == EventType.Used || UIElementsUtility.s_EventInstance.type == EventType.Layout || UIElementsUtility.s_EventInstance.type == EventType.ExecuteCommand || UIElementsUtility.s_EventInstance.type == EventType.ValidateCommand;
					using (UIElementsUtility.s_EventProfilerMarker.Auto())
					{
						panel.SendEvent(eventBase, flag2 ? DispatchMode.Immediate : DispatchMode.Default);
					}
					bool isPropagationStopped = eventBase.isPropagationStopped;
					if (isPropagationStopped)
					{
						panel.visualTree.IncrementVersion(VersionChangeType.Repaint);
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00018E60 File Offset: 0x00017060
		internal static void GetAllPanels(List<Panel> panels, ContextType contextType)
		{
			Dictionary<int, Panel>.Enumerator panelsIterator = UIElementsUtility.GetPanelsIterator();
			while (panelsIterator.MoveNext())
			{
				KeyValuePair<int, Panel> keyValuePair = panelsIterator.Current;
				bool flag = keyValuePair.Value.contextType == contextType;
				if (flag)
				{
					keyValuePair = panelsIterator.Current;
					panels.Add(keyValuePair.Value);
				}
			}
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00018EB8 File Offset: 0x000170B8
		internal static Dictionary<int, Panel>.Enumerator GetPanelsIterator()
		{
			return UIElementsUtility.s_UIElementsCache.GetEnumerator();
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00018ED4 File Offset: 0x000170D4
		internal static Panel FindOrCreateEditorPanel(ScriptableObject ownerObject)
		{
			Panel panel;
			bool flag = !UIElementsUtility.s_UIElementsCache.TryGetValue(ownerObject.GetInstanceID(), out panel);
			if (flag)
			{
				panel = Panel.CreateEditorPanel(ownerObject);
				UIElementsUtility.RegisterCachedPanel(ownerObject.GetInstanceID(), panel);
			}
			else
			{
				Debug.Assert(ContextType.Editor == panel.contextType, "Panel is not an editor panel.");
			}
			return panel;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00018F30 File Offset: 0x00017130
		internal static float PixelsPerUnitScaleForElement(VisualElement ve, Sprite sprite)
		{
			bool flag = ve == null || sprite == null;
			float result;
			if (flag)
			{
				result = 1f;
			}
			else
			{
				float num = sprite.pixelsPerUnit;
				num = Mathf.Max(0.01f, num);
				result = 100f / num;
			}
			return result;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00018F78 File Offset: 0x00017178
		internal static string ParseMenuName(string menuName)
		{
			bool flag = string.IsNullOrEmpty(menuName);
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				string text = menuName.TrimEnd(new char[0]);
				int num = text.LastIndexOf(' ');
				bool flag2 = num > -1;
				if (flag2)
				{
					int num2 = Array.IndexOf<char>(UIElementsUtility.s_Modifiers, text[num + 1]);
					bool flag3 = text.Length > num + 1 && num2 > -1;
					if (flag3)
					{
						text = text.Substring(0, num).TrimEnd(new char[0]);
					}
				}
				result = text;
			}
			return result;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00019004 File Offset: 0x00017204
		// Note: this type is marked as 'beforefieldinit'.
		static UIElementsUtility()
		{
		}

		// Token: 0x04000292 RID: 658
		private static Stack<IMGUIContainer> s_ContainerStack = new Stack<IMGUIContainer>();

		// Token: 0x04000293 RID: 659
		private static Dictionary<int, Panel> s_UIElementsCache = new Dictionary<int, Panel>();

		// Token: 0x04000294 RID: 660
		private static Event s_EventInstance = new Event();

		// Token: 0x04000295 RID: 661
		internal static Color editorPlayModeTintColor = Color.white;

		// Token: 0x04000296 RID: 662
		internal static float singleLineHeight = 18f;

		// Token: 0x04000297 RID: 663
		private static UIElementsUtility s_Instance = new UIElementsUtility();

		// Token: 0x04000298 RID: 664
		internal static List<Panel> s_PanelsIterationList = new List<Panel>();

		// Token: 0x04000299 RID: 665
		internal static readonly string s_RepaintProfilerMarkerName = "UIElementsUtility.DoDispatch(Repaint Event)";

		// Token: 0x0400029A RID: 666
		internal static readonly string s_EventProfilerMarkerName = "UIElementsUtility.DoDispatch(Non Repaint Event)";

		// Token: 0x0400029B RID: 667
		private static readonly ProfilerMarker s_RepaintProfilerMarker = new ProfilerMarker(UIElementsUtility.s_RepaintProfilerMarkerName);

		// Token: 0x0400029C RID: 668
		private static readonly ProfilerMarker s_EventProfilerMarker = new ProfilerMarker(UIElementsUtility.s_EventProfilerMarkerName);

		// Token: 0x0400029D RID: 669
		internal static char[] s_Modifiers = new char[]
		{
			'&',
			'%',
			'^',
			'#',
			'_'
		};
	}
}
