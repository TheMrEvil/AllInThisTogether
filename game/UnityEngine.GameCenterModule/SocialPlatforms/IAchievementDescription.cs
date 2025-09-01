using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x0200000C RID: 12
	public interface IAchievementDescription
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004F RID: 79
		// (set) Token: 0x06000050 RID: 80
		string id { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000051 RID: 81
		string title { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000052 RID: 82
		Texture2D image { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000053 RID: 83
		string achievedDescription { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000054 RID: 84
		string unachievedDescription { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000055 RID: 85
		bool hidden { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000056 RID: 86
		int points { get; }
	}
}
