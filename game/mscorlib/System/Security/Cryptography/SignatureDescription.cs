using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Contains information about the properties of a digital signature.</summary>
	// Token: 0x020004B8 RID: 1208
	[ComVisible(true)]
	public class SignatureDescription
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SignatureDescription" /> class.</summary>
		// Token: 0x06003074 RID: 12404 RVA: 0x0000259F File Offset: 0x0000079F
		public SignatureDescription()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SignatureDescription" /> class from the specified <see cref="T:System.Security.SecurityElement" />.</summary>
		/// <param name="el">The <see cref="T:System.Security.SecurityElement" /> from which to get the algorithms for the signature description.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="el" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003075 RID: 12405 RVA: 0x000B0AEC File Offset: 0x000AECEC
		public SignatureDescription(SecurityElement el)
		{
			if (el == null)
			{
				throw new ArgumentNullException("el");
			}
			this._strKey = el.SearchForTextOfTag("Key");
			this._strDigest = el.SearchForTextOfTag("Digest");
			this._strFormatter = el.SearchForTextOfTag("Formatter");
			this._strDeformatter = el.SearchForTextOfTag("Deformatter");
		}

		/// <summary>Gets or sets the key algorithm for the signature description.</summary>
		/// <returns>The key algorithm for the signature description.</returns>
		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06003076 RID: 12406 RVA: 0x000B0B51 File Offset: 0x000AED51
		// (set) Token: 0x06003077 RID: 12407 RVA: 0x000B0B59 File Offset: 0x000AED59
		public string KeyAlgorithm
		{
			get
			{
				return this._strKey;
			}
			set
			{
				this._strKey = value;
			}
		}

		/// <summary>Gets or sets the digest algorithm for the signature description.</summary>
		/// <returns>The digest algorithm for the signature description.</returns>
		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06003078 RID: 12408 RVA: 0x000B0B62 File Offset: 0x000AED62
		// (set) Token: 0x06003079 RID: 12409 RVA: 0x000B0B6A File Offset: 0x000AED6A
		public string DigestAlgorithm
		{
			get
			{
				return this._strDigest;
			}
			set
			{
				this._strDigest = value;
			}
		}

		/// <summary>Gets or sets the formatter algorithm for the signature description.</summary>
		/// <returns>The formatter algorithm for the signature description.</returns>
		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x0600307A RID: 12410 RVA: 0x000B0B73 File Offset: 0x000AED73
		// (set) Token: 0x0600307B RID: 12411 RVA: 0x000B0B7B File Offset: 0x000AED7B
		public string FormatterAlgorithm
		{
			get
			{
				return this._strFormatter;
			}
			set
			{
				this._strFormatter = value;
			}
		}

		/// <summary>Gets or sets the deformatter algorithm for the signature description.</summary>
		/// <returns>The deformatter algorithm for the signature description.</returns>
		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x0600307C RID: 12412 RVA: 0x000B0B84 File Offset: 0x000AED84
		// (set) Token: 0x0600307D RID: 12413 RVA: 0x000B0B8C File Offset: 0x000AED8C
		public string DeformatterAlgorithm
		{
			get
			{
				return this._strDeformatter;
			}
			set
			{
				this._strDeformatter = value;
			}
		}

		/// <summary>Creates an <see cref="T:System.Security.Cryptography.AsymmetricSignatureDeformatter" /> instance with the specified key using the <see cref="P:System.Security.Cryptography.SignatureDescription.DeformatterAlgorithm" /> property.</summary>
		/// <param name="key">The key to use in the <see cref="T:System.Security.Cryptography.AsymmetricSignatureDeformatter" />.</param>
		/// <returns>The newly created <see cref="T:System.Security.Cryptography.AsymmetricSignatureDeformatter" /> instance.</returns>
		// Token: 0x0600307E RID: 12414 RVA: 0x000B0B95 File Offset: 0x000AED95
		public virtual AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureDeformatter asymmetricSignatureDeformatter = (AsymmetricSignatureDeformatter)CryptoConfig.CreateFromName(this._strDeformatter);
			asymmetricSignatureDeformatter.SetKey(key);
			return asymmetricSignatureDeformatter;
		}

		/// <summary>Creates an <see cref="T:System.Security.Cryptography.AsymmetricSignatureFormatter" /> instance with the specified key using the <see cref="P:System.Security.Cryptography.SignatureDescription.FormatterAlgorithm" /> property.</summary>
		/// <param name="key">The key to use in the <see cref="T:System.Security.Cryptography.AsymmetricSignatureFormatter" />.</param>
		/// <returns>The newly created <see cref="T:System.Security.Cryptography.AsymmetricSignatureFormatter" /> instance.</returns>
		// Token: 0x0600307F RID: 12415 RVA: 0x000B0BAE File Offset: 0x000AEDAE
		public virtual AsymmetricSignatureFormatter CreateFormatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureFormatter asymmetricSignatureFormatter = (AsymmetricSignatureFormatter)CryptoConfig.CreateFromName(this._strFormatter);
			asymmetricSignatureFormatter.SetKey(key);
			return asymmetricSignatureFormatter;
		}

		/// <summary>Creates a <see cref="T:System.Security.Cryptography.HashAlgorithm" /> instance using the <see cref="P:System.Security.Cryptography.SignatureDescription.DigestAlgorithm" /> property.</summary>
		/// <returns>The newly created <see cref="T:System.Security.Cryptography.HashAlgorithm" /> instance.</returns>
		// Token: 0x06003080 RID: 12416 RVA: 0x000B0BC7 File Offset: 0x000AEDC7
		public virtual HashAlgorithm CreateDigest()
		{
			return (HashAlgorithm)CryptoConfig.CreateFromName(this._strDigest);
		}

		// Token: 0x040021E9 RID: 8681
		private string _strKey;

		// Token: 0x040021EA RID: 8682
		private string _strDigest;

		// Token: 0x040021EB RID: 8683
		private string _strFormatter;

		// Token: 0x040021EC RID: 8684
		private string _strDeformatter;
	}
}
