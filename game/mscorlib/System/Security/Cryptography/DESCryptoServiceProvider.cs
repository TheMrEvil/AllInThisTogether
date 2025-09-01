using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Defines a wrapper object to access the cryptographic service provider (CSP) version of the Data Encryption Standard (<see cref="T:System.Security.Cryptography.DES" />) algorithm. This class cannot be inherited.</summary>
	// Token: 0x02000489 RID: 1161
	[ComVisible(true)]
	public sealed class DESCryptoServiceProvider : DES
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DESCryptoServiceProvider" /> class.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The Data Encryption Standard (<see cref="T:System.Security.Cryptography.DES" />) cryptographic service provider is not available.</exception>
		// Token: 0x06002EC0 RID: 11968 RVA: 0x000A6DD8 File Offset: 0x000A4FD8
		[SecuritySafeCritical]
		public DESCryptoServiceProvider()
		{
			if (!Utils.HasAlgorithm(26113, 0))
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptographic service provider (CSP) could not be found for this algorithm."));
			}
			this.FeedbackSizeValue = 8;
		}

		/// <summary>Creates a symmetric Data Encryption Standard (<see cref="T:System.Security.Cryptography.DES" />) encryptor object with the specified key (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <param name="rgbKey">The secret key to use for the symmetric algorithm.</param>
		/// <param name="rgbIV">The initialization vector to use for the symmetric algorithm.</param>
		/// <returns>A symmetric <see cref="T:System.Security.Cryptography.DES" /> encryptor object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Mode" /> property is <see cref="F:System.Security.Cryptography.CipherMode.OFB" />.  
		///  -or-  
		///  The value of the <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Mode" /> property is <see cref="F:System.Security.Cryptography.CipherMode.CFB" /> and the value of the <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.FeedbackSize" /> property is not 8.  
		///  -or-  
		///  An invalid key size was used.  
		///  -or-  
		///  The algorithm key size was not available.</exception>
		// Token: 0x06002EC1 RID: 11969 RVA: 0x000A6E04 File Offset: 0x000A5004
		[SecuritySafeCritical]
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			if (DES.IsWeakKey(rgbKey))
			{
				throw new CryptographicException(Environment.GetResourceString("Specified key is a known weak key for '{0}' and cannot be used."), "DES");
			}
			if (DES.IsSemiWeakKey(rgbKey))
			{
				throw new CryptographicException(Environment.GetResourceString("Specified key is a known semi-weak key for '{0}' and cannot be used."), "DES");
			}
			return new DESTransform(this, true, rgbKey, rgbIV);
		}

		/// <summary>Creates a symmetric Data Encryption Standard (<see cref="T:System.Security.Cryptography.DES" />) decryptor object with the specified key (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) and initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).</summary>
		/// <param name="rgbKey">The secret key to use for the symmetric algorithm.</param>
		/// <param name="rgbIV">The initialization vector to use for the symmetric algorithm.</param>
		/// <returns>A symmetric <see cref="T:System.Security.Cryptography.DES" /> decryptor object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Mode" /> property is <see cref="F:System.Security.Cryptography.CipherMode.OFB" />.  
		///  -or-  
		///  The value of the <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Mode" /> property is <see cref="F:System.Security.Cryptography.CipherMode.CFB" /> and the value of the <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.FeedbackSize" /> property is not 8.  
		///  -or-  
		///  An invalid key size was used.  
		///  -or-  
		///  The algorithm key size was not available.</exception>
		// Token: 0x06002EC2 RID: 11970 RVA: 0x000A6E54 File Offset: 0x000A5054
		[SecuritySafeCritical]
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			if (DES.IsWeakKey(rgbKey))
			{
				throw new CryptographicException(Environment.GetResourceString("Specified key is a known weak key for '{0}' and cannot be used."), "DES");
			}
			if (DES.IsSemiWeakKey(rgbKey))
			{
				throw new CryptographicException(Environment.GetResourceString("Specified key is a known semi-weak key for '{0}' and cannot be used."), "DES");
			}
			return new DESTransform(this, false, rgbKey, rgbIV);
		}

		/// <summary>Generates a random key (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) to be used for the algorithm.</summary>
		// Token: 0x06002EC3 RID: 11971 RVA: 0x000A6EA4 File Offset: 0x000A50A4
		public override void GenerateKey()
		{
			this.KeyValue = new byte[8];
			Utils.StaticRandomNumberGenerator.GetBytes(this.KeyValue);
			while (DES.IsWeakKey(this.KeyValue) || DES.IsSemiWeakKey(this.KeyValue))
			{
				Utils.StaticRandomNumberGenerator.GetBytes(this.KeyValue);
			}
		}

		/// <summary>Generates a random initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) to use for the algorithm.</summary>
		// Token: 0x06002EC4 RID: 11972 RVA: 0x000A6EF9 File Offset: 0x000A50F9
		public override void GenerateIV()
		{
			this.IVValue = new byte[8];
			Utils.StaticRandomNumberGenerator.GetBytes(this.IVValue);
		}
	}
}
