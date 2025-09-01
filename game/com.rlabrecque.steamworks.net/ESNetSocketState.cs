using System;

namespace Steamworks
{
	// Token: 0x02000126 RID: 294
	public enum ESNetSocketState
	{
		// Token: 0x04000685 RID: 1669
		k_ESNetSocketStateInvalid,
		// Token: 0x04000686 RID: 1670
		k_ESNetSocketStateConnected,
		// Token: 0x04000687 RID: 1671
		k_ESNetSocketStateInitiated = 10,
		// Token: 0x04000688 RID: 1672
		k_ESNetSocketStateLocalCandidatesFound,
		// Token: 0x04000689 RID: 1673
		k_ESNetSocketStateReceivedRemoteCandidates,
		// Token: 0x0400068A RID: 1674
		k_ESNetSocketStateChallengeHandshake = 15,
		// Token: 0x0400068B RID: 1675
		k_ESNetSocketStateDisconnecting = 21,
		// Token: 0x0400068C RID: 1676
		k_ESNetSocketStateLocalDisconnect,
		// Token: 0x0400068D RID: 1677
		k_ESNetSocketStateTimeoutDuringConnect,
		// Token: 0x0400068E RID: 1678
		k_ESNetSocketStateRemoteEndDisconnected,
		// Token: 0x0400068F RID: 1679
		k_ESNetSocketStateConnectionBroken
	}
}
