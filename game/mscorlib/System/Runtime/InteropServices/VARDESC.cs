using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.VARDESC" /> instead.</summary>
	// Token: 0x0200072D RID: 1837
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.VARDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct VARDESC
	{
		/// <summary>Indicates the member ID of a variable.</summary>
		// Token: 0x04002B64 RID: 11108
		public int memid;

		/// <summary>This field is reserved for future use.</summary>
		// Token: 0x04002B65 RID: 11109
		public string lpstrSchema;

		/// <summary>Contains the variable type.</summary>
		// Token: 0x04002B66 RID: 11110
		public ELEMDESC elemdescVar;

		/// <summary>Defines the properties of a variable.</summary>
		// Token: 0x04002B67 RID: 11111
		public short wVarFlags;

		/// <summary>Defines how a variable should be marshaled.</summary>
		// Token: 0x04002B68 RID: 11112
		public VarEnum varkind;

		/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.VARDESC.DESCUNION" /> instead.</summary>
		// Token: 0x0200072E RID: 1838
		[ComVisible(false)]
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct DESCUNION
		{
			/// <summary>Indicates the offset of this variable within the instance.</summary>
			// Token: 0x04002B69 RID: 11113
			[FieldOffset(0)]
			public int oInst;

			/// <summary>Describes a symbolic constant.</summary>
			// Token: 0x04002B6A RID: 11114
			[FieldOffset(0)]
			public IntPtr lpvarValue;
		}
	}
}
