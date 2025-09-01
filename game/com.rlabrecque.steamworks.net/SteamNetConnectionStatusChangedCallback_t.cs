using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000A5 RID: 165
	[CallbackIdentity(1221)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamNetConnectionStatusChangedCallback_t
	{
		// Token: 0x040001C2 RID: 450
		public const int k_iCallback = 1221;

		// Token: 0x040001C3 RID: 451
		public HSteamNetConnection m_hConn;

		// Token: 0x040001C4 RID: 452
		public SteamNetConnectionInfo_t m_info;

		// Token: 0x040001C5 RID: 453
		public ESteamNetworkingConnectionState m_eOldState;
	}
}
