using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Stores the parameters that are used during a moniker binding operation.</summary>
	// Token: 0x0200079E RID: 1950
	public struct BIND_OPTS
	{
		/// <summary>Specifies the size, in bytes, of the <see langword="BIND_OPTS" /> structure.</summary>
		// Token: 0x04002C43 RID: 11331
		public int cbStruct;

		/// <summary>Controls aspects of moniker binding operations.</summary>
		// Token: 0x04002C44 RID: 11332
		public int grfFlags;

		/// <summary>Represents flags that should be used when opening the file that contains the object identified by the moniker.</summary>
		// Token: 0x04002C45 RID: 11333
		public int grfMode;

		/// <summary>Indicates the amount of time (clock time in milliseconds, as returned by the <see langword="GetTickCount" /> function) that the caller specified to complete the binding operation.</summary>
		// Token: 0x04002C46 RID: 11334
		public int dwTickCountDeadline;
	}
}
