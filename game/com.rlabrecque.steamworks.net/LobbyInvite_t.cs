using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000079 RID: 121
	[CallbackIdentity(503)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyInvite_t
	{
		// Token: 0x04000140 RID: 320
		public const int k_iCallback = 503;

		// Token: 0x04000141 RID: 321
		public ulong m_ulSteamIDUser;

		// Token: 0x04000142 RID: 322
		public ulong m_ulSteamIDLobby;

		// Token: 0x04000143 RID: 323
		public ulong m_ulGameID;
	}
}
