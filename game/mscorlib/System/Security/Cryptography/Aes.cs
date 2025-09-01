using System;
using System.Runtime.CompilerServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract base class from which all implementations of the Advanced Encryption Standard (AES) must inherit.</summary>
	// Token: 0x02000477 RID: 1143
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
	public abstract class Aes : SymmetricAlgorithm
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Aes" /> class.</summary>
		// Token: 0x06002E45 RID: 11845 RVA: 0x000A6090 File Offset: 0x000A4290
		protected Aes()
		{
			this.LegalBlockSizesValue = Aes.s_legalBlockSizes;
			this.LegalKeySizesValue = Aes.s_legalKeySizes;
			this.BlockSizeValue = 128;
			this.FeedbackSizeValue = 8;
			this.KeySizeValue = 256;
			this.ModeValue = CipherMode.CBC;
		}

		/// <summary>Creates a cryptographic object that is used to perform the symmetric algorithm.</summary>
		/// <returns>A cryptographic object that is used to perform the symmetric algorithm.</returns>
		// Token: 0x06002E46 RID: 11846 RVA: 0x000A60DD File Offset: 0x000A42DD
		public new static Aes Create()
		{
			return Aes.Create("AES");
		}

		/// <summary>Creates a cryptographic object that specifies the implementation of AES to use to perform the symmetric algorithm.</summary>
		/// <param name="algorithmName">The name of the specific implementation of AES to use.</param>
		/// <returns>A cryptographic object that is used to perform the symmetric algorithm.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="algorithmName" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002E47 RID: 11847 RVA: 0x000A60E9 File Offset: 0x000A42E9
		public new static Aes Create(string algorithmName)
		{
			if (algorithmName == null)
			{
				throw new ArgumentNullException("algorithmName");
			}
			return CryptoConfig.CreateFromName(algorithmName) as Aes;
		}

		// Token: 0x06002E48 RID: 11848 RVA: 0x000A6104 File Offset: 0x000A4304
		// Note: this type is marked as 'beforefieldinit'.
		static Aes()
		{
		}

		// Token: 0x04002129 RID: 8489
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(128, 128, 0)
		};

		// Token: 0x0400212A RID: 8490
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(128, 256, 64)
		};
	}
}
