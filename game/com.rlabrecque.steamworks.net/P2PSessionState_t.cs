using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000174 RID: 372
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct P2PSessionState_t
	{
		// Token: 0x040009E0 RID: 2528
		public byte m_bConnectionActive;

		// Token: 0x040009E1 RID: 2529
		public byte m_bConnecting;

		// Token: 0x040009E2 RID: 2530
		public byte m_eP2PSessionError;

		// Token: 0x040009E3 RID: 2531
		public byte m_bUsingRelay;

		// Token: 0x040009E4 RID: 2532
		public int m_nBytesQueuedForSend;

		// Token: 0x040009E5 RID: 2533
		public int m_nPacketsQueuedForSend;

		// Token: 0x040009E6 RID: 2534
		public uint m_nRemoteIP;

		// Token: 0x040009E7 RID: 2535
		public ushort m_nRemotePort;
	}
}
