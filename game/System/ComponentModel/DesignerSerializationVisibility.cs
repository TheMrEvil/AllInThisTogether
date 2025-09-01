using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the visibility a property has to the design-time serializer.</summary>
	// Token: 0x02000369 RID: 873
	public enum DesignerSerializationVisibility
	{
		/// <summary>The code generator does not produce code for the object.</summary>
		// Token: 0x04000EAE RID: 3758
		Hidden,
		/// <summary>The code generator produces code for the object.</summary>
		// Token: 0x04000EAF RID: 3759
		Visible,
		/// <summary>The code generator produces code for the contents of the object, rather than for the object itself.</summary>
		// Token: 0x04000EB0 RID: 3760
		Content
	}
}
