using System;

namespace Steamworks
{
	// Token: 0x02000157 RID: 343
	[Flags]
	public enum EMarketNotAllowedReasonFlags
	{
		// Token: 0x0400088C RID: 2188
		k_EMarketNotAllowedReason_None = 0,
		// Token: 0x0400088D RID: 2189
		k_EMarketNotAllowedReason_TemporaryFailure = 1,
		// Token: 0x0400088E RID: 2190
		k_EMarketNotAllowedReason_AccountDisabled = 2,
		// Token: 0x0400088F RID: 2191
		k_EMarketNotAllowedReason_AccountLockedDown = 4,
		// Token: 0x04000890 RID: 2192
		k_EMarketNotAllowedReason_AccountLimited = 8,
		// Token: 0x04000891 RID: 2193
		k_EMarketNotAllowedReason_TradeBanned = 16,
		// Token: 0x04000892 RID: 2194
		k_EMarketNotAllowedReason_AccountNotTrusted = 32,
		// Token: 0x04000893 RID: 2195
		k_EMarketNotAllowedReason_SteamGuardNotEnabled = 64,
		// Token: 0x04000894 RID: 2196
		k_EMarketNotAllowedReason_SteamGuardOnlyRecentlyEnabled = 128,
		// Token: 0x04000895 RID: 2197
		k_EMarketNotAllowedReason_RecentPasswordReset = 256,
		// Token: 0x04000896 RID: 2198
		k_EMarketNotAllowedReason_NewPaymentMethod = 512,
		// Token: 0x04000897 RID: 2199
		k_EMarketNotAllowedReason_InvalidCookie = 1024,
		// Token: 0x04000898 RID: 2200
		k_EMarketNotAllowedReason_UsingNewDevice = 2048,
		// Token: 0x04000899 RID: 2201
		k_EMarketNotAllowedReason_RecentSelfRefund = 4096,
		// Token: 0x0400089A RID: 2202
		k_EMarketNotAllowedReason_NewPaymentMethodCannotBeVerified = 8192,
		// Token: 0x0400089B RID: 2203
		k_EMarketNotAllowedReason_NoRecentPurchases = 16384,
		// Token: 0x0400089C RID: 2204
		k_EMarketNotAllowedReason_AcceptedWalletGift = 32768
	}
}
