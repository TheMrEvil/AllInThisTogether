using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000A3 RID: 163
	[CallbackIdentity(1251)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamNetworkingMessagesSessionRequest_t
	{
		// Token: 0x040001BE RID: 446
		public const int k_iCallback = 1251;

		// Token: 0x040001BF RID: 447
		public SteamNetworkingIdentity m_identityRemote;
	}
}
