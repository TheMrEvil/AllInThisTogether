using System;

namespace System.Security.Cryptography
{
	/// <summary>Provides a Cryptography Next Generation (CNG) implementation of the Triple Data Encryption Standard (3DES) algorithm.</summary>
	// Token: 0x02000060 RID: 96
	public sealed class TripleDESCng : TripleDES
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.TripleDESCng" /> class with an ephemeral key.</summary>
		// Token: 0x0600020F RID: 527 RVA: 0x00006184 File Offset: 0x00004384
		public TripleDESCng()
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.TripleDESCng" /> class with the specified key name, which represents an existing persisted 3DES key.</summary>
		/// <param name="keyName">The name of the key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="keyName" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">
		///         Cryptography Next Generation (CNG) is not supported on this system.
		///  </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">All other errors.
		/// </exception>
		// Token: 0x06000210 RID: 528 RVA: 0x00006184 File Offset: 0x00004384
		public TripleDESCng(string keyName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.TripleDESCng" /> class with the specified key name, which represents an existing persisted 3DES key, and the specified key storage provider (KSP).</summary>
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
		// Token: 0x06000211 RID: 529 RVA: 0x00006184 File Offset: 0x00004384
		public TripleDESCng(string keyName, CngProvider provider)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.TripleDESCng" /> class with the specified key name, which represents an existing persisted 3DES key,  the specified key storage provider (KSP) and key open options.</summary>
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
		// Token: 0x06000212 RID: 530 RVA: 0x00006184 File Offset: 0x00004384
		public TripleDESCng(string keyName, CngProvider provider, CngKeyOpenOptions openOptions)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets or sets the key for the <see cref="T:System.Security.Cryptography.TripleDESCng" /> algorithm.</summary>
		/// <returns>The key for the <see cref="T:System.Security.Cryptography.TripleDESCng" /> algorithm.</returns>
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000213 RID: 531 RVA: 0x000023CA File Offset: 0x000005CA
		// (set) Token: 0x06000214 RID: 532 RVA: 0x000023CA File Offset: 0x000005CA
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

		/// <summary>Gets or sets the size, in bits, of the secret key used by the <see cref="T:System.Security.Cryptography.TripleDESCng" /> algorithm.</summary>
		/// <returns>The size, in bits, of the secret key used by the <see cref="T:System.Security.Cryptography.TripleDESCng" /> algorithm.</returns>
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000215 RID: 533 RVA: 0x000023CA File Offset: 0x000005CA
		// (set) Token: 0x06000216 RID: 534 RVA: 0x000023CA File Offset: 0x000005CA
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

		/// <summary>Creates a symmetric 3DES decryptor object with the current key and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <returns>A symmetric 3DES decryptor object.</returns>
		// Token: 0x06000217 RID: 535 RVA: 0x000023CA File Offset: 0x000005CA
		public override ICryptoTransform CreateDecryptor()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a symmetric 3DES decryptor object with the specified key and initialization vector (IV).</summary>
		/// <param name="rgbKey">The secret key to use for the 3DES algorithm. The key size must be 192 bits.</param>
		/// <param name="rgbIV">The initialization vector to use for the 3DES algorithm.</param>
		/// <returns>A symmetric 3DES decryptor object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="rgbKey" />  is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="rgbKey" />  is not a valid size for this algorithm.-or-<paramref name="rgbIV" /> size does not match the block size for this algorithm.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <paramref name="rgbKey" /> is a known weak key for this algorithm and cannot be used.-or-
		///         <paramref name="rgbIV" />  is <see langword="null" />.</exception>
		// Token: 0x06000218 RID: 536 RVA: 0x000023CA File Offset: 0x000005CA
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a symmetric 3DES encryptor object using the current key and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <returns>A symmetric 3DES encryptor object.</returns>
		// Token: 0x06000219 RID: 537 RVA: 0x000023CA File Offset: 0x000005CA
		public override ICryptoTransform CreateEncryptor()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a symmetric 3DES encryptor object with the specified key and initialization vector (IV).</summary>
		/// <param name="rgbKey">The secret key to use for the 3DES algorithm. The key size must be 192 bits.</param>
		/// <param name="rgbIV">The initialization vector to use for the 3DES algorithm.</param>
		/// <returns>A symmetric 3DES encryptor object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="rgbKey" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="rgbKey" /> is not a valid size for this algorithm.-or-<paramref name="rgbIV" /> size does not match the block size for this algorithm.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <paramref name="rgbKey" /> is a known weak key for this algorithm and cannot be used.-or-
		///         <paramref name="rgbIV" />  is <see langword="null" />.</exception>
		// Token: 0x0600021A RID: 538 RVA: 0x0000392D File Offset: 0x00001B2D
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return null;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000023CA File Offset: 0x000005CA
		protected override void Dispose(bool disposing)
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates a random initialization vector (IV) to use for the 3DES algorithm.</summary>
		// Token: 0x0600021C RID: 540 RVA: 0x000023CA File Offset: 0x000005CA
		public override void GenerateIV()
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates a random key to use for the 3DES algorithm.</summary>
		// Token: 0x0600021D RID: 541 RVA: 0x000023CA File Offset: 0x000005CA
		public override void GenerateKey()
		{
			throw new NotImplementedException();
		}
	}
}
