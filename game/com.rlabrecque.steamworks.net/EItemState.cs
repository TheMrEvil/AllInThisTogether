using System;

namespace Steamworks
{
	// Token: 0x0200013A RID: 314
	[Flags]
	public enum EItemState
	{
		// Token: 0x04000731 RID: 1841
		k_EItemStateNone = 0,
		// Token: 0x04000732 RID: 1842
		k_EItemStateSubscribed = 1,
		// Token: 0x04000733 RID: 1843
		k_EItemStateLegacyItem = 2,
		// Token: 0x04000734 RID: 1844
		k_EItemStateInstalled = 4,
		// Token: 0x04000735 RID: 1845
		k_EItemStateNeedsUpdate = 8,
		// Token: 0x04000736 RID: 1846
		k_EItemStateDownloading = 16,
		// Token: 0x04000737 RID: 1847
		k_EItemStateDownloadPending = 32
	}
}
