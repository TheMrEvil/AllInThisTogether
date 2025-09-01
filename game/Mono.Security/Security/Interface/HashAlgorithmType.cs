using System;

namespace Mono.Security.Interface
{
	// Token: 0x0200003D RID: 61
	public enum HashAlgorithmType
	{
		// Token: 0x04000261 RID: 609
		None,
		// Token: 0x04000262 RID: 610
		Md5,
		// Token: 0x04000263 RID: 611
		Sha1,
		// Token: 0x04000264 RID: 612
		Sha224,
		// Token: 0x04000265 RID: 613
		Sha256,
		// Token: 0x04000266 RID: 614
		Sha384,
		// Token: 0x04000267 RID: 615
		Sha512,
		// Token: 0x04000268 RID: 616
		Unknown = 255,
		// Token: 0x04000269 RID: 617
		Md5Sha1 = 254
	}
}
