using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.FUNCDESC" /> instead.</summary>
	// Token: 0x02000725 RID: 1829
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.FUNCDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	public struct FUNCDESC
	{
		/// <summary>Identifies the function member ID.</summary>
		// Token: 0x04002B3F RID: 11071
		public int memid;

		/// <summary>Stores the count of errors a function can return on a 16-bit system.</summary>
		// Token: 0x04002B40 RID: 11072
		public IntPtr lprgscode;

		/// <summary>Indicates the size of <see cref="F:System.Runtime.InteropServices.FUNCDESC.cParams" />.</summary>
		// Token: 0x04002B41 RID: 11073
		public IntPtr lprgelemdescParam;

		/// <summary>Specifies whether the function is virtual, static, or dispatch-only.</summary>
		// Token: 0x04002B42 RID: 11074
		public FUNCKIND funckind;

		/// <summary>Specifies the type of a property function.</summary>
		// Token: 0x04002B43 RID: 11075
		public INVOKEKIND invkind;

		/// <summary>Specifies the calling convention of a function.</summary>
		// Token: 0x04002B44 RID: 11076
		public CALLCONV callconv;

		/// <summary>Counts the total number of parameters.</summary>
		// Token: 0x04002B45 RID: 11077
		public short cParams;

		/// <summary>Counts the optional parameters.</summary>
		// Token: 0x04002B46 RID: 11078
		public short cParamsOpt;

		/// <summary>Specifies the offset in the VTBL for <see cref="F:System.Runtime.InteropServices.FUNCKIND.FUNC_VIRTUAL" />.</summary>
		// Token: 0x04002B47 RID: 11079
		public short oVft;

		/// <summary>Counts the permitted return values.</summary>
		// Token: 0x04002B48 RID: 11080
		public short cScodes;

		/// <summary>Contains the return type of the function.</summary>
		// Token: 0x04002B49 RID: 11081
		public ELEMDESC elemdescFunc;

		/// <summary>Indicates the <see cref="T:System.Runtime.InteropServices.FUNCFLAGS" /> of a function.</summary>
		// Token: 0x04002B4A RID: 11082
		public short wFuncFlags;
	}
}
