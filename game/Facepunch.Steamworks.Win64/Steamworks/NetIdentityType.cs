using System;

namespace Steamworks
{
	// Token: 0x02000085 RID: 133
	internal enum NetIdentityType
	{
		// Token: 0x0400065D RID: 1629
		Invalid,
		// Token: 0x0400065E RID: 1630
		SteamID = 16,
		// Token: 0x0400065F RID: 1631
		XboxPairwiseID,
		// Token: 0x04000660 RID: 1632
		IPAddress = 1,
		// Token: 0x04000661 RID: 1633
		GenericString,
		// Token: 0x04000662 RID: 1634
		GenericBytes,
		// Token: 0x04000663 RID: 1635
		UnknownType,
		// Token: 0x04000664 RID: 1636
		Force32bit = 2147483647
	}
}
