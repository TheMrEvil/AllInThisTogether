using System;

namespace System.ComponentModel
{
	/// <summary>Defines identifiers for types of inheritance levels.</summary>
	// Token: 0x0200039F RID: 927
	public enum InheritanceLevel
	{
		/// <summary>The object is inherited.</summary>
		// Token: 0x04000F23 RID: 3875
		Inherited = 1,
		/// <summary>The object is inherited, but has read-only access.</summary>
		// Token: 0x04000F24 RID: 3876
		InheritedReadOnly,
		/// <summary>The object is not inherited.</summary>
		// Token: 0x04000F25 RID: 3877
		NotInherited
	}
}
