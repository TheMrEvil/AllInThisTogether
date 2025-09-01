using System;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	// Token: 0x020001A8 RID: 424
	internal abstract class DragEventsProcessor
	{
		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x0003A7A9 File Offset: 0x000389A9
		internal bool isRegistered
		{
			get
			{
				return this.m_IsRegistered;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000DFF RID: 3583 RVA: 0x0003A7B1 File Offset: 0x000389B1
		internal DragEventsProcessor.DragState dragState
		{
			get
			{
				return this.m_DragState;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x0000AD4B File Offset: 0x00008F4B
		protected virtual bool supportsDragEvents
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x0003A7B9 File Offset: 0x000389B9
		internal bool useDragEvents
		{
			get
			{
				return this.isEditorContext && this.supportsDragEvents;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x0003A7CC File Offset: 0x000389CC
		protected IDragAndDrop dragAndDrop
		{
			get
			{
				return DragAndDropUtility.GetDragAndDrop(this.m_Target.panel);
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x0003A7E0 File Offset: 0x000389E0
		internal virtual bool isEditorContext
		{
			get
			{
				Assert.IsNotNull<VisualElement>(this.m_Target);
				Assert.IsNotNull<VisualElement>(this.m_Target.parent);
				return this.m_Target.panel.contextType == ContextType.Editor;
			}
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0003A824 File Offset: 0x00038A24
		internal DragEventsProcessor(VisualElement target)
		{
			this.m_Target = target;
			this.m_Target.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.RegisterCallbacksFromTarget), TrickleDown.NoTrickleDown);
			this.m_Target.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.UnregisterCallbacksFromTarget), TrickleDown.NoTrickleDown);
			this.RegisterCallbacksFromTarget();
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0003A879 File Offset: 0x00038A79
		private void RegisterCallbacksFromTarget(AttachToPanelEvent evt)
		{
			this.RegisterCallbacksFromTarget();
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0003A884 File Offset: 0x00038A84
		private void RegisterCallbacksFromTarget()
		{
			bool isRegistered = this.m_IsRegistered;
			if (!isRegistered)
			{
				this.m_IsRegistered = true;
				this.m_Target.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDownEvent), TrickleDown.TrickleDown);
				this.m_Target.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUpEvent), TrickleDown.TrickleDown);
				this.m_Target.RegisterCallback<PointerLeaveEvent>(new EventCallback<PointerLeaveEvent>(this.OnPointerLeaveEvent), TrickleDown.NoTrickleDown);
				this.m_Target.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMoveEvent), TrickleDown.NoTrickleDown);
				this.m_Target.RegisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancelEvent), TrickleDown.NoTrickleDown);
				this.m_Target.RegisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCapturedOut), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0003A93E File Offset: 0x00038B3E
		private void UnregisterCallbacksFromTarget(DetachFromPanelEvent evt)
		{
			this.UnregisterCallbacksFromTarget(false);
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0003A94C File Offset: 0x00038B4C
		internal void UnregisterCallbacksFromTarget(bool unregisterPanelEvents = false)
		{
			this.m_IsRegistered = false;
			this.m_Target.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDownEvent), TrickleDown.TrickleDown);
			this.m_Target.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUpEvent), TrickleDown.TrickleDown);
			this.m_Target.UnregisterCallback<PointerLeaveEvent>(new EventCallback<PointerLeaveEvent>(this.OnPointerLeaveEvent), TrickleDown.NoTrickleDown);
			this.m_Target.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMoveEvent), TrickleDown.NoTrickleDown);
			this.m_Target.UnregisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancelEvent), TrickleDown.NoTrickleDown);
			this.m_Target.UnregisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCapturedOut), TrickleDown.NoTrickleDown);
			if (unregisterPanelEvents)
			{
				this.m_Target.UnregisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.RegisterCallbacksFromTarget), TrickleDown.NoTrickleDown);
				this.m_Target.UnregisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.UnregisterCallbacksFromTarget), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x06000E09 RID: 3593
		protected abstract bool CanStartDrag(Vector3 pointerPosition);

		// Token: 0x06000E0A RID: 3594
		protected internal abstract StartDragArgs StartDrag(Vector3 pointerPosition);

		// Token: 0x06000E0B RID: 3595
		protected internal abstract void UpdateDrag(Vector3 pointerPosition);

		// Token: 0x06000E0C RID: 3596
		protected internal abstract void OnDrop(Vector3 pointerPosition);

		// Token: 0x06000E0D RID: 3597
		protected abstract void ClearDragAndDropUI(bool dragCancelled);

		// Token: 0x06000E0E RID: 3598 RVA: 0x0003AA30 File Offset: 0x00038C30
		private void OnPointerDownEvent(PointerDownEvent evt)
		{
			bool flag;
			if (evt.button == 0)
			{
				VisualElement visualElement = evt.leafTarget as VisualElement;
				flag = (visualElement != null && visualElement.isIMGUIContainer);
			}
			else
			{
				flag = true;
			}
			bool flag2 = flag;
			if (flag2)
			{
				this.m_DragState = DragEventsProcessor.DragState.None;
			}
			else
			{
				bool flag3 = this.CanStartDrag(evt.position);
				if (flag3)
				{
					this.m_DragState = DragEventsProcessor.DragState.CanStartDrag;
					this.m_Start = evt.position;
				}
			}
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0003AA94 File Offset: 0x00038C94
		internal void OnPointerUpEvent(PointerUpEvent evt)
		{
			bool flag = !this.useDragEvents && this.m_DragState == DragEventsProcessor.DragState.Dragging;
			if (flag)
			{
				DragEventsProcessor dragEventsProcessor = this.GetDropTarget(evt.position) ?? this;
				dragEventsProcessor.UpdateDrag(evt.position);
				dragEventsProcessor.OnDrop(evt.position);
				dragEventsProcessor.ClearDragAndDropUI(false);
				evt.StopPropagation();
			}
			this.m_Target.ReleasePointer(evt.pointerId);
			this.ClearDragAndDropUI(this.m_DragState == DragEventsProcessor.DragState.Dragging);
			this.dragAndDrop.DragCleanup();
			this.m_DragState = DragEventsProcessor.DragState.None;
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0003AB31 File Offset: 0x00038D31
		private void OnPointerLeaveEvent(PointerLeaveEvent evt)
		{
			this.ClearDragAndDropUI(false);
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x0003AB3C File Offset: 0x00038D3C
		private void OnPointerCancelEvent(PointerCancelEvent evt)
		{
			bool flag = !this.useDragEvents;
			if (flag)
			{
				this.ClearDragAndDropUI(true);
			}
			this.m_Target.ReleasePointer(evt.pointerId);
			this.ClearDragAndDropUI(this.m_DragState == DragEventsProcessor.DragState.Dragging);
			this.dragAndDrop.DragCleanup();
			this.m_DragState = DragEventsProcessor.DragState.None;
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x0003AB94 File Offset: 0x00038D94
		private void OnPointerCapturedOut(PointerCaptureOutEvent evt)
		{
			bool flag = !this.useDragEvents;
			if (flag)
			{
				this.ClearDragAndDropUI(true);
			}
			this.ClearDragAndDropUI(this.m_DragState == DragEventsProcessor.DragState.Dragging);
			this.dragAndDrop.DragCleanup();
			this.m_DragState = DragEventsProcessor.DragState.None;
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x0003ABDC File Offset: 0x00038DDC
		private void OnPointerMoveEvent(PointerMoveEvent evt)
		{
			bool isHandledByDraggable = evt.isHandledByDraggable;
			if (!isHandledByDraggable)
			{
				bool flag = !this.useDragEvents && this.m_DragState == DragEventsProcessor.DragState.Dragging;
				if (flag)
				{
					DragEventsProcessor dragEventsProcessor = this.GetDropTarget(evt.position) ?? this;
					dragEventsProcessor.UpdateDrag(evt.position);
				}
				else
				{
					bool flag2 = this.m_DragState != DragEventsProcessor.DragState.CanStartDrag;
					if (!flag2)
					{
						bool flag3 = (this.m_Start - evt.position).sqrMagnitude >= 100f;
						if (flag3)
						{
							StartDragArgs args = this.StartDrag(this.m_Start);
							bool flag4 = !this.useDragEvents;
							if (flag4)
							{
								bool supportsDragEvents = this.supportsDragEvents;
								if (supportsDragEvents)
								{
									this.dragAndDrop.StartDrag(args, evt.position);
								}
							}
							else
							{
								bool flag5 = Event.current != null && Event.current.type != EventType.MouseDown && Event.current.type != EventType.MouseDrag;
								if (flag5)
								{
									return;
								}
								this.dragAndDrop.StartDrag(args, evt.position);
							}
							this.m_DragState = DragEventsProcessor.DragState.Dragging;
							this.m_Target.CapturePointer(evt.pointerId);
							evt.isHandledByDraggable = true;
							evt.StopPropagation();
						}
					}
				}
			}
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0003AD30 File Offset: 0x00038F30
		private DragEventsProcessor GetDropTarget(Vector2 position)
		{
			DragEventsProcessor result = null;
			bool flag = this.m_Target.worldBound.Contains(position);
			if (flag)
			{
				result = this;
			}
			else
			{
				bool supportsDragEvents = this.supportsDragEvents;
				if (supportsDragEvents)
				{
					VisualElement visualElement = this.m_Target.elementPanel.Pick(position);
					BaseVerticalCollectionView baseVerticalCollectionView = (visualElement != null) ? visualElement.GetFirstOfType<BaseVerticalCollectionView>() : null;
					result = ((baseVerticalCollectionView != null) ? baseVerticalCollectionView.dragger : null);
				}
			}
			return result;
		}

		// Token: 0x04000685 RID: 1669
		private bool m_IsRegistered;

		// Token: 0x04000686 RID: 1670
		internal DragEventsProcessor.DragState m_DragState;

		// Token: 0x04000687 RID: 1671
		private Vector3 m_Start;

		// Token: 0x04000688 RID: 1672
		internal readonly VisualElement m_Target;

		// Token: 0x020001A9 RID: 425
		internal enum DragState
		{
			// Token: 0x0400068A RID: 1674
			None,
			// Token: 0x0400068B RID: 1675
			CanStartDrag,
			// Token: 0x0400068C RID: 1676
			Dragging
		}
	}
}
