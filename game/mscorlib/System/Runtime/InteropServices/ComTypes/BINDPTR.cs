using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Contains a pointer to a bound-to <see cref="T:System.Runtime.InteropServices.FUNCDESC" /> structure, <see cref="T:System.Runtime.InteropServices.VARDESC" /> structure, or an <see langword="ITypeComp" /> interface.</summary>
	// Token: 0x020007B1 RID: 1969
	[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
	public struct BINDPTR
	{
		/// <summary>Represents a pointer to a <see cref="T:System.Runtime.InteropServices.FUNCDESC" /> structure.</summary>
		// Token: 0x04002C5D RID: 11357
		[FieldOffset(0)]
		public IntPtr lpfuncdesc;

		/// <summary>Represents a pointer to a <see cref="T:System.Runtime.InteropServices.VARDESC" /> structure.</summary>
		// Token: 0x04002C5E RID: 11358
		[FieldOffset(0)]
		public IntPtr lpvardesc;

		/// <summary>Represents a pointer to an <see cref="T:System.Runtime.InteropServices.ComTypes.ITypeComp" /> interface.</summary>
		// Token: 0x04002C5F RID: 11359
		[FieldOffset(0)]
		public IntPtr lptcomp;
	}
}
