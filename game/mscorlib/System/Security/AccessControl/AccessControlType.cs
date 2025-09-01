using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies whether an <see cref="T:System.Security.AccessControl.AccessRule" /> object is used to allow or deny access. These values are not flags, and they cannot be combined.</summary>
	// Token: 0x02000502 RID: 1282
	public enum AccessControlType
	{
		/// <summary>The <see cref="T:System.Security.AccessControl.AccessRule" /> object is used to allow access to a secured object.</summary>
		// Token: 0x0400240C RID: 9228
		Allow,
		/// <summary>The <see cref="T:System.Security.AccessControl.AccessRule" /> object is used to deny access to a secured object.</summary>
		// Token: 0x0400240D RID: 9229
		Deny
	}
}
