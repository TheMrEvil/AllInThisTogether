using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x0200000A RID: 10
	public interface IUserProfile
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000042 RID: 66
		string userName { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000043 RID: 67
		string id { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000044 RID: 68
		bool isFriend { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000045 RID: 69
		UserState state { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000046 RID: 70
		Texture2D image { get; }
	}
}
