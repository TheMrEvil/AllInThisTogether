using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Describes the type of a variable, return type of a function, or the type of a function parameter.</summary>
	// Token: 0x020007BC RID: 1980
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPEDESC
	{
		/// <summary>If the variable is <see langword="VT_SAFEARRAY" /> or <see langword="VT_PTR" />, the <see langword="lpValue" /> field contains a pointer to a <see langword="TYPEDESC" /> that specifies the element type.</summary>
		// Token: 0x04002CB1 RID: 11441
		public IntPtr lpValue;

		/// <summary>Indicates the variant type for the item described by this <see langword="TYPEDESC" />.</summary>
		// Token: 0x04002CB2 RID: 11442
		public short vt;
	}
}
