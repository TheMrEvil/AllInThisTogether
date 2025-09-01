using System;

namespace Photon.Realtime
{
	// Token: 0x02000009 RID: 9
	public enum DisconnectCause
	{
		// Token: 0x0400004C RID: 76
		None,
		// Token: 0x0400004D RID: 77
		ExceptionOnConnect,
		// Token: 0x0400004E RID: 78
		DnsExceptionOnConnect,
		// Token: 0x0400004F RID: 79
		ServerAddressInvalid,
		// Token: 0x04000050 RID: 80
		Exception,
		// Token: 0x04000051 RID: 81
		SendException,
		// Token: 0x04000052 RID: 82
		ReceiveException,
		// Token: 0x04000053 RID: 83
		ServerTimeout,
		// Token: 0x04000054 RID: 84
		ClientTimeout,
		// Token: 0x04000055 RID: 85
		DisconnectByServerLogic,
		// Token: 0x04000056 RID: 86
		DisconnectByServerReasonUnknown,
		// Token: 0x04000057 RID: 87
		InvalidAuthentication,
		// Token: 0x04000058 RID: 88
		CustomAuthenticationFailed,
		// Token: 0x04000059 RID: 89
		AuthenticationTicketExpired,
		// Token: 0x0400005A RID: 90
		MaxCcuReached,
		// Token: 0x0400005B RID: 91
		InvalidRegion,
		// Token: 0x0400005C RID: 92
		OperationNotAllowedInCurrentState,
		// Token: 0x0400005D RID: 93
		DisconnectByClientLogic,
		// Token: 0x0400005E RID: 94
		DisconnectByOperationLimit,
		// Token: 0x0400005F RID: 95
		DisconnectByDisconnectMessage,
		// Token: 0x04000060 RID: 96
		ApplicationQuit
	}
}
