using System;

namespace Steamworks
{
	// Token: 0x02000143 RID: 323
	public enum ESteamAPICallFailure
	{
		// Token: 0x04000769 RID: 1897
		k_ESteamAPICallFailureNone = -1,
		// Token: 0x0400076A RID: 1898
		k_ESteamAPICallFailureSteamGone,
		// Token: 0x0400076B RID: 1899
		k_ESteamAPICallFailureNetworkFailure,
		// Token: 0x0400076C RID: 1900
		k_ESteamAPICallFailureInvalidHandle,
		// Token: 0x0400076D RID: 1901
		k_ESteamAPICallFailureMismatchedCallback
	}
}
