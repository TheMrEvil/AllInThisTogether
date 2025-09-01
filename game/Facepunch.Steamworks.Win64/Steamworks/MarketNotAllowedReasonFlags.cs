using System;

namespace Steamworks
{
	// Token: 0x02000046 RID: 70
	internal enum MarketNotAllowedReasonFlags
	{
		// Token: 0x0400024B RID: 587
		None,
		// Token: 0x0400024C RID: 588
		TemporaryFailure,
		// Token: 0x0400024D RID: 589
		AccountDisabled,
		// Token: 0x0400024E RID: 590
		AccountLockedDown = 4,
		// Token: 0x0400024F RID: 591
		AccountLimited = 8,
		// Token: 0x04000250 RID: 592
		TradeBanned = 16,
		// Token: 0x04000251 RID: 593
		AccountNotTrusted = 32,
		// Token: 0x04000252 RID: 594
		SteamGuardNotEnabled = 64,
		// Token: 0x04000253 RID: 595
		SteamGuardOnlyRecentlyEnabled = 128,
		// Token: 0x04000254 RID: 596
		RecentPasswordReset = 256,
		// Token: 0x04000255 RID: 597
		NewPaymentMethod = 512,
		// Token: 0x04000256 RID: 598
		InvalidCookie = 1024,
		// Token: 0x04000257 RID: 599
		UsingNewDevice = 2048,
		// Token: 0x04000258 RID: 600
		RecentSelfRefund = 4096,
		// Token: 0x04000259 RID: 601
		NewPaymentMethodCannotBeVerified = 8192,
		// Token: 0x0400025A RID: 602
		NoRecentPurchases = 16384,
		// Token: 0x0400025B RID: 603
		AcceptedWalletGift = 32768
	}
}
