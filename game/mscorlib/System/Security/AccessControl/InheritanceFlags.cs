using System;

namespace System.Security.AccessControl
{
	/// <summary>Inheritance flags specify the semantics of inheritance for access control entries (ACEs).</summary>
	// Token: 0x0200052F RID: 1327
	[Flags]
	public enum InheritanceFlags
	{
		/// <summary>The ACE is not inherited by child objects.</summary>
		// Token: 0x040024AB RID: 9387
		None = 0,
		/// <summary>The ACE is inherited by child container objects.</summary>
		// Token: 0x040024AC RID: 9388
		ContainerInherit = 1,
		/// <summary>The ACE is inherited by child leaf objects.</summary>
		// Token: 0x040024AD RID: 9389
		ObjectInherit = 2
	}
}
