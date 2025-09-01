using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200006C RID: 108
	[CallbackIdentity(2102)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTTPRequestHeadersReceived_t
	{
		// Token: 0x0400010C RID: 268
		public const int k_iCallback = 2102;

		// Token: 0x0400010D RID: 269
		public HTTPRequestHandle m_hRequest;

		// Token: 0x0400010E RID: 270
		public ulong m_ulContextValue;
	}
}
