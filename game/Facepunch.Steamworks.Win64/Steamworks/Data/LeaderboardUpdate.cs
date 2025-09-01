using System;

namespace Steamworks.Data
{
	// Token: 0x020001FC RID: 508
	public struct LeaderboardUpdate
	{
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x00019FB5 File Offset: 0x000181B5
		public int RankChange
		{
			get
			{
				return this.NewGlobalRank - this.OldGlobalRank;
			}
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x00019FC4 File Offset: 0x000181C4
		internal static LeaderboardUpdate From(LeaderboardScoreUploaded_t e)
		{
			return new LeaderboardUpdate
			{
				Score = e.Score,
				Changed = (e.ScoreChanged == 1),
				NewGlobalRank = e.GlobalRankNew,
				OldGlobalRank = e.GlobalRankPrevious
			};
		}

		// Token: 0x04000C09 RID: 3081
		public int Score;

		// Token: 0x04000C0A RID: 3082
		public bool Changed;

		// Token: 0x04000C0B RID: 3083
		public int NewGlobalRank;

		// Token: 0x04000C0C RID: 3084
		public int OldGlobalRank;
	}
}
