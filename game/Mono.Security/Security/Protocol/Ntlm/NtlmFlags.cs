using System;

namespace Mono.Security.Protocol.Ntlm
{
	// Token: 0x0200002F RID: 47
	[Flags]
	public enum NtlmFlags
	{
		// Token: 0x0400010A RID: 266
		NegotiateUnicode = 1,
		// Token: 0x0400010B RID: 267
		NegotiateOem = 2,
		// Token: 0x0400010C RID: 268
		RequestTarget = 4,
		// Token: 0x0400010D RID: 269
		NegotiateNtlm = 512,
		// Token: 0x0400010E RID: 270
		NegotiateDomainSupplied = 4096,
		// Token: 0x0400010F RID: 271
		NegotiateWorkstationSupplied = 8192,
		// Token: 0x04000110 RID: 272
		NegotiateAlwaysSign = 32768,
		// Token: 0x04000111 RID: 273
		NegotiateNtlm2Key = 524288,
		// Token: 0x04000112 RID: 274
		Negotiate128 = 536870912,
		// Token: 0x04000113 RID: 275
		Negotiate56 = -2147483648
	}
}
