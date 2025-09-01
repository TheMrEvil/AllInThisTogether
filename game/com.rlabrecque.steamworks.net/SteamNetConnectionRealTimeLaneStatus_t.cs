using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200017C RID: 380
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamNetConnectionRealTimeLaneStatus_t
	{
		// Token: 0x04000A2A RID: 2602
		public int m_cbPendingUnreliable;

		// Token: 0x04000A2B RID: 2603
		public int m_cbPendingReliable;

		// Token: 0x04000A2C RID: 2604
		public int m_cbSentUnackedReliable;

		// Token: 0x04000A2D RID: 2605
		public int _reservePad1;

		// Token: 0x04000A2E RID: 2606
		public SteamNetworkingMicroseconds m_usecQueueTime;

		// Token: 0x04000A2F RID: 2607
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
		public uint[] reserved;
	}
}
