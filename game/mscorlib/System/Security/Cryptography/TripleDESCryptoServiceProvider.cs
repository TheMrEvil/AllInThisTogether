using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Defines a wrapper object to access the cryptographic service provider (CSP) version of the <see cref="T:System.Security.Cryptography.TripleDES" /> algorithm. This class cannot be inherited.</summary>
	// Token: 0x020004C1 RID: 1217
	[ComVisible(true)]
	public sealed class TripleDESCryptoServiceProvider : TripleDES
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.TripleDESCryptoServiceProvider" /> class.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="T:System.Security.Cryptography.TripleDES" /> cryptographic service provider is not available.</exception>
		// Token: 0x060030AF RID: 12463 RVA: 0x000B1239 File Offset: 0x000AF439
		[SecuritySafeCritical]
		public TripleDESCryptoServiceProvider()
		{
			if (!Utils.HasAlgorithm(26115, 0))
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptographic service provider (CSP) could not be found for this algorithm."));
			}
			this.FeedbackSizeValue = 8;
		}

		/// <summary>Creates a symmetric <see cref="T:System.Security.Cryptography.TripleDES" /> encryptor object with the specified key (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <param name="rgbKey">The secret key to use for the symmetric algorithm.</param>
		/// <param name="rgbIV">The initialization vector to use for the symmetric algorithm.  
		///
		///  The initialization vector must be 8 bytes long. If it is longer than 8 bytes, it is truncated and an exception is not thrown. Before you call <see cref="M:System.Security.Cryptography.TripleDESCryptoServiceProvider.CreateEncryptor(System.Byte[],System.Byte[])" />, check the length of the initialization vector and throw an exception if it is too long.</param>
		/// <returns>A symmetric <see cref="T:System.Security.Cryptography.TripleDES" /> encryptor object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Mode" /> property is <see cref="F:System.Security.Cryptography.CipherMode.OFB" />.  
		///  -or-  
		///  The value of the <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Mode" /> property is <see cref="F:System.Security.Cryptography.CipherMode.CFB" /> and the value of the <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.FeedbackSize" /> property is not 8.  
		///  -or-  
		///  An invalid key size was used.  
		///  -or-  
		///  The algorithm key size was not available.</exception>
		// Token: 0x060030B0 RID: 12464 RVA: 0x000B1265 File Offset: 0x000AF465
		[SecuritySafeCritical]
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			if (TripleDES.IsWeakKey(rgbKey))
			{
				throw new CryptographicException(Environment.GetResourceString("Specified key is a known weak key for '{0}' and cannot be used."), "TripleDES");
			}
			return new TripleDESTransform(this, true, rgbKey, rgbIV);
		}

		/// <summary>Creates a symmetric <see cref="T:System.Security.Cryptography.TripleDES" /> decryptor object with the specified key (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <param name="rgbKey">The secret key to use for the symmetric algorithm.</param>
		/// <param name="rgbIV">The initialization vector to use for the symmetric algorithm.</param>
		/// <returns>A symmetric <see cref="T:System.Security.Cryptography.TripleDES" /> decryptor object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Mode" /> property is <see cref="F:System.Security.Cryptography.CipherMode.OFB" />.  
		///  -or-  
		///  The value of the <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Mode" /> property is <see cref="F:System.Security.Cryptography.CipherMode.CFB" /> and the value of the <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.FeedbackSize" /> property is not 8.  
		///  -or-  
		///  An invalid key size was used.  
		///  -or-  
		///  The algorithm key size was not available.</exception>
		// Token: 0x060030B1 RID: 12465 RVA: 0x000B128D File Offset: 0x000AF48D
		[SecuritySafeCritical]
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			if (TripleDES.IsWeakKey(rgbKey))
			{
				throw new CryptographicException(Environment.GetResourceString("Specified key is a known weak key for '{0}' and cannot be used."), "TripleDES");
			}
			return new TripleDESTransform(this, false, rgbKey, rgbIV);
		}

		/// <summary>Generates a random <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> to be used for the algorithm.</summary>
		// Token: 0x060030B2 RID: 12466 RVA: 0x000B12B8 File Offset: 0x000AF4B8
		public override void GenerateKey()
		{
			this.KeyValue = new byte[this.KeySizeValue / 8];
			Utils.StaticRandomNumberGenerator.GetBytes(this.KeyValue);
			while (TripleDES.IsWeakKey(this.KeyValue))
			{
				Utils.StaticRandomNumberGenerator.GetBytes(this.KeyValue);
			}
		}

		/// <summary>Generates a random initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) to use for the algorithm.</summary>
		// Token: 0x060030B3 RID: 12467 RVA: 0x000A6EF9 File Offset: 0x000A50F9
		public override void GenerateIV()
		{
			this.IVValue = new byte[8];
			Utils.StaticRandomNumberGenerator.GetBytes(this.IVValue);
		}
	}
}
