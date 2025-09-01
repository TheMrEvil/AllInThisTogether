using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x0200078B RID: 1931
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("82BA7092-4C88-427D-A7BC-16DD93FEB67E")]
	[ComImport]
	internal interface IRestrictedErrorInfo
	{
		// Token: 0x0600449A RID: 17562
		void GetErrorDetails([MarshalAs(UnmanagedType.BStr)] out string description, out int error, [MarshalAs(UnmanagedType.BStr)] out string restrictedDescription, [MarshalAs(UnmanagedType.BStr)] out string capabilitySid);

		// Token: 0x0600449B RID: 17563
		void GetReference([MarshalAs(UnmanagedType.BStr)] out string reference);
	}
}
