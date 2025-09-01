using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000BC RID: 188
	internal enum DnsRCode : ushort
	{
		// Token: 0x04000306 RID: 774
		NoError,
		// Token: 0x04000307 RID: 775
		FormErr,
		// Token: 0x04000308 RID: 776
		ServFail,
		// Token: 0x04000309 RID: 777
		NXDomain,
		// Token: 0x0400030A RID: 778
		NotImp,
		// Token: 0x0400030B RID: 779
		Refused,
		// Token: 0x0400030C RID: 780
		YXDomain,
		// Token: 0x0400030D RID: 781
		YXRRSet,
		// Token: 0x0400030E RID: 782
		NXRRSet,
		// Token: 0x0400030F RID: 783
		NotAuth,
		// Token: 0x04000310 RID: 784
		NotZone,
		// Token: 0x04000311 RID: 785
		BadVers = 16,
		// Token: 0x04000312 RID: 786
		BadSig = 16,
		// Token: 0x04000313 RID: 787
		BadKey,
		// Token: 0x04000314 RID: 788
		BadTime,
		// Token: 0x04000315 RID: 789
		BadMode,
		// Token: 0x04000316 RID: 790
		BadName,
		// Token: 0x04000317 RID: 791
		BadAlg,
		// Token: 0x04000318 RID: 792
		BadTrunc
	}
}
