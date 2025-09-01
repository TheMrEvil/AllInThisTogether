using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the actions that are permitted for securable objects.</summary>
	// Token: 0x020004FF RID: 1279
	[Flags]
	public enum AccessControlActions
	{
		/// <summary>Specifies no access.</summary>
		// Token: 0x040023FA RID: 9210
		None = 0,
		/// <summary>Specifies read-only access.</summary>
		// Token: 0x040023FB RID: 9211
		View = 1,
		/// <summary>Specifies write-only access.</summary>
		// Token: 0x040023FC RID: 9212
		Change = 2
	}
}
