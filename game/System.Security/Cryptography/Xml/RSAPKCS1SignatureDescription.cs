using System;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000050 RID: 80
	internal abstract class RSAPKCS1SignatureDescription : SignatureDescription
	{
		// Token: 0x060001FF RID: 511 RVA: 0x000090BC File Offset: 0x000072BC
		public RSAPKCS1SignatureDescription(string hashAlgorithmName)
		{
			base.KeyAlgorithm = typeof(RSA).AssemblyQualifiedName;
			base.FormatterAlgorithm = typeof(RSAPKCS1SignatureFormatter).AssemblyQualifiedName;
			base.DeformatterAlgorithm = typeof(RSAPKCS1SignatureDeformatter).AssemblyQualifiedName;
			base.DigestAlgorithm = hashAlgorithmName;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00009115 File Offset: 0x00007315
		public sealed override AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureDeformatter asymmetricSignatureDeformatter = (AsymmetricSignatureDeformatter)CryptoConfig.CreateFromName(base.DeformatterAlgorithm);
			asymmetricSignatureDeformatter.SetKey(key);
			asymmetricSignatureDeformatter.SetHashAlgorithm(base.DigestAlgorithm);
			return asymmetricSignatureDeformatter;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000913A File Offset: 0x0000733A
		public sealed override AsymmetricSignatureFormatter CreateFormatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureFormatter asymmetricSignatureFormatter = (AsymmetricSignatureFormatter)CryptoConfig.CreateFromName(base.FormatterAlgorithm);
			asymmetricSignatureFormatter.SetKey(key);
			asymmetricSignatureFormatter.SetHashAlgorithm(base.DigestAlgorithm);
			return asymmetricSignatureFormatter;
		}

		// Token: 0x06000202 RID: 514
		public abstract override HashAlgorithm CreateDigest();
	}
}
