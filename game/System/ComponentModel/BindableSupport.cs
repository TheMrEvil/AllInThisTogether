using System;

namespace System.ComponentModel
{
	/// <summary>Specifies values to indicate whether a property can be bound to a data element or another property.</summary>
	// Token: 0x02000383 RID: 899
	public enum BindableSupport
	{
		/// <summary>The property is not bindable at design time.</summary>
		// Token: 0x04000EE3 RID: 3811
		No,
		/// <summary>The property is bindable at design time.</summary>
		// Token: 0x04000EE4 RID: 3812
		Yes,
		/// <summary>The property is set to the default.</summary>
		// Token: 0x04000EE5 RID: 3813
		Default
	}
}
