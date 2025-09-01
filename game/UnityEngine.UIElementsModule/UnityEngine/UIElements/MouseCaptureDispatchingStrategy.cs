using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F2 RID: 498
	internal class MouseCaptureDispatchingStrategy : IEventDispatchingStrategy
	{
		// Token: 0x06000FA0 RID: 4000 RVA: 0x0003FE14 File Offset: 0x0003E014
		public bool CanDispatchEvent(EventBase evt)
		{
			return evt is IMouseEvent || evt.imguiEvent != null;
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0003FE3C File Offset: 0x0003E03C
		public void DispatchEvent(EventBase evt, IPanel panel)
		{
			MouseCaptureDispatchingStrategy.EventBehavior eventBehavior = MouseCaptureDispatchingStrategy.EventBehavior.None;
			IEventHandler eventHandler = (panel != null) ? panel.GetCapturingElement(PointerId.mousePointerId) : null;
			bool flag = eventHandler == null;
			if (!flag)
			{
				VisualElement visualElement = eventHandler as VisualElement;
				bool flag2 = evt.eventTypeId != EventBase<MouseCaptureOutEvent>.TypeId() && visualElement != null && visualElement.panel == null;
				if (flag2)
				{
					visualElement.ReleaseMouse();
				}
				else
				{
					bool flag3 = panel != null && visualElement != null && visualElement.panel != panel;
					if (!flag3)
					{
						IMouseEvent mouseEvent = evt as IMouseEvent;
						bool flag4 = mouseEvent != null && (evt.target == null || evt.target == eventHandler);
						if (flag4)
						{
							eventBehavior = MouseCaptureDispatchingStrategy.EventBehavior.IsCapturable;
							eventBehavior |= MouseCaptureDispatchingStrategy.EventBehavior.IsSentExclusivelyToCapturingElement;
						}
						else
						{
							bool flag5 = evt.imguiEvent != null && evt.target == null;
							if (flag5)
							{
								eventBehavior = MouseCaptureDispatchingStrategy.EventBehavior.IsCapturable;
							}
						}
						bool flag6 = evt.eventTypeId == EventBase<MouseEnterWindowEvent>.TypeId() || evt.eventTypeId == EventBase<MouseLeaveWindowEvent>.TypeId() || evt.eventTypeId == EventBase<WheelEvent>.TypeId();
						if (flag6)
						{
							eventBehavior = MouseCaptureDispatchingStrategy.EventBehavior.None;
						}
						bool flag7 = (eventBehavior & MouseCaptureDispatchingStrategy.EventBehavior.IsCapturable) == MouseCaptureDispatchingStrategy.EventBehavior.IsCapturable;
						if (flag7)
						{
							BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
							bool flag8 = mouseEvent != null && baseVisualElementPanel != null;
							if (flag8)
							{
								IMouseEventInternal mouseEventInternal = mouseEvent as IMouseEventInternal;
								bool flag9 = mouseEventInternal == null || mouseEventInternal.recomputeTopElementUnderMouse;
								bool flag10 = flag9;
								if (flag10)
								{
									baseVisualElementPanel.RecomputeTopElementUnderPointer(PointerId.mousePointerId, mouseEvent.mousePosition, evt);
								}
							}
							evt.dispatch = true;
							evt.target = eventHandler;
							bool skipDisabledElements = evt.skipDisabledElements;
							evt.skipDisabledElements = false;
							CallbackEventHandler callbackEventHandler = eventHandler as CallbackEventHandler;
							if (callbackEventHandler != null)
							{
								callbackEventHandler.HandleEventAtTargetPhase(evt);
							}
							bool flag11 = (eventBehavior & MouseCaptureDispatchingStrategy.EventBehavior.IsSentExclusivelyToCapturingElement) != MouseCaptureDispatchingStrategy.EventBehavior.IsSentExclusivelyToCapturingElement;
							if (flag11)
							{
								evt.target = null;
								evt.skipDisabledElements = skipDisabledElements;
							}
							evt.currentTarget = null;
							evt.propagationPhase = PropagationPhase.None;
							evt.dispatch = false;
							evt.skipElements.Add(eventHandler);
							evt.stopDispatch = ((eventBehavior & MouseCaptureDispatchingStrategy.EventBehavior.IsSentExclusivelyToCapturingElement) == MouseCaptureDispatchingStrategy.EventBehavior.IsSentExclusivelyToCapturingElement);
							bool flag12 = evt.target is IMGUIContainer;
							if (flag12)
							{
								evt.propagateToIMGUI = true;
								evt.skipElements.Add(evt.target);
							}
							else
							{
								evt.propagateToIMGUI = false;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x000020C2 File Offset: 0x000002C2
		public MouseCaptureDispatchingStrategy()
		{
		}

		// Token: 0x020001F3 RID: 499
		[Flags]
		private enum EventBehavior
		{
			// Token: 0x0400071D RID: 1821
			None = 0,
			// Token: 0x0400071E RID: 1822
			IsCapturable = 1,
			// Token: 0x0400071F RID: 1823
			IsSentExclusivelyToCapturingElement = 2
		}
	}
}
