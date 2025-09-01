using System;
using UnityEngine.Serialization;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200006B RID: 107
	[AddComponentMenu("Event/Standalone Input Module")]
	public class StandaloneInputModule : PointerInputModule
	{
		// Token: 0x06000628 RID: 1576 RVA: 0x0001A040 File Offset: 0x00018240
		protected StandaloneInputModule()
		{
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x0001A095 File Offset: 0x00018295
		[Obsolete("Mode is no longer needed on input module as it handles both mouse and keyboard simultaneously.", false)]
		public StandaloneInputModule.InputMode inputMode
		{
			get
			{
				return StandaloneInputModule.InputMode.Mouse;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x0001A098 File Offset: 0x00018298
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x0001A0A0 File Offset: 0x000182A0
		[Obsolete("allowActivationOnMobileDevice has been deprecated. Use forceModuleActive instead (UnityUpgradable) -> forceModuleActive")]
		public bool allowActivationOnMobileDevice
		{
			get
			{
				return this.m_ForceModuleActive;
			}
			set
			{
				this.m_ForceModuleActive = value;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0001A0A9 File Offset: 0x000182A9
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x0001A0B1 File Offset: 0x000182B1
		[Obsolete("forceModuleActive has been deprecated. There is no need to force the module awake as StandaloneInputModule works for all platforms")]
		public bool forceModuleActive
		{
			get
			{
				return this.m_ForceModuleActive;
			}
			set
			{
				this.m_ForceModuleActive = value;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0001A0BA File Offset: 0x000182BA
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x0001A0C2 File Offset: 0x000182C2
		public float inputActionsPerSecond
		{
			get
			{
				return this.m_InputActionsPerSecond;
			}
			set
			{
				this.m_InputActionsPerSecond = value;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x0001A0CB File Offset: 0x000182CB
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x0001A0D3 File Offset: 0x000182D3
		public float repeatDelay
		{
			get
			{
				return this.m_RepeatDelay;
			}
			set
			{
				this.m_RepeatDelay = value;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x0001A0DC File Offset: 0x000182DC
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x0001A0E4 File Offset: 0x000182E4
		public string horizontalAxis
		{
			get
			{
				return this.m_HorizontalAxis;
			}
			set
			{
				this.m_HorizontalAxis = value;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0001A0ED File Offset: 0x000182ED
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x0001A0F5 File Offset: 0x000182F5
		public string verticalAxis
		{
			get
			{
				return this.m_VerticalAxis;
			}
			set
			{
				this.m_VerticalAxis = value;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0001A0FE File Offset: 0x000182FE
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x0001A106 File Offset: 0x00018306
		public string submitButton
		{
			get
			{
				return this.m_SubmitButton;
			}
			set
			{
				this.m_SubmitButton = value;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x0001A10F File Offset: 0x0001830F
		// (set) Token: 0x06000639 RID: 1593 RVA: 0x0001A117 File Offset: 0x00018317
		public string cancelButton
		{
			get
			{
				return this.m_CancelButton;
			}
			set
			{
				this.m_CancelButton = value;
			}
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001A120 File Offset: 0x00018320
		private bool ShouldIgnoreEventsOnNoFocus()
		{
			return true;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001A124 File Offset: 0x00018324
		public override void UpdateModule()
		{
			if (!base.eventSystem.isFocused && this.ShouldIgnoreEventsOnNoFocus())
			{
				if (this.m_InputPointerEvent != null && this.m_InputPointerEvent.pointerDrag != null && this.m_InputPointerEvent.dragging)
				{
					this.ReleaseMouse(this.m_InputPointerEvent, this.m_InputPointerEvent.pointerCurrentRaycast.gameObject);
				}
				this.m_InputPointerEvent = null;
				return;
			}
			this.m_LastMousePosition = this.m_MousePosition;
			this.m_MousePosition = base.input.mousePosition;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0001A1B4 File Offset: 0x000183B4
		private void ReleaseMouse(PointerEventData pointerEvent, GameObject currentOverGo)
		{
			ExecuteEvents.Execute<IPointerUpHandler>(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler);
			GameObject eventHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentOverGo);
			if (pointerEvent.pointerClick == eventHandler && pointerEvent.eligibleForClick)
			{
				ExecuteEvents.Execute<IPointerClickHandler>(pointerEvent.pointerClick, pointerEvent, ExecuteEvents.pointerClickHandler);
			}
			if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
			{
				ExecuteEvents.ExecuteHierarchy<IDropHandler>(currentOverGo, pointerEvent, ExecuteEvents.dropHandler);
			}
			pointerEvent.eligibleForClick = false;
			pointerEvent.pointerPress = null;
			pointerEvent.rawPointerPress = null;
			pointerEvent.pointerClick = null;
			if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
			{
				ExecuteEvents.Execute<IEndDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.endDragHandler);
			}
			pointerEvent.dragging = false;
			pointerEvent.pointerDrag = null;
			if (currentOverGo != pointerEvent.pointerEnter)
			{
				base.HandlePointerExitAndEnter(pointerEvent, null);
				base.HandlePointerExitAndEnter(pointerEvent, currentOverGo);
			}
			this.m_InputPointerEvent = pointerEvent;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001A29C File Offset: 0x0001849C
		public override bool ShouldActivateModule()
		{
			if (!base.ShouldActivateModule())
			{
				return false;
			}
			bool flag = this.m_ForceModuleActive;
			flag |= base.input.GetButtonDown(this.m_SubmitButton);
			flag |= base.input.GetButtonDown(this.m_CancelButton);
			flag |= !Mathf.Approximately(base.input.GetAxisRaw(this.m_HorizontalAxis), 0f);
			flag |= !Mathf.Approximately(base.input.GetAxisRaw(this.m_VerticalAxis), 0f);
			flag |= ((this.m_MousePosition - this.m_LastMousePosition).sqrMagnitude > 0f);
			flag |= base.input.GetMouseButtonDown(0);
			if (base.input.touchCount > 0)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001A368 File Offset: 0x00018568
		public override void ActivateModule()
		{
			if (!base.eventSystem.isFocused && this.ShouldIgnoreEventsOnNoFocus())
			{
				return;
			}
			base.ActivateModule();
			this.m_MousePosition = base.input.mousePosition;
			this.m_LastMousePosition = base.input.mousePosition;
			GameObject gameObject = base.eventSystem.currentSelectedGameObject;
			if (gameObject == null)
			{
				gameObject = base.eventSystem.firstSelectedGameObject;
			}
			base.eventSystem.SetSelectedGameObject(gameObject, this.GetBaseEventData());
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001A3E6 File Offset: 0x000185E6
		public override void DeactivateModule()
		{
			base.DeactivateModule();
			base.ClearSelection();
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001A3F4 File Offset: 0x000185F4
		public override void Process()
		{
			if (!base.eventSystem.isFocused && this.ShouldIgnoreEventsOnNoFocus())
			{
				return;
			}
			bool flag = this.SendUpdateEventToSelectedObject();
			if (!this.ProcessTouchEvents() && base.input.mousePresent)
			{
				this.ProcessMouseEvent();
			}
			if (base.eventSystem.sendNavigationEvents)
			{
				if (!flag)
				{
					flag |= this.SendMoveEventToSelectedObject();
				}
				if (!flag)
				{
					this.SendSubmitEventToSelectedObject();
				}
			}
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001A45C File Offset: 0x0001865C
		private bool ProcessTouchEvents()
		{
			for (int i = 0; i < base.input.touchCount; i++)
			{
				Touch touch = base.input.GetTouch(i);
				if (touch.type != TouchType.Indirect)
				{
					bool pressed;
					bool flag;
					PointerEventData touchPointerEventData = base.GetTouchPointerEventData(touch, out pressed, out flag);
					this.ProcessTouchPress(touchPointerEventData, pressed, flag);
					if (!flag)
					{
						this.ProcessMove(touchPointerEventData);
						this.ProcessDrag(touchPointerEventData);
					}
					else
					{
						base.RemovePointerData(touchPointerEventData);
					}
				}
			}
			return base.input.touchCount > 0;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001A4D8 File Offset: 0x000186D8
		protected void ProcessTouchPress(PointerEventData pointerEvent, bool pressed, bool released)
		{
			GameObject gameObject = pointerEvent.pointerCurrentRaycast.gameObject;
			if (pressed)
			{
				pointerEvent.eligibleForClick = true;
				pointerEvent.delta = Vector2.zero;
				pointerEvent.dragging = false;
				pointerEvent.useDragThreshold = true;
				pointerEvent.pressPosition = pointerEvent.position;
				pointerEvent.pointerPressRaycast = pointerEvent.pointerCurrentRaycast;
				base.DeselectIfSelectionChanged(gameObject, pointerEvent);
				if (pointerEvent.pointerEnter != gameObject)
				{
					base.HandlePointerExitAndEnter(pointerEvent, gameObject);
					pointerEvent.pointerEnter = gameObject;
				}
				GameObject gameObject2 = ExecuteEvents.ExecuteHierarchy<IPointerDownHandler>(gameObject, pointerEvent, ExecuteEvents.pointerDownHandler);
				GameObject eventHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				if (gameObject2 == null)
				{
					gameObject2 = eventHandler;
				}
				float unscaledTime = Time.unscaledTime;
				if (gameObject2 == pointerEvent.lastPress)
				{
					if (unscaledTime - pointerEvent.clickTime < 0.3f)
					{
						int clickCount = pointerEvent.clickCount + 1;
						pointerEvent.clickCount = clickCount;
					}
					else
					{
						pointerEvent.clickCount = 1;
					}
					pointerEvent.clickTime = unscaledTime;
				}
				else
				{
					pointerEvent.clickCount = 1;
				}
				pointerEvent.pointerPress = gameObject2;
				pointerEvent.rawPointerPress = gameObject;
				pointerEvent.pointerClick = eventHandler;
				pointerEvent.clickTime = unscaledTime;
				pointerEvent.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(gameObject);
				if (pointerEvent.pointerDrag != null)
				{
					ExecuteEvents.Execute<IInitializePotentialDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.initializePotentialDrag);
				}
			}
			if (released)
			{
				ExecuteEvents.Execute<IPointerUpHandler>(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler);
				GameObject eventHandler2 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				if (pointerEvent.pointerClick == eventHandler2 && pointerEvent.eligibleForClick)
				{
					ExecuteEvents.Execute<IPointerClickHandler>(pointerEvent.pointerClick, pointerEvent, ExecuteEvents.pointerClickHandler);
				}
				if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
				{
					ExecuteEvents.ExecuteHierarchy<IDropHandler>(gameObject, pointerEvent, ExecuteEvents.dropHandler);
				}
				pointerEvent.eligibleForClick = false;
				pointerEvent.pointerPress = null;
				pointerEvent.rawPointerPress = null;
				pointerEvent.pointerClick = null;
				if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
				{
					ExecuteEvents.Execute<IEndDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.endDragHandler);
				}
				pointerEvent.dragging = false;
				pointerEvent.pointerDrag = null;
				ExecuteEvents.ExecuteHierarchy<IPointerExitHandler>(pointerEvent.pointerEnter, pointerEvent, ExecuteEvents.pointerExitHandler);
				pointerEvent.pointerEnter = null;
			}
			this.m_InputPointerEvent = pointerEvent;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0001A6F0 File Offset: 0x000188F0
		protected bool SendSubmitEventToSelectedObject()
		{
			if (base.eventSystem.currentSelectedGameObject == null)
			{
				return false;
			}
			BaseEventData baseEventData = this.GetBaseEventData();
			if (base.input.GetButtonDown(this.m_SubmitButton))
			{
				ExecuteEvents.Execute<ISubmitHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.submitHandler);
			}
			if (base.input.GetButtonDown(this.m_CancelButton))
			{
				ExecuteEvents.Execute<ICancelHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.cancelHandler);
			}
			return baseEventData.used;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001A774 File Offset: 0x00018974
		private Vector2 GetRawMoveVector()
		{
			Vector2 zero = Vector2.zero;
			zero.x = base.input.GetAxisRaw(this.m_HorizontalAxis);
			zero.y = base.input.GetAxisRaw(this.m_VerticalAxis);
			if (base.input.GetButtonDown(this.m_HorizontalAxis))
			{
				if (zero.x < 0f)
				{
					zero.x = -1f;
				}
				if (zero.x > 0f)
				{
					zero.x = 1f;
				}
			}
			if (base.input.GetButtonDown(this.m_VerticalAxis))
			{
				if (zero.y < 0f)
				{
					zero.y = -1f;
				}
				if (zero.y > 0f)
				{
					zero.y = 1f;
				}
			}
			return zero;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0001A844 File Offset: 0x00018A44
		protected bool SendMoveEventToSelectedObject()
		{
			float unscaledTime = Time.unscaledTime;
			Vector2 rawMoveVector = this.GetRawMoveVector();
			if (Mathf.Approximately(rawMoveVector.x, 0f) && Mathf.Approximately(rawMoveVector.y, 0f))
			{
				this.m_ConsecutiveMoveCount = 0;
				return false;
			}
			bool flag = Vector2.Dot(rawMoveVector, this.m_LastMoveVector) > 0f;
			if (flag && this.m_ConsecutiveMoveCount == 1)
			{
				if (unscaledTime <= this.m_PrevActionTime + this.m_RepeatDelay)
				{
					return false;
				}
			}
			else if (unscaledTime <= this.m_PrevActionTime + 1f / this.m_InputActionsPerSecond)
			{
				return false;
			}
			AxisEventData axisEventData = this.GetAxisEventData(rawMoveVector.x, rawMoveVector.y, 0.6f);
			if (axisEventData.moveDir != MoveDirection.None)
			{
				ExecuteEvents.Execute<IMoveHandler>(base.eventSystem.currentSelectedGameObject, axisEventData, ExecuteEvents.moveHandler);
				if (!flag)
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
			return axisEventData.used;
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001A942 File Offset: 0x00018B42
		protected void ProcessMouseEvent()
		{
			this.ProcessMouseEvent(0);
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001A94B File Offset: 0x00018B4B
		[Obsolete("This method is no longer checked, overriding it with return true does nothing!")]
		protected virtual bool ForceAutoSelect()
		{
			return false;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001A950 File Offset: 0x00018B50
		protected void ProcessMouseEvent(int id)
		{
			PointerInputModule.MouseState mousePointerEventData = this.GetMousePointerEventData(id);
			PointerInputModule.MouseButtonEventData eventData = mousePointerEventData.GetButtonState(PointerEventData.InputButton.Left).eventData;
			this.m_CurrentFocusedGameObject = eventData.buttonData.pointerCurrentRaycast.gameObject;
			this.ProcessMousePress(eventData);
			this.ProcessMove(eventData.buttonData);
			this.ProcessDrag(eventData.buttonData);
			this.ProcessMousePress(mousePointerEventData.GetButtonState(PointerEventData.InputButton.Right).eventData);
			this.ProcessDrag(mousePointerEventData.GetButtonState(PointerEventData.InputButton.Right).eventData.buttonData);
			this.ProcessMousePress(mousePointerEventData.GetButtonState(PointerEventData.InputButton.Middle).eventData);
			this.ProcessDrag(mousePointerEventData.GetButtonState(PointerEventData.InputButton.Middle).eventData.buttonData);
			if (!Mathf.Approximately(eventData.buttonData.scrollDelta.sqrMagnitude, 0f))
			{
				ExecuteEvents.ExecuteHierarchy<IScrollHandler>(ExecuteEvents.GetEventHandler<IScrollHandler>(eventData.buttonData.pointerCurrentRaycast.gameObject), eventData.buttonData, ExecuteEvents.scrollHandler);
			}
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001AA44 File Offset: 0x00018C44
		protected bool SendUpdateEventToSelectedObject()
		{
			if (base.eventSystem.currentSelectedGameObject == null)
			{
				return false;
			}
			BaseEventData baseEventData = this.GetBaseEventData();
			ExecuteEvents.Execute<IUpdateSelectedHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.updateSelectedHandler);
			return baseEventData.used;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001AA8C File Offset: 0x00018C8C
		protected void ProcessMousePress(PointerInputModule.MouseButtonEventData data)
		{
			PointerEventData buttonData = data.buttonData;
			GameObject gameObject = buttonData.pointerCurrentRaycast.gameObject;
			if (data.PressedThisFrame())
			{
				buttonData.eligibleForClick = true;
				buttonData.delta = Vector2.zero;
				buttonData.dragging = false;
				buttonData.useDragThreshold = true;
				buttonData.pressPosition = buttonData.position;
				buttonData.pointerPressRaycast = buttonData.pointerCurrentRaycast;
				base.DeselectIfSelectionChanged(gameObject, buttonData);
				GameObject gameObject2 = ExecuteEvents.ExecuteHierarchy<IPointerDownHandler>(gameObject, buttonData, ExecuteEvents.pointerDownHandler);
				GameObject eventHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				if (gameObject2 == null)
				{
					gameObject2 = eventHandler;
				}
				float unscaledTime = Time.unscaledTime;
				if (gameObject2 == buttonData.lastPress)
				{
					if (unscaledTime - buttonData.clickTime < 0.3f)
					{
						PointerEventData pointerEventData = buttonData;
						int clickCount = pointerEventData.clickCount + 1;
						pointerEventData.clickCount = clickCount;
					}
					else
					{
						buttonData.clickCount = 1;
					}
					buttonData.clickTime = unscaledTime;
				}
				else
				{
					buttonData.clickCount = 1;
				}
				buttonData.pointerPress = gameObject2;
				buttonData.rawPointerPress = gameObject;
				buttonData.pointerClick = eventHandler;
				buttonData.clickTime = unscaledTime;
				buttonData.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(gameObject);
				if (buttonData.pointerDrag != null)
				{
					ExecuteEvents.Execute<IInitializePotentialDragHandler>(buttonData.pointerDrag, buttonData, ExecuteEvents.initializePotentialDrag);
				}
				this.m_InputPointerEvent = buttonData;
			}
			if (data.ReleasedThisFrame())
			{
				this.ReleaseMouse(buttonData, gameObject);
			}
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001ABCD File Offset: 0x00018DCD
		protected GameObject GetCurrentFocusedGameObject()
		{
			return this.m_CurrentFocusedGameObject;
		}

		// Token: 0x04000210 RID: 528
		private float m_PrevActionTime;

		// Token: 0x04000211 RID: 529
		private Vector2 m_LastMoveVector;

		// Token: 0x04000212 RID: 530
		private int m_ConsecutiveMoveCount;

		// Token: 0x04000213 RID: 531
		private Vector2 m_LastMousePosition;

		// Token: 0x04000214 RID: 532
		private Vector2 m_MousePosition;

		// Token: 0x04000215 RID: 533
		private GameObject m_CurrentFocusedGameObject;

		// Token: 0x04000216 RID: 534
		private PointerEventData m_InputPointerEvent;

		// Token: 0x04000217 RID: 535
		[SerializeField]
		private string m_HorizontalAxis = "Horizontal";

		// Token: 0x04000218 RID: 536
		[SerializeField]
		private string m_VerticalAxis = "Vertical";

		// Token: 0x04000219 RID: 537
		[SerializeField]
		private string m_SubmitButton = "Submit";

		// Token: 0x0400021A RID: 538
		[SerializeField]
		private string m_CancelButton = "Cancel";

		// Token: 0x0400021B RID: 539
		[SerializeField]
		private float m_InputActionsPerSecond = 10f;

		// Token: 0x0400021C RID: 540
		[SerializeField]
		private float m_RepeatDelay = 0.5f;

		// Token: 0x0400021D RID: 541
		[SerializeField]
		[FormerlySerializedAs("m_AllowActivationOnMobileDevice")]
		[HideInInspector]
		private bool m_ForceModuleActive;

		// Token: 0x020000C9 RID: 201
		[Obsolete("Mode is no longer needed on input module as it handles both mouse and keyboard simultaneously.", false)]
		public enum InputMode
		{
			// Token: 0x0400034D RID: 845
			Mouse,
			// Token: 0x0400034E RID: 846
			Buttons
		}
	}
}
