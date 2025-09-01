using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000F3 RID: 243
	[CallbackIdentity(1111)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LeaderboardUGCSet_t
	{
		// Token: 0x040002FE RID: 766
		public const int k_iCallback = 1111;

		// Token: 0x040002FF RID: 767
		public EResult m_eResult;

		// Token: 0x04000300 RID: 768
		public SteamLeaderboard_t m_hSteamLeaderboard;
	}
}
