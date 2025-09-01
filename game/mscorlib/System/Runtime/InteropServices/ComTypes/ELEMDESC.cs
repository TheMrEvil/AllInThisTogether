using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Contains the type description and process transfer information for a variable, function, or a function parameter.</summary>
	// Token: 0x020007BD RID: 1981
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct ELEMDESC
	{
		/// <summary>Identifies the type of the element.</summary>
		// Token: 0x04002CB3 RID: 11443
		public TYPEDESC tdesc;

		/// <summary>Contains information about an element.</summary>
		// Token: 0x04002CB4 RID: 11444
		public ELEMDESC.DESCUNION desc;

		/// <summary>Contains information about an element.</summary>
		// Token: 0x020007BE RID: 1982
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct DESCUNION
		{
			/// <summary>Contains information for remoting the element.</summary>
			// Token: 0x04002CB5 RID: 11445
			[FieldOffset(0)]
			public IDLDESC idldesc;

			/// <summary>Contains information about the parameter.</summary>
			// Token: 0x04002CB6 RID: 11446
			[FieldOffset(0)]
			public PARAMDESC paramdesc;
		}
	}
}
