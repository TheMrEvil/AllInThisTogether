using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000746 RID: 1862
	internal struct Win32_IP_ADDR_STRING
	{
		// Token: 0x04002325 RID: 8997
		public IntPtr Next;

		// Token: 0x04002326 RID: 8998
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
		public string IpAddress;

		// Token: 0x04002327 RID: 8999
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
		public string IpMask;

		// Token: 0x04002328 RID: 9000
		public uint Context;
	}
}
