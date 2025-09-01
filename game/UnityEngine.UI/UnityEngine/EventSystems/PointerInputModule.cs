using System;
using System.Collections.Generic;
using System.Text;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200006A RID: 106
	public abstract class PointerInputModule : BaseInputModule
	{
		// Token: 0x06000618 RID: 1560 RVA: 0x0001999A File Offset: 0x00017B9A
		protected bool GetPointerData(int id, out PointerEventData data, bool create)
		{
			if (!this.m_PointerData.TryGetValue(id, out data) && create)
			{
				data = new PointerEventData(base.eventSystem)
				{
					pointerId = id
				};
				this.m_PointerData.Add(id, data);
				return true;
			}
			return false;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x000199D5 File Offset: 0x00017BD5
		protected void RemovePointerData(PointerEventData data)
		{
			this.m_PointerData.Remove(data.pointerId);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x000199EC File Offset: 0x00017BEC
		protected PointerEventData GetTouchPointerEventData(Touch input, out bool pressed, out bool released)
		{
			PointerEventData pointerEventData;
			bool pointerData = this.GetPointerData(input.fingerId, out pointerEventData, true);
			pointerEventData.Reset();
			pressed = (pointerData || input.phase == TouchPhase.Began);
			released = (input.phase == TouchPhase.Canceled || input.phase == TouchPhase.Ended);
			if (pointerData)
			{
				pointerEventData.position = input.position;
			}
			if (pressed)
			{
				pointerEventData.delta = Vector2.zero;
			}
			else
			{
				pointerEventData.delta = input.position - pointerEventData.position;
			}
			pointerEventData.position = input.position;
			pointerEventData.button = PointerEventData.InputButton.Left;
			if (input.phase == TouchPhase.Canceled)
			{
				pointerEventData.pointerCurrentRaycast = default(RaycastResult);
			}
			else
			{
				base.eventSystem.RaycastAll(pointerEventData, this.m_RaycastResultCache);
				RaycastResult pointerCurrentRaycast = BaseInputModule.FindFirstRaycast(this.m_RaycastResultCache);
				pointerEventData.pointerCurrentRaycast = pointerCurrentRaycast;
				this.m_RaycastResultCache.Clear();
			}
			pointerEventData.pressure = input.pressure;
			pointerEventData.altitudeAngle = input.altitudeAngle;
			pointerEventData.azimuthAngle = input.azimuthAngle;
			pointerEventData.radius = Vector2.one * input.radius;
			pointerEventData.radiusVariance = Vector2.one * input.radiusVariance;
			return pointerEventData;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00019B2C File Offset: 0x00017D2C
		protected void CopyFromTo(PointerEventData from, PointerEventData to)
		{
			to.position = from.position;
			to.delta = from.delta;
			to.scrollDelta = from.scrollDelta;
			to.pointerCurrentRaycast = from.pointerCurrentRaycast;
			to.pointerEnter = from.pointerEnter;
			to.pressure = from.pressure;
			to.tangentialPressure = from.tangentialPressure;
			to.altitudeAngle = from.altitudeAngle;
			to.azimuthAngle = from.azimuthAngle;
			to.twist = from.twist;
			to.radius = from.radius;
			to.radiusVariance = from.radiusVariance;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00019BCC File Offset: 0x00017DCC
		protected PointerEventData.FramePressState StateForMouseButton(int buttonId)
		{
			bool mouseButtonDown = base.input.GetMouseButtonDown(buttonId);
			bool mouseButtonUp = base.input.GetMouseButtonUp(buttonId);
			if (mouseButtonDown && mouseButtonUp)
			{
				return PointerEventData.FramePressState.PressedAndReleased;
			}
			if (mouseButtonDown)
			{
				return PointerEventData.FramePressState.Pressed;
			}
			if (mouseButtonUp)
			{
				return PointerEventData.FramePressState.Released;
			}
			return PointerEventData.FramePressState.NotChanged;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00019C05 File Offset: 0x00017E05
		protected virtual PointerInputModule.MouseState GetMousePointerEventData()
		{
			return this.GetMousePointerEventData(0);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00019C10 File Offset: 0x00017E10
		protected virtual PointerInputModule.MouseState GetMousePointerEventData(int id)
		{
			PointerEventData pointerEventData;
			bool pointerData = this.GetPointerData(-1, out pointerEventData, true);
			pointerEventData.Reset();
			if (pointerData)
			{
				pointerEventData.position = base.input.mousePosition;
			}
			Vector2 mousePosition = base.input.mousePosition;
			if (Cursor.lockState == CursorLockMode.Locked)
			{
				pointerEventData.position = new Vector2(-1f, -1f);
				pointerEventData.delta = Vector2.zero;
			}
			else
			{
				pointerEventData.delta = mousePosition - pointerEventData.position;
				pointerEventData.position = mousePosition;
			}
			pointerEventData.scrollDelta = base.input.mouseScrollDelta;
			pointerEventData.button = PointerEventData.InputButton.Left;
			base.eventSystem.RaycastAll(pointerEventData, this.m_RaycastResultCache);
			RaycastResult pointerCurrentRaycast = BaseInputModule.FindFirstRaycast(this.m_RaycastResultCache);
			pointerEventData.pointerCurrentRaycast = pointerCurrentRaycast;
			this.m_RaycastResultCache.Clear();
			PointerEventData pointerEventData2;
			this.GetPointerData(-2, out pointerEventData2, true);
			pointerEventData2.Reset();
			this.CopyFromTo(pointerEventData, pointerEventData2);
			pointerEventData2.button = PointerEventData.InputButton.Right;
			PointerEventData pointerEventData3;
			this.GetPointerData(-3, out pointerEventData3, true);
			pointerEventData3.Reset();
			this.CopyFromTo(pointerEventData, pointerEventData3);
			pointerEventData3.button = PointerEventData.InputButton.Middle;
			this.m_MouseState.SetButtonState(PointerEventData.InputButton.Left, this.StateForMouseButton(0), pointerEventData);
			this.m_MouseState.SetButtonState(PointerEventData.InputButton.Right, this.StateForMouseButton(1), pointerEventData2);
			this.m_MouseState.SetButtonState(PointerEventData.InputButton.Middle, this.StateForMouseButton(2), pointerEventData3);
			return this.m_MouseState;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00019D60 File Offset: 0x00017F60
		protected PointerEventData GetLastPointerEventData(int id)
		{
			PointerEventData result;
			this.GetPointerData(id, out result, false);
			return result;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00019D7C File Offset: 0x00017F7C
		private static bool ShouldStartDrag(Vector2 pressPos, Vector2 currentPos, float threshold, bool useDragThreshold)
		{
			return !useDragThreshold || (pressPos - currentPos).sqrMagnitude >= threshold * threshold;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00019DA8 File Offset: 0x00017FA8
		protected virtual void ProcessMove(PointerEventData pointerEvent)
		{
			GameObject newEnterTarget = (Cursor.lockState == CursorLockMode.Locked) ? null : pointerEvent.pointerCurrentRaycast.gameObject;
			base.HandlePointerExitAndEnter(pointerEvent, newEnterTarget);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00019DD8 File Offset: 0x00017FD8
		protected virtual void ProcessDrag(PointerEventData pointerEvent)
		{
			if (!pointerEvent.IsPointerMoving() || Cursor.lockState == CursorLockMode.Locked || pointerEvent.pointerDrag == null)
			{
				return;
			}
			if (!pointerEvent.dragging && PointerInputModule.ShouldStartDrag(pointerEvent.pressPosition, pointerEvent.position, (float)base.eventSystem.pixelDragThreshold, pointerEvent.useDragThreshold))
			{
				ExecuteEvents.Execute<IBeginDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.beginDragHandler);
				pointerEvent.dragging = true;
			}
			if (pointerEvent.dragging)
			{
				if (pointerEvent.pointerPress != pointerEvent.pointerDrag)
				{
					ExecuteEvents.Execute<IPointerUpHandler>(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler);
					pointerEvent.eligibleForClick = false;
					pointerEvent.pointerPress = null;
					pointerEvent.rawPointerPress = null;
				}
				ExecuteEvents.Execute<IDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.dragHandler);
			}
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00019EA0 File Offset: 0x000180A0
		public override bool IsPointerOverGameObject(int pointerId)
		{
			PointerEventData lastPointerEventData = this.GetLastPointerEventData(pointerId);
			return lastPointerEventData != null && lastPointerEventData.pointerEnter != null;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00019EC8 File Offset: 0x000180C8
		protected void ClearSelection()
		{
			BaseEventData baseEventData = this.GetBaseEventData();
			foreach (PointerEventData currentPointerData in this.m_PointerData.Values)
			{
				base.HandlePointerExitAndEnter(currentPointerData, null);
			}
			this.m_PointerData.Clear();
			base.eventSystem.SetSelectedGameObject(null, baseEventData);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00019F40 File Offset: 0x00018140
		public override string ToString()
		{
			string str = "<b>Pointer Input Module of type: </b>";
			Type type = base.GetType();
			StringBuilder stringBuilder = new StringBuilder(str + ((type != null) ? type.ToString() : null));
			stringBuilder.AppendLine();
			foreach (KeyValuePair<int, PointerEventData> keyValuePair in this.m_PointerData)
			{
				if (keyValuePair.Value != null)
				{
					stringBuilder.AppendLine("<B>Pointer:</b> " + keyValuePair.Key.ToString());
					stringBuilder.AppendLine(keyValuePair.Value.ToString());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00019FF8 File Offset: 0x000181F8
		protected void DeselectIfSelectionChanged(GameObject currentOverGo, BaseEventData pointerEvent)
		{
			if (ExecuteEvents.GetEventHandler<ISelectHandler>(currentOverGo) != base.eventSystem.currentSelectedGameObject)
			{
				base.eventSystem.SetSelectedGameObject(null, pointerEvent);
			}
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001A01F File Offset: 0x0001821F
		protected PointerInputModule()
		{
		}

		// Token: 0x0400020A RID: 522
		public const int kMouseLeftId = -1;

		// Token: 0x0400020B RID: 523
		public const int kMouseRightId = -2;

		// Token: 0x0400020C RID: 524
		public const int kMouseMiddleId = -3;

		// Token: 0x0400020D RID: 525
		public const int kFakeTouchesId = -4;

		// Token: 0x0400020E RID: 526
		protected Dictionary<int, PointerEventData> m_PointerData = new Dictionary<int, PointerEventData>();

		// Token: 0x0400020F RID: 527
		private readonly PointerInputModule.MouseState m_MouseState = new PointerInputModule.MouseState();

		// Token: 0x020000C6 RID: 198
		protected class ButtonState
		{
			// Token: 0x170001F1 RID: 497
			// (get) Token: 0x06000749 RID: 1865 RVA: 0x0001C7F9 File Offset: 0x0001A9F9
			// (set) Token: 0x0600074A RID: 1866 RVA: 0x0001C801 File Offset: 0x0001AA01
			public PointerInputModule.MouseButtonEventData eventData
			{
				get
				{
					return this.m_EventData;
				}
				set
				{
					this.m_EventData = value;
				}
			}

			// Token: 0x170001F2 RID: 498
			// (get) Token: 0x0600074B RID: 1867 RVA: 0x0001C80A File Offset: 0x0001AA0A
			// (set) Token: 0x0600074C RID: 1868 RVA: 0x0001C812 File Offset: 0x0001AA12
			public PointerEventData.InputButton button
			{
				get
				{
					return this.m_Button;
				}
				set
				{
					this.m_Button = value;
				}
			}

			// Token: 0x0600074D RID: 1869 RVA: 0x0001C81B File Offset: 0x0001AA1B
			public ButtonState()
			{
			}

			// Token: 0x04000347 RID: 839
			private PointerEventData.InputButton m_Button;

			// Token: 0x04000348 RID: 840
			private PointerInputModule.MouseButtonEventData m_EventData;
		}

		// Token: 0x020000C7 RID: 199
		protected class MouseState
		{
			// Token: 0x0600074E RID: 1870 RVA: 0x0001C824 File Offset: 0x0001AA24
			public bool AnyPressesThisFrame()
			{
				int count = this.m_TrackedButtons.Count;
				for (int i = 0; i < count; i++)
				{
					if (this.m_TrackedButtons[i].eventData.PressedThisFrame())
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600074F RID: 1871 RVA: 0x0001C864 File Offset: 0x0001AA64
			public bool AnyReleasesThisFrame()
			{
				int count = this.m_TrackedButtons.Count;
				for (int i = 0; i < count; i++)
				{
					if (this.m_TrackedButtons[i].eventData.ReleasedThisFrame())
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000750 RID: 1872 RVA: 0x0001C8A4 File Offset: 0x0001AAA4
			public PointerInputModule.ButtonState GetButtonState(PointerEventData.InputButton button)
			{
				PointerInputModule.ButtonState buttonState = null;
				int count = this.m_TrackedButtons.Count;
				for (int i = 0; i < count; i++)
				{
					if (this.m_TrackedButtons[i].button == button)
					{
						buttonState = this.m_TrackedButtons[i];
						break;
					}
				}
				if (buttonState == null)
				{
					buttonState = new PointerInputModule.ButtonState
					{
						button = button,
						eventData = new PointerInputModule.MouseButtonEventData()
					};
					this.m_TrackedButtons.Add(buttonState);
				}
				return buttonState;
			}

			// Token: 0x06000751 RID: 1873 RVA: 0x0001C916 File Offset: 0x0001AB16
			public void SetButtonState(PointerEventData.InputButton button, PointerEventData.FramePressState stateForMouseButton, PointerEventData data)
			{
				PointerInputModule.ButtonState buttonState = this.GetButtonState(button);
				buttonState.eventData.buttonState = stateForMouseButton;
				buttonState.eventData.buttonData = data;
			}

			// Token: 0x06000752 RID: 1874 RVA: 0x0001C936 File Offset: 0x0001AB36
			public MouseState()
			{
			}

			// Token: 0x04000349 RID: 841
			private List<PointerInputModule.ButtonState> m_TrackedButtons = new List<PointerInputModule.ButtonState>();
		}

		// Token: 0x020000C8 RID: 200
		public class MouseButtonEventData
		{
			// Token: 0x06000753 RID: 1875 RVA: 0x0001C949 File Offset: 0x0001AB49
			public bool PressedThisFrame()
			{
				return this.buttonState == PointerEventData.FramePressState.Pressed || this.buttonState == PointerEventData.FramePressState.PressedAndReleased;
			}

			// Token: 0x06000754 RID: 1876 RVA: 0x0001C95E File Offset: 0x0001AB5E
			public bool ReleasedThisFrame()
			{
				return this.buttonState == PointerEventData.FramePressState.Released || this.buttonState == PointerEventData.FramePressState.PressedAndReleased;
			}

			// Token: 0x06000755 RID: 1877 RVA: 0x0001C974 File Offset: 0x0001AB74
			public MouseButtonEventData()
			{
			}

			// Token: 0x0400034A RID: 842
			public PointerEventData.FramePressState buttonState;

			// Token: 0x0400034B RID: 843
			public PointerEventData buttonData;
		}
	}
}
