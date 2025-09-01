using System;

namespace Mono.Btls
{
	// Token: 0x0200010F RID: 271
	[Flags]
	internal enum MonoBtlsX509VerifyFlags
	{
		// Token: 0x04000469 RID: 1129
		DEFAULT = 0,
		// Token: 0x0400046A RID: 1130
		CRL_CHECK = 1,
		// Token: 0x0400046B RID: 1131
		CRL_CHECK_ALL = 2,
		// Token: 0x0400046C RID: 1132
		X509_STRIC = 4
	}
}
