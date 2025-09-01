using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000EC RID: 236
	[CallbackIdentity(1104)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LeaderboardFindResult_t
	{
		// Token: 0x040002E3 RID: 739
		public const int k_iCallback = 1104;

		// Token: 0x040002E4 RID: 740
		public SteamLeaderboard_t m_hSteamLeaderboard;

		// Token: 0x040002E5 RID: 741
		public byte m_bLeaderboardFound;
	}
}
