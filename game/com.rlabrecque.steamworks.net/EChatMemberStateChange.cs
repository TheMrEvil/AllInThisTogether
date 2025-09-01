using System;

namespace Steamworks
{
	// Token: 0x0200011F RID: 287
	[Flags]
	public enum EChatMemberStateChange
	{
		// Token: 0x04000660 RID: 1632
		k_EChatMemberStateChangeEntered = 1,
		// Token: 0x04000661 RID: 1633
		k_EChatMemberStateChangeLeft = 2,
		// Token: 0x04000662 RID: 1634
		k_EChatMemberStateChangeDisconnected = 4,
		// Token: 0x04000663 RID: 1635
		k_EChatMemberStateChangeKicked = 8,
		// Token: 0x04000664 RID: 1636
		k_EChatMemberStateChangeBanned = 16
	}
}
