using System;

namespace Steamworks
{
	// Token: 0x02000037 RID: 55
	public enum AuthResponse
	{
		// Token: 0x0400017D RID: 381
		OK,
		// Token: 0x0400017E RID: 382
		UserNotConnectedToSteam,
		// Token: 0x0400017F RID: 383
		NoLicenseOrExpired,
		// Token: 0x04000180 RID: 384
		VACBanned,
		// Token: 0x04000181 RID: 385
		LoggedInElseWhere,
		// Token: 0x04000182 RID: 386
		VACCheckTimedOut,
		// Token: 0x04000183 RID: 387
		AuthTicketCanceled,
		// Token: 0x04000184 RID: 388
		AuthTicketInvalidAlreadyUsed,
		// Token: 0x04000185 RID: 389
		AuthTicketInvalid,
		// Token: 0x04000186 RID: 390
		PublisherIssuedBan
	}
}
