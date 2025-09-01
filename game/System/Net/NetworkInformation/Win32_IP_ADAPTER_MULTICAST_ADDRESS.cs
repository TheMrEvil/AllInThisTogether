using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200074A RID: 1866
	internal struct Win32_IP_ADAPTER_MULTICAST_ADDRESS
	{
		// Token: 0x04002333 RID: 9011
		public Win32LengthFlagsUnion LengthFlags;

		// Token: 0x04002334 RID: 9012
		public IntPtr Next;

		// Token: 0x04002335 RID: 9013
		public Win32_SOCKET_ADDRESS Address;
	}
}
