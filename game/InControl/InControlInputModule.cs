using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace InControl
{
	// Token: 0x0200003F RID: 63
	[AddComponentMenu("Event/InControl Input Module")]
	public class InControlInputModule : PointerInputModule
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000902A File Offset: 0x0000722A
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x00009032 File Offset: 0x00007232
		public PlayerAction SubmitAction
		{
			[CompilerGenerated]
			get
			{
				return this.<SubmitAction>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SubmitAction>k__BackingField = value;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000903B File Offset: 0x0000723B
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x00009043 File Offset: 0x00007243
		public PlayerAction CancelAction
		{
			[CompilerGenerated]
			get
			{
				return this.<CancelAction>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CancelAction>k__BackingField = value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000904C File Offset: 0x0000724C
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x00009054 File Offset: 0x00007254
		public PlayerTwoAxisAction MoveAction
		{
			[CompilerGenerated]
			get
			{
				return this.<MoveAction>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MoveAction>k__BackingField = value;
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00009060 File Offset: 0x00007260
		protected InControlInputModule()
		{
			this.direction = new TwoAxisInputControl();
			this.direction.StateThreshold = this.analogMoveThreshold;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x000090CE File Offset: 0x000072CE
		public override void UpdateModule()
		{
			this.lastMousePosition = this.thisMousePosition;
			this.thisMousePosition = InputManager.MouseProvider.GetPosition();
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x000090F1 File Offset: 0x000072F1
		public override bool IsModuleSupported()
		{
			return this.forceModuleActive || InputManager.MouseProvider.HasMousePresent() || Input.touchSupported;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00009114 File Offset: 0x00007314
		public override bool ShouldActivateModule()
		{
			if (!base.enabled || !base.gameObject.activeInHierarchy)
			{
				return false;
			}
			this.UpdateInputState();
			bool flag = false;
			flag |= this.SubmitWasPressed;
			flag |= this.CancelWasPressed;
			flag |= this.VectorWasPressed;
			if (this.allowMouseInput)
			{
				flag |= this.MouseHasMoved;
				flag |= InControlInputModule.MouseButtonWasPressed;
			}
			if (this.allowTouchInput)
			{
				flag |= (Input.touchCount > 0);
			}
			return flag;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00009188 File Offset: 0x00007388
		public override void ActivateModule()
		{
			base.ActivateModule();
			this.thisMousePosition = InputManager.MouseProvider.GetPosition();
			this.lastMousePosition = this.thisMousePosition;
			GameObject gameObject = base.eventSystem.currentSelectedGameObject;
			if (gameObject == null)
			{
				gameObject = base.eventSystem.firstSelectedGameObject;
			}
			base.eventSystem.SetSelectedGameObject(gameObject, this.GetBaseEventData());
		}

		// Token: 0x060002DC RID: 732 RVA: 0x000091F0 File Offset: 0x000073F0
		public override void Process()
		{
			bool flag = this.SendUpdateEventToSelectedObject();
			if (base.eventSystem.sendNavigationEvents)
			{
				if (!flag)
				{
					flag = this.SendVectorEventToSelectedObject();
				}
				if (!flag)
				{
					this.SendButtonEventToSelectedObject();
				}
			}
			if (this.allowTouchInput && this.ProcessTouchEvents())
			{
				return;
			}
			if (this.allowMouseInput)
			{
				this.ProcessMouseEvent();
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00009244 File Offset: 0x00007444
		private bool ProcessTouchEvents()
		{
			int touchCount = Input.touchCount;
			for (int i = 0; i < touchCount; i++)
			{
				Touch touch = Input.GetTouch(i);
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
			return touchCount > 0;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000092B0 File Offset: 0x000074B0
		private bool SendButtonEventToSelectedObject()
		{
			if (base.eventSystem.currentSelectedGameObject == null)
			{
				return false;
			}
			BaseEventData baseEventData = this.GetBaseEventData();
			if (this.SubmitWasPressed)
			{
				ExecuteEvents.Execute<ISubmitHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.submitHandler);
			}
			else
			{
				bool submitWasReleased = this.SubmitWasReleased;
			}
			if (this.CancelWasPressed)
			{
				ExecuteEvents.Execute<ICancelHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.cancelHandler);
			}
			return baseEventData.used;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00009328 File Offset: 0x00007528
		private bool SendVectorEventToSelectedObject()
		{
			if (!this.VectorWasPressed)
			{
				return false;
			}
			AxisEventData axisEventData = this.GetAxisEventData(this.thisVectorState.x, this.thisVectorState.y, 0.5f);
			if (axisEventData.moveDir != MoveDirection.None)
			{
				if (base.eventSystem.currentSelectedGameObject == null)
				{
					base.eventSystem.SetSelectedGameObject(base.eventSystem.firstSelectedGameObject, this.GetBaseEventData());
				}
				else
				{
					ExecuteEvents.Execute<IMoveHandler>(base.eventSystem.currentSelectedGameObject, axisEventData, ExecuteEvents.moveHandler);
				}
				this.SetVectorRepeatTimer();
			}
			return axisEventData.used;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x000093C0 File Offset: 0x000075C0
		protected override void ProcessMove(PointerEventData pointerEvent)
		{
			GameObject pointerEnter = pointerEvent.pointerEnter;
			base.ProcessMove(pointerEvent);
			if (this.focusOnMouseHover && pointerEnter != pointerEvent.pointerEnter)
			{
				GameObject eventHandler = ExecuteEvents.GetEventHandler<ISelectHandler>(pointerEvent.pointerEnter);
				base.eventSystem.SetSelectedGameObject(eventHandler, pointerEvent);
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000940A File Offset: 0x0000760A
		private void Update()
		{
			this.direction.Filter(this.Device.Direction, Time.deltaTime);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00009428 File Offset: 0x00007628
		private void UpdateInputState()
		{
			this.lastVectorState = this.thisVectorState;
			this.thisVectorState = Vector2.zero;
			TwoAxisInputControl twoAxisInputControl = this.MoveAction ?? this.direction;
			if (Utility.AbsoluteIsOverThreshold(twoAxisInputControl.X, this.analogMoveThreshold))
			{
				this.thisVectorState.x = Mathf.Sign(twoAxisInputControl.X);
			}
			if (Utility.AbsoluteIsOverThreshold(twoAxisInputControl.Y, this.analogMoveThreshold))
			{
				this.thisVectorState.y = Mathf.Sign(twoAxisInputControl.Y);
			}
			this.moveWasRepeated = false;
			if (this.VectorIsReleased)
			{
				this.nextMoveRepeatTime = 0f;
			}
			else if (this.VectorIsPressed)
			{
				float realtimeSinceStartup = Time.realtimeSinceStartup;
				if (this.lastVectorState == Vector2.zero)
				{
					this.nextMoveRepeatTime = realtimeSinceStartup + this.moveRepeatFirstDuration;
				}
				else if (realtimeSinceStartup >= this.nextMoveRepeatTime)
				{
					this.moveWasRepeated = true;
					this.nextMoveRepeatTime = realtimeSinceStartup + this.moveRepeatDelayDuration;
				}
			}
			this.lastSubmitState = this.thisSubmitState;
			this.thisSubmitState = ((this.SubmitAction == null) ? this.SubmitButton.IsPressed : this.SubmitAction.IsPressed);
			this.lastCancelState = this.thisCancelState;
			this.thisCancelState = ((this.CancelAction == null) ? this.CancelButton.IsPressed : this.CancelAction.IsPressed);
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00009586 File Offset: 0x00007786
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x0000957D File Offset: 0x0000777D
		public InputDevice Device
		{
			get
			{
				return this.inputDevice ?? InputManager.ActiveDevice;
			}
			set
			{
				this.inputDevice = value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00009597 File Offset: 0x00007797
		private InputControl SubmitButton
		{
			get
			{
				return this.Device.GetControl((InputControlType)this.submitButton);
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x000095AA File Offset: 0x000077AA
		private InputControl CancelButton
		{
			get
			{
				return this.Device.GetControl((InputControlType)this.cancelButton);
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x000095BD File Offset: 0x000077BD
		private void SetVectorRepeatTimer()
		{
			this.nextMoveRepeatTime = Mathf.Max(this.nextMoveRepeatTime, Time.realtimeSinceStartup + this.moveRepeatDelayDuration);
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x000095DC File Offset: 0x000077DC
		private bool VectorIsPressed
		{
			get
			{
				return this.thisVectorState != Vector2.zero;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x000095EE File Offset: 0x000077EE
		private bool VectorIsReleased
		{
			get
			{
				return this.thisVectorState == Vector2.zero;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00009600 File Offset: 0x00007800
		private bool VectorHasChanged
		{
			get
			{
				return this.thisVectorState != this.lastVectorState;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00009613 File Offset: 0x00007813
		private bool VectorWasPressed
		{
			get
			{
				return this.moveWasRepeated || (this.VectorIsPressed && this.lastVectorState == Vector2.zero);
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00009639 File Offset: 0x00007839
		private bool SubmitWasPressed
		{
			get
			{
				return this.thisSubmitState && this.thisSubmitState != this.lastSubmitState;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00009656 File Offset: 0x00007856
		private bool SubmitWasReleased
		{
			get
			{
				return !this.thisSubmitState && this.thisSubmitState != this.lastSubmitState;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00009673 File Offset: 0x00007873
		private bool CancelWasPressed
		{
			get
			{
				return this.thisCancelState && this.thisCancelState != this.lastCancelState;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00009690 File Offset: 0x00007890
		private bool MouseHasMoved
		{
			get
			{
				return (this.thisMousePosition - this.lastMousePosition).sqrMagnitude > 0f;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x000096BD File Offset: 0x000078BD
		private static bool MouseButtonWasPressed
		{
			get
			{
				return InputManager.MouseProvider.GetButtonWasPressed(Mouse.LeftButton);
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x000096CC File Offset: 0x000078CC
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

		// Token: 0x060002F2 RID: 754 RVA: 0x00009712 File Offset: 0x00007912
		protected void ProcessMouseEvent()
		{
			this.ProcessMouseEvent(0);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000971C File Offset: 0x0000791C
		protected void ProcessMouseEvent(int id)
		{
			PointerInputModule.MouseState mousePointerEventData = this.GetMousePointerEventData(id);
			PointerInputModule.MouseButtonEventData eventData = mousePointerEventData.GetButtonState(PointerEventData.InputButton.Left).eventData;
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

		// Token: 0x060002F4 RID: 756 RVA: 0x000097F8 File Offset: 0x000079F8
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
				if (gameObject2 == null)
				{
					gameObject2 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
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
				buttonData.clickTime = unscaledTime;
				buttonData.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(gameObject);
				if (buttonData.pointerDrag != null)
				{
					ExecuteEvents.Execute<IInitializePotentialDragHandler>(buttonData.pointerDrag, buttonData, ExecuteEvents.initializePotentialDrag);
				}
			}
			if (data.ReleasedThisFrame())
			{
				ExecuteEvents.Execute<IPointerUpHandler>(buttonData.pointerPress, buttonData, ExecuteEvents.pointerUpHandler);
				GameObject eventHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				if (buttonData.pointerPress == eventHandler && buttonData.eligibleForClick)
				{
					ExecuteEvents.Execute<IPointerClickHandler>(buttonData.pointerPress, buttonData, ExecuteEvents.pointerClickHandler);
				}
				else if (buttonData.pointerDrag != null && buttonData.dragging)
				{
					ExecuteEvents.ExecuteHierarchy<IDropHandler>(gameObject, buttonData, ExecuteEvents.dropHandler);
				}
				buttonData.eligibleForClick = false;
				buttonData.pointerPress = null;
				buttonData.rawPointerPress = null;
				if (buttonData.pointerDrag != null && buttonData.dragging)
				{
					ExecuteEvents.Execute<IEndDragHandler>(buttonData.pointerDrag, buttonData, ExecuteEvents.endDragHandler);
				}
				buttonData.dragging = false;
				buttonData.pointerDrag = null;
				if (gameObject != buttonData.pointerEnter)
				{
					base.HandlePointerExitAndEnter(buttonData, null);
					base.HandlePointerExitAndEnter(buttonData, gameObject);
				}
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x000099F4 File Offset: 0x00007BF4
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
				if (gameObject2 == null)
				{
					gameObject2 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
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
				GameObject eventHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				if (pointerEvent.pointerPress == eventHandler && pointerEvent.eligibleForClick)
				{
					ExecuteEvents.Execute<IPointerClickHandler>(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerClickHandler);
				}
				else if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
				{
					ExecuteEvents.ExecuteHierarchy<IDropHandler>(gameObject, pointerEvent, ExecuteEvents.dropHandler);
				}
				pointerEvent.eligibleForClick = false;
				pointerEvent.pointerPress = null;
				pointerEvent.rawPointerPress = null;
				if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
				{
					ExecuteEvents.Execute<IEndDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.endDragHandler);
				}
				pointerEvent.dragging = false;
				pointerEvent.pointerDrag = null;
				if (pointerEvent.pointerDrag != null)
				{
					ExecuteEvents.Execute<IEndDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.endDragHandler);
				}
				pointerEvent.pointerDrag = null;
				ExecuteEvents.ExecuteHierarchy<IPointerExitHandler>(pointerEvent.pointerEnter, pointerEvent, ExecuteEvents.pointerExitHandler);
				pointerEvent.pointerEnter = null;
			}
		}

		// Token: 0x040002D4 RID: 724
		public InControlInputModule.Button submitButton = InControlInputModule.Button.Action1;

		// Token: 0x040002D5 RID: 725
		public InControlInputModule.Button cancelButton = InControlInputModule.Button.Action2;

		// Token: 0x040002D6 RID: 726
		[Range(0.1f, 0.9f)]
		public float analogMoveThreshold = 0.5f;

		// Token: 0x040002D7 RID: 727
		public float moveRepeatFirstDuration = 0.8f;

		// Token: 0x040002D8 RID: 728
		public float moveRepeatDelayDuration = 0.1f;

		// Token: 0x040002D9 RID: 729
		[FormerlySerializedAs("allowMobileDevice")]
		public bool forceModuleActive;

		// Token: 0x040002DA RID: 730
		public bool allowMouseInput = true;

		// Token: 0x040002DB RID: 731
		public bool focusOnMouseHover;

		// Token: 0x040002DC RID: 732
		public bool allowTouchInput = true;

		// Token: 0x040002DD RID: 733
		private InputDevice inputDevice;

		// Token: 0x040002DE RID: 734
		private Vector3 thisMousePosition;

		// Token: 0x040002DF RID: 735
		private Vector3 lastMousePosition;

		// Token: 0x040002E0 RID: 736
		private Vector2 thisVectorState;

		// Token: 0x040002E1 RID: 737
		private Vector2 lastVectorState;

		// Token: 0x040002E2 RID: 738
		private bool thisSubmitState;

		// Token: 0x040002E3 RID: 739
		private bool lastSubmitState;

		// Token: 0x040002E4 RID: 740
		private bool thisCancelState;

		// Token: 0x040002E5 RID: 741
		private bool lastCancelState;

		// Token: 0x040002E6 RID: 742
		private bool moveWasRepeated;

		// Token: 0x040002E7 RID: 743
		private float nextMoveRepeatTime;

		// Token: 0x040002E8 RID: 744
		private TwoAxisInputControl direction;

		// Token: 0x040002E9 RID: 745
		[CompilerGenerated]
		private PlayerAction <SubmitAction>k__BackingField;

		// Token: 0x040002EA RID: 746
		[CompilerGenerated]
		private PlayerAction <CancelAction>k__BackingField;

		// Token: 0x040002EB RID: 747
		[CompilerGenerated]
		private PlayerTwoAxisAction <MoveAction>k__BackingField;

		// Token: 0x02000210 RID: 528
		public enum Button
		{
			// Token: 0x04000451 RID: 1105
			Action1 = 19,
			// Token: 0x04000452 RID: 1106
			Action2,
			// Token: 0x04000453 RID: 1107
			Action3,
			// Token: 0x04000454 RID: 1108
			Action4
		}
	}
}
