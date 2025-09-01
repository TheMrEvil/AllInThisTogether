using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Describes the exceptions that occur during <see langword="IDispatch::Invoke" />.</summary>
	// Token: 0x020007C3 RID: 1987
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct EXCEPINFO
	{
		/// <summary>Represents an error code identifying the error.</summary>
		// Token: 0x04002CC8 RID: 11464
		public short wCode;

		/// <summary>This field is reserved; it must be set to 0.</summary>
		// Token: 0x04002CC9 RID: 11465
		public short wReserved;

		/// <summary>Indicates the name of the source of the exception. Typically, this is an application name.</summary>
		// Token: 0x04002CCA RID: 11466
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrSource;

		/// <summary>Describes the error intended for the customer.</summary>
		// Token: 0x04002CCB RID: 11467
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrDescription;

		/// <summary>Contains the fully-qualified drive, path, and file name of a Help file that contains more information about the error.</summary>
		// Token: 0x04002CCC RID: 11468
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrHelpFile;

		/// <summary>Indicates the Help context ID of the topic within the Help file.</summary>
		// Token: 0x04002CCD RID: 11469
		public int dwHelpContext;

		/// <summary>This field is reserved; it must be set to <see langword="null" />.</summary>
		// Token: 0x04002CCE RID: 11470
		public IntPtr pvReserved;

		/// <summary>Represents a pointer to a function that takes an <see cref="T:System.Runtime.InteropServices.EXCEPINFO" /> structure as an argument and returns an HRESULT value. If deferred fill-in is not desired, this field is set to <see langword="null" />.</summary>
		// Token: 0x04002CCF RID: 11471
		public IntPtr pfnDeferredFillIn;

		/// <summary>A return value describing the error.</summary>
		// Token: 0x04002CD0 RID: 11472
		public int scode;
	}
}
