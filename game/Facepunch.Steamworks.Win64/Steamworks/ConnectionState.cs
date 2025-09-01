using System;

namespace Steamworks
{
	// Token: 0x02000086 RID: 134
	public enum ConnectionState
	{
		// Token: 0x04000666 RID: 1638
		None,
		// Token: 0x04000667 RID: 1639
		Connecting,
		// Token: 0x04000668 RID: 1640
		FindingRoute,
		// Token: 0x04000669 RID: 1641
		Connected,
		// Token: 0x0400066A RID: 1642
		ClosedByPeer,
		// Token: 0x0400066B RID: 1643
		ProblemDetectedLocally,
		// Token: 0x0400066C RID: 1644
		FinWait = -1,
		// Token: 0x0400066D RID: 1645
		Linger = -2,
		// Token: 0x0400066E RID: 1646
		Dead = -3
	}
}
