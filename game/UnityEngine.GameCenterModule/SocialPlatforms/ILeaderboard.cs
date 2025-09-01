using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x02000011 RID: 17
	public interface ILeaderboard
	{
		// Token: 0x06000061 RID: 97
		void SetUserFilter(string[] userIDs);

		// Token: 0x06000062 RID: 98
		void LoadScores(Action<bool> callback);

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000063 RID: 99
		bool loading { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000064 RID: 100
		// (set) Token: 0x06000065 RID: 101
		string id { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000066 RID: 102
		// (set) Token: 0x06000067 RID: 103
		UserScope userScope { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000068 RID: 104
		// (set) Token: 0x06000069 RID: 105
		Range range { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600006A RID: 106
		// (set) Token: 0x0600006B RID: 107
		TimeScope timeScope { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600006C RID: 108
		IScore localUserScore { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600006D RID: 109
		uint maxRange { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600006E RID: 110
		IScore[] scores { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600006F RID: 111
		string title { get; }
	}
}
