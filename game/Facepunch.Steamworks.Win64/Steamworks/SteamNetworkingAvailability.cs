using System;

namespace Steamworks
{
	// Token: 0x02000084 RID: 132
	public enum SteamNetworkingAvailability
	{
		// Token: 0x04000652 RID: 1618
		CannotTry = -102,
		// Token: 0x04000653 RID: 1619
		Failed,
		// Token: 0x04000654 RID: 1620
		Previously,
		// Token: 0x04000655 RID: 1621
		Retrying = -10,
		// Token: 0x04000656 RID: 1622
		NeverTried = 1,
		// Token: 0x04000657 RID: 1623
		Waiting,
		// Token: 0x04000658 RID: 1624
		Attempting,
		// Token: 0x04000659 RID: 1625
		Current = 100,
		// Token: 0x0400065A RID: 1626
		Unknown = 0,
		// Token: 0x0400065B RID: 1627
		Force32bit = 2147483647
	}
}
