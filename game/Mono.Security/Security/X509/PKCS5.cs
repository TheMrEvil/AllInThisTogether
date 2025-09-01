using System;

namespace Mono.Security.X509
{
	// Token: 0x0200000C RID: 12
	public class PKCS5
	{
		// Token: 0x06000066 RID: 102 RVA: 0x000043DD File Offset: 0x000025DD
		public PKCS5()
		{
		}

		// Token: 0x0400004B RID: 75
		public const string pbeWithMD2AndDESCBC = "1.2.840.113549.1.5.1";

		// Token: 0x0400004C RID: 76
		public const string pbeWithMD5AndDESCBC = "1.2.840.113549.1.5.3";

		// Token: 0x0400004D RID: 77
		public const string pbeWithMD2AndRC2CBC = "1.2.840.113549.1.5.4";

		// Token: 0x0400004E RID: 78
		public const string pbeWithMD5AndRC2CBC = "1.2.840.113549.1.5.6";

		// Token: 0x0400004F RID: 79
		public const string pbeWithSHA1AndDESCBC = "1.2.840.113549.1.5.10";

		// Token: 0x04000050 RID: 80
		public const string pbeWithSHA1AndRC2CBC = "1.2.840.113549.1.5.11";
	}
}
