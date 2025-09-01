using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Defines a wrapper object to access the cryptographic service provider (CSP) implementation of the <see cref="T:System.Security.Cryptography.DSA" /> algorithm. This class cannot be inherited.</summary>
	// Token: 0x020004C9 RID: 1225
	[ComVisible(true)]
	public sealed class DSACryptoServiceProvider : DSA, ICspAsymmetricAlgorithm
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> class.</summary>
		// Token: 0x060030FC RID: 12540 RVA: 0x000B39DE File Offset: 0x000B1BDE
		public DSACryptoServiceProvider() : this(1024)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> class with the specified parameters for the cryptographic service provider (CSP).</summary>
		/// <param name="parameters">The parameters for the CSP.</param>
		// Token: 0x060030FD RID: 12541 RVA: 0x000B39EB File Offset: 0x000B1BEB
		public DSACryptoServiceProvider(CspParameters parameters) : this(1024, parameters)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> class with the specified key size.</summary>
		/// <param name="dwKeySize">The size of the key for the asymmetric algorithm in bits.</param>
		// Token: 0x060030FE RID: 12542 RVA: 0x000B39F9 File Offset: 0x000B1BF9
		public DSACryptoServiceProvider(int dwKeySize)
		{
			this.privateKeyExportable = true;
			base..ctor();
			this.Common(dwKeySize, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> class with the specified key size and parameters for the cryptographic service provider (CSP).</summary>
		/// <param name="dwKeySize">The size of the key for the cryptographic algorithm in bits.</param>
		/// <param name="parameters">The parameters for the CSP.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The CSP cannot be acquired.  
		///  -or-  
		///  The key cannot be created.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="dwKeySize" /> is out of range.</exception>
		// Token: 0x060030FF RID: 12543 RVA: 0x000B3A10 File Offset: 0x000B1C10
		public DSACryptoServiceProvider(int dwKeySize, CspParameters parameters)
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

		// Token: 0x06003100 RID: 12544 RVA: 0x000B3A44 File Offset: 0x000B1C44
		private void Common(int dwKeySize, bool parameters)
		{
			this.LegalKeySizesValue = new KeySizes[1];
			this.LegalKeySizesValue[0] = new KeySizes(512, 1024, 64);
			this.KeySize = dwKeySize;
			this.dsa = new DSAManaged(dwKeySize);
			this.dsa.KeyGenerated += this.OnKeyGenerated;
			this.persistKey = parameters;
			if (parameters)
			{
				return;
			}
			CspParameters cspParameters = new CspParameters(13);
			if (DSACryptoServiceProvider.useMachineKeyStore)
			{
				cspParameters.Flags |= CspProviderFlags.UseMachineKeyStore;
			}
			this.store = new KeyPairPersistence(cspParameters);
		}

		// Token: 0x06003101 RID: 12545 RVA: 0x000B3AD4 File Offset: 0x000B1CD4
		private void Common(CspParameters parameters)
		{
			this.store = new KeyPairPersistence(parameters);
			this.store.Load();
			if (this.store.KeyValue != null)
			{
				this.persisted = true;
				this.FromXmlString(this.store.KeyValue);
			}
			this.privateKeyExportable = ((parameters.Flags & CspProviderFlags.UseNonExportableKey) == CspProviderFlags.NoFlags);
		}

		// Token: 0x06003102 RID: 12546 RVA: 0x000B3B30 File Offset: 0x000B1D30
		~DSACryptoServiceProvider()
		{
			this.Dispose(false);
		}

		/// <summary>Gets the name of the key exchange algorithm.</summary>
		/// <returns>The name of the key exchange algorithm.</returns>
		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06003103 RID: 12547 RVA: 0x0000AF5E File Offset: 0x0000915E
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the size of the key used by the asymmetric algorithm in bits.</summary>
		/// <returns>The size of the key used by the asymmetric algorithm.</returns>
		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06003104 RID: 12548 RVA: 0x000B3B60 File Offset: 0x000B1D60
		public override int KeySize
		{
			get
			{
				return this.dsa.KeySize;
			}
		}

		/// <summary>Gets or sets a value indicating whether the key should be persisted in the cryptographic service provider (CSP).</summary>
		/// <returns>
		///   <see langword="true" /> if the key should be persisted in the CSP; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06003105 RID: 12549 RVA: 0x000B3B6D File Offset: 0x000B1D6D
		// (set) Token: 0x06003106 RID: 12550 RVA: 0x000B3B75 File Offset: 0x000B1D75
		public bool PersistKeyInCsp
		{
			get
			{
				return this.persistKey;
			}
			set
			{
				this.persistKey = value;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> object contains only a public key.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> object contains only a public key; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06003107 RID: 12551 RVA: 0x000B3B7E File Offset: 0x000B1D7E
		[ComVisible(false)]
		public bool PublicOnly
		{
			get
			{
				return this.dsa.PublicOnly;
			}
		}

		/// <summary>Gets the name of the signature algorithm.</summary>
		/// <returns>The name of the signature algorithm.</returns>
		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06003108 RID: 12552 RVA: 0x00012A75 File Offset: 0x00010C75
		public override string SignatureAlgorithm
		{
			get
			{
				return "http://www.w3.org/2000/09/xmldsig#dsa-sha1";
			}
		}

		/// <summary>Gets or sets a value indicating whether the key should be persisted in the computer's key store instead of the user profile store.</summary>
		/// <returns>
		///   <see langword="true" /> if the key should be persisted in the computer key store; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06003109 RID: 12553 RVA: 0x000B3B8B File Offset: 0x000B1D8B
		// (set) Token: 0x0600310A RID: 12554 RVA: 0x000B3B92 File Offset: 0x000B1D92
		public static bool UseMachineKeyStore
		{
			get
			{
				return DSACryptoServiceProvider.useMachineKeyStore;
			}
			set
			{
				DSACryptoServiceProvider.useMachineKeyStore = value;
			}
		}

		/// <summary>Exports the <see cref="T:System.Security.Cryptography.DSAParameters" />.</summary>
		/// <param name="includePrivateParameters">
		///   <see langword="true" /> to include private parameters; otherwise, <see langword="false" />.</param>
		/// <returns>The parameters for <see cref="T:System.Security.Cryptography.DSA" />.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key cannot be exported.</exception>
		// Token: 0x0600310B RID: 12555 RVA: 0x000B3B9A File Offset: 0x000B1D9A
		public override DSAParameters ExportParameters(bool includePrivateParameters)
		{
			if (includePrivateParameters && !this.privateKeyExportable)
			{
				throw new CryptographicException(Locale.GetText("Cannot export private key"));
			}
			return this.dsa.ExportParameters(includePrivateParameters);
		}

		/// <summary>Imports the specified <see cref="T:System.Security.Cryptography.DSAParameters" />.</summary>
		/// <param name="parameters">The parameters for <see cref="T:System.Security.Cryptography.DSA" />.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.  
		///  -or-  
		///  The <paramref name="parameters" /> parameter has missing fields.</exception>
		// Token: 0x0600310C RID: 12556 RVA: 0x000B3BC3 File Offset: 0x000B1DC3
		public override void ImportParameters(DSAParameters parameters)
		{
			this.dsa.ImportParameters(parameters);
		}

		/// <summary>Creates the <see cref="T:System.Security.Cryptography.DSA" /> signature for the specified data.</summary>
		/// <param name="rgbHash">The data to be signed.</param>
		/// <returns>The digital signature for the specified data.</returns>
		// Token: 0x0600310D RID: 12557 RVA: 0x000B3BD1 File Offset: 0x000B1DD1
		public override byte[] CreateSignature(byte[] rgbHash)
		{
			return this.dsa.CreateSignature(rgbHash);
		}

		/// <summary>Computes the hash value of the specified byte array and signs the resulting hash value.</summary>
		/// <param name="buffer">The input data for which to compute the hash.</param>
		/// <returns>The <see cref="T:System.Security.Cryptography.DSA" /> signature for the specified data.</returns>
		// Token: 0x0600310E RID: 12558 RVA: 0x000B3BE0 File Offset: 0x000B1DE0
		public byte[] SignData(byte[] buffer)
		{
			byte[] rgbHash = SHA1.Create().ComputeHash(buffer);
			return this.dsa.CreateSignature(rgbHash);
		}

		/// <summary>Signs a byte array from the specified start point to the specified end point.</summary>
		/// <param name="buffer">The input data to sign.</param>
		/// <param name="offset">The offset into the array from which to begin using data.</param>
		/// <param name="count">The number of bytes in the array to use as data.</param>
		/// <returns>The <see cref="T:System.Security.Cryptography.DSA" /> signature for the specified data.</returns>
		// Token: 0x0600310F RID: 12559 RVA: 0x000B3C08 File Offset: 0x000B1E08
		public byte[] SignData(byte[] buffer, int offset, int count)
		{
			byte[] rgbHash = SHA1.Create().ComputeHash(buffer, offset, count);
			return this.dsa.CreateSignature(rgbHash);
		}

		/// <summary>Computes the hash value of the specified input stream and signs the resulting hash value.</summary>
		/// <param name="inputStream">The input data for which to compute the hash.</param>
		/// <returns>The <see cref="T:System.Security.Cryptography.DSA" /> signature for the specified data.</returns>
		// Token: 0x06003110 RID: 12560 RVA: 0x000B3C30 File Offset: 0x000B1E30
		public byte[] SignData(Stream inputStream)
		{
			byte[] rgbHash = SHA1.Create().ComputeHash(inputStream);
			return this.dsa.CreateSignature(rgbHash);
		}

		/// <summary>Computes the signature for the specified hash value by encrypting it with the private key.</summary>
		/// <param name="rgbHash">The hash value of the data to be signed.</param>
		/// <param name="str">The name of the hash algorithm used to create the hash value of the data.</param>
		/// <returns>The <see cref="T:System.Security.Cryptography.DSA" /> signature for the specified hash value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rgbHash" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.  
		///  -or-  
		///  There is no private key.</exception>
		// Token: 0x06003111 RID: 12561 RVA: 0x000B3C55 File Offset: 0x000B1E55
		public byte[] SignHash(byte[] rgbHash, string str)
		{
			if (string.Compare(str, "SHA1", true, CultureInfo.InvariantCulture) != 0)
			{
				throw new CryptographicException(Locale.GetText("Only SHA1 is supported."));
			}
			return this.dsa.CreateSignature(rgbHash);
		}

		/// <summary>Verifies the specified signature data by comparing it to the signature computed for the specified data.</summary>
		/// <param name="rgbData">The data that was signed.</param>
		/// <param name="rgbSignature">The signature data to be verified.</param>
		/// <returns>
		///   <see langword="true" /> if the signature verifies as valid; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003112 RID: 12562 RVA: 0x000B3C88 File Offset: 0x000B1E88
		public bool VerifyData(byte[] rgbData, byte[] rgbSignature)
		{
			byte[] rgbHash = SHA1.Create().ComputeHash(rgbData);
			return this.dsa.VerifySignature(rgbHash, rgbSignature);
		}

		/// <summary>Verifies the specified signature data by comparing it to the signature computed for the specified hash value.</summary>
		/// <param name="rgbHash">The hash value of the data to be signed.</param>
		/// <param name="str">The name of the hash algorithm used to create the hash value of the data.</param>
		/// <param name="rgbSignature">The signature data to be verified.</param>
		/// <returns>
		///   <see langword="true" /> if the signature verifies as valid; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rgbHash" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="rgbSignature" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.  
		///  -or-  
		///  The signature cannot be verified.</exception>
		// Token: 0x06003113 RID: 12563 RVA: 0x000B3CAE File Offset: 0x000B1EAE
		public bool VerifyHash(byte[] rgbHash, string str, byte[] rgbSignature)
		{
			if (str == null)
			{
				str = "SHA1";
			}
			if (string.Compare(str, "SHA1", true, CultureInfo.InvariantCulture) != 0)
			{
				throw new CryptographicException(Locale.GetText("Only SHA1 is supported."));
			}
			return this.dsa.VerifySignature(rgbHash, rgbSignature);
		}

		/// <summary>Verifies the <see cref="T:System.Security.Cryptography.DSA" /> signature for the specified data.</summary>
		/// <param name="rgbHash">The data signed with <paramref name="rgbSignature" />.</param>
		/// <param name="rgbSignature">The signature to be verified for <paramref name="rgbData" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="rgbSignature" /> matches the signature computed using the specified hash algorithm and key on <paramref name="rgbHash" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003114 RID: 12564 RVA: 0x000B3CEA File Offset: 0x000B1EEA
		public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
		{
			return this.dsa.VerifySignature(rgbHash, rgbSignature);
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x000B3CFC File Offset: 0x000B1EFC
		protected override byte[] HashData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm)
		{
			if (hashAlgorithm != HashAlgorithmName.SHA1)
			{
				throw new CryptographicException(Environment.GetResourceString("'{0}' is not a known hash algorithm.", new object[]
				{
					hashAlgorithm.Name
				}));
			}
			return HashAlgorithm.Create(hashAlgorithm.Name).ComputeHash(data, offset, count);
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x000B3D4C File Offset: 0x000B1F4C
		protected override byte[] HashData(Stream data, HashAlgorithmName hashAlgorithm)
		{
			if (hashAlgorithm != HashAlgorithmName.SHA1)
			{
				throw new CryptographicException(Environment.GetResourceString("'{0}' is not a known hash algorithm.", new object[]
				{
					hashAlgorithm.Name
				}));
			}
			return HashAlgorithm.Create(hashAlgorithm.Name).ComputeHash(data);
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x000B3D98 File Offset: 0x000B1F98
		protected override void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (this.persisted && !this.persistKey)
				{
					this.store.Remove();
				}
				if (this.dsa != null)
				{
					this.dsa.Clear();
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x000B3DD8 File Offset: 0x000B1FD8
		private void OnKeyGenerated(object sender, EventArgs e)
		{
			if (this.persistKey && !this.persisted)
			{
				this.store.KeyValue = this.ToXmlString(!this.dsa.PublicOnly);
				this.store.Save();
				this.persisted = true;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CspKeyContainerInfo" /> object that describes additional information about a cryptographic key pair.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CspKeyContainerInfo" /> object that describes additional information about a cryptographic key pair.</returns>
		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06003119 RID: 12569 RVA: 0x0000AF5E File Offset: 0x0000915E
		[ComVisible(false)]
		[MonoTODO("call into KeyPairPersistence to get details")]
		public CspKeyContainerInfo CspKeyContainerInfo
		{
			[SecuritySafeCritical]
			get
			{
				return null;
			}
		}

		/// <summary>Exports a blob containing the key information associated with a <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> object.</summary>
		/// <param name="includePrivateParameters">
		///   <see langword="true" /> to include the private key; otherwise, <see langword="false" />.</param>
		/// <returns>A byte array containing the key information associated with a <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> object.</returns>
		// Token: 0x0600311A RID: 12570 RVA: 0x000B3E28 File Offset: 0x000B2028
		[ComVisible(false)]
		[SecuritySafeCritical]
		public byte[] ExportCspBlob(bool includePrivateParameters)
		{
			byte[] result;
			if (includePrivateParameters)
			{
				result = CryptoConvert.ToCapiPrivateKeyBlob(this);
			}
			else
			{
				result = CryptoConvert.ToCapiPublicKeyBlob(this);
			}
			return result;
		}

		/// <summary>Imports a blob that represents DSA key information.</summary>
		/// <param name="keyBlob">A byte array that represents a DSA key blob.</param>
		// Token: 0x0600311B RID: 12571 RVA: 0x000B3E4C File Offset: 0x000B204C
		[SecuritySafeCritical]
		[ComVisible(false)]
		public void ImportCspBlob(byte[] keyBlob)
		{
			if (keyBlob == null)
			{
				throw new ArgumentNullException("keyBlob");
			}
			DSA dsa = CryptoConvert.FromCapiKeyBlobDSA(keyBlob);
			if (dsa is DSACryptoServiceProvider)
			{
				DSAParameters parameters = dsa.ExportParameters(!(dsa as DSACryptoServiceProvider).PublicOnly);
				this.ImportParameters(parameters);
				return;
			}
			try
			{
				DSAParameters parameters2 = dsa.ExportParameters(true);
				this.ImportParameters(parameters2);
			}
			catch
			{
				DSAParameters parameters3 = dsa.ExportParameters(false);
				this.ImportParameters(parameters3);
			}
		}

		// Token: 0x04002255 RID: 8789
		private const int PROV_DSS_DH = 13;

		// Token: 0x04002256 RID: 8790
		private KeyPairPersistence store;

		// Token: 0x04002257 RID: 8791
		private bool persistKey;

		// Token: 0x04002258 RID: 8792
		private bool persisted;

		// Token: 0x04002259 RID: 8793
		private bool privateKeyExportable;

		// Token: 0x0400225A RID: 8794
		private bool m_disposed;

		// Token: 0x0400225B RID: 8795
		private DSAManaged dsa;

		// Token: 0x0400225C RID: 8796
		private static bool useMachineKeyStore;
	}
}
