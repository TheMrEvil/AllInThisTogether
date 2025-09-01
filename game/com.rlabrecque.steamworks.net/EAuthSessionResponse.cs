using System;

namespace Steamworks
{
	// Token: 0x0200014F RID: 335
	public enum EAuthSessionResponse
	{
		// Token: 0x04000831 RID: 2097
		k_EAuthSessionResponseOK,
		// Token: 0x04000832 RID: 2098
		k_EAuthSessionResponseUserNotConnectedToSteam,
		// Token: 0x04000833 RID: 2099
		k_EAuthSessionResponseNoLicenseOrExpired,
		// Token: 0x04000834 RID: 2100
		k_EAuthSessionResponseVACBanned,
		// Token: 0x04000835 RID: 2101
		k_EAuthSessionResponseLoggedInElseWhere,
		// Token: 0x04000836 RID: 2102
		k_EAuthSessionResponseVACCheckTimedOut,
		// Token: 0x04000837 RID: 2103
		k_EAuthSessionResponseAuthTicketCanceled,
		// Token: 0x04000838 RID: 2104
		k_EAuthSessionResponseAuthTicketInvalidAlreadyUsed,
		// Token: 0x04000839 RID: 2105
		k_EAuthSessionResponseAuthTicketInvalid,
		// Token: 0x0400083A RID: 2106
		k_EAuthSessionResponsePublisherIssuedBan,
		// Token: 0x0400083B RID: 2107
		k_EAuthSessionResponseAuthTicketNetworkIdentityFailure
	}
}
