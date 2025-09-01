using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000080 RID: 128
	[CallbackIdentity(512)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyKicked_t
	{
		// Token: 0x0400015E RID: 350
		public const int k_iCallback = 512;

		// Token: 0x0400015F RID: 351
		public ulong m_ulSteamIDLobby;

		// Token: 0x04000160 RID: 352
		public ulong m_ulSteamIDAdmin;

		// Token: 0x04000161 RID: 353
		public byte m_bKickedDueToDisconnect;
	}
}
