using System;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200004E RID: 78
	internal class RSAPKCS1SHA384SignatureDescription : RSAPKCS1SignatureDescription
	{
		// Token: 0x060001FB RID: 507 RVA: 0x00009091 File Offset: 0x00007291
		public RSAPKCS1SHA384SignatureDescription() : base("SHA384")
		{
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000909E File Offset: 0x0000729E
		public sealed override HashAlgorithm CreateDigest()
		{
			return SHA384.Create();
		}
	}
}
