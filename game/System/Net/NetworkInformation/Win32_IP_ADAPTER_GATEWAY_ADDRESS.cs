using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200074B RID: 1867
	internal struct Win32_IP_ADAPTER_GATEWAY_ADDRESS
	{
		// Token: 0x04002336 RID: 9014
		public Win32LengthFlagsUnion LengthFlags;

		// Token: 0x04002337 RID: 9015
		public IntPtr Next;

		// Token: 0x04002338 RID: 9016
		public Win32_SOCKET_ADDRESS Address;
	}
}
