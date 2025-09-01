using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020001AD RID: 429
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LeaderboardEntry_t
	{
		// Token: 0x04000B60 RID: 2912
		internal ulong SteamIDUser;

		// Token: 0x04000B61 RID: 2913
		internal int GlobalRank;

		// Token: 0x04000B62 RID: 2914
		internal int Score;

		// Token: 0x04000B63 RID: 2915
		internal int CDetails;

		// Token: 0x04000B64 RID: 2916
		internal ulong UGC;
	}
}
