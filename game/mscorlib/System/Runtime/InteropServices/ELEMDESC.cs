using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.ELEMDESC" /> instead.</summary>
	// Token: 0x0200072B RID: 1835
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.ELEMDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct ELEMDESC
	{
		/// <summary>Identifies the type of the element.</summary>
		// Token: 0x04002B60 RID: 11104
		public TYPEDESC tdesc;

		/// <summary>Contains information about an element.</summary>
		// Token: 0x04002B61 RID: 11105
		public ELEMDESC.DESCUNION desc;

		/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.ELEMDESC.DESCUNION" /> instead.</summary>
		// Token: 0x0200072C RID: 1836
		[ComVisible(false)]
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct DESCUNION
		{
			/// <summary>Contains information for remoting the element.</summary>
			// Token: 0x04002B62 RID: 11106
			[FieldOffset(0)]
			public IDLDESC idldesc;

			/// <summary>Contains information about the parameter.</summary>
			// Token: 0x04002B63 RID: 11107
			[FieldOffset(0)]
			public PARAMDESC paramdesc;
		}
	}
}
