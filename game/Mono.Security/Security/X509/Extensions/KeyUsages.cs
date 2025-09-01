using System;

namespace Mono.Security.X509.Extensions
{
	// Token: 0x02000025 RID: 37
	[Flags]
	public enum KeyUsages
	{
		// Token: 0x040000E9 RID: 233
		digitalSignature = 128,
		// Token: 0x040000EA RID: 234
		nonRepudiation = 64,
		// Token: 0x040000EB RID: 235
		keyEncipherment = 32,
		// Token: 0x040000EC RID: 236
		dataEncipherment = 16,
		// Token: 0x040000ED RID: 237
		keyAgreement = 8,
		// Token: 0x040000EE RID: 238
		keyCertSign = 4,
		// Token: 0x040000EF RID: 239
		cRLSign = 2,
		// Token: 0x040000F0 RID: 240
		encipherOnly = 1,
		// Token: 0x040000F1 RID: 241
		decipherOnly = 2048,
		// Token: 0x040000F2 RID: 242
		none = 0
	}
}
