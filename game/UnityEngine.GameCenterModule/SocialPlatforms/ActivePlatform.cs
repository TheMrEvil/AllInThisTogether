using System;

namespace UnityEngine.SocialPlatforms
{
	// Token: 0x02000006 RID: 6
	internal static class ActivePlatform
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002CE0 File Offset: 0x00000EE0
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002D0D File Offset: 0x00000F0D
		internal static ISocialPlatform Instance
		{
			get
			{
				bool flag = ActivePlatform._active == null;
				if (flag)
				{
					ActivePlatform._active = ActivePlatform.SelectSocialPlatform();
				}
				return ActivePlatform._active;
			}
			set
			{
				ActivePlatform._active = value;
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002D18 File Offset: 0x00000F18
		private static ISocialPlatform SelectSocialPlatform()
		{
			return new Local();
		}

		// Token: 0x0400000B RID: 11
		private static ISocialPlatform _active;
	}
}
