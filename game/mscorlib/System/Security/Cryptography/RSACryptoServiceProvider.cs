using System;
using System.IO;
using System.Runtime.InteropServices;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Performs asymmetric encryption and decryption using the implementation of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm provided by the cryptographic service provider (CSP). This class cannot be inherited.</summary>
	// Token: 0x020004AB RID: 1195
	[ComVisible(true)]
	public sealed class RSACryptoServiceProvider : RSA, ICspAsymmetricAlgorithm
	{
		/// <summary>Gets the name of the signature algorithm available with this implementation of <see cref="T:System.Security.Cryptography.RSA" />.</summary>
		/// <returns>The name of the signature algorithm.</returns>
		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06002FDA RID: 12250 RVA: 0x000110AD File Offset: 0x0000F2AD
		public override string SignatureAlgorithm
		{
			get
			{
				return "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
			}
		}

		/// <summary>Gets or sets a value indicating whether the key should be persisted in the computer's key store instead of the user profile store.</summary>
		/// <returns>
		///   <see langword="true" /> if the key should be persisted in the computer key store; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06002FDB RID: 12251 RVA: 0x000ADC8D File Offset: 0x000ABE8D
		// (set) Token: 0x06002FDC RID: 12252 RVA: 0x000ADC99 File Offset: 0x000ABE99
		public static bool UseMachineKeyStore
		{
			get
			{
				return RSACryptoServiceProvider.s_UseMachineKeyStore == CspProviderFlags.UseMachineKeyStore;
			}
			set
			{
				RSACryptoServiceProvider.s_UseMachineKeyStore = (value ? CspProviderFlags.UseMachineKeyStore : CspProviderFlags.NoFlags);
			}
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x000ADCA9 File Offset: 0x000ABEA9
		[SecuritySafeCritical]
		protected override byte[] HashData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm)
		{
			return HashAlgorithm.Create(hashAlgorithm.Name).ComputeHash(data, offset, count);
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x000ADCBF File Offset: 0x000ABEBF
		[SecuritySafeCritical]
		protected override byte[] HashData(Stream data, HashAlgorithmName hashAlgorithm)
		{
			return HashAlgorithm.Create(hashAlgorithm.Name).ComputeHash(data);
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x000ADCD4 File Offset: 0x000ABED4
		private static int GetAlgorithmId(HashAlgorithmName hashAlgorithm)
		{
			string name = hashAlgorithm.Name;
			if (name == "MD5")
			{
				return 32771;
			}
			if (name == "SHA1")
			{
				return 32772;
			}
			if (name == "SHA256")
			{
				return 32780;
			}
			if (name == "SHA384")
			{
				return 32781;
			}
			if (!(name == "SHA512"))
			{
				throw new CryptographicException(Environment.GetResourceString("'{0}' is not a known hash algorithm.", new object[]
				{
					hashAlgorithm.Name
				}));
			}
			return 32782;
		}

		/// <summary>Encrypts data with the <see cref="T:System.Security.Cryptography.RSA" /> algorithm using the specified padding.</summary>
		/// <param name="data">The data to encrypt.</param>
		/// <param name="padding">The padding.</param>
		/// <returns>The encrypted data.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="padding" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The padding mode is not supported.</exception>
		// Token: 0x06002FE0 RID: 12256 RVA: 0x000ADD6C File Offset: 0x000ABF6C
		public override byte[] Encrypt(byte[] data, RSAEncryptionPadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			if (padding == RSAEncryptionPadding.Pkcs1)
			{
				return this.Encrypt(data, false);
			}
			if (padding == RSAEncryptionPadding.OaepSHA1)
			{
				return this.Encrypt(data, true);
			}
			throw RSACryptoServiceProvider.PaddingModeNotSupported();
		}

		/// <summary>Decrypts data that was previously encrypted with the <see cref="T:System.Security.Cryptography.RSA" /> algorithm by using the specified padding.</summary>
		/// <param name="data">The data to decrypt.</param>
		/// <param name="padding">The padding.</param>
		/// <returns>The decrypted data.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="padding" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The padding mode is not supported.</exception>
		// Token: 0x06002FE1 RID: 12257 RVA: 0x000ADDCC File Offset: 0x000ABFCC
		public override byte[] Decrypt(byte[] data, RSAEncryptionPadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			if (padding == RSAEncryptionPadding.Pkcs1)
			{
				return this.Decrypt(data, false);
			}
			if (padding == RSAEncryptionPadding.OaepSHA1)
			{
				return this.Decrypt(data, true);
			}
			throw RSACryptoServiceProvider.PaddingModeNotSupported();
		}

		/// <summary>Computes the signature for the specified hash value by encrypting it with the private key using the specified padding.</summary>
		/// <param name="hash">The hash value of the data to be signed.</param>
		/// <param name="hashAlgorithm">The hash algorithm name used to create the hash value of the data.</param>
		/// <param name="padding">The padding.</param>
		/// <returns>The <see cref="T:System.Security.Cryptography.RSA" /> signature for the specified hash value.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="hashAlgorithm" /> is <see langword="null" /> or <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hash" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="padding" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <paramref name="padding" /> does not equal <see cref="P:System.Security.Cryptography.RSASignaturePadding.Pkcs1" />.</exception>
		// Token: 0x06002FE2 RID: 12258 RVA: 0x000ADE2C File Offset: 0x000AC02C
		public override byte[] SignHash(byte[] hash, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (hash == null)
			{
				throw new ArgumentNullException("hash");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw RSA.HashAlgorithmNameNullOrEmpty();
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			if (padding != RSASignaturePadding.Pkcs1)
			{
				throw RSACryptoServiceProvider.PaddingModeNotSupported();
			}
			return this.SignHash(hash, RSACryptoServiceProvider.GetAlgorithmId(hashAlgorithm));
		}

		/// <summary>Verifies that a digital signature is valid by determining the hash value in the signature using the specified hashing algorithm and padding, and comparing it to the provided hash value.</summary>
		/// <param name="hash">The hash value of the signed data.</param>
		/// <param name="signature">The signature data to be verified.</param>
		/// <param name="hashAlgorithm">The hash algorithm name used to create the hash value.</param>
		/// <param name="padding">The padding.</param>
		/// <returns>
		///   <see langword="true" /> if the signature is valid; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="hashAlgorithm" /> is <see langword="null" /> or <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hash" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="padding" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <paramref name="padding" /> does not equal <see cref="P:System.Security.Cryptography.RSASignaturePadding.Pkcs1" />.</exception>
		// Token: 0x06002FE3 RID: 12259 RVA: 0x000ADE90 File Offset: 0x000AC090
		public override bool VerifyHash(byte[] hash, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (hash == null)
			{
				throw new ArgumentNullException("hash");
			}
			if (signature == null)
			{
				throw new ArgumentNullException("signature");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw RSA.HashAlgorithmNameNullOrEmpty();
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			if (padding != RSASignaturePadding.Pkcs1)
			{
				throw RSACryptoServiceProvider.PaddingModeNotSupported();
			}
			return this.VerifyHash(hash, RSACryptoServiceProvider.GetAlgorithmId(hashAlgorithm), signature);
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x000ADF04 File Offset: 0x000AC104
		private static Exception PaddingModeNotSupported()
		{
			return new CryptographicException(Environment.GetResourceString("Specified padding mode is not valid for this algorithm."));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> class using the default key.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.</exception>
		// Token: 0x06002FE5 RID: 12261 RVA: 0x000ADF15 File Offset: 0x000AC115
		public RSACryptoServiceProvider() : this(1024)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> class with the specified parameters.</summary>
		/// <param name="parameters">The parameters to be passed to the cryptographic service provider (CSP).</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The CSP cannot be acquired.</exception>
		// Token: 0x06002FE6 RID: 12262 RVA: 0x000ADF22 File Offset: 0x000AC122
		public RSACryptoServiceProvider(CspParameters parameters) : this(1024, parameters)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> class with the specified key size.</summary>
		/// <param name="dwKeySize">The size of the key to use in bits.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.</exception>
		// Token: 0x06002FE7 RID: 12263 RVA: 0x000ADF30 File Offset: 0x000AC130
		public RSACryptoServiceProvider(int dwKeySize)
		{
			this.privateKeyExportable = true;
			base..ctor();
			this.Common(dwKeySize, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> class with the specified key size and parameters.</summary>
		/// <param name="dwKeySize">The size of the key to use in bits.</param>
		/// <param name="parameters">The parameters to be passed to the cryptographic service provider (CSP).</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The CSP cannot be acquired.  
		///  -or-  
		///  The key cannot be created.</exception>
		// Token: 0x06002FE8 RID: 12264 RVA: 0x000ADF48 File Offset: 0x000AC148
		public RSACryptoServiceProvider(int dwKeySize, CspParameters parameters)
		{
			this.privateKeyExportable = true;
			base..ctor();
			bool flag = parameters != null;
			this.Common(dwKeySize, flag);
			if (flag)
			{
				this.Common(parameters);
			}
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x000ADF7C File Offset: 0x000AC17C
		private void Common(int dwKeySize, bool parameters)
		{
			this.LegalKeySizesValue = new KeySizes[1];
			this.LegalKeySizesValue[0] = new KeySizes(384, 16384, 8);
			base.KeySize = dwKeySize;
			this.rsa = new RSAManaged(this.KeySize);
			this.rsa.KeyGenerated += this.OnKeyGenerated;
			this.persistKey = parameters;
			if (parameters)
			{
				return;
			}
			CspParameters cspParameters = new CspParameters(1);
			if (RSACryptoServiceProvider.UseMachineKeyStore)
			{
				cspParameters.Flags |= CspProviderFlags.UseMachineKeyStore;
			}
			this.store = new KeyPairPersistence(cspParameters);
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x000AE010 File Offset: 0x000AC210
		private void Common(CspParameters p)
		{
			this.store = new KeyPairPersistence(p);
			bool flag = this.store.Load();
			bool flag2 = (p.Flags & CspProviderFlags.UseExistingKey) > CspProviderFlags.NoFlags;
			this.privateKeyExportable = ((p.Flags & CspProviderFlags.UseNonExportableKey) == CspProviderFlags.NoFlags);
			if (flag2 && !flag)
			{
				throw new CryptographicException("Keyset does not exist");
			}
			if (this.store.KeyValue != null)
			{
				this.persisted = true;
				this.FromXmlString(this.store.KeyValue);
			}
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x000AE088 File Offset: 0x000AC288
		~RSACryptoServiceProvider()
		{
			this.Dispose(false);
		}

		/// <summary>Gets the name of the key exchange algorithm available with this implementation of <see cref="T:System.Security.Cryptography.RSA" />.</summary>
		/// <returns>The name of the key exchange algorithm if it exists; otherwise, <see langword="null" />.</returns>
		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06002FEC RID: 12268 RVA: 0x0001107E File Offset: 0x0000F27E
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return "RSA-PKCS1-KeyEx";
			}
		}

		/// <summary>Gets the size of the current key.</summary>
		/// <returns>The size of the key in bits.</returns>
		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06002FED RID: 12269 RVA: 0x000AE0B8 File Offset: 0x000AC2B8
		public override int KeySize
		{
			get
			{
				if (this.rsa == null)
				{
					return this.KeySizeValue;
				}
				return this.rsa.KeySize;
			}
		}

		/// <summary>Gets or sets a value indicating whether the key should be persisted in the cryptographic service provider (CSP).</summary>
		/// <returns>
		///   <see langword="true" /> if the key should be persisted in the CSP; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06002FEE RID: 12270 RVA: 0x000AE0D4 File Offset: 0x000AC2D4
		// (set) Token: 0x06002FEF RID: 12271 RVA: 0x000AE0DC File Offset: 0x000AC2DC
		public bool PersistKeyInCsp
		{
			get
			{
				return this.persistKey;
			}
			set
			{
				this.persistKey = value;
				if (this.persistKey)
				{
					this.OnKeyGenerated(this.rsa, null);
				}
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> object contains only a public key.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> object contains only a public key; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06002FF0 RID: 12272 RVA: 0x000AE0FA File Offset: 0x000AC2FA
		[ComVisible(false)]
		public bool PublicOnly
		{
			get
			{
				return this.rsa.PublicOnly;
			}
		}

		/// <summary>Decrypts data with the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		/// <param name="rgb">The data to be decrypted.</param>
		/// <param name="fOAEP">
		///   <see langword="true" /> to perform direct <see cref="T:System.Security.Cryptography.RSA" /> decryption using OAEP padding (only available on a computer running Microsoft Windows XP or later); otherwise, <see langword="false" /> to use PKCS#1 v1.5 padding.</param>
		/// <returns>The decrypted data, which is the original plain text before encryption.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.  
		///  -or-  
		///  The <paramref name="fOAEP" /> parameter is <see langword="true" /> and the length of the <paramref name="rgb" /> parameter is greater than <see cref="P:System.Security.Cryptography.RSACryptoServiceProvider.KeySize" />.  
		///  -or-  
		///  The <paramref name="fOAEP" /> parameter is <see langword="true" /> and OAEP is not supported.  
		///  -or-  
		///  The key does not match the encrypted data. However, the exception wording may not be accurate. For example, it may say Not enough storage is available to process this command.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rgb" /> is <see langword="null" />.</exception>
		// Token: 0x06002FF1 RID: 12273 RVA: 0x000AE108 File Offset: 0x000AC308
		public byte[] Decrypt(byte[] rgb, bool fOAEP)
		{
			if (rgb == null)
			{
				throw new ArgumentNullException("rgb");
			}
			if (rgb.Length > this.KeySize / 8)
			{
				throw new CryptographicException(Environment.GetResourceString("The data to be decrypted exceeds the maximum for this modulus of {0} bytes.", new object[]
				{
					this.KeySize / 8
				}));
			}
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("rsa");
			}
			AsymmetricKeyExchangeDeformatter asymmetricKeyExchangeDeformatter;
			if (fOAEP)
			{
				asymmetricKeyExchangeDeformatter = new RSAOAEPKeyExchangeDeformatter(this.rsa);
			}
			else
			{
				asymmetricKeyExchangeDeformatter = new RSAPKCS1KeyExchangeDeformatter(this.rsa);
			}
			return asymmetricKeyExchangeDeformatter.DecryptKeyExchange(rgb);
		}

		/// <summary>This method is not supported in the current version.</summary>
		/// <param name="rgb">The data to be decrypted.</param>
		/// <returns>The decrypted data, which is the original plain text before encryption.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported in the current version.</exception>
		// Token: 0x06002FF2 RID: 12274 RVA: 0x000AE18F File Offset: 0x000AC38F
		public override byte[] DecryptValue(byte[] rgb)
		{
			if (!this.rsa.IsCrtPossible)
			{
				throw new CryptographicException("Incomplete private key - missing CRT.");
			}
			return this.rsa.DecryptValue(rgb);
		}

		/// <summary>Encrypts data with the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		/// <param name="rgb">The data to be encrypted.</param>
		/// <param name="fOAEP">
		///   <see langword="true" /> to perform direct <see cref="T:System.Security.Cryptography.RSA" /> encryption using OAEP padding (only available on a computer running Windows XP or later); otherwise, <see langword="false" /> to use PKCS#1 v1.5 padding.</param>
		/// <returns>The encrypted data.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.  
		///  -or-  
		///  The length of the <paramref name="rgb" /> parameter is greater than the maximum allowed length.  
		///  -or-  
		///  The <paramref name="fOAEP" /> parameter is <see langword="true" /> and OAEP padding is not supported.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rgb" /> is <see langword="null" />.</exception>
		// Token: 0x06002FF3 RID: 12275 RVA: 0x000AE1B8 File Offset: 0x000AC3B8
		public byte[] Encrypt(byte[] rgb, bool fOAEP)
		{
			AsymmetricKeyExchangeFormatter asymmetricKeyExchangeFormatter;
			if (fOAEP)
			{
				asymmetricKeyExchangeFormatter = new RSAOAEPKeyExchangeFormatter(this.rsa);
			}
			else
			{
				asymmetricKeyExchangeFormatter = new RSAPKCS1KeyExchangeFormatter(this.rsa);
			}
			return asymmetricKeyExchangeFormatter.CreateKeyExchange(rgb);
		}

		/// <summary>This method is not supported in the current version.</summary>
		/// <param name="rgb">The data to be encrypted.</param>
		/// <returns>The encrypted data.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported in the current version.</exception>
		// Token: 0x06002FF4 RID: 12276 RVA: 0x000AE1EB File Offset: 0x000AC3EB
		public override byte[] EncryptValue(byte[] rgb)
		{
			return this.rsa.EncryptValue(rgb);
		}

		/// <summary>Exports the <see cref="T:System.Security.Cryptography.RSAParameters" />.</summary>
		/// <param name="includePrivateParameters">
		///   <see langword="true" /> to include private parameters; otherwise, <see langword="false" />.</param>
		/// <returns>The parameters for <see cref="T:System.Security.Cryptography.RSA" />.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key cannot be exported.</exception>
		// Token: 0x06002FF5 RID: 12277 RVA: 0x000AE1FC File Offset: 0x000AC3FC
		public override RSAParameters ExportParameters(bool includePrivateParameters)
		{
			if (includePrivateParameters && !this.privateKeyExportable)
			{
				throw new CryptographicException("cannot export private key");
			}
			RSAParameters rsaparameters = this.rsa.ExportParameters(includePrivateParameters);
			if (includePrivateParameters)
			{
				if (rsaparameters.D == null)
				{
					throw new ArgumentNullException("Missing D parameter for the private key.");
				}
				if (rsaparameters.P == null || rsaparameters.Q == null || rsaparameters.DP == null || rsaparameters.DQ == null || rsaparameters.InverseQ == null)
				{
					throw new CryptographicException("Missing some CRT parameters for the private key.");
				}
			}
			return rsaparameters;
		}

		/// <summary>Imports the specified <see cref="T:System.Security.Cryptography.RSAParameters" />.</summary>
		/// <param name="parameters">The parameters for <see cref="T:System.Security.Cryptography.RSA" />.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.  
		///  -or-  
		///  The <paramref name="parameters" /> parameter has missing fields.</exception>
		// Token: 0x06002FF6 RID: 12278 RVA: 0x000AE276 File Offset: 0x000AC476
		public override void ImportParameters(RSAParameters parameters)
		{
			this.rsa.ImportParameters(parameters);
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x000AE284 File Offset: 0x000AC484
		private HashAlgorithm GetHash(object halg)
		{
			if (halg == null)
			{
				throw new ArgumentNullException("halg");
			}
			HashAlgorithm hashAlgorithm;
			if (halg is string)
			{
				hashAlgorithm = this.GetHashFromString((string)halg);
			}
			else if (halg is HashAlgorithm)
			{
				hashAlgorithm = (HashAlgorithm)halg;
			}
			else
			{
				if (!(halg is Type))
				{
					throw new ArgumentException("halg");
				}
				hashAlgorithm = (HashAlgorithm)Activator.CreateInstance((Type)halg);
			}
			if (hashAlgorithm == null)
			{
				throw new ArgumentException("Could not find provider for halg='" + ((halg != null) ? halg.ToString() : null) + "'.", "halg");
			}
			return hashAlgorithm;
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x000AE31C File Offset: 0x000AC51C
		private HashAlgorithm GetHashFromString(string name)
		{
			HashAlgorithm hashAlgorithm = HashAlgorithm.Create(name);
			if (hashAlgorithm != null)
			{
				return hashAlgorithm;
			}
			HashAlgorithm result;
			try
			{
				result = HashAlgorithm.Create(this.GetHashNameFromOID(name));
			}
			catch (CryptographicException ex)
			{
				throw new ArgumentException(ex.Message, "halg", ex);
			}
			return result;
		}

		/// <summary>Computes the hash value of the specified byte array using the specified hash algorithm, and signs the resulting hash value.</summary>
		/// <param name="buffer">The input data for which to compute the hash.</param>
		/// <param name="halg">The hash algorithm to use to create the hash value.</param>
		/// <returns>The <see cref="T:System.Security.Cryptography.RSA" /> signature for the specified data.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="halg" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="halg" /> parameter is not a valid type.</exception>
		// Token: 0x06002FF9 RID: 12281 RVA: 0x000AE36C File Offset: 0x000AC56C
		public byte[] SignData(byte[] buffer, object halg)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			return this.SignData(buffer, 0, buffer.Length, halg);
		}

		/// <summary>Computes the hash value of the specified input stream using the specified hash algorithm, and signs the resulting hash value.</summary>
		/// <param name="inputStream">The input data for which to compute the hash.</param>
		/// <param name="halg">The hash algorithm to use to create the hash value.</param>
		/// <returns>The <see cref="T:System.Security.Cryptography.RSA" /> signature for the specified data.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="halg" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="halg" /> parameter is not a valid type.</exception>
		// Token: 0x06002FFA RID: 12282 RVA: 0x000AE388 File Offset: 0x000AC588
		public byte[] SignData(Stream inputStream, object halg)
		{
			HashAlgorithm hash = this.GetHash(halg);
			byte[] hashValue = hash.ComputeHash(inputStream);
			return PKCS1.Sign_v15(this, hash, hashValue);
		}

		/// <summary>Computes the hash value of a subset of the specified byte array using the specified hash algorithm, and signs the resulting hash value.</summary>
		/// <param name="buffer">The input data for which to compute the hash.</param>
		/// <param name="offset">The offset into the array from which to begin using data.</param>
		/// <param name="count">The number of bytes in the array to use as data.</param>
		/// <param name="halg">The hash algorithm to use to create the hash value.</param>
		/// <returns>The <see cref="T:System.Security.Cryptography.RSA" /> signature for the specified data.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="halg" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="halg" /> parameter is not a valid type.</exception>
		// Token: 0x06002FFB RID: 12283 RVA: 0x000AE3B0 File Offset: 0x000AC5B0
		public byte[] SignData(byte[] buffer, int offset, int count, object halg)
		{
			HashAlgorithm hash = this.GetHash(halg);
			byte[] hashValue = hash.ComputeHash(buffer, offset, count);
			return PKCS1.Sign_v15(this, hash, hashValue);
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x000AE3D8 File Offset: 0x000AC5D8
		private string GetHashNameFromOID(string oid)
		{
			if (oid == "1.3.14.3.2.26")
			{
				return "SHA1";
			}
			if (oid == "1.2.840.113549.2.5")
			{
				return "MD5";
			}
			if (oid == "2.16.840.1.101.3.4.2.1")
			{
				return "SHA256";
			}
			if (oid == "2.16.840.1.101.3.4.2.2")
			{
				return "SHA384";
			}
			if (!(oid == "2.16.840.1.101.3.4.2.3"))
			{
				throw new CryptographicException(oid + " is an unsupported hash algorithm for RSA signing");
			}
			return "SHA512";
		}

		/// <summary>Computes the signature for the specified hash value by encrypting it with the private key.</summary>
		/// <param name="rgbHash">The hash value of the data to be signed.</param>
		/// <param name="str">The hash algorithm identifier (OID) used to create the hash value of the data.</param>
		/// <returns>The <see cref="T:System.Security.Cryptography.RSA" /> signature for the specified hash value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rgbHash" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.  
		///  -or-  
		///  There is no private key.</exception>
		// Token: 0x06002FFD RID: 12285 RVA: 0x000AE458 File Offset: 0x000AC658
		public byte[] SignHash(byte[] rgbHash, string str)
		{
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			HashAlgorithm hash = HashAlgorithm.Create((str == null) ? "SHA1" : this.GetHashNameFromOID(str));
			return PKCS1.Sign_v15(this, hash, rgbHash);
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x000AE492 File Offset: 0x000AC692
		private byte[] SignHash(byte[] rgbHash, int calgHash)
		{
			return PKCS1.Sign_v15(this, RSACryptoServiceProvider.InternalHashToHashAlgorithm(calgHash), rgbHash);
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x000AE4A4 File Offset: 0x000AC6A4
		private static HashAlgorithm InternalHashToHashAlgorithm(int calgHash)
		{
			if (calgHash == 32771)
			{
				return MD5.Create();
			}
			if (calgHash == 32772)
			{
				return SHA1.Create();
			}
			switch (calgHash)
			{
			case 32780:
				return SHA256.Create();
			case 32781:
				return SHA384.Create();
			case 32782:
				return SHA512.Create();
			default:
				throw new NotImplementedException(calgHash.ToString());
			}
		}

		/// <summary>Verifies that a digital signature is valid by determining the hash value in the signature using the provided public key and comparing it to the hash value of the provided data.</summary>
		/// <param name="buffer">The data that was signed.</param>
		/// <param name="halg">The name of the hash algorithm used to create the hash value of the data.</param>
		/// <param name="signature">The signature data to be verified.</param>
		/// <returns>
		///   <see langword="true" /> if the signature is valid; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="halg" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="halg" /> parameter is not a valid type.</exception>
		// Token: 0x06003000 RID: 12288 RVA: 0x000AE508 File Offset: 0x000AC708
		public bool VerifyData(byte[] buffer, object halg, byte[] signature)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (signature == null)
			{
				throw new ArgumentNullException("signature");
			}
			HashAlgorithm hash = this.GetHash(halg);
			byte[] hashValue = hash.ComputeHash(buffer);
			return PKCS1.Verify_v15(this, hash, hashValue, signature);
		}

		/// <summary>Verifies that a digital signature is valid by determining the hash value in the signature using the provided public key and comparing it to the provided hash value.</summary>
		/// <param name="rgbHash">The hash value of the signed data.</param>
		/// <param name="str">The hash algorithm identifier (OID) used to create the hash value of the data.</param>
		/// <param name="rgbSignature">The signature data to be verified.</param>
		/// <returns>
		///   <see langword="true" /> if the signature is valid; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rgbHash" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="rgbSignature" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.  
		///  -or-  
		///  The signature cannot be verified.</exception>
		// Token: 0x06003001 RID: 12289 RVA: 0x000AE54C File Offset: 0x000AC74C
		public bool VerifyHash(byte[] rgbHash, string str, byte[] rgbSignature)
		{
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			if (rgbSignature == null)
			{
				throw new ArgumentNullException("rgbSignature");
			}
			HashAlgorithm hash = HashAlgorithm.Create((str == null) ? "SHA1" : this.GetHashNameFromOID(str));
			return PKCS1.Verify_v15(this, hash, rgbHash, rgbSignature);
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x000AE595 File Offset: 0x000AC795
		private bool VerifyHash(byte[] rgbHash, int calgHash, byte[] rgbSignature)
		{
			return PKCS1.Verify_v15(this, RSACryptoServiceProvider.InternalHashToHashAlgorithm(calgHash), rgbHash, rgbSignature);
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x000AE5A5 File Offset: 0x000AC7A5
		protected override void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (this.persisted && !this.persistKey)
				{
					this.store.Remove();
				}
				if (this.rsa != null)
				{
					this.rsa.Clear();
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x000AE5E4 File Offset: 0x000AC7E4
		private void OnKeyGenerated(object sender, EventArgs e)
		{
			if (this.persistKey && !this.persisted)
			{
				this.store.KeyValue = this.ToXmlString(!this.rsa.PublicOnly);
				this.store.Save();
				this.persisted = true;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CspKeyContainerInfo" /> object that describes additional information about a cryptographic key pair.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CspKeyContainerInfo" /> object that describes additional information about a cryptographic key pair.</returns>
		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06003005 RID: 12293 RVA: 0x000AE632 File Offset: 0x000AC832
		[ComVisible(false)]
		public CspKeyContainerInfo CspKeyContainerInfo
		{
			[SecuritySafeCritical]
			get
			{
				return new CspKeyContainerInfo(this.store.Parameters);
			}
		}

		/// <summary>Exports a blob containing the key information associated with an <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> object.</summary>
		/// <param name="includePrivateParameters">
		///   <see langword="true" /> to include the private key; otherwise, <see langword="false" />.</param>
		/// <returns>A byte array containing the key information associated with an <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> object.</returns>
		// Token: 0x06003006 RID: 12294 RVA: 0x000AE644 File Offset: 0x000AC844
		[SecuritySafeCritical]
		[ComVisible(false)]
		public byte[] ExportCspBlob(bool includePrivateParameters)
		{
			byte[] array;
			if (includePrivateParameters)
			{
				array = CryptoConvert.ToCapiPrivateKeyBlob(this);
			}
			else
			{
				array = CryptoConvert.ToCapiPublicKeyBlob(this);
			}
			array[5] = ((this.store != null && this.store.Parameters.KeyNumber == 2) ? 36 : 164);
			return array;
		}

		/// <summary>Imports a blob that represents RSA key information.</summary>
		/// <param name="keyBlob">A byte array that represents an RSA key blob.</param>
		// Token: 0x06003007 RID: 12295 RVA: 0x000AE690 File Offset: 0x000AC890
		[ComVisible(false)]
		[SecuritySafeCritical]
		public void ImportCspBlob(byte[] keyBlob)
		{
			if (keyBlob == null)
			{
				throw new ArgumentNullException("keyBlob");
			}
			RSA rsa = CryptoConvert.FromCapiKeyBlob(keyBlob);
			if (rsa is RSACryptoServiceProvider)
			{
				RSAParameters parameters = rsa.ExportParameters(!(rsa as RSACryptoServiceProvider).PublicOnly);
				this.ImportParameters(parameters);
			}
			else
			{
				try
				{
					RSAParameters parameters2 = rsa.ExportParameters(true);
					this.ImportParameters(parameters2);
				}
				catch
				{
					RSAParameters parameters3 = rsa.ExportParameters(false);
					this.ImportParameters(parameters3);
				}
			}
			CspParameters cspParameters = new CspParameters(1);
			cspParameters.KeyNumber = ((keyBlob[5] == 36) ? 2 : 1);
			if (RSACryptoServiceProvider.UseMachineKeyStore)
			{
				cspParameters.Flags |= CspProviderFlags.UseMachineKeyStore;
			}
			this.store = new KeyPairPersistence(cspParameters);
		}

		// Token: 0x040021C0 RID: 8640
		private static volatile CspProviderFlags s_UseMachineKeyStore;

		// Token: 0x040021C1 RID: 8641
		private const int PROV_RSA_FULL = 1;

		// Token: 0x040021C2 RID: 8642
		private const int AT_KEYEXCHANGE = 1;

		// Token: 0x040021C3 RID: 8643
		private const int AT_SIGNATURE = 2;

		// Token: 0x040021C4 RID: 8644
		private KeyPairPersistence store;

		// Token: 0x040021C5 RID: 8645
		private bool persistKey;

		// Token: 0x040021C6 RID: 8646
		private bool persisted;

		// Token: 0x040021C7 RID: 8647
		private bool privateKeyExportable;

		// Token: 0x040021C8 RID: 8648
		private bool m_disposed;

		// Token: 0x040021C9 RID: 8649
		private RSAManaged rsa;
	}
}
