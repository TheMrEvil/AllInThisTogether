using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the type of access control modification to perform. This enumeration is used by methods of the <see cref="T:System.Security.AccessControl.ObjectSecurity" /> class and its descendents.</summary>
	// Token: 0x02000500 RID: 1280
	public enum AccessControlModification
	{
		/// <summary>Add the specified authorization rule to the access control list (ACL).</summary>
		// Token: 0x040023FE RID: 9214
		Add,
		/// <summary>Remove all authorization rules from the ACL, then add the specified authorization rule to the ACL.</summary>
		// Token: 0x040023FF RID: 9215
		Set,
		/// <summary>Remove authorization rules that contain the same SID as the specified authorization rule from the ACL, and then add the specified authorization rule to the ACL.</summary>
		// Token: 0x04002400 RID: 9216
		Reset,
		/// <summary>Remove authorization rules that contain the same security identifier (SID) and access mask as the specified authorization rule from the ACL.</summary>
		// Token: 0x04002401 RID: 9217
		Remove,
		/// <summary>Remove authorization rules that contain the same SID as the specified authorization rule from the ACL.</summary>
		// Token: 0x04002402 RID: 9218
		RemoveAll,
		/// <summary>Remove authorization rules that exactly match the specified authorization rule from the ACL.</summary>
		// Token: 0x04002403 RID: 9219
		RemoveSpecific
	}
}
