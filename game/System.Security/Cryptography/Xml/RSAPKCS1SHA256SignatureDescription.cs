using System;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200004D RID: 77
	internal class RSAPKCS1SHA256SignatureDescription : RSAPKCS1SignatureDescription
	{
		// Token: 0x060001F9 RID: 505 RVA: 0x0000907D File Offset: 0x0000727D
		public RSAPKCS1SHA256SignatureDescription() : base("SHA256")
		{
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000908A File Offset: 0x0000728A
		public sealed override HashAlgorithm CreateDigest()
		{
			return SHA256.Create();
		}
	}
}
