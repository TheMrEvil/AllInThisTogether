using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200017B RID: 379
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamNetConnectionRealTimeStatus_t
	{
		// Token: 0x04000A1C RID: 2588
		public ESteamNetworkingConnectionState m_eState;

		// Token: 0x04000A1D RID: 2589
		public int m_nPing;

		// Token: 0x04000A1E RID: 2590
		public float m_flConnectionQualityLocal;

		// Token: 0x04000A1F RID: 2591
		public float m_flConnectionQualityRemote;

		// Token: 0x04000A20 RID: 2592
		public float m_flOutPacketsPerSec;

		// Token: 0x04000A21 RID: 2593
		public float m_flOutBytesPerSec;

		// Token: 0x04000A22 RID: 2594
		public float m_flInPacketsPerSec;

		// Token: 0x04000A23 RID: 2595
		public float m_flInBytesPerSec;

		// Token: 0x04000A24 RID: 2596
		public int m_nSendRateBytesPerSecond;

		// Token: 0x04000A25 RID: 2597
		public int m_cbPendingUnreliable;

		// Token: 0x04000A26 RID: 2598
		public int m_cbPendingReliable;

		// Token: 0x04000A27 RID: 2599
		public int m_cbSentUnackedReliable;

		// Token: 0x04000A28 RID: 2600
		public SteamNetworkingMicroseconds m_usecQueueTime;

		// Token: 0x04000A29 RID: 2601
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public uint[] reserved;
	}
}
