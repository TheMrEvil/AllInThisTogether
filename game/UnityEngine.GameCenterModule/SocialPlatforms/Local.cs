using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.SocialPlatforms.Impl;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x02000003 RID: 3
	public class Local : ISocialPlatform
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002144 File Offset: 0x00000344
		public ILocalUser localUser
		{
			get
			{
				bool flag = Local.m_LocalUser == null;
				if (flag)
				{
					Local.m_LocalUser = new LocalUser();
				}
				return Local.m_LocalUser;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002174 File Offset: 0x00000374
		void ISocialPlatform.Authenticate(ILocalUser user, Action<bool> callback)
		{
			LocalUser localUser = (LocalUser)user;
			this.m_DefaultTexture = this.CreateDummyTexture(32, 32);
			this.PopulateStaticData();
			localUser.SetAuthenticated(true);
			localUser.SetUnderage(false);
			localUser.SetUserID("1000");
			localUser.SetUserName("Lerpz");
			localUser.SetImage(this.m_DefaultTexture);
			bool flag = callback != null;
			if (flag)
			{
				callback(true);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021E8 File Offset: 0x000003E8
		void ISocialPlatform.Authenticate(ILocalUser user, Action<bool, string> callback)
		{
			((ISocialPlatform)this).Authenticate(user, delegate(bool success)
			{
				callback(success, null);
			});
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002218 File Offset: 0x00000418
		void ISocialPlatform.LoadFriends(ILocalUser user, Action<bool> callback)
		{
			bool flag = !this.VerifyUser();
			if (!flag)
			{
				LocalUser localUser = (LocalUser)user;
				IUserProfile[] friends = this.m_Friends.ToArray();
				localUser.SetFriends(friends);
				bool flag2 = callback != null;
				if (flag2)
				{
					callback(true);
				}
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002260 File Offset: 0x00000460
		public void LoadUsers(string[] userIDs, Action<IUserProfile[]> callback)
		{
			List<UserProfile> list = new List<UserProfile>();
			bool flag = !this.VerifyUser();
			if (!flag)
			{
				foreach (string b in userIDs)
				{
					foreach (UserProfile userProfile in this.m_Users)
					{
						bool flag2 = userProfile.id == b;
						if (flag2)
						{
							list.Add(userProfile);
						}
					}
					foreach (UserProfile userProfile2 in this.m_Friends)
					{
						bool flag3 = userProfile2.id == b;
						if (flag3)
						{
							list.Add(userProfile2);
						}
					}
				}
				IUserProfile[] obj = list.ToArray();
				callback(obj);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002374 File Offset: 0x00000574
		public void ReportProgress(string id, double progress, Action<bool> callback)
		{
			bool flag = !this.VerifyUser();
			if (!flag)
			{
				foreach (Achievement achievement in this.m_Achievements)
				{
					bool flag2 = achievement.id == id && achievement.percentCompleted <= progress;
					if (flag2)
					{
						bool flag3 = progress >= 100.0;
						if (flag3)
						{
							achievement.SetCompleted(true);
						}
						achievement.SetHidden(false);
						achievement.SetLastReportedDate(DateTime.Now);
						achievement.percentCompleted = progress;
						bool flag4 = callback != null;
						if (flag4)
						{
							callback(true);
						}
						return;
					}
				}
				foreach (AchievementDescription achievementDescription in this.m_AchievementDescriptions)
				{
					bool flag5 = achievementDescription.id == id;
					if (flag5)
					{
						bool completed = progress >= 100.0;
						Achievement item = new Achievement(id, progress, completed, false, DateTime.Now);
						this.m_Achievements.Add(item);
						bool flag6 = callback != null;
						if (flag6)
						{
							callback(true);
						}
						return;
					}
				}
				Debug.LogError("Achievement ID not found");
				bool flag7 = callback != null;
				if (flag7)
				{
					callback(false);
				}
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002504 File Offset: 0x00000704
		public void LoadAchievementDescriptions(Action<IAchievementDescription[]> callback)
		{
			bool flag = !this.VerifyUser();
			if (!flag)
			{
				bool flag2 = callback != null;
				if (flag2)
				{
					IAchievementDescription[] obj = this.m_AchievementDescriptions.ToArray();
					callback(obj);
				}
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002540 File Offset: 0x00000740
		public void LoadAchievements(Action<IAchievement[]> callback)
		{
			bool flag = !this.VerifyUser();
			if (!flag)
			{
				bool flag2 = callback != null;
				if (flag2)
				{
					IAchievement[] obj = this.m_Achievements.ToArray();
					callback(obj);
				}
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000257C File Offset: 0x0000077C
		public void ReportScore(long score, string board, Action<bool> callback)
		{
			bool flag = !this.VerifyUser();
			if (!flag)
			{
				foreach (Leaderboard leaderboard in this.m_Leaderboards)
				{
					bool flag2 = leaderboard.id == board;
					if (flag2)
					{
						List<Score> list = new List<Score>((Score[])leaderboard.scores);
						list.Add(new Score(board, score, this.localUser.id, DateTime.Now, score.ToString() + " points", 0));
						Leaderboard leaderboard2 = leaderboard;
						IScore[] scores = list.ToArray();
						leaderboard2.SetScores(scores);
						bool flag3 = callback != null;
						if (flag3)
						{
							callback(true);
						}
						return;
					}
				}
				Debug.LogError("Leaderboard not found");
				bool flag4 = callback != null;
				if (flag4)
				{
					callback(false);
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000267C File Offset: 0x0000087C
		public void LoadScores(string leaderboardID, Action<IScore[]> callback)
		{
			bool flag = !this.VerifyUser();
			if (!flag)
			{
				foreach (Leaderboard leaderboard in this.m_Leaderboards)
				{
					bool flag2 = leaderboard.id == leaderboardID;
					if (flag2)
					{
						this.SortScores(leaderboard);
						bool flag3 = callback != null;
						if (flag3)
						{
							callback(leaderboard.scores);
						}
						return;
					}
				}
				Debug.LogError("Leaderboard not found");
				bool flag4 = callback != null;
				if (flag4)
				{
					IScore[] obj = new Score[0];
					callback(obj);
				}
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002738 File Offset: 0x00000938
		void ISocialPlatform.LoadScores(ILeaderboard board, Action<bool> callback)
		{
			bool flag = !this.VerifyUser();
			if (!flag)
			{
				Leaderboard leaderboard = (Leaderboard)board;
				foreach (Leaderboard leaderboard2 in this.m_Leaderboards)
				{
					bool flag2 = leaderboard2.id == leaderboard.id;
					if (flag2)
					{
						leaderboard.SetTitle(leaderboard2.title);
						leaderboard.SetScores(leaderboard2.scores);
						leaderboard.SetMaxRange((uint)leaderboard2.scores.Length);
					}
				}
				this.SortScores(leaderboard);
				this.SetLocalPlayerScore(leaderboard);
				bool flag3 = callback != null;
				if (flag3)
				{
					callback(true);
				}
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002808 File Offset: 0x00000A08
		bool ISocialPlatform.GetLoading(ILeaderboard board)
		{
			bool flag = !this.VerifyUser();
			return !flag && ((Leaderboard)board).loading;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002838 File Offset: 0x00000A38
		private void SortScores(Leaderboard board)
		{
			List<Score> list = new List<Score>((Score[])board.scores);
			list.Sort((Score s1, Score s2) => s2.value.CompareTo(s1.value));
			for (int i = 0; i < list.Count; i++)
			{
				list[i].SetRank(i + 1);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000028A4 File Offset: 0x00000AA4
		private void SetLocalPlayerScore(Leaderboard board)
		{
			foreach (Score score in board.scores)
			{
				bool flag = score.userID == this.localUser.id;
				if (flag)
				{
					board.SetLocalUserScore(score);
					break;
				}
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000028F8 File Offset: 0x00000AF8
		public void ShowAchievementsUI()
		{
			Debug.Log("ShowAchievementsUI not implemented");
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002906 File Offset: 0x00000B06
		public void ShowLeaderboardUI()
		{
			Debug.Log("ShowLeaderboardUI not implemented");
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002914 File Offset: 0x00000B14
		public ILeaderboard CreateLeaderboard()
		{
			return new Leaderboard();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002930 File Offset: 0x00000B30
		public IAchievement CreateAchievement()
		{
			return new Achievement();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000294C File Offset: 0x00000B4C
		private bool VerifyUser()
		{
			bool flag = !this.localUser.authenticated;
			bool result;
			if (flag)
			{
				Debug.LogError("Must authenticate first");
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002984 File Offset: 0x00000B84
		private void PopulateStaticData()
		{
			this.m_Friends.Add(new UserProfile("Fred", "1001", true, UserState.Online, this.m_DefaultTexture));
			this.m_Friends.Add(new UserProfile("Julia", "1002", true, UserState.Online, this.m_DefaultTexture));
			this.m_Friends.Add(new UserProfile("Jeff", "1003", true, UserState.Online, this.m_DefaultTexture));
			this.m_Users.Add(new UserProfile("Sam", "1004", false, UserState.Offline, this.m_DefaultTexture));
			this.m_Users.Add(new UserProfile("Max", "1005", false, UserState.Offline, this.m_DefaultTexture));
			this.m_AchievementDescriptions.Add(new AchievementDescription("Achievement01", "First achievement", this.m_DefaultTexture, "Get first achievement", "Received first achievement", false, 10));
			this.m_AchievementDescriptions.Add(new AchievementDescription("Achievement02", "Second achievement", this.m_DefaultTexture, "Get second achievement", "Received second achievement", false, 20));
			this.m_AchievementDescriptions.Add(new AchievementDescription("Achievement03", "Third achievement", this.m_DefaultTexture, "Get third achievement", "Received third achievement", false, 15));
			Leaderboard leaderboard = new Leaderboard();
			leaderboard.SetTitle("High Scores");
			leaderboard.id = "Leaderboard01";
			List<Score> list = new List<Score>();
			list.Add(new Score("Leaderboard01", 300L, "1001", DateTime.Now.AddDays(-1.0), "300 points", 1));
			list.Add(new Score("Leaderboard01", 255L, "1002", DateTime.Now.AddDays(-1.0), "255 points", 2));
			list.Add(new Score("Leaderboard01", 55L, "1003", DateTime.Now.AddDays(-1.0), "55 points", 3));
			list.Add(new Score("Leaderboard01", 10L, "1004", DateTime.Now.AddDays(-1.0), "10 points", 4));
			Leaderboard leaderboard2 = leaderboard;
			IScore[] scores = list.ToArray();
			leaderboard2.SetScores(scores);
			this.m_Leaderboards.Add(leaderboard);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002BE8 File Offset: 0x00000DE8
		private Texture2D CreateDummyTexture(int width, int height)
		{
			Texture2D texture2D = new Texture2D(width, height);
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					Color color = ((j & i) > 0) ? Color.white : Color.gray;
					texture2D.SetPixel(j, i, color);
				}
			}
			texture2D.Apply();
			return texture2D;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002C51 File Offset: 0x00000E51
		public Local()
		{
		}

		// Token: 0x04000001 RID: 1
		private static LocalUser m_LocalUser;

		// Token: 0x04000002 RID: 2
		private List<UserProfile> m_Friends = new List<UserProfile>();

		// Token: 0x04000003 RID: 3
		private List<UserProfile> m_Users = new List<UserProfile>();

		// Token: 0x04000004 RID: 4
		private List<AchievementDescription> m_AchievementDescriptions = new List<AchievementDescription>();

		// Token: 0x04000005 RID: 5
		private List<Achievement> m_Achievements = new List<Achievement>();

		// Token: 0x04000006 RID: 6
		private List<Leaderboard> m_Leaderboards = new List<Leaderboard>();

		// Token: 0x04000007 RID: 7
		private Texture2D m_DefaultTexture;

		// Token: 0x02000004 RID: 4
		[CompilerGenerated]
		private sealed class <>c__DisplayClass10_0
		{
			// Token: 0x06000024 RID: 36 RVA: 0x00002C91 File Offset: 0x00000E91
			public <>c__DisplayClass10_0()
			{
			}

			// Token: 0x06000025 RID: 37 RVA: 0x00002C9A File Offset: 0x00000E9A
			internal void <UnityEngine.SocialPlatforms.ISocialPlatform.Authenticate>b__0(bool success)
			{
				this.callback(success, null);
			}

			// Token: 0x04000008 RID: 8
			public Action<bool, string> callback;
		}

		// Token: 0x02000005 RID: 5
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000026 RID: 38 RVA: 0x00002CAA File Offset: 0x00000EAA
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000027 RID: 39 RVA: 0x00002C91 File Offset: 0x00000E91
			public <>c()
			{
			}

			// Token: 0x06000028 RID: 40 RVA: 0x00002CB8 File Offset: 0x00000EB8
			internal int <SortScores>b__20_0(Score s1, Score s2)
			{
				return s2.value.CompareTo(s1.value);
			}

			// Token: 0x04000009 RID: 9
			public static readonly Local.<>c <>9 = new Local.<>c();

			// Token: 0x0400000A RID: 10
			public static Comparison<Score> <>9__20_0;
		}
	}
}
