using System;

namespace Steamworks
{
	// Token: 0x02000035 RID: 53
	internal enum DenyReason
	{
		// Token: 0x04000165 RID: 357
		Invalid,
		// Token: 0x04000166 RID: 358
		InvalidVersion,
		// Token: 0x04000167 RID: 359
		Generic,
		// Token: 0x04000168 RID: 360
		NotLoggedOn,
		// Token: 0x04000169 RID: 361
		NoLicense,
		// Token: 0x0400016A RID: 362
		Cheater,
		// Token: 0x0400016B RID: 363
		LoggedInElseWhere,
		// Token: 0x0400016C RID: 364
		UnknownText,
		// Token: 0x0400016D RID: 365
		IncompatibleAnticheat,
		// Token: 0x0400016E RID: 366
		MemoryCorruption,
		// Token: 0x0400016F RID: 367
		IncompatibleSoftware,
		// Token: 0x04000170 RID: 368
		SteamConnectionLost,
		// Token: 0x04000171 RID: 369
		SteamConnectionError,
		// Token: 0x04000172 RID: 370
		SteamResponseTimedOut,
		// Token: 0x04000173 RID: 371
		SteamValidationStalled,
		// Token: 0x04000174 RID: 372
		SteamOwnerLeftGuestUser
	}
}
