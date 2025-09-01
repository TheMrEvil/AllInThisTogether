using System;
using UnityEngine.SocialPlatforms;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	public static class Social
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002067 File Offset: 0x00000267
		public static ISocialPlatform Active
		{
			get
			{
				return ActivePlatform.Instance;
			}
			set
			{
				ActivePlatform.Instance = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002074 File Offset: 0x00000274
		public static ILocalUser localUser
		{
			get
			{
				return Social.Active.localUser;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002090 File Offset: 0x00000290
		public static void LoadUsers(string[] userIDs, Action<IUserProfile[]> callback)
		{
			Social.Active.LoadUsers(userIDs, callback);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020A0 File Offset: 0x000002A0
		public static void ReportProgress(string achievementID, double progress, Action<bool> callback)
		{
			Social.Active.ReportProgress(achievementID, progress, callback);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020B1 File Offset: 0x000002B1
		public static void LoadAchievementDescriptions(Action<IAchievementDescription[]> callback)
		{
			Social.Active.LoadAchievementDescriptions(callback);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020C0 File Offset: 0x000002C0
		public static void LoadAchievements(Action<IAchievement[]> callback)
		{
			Social.Active.LoadAchievements(callback);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020CF File Offset: 0x000002CF
		public static void ReportScore(long score, string board, Action<bool> callback)
		{
			Social.Active.ReportScore(score, board, callback);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020E0 File Offset: 0x000002E0
		public static void LoadScores(string leaderboardID, Action<IScore[]> callback)
		{
			Social.Active.LoadScores(leaderboardID, callback);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020F0 File Offset: 0x000002F0
		public static ILeaderboard CreateLeaderboard()
		{
			return Social.Active.CreateLeaderboard();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000210C File Offset: 0x0000030C
		public static IAchievement CreateAchievement()
		{
			return Social.Active.CreateAchievement();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002128 File Offset: 0x00000328
		public static void ShowAchievementsUI()
		{
			Social.Active.ShowAchievementsUI();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002136 File Offset: 0x00000336
		public static void ShowLeaderboardUI()
		{
			Social.Active.ShowLeaderboardUI();
		}
	}
}
