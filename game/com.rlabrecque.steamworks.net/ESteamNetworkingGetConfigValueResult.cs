using System;

namespace Steamworks
{
	// Token: 0x02000169 RID: 361
	public enum ESteamNetworkingGetConfigValueResult
	{
		// Token: 0x04000998 RID: 2456
		k_ESteamNetworkingGetConfigValue_BadValue = -1,
		// Token: 0x04000999 RID: 2457
		k_ESteamNetworkingGetConfigValue_BadScopeObj = -2,
		// Token: 0x0400099A RID: 2458
		k_ESteamNetworkingGetConfigValue_BufferTooSmall = -3,
		// Token: 0x0400099B RID: 2459
		k_ESteamNetworkingGetConfigValue_OK = 1,
		// Token: 0x0400099C RID: 2460
		k_ESteamNetworkingGetConfigValue_OKInherited,
		// Token: 0x0400099D RID: 2461
		k_ESteamNetworkingGetConfigValueResult__Force32Bit = 2147483647
	}
}
