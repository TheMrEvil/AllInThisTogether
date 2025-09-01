using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.DISPPARAMS" /> instead.</summary>
	// Token: 0x0200072F RID: 1839
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.DISPPARAMS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct DISPPARAMS
	{
		/// <summary>Represents a reference to the array of arguments.</summary>
		// Token: 0x04002B6B RID: 11115
		public IntPtr rgvarg;

		/// <summary>Represents the dispatch IDs of named arguments.</summary>
		// Token: 0x04002B6C RID: 11116
		public IntPtr rgdispidNamedArgs;

		/// <summary>Represents the count of arguments.</summary>
		// Token: 0x04002B6D RID: 11117
		public int cArgs;

		/// <summary>Represents the count of named arguments.</summary>
		// Token: 0x04002B6E RID: 11118
		public int cNamedArgs;
	}
}
