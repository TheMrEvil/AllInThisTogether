using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200016D RID: 365
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct FriendGameInfo_t
	{
		// Token: 0x040009B3 RID: 2483
		public CGameID m_gameID;

		// Token: 0x040009B4 RID: 2484
		public uint m_unGameIP;

		// Token: 0x040009B5 RID: 2485
		public ushort m_usGamePort;

		// Token: 0x040009B6 RID: 2486
		public ushort m_usQueryPort;

		// Token: 0x040009B7 RID: 2487
		public CSteamID m_steamIDLobby;
	}
}
