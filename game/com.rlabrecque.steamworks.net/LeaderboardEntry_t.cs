using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000177 RID: 375
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LeaderboardEntry_t
	{
		// Token: 0x04000A04 RID: 2564
		public CSteamID m_steamIDUser;

		// Token: 0x04000A05 RID: 2565
		public int m_nGlobalRank;

		// Token: 0x04000A06 RID: 2566
		public int m_nScore;

		// Token: 0x04000A07 RID: 2567
		public int m_cDetails;

		// Token: 0x04000A08 RID: 2568
		public UGCHandle_t m_hUGC;
	}
}
