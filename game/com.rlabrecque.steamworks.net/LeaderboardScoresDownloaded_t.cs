using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000ED RID: 237
	[CallbackIdentity(1105)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LeaderboardScoresDownloaded_t
	{
		// Token: 0x040002E6 RID: 742
		public const int k_iCallback = 1105;

		// Token: 0x040002E7 RID: 743
		public SteamLeaderboard_t m_hSteamLeaderboard;

		// Token: 0x040002E8 RID: 744
		public SteamLeaderboardEntries_t m_hSteamLeaderboardEntries;

		// Token: 0x040002E9 RID: 745
		public int m_cEntryCount;
	}
}
