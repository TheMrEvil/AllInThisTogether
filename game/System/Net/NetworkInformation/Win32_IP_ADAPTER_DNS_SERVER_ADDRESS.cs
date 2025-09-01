using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000749 RID: 1865
	internal struct Win32_IP_ADAPTER_DNS_SERVER_ADDRESS
	{
		// Token: 0x04002330 RID: 9008
		public Win32LengthFlagsUnion LengthFlags;

		// Token: 0x04002331 RID: 9009
		public IntPtr Next;

		// Token: 0x04002332 RID: 9010
		public Win32_SOCKET_ADDRESS Address;
	}
}
