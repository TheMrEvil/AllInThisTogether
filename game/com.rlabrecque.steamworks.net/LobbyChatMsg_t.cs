using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200007D RID: 125
	[CallbackIdentity(507)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyChatMsg_t
	{
		// Token: 0x04000152 RID: 338
		public const int k_iCallback = 507;

		// Token: 0x04000153 RID: 339
		public ulong m_ulSteamIDLobby;

		// Token: 0x04000154 RID: 340
		public ulong m_ulSteamIDUser;

		// Token: 0x04000155 RID: 341
		public byte m_eChatEntryType;

		// Token: 0x04000156 RID: 342
		public uint m_iChatID;
	}
}
