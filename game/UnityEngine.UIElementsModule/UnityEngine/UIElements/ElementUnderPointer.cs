using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001CF RID: 463
	internal class ElementUnderPointer
	{
		// Token: 0x06000EA8 RID: 3752 RVA: 0x0003D46C File Offset: 0x0003B66C
		internal VisualElement GetTopElementUnderPointer(int pointerId, out Vector2 pickPosition, out bool isTemporary)
		{
			pickPosition = this.m_PickingPointerPositions[pointerId];
			isTemporary = this.m_IsPickingPointerTemporaries[pointerId];
			return this.m_PendingTopElementUnderPointer[pointerId];
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0003D4A4 File Offset: 0x0003B6A4
		internal VisualElement GetTopElementUnderPointer(int pointerId)
		{
			return this.m_PendingTopElementUnderPointer[pointerId];
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0003D4C0 File Offset: 0x0003B6C0
		internal void SetElementUnderPointer(VisualElement newElementUnderPointer, int pointerId, Vector2 pointerPos)
		{
			Debug.Assert(pointerId >= 0);
			VisualElement visualElement = this.m_TopElementUnderPointer[pointerId];
			this.m_IsPickingPointerTemporaries[pointerId] = false;
			this.m_PickingPointerPositions[pointerId] = pointerPos;
			bool flag = newElementUnderPointer == visualElement;
			if (!flag)
			{
				this.m_PendingTopElementUnderPointer[pointerId] = newElementUnderPointer;
				this.m_TriggerPointerEvent[pointerId] = null;
				this.m_TriggerMouseEvent[pointerId] = null;
			}
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x0003D520 File Offset: 0x0003B720
		private Vector2 GetEventPointerPosition(EventBase triggerEvent)
		{
			IPointerEvent pointerEvent = triggerEvent as IPointerEvent;
			bool flag = pointerEvent != null;
			Vector2 result;
			if (flag)
			{
				result = new Vector2(pointerEvent.position.x, pointerEvent.position.y);
			}
			else
			{
				IMouseEvent mouseEvent = triggerEvent as IMouseEvent;
				bool flag2 = mouseEvent != null;
				if (flag2)
				{
					result = mouseEvent.mousePosition;
				}
				else
				{
					result = new Vector2(float.MinValue, float.MinValue);
				}
			}
			return result;
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0003D58A File Offset: 0x0003B78A
		internal void SetTemporaryElementUnderPointer(VisualElement newElementUnderPointer, int pointerId, EventBase triggerEvent)
		{
			this.SetElementUnderPointer(newElementUnderPointer, pointerId, triggerEvent, true);
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0003D598 File Offset: 0x0003B798
		internal void SetElementUnderPointer(VisualElement newElementUnderPointer, int pointerId, EventBase triggerEvent)
		{
			this.SetElementUnderPointer(newElementUnderPointer, pointerId, triggerEvent, false);
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0003D5A8 File Offset: 0x0003B7A8
		private void SetElementUnderPointer(VisualElement newElementUnderPointer, int pointerId, EventBase triggerEvent, bool temporary)
		{
			Debug.Assert(pointerId >= 0);
			this.m_IsPickingPointerTemporaries[pointerId] = temporary;
			this.m_PickingPointerPositions[pointerId] = this.GetEventPointerPosition(triggerEvent);
			VisualElement visualElement = this.m_TopElementUnderPointer[pointerId];
			bool flag = newElementUnderPointer == visualElement;
			if (!flag)
			{
				this.m_PendingTopElementUnderPointer[pointerId] = newElementUnderPointer;
				bool flag2 = this.m_TriggerPointerEvent[pointerId] == null && triggerEvent is IPointerEvent;
				if (flag2)
				{
					this.m_TriggerPointerEvent[pointerId] = (triggerEvent as IPointerEvent);
				}
				bool flag3 = this.m_TriggerMouseEvent[pointerId] == null && triggerEvent is IMouseEvent;
				if (flag3)
				{
					this.m_TriggerMouseEvent[pointerId] = (triggerEvent as IMouseEvent);
				}
			}
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0003D654 File Offset: 0x0003B854
		internal void CommitElementUnderPointers(EventDispatcher dispatcher, ContextType contextType)
		{
			for (int i = 0; i < this.m_TopElementUnderPointer.Length; i++)
			{
				IPointerEvent pointerEvent = this.m_TriggerPointerEvent[i];
				VisualElement visualElement = this.m_TopElementUnderPointer[i];
				VisualElement visualElement2 = this.m_PendingTopElementUnderPointer[i];
				bool flag = visualElement2 == visualElement;
				if (flag)
				{
					bool flag2 = pointerEvent != null;
					if (flag2)
					{
						Vector3 position = pointerEvent.position;
						this.m_PickingPointerPositions[i] = new Vector2(position.x, position.y);
					}
					else
					{
						bool flag3 = this.m_TriggerMouseEvent[i] != null;
						if (flag3)
						{
							this.m_PickingPointerPositions[i] = this.m_TriggerMouseEvent[i].mousePosition;
						}
					}
				}
				else
				{
					this.m_TopElementUnderPointer[i] = visualElement2;
					bool flag4 = pointerEvent == null && this.m_TriggerMouseEvent[i] == null;
					if (flag4)
					{
						using (new EventDispatcherGate(dispatcher))
						{
							Vector2 pointerPosition = PointerDeviceState.GetPointerPosition(i, contextType);
							PointerEventsHelper.SendOverOut(visualElement, visualElement2, null, pointerPosition, i);
							PointerEventsHelper.SendEnterLeave<PointerLeaveEvent, PointerEnterEvent>(visualElement, visualElement2, null, pointerPosition, i);
							this.m_PickingPointerPositions[i] = pointerPosition;
							bool flag5 = i == PointerId.mousePointerId;
							if (flag5)
							{
								MouseEventsHelper.SendMouseOverMouseOut(visualElement, visualElement2, null, pointerPosition);
								MouseEventsHelper.SendEnterLeave<MouseLeaveEvent, MouseEnterEvent>(visualElement, visualElement2, null, pointerPosition);
							}
						}
					}
					bool flag6 = pointerEvent != null;
					if (flag6)
					{
						Vector3 position2 = pointerEvent.position;
						this.m_PickingPointerPositions[i] = new Vector2(position2.x, position2.y);
						EventBase eventBase = pointerEvent as EventBase;
						bool flag7 = eventBase != null && (eventBase.eventTypeId == EventBase<PointerMoveEvent>.TypeId() || eventBase.eventTypeId == EventBase<PointerDownEvent>.TypeId() || eventBase.eventTypeId == EventBase<PointerUpEvent>.TypeId() || eventBase.eventTypeId == EventBase<PointerCancelEvent>.TypeId());
						if (flag7)
						{
							using (new EventDispatcherGate(dispatcher))
							{
								PointerEventsHelper.SendOverOut(visualElement, visualElement2, pointerEvent, position2, i);
								PointerEventsHelper.SendEnterLeave<PointerLeaveEvent, PointerEnterEvent>(visualElement, visualElement2, pointerEvent, position2, i);
							}
						}
					}
					this.m_TriggerPointerEvent[i] = null;
					IMouseEvent mouseEvent = this.m_TriggerMouseEvent[i];
					bool flag8 = mouseEvent != null;
					if (flag8)
					{
						Vector2 mousePosition = mouseEvent.mousePosition;
						this.m_PickingPointerPositions[i] = mousePosition;
						EventBase eventBase2 = mouseEvent as EventBase;
						bool flag9 = eventBase2 != null;
						if (flag9)
						{
							bool flag10 = eventBase2.eventTypeId == EventBase<MouseMoveEvent>.TypeId() || eventBase2.eventTypeId == EventBase<MouseDownEvent>.TypeId() || eventBase2.eventTypeId == EventBase<MouseUpEvent>.TypeId() || eventBase2.eventTypeId == EventBase<WheelEvent>.TypeId();
							if (flag10)
							{
								using (new EventDispatcherGate(dispatcher))
								{
									MouseEventsHelper.SendMouseOverMouseOut(visualElement, visualElement2, mouseEvent, mousePosition);
									MouseEventsHelper.SendEnterLeave<MouseLeaveEvent, MouseEnterEvent>(visualElement, visualElement2, mouseEvent, mousePosition);
								}
							}
							else
							{
								bool flag11 = eventBase2.eventTypeId == EventBase<MouseEnterWindowEvent>.TypeId() || eventBase2.eventTypeId == EventBase<MouseLeaveWindowEvent>.TypeId();
								if (flag11)
								{
									using (new EventDispatcherGate(dispatcher))
									{
										PointerEventsHelper.SendOverOut(visualElement, visualElement2, null, mousePosition, i);
										PointerEventsHelper.SendEnterLeave<PointerLeaveEvent, PointerEnterEvent>(visualElement, visualElement2, null, mousePosition, i);
										bool flag12 = i == PointerId.mousePointerId;
										if (flag12)
										{
											MouseEventsHelper.SendMouseOverMouseOut(visualElement, visualElement2, mouseEvent, mousePosition);
											MouseEventsHelper.SendEnterLeave<MouseLeaveEvent, MouseEnterEvent>(visualElement, visualElement2, mouseEvent, mousePosition);
										}
									}
								}
							}
						}
						this.m_TriggerMouseEvent[i] = null;
					}
				}
			}
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0003D9F4 File Offset: 0x0003BBF4
		public ElementUnderPointer()
		{
		}

		// Token: 0x040006C4 RID: 1732
		private VisualElement[] m_PendingTopElementUnderPointer = new VisualElement[PointerId.maxPointers];

		// Token: 0x040006C5 RID: 1733
		private VisualElement[] m_TopElementUnderPointer = new VisualElement[PointerId.maxPointers];

		// Token: 0x040006C6 RID: 1734
		private IPointerEvent[] m_TriggerPointerEvent = new IPointerEvent[PointerId.maxPointers];

		// Token: 0x040006C7 RID: 1735
		private IMouseEvent[] m_TriggerMouseEvent = new IMouseEvent[PointerId.maxPointers];

		// Token: 0x040006C8 RID: 1736
		private Vector2[] m_PickingPointerPositions = new Vector2[PointerId.maxPointers];

		// Token: 0x040006C9 RID: 1737
		private bool[] m_IsPickingPointerTemporaries = new bool[PointerId.maxPointers];
	}
}
