using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Represents a generalized Clipboard format.</summary>
	// Token: 0x0200018A RID: 394
	public struct FORMATETC
	{
		/// <summary>Specifies the particular clipboard format of interest.</summary>
		// Token: 0x04000700 RID: 1792
		[MarshalAs(UnmanagedType.U2)]
		public short cfFormat;

		/// <summary>Specifies one of the <see cref="T:System.Runtime.InteropServices.ComTypes.DVASPECT" /> enumeration constants that indicates how much detail should be contained in the rendering.</summary>
		// Token: 0x04000701 RID: 1793
		[MarshalAs(UnmanagedType.U4)]
		public DVASPECT dwAspect;

		/// <summary>Specifies part of the aspect when the data must be split across page boundaries.</summary>
		// Token: 0x04000702 RID: 1794
		public int lindex;

		/// <summary>Specifies a pointer to a <see langword="DVTARGETDEVICE" /> structure containing information about the target device that the data is being composed for.</summary>
		// Token: 0x04000703 RID: 1795
		public IntPtr ptd;

		/// <summary>Specifies one of the <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" /> enumeration constants, which indicates the type of storage medium used to transfer the object's data.</summary>
		// Token: 0x04000704 RID: 1796
		[MarshalAs(UnmanagedType.U4)]
		public TYMED tymed;
	}
}
