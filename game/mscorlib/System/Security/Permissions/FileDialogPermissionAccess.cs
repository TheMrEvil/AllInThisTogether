using System;

namespace System.Security.Permissions
{
	/// <summary>Specifies the type of access to files allowed through the File dialog boxes.</summary>
	// Token: 0x02000429 RID: 1065
	[Flags]
	public enum FileDialogPermissionAccess
	{
		/// <summary>No access to files through the File dialog boxes.</summary>
		// Token: 0x04001FCE RID: 8142
		None = 0,
		/// <summary>Ability to open files through the File dialog boxes.</summary>
		// Token: 0x04001FCF RID: 8143
		Open = 1,
		/// <summary>Ability to open and save files through the File dialog boxes.</summary>
		// Token: 0x04001FD0 RID: 8144
		OpenSave = 3,
		/// <summary>Ability to save files through the File dialog boxes.</summary>
		// Token: 0x04001FD1 RID: 8145
		Save = 2
	}
}
