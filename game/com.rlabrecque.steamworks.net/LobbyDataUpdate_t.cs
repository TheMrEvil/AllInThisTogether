using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200007B RID: 123
	[CallbackIdentity(505)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyDataUpdate_t
	{
		// Token: 0x04000149 RID: 329
		public const int k_iCallback = 505;

		// Token: 0x0400014A RID: 330
		public ulong m_ulSteamIDLobby;

		// Token: 0x0400014B RID: 331
		public ulong m_ulSteamIDMember;

		// Token: 0x0400014C RID: 332
		public byte m_bSuccess;
	}
}
