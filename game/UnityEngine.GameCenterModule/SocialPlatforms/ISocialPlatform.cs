using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x02000007 RID: 7
	public interface ISocialPlatform
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002C RID: 44
		ILocalUser localUser { get; }

		// Token: 0x0600002D RID: 45
		void LoadUsers(string[] userIDs, Action<IUserProfile[]> callback);

		// Token: 0x0600002E RID: 46
		void ReportProgress(string achievementID, double progress, Action<bool> callback);

		// Token: 0x0600002F RID: 47
		void LoadAchievementDescriptions(Action<IAchievementDescription[]> callback);

		// Token: 0x06000030 RID: 48
		void LoadAchievements(Action<IAchievement[]> callback);

		// Token: 0x06000031 RID: 49
		IAchievement CreateAchievement();

		// Token: 0x06000032 RID: 50
		void ReportScore(long score, string board, Action<bool> callback);

		// Token: 0x06000033 RID: 51
		void LoadScores(string leaderboardID, Action<IScore[]> callback);

		// Token: 0x06000034 RID: 52
		ILeaderboard CreateLeaderboard();

		// Token: 0x06000035 RID: 53
		void ShowAchievementsUI();

		// Token: 0x06000036 RID: 54
		void ShowLeaderboardUI();

		// Token: 0x06000037 RID: 55
		void Authenticate(ILocalUser user, Action<bool> callback);

		// Token: 0x06000038 RID: 56
		void Authenticate(ILocalUser user, Action<bool, string> callback);

		// Token: 0x06000039 RID: 57
		void LoadFriends(ILocalUser user, Action<bool> callback);

		// Token: 0x0600003A RID: 58
		void LoadScores(ILeaderboard board, Action<bool> callback);

		// Token: 0x0600003B RID: 59
		bool GetLoading(ILeaderboard board);
	}
}
