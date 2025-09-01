using System;

namespace Steamworks
{
	// Token: 0x02000164 RID: 356
	public enum ESteamNetworkingConnectionState
	{
		// Token: 0x0400091F RID: 2335
		k_ESteamNetworkingConnectionState_None,
		// Token: 0x04000920 RID: 2336
		k_ESteamNetworkingConnectionState_Connecting,
		// Token: 0x04000921 RID: 2337
		k_ESteamNetworkingConnectionState_FindingRoute,
		// Token: 0x04000922 RID: 2338
		k_ESteamNetworkingConnectionState_Connected,
		// Token: 0x04000923 RID: 2339
		k_ESteamNetworkingConnectionState_ClosedByPeer,
		// Token: 0x04000924 RID: 2340
		k_ESteamNetworkingConnectionState_ProblemDetectedLocally,
		// Token: 0x04000925 RID: 2341
		k_ESteamNetworkingConnectionState_FinWait = -1,
		// Token: 0x04000926 RID: 2342
		k_ESteamNetworkingConnectionState_Linger = -2,
		// Token: 0x04000927 RID: 2343
		k_ESteamNetworkingConnectionState_Dead = -3,
		// Token: 0x04000928 RID: 2344
		k_ESteamNetworkingConnectionState__Force32Bit = 2147483647
	}
}
