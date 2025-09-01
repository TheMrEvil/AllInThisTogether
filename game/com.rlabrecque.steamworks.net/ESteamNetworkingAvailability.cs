using System;

namespace Steamworks
{
	// Token: 0x02000161 RID: 353
	public enum ESteamNetworkingAvailability
	{
		// Token: 0x04000903 RID: 2307
		k_ESteamNetworkingAvailability_CannotTry = -102,
		// Token: 0x04000904 RID: 2308
		k_ESteamNetworkingAvailability_Failed,
		// Token: 0x04000905 RID: 2309
		k_ESteamNetworkingAvailability_Previously,
		// Token: 0x04000906 RID: 2310
		k_ESteamNetworkingAvailability_Retrying = -10,
		// Token: 0x04000907 RID: 2311
		k_ESteamNetworkingAvailability_NeverTried = 1,
		// Token: 0x04000908 RID: 2312
		k_ESteamNetworkingAvailability_Waiting,
		// Token: 0x04000909 RID: 2313
		k_ESteamNetworkingAvailability_Attempting,
		// Token: 0x0400090A RID: 2314
		k_ESteamNetworkingAvailability_Current = 100,
		// Token: 0x0400090B RID: 2315
		k_ESteamNetworkingAvailability_Unknown = 0,
		// Token: 0x0400090C RID: 2316
		k_ESteamNetworkingAvailability__Force32bit = 2147483647
	}
}
