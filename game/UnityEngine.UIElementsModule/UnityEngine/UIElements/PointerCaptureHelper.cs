using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000063 RID: 99
	public static class PointerCaptureHelper
	{
		// Token: 0x060002CA RID: 714 RVA: 0x0000A504 File Offset: 0x00008704
		private static PointerDispatchState GetStateFor(IEventHandler handler)
		{
			VisualElement visualElement = handler as VisualElement;
			PointerDispatchState result;
			if (visualElement == null)
			{
				result = null;
			}
			else
			{
				IPanel panel = visualElement.panel;
				if (panel == null)
				{
					result = null;
				}
				else
				{
					EventDispatcher dispatcher = panel.dispatcher;
					result = ((dispatcher != null) ? dispatcher.pointerState : null);
				}
			}
			return result;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000A544 File Offset: 0x00008744
		public static bool HasPointerCapture(this IEventHandler handler, int pointerId)
		{
			PointerDispatchState stateFor = PointerCaptureHelper.GetStateFor(handler);
			return stateFor != null && stateFor.HasPointerCapture(handler, pointerId);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000A56A File Offset: 0x0000876A
		public static void CapturePointer(this IEventHandler handler, int pointerId)
		{
			PointerDispatchState stateFor = PointerCaptureHelper.GetStateFor(handler);
			if (stateFor != null)
			{
				stateFor.CapturePointer(handler, pointerId);
			}
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000A581 File Offset: 0x00008781
		public static void ReleasePointer(this IEventHandler handler, int pointerId)
		{
			PointerDispatchState stateFor = PointerCaptureHelper.GetStateFor(handler);
			if (stateFor != null)
			{
				stateFor.ReleasePointer(handler, pointerId);
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000A598 File Offset: 0x00008798
		public static IEventHandler GetCapturingElement(this IPanel panel, int pointerId)
		{
			IEventHandler result;
			if (panel == null)
			{
				result = null;
			}
			else
			{
				EventDispatcher dispatcher = panel.dispatcher;
				result = ((dispatcher != null) ? dispatcher.pointerState.GetCapturingElement(pointerId) : null);
			}
			return result;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000A5C8 File Offset: 0x000087C8
		public static void ReleasePointer(this IPanel panel, int pointerId)
		{
			if (panel != null)
			{
				EventDispatcher dispatcher = panel.dispatcher;
				if (dispatcher != null)
				{
					dispatcher.pointerState.ReleasePointer(pointerId);
				}
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000A5E8 File Offset: 0x000087E8
		internal static void ActivateCompatibilityMouseEvents(this IPanel panel, int pointerId)
		{
			if (panel != null)
			{
				EventDispatcher dispatcher = panel.dispatcher;
				if (dispatcher != null)
				{
					dispatcher.pointerState.ActivateCompatibilityMouseEvents(pointerId);
				}
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000A608 File Offset: 0x00008808
		internal static void PreventCompatibilityMouseEvents(this IPanel panel, int pointerId)
		{
			if (panel != null)
			{
				EventDispatcher dispatcher = panel.dispatcher;
				if (dispatcher != null)
				{
					dispatcher.pointerState.PreventCompatibilityMouseEvents(pointerId);
				}
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000A628 File Offset: 0x00008828
		internal static bool ShouldSendCompatibilityMouseEvents(this IPanel panel, IPointerEvent evt)
		{
			bool? flag;
			if (panel == null)
			{
				flag = null;
			}
			else
			{
				EventDispatcher dispatcher = panel.dispatcher;
				flag = ((dispatcher != null) ? new bool?(dispatcher.pointerState.ShouldSendCompatibilityMouseEvents(evt)) : null);
			}
			return flag ?? true;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000A681 File Offset: 0x00008881
		internal static void ProcessPointerCapture(this IPanel panel, int pointerId)
		{
			if (panel != null)
			{
				EventDispatcher dispatcher = panel.dispatcher;
				if (dispatcher != null)
				{
					dispatcher.pointerState.ProcessPointerCapture(pointerId);
				}
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000A6A1 File Offset: 0x000088A1
		internal static void ResetPointerDispatchState(this IPanel panel)
		{
			if (panel != null)
			{
				EventDispatcher dispatcher = panel.dispatcher;
				if (dispatcher != null)
				{
					dispatcher.pointerState.Reset();
				}
			}
		}
	}
}
