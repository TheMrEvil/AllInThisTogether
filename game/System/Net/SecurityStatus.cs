using System;

namespace System.Net
{
	// Token: 0x020005E3 RID: 1507
	internal enum SecurityStatus
	{
		// Token: 0x04001B18 RID: 6936
		OK,
		// Token: 0x04001B19 RID: 6937
		ContinueNeeded = 590610,
		// Token: 0x04001B1A RID: 6938
		CompleteNeeded,
		// Token: 0x04001B1B RID: 6939
		CompAndContinue,
		// Token: 0x04001B1C RID: 6940
		ContextExpired = 590615,
		// Token: 0x04001B1D RID: 6941
		CredentialsNeeded = 590624,
		// Token: 0x04001B1E RID: 6942
		Renegotiate,
		// Token: 0x04001B1F RID: 6943
		OutOfMemory = -2146893056,
		// Token: 0x04001B20 RID: 6944
		InvalidHandle,
		// Token: 0x04001B21 RID: 6945
		Unsupported,
		// Token: 0x04001B22 RID: 6946
		TargetUnknown,
		// Token: 0x04001B23 RID: 6947
		InternalError,
		// Token: 0x04001B24 RID: 6948
		PackageNotFound,
		// Token: 0x04001B25 RID: 6949
		NotOwner,
		// Token: 0x04001B26 RID: 6950
		CannotInstall,
		// Token: 0x04001B27 RID: 6951
		InvalidToken,
		// Token: 0x04001B28 RID: 6952
		CannotPack,
		// Token: 0x04001B29 RID: 6953
		QopNotSupported,
		// Token: 0x04001B2A RID: 6954
		NoImpersonation,
		// Token: 0x04001B2B RID: 6955
		LogonDenied,
		// Token: 0x04001B2C RID: 6956
		UnknownCredentials,
		// Token: 0x04001B2D RID: 6957
		NoCredentials,
		// Token: 0x04001B2E RID: 6958
		MessageAltered,
		// Token: 0x04001B2F RID: 6959
		OutOfSequence,
		// Token: 0x04001B30 RID: 6960
		NoAuthenticatingAuthority,
		// Token: 0x04001B31 RID: 6961
		IncompleteMessage = -2146893032,
		// Token: 0x04001B32 RID: 6962
		IncompleteCredentials = -2146893024,
		// Token: 0x04001B33 RID: 6963
		BufferNotEnough,
		// Token: 0x04001B34 RID: 6964
		WrongPrincipal,
		// Token: 0x04001B35 RID: 6965
		TimeSkew = -2146893020,
		// Token: 0x04001B36 RID: 6966
		UntrustedRoot,
		// Token: 0x04001B37 RID: 6967
		IllegalMessage,
		// Token: 0x04001B38 RID: 6968
		CertUnknown,
		// Token: 0x04001B39 RID: 6969
		CertExpired,
		// Token: 0x04001B3A RID: 6970
		AlgorithmMismatch = -2146893007,
		// Token: 0x04001B3B RID: 6971
		SecurityQosFailed,
		// Token: 0x04001B3C RID: 6972
		SmartcardLogonRequired = -2146892994,
		// Token: 0x04001B3D RID: 6973
		UnsupportedPreauth = -2146892989,
		// Token: 0x04001B3E RID: 6974
		BadBinding = -2146892986
	}
}
