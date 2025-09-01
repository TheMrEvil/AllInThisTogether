using System;

namespace System.Security.Cryptography
{
	/// <summary>Provides a Cryptography Next Generation (CNG) implementation of the Advanced Encryption Standard (AES) algorithm.</summary>
	// Token: 0x02000055 RID: 85
	public sealed class AesCng : Aes
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AesCng" /> class with an ephemeral key.</summary>
		// Token: 0x060001B5 RID: 437 RVA: 0x00003F0D File Offset: 0x0000210D
		public AesCng()
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AesCng" /> class with the specified key name, which represents an existing persisted AES key. </summary>
		/// <param name="keyName">The name of the key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="keyName" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">
		///         Cryptography Next Generation (CNG) is not supported on this system.
		///  </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">All other errors.
		/// </exception>
		// Token: 0x060001B6 RID: 438 RVA: 0x00003F0D File Offset: 0x0000210D
		public AesCng(string keyName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AesCng" /> class with the specified key name, which represents an existing persisted AES key, and the specified key storage provider (KSP).</summary>
		/// <param name="keyName">The name of the key.</param>
		/// <param name="provider">The KSP that contains the key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="keyName" /> is <see langword="null" />. -or-
		///         <paramref name="provider" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">
		///         Cryptography Next Generation (CNG) is not supported on this system.
		///  </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">All other errors.
		/// </exception>
		// Token: 0x060001B7 RID: 439 RVA: 0x00003F0D File Offset: 0x0000210D
		public AesCng(string keyName, CngProvider provider)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AesCng" /> class with the specified key name, which represents an existing persisted AES key,  the specified key storage provider (KSP) and key open options.</summary>
		/// <param name="keyName">The name of the key.</param>
		/// <param name="provider">The KSP that contains the key.</param>
		/// <param name="openOptions">A bitwise combination of the enumeration values that specify options for opening the key, such as where the key is opened from (machine or user storage) and whether to suppress UI prompting.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="keyName" /> is <see langword="null" />. -or-
		///         <paramref name="provider" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">
		///         Cryptography Next Generation (CNG) is not supported on this system.
		///  </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">All other errors.
		/// </exception>
		// Token: 0x060001B8 RID: 440 RVA: 0x00003F0D File Offset: 0x0000210D
		public AesCng(string keyName, CngProvider provider, CngKeyOpenOptions openOptions)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets or sets the key for the <see cref="T:System.Security.Cryptography.AesCng" /> algorithm.</summary>
		/// <returns>The key for the <see cref="T:System.Security.Cryptography.AesCng" /> algorithm.</returns>
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x000023CA File Offset: 0x000005CA
		// (set) Token: 0x060001BA RID: 442 RVA: 0x000023CA File Offset: 0x000005CA
		public override byte[] Key
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the size, in bits, of the secret key used by the <see cref="T:System.Security.Cryptography.AesCng" /> algorithm.</summary>
		/// <returns>The size, in bits, of the secret key used by the <see cref="T:System.Security.Cryptography.AesCng" /> algorithm.</returns>
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001BB RID: 443 RVA: 0x000023CA File Offset: 0x000005CA
		// (set) Token: 0x060001BC RID: 444 RVA: 0x000023CA File Offset: 0x000005CA
		public override int KeySize
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Creates a symmetric AES decryptor object with the current key and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <returns>A symmetric AES decryptor object.</returns>
		// Token: 0x060001BD RID: 445 RVA: 0x000023CA File Offset: 0x000005CA
		public override ICryptoTransform CreateDecryptor()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a symmetric AES decryptor object with the specified key and initialization vector (IV).</summary>
		/// <param name="rgbKey">The secret key to use for the AES algorithm. The key size must be 128, 192, or 256 bits.</param>
		/// <param name="rgbIV">The initialization vector to use for the AES algorithm.</param>
		/// <returns>A symmetric AES decryptor object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="rgbKey" />  is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="rgbKey" />  is not a valid size for this algorithm.-or-<paramref name="rgbIV" /> size does not match the block size for this algorithm.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <paramref name="rgbKey" /> is a known weak key for this algorithm and cannot be used.-or-
		///         <paramref name="rgbIV" />  is <see langword="null" />.</exception>
		// Token: 0x060001BE RID: 446 RVA: 0x000023CA File Offset: 0x000005CA
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a symmetric AES encryptor object using the current key and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <returns>A symmetric AES encryptor object.</returns>
		// Token: 0x060001BF RID: 447 RVA: 0x000023CA File Offset: 0x000005CA
		public override ICryptoTransform CreateEncryptor()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a symmetric AES encryptor object with the specified key and initialization vector (IV).</summary>
		/// <param name="rgbKey">The secret key to use for the AES algorithm. The key size must be 128, 192, or 256 bits.</param>
		/// <param name="rgbIV">The initialization vector to use for the AES algorithm.</param>
		/// <returns>A symmetric AES encryptor object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="rgbKey" />  is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="rgbKey" />  is not a valid size for this algorithm.-or-<paramref name="rgbIV" /> size does not match the block size for this algorithm.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <paramref name="rgbKey" /> is a known weak key for this algorithm and cannot be used.-or-
		///         <paramref name="rgbIV" />  is <see langword="null" />.</exception>
		// Token: 0x060001C0 RID: 448 RVA: 0x0000392D File Offset: 0x00001B2D
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return null;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000023CA File Offset: 0x000005CA
		protected override void Dispose(bool disposing)
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates a random initialization vector (IV) to use for the AES algorithm.</summary>
		// Token: 0x060001C2 RID: 450 RVA: 0x000023CA File Offset: 0x000005CA
		public override void GenerateIV()
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates a random key to use for the AES algorithm.</summary>
		// Token: 0x060001C3 RID: 451 RVA: 0x000023CA File Offset: 0x000005CA
		public override void GenerateKey()
		{
			throw new NotImplementedException();
		}
	}
}
