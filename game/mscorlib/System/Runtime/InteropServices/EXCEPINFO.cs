using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.EXCEPINFO" /> instead.</summary>
	// Token: 0x02000730 RID: 1840
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.EXCEPINFO instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct EXCEPINFO
	{
		/// <summary>Represents an error code identifying the error.</summary>
		// Token: 0x04002B6F RID: 11119
		public short wCode;

		/// <summary>This field is reserved; must be set to 0.</summary>
		// Token: 0x04002B70 RID: 11120
		public short wReserved;

		/// <summary>Indicates the name of the source of the exception. Typically, this is an application name.</summary>
		// Token: 0x04002B71 RID: 11121
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrSource;

		/// <summary>Describes the error intended for the customer.</summary>
		// Token: 0x04002B72 RID: 11122
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrDescription;

		/// <summary>Contains the fully-qualified drive, path, and file name of a Help file with more information about the error.</summary>
		// Token: 0x04002B73 RID: 11123
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrHelpFile;

		/// <summary>Indicates the Help context ID of the topic within the Help file.</summary>
		// Token: 0x04002B74 RID: 11124
		public int dwHelpContext;

		/// <summary>This field is reserved; must be set to <see langword="null" />.</summary>
		// Token: 0x04002B75 RID: 11125
		public IntPtr pvReserved;

		/// <summary>Represents a pointer to a function that takes an <see cref="T:System.Runtime.InteropServices.EXCEPINFO" /> structure as an argument and returns an HRESULT value. If deferred fill-in is not desired, this field is set to <see langword="null" />.</summary>
		// Token: 0x04002B76 RID: 11126
		public IntPtr pfnDeferredFillIn;
	}
}
