using System;

namespace Steamworks
{
	// Token: 0x02000165 RID: 357
	public enum ESteamNetConnectionEnd
	{
		// Token: 0x0400092A RID: 2346
		k_ESteamNetConnectionEnd_Invalid,
		// Token: 0x0400092B RID: 2347
		k_ESteamNetConnectionEnd_App_Min = 1000,
		// Token: 0x0400092C RID: 2348
		k_ESteamNetConnectionEnd_App_Generic = 1000,
		// Token: 0x0400092D RID: 2349
		k_ESteamNetConnectionEnd_App_Max = 1999,
		// Token: 0x0400092E RID: 2350
		k_ESteamNetConnectionEnd_AppException_Min,
		// Token: 0x0400092F RID: 2351
		k_ESteamNetConnectionEnd_AppException_Generic = 2000,
		// Token: 0x04000930 RID: 2352
		k_ESteamNetConnectionEnd_AppException_Max = 2999,
		// Token: 0x04000931 RID: 2353
		k_ESteamNetConnectionEnd_Local_Min,
		// Token: 0x04000932 RID: 2354
		k_ESteamNetConnectionEnd_Local_OfflineMode,
		// Token: 0x04000933 RID: 2355
		k_ESteamNetConnectionEnd_Local_ManyRelayConnectivity,
		// Token: 0x04000934 RID: 2356
		k_ESteamNetConnectionEnd_Local_HostedServerPrimaryRelay,
		// Token: 0x04000935 RID: 2357
		k_ESteamNetConnectionEnd_Local_NetworkConfig,
		// Token: 0x04000936 RID: 2358
		k_ESteamNetConnectionEnd_Local_Rights,
		// Token: 0x04000937 RID: 2359
		k_ESteamNetConnectionEnd_Local_P2P_ICE_NoPublicAddresses,
		// Token: 0x04000938 RID: 2360
		k_ESteamNetConnectionEnd_Local_Max = 3999,
		// Token: 0x04000939 RID: 2361
		k_ESteamNetConnectionEnd_Remote_Min,
		// Token: 0x0400093A RID: 2362
		k_ESteamNetConnectionEnd_Remote_Timeout,
		// Token: 0x0400093B RID: 2363
		k_ESteamNetConnectionEnd_Remote_BadCrypt,
		// Token: 0x0400093C RID: 2364
		k_ESteamNetConnectionEnd_Remote_BadCert,
		// Token: 0x0400093D RID: 2365
		k_ESteamNetConnectionEnd_Remote_BadProtocolVersion = 4006,
		// Token: 0x0400093E RID: 2366
		k_ESteamNetConnectionEnd_Remote_P2P_ICE_NoPublicAddresses,
		// Token: 0x0400093F RID: 2367
		k_ESteamNetConnectionEnd_Remote_Max = 4999,
		// Token: 0x04000940 RID: 2368
		k_ESteamNetConnectionEnd_Misc_Min,
		// Token: 0x04000941 RID: 2369
		k_ESteamNetConnectionEnd_Misc_Generic,
		// Token: 0x04000942 RID: 2370
		k_ESteamNetConnectionEnd_Misc_InternalError,
		// Token: 0x04000943 RID: 2371
		k_ESteamNetConnectionEnd_Misc_Timeout,
		// Token: 0x04000944 RID: 2372
		k_ESteamNetConnectionEnd_Misc_SteamConnectivity = 5005,
		// Token: 0x04000945 RID: 2373
		k_ESteamNetConnectionEnd_Misc_NoRelaySessionsToClient,
		// Token: 0x04000946 RID: 2374
		k_ESteamNetConnectionEnd_Misc_P2P_Rendezvous = 5008,
		// Token: 0x04000947 RID: 2375
		k_ESteamNetConnectionEnd_Misc_P2P_NAT_Firewall,
		// Token: 0x04000948 RID: 2376
		k_ESteamNetConnectionEnd_Misc_PeerSentNoConnection,
		// Token: 0x04000949 RID: 2377
		k_ESteamNetConnectionEnd_Misc_Max = 5999,
		// Token: 0x0400094A RID: 2378
		k_ESteamNetConnectionEnd__Force32Bit = 2147483647
	}
}
