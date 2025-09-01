using System;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200004F RID: 79
	internal class RSAPKCS1SHA512SignatureDescription : RSAPKCS1SignatureDescription
	{
		// Token: 0x060001FD RID: 509 RVA: 0x000090A5 File Offset: 0x000072A5
		public RSAPKCS1SHA512SignatureDescription() : base("SHA512")
		{
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000090B2 File Offset: 0x000072B2
		public sealed override HashAlgorithm CreateDigest()
		{
			return SHA512.Create();
		}
	}
}
