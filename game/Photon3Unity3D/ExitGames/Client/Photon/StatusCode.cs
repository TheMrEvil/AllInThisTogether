using System;

namespace ExitGames.Client.Photon
{
	// Token: 0x0200000A RID: 10
	public enum StatusCode
	{
		// Token: 0x0400004C RID: 76
		Connect = 1024,
		// Token: 0x0400004D RID: 77
		Disconnect,
		// Token: 0x0400004E RID: 78
		Exception,
		// Token: 0x0400004F RID: 79
		ExceptionOnConnect = 1023,
		// Token: 0x04000050 RID: 80
		ServerAddressInvalid = 1050,
		// Token: 0x04000051 RID: 81
		DnsExceptionOnConnect,
		// Token: 0x04000052 RID: 82
		SecurityExceptionOnConnect = 1022,
		// Token: 0x04000053 RID: 83
		SendError = 1030,
		// Token: 0x04000054 RID: 84
		ExceptionOnReceive = 1039,
		// Token: 0x04000055 RID: 85
		TimeoutDisconnect,
		// Token: 0x04000056 RID: 86
		DisconnectByServerTimeout,
		// Token: 0x04000057 RID: 87
		DisconnectByServerUserLimit,
		// Token: 0x04000058 RID: 88
		DisconnectByServerLogic,
		// Token: 0x04000059 RID: 89
		DisconnectByServerReasonUnknown,
		// Token: 0x0400005A RID: 90
		EncryptionEstablished = 1048,
		// Token: 0x0400005B RID: 91
		EncryptionFailedToEstablish
	}
}
