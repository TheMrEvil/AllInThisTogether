using System;

namespace Steamworks
{
	// Token: 0x02000033 RID: 51
	public enum Result
	{
		// Token: 0x040000E6 RID: 230
		None,
		// Token: 0x040000E7 RID: 231
		OK,
		// Token: 0x040000E8 RID: 232
		Fail,
		// Token: 0x040000E9 RID: 233
		NoConnection,
		// Token: 0x040000EA RID: 234
		InvalidPassword = 5,
		// Token: 0x040000EB RID: 235
		LoggedInElsewhere,
		// Token: 0x040000EC RID: 236
		InvalidProtocolVer,
		// Token: 0x040000ED RID: 237
		InvalidParam,
		// Token: 0x040000EE RID: 238
		FileNotFound,
		// Token: 0x040000EF RID: 239
		Busy,
		// Token: 0x040000F0 RID: 240
		InvalidState,
		// Token: 0x040000F1 RID: 241
		InvalidName,
		// Token: 0x040000F2 RID: 242
		InvalidEmail,
		// Token: 0x040000F3 RID: 243
		DuplicateName,
		// Token: 0x040000F4 RID: 244
		AccessDenied,
		// Token: 0x040000F5 RID: 245
		Timeout,
		// Token: 0x040000F6 RID: 246
		Banned,
		// Token: 0x040000F7 RID: 247
		AccountNotFound,
		// Token: 0x040000F8 RID: 248
		InvalidSteamID,
		// Token: 0x040000F9 RID: 249
		ServiceUnavailable,
		// Token: 0x040000FA RID: 250
		NotLoggedOn,
		// Token: 0x040000FB RID: 251
		Pending,
		// Token: 0x040000FC RID: 252
		EncryptionFailure,
		// Token: 0x040000FD RID: 253
		InsufficientPrivilege,
		// Token: 0x040000FE RID: 254
		LimitExceeded,
		// Token: 0x040000FF RID: 255
		Revoked,
		// Token: 0x04000100 RID: 256
		Expired,
		// Token: 0x04000101 RID: 257
		AlreadyRedeemed,
		// Token: 0x04000102 RID: 258
		DuplicateRequest,
		// Token: 0x04000103 RID: 259
		AlreadyOwned,
		// Token: 0x04000104 RID: 260
		IPNotFound,
		// Token: 0x04000105 RID: 261
		PersistFailed,
		// Token: 0x04000106 RID: 262
		LockingFailed,
		// Token: 0x04000107 RID: 263
		LogonSessionReplaced,
		// Token: 0x04000108 RID: 264
		ConnectFailed,
		// Token: 0x04000109 RID: 265
		HandshakeFailed,
		// Token: 0x0400010A RID: 266
		IOFailure,
		// Token: 0x0400010B RID: 267
		RemoteDisconnect,
		// Token: 0x0400010C RID: 268
		ShoppingCartNotFound,
		// Token: 0x0400010D RID: 269
		Blocked,
		// Token: 0x0400010E RID: 270
		Ignored,
		// Token: 0x0400010F RID: 271
		NoMatch,
		// Token: 0x04000110 RID: 272
		AccountDisabled,
		// Token: 0x04000111 RID: 273
		ServiceReadOnly,
		// Token: 0x04000112 RID: 274
		AccountNotFeatured,
		// Token: 0x04000113 RID: 275
		AdministratorOK,
		// Token: 0x04000114 RID: 276
		ContentVersion,
		// Token: 0x04000115 RID: 277
		TryAnotherCM,
		// Token: 0x04000116 RID: 278
		PasswordRequiredToKickSession,
		// Token: 0x04000117 RID: 279
		AlreadyLoggedInElsewhere,
		// Token: 0x04000118 RID: 280
		Suspended,
		// Token: 0x04000119 RID: 281
		Cancelled,
		// Token: 0x0400011A RID: 282
		DataCorruption,
		// Token: 0x0400011B RID: 283
		DiskFull,
		// Token: 0x0400011C RID: 284
		RemoteCallFailed,
		// Token: 0x0400011D RID: 285
		PasswordUnset,
		// Token: 0x0400011E RID: 286
		ExternalAccountUnlinked,
		// Token: 0x0400011F RID: 287
		PSNTicketInvalid,
		// Token: 0x04000120 RID: 288
		ExternalAccountAlreadyLinked,
		// Token: 0x04000121 RID: 289
		RemoteFileConflict,
		// Token: 0x04000122 RID: 290
		IllegalPassword,
		// Token: 0x04000123 RID: 291
		SameAsPreviousValue,
		// Token: 0x04000124 RID: 292
		AccountLogonDenied,
		// Token: 0x04000125 RID: 293
		CannotUseOldPassword,
		// Token: 0x04000126 RID: 294
		InvalidLoginAuthCode,
		// Token: 0x04000127 RID: 295
		AccountLogonDeniedNoMail,
		// Token: 0x04000128 RID: 296
		HardwareNotCapableOfIPT,
		// Token: 0x04000129 RID: 297
		IPTInitError,
		// Token: 0x0400012A RID: 298
		ParentalControlRestricted,
		// Token: 0x0400012B RID: 299
		FacebookQueryError,
		// Token: 0x0400012C RID: 300
		ExpiredLoginAuthCode,
		// Token: 0x0400012D RID: 301
		IPLoginRestrictionFailed,
		// Token: 0x0400012E RID: 302
		AccountLockedDown,
		// Token: 0x0400012F RID: 303
		AccountLogonDeniedVerifiedEmailRequired,
		// Token: 0x04000130 RID: 304
		NoMatchingURL,
		// Token: 0x04000131 RID: 305
		BadResponse,
		// Token: 0x04000132 RID: 306
		RequirePasswordReEntry,
		// Token: 0x04000133 RID: 307
		ValueOutOfRange,
		// Token: 0x04000134 RID: 308
		UnexpectedError,
		// Token: 0x04000135 RID: 309
		Disabled,
		// Token: 0x04000136 RID: 310
		InvalidCEGSubmission,
		// Token: 0x04000137 RID: 311
		RestrictedDevice,
		// Token: 0x04000138 RID: 312
		RegionLocked,
		// Token: 0x04000139 RID: 313
		RateLimitExceeded,
		// Token: 0x0400013A RID: 314
		AccountLoginDeniedNeedTwoFactor,
		// Token: 0x0400013B RID: 315
		ItemDeleted,
		// Token: 0x0400013C RID: 316
		AccountLoginDeniedThrottle,
		// Token: 0x0400013D RID: 317
		TwoFactorCodeMismatch,
		// Token: 0x0400013E RID: 318
		TwoFactorActivationCodeMismatch,
		// Token: 0x0400013F RID: 319
		AccountAssociatedToMultiplePartners,
		// Token: 0x04000140 RID: 320
		NotModified,
		// Token: 0x04000141 RID: 321
		NoMobileDevice,
		// Token: 0x04000142 RID: 322
		TimeNotSynced,
		// Token: 0x04000143 RID: 323
		SmsCodeFailed,
		// Token: 0x04000144 RID: 324
		AccountLimitExceeded,
		// Token: 0x04000145 RID: 325
		AccountActivityLimitExceeded,
		// Token: 0x04000146 RID: 326
		PhoneActivityLimitExceeded,
		// Token: 0x04000147 RID: 327
		RefundToWallet,
		// Token: 0x04000148 RID: 328
		EmailSendFailure,
		// Token: 0x04000149 RID: 329
		NotSettled,
		// Token: 0x0400014A RID: 330
		NeedCaptcha,
		// Token: 0x0400014B RID: 331
		GSLTDenied,
		// Token: 0x0400014C RID: 332
		GSOwnerDenied,
		// Token: 0x0400014D RID: 333
		InvalidItemType,
		// Token: 0x0400014E RID: 334
		IPBanned,
		// Token: 0x0400014F RID: 335
		GSLTExpired,
		// Token: 0x04000150 RID: 336
		InsufficientFunds,
		// Token: 0x04000151 RID: 337
		TooManyPending,
		// Token: 0x04000152 RID: 338
		NoSiteLicensesFound,
		// Token: 0x04000153 RID: 339
		WGNetworkSendExceeded,
		// Token: 0x04000154 RID: 340
		AccountNotFriends,
		// Token: 0x04000155 RID: 341
		LimitedUserAccount,
		// Token: 0x04000156 RID: 342
		CantRemoveItem,
		// Token: 0x04000157 RID: 343
		AccountDeleted,
		// Token: 0x04000158 RID: 344
		ExistingUserCancelledLicense
	}
}
