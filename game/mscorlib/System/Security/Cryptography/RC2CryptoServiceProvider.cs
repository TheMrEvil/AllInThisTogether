using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Defines a wrapper object to access the cryptographic service provider (CSP) implementation of the <see cref="T:System.Security.Cryptography.RC2" /> algorithm. This class cannot be inherited.</summary>
	// Token: 0x020004A2 RID: 1186
	[ComVisible(true)]
	public sealed class RC2CryptoServiceProvider : RC2
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RC2CryptoServiceProvider" /> class.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.</exception>
		/// <exception cref="T:System.InvalidOperationException">A non-compliant FIPS algorithm was found.</exception>
		// Token: 0x06002F76 RID: 12150 RVA: 0x000A9008 File Offset: 0x000A7208
		[SecuritySafeCritical]
		public RC2CryptoServiceProvider()
		{
			if (CryptoConfig.AllowOnlyFipsAlgorithms)
			{
				throw new InvalidOperationException(Environment.GetResourceString("This implementation is not part of the Windows Platform FIPS validated cryptographic algorithms."));
			}
			if (!Utils.HasAlgorithm(26114, 0))
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptographic service provider (CSP) could not be found for this algorithm."));
			}
			this.LegalKeySizesValue = RC2CryptoServiceProvider.s_legalKeySizes;
			this.FeedbackSizeValue = 8;
		}

		/// <summary>Gets or sets the effective size, in bits, of the secret key used by the <see cref="T:System.Security.Cryptography.RC2" /> algorithm.</summary>
		/// <returns>The effective key size, in bits, used by the <see cref="T:System.Security.Cryptography.RC2" /> algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The <see cref="P:System.Security.Cryptography.RC2CryptoServiceProvider.EffectiveKeySize" /> property was set to a value other than the <see cref="F:System.Security.Cryptography.SymmetricAlgorithm.KeySizeValue" /> property.</exception>
		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06002F77 RID: 12151 RVA: 0x000A8F8E File Offset: 0x000A718E
		// (set) Token: 0x06002F78 RID: 12152 RVA: 0x000A9061 File Offset: 0x000A7261
		public override int EffectiveKeySize
		{
			get
			{
				return this.KeySizeValue;
			}
			set
			{
				if (value != this.KeySizeValue)
				{
					throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("EffectiveKeySize must be the same as KeySize in this implementation."));
				}
			}
		}

		/// <summary>Gets or sets a value that determines whether to create a key with an 11-byte-long, zero-value salt.</summary>
		/// <returns>
		///   <see langword="true" /> if the key should be created with an 11-byte-long, zero-value salt; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06002F79 RID: 12153 RVA: 0x000A907C File Offset: 0x000A727C
		// (set) Token: 0x06002F7A RID: 12154 RVA: 0x000A9084 File Offset: 0x000A7284
		[ComVisible(false)]
		public bool UseSalt
		{
			get
			{
				return this.m_use40bitSalt;
			}
			set
			{
				this.m_use40bitSalt = value;
			}
		}

		/// <summary>Creates a symmetric <see cref="T:System.Security.Cryptography.RC2" /> encryptor object with the specified key (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <param name="rgbKey">The secret key to use for the symmetric algorithm.</param>
		/// <param name="rgbIV">The initialization vector to use for the symmetric algorithm.</param>
		/// <returns>A symmetric <see cref="T:System.Security.Cryptography.RC2" /> encryptor object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An <see cref="F:System.Security.Cryptography.CipherMode.OFB" /> cipher mode was used.  
		///  -or-  
		///  A <see cref="F:System.Security.Cryptography.CipherMode.CFB" /> cipher mode with a feedback size other than 8 bits was used.  
		///  -or-  
		///  An invalid key size was used.  
		///  -or-  
		///  The algorithm key size was not available.</exception>
		// Token: 0x06002F7B RID: 12155 RVA: 0x000A908D File Offset: 0x000A728D
		[SecuritySafeCritical]
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			if (this.m_use40bitSalt)
			{
				throw new NotImplementedException("UseSalt=true is not implemented on Mono yet");
			}
			return new RC2Transform(this, true, rgbKey, rgbIV);
		}

		/// <summary>Creates a symmetric <see cref="T:System.Security.Cryptography.RC2" /> decryptor object with the specified key (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <param name="rgbKey">The secret key to use for the symmetric algorithm.</param>
		/// <param name="rgbIV">The initialization vector to use for the symmetric algorithm.</param>
		/// <returns>A symmetric <see cref="T:System.Security.Cryptography.RC2" /> decryptor object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An <see cref="F:System.Security.Cryptography.CipherMode.OFB" /> cipher mode was used.  
		///  -or-  
		///  A <see cref="F:System.Security.Cryptography.CipherMode.CFB" /> cipher mode with a feedback size other than 8 bits was used.  
		///  -or-  
		///  An invalid key size was used.  
		///  -or-  
		///  The algorithm key size was not available.</exception>
		// Token: 0x06002F7C RID: 12156 RVA: 0x000A90AB File Offset: 0x000A72AB
		[SecuritySafeCritical]
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			if (this.m_use40bitSalt)
			{
				throw new NotImplementedException("UseSalt=true is not implemented on Mono yet");
			}
			return new RC2Transform(this, false, rgbKey, rgbIV);
		}

		/// <summary>Generates a random key (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) to be used for the algorithm.</summary>
		// Token: 0x06002F7D RID: 12157 RVA: 0x000A90C9 File Offset: 0x000A72C9
		public override void GenerateKey()
		{
			this.KeyValue = new byte[this.KeySizeValue / 8];
			Utils.StaticRandomNumberGenerator.GetBytes(this.KeyValue);
		}

		/// <summary>Generates a random initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) to use for the algorithm.</summary>
		// Token: 0x06002F7E RID: 12158 RVA: 0x000A6EF9 File Offset: 0x000A50F9
		public override void GenerateIV()
		{
			this.IVValue = new byte[8];
			Utils.StaticRandomNumberGenerator.GetBytes(this.IVValue);
		}

		// Token: 0x06002F7F RID: 12159 RVA: 0x000A90EE File Offset: 0x000A72EE
		// Note: this type is marked as 'beforefieldinit'.
		static RC2CryptoServiceProvider()
		{
		}

		// Token: 0x04002195 RID: 8597
		private bool m_use40bitSalt;

		// Token: 0x04002196 RID: 8598
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(40, 128, 8)
		};
	}
}
