using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Cryptography
{
	/// <summary>Derives a key from a password using an extension of the PBKDF1 algorithm.</summary>
	// Token: 0x0200049E RID: 1182
	[ComVisible(true)]
	public class PasswordDeriveBytes : DeriveBytes
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> class with the password and key salt to use to derive the key.</summary>
		/// <param name="strPassword">The password for which to derive the key.</param>
		/// <param name="rgbSalt">The key salt to use to derive the key.</param>
		// Token: 0x06002F47 RID: 12103 RVA: 0x000A8615 File Offset: 0x000A6815
		public PasswordDeriveBytes(string strPassword, byte[] rgbSalt) : this(strPassword, rgbSalt, new CspParameters())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> class specifying the password and key salt to use to derive the key.</summary>
		/// <param name="password">The password to derive the key for.</param>
		/// <param name="salt">The key salt to use to derive the key.</param>
		// Token: 0x06002F48 RID: 12104 RVA: 0x000A8624 File Offset: 0x000A6824
		public PasswordDeriveBytes(byte[] password, byte[] salt) : this(password, salt, new CspParameters())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> class with the password, key salt, hash name, and number of iterations to use to derive the key.</summary>
		/// <param name="strPassword">The password for which to derive the key.</param>
		/// <param name="rgbSalt">The key salt to use to derive the key.</param>
		/// <param name="strHashName">The name of the hash algorithm for the operation.</param>
		/// <param name="iterations">The number of iterations for the operation.</param>
		// Token: 0x06002F49 RID: 12105 RVA: 0x000A8633 File Offset: 0x000A6833
		public PasswordDeriveBytes(string strPassword, byte[] rgbSalt, string strHashName, int iterations) : this(strPassword, rgbSalt, strHashName, iterations, new CspParameters())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> class specifying the password, key salt, hash name, and iterations to use to derive the key.</summary>
		/// <param name="password">The password to derive the key for.</param>
		/// <param name="salt">The key salt to use to derive the key.</param>
		/// <param name="hashName">The hash algorithm to use to derive the key.</param>
		/// <param name="iterations">The iteration count to use to derive the key.</param>
		// Token: 0x06002F4A RID: 12106 RVA: 0x000A8645 File Offset: 0x000A6845
		public PasswordDeriveBytes(byte[] password, byte[] salt, string hashName, int iterations) : this(password, salt, hashName, iterations, new CspParameters())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> class with the password, key salt, and cryptographic service provider (CSP) parameters to use to derive the key.</summary>
		/// <param name="strPassword">The password for which to derive the key.</param>
		/// <param name="rgbSalt">The key salt to use to derive the key.</param>
		/// <param name="cspParams">The CSP parameters for the operation.</param>
		// Token: 0x06002F4B RID: 12107 RVA: 0x000A8657 File Offset: 0x000A6857
		public PasswordDeriveBytes(string strPassword, byte[] rgbSalt, CspParameters cspParams) : this(strPassword, rgbSalt, "SHA1", 100, cspParams)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> class specifying the password, key salt, and cryptographic service provider (CSP) to use to derive the key.</summary>
		/// <param name="password">The password to derive the key for.</param>
		/// <param name="salt">The key salt to use to derive the key.</param>
		/// <param name="cspParams">The cryptographic service provider (CSP) parameters for the operation.</param>
		// Token: 0x06002F4C RID: 12108 RVA: 0x000A8669 File Offset: 0x000A6869
		public PasswordDeriveBytes(byte[] password, byte[] salt, CspParameters cspParams) : this(password, salt, "SHA1", 100, cspParams)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> class with the password, key salt, hash name, number of iterations, and cryptographic service provider (CSP) parameters to use to derive the key.</summary>
		/// <param name="strPassword">The password for which to derive the key.</param>
		/// <param name="rgbSalt">The key salt to use to derive the key.</param>
		/// <param name="strHashName">The name of the hash algorithm for the operation.</param>
		/// <param name="iterations">The number of iterations for the operation.</param>
		/// <param name="cspParams">The CSP parameters for the operation.</param>
		// Token: 0x06002F4D RID: 12109 RVA: 0x000A867B File Offset: 0x000A687B
		public PasswordDeriveBytes(string strPassword, byte[] rgbSalt, string strHashName, int iterations, CspParameters cspParams) : this(new UTF8Encoding(false).GetBytes(strPassword), rgbSalt, strHashName, iterations, cspParams)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> class specifying the password, key salt, hash name, iterations, and cryptographic service provider (CSP) to use to derive the key.</summary>
		/// <param name="password">The password to derive the key for.</param>
		/// <param name="salt">The key salt to use to derive the key.</param>
		/// <param name="hashName">The hash algorithm to use to derive the key.</param>
		/// <param name="iterations">The iteration count to use to derive the key.</param>
		/// <param name="cspParams">The cryptographic service provider (CSP) parameters for the operation.</param>
		// Token: 0x06002F4E RID: 12110 RVA: 0x000A8695 File Offset: 0x000A6895
		[SecuritySafeCritical]
		public PasswordDeriveBytes(byte[] password, byte[] salt, string hashName, int iterations, CspParameters cspParams)
		{
			this.IterationCount = iterations;
			this.Salt = salt;
			this.HashName = hashName;
			this._password = password;
		}

		/// <summary>Gets or sets the name of the hash algorithm for the operation.</summary>
		/// <returns>The name of the hash algorithm for the operation.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The name of the hash value is fixed and an attempt is made to change this value.</exception>
		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06002F4F RID: 12111 RVA: 0x000A86BA File Offset: 0x000A68BA
		// (set) Token: 0x06002F50 RID: 12112 RVA: 0x000A86C4 File Offset: 0x000A68C4
		public string HashName
		{
			get
			{
				return this._hashName;
			}
			set
			{
				if (this._baseValue != null)
				{
					throw new CryptographicException(Environment.GetResourceString("Value of '{0}' cannot be changed after the bytes have been retrieved.", new object[]
					{
						"HashName"
					}));
				}
				this._hashName = value;
				this._hash = (HashAlgorithm)CryptoConfig.CreateFromName(this._hashName);
			}
		}

		/// <summary>Gets or sets the number of iterations for the operation.</summary>
		/// <returns>The number of iterations for the operation.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The number of iterations is fixed and an attempt is made to change this value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property cannot be set because its value is out of range. This property requires a non-negative number.</exception>
		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06002F51 RID: 12113 RVA: 0x000A8714 File Offset: 0x000A6914
		// (set) Token: 0x06002F52 RID: 12114 RVA: 0x000A871C File Offset: 0x000A691C
		public int IterationCount
		{
			get
			{
				return this._iterations;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("Positive number required."));
				}
				if (this._baseValue != null)
				{
					throw new CryptographicException(Environment.GetResourceString("Value of '{0}' cannot be changed after the bytes have been retrieved.", new object[]
					{
						"IterationCount"
					}));
				}
				this._iterations = value;
			}
		}

		/// <summary>Gets or sets the key salt value for the operation.</summary>
		/// <returns>The key salt value for the operation.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key salt value is fixed and an attempt is made to change this value.</exception>
		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06002F53 RID: 12115 RVA: 0x000A876F File Offset: 0x000A696F
		// (set) Token: 0x06002F54 RID: 12116 RVA: 0x000A878C File Offset: 0x000A698C
		public byte[] Salt
		{
			get
			{
				if (this._salt == null)
				{
					return null;
				}
				return (byte[])this._salt.Clone();
			}
			set
			{
				if (this._baseValue != null)
				{
					throw new CryptographicException(Environment.GetResourceString("Value of '{0}' cannot be changed after the bytes have been retrieved.", new object[]
					{
						"Salt"
					}));
				}
				if (value == null)
				{
					this._salt = null;
					return;
				}
				this._salt = (byte[])value.Clone();
			}
		}

		/// <summary>Returns pseudo-random key bytes.</summary>
		/// <param name="cb">The number of pseudo-random key bytes to generate.</param>
		/// <returns>A byte array filled with pseudo-random key bytes.</returns>
		// Token: 0x06002F55 RID: 12117 RVA: 0x000A87DC File Offset: 0x000A69DC
		[SecuritySafeCritical]
		[Obsolete("Rfc2898DeriveBytes replaces PasswordDeriveBytes for deriving key material from a password and is preferred in new applications.")]
		public override byte[] GetBytes(int cb)
		{
			if (cb < 1)
			{
				throw new IndexOutOfRangeException("cb");
			}
			int num = 0;
			byte[] array = new byte[cb];
			if (this._baseValue == null)
			{
				this.ComputeBaseValue();
			}
			else if (this._extra != null)
			{
				num = this._extra.Length - this._extraCount;
				if (num >= cb)
				{
					Buffer.InternalBlockCopy(this._extra, this._extraCount, array, 0, cb);
					if (num > cb)
					{
						this._extraCount += cb;
					}
					else
					{
						this._extra = null;
					}
					return array;
				}
				Buffer.InternalBlockCopy(this._extra, num, array, 0, num);
				this._extra = null;
			}
			byte[] array2 = this.ComputeBytes(cb - num);
			Buffer.InternalBlockCopy(array2, 0, array, num, cb - num);
			if (array2.Length + num > cb)
			{
				this._extra = array2;
				this._extraCount = cb - num;
			}
			return array;
		}

		/// <summary>Resets the state of the operation.</summary>
		// Token: 0x06002F56 RID: 12118 RVA: 0x000A88A7 File Offset: 0x000A6AA7
		public override void Reset()
		{
			this._prefix = 0;
			this._extra = null;
			this._baseValue = null;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002F57 RID: 12119 RVA: 0x000A88C0 File Offset: 0x000A6AC0
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				if (this._hash != null)
				{
					this._hash.Dispose();
				}
				if (this._baseValue != null)
				{
					Array.Clear(this._baseValue, 0, this._baseValue.Length);
				}
				if (this._extra != null)
				{
					Array.Clear(this._extra, 0, this._extra.Length);
				}
				if (this._password != null)
				{
					Array.Clear(this._password, 0, this._password.Length);
				}
				if (this._salt != null)
				{
					Array.Clear(this._salt, 0, this._salt.Length);
				}
			}
		}

		/// <summary>Derives a cryptographic key from the <see cref="T:System.Security.Cryptography.PasswordDeriveBytes" /> object.</summary>
		/// <param name="algname">The algorithm name for which to derive the key.</param>
		/// <param name="alghashname">The hash algorithm name to use to derive the key.</param>
		/// <param name="keySize">The size of the key, in bits, to derive.</param>
		/// <param name="rgbIV">The initialization vector (IV) to use to derive the key.</param>
		/// <returns>The derived key.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="keySize" /> parameter is incorrect.  
		///  -or-  
		///  The cryptographic service provider (CSP) cannot be acquired.  
		///  -or-  
		///  The <paramref name="algname" /> parameter is not a valid algorithm name.  
		///  -or-  
		///  The <paramref name="alghashname" /> parameter is not a valid hash algorithm name.</exception>
		// Token: 0x06002F58 RID: 12120 RVA: 0x000A895D File Offset: 0x000A6B5D
		[SecuritySafeCritical]
		public byte[] CryptDeriveKey(string algname, string alghashname, int keySize, byte[] rgbIV)
		{
			if (keySize < 0)
			{
				throw new CryptographicException(Environment.GetResourceString("Specified key is not a valid size for this algorithm."));
			}
			throw new NotSupportedException("CspParameters are not supported by Mono");
		}

		// Token: 0x06002F59 RID: 12121 RVA: 0x000A8980 File Offset: 0x000A6B80
		private byte[] ComputeBaseValue()
		{
			this._hash.Initialize();
			this._hash.TransformBlock(this._password, 0, this._password.Length, this._password, 0);
			if (this._salt != null)
			{
				this._hash.TransformBlock(this._salt, 0, this._salt.Length, this._salt, 0);
			}
			this._hash.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
			this._baseValue = this._hash.Hash;
			this._hash.Initialize();
			for (int i = 1; i < this._iterations - 1; i++)
			{
				this._hash.ComputeHash(this._baseValue);
				this._baseValue = this._hash.Hash;
			}
			return this._baseValue;
		}

		// Token: 0x06002F5A RID: 12122 RVA: 0x000A8A50 File Offset: 0x000A6C50
		[SecurityCritical]
		private byte[] ComputeBytes(int cb)
		{
			int num = 0;
			this._hash.Initialize();
			int num2 = this._hash.HashSize / 8;
			byte[] array = new byte[(cb + num2 - 1) / num2 * num2];
			using (CryptoStream cryptoStream = new CryptoStream(Stream.Null, this._hash, CryptoStreamMode.Write))
			{
				this.HashPrefix(cryptoStream);
				cryptoStream.Write(this._baseValue, 0, this._baseValue.Length);
				cryptoStream.Close();
			}
			Buffer.InternalBlockCopy(this._hash.Hash, 0, array, num, num2);
			num += num2;
			while (cb > num)
			{
				this._hash.Initialize();
				using (CryptoStream cryptoStream2 = new CryptoStream(Stream.Null, this._hash, CryptoStreamMode.Write))
				{
					this.HashPrefix(cryptoStream2);
					cryptoStream2.Write(this._baseValue, 0, this._baseValue.Length);
					cryptoStream2.Close();
				}
				Buffer.InternalBlockCopy(this._hash.Hash, 0, array, num, num2);
				num += num2;
			}
			return array;
		}

		// Token: 0x06002F5B RID: 12123 RVA: 0x000A8B70 File Offset: 0x000A6D70
		private void HashPrefix(CryptoStream cs)
		{
			int num = 0;
			byte[] array = new byte[]
			{
				48,
				48,
				48
			};
			if (this._prefix > 999)
			{
				throw new CryptographicException(Environment.GetResourceString("Requested number of bytes exceeds the maximum."));
			}
			if (this._prefix >= 100)
			{
				byte[] array2 = array;
				int num2 = 0;
				array2[num2] += (byte)(this._prefix / 100);
				num++;
			}
			if (this._prefix >= 10)
			{
				byte[] array3 = array;
				int num3 = num;
				array3[num3] += (byte)(this._prefix % 100 / 10);
				num++;
			}
			if (this._prefix > 0)
			{
				byte[] array4 = array;
				int num4 = num;
				array4[num4] += (byte)(this._prefix % 10);
				num++;
				cs.Write(array, 0, num);
			}
			this._prefix++;
		}

		// Token: 0x04002188 RID: 8584
		private int _extraCount;

		// Token: 0x04002189 RID: 8585
		private int _prefix;

		// Token: 0x0400218A RID: 8586
		private int _iterations;

		// Token: 0x0400218B RID: 8587
		private byte[] _baseValue;

		// Token: 0x0400218C RID: 8588
		private byte[] _extra;

		// Token: 0x0400218D RID: 8589
		private byte[] _salt;

		// Token: 0x0400218E RID: 8590
		private string _hashName;

		// Token: 0x0400218F RID: 8591
		private byte[] _password;

		// Token: 0x04002190 RID: 8592
		private HashAlgorithm _hash;
	}
}
