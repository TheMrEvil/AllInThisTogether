using System;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200004C RID: 76
	internal class RSAPKCS1SHA1SignatureDescription : RSAPKCS1SignatureDescription
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x00009070 File Offset: 0x00007270
		public RSAPKCS1SHA1SignatureDescription() : base("SHA1")
		{
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000558D File Offset: 0x0000378D
		public sealed override HashAlgorithm CreateDigest()
		{
			return SHA1.Create();
		}
	}
}
