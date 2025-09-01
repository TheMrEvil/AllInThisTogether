using System;

namespace Steamworks
{
	// Token: 0x0200012A RID: 298
	[Flags]
	public enum ERemoteStoragePlatform
	{
		// Token: 0x040006AC RID: 1708
		k_ERemoteStoragePlatformNone = 0,
		// Token: 0x040006AD RID: 1709
		k_ERemoteStoragePlatformWindows = 1,
		// Token: 0x040006AE RID: 1710
		k_ERemoteStoragePlatformOSX = 2,
		// Token: 0x040006AF RID: 1711
		k_ERemoteStoragePlatformPS3 = 4,
		// Token: 0x040006B0 RID: 1712
		k_ERemoteStoragePlatformLinux = 8,
		// Token: 0x040006B1 RID: 1713
		k_ERemoteStoragePlatformSwitch = 16,
		// Token: 0x040006B2 RID: 1714
		k_ERemoteStoragePlatformAndroid = 32,
		// Token: 0x040006B3 RID: 1715
		k_ERemoteStoragePlatformIOS = 64,
		// Token: 0x040006B4 RID: 1716
		k_ERemoteStoragePlatformAll = -1
	}
}
