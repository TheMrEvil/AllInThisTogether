using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.FILETIME" /> instead.</summary>
	// Token: 0x0200073E RID: 1854
	[Obsolete]
	public struct FILETIME
	{
		/// <summary>Specifies the low 32 bits of the <see langword="FILETIME" />.</summary>
		// Token: 0x04002BBF RID: 11199
		public int dwLowDateTime;

		/// <summary>Specifies the high 32 bits of the <see langword="FILETIME" />.</summary>
		// Token: 0x04002BC0 RID: 11200
		public int dwHighDateTime;
	}
}
