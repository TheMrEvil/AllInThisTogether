using System;

namespace System.Security.Permissions
{
	/// <summary>Specifies the type of windows that code is allowed to use.</summary>
	// Token: 0x02000431 RID: 1073
	public enum UIPermissionWindow
	{
		/// <summary>Users can use all windows and user input events without restriction.</summary>
		// Token: 0x04002000 RID: 8192
		AllWindows = 3,
		/// <summary>Users cannot use any windows or user interface events. No user interface can be used.</summary>
		// Token: 0x04002001 RID: 8193
		NoWindows = 0,
		/// <summary>Users can only use <see cref="F:System.Security.Permissions.UIPermissionWindow.SafeSubWindows" /> for drawing, and can only use user input events for user interface within that subwindow. Examples of <see cref="F:System.Security.Permissions.UIPermissionWindow.SafeSubWindows" /> are a <see cref="T:System.Windows.Forms.MessageBox" />, common dialog controls, and a control displayed within a browser.</summary>
		// Token: 0x04002002 RID: 8194
		SafeSubWindows,
		/// <summary>Users can only use <see cref="F:System.Security.Permissions.UIPermissionWindow.SafeTopLevelWindows" /> and <see cref="F:System.Security.Permissions.UIPermissionWindow.SafeSubWindows" /> for drawing, and can only use user input events for the user interface within those top-level windows and subwindows.</summary>
		// Token: 0x04002003 RID: 8195
		SafeTopLevelWindows
	}
}
