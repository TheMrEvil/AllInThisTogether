using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000748 RID: 1864
	internal struct Win32_IP_ADAPTER_ANYCAST_ADDRESS
	{
		// Token: 0x0400232D RID: 9005
		public Win32LengthFlagsUnion LengthFlags;

		// Token: 0x0400232E RID: 9006
		public IntPtr Next;

		// Token: 0x0400232F RID: 9007
		public Win32_SOCKET_ADDRESS Address;
	}
}
