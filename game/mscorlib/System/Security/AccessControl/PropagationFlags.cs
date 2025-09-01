using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies how Access Control Entries (ACEs) are propagated to child objects.  These flags are significant only if inheritance flags are present.</summary>
	// Token: 0x02000543 RID: 1347
	[Flags]
	public enum PropagationFlags
	{
		/// <summary>Specifies that no inheritance flags are set.</summary>
		// Token: 0x040024D1 RID: 9425
		None = 0,
		/// <summary>Specifies that the ACE is not propagated to child objects.</summary>
		// Token: 0x040024D2 RID: 9426
		NoPropagateInherit = 1,
		/// <summary>Specifies that the ACE is propagated only to child objects. This includes both container and leaf child objects.</summary>
		// Token: 0x040024D3 RID: 9427
		InheritOnly = 2
	}
}
