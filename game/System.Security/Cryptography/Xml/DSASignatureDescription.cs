using System;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000030 RID: 48
	internal class DSASignatureDescription : SignatureDescription
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x000054E8 File Offset: 0x000036E8
		public DSASignatureDescription()
		{
			base.KeyAlgorithm = typeof(DSA).AssemblyQualifiedName;
			base.FormatterAlgorithm = typeof(DSASignatureFormatter).AssemblyQualifiedName;
			base.DeformatterAlgorithm = typeof(DSASignatureDeformatter).AssemblyQualifiedName;
			base.DigestAlgorithm = "SHA1";
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005545 File Offset: 0x00003745
		public sealed override AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureDeformatter asymmetricSignatureDeformatter = (AsymmetricSignatureDeformatter)CryptoConfig.CreateFromName(base.DeformatterAlgorithm);
			asymmetricSignatureDeformatter.SetKey(key);
			asymmetricSignatureDeformatter.SetHashAlgorithm("SHA1");
			return asymmetricSignatureDeformatter;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005569 File Offset: 0x00003769
		public sealed override AsymmetricSignatureFormatter CreateFormatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureFormatter asymmetricSignatureFormatter = (AsymmetricSignatureFormatter)CryptoConfig.CreateFromName(base.FormatterAlgorithm);
			asymmetricSignatureFormatter.SetKey(key);
			asymmetricSignatureFormatter.SetHashAlgorithm("SHA1");
			return asymmetricSignatureFormatter;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000558D File Offset: 0x0000378D
		public sealed override HashAlgorithm CreateDigest()
		{
			return SHA1.Create();
		}

		// Token: 0x04000163 RID: 355
		private const string HashAlgorithm = "SHA1";
	}
}
