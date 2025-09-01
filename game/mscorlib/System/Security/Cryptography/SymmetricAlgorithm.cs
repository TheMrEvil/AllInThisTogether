using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract base class from which all implementations of symmetric algorithms must inherit.</summary>
	// Token: 0x020004BF RID: 1215
	[ComVisible(true)]
	public abstract class SymmetricAlgorithm : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> class.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The implementation of the class derived from the symmetric algorithm is not valid.</exception>
		// Token: 0x06003089 RID: 12425 RVA: 0x000B0CB6 File Offset: 0x000AEEB6
		protected SymmetricAlgorithm()
		{
			this.ModeValue = CipherMode.CBC;
			this.PaddingValue = PaddingMode.PKCS7;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> class.</summary>
		// Token: 0x0600308A RID: 12426 RVA: 0x000B0CCC File Offset: 0x000AEECC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> class.</summary>
		// Token: 0x0600308B RID: 12427 RVA: 0x000A5BFB File Offset: 0x000A3DFB
		public void Clear()
		{
			((IDisposable)this).Dispose();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x0600308C RID: 12428 RVA: 0x000B0CDC File Offset: 0x000AEEDC
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.KeyValue != null)
				{
					Array.Clear(this.KeyValue, 0, this.KeyValue.Length);
					this.KeyValue = null;
				}
				if (this.IVValue != null)
				{
					Array.Clear(this.IVValue, 0, this.IVValue.Length);
					this.IVValue = null;
				}
			}
		}

		/// <summary>Gets or sets the block size, in bits, of the cryptographic operation.</summary>
		/// <returns>The block size, in bits.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The block size is invalid.</exception>
		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x0600308D RID: 12429 RVA: 0x000B0D32 File Offset: 0x000AEF32
		// (set) Token: 0x0600308E RID: 12430 RVA: 0x000B0D3C File Offset: 0x000AEF3C
		public virtual int BlockSize
		{
			get
			{
				return this.BlockSizeValue;
			}
			set
			{
				for (int i = 0; i < this.LegalBlockSizesValue.Length; i++)
				{
					if (this.LegalBlockSizesValue[i].SkipSize == 0)
					{
						if (this.LegalBlockSizesValue[i].MinSize == value)
						{
							this.BlockSizeValue = value;
							this.IVValue = null;
							return;
						}
					}
					else
					{
						for (int j = this.LegalBlockSizesValue[i].MinSize; j <= this.LegalBlockSizesValue[i].MaxSize; j += this.LegalBlockSizesValue[i].SkipSize)
						{
							if (j == value)
							{
								if (this.BlockSizeValue != value)
								{
									this.BlockSizeValue = value;
									this.IVValue = null;
								}
								return;
							}
						}
					}
				}
				throw new CryptographicException(Environment.GetResourceString("Specified block size is not valid for this algorithm."));
			}
		}

		/// <summary>Gets or sets the feedback size, in bits, of the cryptographic operation.</summary>
		/// <returns>The feedback size in bits.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The feedback size is larger than the block size.</exception>
		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x0600308F RID: 12431 RVA: 0x000B0DE8 File Offset: 0x000AEFE8
		// (set) Token: 0x06003090 RID: 12432 RVA: 0x000B0DF0 File Offset: 0x000AEFF0
		public virtual int FeedbackSize
		{
			get
			{
				return this.FeedbackSizeValue;
			}
			set
			{
				if (value <= 0 || value > this.BlockSizeValue || value % 8 != 0)
				{
					throw new CryptographicException(Environment.GetResourceString("Specified feedback size is invalid."));
				}
				this.FeedbackSizeValue = value;
			}
		}

		/// <summary>Gets or sets the initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) for the symmetric algorithm.</summary>
		/// <returns>The initialization vector.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt was made to set the initialization vector to <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An attempt was made to set the initialization vector to an invalid size.</exception>
		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06003091 RID: 12433 RVA: 0x000B0E1B File Offset: 0x000AF01B
		// (set) Token: 0x06003092 RID: 12434 RVA: 0x000B0E3B File Offset: 0x000AF03B
		public virtual byte[] IV
		{
			get
			{
				if (this.IVValue == null)
				{
					this.GenerateIV();
				}
				return (byte[])this.IVValue.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length != this.BlockSizeValue / 8)
				{
					throw new CryptographicException(Environment.GetResourceString("Specified initialization vector (IV) does not match the block size for this algorithm."));
				}
				this.IVValue = (byte[])value.Clone();
			}
		}

		/// <summary>Gets or sets the secret key for the symmetric algorithm.</summary>
		/// <returns>The secret key to use for the symmetric algorithm.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt was made to set the key to <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key size is invalid.</exception>
		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06003093 RID: 12435 RVA: 0x0000DD46 File Offset: 0x0000BF46
		// (set) Token: 0x06003094 RID: 12436 RVA: 0x000B0E7C File Offset: 0x000AF07C
		public virtual byte[] Key
		{
			get
			{
				if (this.KeyValue == null)
				{
					this.GenerateKey();
				}
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!this.ValidKeySize(value.Length * 8))
				{
					throw new CryptographicException(Environment.GetResourceString("Specified key is not a valid size for this algorithm."));
				}
				this.KeyValue = (byte[])value.Clone();
				this.KeySizeValue = value.Length * 8;
			}
		}

		/// <summary>Gets the block sizes, in bits, that are supported by the symmetric algorithm.</summary>
		/// <returns>An array that contains the block sizes supported by the algorithm.</returns>
		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06003095 RID: 12437 RVA: 0x000B0ED0 File Offset: 0x000AF0D0
		public virtual KeySizes[] LegalBlockSizes
		{
			get
			{
				return (KeySizes[])this.LegalBlockSizesValue.Clone();
			}
		}

		/// <summary>Gets the key sizes, in bits, that are supported by the symmetric algorithm.</summary>
		/// <returns>An array that contains the key sizes supported by the algorithm.</returns>
		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06003096 RID: 12438 RVA: 0x000B0EE2 File Offset: 0x000AF0E2
		public virtual KeySizes[] LegalKeySizes
		{
			get
			{
				return (KeySizes[])this.LegalKeySizesValue.Clone();
			}
		}

		/// <summary>Gets or sets the size, in bits, of the secret key used by the symmetric algorithm.</summary>
		/// <returns>The size, in bits, of the secret key used by the symmetric algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key size is not valid.</exception>
		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06003097 RID: 12439 RVA: 0x000A8F8E File Offset: 0x000A718E
		// (set) Token: 0x06003098 RID: 12440 RVA: 0x000B0EF4 File Offset: 0x000AF0F4
		public virtual int KeySize
		{
			get
			{
				return this.KeySizeValue;
			}
			set
			{
				if (!this.ValidKeySize(value))
				{
					throw new CryptographicException(Environment.GetResourceString("Specified key is not a valid size for this algorithm."));
				}
				this.KeySizeValue = value;
				this.KeyValue = null;
			}
		}

		/// <summary>Gets or sets the mode for operation of the symmetric algorithm.</summary>
		/// <returns>The mode for operation of the symmetric algorithm. The default is <see cref="F:System.Security.Cryptography.CipherMode.CBC" />.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cipher mode is not one of the <see cref="T:System.Security.Cryptography.CipherMode" /> values.</exception>
		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06003099 RID: 12441 RVA: 0x000B0F1D File Offset: 0x000AF11D
		// (set) Token: 0x0600309A RID: 12442 RVA: 0x000B0F25 File Offset: 0x000AF125
		public virtual CipherMode Mode
		{
			get
			{
				return this.ModeValue;
			}
			set
			{
				if (value < CipherMode.CBC || CipherMode.CFB < value)
				{
					throw new CryptographicException(Environment.GetResourceString("Specified cipher mode is not valid for this algorithm."));
				}
				this.ModeValue = value;
			}
		}

		/// <summary>Gets or sets the padding mode used in the symmetric algorithm.</summary>
		/// <returns>The padding mode used in the symmetric algorithm. The default is <see cref="F:System.Security.Cryptography.PaddingMode.PKCS7" />.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The padding mode is not one of the <see cref="T:System.Security.Cryptography.PaddingMode" /> values.</exception>
		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x0600309B RID: 12443 RVA: 0x000B0F46 File Offset: 0x000AF146
		// (set) Token: 0x0600309C RID: 12444 RVA: 0x000B0F4E File Offset: 0x000AF14E
		public virtual PaddingMode Padding
		{
			get
			{
				return this.PaddingValue;
			}
			set
			{
				if (value < PaddingMode.None || PaddingMode.ISO10126 < value)
				{
					throw new CryptographicException(Environment.GetResourceString("Specified padding mode is not valid for this algorithm."));
				}
				this.PaddingValue = value;
			}
		}

		/// <summary>Determines whether the specified key size is valid for the current algorithm.</summary>
		/// <param name="bitLength">The length, in bits, to check for a valid key size.</param>
		/// <returns>
		///   <see langword="true" /> if the specified key size is valid for the current algorithm; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600309D RID: 12445 RVA: 0x000B0F70 File Offset: 0x000AF170
		public bool ValidKeySize(int bitLength)
		{
			KeySizes[] legalKeySizes = this.LegalKeySizes;
			if (legalKeySizes == null)
			{
				return false;
			}
			for (int i = 0; i < legalKeySizes.Length; i++)
			{
				if (legalKeySizes[i].SkipSize == 0)
				{
					if (legalKeySizes[i].MinSize == bitLength)
					{
						return true;
					}
				}
				else
				{
					for (int j = legalKeySizes[i].MinSize; j <= legalKeySizes[i].MaxSize; j += legalKeySizes[i].SkipSize)
					{
						if (j == bitLength)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		/// <summary>Creates a default cryptographic object used to perform the symmetric algorithm.</summary>
		/// <returns>A default cryptographic object used to perform the symmetric algorithm.</returns>
		// Token: 0x0600309E RID: 12446 RVA: 0x000B0FD6 File Offset: 0x000AF1D6
		public static SymmetricAlgorithm Create()
		{
			return SymmetricAlgorithm.Create("System.Security.Cryptography.SymmetricAlgorithm");
		}

		/// <summary>Creates the specified cryptographic object used to perform the symmetric algorithm.</summary>
		/// <param name="algName">The name of the specific implementation of the <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> class to use.</param>
		/// <returns>A cryptographic object used to perform the symmetric algorithm.</returns>
		// Token: 0x0600309F RID: 12447 RVA: 0x000B0FE2 File Offset: 0x000AF1E2
		public static SymmetricAlgorithm Create(string algName)
		{
			return (SymmetricAlgorithm)CryptoConfig.CreateFromName(algName);
		}

		/// <summary>Creates a symmetric encryptor object with the current <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> property and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <returns>A symmetric encryptor object.</returns>
		// Token: 0x060030A0 RID: 12448 RVA: 0x000B0FEF File Offset: 0x000AF1EF
		public virtual ICryptoTransform CreateEncryptor()
		{
			return this.CreateEncryptor(this.Key, this.IV);
		}

		/// <summary>When overridden in a derived class, creates a symmetric encryptor object with the specified <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> property and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <param name="rgbKey">The secret key to use for the symmetric algorithm.</param>
		/// <param name="rgbIV">The initialization vector to use for the symmetric algorithm.</param>
		/// <returns>A symmetric encryptor object.</returns>
		// Token: 0x060030A1 RID: 12449
		public abstract ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV);

		/// <summary>Creates a symmetric decryptor object with the current <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> property and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <returns>A symmetric decryptor object.</returns>
		// Token: 0x060030A2 RID: 12450 RVA: 0x000B1003 File Offset: 0x000AF203
		public virtual ICryptoTransform CreateDecryptor()
		{
			return this.CreateDecryptor(this.Key, this.IV);
		}

		/// <summary>When overridden in a derived class, creates a symmetric decryptor object with the specified <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> property and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <param name="rgbKey">The secret key to use for the symmetric algorithm.</param>
		/// <param name="rgbIV">The initialization vector to use for the symmetric algorithm.</param>
		/// <returns>A symmetric decryptor object.</returns>
		// Token: 0x060030A3 RID: 12451
		public abstract ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV);

		/// <summary>When overridden in a derived class, generates a random key (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) to use for the algorithm.</summary>
		// Token: 0x060030A4 RID: 12452
		public abstract void GenerateKey();

		/// <summary>When overridden in a derived class, generates a random initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) to use for the algorithm.</summary>
		// Token: 0x060030A5 RID: 12453
		public abstract void GenerateIV();

		/// <summary>Represents the block size, in bits, of the cryptographic operation.</summary>
		// Token: 0x040021EE RID: 8686
		protected int BlockSizeValue;

		/// <summary>Represents the feedback size, in bits, of the cryptographic operation.</summary>
		// Token: 0x040021EF RID: 8687
		protected int FeedbackSizeValue;

		/// <summary>Represents the initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) for the symmetric algorithm.</summary>
		// Token: 0x040021F0 RID: 8688
		protected byte[] IVValue;

		/// <summary>Represents the secret key for the symmetric algorithm.</summary>
		// Token: 0x040021F1 RID: 8689
		protected byte[] KeyValue;

		/// <summary>Specifies the block sizes, in bits, that are supported by the symmetric algorithm.</summary>
		// Token: 0x040021F2 RID: 8690
		protected KeySizes[] LegalBlockSizesValue;

		/// <summary>Specifies the key sizes, in bits, that are supported by the symmetric algorithm.</summary>
		// Token: 0x040021F3 RID: 8691
		protected KeySizes[] LegalKeySizesValue;

		/// <summary>Represents the size, in bits, of the secret key used by the symmetric algorithm.</summary>
		// Token: 0x040021F4 RID: 8692
		protected int KeySizeValue;

		/// <summary>Represents the cipher mode used in the symmetric algorithm.</summary>
		// Token: 0x040021F5 RID: 8693
		protected CipherMode ModeValue;

		/// <summary>Represents the padding mode used in the symmetric algorithm.</summary>
		// Token: 0x040021F6 RID: 8694
		protected PaddingMode PaddingValue;
	}
}
