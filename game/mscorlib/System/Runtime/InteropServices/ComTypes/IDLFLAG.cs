using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Describes how to transfer a structure element, parameter, or function return value between processes.</summary>
	// Token: 0x020007B8 RID: 1976
	[Flags]
	[Serializable]
	public enum IDLFLAG : short
	{
		/// <summary>Does not specify whether the parameter passes or receives information.</summary>
		// Token: 0x04002C9F RID: 11423
		IDLFLAG_NONE = 0,
		/// <summary>The parameter passes information from the caller to the callee.</summary>
		// Token: 0x04002CA0 RID: 11424
		IDLFLAG_FIN = 1,
		/// <summary>The parameter returns information from the callee to the caller.</summary>
		// Token: 0x04002CA1 RID: 11425
		IDLFLAG_FOUT = 2,
		/// <summary>The parameter is the local identifier of a client application.</summary>
		// Token: 0x04002CA2 RID: 11426
		IDLFLAG_FLCID = 4,
		/// <summary>The parameter is the return value of the member.</summary>
		// Token: 0x04002CA3 RID: 11427
		IDLFLAG_FRETVAL = 8
	}
}
