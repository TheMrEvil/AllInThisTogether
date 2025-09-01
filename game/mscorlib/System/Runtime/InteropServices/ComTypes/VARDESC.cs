using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Describes a variable, constant, or data member.</summary>
	// Token: 0x020007C0 RID: 1984
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct VARDESC
	{
		/// <summary>Indicates the member ID of a variable.</summary>
		// Token: 0x04002CBC RID: 11452
		public int memid;

		/// <summary>This field is reserved for future use.</summary>
		// Token: 0x04002CBD RID: 11453
		public string lpstrSchema;

		/// <summary>Contains information about a variable.</summary>
		// Token: 0x04002CBE RID: 11454
		public VARDESC.DESCUNION desc;

		/// <summary>Contains the variable type.</summary>
		// Token: 0x04002CBF RID: 11455
		public ELEMDESC elemdescVar;

		/// <summary>Defines the properties of a variable.</summary>
		// Token: 0x04002CC0 RID: 11456
		public short wVarFlags;

		/// <summary>Defines how to marshal a variable.</summary>
		// Token: 0x04002CC1 RID: 11457
		public VARKIND varkind;

		/// <summary>Contains information about a variable.</summary>
		// Token: 0x020007C1 RID: 1985
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct DESCUNION
		{
			/// <summary>Indicates the offset of this variable within the instance.</summary>
			// Token: 0x04002CC2 RID: 11458
			[FieldOffset(0)]
			public int oInst;

			/// <summary>Describes a symbolic constant.</summary>
			// Token: 0x04002CC3 RID: 11459
			[FieldOffset(0)]
			public IntPtr lpvarValue;
		}
	}
}
