using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Defines the kind of variable.</summary>
	// Token: 0x020007BF RID: 1983
	[Serializable]
	public enum VARKIND
	{
		/// <summary>The variable is a field or member of the type. It exists at a fixed offset within each instance of the type.</summary>
		// Token: 0x04002CB8 RID: 11448
		VAR_PERINSTANCE,
		/// <summary>There is only one instance of the variable.</summary>
		// Token: 0x04002CB9 RID: 11449
		VAR_STATIC,
		/// <summary>The <see langword="VARDESC" /> structure describes a symbolic constant. There is no memory associated with it.</summary>
		// Token: 0x04002CBA RID: 11450
		VAR_CONST,
		/// <summary>The variable can be accessed only through <see langword="IDispatch::Invoke" />.</summary>
		// Token: 0x04002CBB RID: 11451
		VAR_DISPATCH
	}
}
