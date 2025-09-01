using System;

namespace System.Security.Cryptography
{
	/// <summary>Provides a Cryptography Next Generation (CNG) implementation of the RSA algorithm. </summary>
	// Token: 0x02000054 RID: 84
	public sealed class RSACng : RSA
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSACng" /> class with a random 2,048-bit key pair. </summary>
		// Token: 0x060001AE RID: 430 RVA: 0x00003EF3 File Offset: 0x000020F3
		public RSACng() : this(2048)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSACng" /> class with a randomly generated key of the specified size. </summary>
		/// <param name="keySize">The size of the key to generate in bits. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///         <paramref name="keySize" /> is not valid. </exception>
		// Token: 0x060001AF RID: 431 RVA: 0x00003F00 File Offset: 0x00002100
		public RSACng(int keySize)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSACng" /> class with the specified key. </summary>
		/// <param name="key">The key to use for RSA operations. </param>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="key" /> is not a valid RSA key. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="key" /> is <see langword="null" />. </exception>
		// Token: 0x060001B0 RID: 432 RVA: 0x00003F00 File Offset: 0x00002100
		public RSACng(CngKey key)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the key that will be used by the <see cref="T:System.Security.Cryptography.RSACng" /> object for any cryptographic operation that it performs. </summary>
		/// <returns>The key used by the <see cref="T:System.Security.Cryptography.RSACng" /> object. </returns>
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x000023CA File Offset: 0x000005CA
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x000023CA File Offset: 0x000005CA
		public CngKey Key
		{
			[SecuritySafeCritical]
			get
			{
				throw new NotImplementedException();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Exports the key used by the RSA object into a <see cref="T:System.Security.Cryptography.RSAParameters" /> object. </summary>
		/// <param name="includePrivateParameters">
		///       <see langword="true" /> to include private parameters; otherwise, <see langword="false" />. </param>
		/// <returns>The key used by the RSA object. </returns>
		// Token: 0x060001B3 RID: 435 RVA: 0x000023CA File Offset: 0x000005CA
		public override RSAParameters ExportParameters(bool includePrivateParameters)
		{
			throw new NotImplementedException();
		}

		/// <summary>Replaces the existing key that the current instance is working with by creating a new <see cref="T:System.Security.Cryptography.CngKey" /> for the parameters structure. </summary>
		/// <param name="parameters">The RSA parameters. </param>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="parameters" /> contains neither an exponent nor a modulus. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///         <paramref name="parameters" /> is not a valid RSA key. -or-
		///         <paramref name="parameters" /> is a full key pair and the default KSP is used. </exception>
		// Token: 0x060001B4 RID: 436 RVA: 0x000023CA File Offset: 0x000005CA
		public override void ImportParameters(RSAParameters parameters)
		{
			throw new NotImplementedException();
		}
	}
}
