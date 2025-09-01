using System;

namespace Steamworks
{
	// Token: 0x0200003B RID: 59
	internal enum AppOwnershipFlags
	{
		// Token: 0x0400019F RID: 415
		None,
		// Token: 0x040001A0 RID: 416
		OwnsLicense,
		// Token: 0x040001A1 RID: 417
		FreeLicense,
		// Token: 0x040001A2 RID: 418
		RegionRestricted = 4,
		// Token: 0x040001A3 RID: 419
		LowViolence = 8,
		// Token: 0x040001A4 RID: 420
		InvalidPlatform = 16,
		// Token: 0x040001A5 RID: 421
		SharedLicense = 32,
		// Token: 0x040001A6 RID: 422
		FreeWeekend = 64,
		// Token: 0x040001A7 RID: 423
		RetailLicense = 128,
		// Token: 0x040001A8 RID: 424
		LicenseLocked = 256,
		// Token: 0x040001A9 RID: 425
		LicensePending = 512,
		// Token: 0x040001AA RID: 426
		LicenseExpired = 1024,
		// Token: 0x040001AB RID: 427
		LicensePermanent = 2048,
		// Token: 0x040001AC RID: 428
		LicenseRecurring = 4096,
		// Token: 0x040001AD RID: 429
		LicenseCanceled = 8192,
		// Token: 0x040001AE RID: 430
		AutoGrant = 16384,
		// Token: 0x040001AF RID: 431
		PendingGift = 32768,
		// Token: 0x040001B0 RID: 432
		RentalNotActivated = 65536,
		// Token: 0x040001B1 RID: 433
		Rental = 131072,
		// Token: 0x040001B2 RID: 434
		SiteLicense = 262144,
		// Token: 0x040001B3 RID: 435
		LegacyFreeSub = 524288,
		// Token: 0x040001B4 RID: 436
		InvalidOSType = 1048576
	}
}
