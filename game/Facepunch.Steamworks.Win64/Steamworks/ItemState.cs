using System;

namespace Steamworks
{
	// Token: 0x0200007D RID: 125
	internal enum ItemState
	{
		// Token: 0x04000616 RID: 1558
		None,
		// Token: 0x04000617 RID: 1559
		Subscribed,
		// Token: 0x04000618 RID: 1560
		LegacyItem,
		// Token: 0x04000619 RID: 1561
		Installed = 4,
		// Token: 0x0400061A RID: 1562
		NeedsUpdate = 8,
		// Token: 0x0400061B RID: 1563
		Downloading = 16,
		// Token: 0x0400061C RID: 1564
		DownloadPending = 32
	}
}
