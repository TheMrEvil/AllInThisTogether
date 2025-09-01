using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Defines a function description.</summary>
	// Token: 0x020007B7 RID: 1975
	public struct FUNCDESC
	{
		/// <summary>Identifies the function member ID.</summary>
		// Token: 0x04002C92 RID: 11410
		public int memid;

		/// <summary>Stores the count of errors a function can return on a 16-bit system.</summary>
		// Token: 0x04002C93 RID: 11411
		public IntPtr lprgscode;

		/// <summary>Indicates the size of <see cref="F:System.Runtime.InteropServices.FUNCDESC.cParams" />.</summary>
		// Token: 0x04002C94 RID: 11412
		public IntPtr lprgelemdescParam;

		/// <summary>Specifies whether the function is virtual, static, or dispatch-only.</summary>
		// Token: 0x04002C95 RID: 11413
		public FUNCKIND funckind;

		/// <summary>Specifies the type of a property function.</summary>
		// Token: 0x04002C96 RID: 11414
		public INVOKEKIND invkind;

		/// <summary>Specifies the calling convention of a function.</summary>
		// Token: 0x04002C97 RID: 11415
		public CALLCONV callconv;

		/// <summary>Counts the total number of parameters.</summary>
		// Token: 0x04002C98 RID: 11416
		public short cParams;

		/// <summary>Counts the optional parameters.</summary>
		// Token: 0x04002C99 RID: 11417
		public short cParamsOpt;

		/// <summary>Specifies the offset in the VTBL for <see cref="F:System.Runtime.InteropServices.FUNCKIND.FUNC_VIRTUAL" />.</summary>
		// Token: 0x04002C9A RID: 11418
		public short oVft;

		/// <summary>Counts the permitted return values.</summary>
		// Token: 0x04002C9B RID: 11419
		public short cScodes;

		/// <summary>Contains the return type of the function.</summary>
		// Token: 0x04002C9C RID: 11420
		public ELEMDESC elemdescFunc;

		/// <summary>Indicates the <see cref="T:System.Runtime.InteropServices.FUNCFLAGS" /> of a function.</summary>
		// Token: 0x04002C9D RID: 11421
		public short wFuncFlags;
	}
}
