using System;

namespace System.Security.Permissions
{
	/// <summary>Specifies the type of clipboard access that is allowed to the calling code.</summary>
	// Token: 0x02000430 RID: 1072
	public enum UIPermissionClipboard
	{
		/// <summary>Clipboard can be used without restriction.</summary>
		// Token: 0x04001FFC RID: 8188
		AllClipboard = 2,
		/// <summary>Clipboard cannot be used.</summary>
		// Token: 0x04001FFD RID: 8189
		NoClipboard = 0,
		/// <summary>The ability to put data on the clipboard (<see langword="Copy" />, <see langword="Cut" />) is unrestricted. Intrinsic controls that accept <see langword="Paste" />, such as text box, can accept the clipboard data, but user controls that must programmatically read the clipboard cannot.</summary>
		// Token: 0x04001FFE RID: 8190
		OwnClipboard
	}
}
