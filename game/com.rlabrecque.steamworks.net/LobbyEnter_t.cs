using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200007A RID: 122
	[CallbackIdentity(504)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyEnter_t
	{
		// Token: 0x04000144 RID: 324
		public const int k_iCallback = 504;

		// Token: 0x04000145 RID: 325
		public ulong m_ulSteamIDLobby;

		// Token: 0x04000146 RID: 326
		public uint m_rgfChatPermissions;

		// Token: 0x04000147 RID: 327
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bLocked;

		// Token: 0x04000148 RID: 328
		public uint m_EChatRoomEnterResponse;
	}
}
