using System;

namespace System.Security.Cryptography
{
	/// <summary>Provides a managed implementation of the Advanced Encryption Standard (AES) symmetric algorithm. </summary>
	// Token: 0x02000038 RID: 56
	public sealed class AesManaged : Aes
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AesManaged" /> class. </summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The Windows security policy setting for FIPS is enabled.</exception>
		/// <exception cref="T:System.InvalidOperationException">This implementation is not part of the Windows Platform FIPS-validated cryptographic algorithms.</exception>
		// Token: 0x060000C7 RID: 199 RVA: 0x00002C4C File Offset: 0x00000E4C
		public AesManaged()
		{
			if (CryptoConfig.AllowOnlyFipsAlgorithms)
			{
				throw new InvalidOperationException(SR.GetString("This implementation is not part of the Windows Platform FIPS validated cryptographic algorithms."));
			}
			this.m_rijndael = new RijndaelManaged();
			this.m_rijndael.BlockSize = this.BlockSize;
			this.m_rijndael.KeySize = this.KeySize;
		}

		/// <summary>Gets or sets the number of bits to use as feedback. </summary>
		/// <returns>The feedback size, in bits.</returns>
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00002CA3 File Offset: 0x00000EA3
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00002CB0 File Offset: 0x00000EB0
		public override int FeedbackSize
		{
			get
			{
				return this.m_rijndael.FeedbackSize;
			}
			set
			{
				this.m_rijndael.FeedbackSize = value;
			}
		}

		/// <summary>Gets or sets the initialization vector (IV) to use for the symmetric algorithm. </summary>
		/// <returns>The initialization vector to use for the symmetric algorithm</returns>
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00002CBE File Offset: 0x00000EBE
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00002CCB File Offset: 0x00000ECB
		public override byte[] IV
		{
			get
			{
				return this.m_rijndael.IV;
			}
			set
			{
				this.m_rijndael.IV = value;
			}
		}

		/// <summary>Gets or sets the secret key used for the symmetric algorithm.</summary>
		/// <returns>The key for the symmetric algorithm.</returns>
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00002CD9 File Offset: 0x00000ED9
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00002CE6 File Offset: 0x00000EE6
		public override byte[] Key
		{
			get
			{
				return this.m_rijndael.Key;
			}
			set
			{
				this.m_rijndael.Key = value;
			}
		}

		/// <summary>Gets or sets the size, in bits, of the secret key used for the symmetric algorithm. </summary>
		/// <returns>The size, in bits, of the key used by the symmetric algorithm.</returns>
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00002CF4 File Offset: 0x00000EF4
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00002D01 File Offset: 0x00000F01
		public override int KeySize
		{
			get
			{
				return this.m_rijndael.KeySize;
			}
			set
			{
				this.m_rijndael.KeySize = value;
			}
		}

		/// <summary>Gets or sets the mode for operation of the symmetric algorithm.</summary>
		/// <returns>One of the enumeration values that specifies the block cipher mode to use for encryption. The default is <see cref="F:System.Security.Cryptography.CipherMode.CBC" />.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///         <see cref="P:System.Security.Cryptography.AesManaged.Mode" /> is set to <see cref="F:System.Security.Cryptography.CipherMode.CFB" /> or <see cref="F:System.Security.Cryptography.CipherMode.OFB" />.</exception>
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00002D0F File Offset: 0x00000F0F
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00002D1C File Offset: 0x00000F1C
		public override CipherMode Mode
		{
			get
			{
				return this.m_rijndael.Mode;
			}
			set
			{
				if (value == CipherMode.CFB || value == CipherMode.OFB)
				{
					throw new CryptographicException(SR.GetString("Specified cipher mode is not valid for this algorithm."));
				}
				this.m_rijndael.Mode = value;
			}
		}

