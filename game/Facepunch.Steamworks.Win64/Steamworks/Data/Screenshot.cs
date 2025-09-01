using System;

namespace Steamworks.Data
{
	// Token: 0x02000203 RID: 515
	public struct Screenshot
	{
		// Token: 0x06001021 RID: 4129 RVA: 0x0001AA8C File Offset: 0x00018C8C
		public bool TagUser(SteamId user)
		{
			return SteamScreenshots.Internal.TagUser(this.Value, user);
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0001AAB0 File Offset: 0x00018CB0
		public bool SetLocation(string location)
		{
			return SteamScreenshots.Internal.SetLocation(this.Value, location);
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x0001AAD4 File Offset: 0x00018CD4
		public bool TagPublishedFile(PublishedFileId file)
		{
			return SteamScreenshots.Internal.TagPublishedFile(this.Value, file);
		}

		// Token: 0x04000C1E RID: 3102
		internal ScreenshotHandle Value;
	}
}
