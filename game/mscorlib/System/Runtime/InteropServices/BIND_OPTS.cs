using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.BIND_OPTS" /> instead.</summary>
	// Token: 0x02000739 RID: 1849
	[Obsolete]
	public struct BIND_OPTS
	{
		/// <summary>Specifies the size of the <see langword="BIND_OPTS" /> structure in bytes.</summary>
		// Token: 0x04002BAF RID: 11183
		public int cbStruct;

		/// <summary>Controls aspects of moniker binding operations.</summary>
		// Token: 0x04002BB0 RID: 11184
		public int grfFlags;

		/// <summary>Flags that should be used when opening the file that contains the object identified by the moniker.</summary>
		// Token: 0x04002BB1 RID: 11185
		public int grfMode;

		/// <summary>Indicates the amount of time (clock time in milliseconds, as returned by the <see langword="GetTickCount" /> function) the caller specified to complete the binding operation.</summary>
		// Token: 0x04002BB2 RID: 11186
		public int dwTickCountDeadline;
	}
}