		/// <summary>Gets or sets the padding mode used in the symmetric algorithm. </summary>
		/// <returns>One of the enumeration values that specifies the type of padding to apply. The default is <see cref="F:System.Security.Cryptography.PaddingMode.PKCS7" />.</returns>
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00002D42 File Offset: 0x00000F42
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00002D4F File Offset: 0x00000F4F
		public override PaddingMode Padding
		{
			get
			{
				return this.m_rijndael.Padding;
			}
			set
			{
				this.m_rijndael.Padding = value;
			}
		}

		/// <summary>Creates a symmetric decryptor object using the current key and initialization vector (IV).</summary>
		/// <returns>A symmetric decryptor object.</returns>
		// Token: 0x060000D4 RID: 212 RVA: 0x00002D5D File Offset: 0x00000F5D
		public override ICryptoTransform CreateDecryptor()
		{
			return this.m_rijndael.CreateDecryptor();
		}

		/// <summary>Creates a symmetric decryptor object using the specified key and initialization vector (IV).</summary>
		/// <param name="key">The secret key to use for the symmetric algorithm.</param>
		/// <param name="iv">The initialization vector to use for the symmetric algorithm.</param>
		/// <returns>A symmetric decryptor object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="key" /> or <paramref name="iv" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="key" /> is invalid.</exception>
		// Token: 0x060000D5 RID: 213 RVA: 0x00002D6C File Offset: 0x00000F6C
		public override ICryptoTransform CreateDecryptor(byte[] key, byte[] iv)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (!base.ValidKeySize(key.Length * 8))
			{
				throw new ArgumentException(SR.GetString("Specified key is not a valid size for this algorithm."), "key");
			}
			if (iv != null && iv.Length * 8 != this.BlockSizeValue)
			{
				throw new ArgumentException(SR.GetString("Specified initialization vector (IV) does not match the block size for this algorithm."), "iv");
			}
			return this.m_rijndael.CreateDecryptor(key, iv);
		}

		/// <summary>Creates a symmetric encryptor object using the current key and initialization vector (IV).</summary>
		/// <returns>A symmetric encryptor object.</returns>
		// Token: 0x060000D6 RID: 214 RVA: 0x00002DDB File Offset: 0x00000FDB
		public override ICryptoTransform CreateEncryptor()
		{
			return this.m_rijndael.CreateEncryptor();
		}

		/// <summary>Creates a symmetric encryptor object using the specified key and initialization vector (IV).</summary>
		/// <param name="key">The secret key to use for the symmetric algorithm.</param>
		/// <param name="iv">The initialization vector to use for the symmetric algorithm.</param>
		/// <returns>A symmetric encryptor object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="key" /> or <paramref name="iv" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="key" /> is invalid.</exception>
		// Token: 0x060000D7 RID: 215 RVA: 0x00002DE8 File Offset: 0x00000FE8
		public override ICryptoTransform CreateEncryptor(byte[] key, byte[] iv)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (!base.ValidKeySize(key.Length * 8))
			{
				throw new ArgumentException(SR.GetString("Specified key is not a valid size for this algorithm."), "key");
			}
			if (iv != null && iv.Length * 8 != this.BlockSizeValue)
			{
				throw new ArgumentException(SR.GetString("Specified initialization vector (IV) does not match the block size for this algorithm."), "iv");
			}
			return this.m_rijndael.CreateEncryptor(key, iv);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00002E58 File Offset: 0x00001058
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					((IDisposable)this.m_rijndael).Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>Generates a random initialization vector (IV) to use for the symmetric algorithm.</summary>
		// Token: 0x060000D9 RID: 217 RVA: 0x00002E90 File Offset: 0x00001090
		public override void GenerateIV()
		{
			this.m_rijndael.GenerateIV();
		}

		/// <summary>Generates a random key to use for the symmetric algorithm. </summary>
		// Token: 0x060000DA RID: 218 RVA: 0x00002E9D File Offset: 0x0000109D
		public override void GenerateKey()
		{
			this.m_rijndael.GenerateKey();
		}

		// Token: 0x040002DA RID: 730
		private RijndaelManaged m_rijndael;
	}
}
