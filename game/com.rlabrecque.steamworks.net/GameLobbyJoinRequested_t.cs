using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000032 RID: 50
	[CallbackIdentity(333)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameLobbyJoinRequested_t
	{
		// Token: 0x04000025 RID: 37
		public const int k_iCallback = 333;

		// Token: 0x04000026 RID: 38
		public CSteamID m_steamIDLobby;

		// Token: 0x04000027 RID: 39
		public CSteamID m_steamIDFriend;
	}
}
