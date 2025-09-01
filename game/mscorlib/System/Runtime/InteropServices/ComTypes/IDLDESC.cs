using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Contains information needed for transferring a structure element, parameter, or function return value between processes.</summary>
	// Token: 0x020007B9 RID: 1977
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct IDLDESC
	{
		/// <summary>Reserved; set to <see langword="null" />.</summary>
		// Token: 0x04002CA4 RID: 11428
		public IntPtr dwReserved;

		/// <summary>Indicates an <see cref="T:System.Runtime.InteropServices.IDLFLAG" /> value describing the type.</summary>
		// Token: 0x04002CA5 RID: 11429
		public IDLFLAG wIDLFlags;
	}
}
