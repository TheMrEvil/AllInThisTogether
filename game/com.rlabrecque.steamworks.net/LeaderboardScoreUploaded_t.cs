using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000EE RID: 238
	[CallbackIdentity(1106)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LeaderboardScoreUploaded_t
	{
		// Token: 0x040002EA RID: 746
		public const int k_iCallback = 1106;

		// Token: 0x040002EB RID: 747
		public byte m_bSuccess;

		// Token: 0x040002EC RID: 748
		public SteamLeaderboard_t m_hSteamLeaderboard;

		// Token: 0x040002ED RID: 749
		public int m_nScore;

		// Token: 0x040002EE RID: 750
		public byte m_bScoreChanged;

		// Token: 0x040002EF RID: 751
		public int m_nGlobalRankNew;

		// Token: 0x040002F0 RID: 752
		public int m_nGlobalRankPrevious;
	}
}
