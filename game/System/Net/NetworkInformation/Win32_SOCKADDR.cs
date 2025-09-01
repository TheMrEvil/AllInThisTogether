using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200074E RID: 1870
	internal struct Win32_SOCKADDR
	{
		// Token: 0x04002346 RID: 9030
		public ushort AddressFamily;

		// Token: 0x04002347 RID: 9031
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
		public byte[] AddressData;
	}
}
