using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the presence of object types for Access Control Entries (ACEs).</summary>
	// Token: 0x0200053F RID: 1343
	[Flags]
	public enum ObjectAceFlags
	{
		/// <summary>No object types are present.</summary>
		// Token: 0x040024C8 RID: 9416
		None = 0,
		/// <summary>The type of object that is associated with the ACE is present.</summary>
		// Token: 0x040024C9 RID: 9417
		ObjectAceTypePresent = 1,
		/// <summary>The type of object that can inherit the ACE.</summary>
		// Token: 0x040024CA RID: 9418
		InheritedObjectAceTypePresent = 2
	}
}
