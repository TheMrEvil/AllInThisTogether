using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x0200000B RID: 11
	public interface IAchievement
	{
		// Token: 0x06000047 RID: 71
		void ReportProgress(Action<bool> callback);

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000048 RID: 72
		// (set) Token: 0x06000049 RID: 73
		string id { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004A RID: 74
		// (set) Token: 0x0600004B RID: 75
		double percentCompleted { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004C RID: 76
		bool completed { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004D RID: 77
		bool hidden { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004E RID: 78
		DateTime lastReportedDate { get; }
	}
}
