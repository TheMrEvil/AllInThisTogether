using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002EA RID: 746
	public abstract class X509SignatureGenerator
	{
		// Token: 0x060017D7 RID: 6103 RVA: 0x0005E6FC File Offset: 0x0005C8FC
		protected X509SignatureGenerator()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x00011F54 File Offset: 0x00010154
		public PublicKey PublicKey
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x060017D9 RID: 6105
		protected abstract PublicKey BuildPublicKey();

		// Token: 0x060017DA RID: 6106 RVA: 0x00011F54 File Offset: 0x00010154
		public static X509SignatureGenerator CreateForECDsa(ECDsa key)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x00011F54 File Offset: 0x00010154
		public static X509SignatureGenerator CreateForRSA(RSA key, RSASignaturePadding signaturePadding)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060017DC RID: 6108
		public abstract byte[] GetSignatureAlgorithmIdentifier(HashAlgorithmName hashAlgorithm);

		// Token: 0x060017DD RID: 6109
		public abstract byte[] SignData(byte[] data, HashAlgorithmName hashAlgorithm);
	}
}
