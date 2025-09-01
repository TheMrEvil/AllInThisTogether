using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000A9 RID: 169
	[CallbackIdentity(5701)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamRemotePlaySessionConnected_t
	{
		// Token: 0x040001D0 RID: 464
		public const int k_iCallback = 5701;

		// Token: 0x040001D1 RID: 465
		public RemotePlaySessionID_t m_unSessionID;
	}
}
