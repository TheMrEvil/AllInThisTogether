using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000019 RID: 25
	internal class DefaultEventSystem
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000042C9 File Offset: 0x000024C9
		private bool isAppFocused
		{
			get
			{
				return Application.isFocused;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000042D0 File Offset: 0x000024D0
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x000042F6 File Offset: 0x000024F6
		internal DefaultEventSystem.IInput input
		{
			get
			{
				DefaultEventSystem.IInput result;
				if ((result = this.m_Input) == null)
				{
					result = (this.m_Input = this.GetDefaultInput());
				}
				return result;
			}
			set
			{
				this.m_Input = value;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004300 File Offset: 0x00002500
		private DefaultEventSystem.IInput GetDefaultInput()
		{
			DefaultEventSystem.IInput input = new DefaultEventSystem.Input();
			try
			{
				input.GetAxisRaw(this.m_HorizontalAxis);
			}
			catch (InvalidOperationException)
			{
				input = new DefaultEventSystem.NoInput();
				Debug.LogWarning("UI Toolkit is currently relying on legacy Input Manager for its active input source, but the legacy Input Manager is not available using your current Project Settings. Some UI Toolkit functionality might be missing or not working properly as a result. To fix this problem, you can enable \"Input Manager (old)\" or \"Both\" in the Active Input Source setting of the Player section. UI Toolkit is using its internal default event system to process input. Alternatively, you may activate new Input System support with UI Toolkit by adding an EventSystem component to your active scene.");
			}
			return input;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004350 File Offset: 0x00002550
		private bool ShouldIgnoreEventsOnAppNotFocused()
		{
			OperatingSystemFamily operatingSystemFamily = SystemInfo.operatingSystemFamily;
			OperatingSystemFamily operatingSystemFamily2 = operatingSystemFamily;
			return operatingSystemFamily2 - OperatingSystemFamily.MacOSX <= 2;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00004377 File Offset: 0x00002577
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00004380 File Offset: 0x00002580
		public BaseRuntimePanel focusedPanel
		{
			get
			{
				return this.m_FocusedPanel;
			}
			set
			{
				bool flag = this.m_FocusedPanel != value;
				if (flag)
				{
					BaseRuntimePanel focusedPanel = this.m_FocusedPanel;
					if (focusedPanel != null)
					{
						focusedPanel.Blur();
					}
					this.m_FocusedPanel = value;
					BaseRuntimePanel focusedPanel2 = this.m_FocusedPanel;
					if (focusedPanel2 != null)
					{
						focusedPanel2.Focus();
					}
				}
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000043CC File Offset: 0x000025CC
		public void Update(DefaultEventSystem.UpdateMode updateMode = DefaultEventSystem.UpdateMode.Always)
		{
			bool flag = !this.isAppFocused && this.ShouldIgnoreEventsOnAppNotFocused() && updateMode == DefaultEventSystem.UpdateMode.IgnoreIfAppNotFocused;
			if (!flag)
			{
				this.m_SendingTouchEvents = this.ProcessTouchEvents();
				this.SendIMGUIEvents();
				this.SendInputEvents();
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004414 File Offset: 0x00002614
		private void SendIMGUIEvents()
		{
			while (Event.PopEvent(this.m_Event))
			{
				bool flag = this.m_Event.type == EventType.Repaint;
				if (!flag)
				{
					bool flag2 = this.m_Event.type == EventType.KeyUp || this.m_Event.type == EventType.KeyDown;
					if (flag2)
					{
						this.SendFocusBasedEvent<DefaultEventSystem>((DefaultEventSystem self) => UIElementsRuntimeUtility.CreateEvent(self.m_Event), this);
					}
					else
					{
						bool flag3 = !this.m_SendingTouchEvents && this.input.mousePresent;
						if (flag3)
						{
							int? targetDisplay;
							Vector2 localScreenPosition = DefaultEventSystem.GetLocalScreenPosition(this.m_Event, out targetDisplay);
							bool flag4 = this.m_Event.type == EventType.ScrollWheel;
							if (flag4)
							{
								this.SendPositionBasedEvent<DefaultEventSystem>(localScreenPosition, this.m_Event.delta, PointerId.mousePointerId, targetDisplay, delegate(Vector3 panelPosition, Vector3 panelDelta, DefaultEventSystem self)
								{
									self.m_Event.mousePosition = panelPosition;
									return UIElementsRuntimeUtility.CreateEvent(self.m_Event);
								}, this, false);
							}
							else
							{
								this.SendPositionBasedEvent<DefaultEventSystem>(localScreenPosition, this.m_Event.delta, PointerId.mousePointerId, targetDisplay, delegate(Vector3 panelPosition, Vector3 panelDelta, DefaultEventSystem self)
								{
									self.m_Event.mousePosition = panelPosition;
									self.m_Event.delta = panelDelta;
									return UIElementsRuntimeUtility.CreateEvent(self.m_Event);
								}, this, this.m_Event.type == EventType.MouseDown);
							}
						}
					}
				}
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004584 File Offset: 0x00002784
		private void SendInputEvents()
		{
			bool flag = this.ShouldSendMoveFromInput();
			bool flag2 = flag;
			if (flag2)
			{
				this.SendFocusBasedEvent<DefaultEventSystem>((DefaultEventSystem self) => NavigationMoveEvent.GetPooled(self.GetRawMoveVector()), this);
			}
			bool buttonDown = this.input.GetButtonDown(this.m_SubmitButton);
			if (buttonDown)
			{
				this.SendFocusBasedEvent<DefaultEventSystem>((DefaultEventSystem self) => EventBase<NavigationSubmitEvent>.GetPooled(), this);
			}
			bool buttonDown2 = this.input.GetButtonDown(this.m_CancelButton);
			if (buttonDown2)
			{
				this.SendFocusBasedEvent<DefaultEventSystem>((DefaultEventSystem self) => EventBase<NavigationCancelEvent>.GetPooled(), this);
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004644 File Offset: 0x00002844
		internal void SendFocusBasedEvent<TArg>(Func<TArg, EventBase> evtFactory, TArg arg)
		{
			bool flag = this.focusedPanel != null;
			if (flag)
			{
				using (EventBase eventBase = evtFactory(arg))
				{
					this.focusedPanel.visualTree.SendEvent(eventBase);
					this.UpdateFocusedPanel(this.focusedPanel);
					return;
				}
			}
			List<Panel> sortedPlayerPanels = UIElementsRuntimeUtility.GetSortedPlayerPanels();
			for (int i = sortedPlayerPanels.Count - 1; i >= 0; i--)
			{
				Panel panel = sortedPlayerPanels[i];
				BaseRuntimePanel baseRuntimePanel = panel as BaseRuntimePanel;
				bool flag2 = baseRuntimePanel != null;
				if (flag2)
				{
					using (EventBase eventBase2 = evtFactory(arg))
					{
						baseRuntimePanel.visualTree.SendEvent(eventBase2);
						bool processedByFocusController = eventBase2.processedByFocusController;
						if (processedByFocusController)
						{
							this.UpdateFocusedPanel(baseRuntimePanel);
						}
						bool isPropagationStopped = eventBase2.isPropagationStopped;
						if (isPropagationStopped)
						{
							break;
						}
					}
				}
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000474C File Offset: 0x0000294C
		internal void SendPositionBasedEvent<TArg>(Vector3 mousePosition, Vector3 delta, int pointerId, Func<Vector3, Vector3, TArg, EventBase> evtFactory, TArg arg, bool deselectIfNoTarget = false)
		{
			this.SendPositionBasedEvent<TArg>(mousePosition, delta, pointerId, null, evtFactory, arg, deselectIfNoTarget);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004774 File Offset: 0x00002974
		private void SendPositionBasedEvent<TArg>(Vector3 mousePosition, Vector3 delta, int pointerId, int? targetDisplay, Func<Vector3, Vector3, TArg, EventBase> evtFactory, TArg arg, bool deselectIfNoTarget = false)
		{
			bool flag = this.focusedPanel != null;
			if (flag)
			{
				this.UpdateFocusedPanel(this.focusedPanel);
			}
			IPanel panel = PointerDeviceState.GetPlayerPanelWithSoftPointerCapture(pointerId);
			IEventHandler capturingElement = RuntimePanel.s_EventDispatcher.pointerState.GetCapturingElement(pointerId);
			VisualElement visualElement = capturingElement as VisualElement;
			bool flag2 = visualElement != null;
			if (flag2)
			{
				panel = visualElement.panel;
			}
			BaseRuntimePanel baseRuntimePanel = null;
			Vector2 zero = Vector2.zero;
			Vector2 zero2 = Vector2.zero;
			BaseRuntimePanel baseRuntimePanel2 = panel as BaseRuntimePanel;
			bool flag3 = baseRuntimePanel2 != null;
			if (flag3)
			{
				baseRuntimePanel = baseRuntimePanel2;
				baseRuntimePanel.ScreenToPanel(mousePosition, delta, out zero, out zero2, false);
			}
			else
			{
				List<Panel> sortedPlayerPanels = UIElementsRuntimeUtility.GetSortedPlayerPanels();
				for (int i = sortedPlayerPanels.Count - 1; i >= 0; i--)
				{
					BaseRuntimePanel baseRuntimePanel3 = sortedPlayerPanels[i] as BaseRuntimePanel;
					bool flag4;
					if (baseRuntimePanel3 != null)
					{
						if (targetDisplay != null)
						{
							int targetDisplay2 = baseRuntimePanel3.targetDisplay;
							int? num = targetDisplay;
							flag4 = (targetDisplay2 == num.GetValueOrDefault() & num != null);
						}
						else
						{
							flag4 = true;
						}
					}
					else
					{
						flag4 = false;
					}
					bool flag5 = flag4;
					if (flag5)
					{
						bool flag6 = baseRuntimePanel3.ScreenToPanel(mousePosition, delta, out zero, out zero2, false) && baseRuntimePanel3.Pick(zero) != null;
						if (flag6)
						{
							baseRuntimePanel = baseRuntimePanel3;
							break;
						}
					}
				}
			}
			BaseRuntimePanel baseRuntimePanel4 = PointerDeviceState.GetPanel(pointerId, ContextType.Player) as BaseRuntimePanel;
			bool flag7 = baseRuntimePanel4 != baseRuntimePanel;
			if (flag7)
			{
				if (baseRuntimePanel4 != null)
				{
					baseRuntimePanel4.PointerLeavesPanel(pointerId, baseRuntimePanel4.ScreenToPanel(mousePosition));
				}
				if (baseRuntimePanel != null)
				{
					baseRuntimePanel.PointerEntersPanel(pointerId, zero);
				}
			}
			bool flag8 = baseRuntimePanel != null;
			if (flag8)
			{
				using (EventBase eventBase = evtFactory(zero, zero2, arg))
				{
					baseRuntimePanel.visualTree.SendEvent(eventBase);
					bool processedByFocusController = eventBase.processedByFocusController;
					if (processedByFocusController)
					{
						this.UpdateFocusedPanel(baseRuntimePanel);
					}
					bool flag9 = eventBase.eventTypeId == EventBase<PointerDownEvent>.TypeId();
					if (flag9)
					{
						PointerDeviceState.SetPlayerPanelWithSoftPointerCapture(pointerId, baseRuntimePanel);
					}
					else
					{
						bool flag10 = eventBase.eventTypeId == EventBase<PointerUpEvent>.TypeId() && ((PointerUpEvent)eventBase).pressedButtons == 0;
						if (flag10)
						{
							PointerDeviceState.SetPlayerPanelWithSoftPointerCapture(pointerId, null);
						}
					}
				}
			}
			else if (deselectIfNoTarget)
			{
				this.focusedPanel = null;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000049E0 File Offset: 0x00002BE0
		private void UpdateFocusedPanel(BaseRuntimePanel runtimePanel)
		{
			bool flag = runtimePanel.focusController.focusedElement != null;
			if (flag)
			{
				this.focusedPanel = runtimePanel;
			}
			else
			{
				bool flag2 = this.focusedPanel == runtimePanel;
				if (flag2)
				{
					this.focusedPanel = null;
				}
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004A24 File Offset: 0x00002C24
		private static EventBase MakeTouchEvent(Touch touch, EventModifiers modifiers)
		{
			EventBase result;
			switch (touch.phase)
			{
			case TouchPhase.Began:
				result = PointerEventBase<PointerDownEvent>.GetPooled(touch, modifiers);
				break;
			case TouchPhase.Moved:
				result = PointerEventBase<PointerMoveEvent>.GetPooled(touch, modifiers);
				break;
			case TouchPhase.Stationary:
				result = PointerEventBase<PointerStationaryEvent>.GetPooled(touch, modifiers);
				break;
			case TouchPhase.Ended:
				result = PointerEventBase<PointerUpEvent>.GetPooled(touch, modifiers);
				break;
			case TouchPhase.Canceled:
				result = PointerEventBase<PointerCancelEvent>.GetPooled(touch, modifiers);
				break;
			default:
				result = null;
				break;
			}
			return result;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004A90 File Offset: 0x00002C90
		private bool ProcessTouchEvents()
		{
			for (int i = 0; i < this.input.touchCount; i++)
			{
				Touch touch = this.input.GetTouch(i);
				bool flag = touch.type == TouchType.Indirect;
				if (!flag)
				{
					int? targetDisplay;
					touch.position = UIElementsRuntimeUtility.MultiDisplayBottomLeftToPanelPosition(touch.position, out targetDisplay);
					int? num;
					touch.rawPosition = UIElementsRuntimeUtility.MultiDisplayBottomLeftToPanelPosition(touch.rawPosition, out num);
					touch.deltaPosition = UIElementsRuntimeUtility.ScreenBottomLeftToPanelDelta(touch.deltaPosition);
					this.SendPositionBasedEvent<Touch>(touch.position, touch.deltaPosition, PointerId.touchPointerIdBase + touch.fingerId, targetDisplay, delegate(Vector3 panelPosition, Vector3 panelDelta, Touch _touch)
					{
						_touch.position = panelPosition;
						_touch.deltaPosition = panelDelta;
						return DefaultEventSystem.MakeTouchEvent(_touch, EventModifiers.None);
					}, touch, false);
				}
			}
			return this.input.touchCount > 0;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004B84 File Offset: 0x00002D84
		private Vector2 GetRawMoveVector()
		{
			Vector2 zero = Vector2.zero;
			zero.x = this.input.GetAxisRaw(this.m_HorizontalAxis);
			zero.y = this.input.GetAxisRaw(this.m_VerticalAxis);
			bool buttonDown = this.input.GetButtonDown(this.m_HorizontalAxis);
			if (buttonDown)
			{
				bool flag = zero.x < 0f;
				if (flag)
				{
					zero.x = -1f;
				}
				bool flag2 = zero.x > 0f;
				if (flag2)
				{
					zero.x = 1f;
				}
			}
			bool buttonDown2 = this.input.GetButtonDown(this.m_VerticalAxis);
			if (buttonDown2)
			{
				bool flag3 = zero.y < 0f;
				if (flag3)
				{
					zero.y = -1f;
				}
				bool flag4 = zero.y > 0f;
				if (flag4)
				{
					zero.y = 1f;
				}
			}
			return zero;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004C78 File Offset: 0x00002E78
		private bool ShouldSendMoveFromInput()
		{
			float unscaledTime = Time.unscaledTime;
			Vector2 rawMoveVector = this.GetRawMoveVector();
			bool flag = Mathf.Approximately(rawMoveVector.x, 0f) && Mathf.Approximately(rawMoveVector.y, 0f);
			bool result;
			if (flag)
			{
				this.m_ConsecutiveMoveCount = 0;
				result = false;
			}
			else
			{
				bool flag2 = this.input.GetButtonDown(this.m_HorizontalAxis) || this.input.GetButtonDown(this.m_VerticalAxis);
				bool flag3 = Vector2.Dot(rawMoveVector, this.m_LastMoveVector) > 0f;
				bool flag4 = !flag2;
				if (flag4)
				{
					bool flag5 = flag3 && this.m_ConsecutiveMoveCount == 1;
					if (flag5)
					{
						flag2 = (unscaledTime > this.m_PrevActionTime + this.m_RepeatDelay);
					}
					else
					{
						flag2 = (unscaledTime > this.m_PrevActionTime + 1f / this.m_InputActionsPerSecond);
					}
				}
				bool flag6 = !flag2;
				if (flag6)
				{
					result = false;
				}
				else
				{
					NavigationMoveEvent.Direction direction = NavigationMoveEvent.DetermineMoveDirection(rawMoveVector.x, rawMoveVector.y, 0.6f);
					bool flag7 = direction > NavigationMoveEvent.Direction.None;
					if (flag7)
					{
						bool flag8 = !flag3;
						if (flag8)
						{
							this.m_ConsecutiveMoveCount = 0;
						}
						this.m_ConsecutiveMoveCount++;
						this.m_PrevActionTime = unscaledTime;
						this.m_LastMoveVector = rawMoveVector;
					}
					else
					{
						this.m_ConsecutiveMoveCount = 0;
					}
					result = (direction > NavigationMoveEvent.Direction.None);
				}
			}
			return result;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004DCC File Offset: 0x00002FCC
		private static Vector2 GetLocalScreenPosition(Event evt, out int? targetDisplay)
		{
			targetDisplay = null;
			return evt.mousePosition;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004DEC File Offset: 0x00002FEC
		public DefaultEventSystem()
		{
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004E4D File Offset: 0x0000304D
		// Note: this type is marked as 'beforefieldinit'.
		static DefaultEventSystem()
		{
		}

		// Token: 0x0400003C RID: 60
		internal static Func<bool> IsEditorRemoteConnected = () => false;

		// Token: 0x0400003D RID: 61
		private DefaultEventSystem.IInput m_Input;

		// Token: 0x0400003E RID: 62
		private readonly string m_HorizontalAxis = "Horizontal";

		// Token: 0x0400003F RID: 63
		private readonly string m_VerticalAxis = "Vertical";

		// Token: 0x04000040 RID: 64
		private readonly string m_SubmitButton = "Submit";

		// Token: 0x04000041 RID: 65
		private readonly string m_CancelButton = "Cancel";

		// Token: 0x04000042 RID: 66
		private readonly float m_InputActionsPerSecond = 10f;

		// Token: 0x04000043 RID: 67
		private readonly float m_RepeatDelay = 0.5f;

		// Token: 0x04000044 RID: 68
		private bool m_SendingTouchEvents;

		// Token: 0x04000045 RID: 69
		private Event m_Event = new Event();

		// Token: 0x04000046 RID: 70
		private BaseRuntimePanel m_FocusedPanel;

		// Token: 0x04000047 RID: 71
		private int m_ConsecutiveMoveCount;

		// Token: 0x04000048 RID: 72
		private Vector2 m_LastMoveVector;

		// Token: 0x04000049 RID: 73
		private float m_PrevActionTime;

		// Token: 0x0200001A RID: 26
		public enum UpdateMode
		{
			// Token: 0x0400004B RID: 75
			Always,
			// Token: 0x0400004C RID: 76
			IgnoreIfAppNotFocused
		}

		// Token: 0x0200001B RID: 27
		internal interface IInput
		{
			// Token: 0x060000BA RID: 186
			bool GetButtonDown(string button);

			// Token: 0x060000BB RID: 187
			float GetAxisRaw(string axis);

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x060000BC RID: 188
			int touchCount { get; }

			// Token: 0x060000BD RID: 189
			Touch GetTouch(int index);

			// Token: 0x1700001C RID: 28
			// (get) Token: 0x060000BE RID: 190
			bool mousePresent { get; }
		}

		// Token: 0x0200001C RID: 28
		private class Input : DefaultEventSystem.IInput
		{
			// Token: 0x060000BF RID: 191 RVA: 0x00004E64 File Offset: 0x00003064
			public bool GetButtonDown(string button)
			{
				return UnityEngine.Input.GetButtonDown(button);
			}

			// Token: 0x060000C0 RID: 192 RVA: 0x00004E6C File Offset: 0x0000306C
			public float GetAxisRaw(string axis)
			{
				return UnityEngine.Input.GetAxis(axis);
			}

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x060000C1 RID: 193 RVA: 0x00004E74 File Offset: 0x00003074
			public int touchCount
			{
				get
				{
					return UnityEngine.Input.touchCount;
				}
			}

			// Token: 0x060000C2 RID: 194 RVA: 0x00004E7B File Offset: 0x0000307B
			public Touch GetTouch(int index)
			{
				return UnityEngine.Input.GetTouch(index);
			}

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x060000C3 RID: 195 RVA: 0x00004E83 File Offset: 0x00003083
			public bool mousePresent
			{
				get
				{
					return UnityEngine.Input.mousePresent;
				}
			}

			// Token: 0x060000C4 RID: 196 RVA: 0x000020C2 File Offset: 0x000002C2
			public Input()
			{
			}
		}

		// Token: 0x0200001D RID: 29
		private class NoInput : DefaultEventSystem.IInput
		{
			// Token: 0x060000C5 RID: 197 RVA: 0x00004E8A File Offset: 0x0000308A
			public bool GetButtonDown(string button)
			{
				return false;
			}

			// Token: 0x060000C6 RID: 198 RVA: 0x00004E8D File Offset: 0x0000308D
			public float GetAxisRaw(string axis)
			{
				return 0f;
			}

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x060000C7 RID: 199 RVA: 0x00004E8A File Offset: 0x0000308A
			public int touchCount
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x060000C8 RID: 200 RVA: 0x00004E94 File Offset: 0x00003094
			public Touch GetTouch(int index)
			{
				return default(Touch);
			}

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x060000C9 RID: 201 RVA: 0x00004E8A File Offset: 0x0000308A
			public bool mousePresent
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060000CA RID: 202 RVA: 0x000020C2 File Offset: 0x000002C2
			public NoInput()
			{
			}
		}

		// Token: 0x0200001E RID: 30
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060000CB RID: 203 RVA: 0x00004EAA File Offset: 0x000030AA
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060000CC RID: 204 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c()
			{
			}

			// Token: 0x060000CD RID: 205 RVA: 0x00004EB6 File Offset: 0x000030B6
			internal EventBase <SendIMGUIEvents>b__23_0(DefaultEventSystem self)
			{
				return UIElementsRuntimeUtility.CreateEvent(self.m_Event);
			}

			// Token: 0x060000CE RID: 206 RVA: 0x00004EC4 File Offset: 0x000030C4
			internal EventBase <SendIMGUIEvents>b__23_1(Vector3 panelPosition, Vector3 panelDelta, DefaultEventSystem self)
			{
				self.m_Event.mousePosition = panelPosition;
				return UIElementsRuntimeUtility.CreateEvent(self.m_Event);
			}

			// Token: 0x060000CF RID: 207 RVA: 0x00004EF4 File Offset: 0x000030F4
			internal EventBase <SendIMGUIEvents>b__23_2(Vector3 panelPosition, Vector3 panelDelta, DefaultEventSystem self)
			{
				self.m_Event.mousePosition = panelPosition;
				self.m_Event.delta = panelDelta;
				return UIElementsRuntimeUtility.CreateEvent(self.m_Event);
			}

			// Token: 0x060000D0 RID: 208 RVA: 0x00004F35 File Offset: 0x00003135
			internal EventBase <SendInputEvents>b__24_0(DefaultEventSystem self)
			{
				return NavigationMoveEvent.GetPooled(self.GetRawMoveVector());
			}

			// Token: 0x060000D1 RID: 209 RVA: 0x00004F42 File Offset: 0x00003142
			internal EventBase <SendInputEvents>b__24_1(DefaultEventSystem self)
			{
				return EventBase<NavigationSubmitEvent>.GetPooled();
			}

			// Token: 0x060000D2 RID: 210 RVA: 0x00004F49 File Offset: 0x00003149
			internal EventBase <SendInputEvents>b__24_2(DefaultEventSystem self)
			{
				return EventBase<NavigationCancelEvent>.GetPooled();
			}

			// Token: 0x060000D3 RID: 211 RVA: 0x00004F50 File Offset: 0x00003150
			internal EventBase <ProcessTouchEvents>b__30_0(Vector3 panelPosition, Vector3 panelDelta, Touch _touch)
			{
				_touch.position = panelPosition;
				_touch.deltaPosition = panelDelta;
				return DefaultEventSystem.MakeTouchEvent(_touch, EventModifiers.None);
			}

			// Token: 0x060000D4 RID: 212 RVA: 0x00004E8A File Offset: 0x0000308A
			internal bool <.cctor>b__41_0()
			{
				return false;
			}

			// Token: 0x0400004D RID: 77
			public static readonly DefaultEventSystem.<>c <>9 = new DefaultEventSystem.<>c();

			// Token: 0x0400004E RID: 78
			public static Func<DefaultEventSystem, EventBase> <>9__23_0;

			// Token: 0x0400004F RID: 79
			public static Func<Vector3, Vector3, DefaultEventSystem, EventBase> <>9__23_1;

			// Token: 0x04000050 RID: 80
			public static Func<Vector3, Vector3, DefaultEventSystem, EventBase> <>9__23_2;

			// Token: 0x04000051 RID: 81
			public static Func<DefaultEventSystem, EventBase> <>9__24_0;

			// Token: 0x04000052 RID: 82
			public static Func<DefaultEventSystem, EventBase> <>9__24_1;

			// Token: 0x04000053 RID: 83
			public static Func<DefaultEventSystem, EventBase> <>9__24_2;

			// Token: 0x04000054 RID: 84
			public static Func<Vector3, Vector3, Touch, EventBase> <>9__30_0;
		}
	}
}
