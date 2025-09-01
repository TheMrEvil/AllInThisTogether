using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract base class from which all implementations of asymmetric signature deformatters derive.</summary>
	// Token: 0x0200047B RID: 1147
	[ComVisible(true)]
	public abstract class AsymmetricSignatureDeformatter
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Security.Cryptography.AsymmetricSignatureDeformatter" />.</summary>
		// Token: 0x06002E6C RID: 11884 RVA: 0x0000259F File Offset: 0x0000079F
		protected AsymmetricSignatureDeformatter()
		{
		}

		/// <summary>When overridden in a derived class, sets the public key to use for verifying the signature.</summary>
		/// <param name="key">The instance of an implementation of <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> that holds the public key.</param>
		// Token: 0x06002E6D RID: 11885
		public abstract void SetKey(AsymmetricAlgorithm key);

		/// <summary>When overridden in a derived class, sets the hash algorithm to use for verifying the signature.</summary>
		/// <param name="strName">The name of the hash algorithm to use for verifying the signature.</param>
		// Token: 0x06002E6E RID: 11886
		public abstract void SetHashAlgorithm(string strName);

		/// <summary>Verifies the signature from the specified hash value.</summary>
		/// <param name="hash">The hash algorithm to use to verify the signature.</param>
		/// <param name="rgbSignature">The signature to be verified.</param>
		/// <returns>
		///   <see langword="true" /> if the signature is valid for the hash; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="hash" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002E6F RID: 11887 RVA: 0x000A6221 File Offset: 0x000A4421
		public virtual bool VerifySignature(HashAlgorithm hash, byte[] rgbSignature)
		{
			if (hash == null)
			{
				throw new ArgumentNullException("hash");
			}
			this.SetHashAlgorithm(hash.ToString());
			return this.VerifySignature(hash.Hash, rgbSignature);
		}

		/// <summary>When overridden in a derived class, verifies the signature for the specified data.</summary>
		/// <param name="rgbHash">The data signed with <paramref name="rgbSignature" />.</param>
		/// <param name="rgbSignature">The signature to be verified for <paramref name="rgbHash" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="rgbSignature" /> matches the signature computed using the specified hash algorithm and key on <paramref name="rgbHash" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002E70 RID: 11888
		public abstract bool VerifySignature(byte[] rgbHash, byte[] rgbSignature);
	}
}
