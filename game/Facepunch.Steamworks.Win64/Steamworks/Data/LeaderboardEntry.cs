using System;
using System.Linq;

namespace Steamworks.Data
{
	// Token: 0x020001FB RID: 507
	public struct LeaderboardEntry
	{
		// Token: 0x06000FDA RID: 4058 RVA: 0x00019F38 File Offset: 0x00018138
		internal static LeaderboardEntry From(LeaderboardEntry_t e, int[] detailsBuffer)
		{
			LeaderboardEntry result = new LeaderboardEntry
			{
				User = new Friend(e.SteamIDUser),
				GlobalRank = e.GlobalRank,
				Score = e.Score,
				Details = null
			};
			bool flag = e.CDetails > 0;
			if (flag)
			{
				result.Details = detailsBuffer.Take(e.CDetails).ToArray<int>();
			}
			return result;
		}

		// Token: 0x04000C05 RID: 3077
		public Friend User;

		// Token: 0x04000C06 RID: 3078
		public int GlobalRank;

		// Token: 0x04000C07 RID: 3079
		public int Score;

		// Token: 0x04000C08 RID: 3080
		public int[] Details;
	}
}
