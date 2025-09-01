using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000C2 RID: 194
	internal interface IUIElementsUtility
	{
		// Token: 0x06000676 RID: 1654
		bool TakeCapture();

		// Token: 0x06000677 RID: 1655
		bool ReleaseCapture();

		// Token: 0x06000678 RID: 1656
		bool ProcessEvent(int instanceID, IntPtr nativeEventPtr, ref bool eventHandled);

		// Token: 0x06000679 RID: 1657
		bool CleanupRoots();

		// Token: 0x0600067A RID: 1658
		bool EndContainerGUIFromException(Exception exception);

		// Token: 0x0600067B RID: 1659
		bool MakeCurrentIMGUIContainerDirty();

		// Token: 0x0600067C RID: 1660
		void UpdateSchedulers();

		// Token: 0x0600067D RID: 1661
		void RequestRepaintForPanels(Action<ScriptableObject> repaintCallback);
	}
}
