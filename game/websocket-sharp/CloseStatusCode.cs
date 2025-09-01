using System;

namespace WebSocketSharp
{
	// Token: 0x02000008 RID: 8
	public enum CloseStatusCode : ushort
	{
		// Token: 0x04000046 RID: 70
		Normal = 1000,
		// Token: 0x04000047 RID: 71
		Away,
		// Token: 0x04000048 RID: 72
		ProtocolError,
		// Token: 0x04000049 RID: 73
		UnsupportedData,
		// Token: 0x0400004A RID: 74
		Undefined,
		// Token: 0x0400004B RID: 75
		NoStatus,
		// Token: 0x0400004C RID: 76
		Abnormal,
		// Token: 0x0400004D RID: 77
		InvalidData,
		// Token: 0x0400004E RID: 78
		PolicyViolation,
		// Token: 0x0400004F RID: 79
		TooBig,
		// Token: 0x04000050 RID: 80
		MandatoryExtension,
		// Token: 0x04000051 RID: 81
		ServerError,
		// Token: 0x04000052 RID: 82
		TlsHandshakeFailure = 1015
	}
}
