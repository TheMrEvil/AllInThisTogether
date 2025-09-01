using System;

namespace Steamworks
{
	// Token: 0x02000162 RID: 354
	public enum ESteamNetworkingIdentityType
	{
		// Token: 0x0400090E RID: 2318
		k_ESteamNetworkingIdentityType_Invalid,
		// Token: 0x0400090F RID: 2319
		k_ESteamNetworkingIdentityType_SteamID = 16,
		// Token: 0x04000910 RID: 2320
		k_ESteamNetworkingIdentityType_XboxPairwiseID,
		// Token: 0x04000911 RID: 2321
		k_ESteamNetworkingIdentityType_SonyPSN,
		// Token: 0x04000912 RID: 2322
		k_ESteamNetworkingIdentityType_GoogleStadia,
		// Token: 0x04000913 RID: 2323
		k_ESteamNetworkingIdentityType_IPAddress = 1,
		// Token: 0x04000914 RID: 2324
		k_ESteamNetworkingIdentityType_GenericString,
		// Token: 0x04000915 RID: 2325
		k_ESteamNetworkingIdentityType_GenericBytes,
		// Token: 0x04000916 RID: 2326
		k_ESteamNetworkingIdentityType_UnknownType,
		// Token: 0x04000917 RID: 2327
		k_ESteamNetworkingIdentityType__Force32bit = 2147483647
	}
}
