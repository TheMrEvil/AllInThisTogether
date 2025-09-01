using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200004E RID: 78
	public static class MouseCaptureController
	{
		// Token: 0x060001DC RID: 476 RVA: 0x0000894C File Offset: 0x00006B4C
		public static bool IsMouseCaptured()
		{
			bool flag = !MouseCaptureController.m_IsMouseCapturedWarningEmitted;
			if (flag)
			{
				Debug.LogError("MouseCaptureController.IsMouseCaptured() can not be used in playmode. Please use PointerCaptureHelper.GetCapturingElement() instead.");
				MouseCaptureController.m_IsMouseCapturedWarningEmitted = true;
			}
			return false;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00008980 File Offset: 0x00006B80
		public static bool HasMouseCapture(this IEventHandler handler)
		{
			VisualElement handler2 = handler as VisualElement;
			return handler2.HasPointerCapture(PointerId.mousePointerId);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000089A4 File Offset: 0x00006BA4
		public static void CaptureMouse(this IEventHandler handler)
		{
			VisualElement visualElement = handler as VisualElement;
			bool flag = visualElement != null;
			if (flag)
			{
				visualElement.CapturePointer(PointerId.mousePointerId);
				visualElement.panel.ProcessPointerCapture(PointerId.mousePointerId);
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000089E0 File Offset: 0x00006BE0
		public static void ReleaseMouse(this IEventHandler handler)
		{
			VisualElement visualElement = handler as VisualElement;
			bool flag = visualElement != null;
			if (flag)
			{
				visualElement.ReleasePointer(PointerId.mousePointerId);
				visualElement.panel.ProcessPointerCapture(PointerId.mousePointerId);
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00008A1C File Offset: 0x00006C1C
		public static void ReleaseMouse()
		{
			bool flag = !MouseCaptureController.m_ReleaseMouseWarningEmitted;
			if (flag)
			{
				Debug.LogError("MouseCaptureController.ReleaseMouse() can not be used in playmode. Please use PointerCaptureHelper.GetCapturingElement() instead.");
				MouseCaptureController.m_ReleaseMouseWarningEmitted = true;
			}
		}

		// Token: 0x040000D8 RID: 216
		private static bool m_IsMouseCapturedWarningEmitted;

		// Token: 0x040000D9 RID: 217
		private static bool m_ReleaseMouseWarningEmitted;
	}
}
