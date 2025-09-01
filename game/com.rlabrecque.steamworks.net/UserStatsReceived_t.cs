using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E9 RID: 233
	[CallbackIdentity(1101)]
	[StructLayout(LayoutKind.Explicit, Pack = 8)]
	public struct UserStatsReceived_t
	{
		// Token: 0x040002D6 RID: 726
		public const int k_iCallback = 1101;

		// Token: 0x040002D7 RID: 727
		[FieldOffset(0)]
		public ulong m_nGameID;

		// Token: 0x040002D8 RID: 728
		[FieldOffset(8)]
		public EResult m_eResult;

		// Token: 0x040002D9 RID: 729
		[FieldOffset(12)]
		public CSteamID m_steamIDUser;
	}
}
