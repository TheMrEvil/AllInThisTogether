using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200007E RID: 126
	[CallbackIdentity(509)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyGameCreated_t
	{
		// Token: 0x04000157 RID: 343
		public const int k_iCallback = 509;

		// Token: 0x04000158 RID: 344
		public ulong m_ulSteamIDLobby;

		// Token: 0x04000159 RID: 345
		public ulong m_ulSteamIDGameServer;

		// Token: 0x0400015A RID: 346
		public uint m_unIP;

		// Token: 0x0400015B RID: 347
		public ushort m_usPort;
	}
}
