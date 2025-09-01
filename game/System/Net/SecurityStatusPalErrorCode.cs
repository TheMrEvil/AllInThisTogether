using System;

namespace System.Net
{
	// Token: 0x0200056D RID: 1389
	internal enum SecurityStatusPalErrorCode
	{
		// Token: 0x04001845 RID: 6213
		NotSet,
		// Token: 0x04001846 RID: 6214
		OK,
		// Token: 0x04001847 RID: 6215
		ContinueNeeded,
		// Token: 0x04001848 RID: 6216
		CompleteNeeded,
		// Token: 0x04001849 RID: 6217
		CompAndContinue,
		// Token: 0x0400184A RID: 6218
		ContextExpired,
		// Token: 0x0400184B RID: 6219
		CredentialsNeeded,
		// Token: 0x0400184C RID: 6220
		Renegotiate,
		// Token: 0x0400184D RID: 6221
		OutOfMemory,
		// Token: 0x0400184E RID: 6222
		InvalidHandle,
		// Token: 0x0400184F RID: 6223
		Unsupported,
		// Token: 0x04001850 RID: 6224
		TargetUnknown,
		// Token: 0x04001851 RID: 6225
		InternalError,
		// Token: 0x04001852 RID: 6226
		PackageNotFound,
		// Token: 0x04001853 RID: 6227
		NotOwner,
		// Token: 0x04001854 RID: 6228
		CannotInstall,
		// Token: 0x04001855 RID: 6229
		InvalidToken,
		// Token: 0x04001856 RID: 6230
		CannotPack,
		// Token: 0x04001857 RID: 6231
		QopNotSupported,
		// Token: 0x04001858 RID: 6232
		NoImpersonation,
		// Token: 0x04001859 RID: 6233
		LogonDenied,
		// Token: 0x0400185A RID: 6234
		UnknownCredentials,
		// Token: 0x0400185B RID: 6235
		NoCredentials,
		// Token: 0x0400185C RID: 6236
		MessageAltered,
		// Token: 0x0400185D RID: 6237
		OutOfSequence,
		// Token: 0x0400185E RID: 6238
		NoAuthenticatingAuthority,
		// Token: 0x0400185F RID: 6239
		IncompleteMessage,
		// Token: 0x04001860 RID: 6240
		IncompleteCredentials,
		// Token: 0x04001861 RID: 6241
		BufferNotEnough,
		// Token: 0x04001862 RID: 6242
		WrongPrincipal,
		// Token: 0x04001863 RID: 6243
		TimeSkew,
		// Token: 0x04001864 RID: 6244
		UntrustedRoot,
		// Token: 0x04001865 RID: 6245
		IllegalMessage,
		// Token: 0x04001866 RID: 6246
		CertUnknown,
		// Token: 0x04001867 RID: 6247
		CertExpired,
		// Token: 0x04001868 RID: 6248
		AlgorithmMismatch,
		// Token: 0x04001869 RID: 6249
		SecurityQosFailed,
		// Token: 0x0400186A RID: 6250
		SmartcardLogonRequired,
		// Token: 0x0400186B RID: 6251
		UnsupportedPreauth,
		// Token: 0x0400186C RID: 6252
		BadBinding,
		// Token: 0x0400186D RID: 6253
		DowngradeDetected,
		// Token: 0x0400186E RID: 6254
		ApplicationProtocolMismatch
	}
}
