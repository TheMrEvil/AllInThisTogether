using System;

namespace System.Security.Cryptography
{
	// Token: 0x020004BE RID: 1214
	internal class DSASignatureDescription : SignatureDescription
	{
		// Token: 0x06003088 RID: 12424 RVA: 0x000B0C82 File Offset: 0x000AEE82
		public DSASignatureDescription()
		{
			base.KeyAlgorithm = "System.Security.Cryptography.DSACryptoServiceProvider";
			base.DigestAlgorithm = "System.Security.Cryptography.SHA1CryptoServiceProvider";
			base.FormatterAlgorithm = "System.Security.Cryptography.DSASignatureFormatter";
			base.DeformatterAlgorithm = "System.Security.Cryptography.DSASignatureDeformatter";
		}
	}
}
