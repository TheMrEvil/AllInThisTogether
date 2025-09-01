using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the base class from which all asymmetric key exchange formatters derive.</summary>
	// Token: 0x0200047A RID: 1146
	[ComVisible(true)]
	public abstract class AsymmetricKeyExchangeFormatter
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Security.Cryptography.AsymmetricKeyExchangeFormatter" />.</summary>
		// Token: 0x06002E67 RID: 11879 RVA: 0x0000259F File Offset: 0x0000079F
		protected AsymmetricKeyExchangeFormatter()
		{
		}

		/// <summary>When overridden in a derived class, gets the parameters for the asymmetric key exchange.</summary>
		/// <returns>A string in XML format containing the parameters of the asymmetric key exchange operation.</returns>
		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06002E68 RID: 11880
		public abstract string Parameters { get; }

		/// <summary>When overridden in a derived class, sets the public key to use for encrypting the secret information.</summary>
		/// <param name="key">The instance of the implementation of <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> that holds the public key.</param>
		// Token: 0x06002E69 RID: 11881
		public abstract void SetKey(AsymmetricAlgorithm key);

		/// <summary>When overridden in a derived class, creates the encrypted key exchange data from the specified input data.</summary>
		/// <param name="data">The secret information to be passed in the key exchange.</param>
		/// <returns>The encrypted key exchange data to be sent to the intended recipient.</returns>
		// Token: 0x06002E6A RID: 11882
		public abstract byte[] CreateKeyExchange(byte[] data);

		/// <summary>When overridden in a derived class, creates the encrypted key exchange data from the specified input data.</summary>
		/// <param name="data">The secret information to be passed in the key exchange.</param>
		/// <param name="symAlgType">This parameter is not used in the current version.</param>
		/// <returns>The encrypted key exchange data to be sent to the intended recipient.</returns>
		// Token: 0x06002E6B RID: 11883
		public abstract byte[] CreateKeyExchange(byte[] data, Type symAlgType);
	}
}
