using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000740 RID: 1856
	[Guid("1CF2B120-547D-101B-8E65-08002B2BD119")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[SuppressUnmanagedCodeSecurity]
	[ComImport]
	internal interface IErrorInfo
	{
		// Token: 0x06004139 RID: 16697
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int GetGUID(out Guid pGuid);

		// Token: 0x0600413A RID: 16698
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int GetSource([MarshalAs(UnmanagedType.BStr)] out string pBstrSource);

		// Token: 0x0600413B RID: 16699
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int GetDescription([MarshalAs(UnmanagedType.BStr)] out string pbstrDescription);

		// Token: 0x0600413C RID: 16700
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int GetHelpFile([MarshalAs(UnmanagedType.BStr)] out string pBstrHelpFile);

		// Token: 0x0600413D RID: 16701
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		int GetHelpContext(out uint pdwHelpContext);
	}
}
