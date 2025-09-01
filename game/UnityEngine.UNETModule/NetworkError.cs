using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000005 RID: 5
	public enum NetworkError
	{
		// Token: 0x04000015 RID: 21
		Ok,
		// Token: 0x04000016 RID: 22
		WrongHost,
		// Token: 0x04000017 RID: 23
		WrongConnection,
		// Token: 0x04000018 RID: 24
		WrongChannel,
		// Token: 0x04000019 RID: 25
		NoResources,
		// Token: 0x0400001A RID: 26
		BadMessage,
		// Token: 0x0400001B RID: 27
		Timeout,
		// Token: 0x0400001C RID: 28
		MessageToLong,
		// Token: 0x0400001D RID: 29
		WrongOperation,
		// Token: 0x0400001E RID: 30
		VersionMismatch,
		// Token: 0x0400001F RID: 31
		CRCMismatch,
		// Token: 0x04000020 RID: 32
		DNSFailure,
		// Token: 0x04000021 RID: 33
		UsageError
	}
}
