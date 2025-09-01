using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Contains the arguments passed to a method or property by <see langword="IDispatch::Invoke" />.</summary>
	// Token: 0x020007C2 RID: 1986
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct DISPPARAMS
	{
		/// <summary>Represents a reference to the array of arguments.</summary>
		// Token: 0x04002CC4 RID: 11460
		public IntPtr rgvarg;

		/// <summary>Represents the dispatch IDs of named arguments.</summary>
		// Token: 0x04002CC5 RID: 11461
		public IntPtr rgdispidNamedArgs;

		/// <summary>Represents the count of arguments.</summary>
		// Token: 0x04002CC6 RID: 11462
		public int cArgs;

		/// <summary>Represents the count of named arguments</summary>
		// Token: 0x04002CC7 RID: 11463
		public int cNamedArgs;
	}
}
