using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200074C RID: 1868
	internal struct Win32_IP_ADAPTER_WINS_SERVER_ADDRESS
	{
		// Token: 0x04002339 RID: 9017
		public Win32LengthFlagsUnion LengthFlags;

		// Token: 0x0400233A RID: 9018
		public IntPtr Next;

		// Token: 0x0400233B RID: 9019
		public Win32_SOCKET_ADDRESS Address;
	}
}
