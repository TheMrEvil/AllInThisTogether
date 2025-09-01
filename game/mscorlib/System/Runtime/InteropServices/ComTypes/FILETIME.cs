using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Represents the number of 100-nanosecond intervals since January 1, 1601. This structure is a 64-bit value.</summary>
	// Token: 0x020007A9 RID: 1961
	public struct FILETIME
	{
		/// <summary>Specifies the low 32 bits of the <see langword="FILETIME" />.</summary>
		// Token: 0x04002C49 RID: 11337
		public int dwLowDateTime;

		/// <summary>Specifies the high 32 bits of the <see langword="FILETIME" />.</summary>
		// Token: 0x04002C4A RID: 11338
		public int dwHighDateTime;
	}
}
