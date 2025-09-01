using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the base class from which all asymmetric key exchange deformatters derive.</summary>
	// Token: 0x02000479 RID: 1145
	[ComVisible(true)]
	public abstract class AsymmetricKeyExchangeDeformatter
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Security.Cryptography.AsymmetricKeyExchangeDeformatter" />.</summary>
		// Token: 0x06002E62 RID: 11874 RVA: 0x0000259F File Offset: 0x0000079F
		protected AsymmetricKeyExchangeDeformatter()
		{
		}

		/// <summary>When overridden in a derived class, gets or sets the parameters for the asymmetric key exchange.</summary>
		/// <returns>A string in XML format containing the parameters of the asymmetric key exchange operation.</returns>
		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06002E63 RID: 11875
		// (set) Token: 0x06002E64 RID: 11876
		public abstract string Parameters { get; set; }

		/// <summary>When overridden in a derived class, sets the private key to use for decrypting the secret information.</summary>
		/// <param name="key">The instance of the implementation of <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> that holds the private key.</param>
		// Token: 0x06002E65 RID: 11877
		public abstract void SetKey(AsymmetricAlgorithm key);

		/// <summary>When overridden in a derived class, extracts secret information from the encrypted key exchange data.</summary>
		/// <param name="rgb">The key exchange data within which the secret information is hidden.</param>
		/// <returns>The secret information derived from the key exchange data.</returns>
		// Token: 0x06002E66 RID: 11878
		public abstract byte[] DecryptKeyExchange(byte[] rgb);
	}
}
