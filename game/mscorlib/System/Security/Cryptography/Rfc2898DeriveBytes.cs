using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Text;
using Internal.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Implements password-based key derivation functionality, PBKDF2, by using a pseudo-random number generator based on <see cref="T:System.Security.Cryptography.HMACSHA1" />.</summary>
	// Token: 0x02000466 RID: 1126
	public class Rfc2898DeriveBytes : DeriveBytes
	{
		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06002DBB RID: 11707 RVA: 0x000A3B54 File Offset: 0x000A1D54
		public HashAlgorithmName HashAlgorithm
		{
			[CompilerGenerated]
			get
			{
				return this.<HashAlgorithm>k__BackingField;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class using a password, a salt, and number of iterations to derive the key.</summary>
		/// <param name="password">The password used to derive the key.</param>
		/// <param name="salt">The key salt used to derive the key.</param>
		/// <param name="iterations">The number of iterations for the operation.</param>
		/// <exception cref="T:System.ArgumentException">The specified salt size is smaller than 8 bytes or the iteration count is less than 1.</exception>
		/// <exception cref="T:System.ArgumentNullException">The password or salt is <see langword="null" />.</exception>
		// Token: 0x06002DBC RID: 11708 RVA: 0x000A3B5C File Offset: 0x000A1D5C
		public Rfc2898DeriveBytes(byte[] password, byte[] salt, int iterations) : this(password, salt, iterations, HashAlgorithmName.SHA1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class using the specified password, salt, number of iterations and the hash algorithm name to derive the key.</summary>
		/// <param name="password">The password to use to derive the key.</param>
		/// <param name="salt">The key salt to use to derive the key.</param>
		/// <param name="iterations">The number of iterations for the operation.</param>
		/// <param name="hashAlgorithm">The hash algorithm to use to derive the key.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="saltSize" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /> property of <paramref name="hashAlgorithm" /> is either <see langword="null" /> or <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">Hash algorithm name is invalid.</exception>
		// Token: 0x06002DBD RID: 11709 RVA: 0x000A3B6C File Offset: 0x000A1D6C
		public Rfc2898DeriveBytes(byte[] password, byte[] salt, int iterations, HashAlgorithmName hashAlgorithm)
		{
			if (salt == null)
			{
				throw new ArgumentNullException("salt");
			}
			if (salt.Length < 8)
			{
				throw new ArgumentException("Salt is not at least eight bytes.", "salt");
			}
			if (iterations <= 0)
			{
				throw new ArgumentOutOfRangeException("iterations", "Positive number required.");
			}
			if (password == null)
			{
				throw new NullReferenceException();
			}
			this._salt = salt.CloneByteArray();
			this._iterations = (uint)iterations;
			this._password = password.CloneByteArray();
			this.HashAlgorithm = hashAlgorithm;
			this._hmac = this.OpenHmac();
			this._blockSize = this._hmac.HashSize >> 3;
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class using a password and salt to derive the key.</summary>
		/// <param name="password">The password used to derive the key.</param>
		/// <param name="salt">The key salt used to derive the key.</param>
		/// <exception cref="T:System.ArgumentException">The specified salt size is smaller than 8 bytes or the iteration count is less than 1.</exception>
		/// <exception cref="T:System.ArgumentNullException">The password or salt is <see langword="null" />.</exception>
		// Token: 0x06002DBE RID: 11710 RVA: 0x000A3C0C File Offset: 0x000A1E0C
		public Rfc2898DeriveBytes(string password, byte[] salt) : this(password, salt, 1000)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class using a password, a salt, and number of iterations to derive the key.</summary>
		/// <param name="password">The password used to derive the key.</param>
		/// <param name="salt">The key salt used to derive the key.</param>
		/// <param name="iterations">The number of iterations for the operation.</param>
		/// <exception cref="T:System.ArgumentException">The specified salt size is smaller than 8 bytes or the iteration count is less than 1.</exception>
		/// <exception cref="T:System.ArgumentNullException">The password or salt is <see langword="null" />.</exception>
		// Token: 0x06002DBF RID: 11711 RVA: 0x000A3C1B File Offset: 0x000A1E1B
		public Rfc2898DeriveBytes(string password, byte[] salt, int iterations) : this(password, salt, iterations, HashAlgorithmName.SHA1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class using the specified password, salt, number of iterations and the hash algorithm name to derive the key.</summary>
		/// <param name="password">The password to use to derive the key.</param>
		/// <param name="salt">The key salt to use to derive the key.</param>
		/// <param name="iterations">The number of iterations for the operation.</param>
		/// <param name="hashAlgorithm">The hash algorithm to use to derive the key.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /> property of <paramref name="hashAlgorithm" /> is either <see langword="null" /> or <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">Hash algorithm name is invalid.</exception>
		// Token: 0x06002DC0 RID: 11712 RVA: 0x000A3C2B File Offset: 0x000A1E2B
		public Rfc2898DeriveBytes(string password, byte[] salt, int iterations, HashAlgorithmName hashAlgorithm) : this(Encoding.UTF8.GetBytes(password), salt, iterations, hashAlgorithm)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class using the password and salt size to derive the key.</summary>
		/// <param name="password">The password used to derive the key.</param>
		/// <param name="saltSize">The size of the random salt that you want the class to generate.</param>
		/// <exception cref="T:System.ArgumentException">The specified salt size is smaller than 8 bytes.</exception>
		/// <exception cref="T:System.ArgumentNullException">The password or salt is <see langword="null" />.</exception>
		// Token: 0x06002DC1 RID: 11713 RVA: 0x000A3C42 File Offset: 0x000A1E42
		public Rfc2898DeriveBytes(string password, int saltSize) : this(password, saltSize, 1000)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class using a password, a salt size, and number of iterations to derive the key.</summary>
		/// <param name="password">The password used to derive the key.</param>
		/// <param name="saltSize">The size of the random salt that you want the class to generate.</param>
		/// <param name="iterations">The number of iterations for the operation.</param>
		/// <exception cref="T:System.ArgumentException">The specified salt size is smaller than 8 bytes or the iteration count is less than 1.</exception>
		/// <exception cref="T:System.ArgumentNullException">The password or salt is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="iterations" /> is out of range. This parameter requires a non-negative number.</exception>
		// Token: 0x06002DC2 RID: 11714 RVA: 0x000A3C51 File Offset: 0x000A1E51
		public Rfc2898DeriveBytes(string password, int saltSize, int iterations) : this(password, saltSize, iterations, HashAlgorithmName.SHA1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class using the specified password, salt size, number of iterations and the hash algorithm name to derive the key.</summary>
		/// <param name="password">The password to use to derive the key.</param>
		/// <param name="saltSize">The size of the random salt that you want the class to generate.</param>
		/// <param name="iterations">The number of iterations for the operation.</param>
		/// <param name="hashAlgorithm">The hash algorithm to use to derive the key.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="saltSize" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /> property of <paramref name="hashAlgorithm" /> is either <see langword="null" /> or <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">Hash algorithm name is invalid.</exception>
		// Token: 0x06002DC3 RID: 11715 RVA: 0x000A3C64 File Offset: 0x000A1E64
		public Rfc2898DeriveBytes(string password, int saltSize, int iterations, HashAlgorithmName hashAlgorithm)
		{
			if (saltSize < 0)
			{
				throw new ArgumentOutOfRangeException("saltSize", "Non-negative number required.");
			}
			if (saltSize < 8)
			{
				throw new ArgumentException("Salt is not at least eight bytes.", "saltSize");
			}
			if (iterations <= 0)
			{
				throw new ArgumentOutOfRangeException("iterations", "Positive number required.");
			}
			this._salt = Helpers.GenerateRandom(saltSize);
			this._iterations = (uint)iterations;
			this._password = Encoding.UTF8.GetBytes(password);
			this.HashAlgorithm = hashAlgorithm;
			this._hmac = this.OpenHmac();
			this._blockSize = this._hmac.HashSize >> 3;
			this.Initialize();
		}

		/// <summary>Gets or sets the number of iterations for the operation.</summary>
		/// <returns>The number of iterations for the operation.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of iterations is less than 1.</exception>
		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06002DC4 RID: 11716 RVA: 0x000A3D04 File Offset: 0x000A1F04
		// (set) Token: 0x06002DC5 RID: 11717 RVA: 0x000A3D0C File Offset: 0x000A1F0C
		public int IterationCount
		{
			get
			{
				return (int)this._iterations;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value", "Positive number required.");
				}
				this._iterations = (uint)value;
				this.Initialize();
			}
		}

		/// <summary>Gets or sets the key salt value for the operation.</summary>
		/// <returns>The key salt value for the operation.</returns>
		/// <exception cref="T:System.ArgumentException">The specified salt size is smaller than 8 bytes.</exception>
		/// <exception cref="T:System.ArgumentNullException">The salt is <see langword="null" />.</exception>
		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06002DC6 RID: 11718 RVA: 0x000A3D2F File Offset: 0x000A1F2F
		// (set) Token: 0x06002DC7 RID: 11719 RVA: 0x000A3D3C File Offset: 0x000A1F3C
		public byte[] Salt
		{
			get
			{
				return this._salt.CloneByteArray();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length < 8)
				{
					throw new ArgumentException("Salt is not at least eight bytes.");
				}
				this._salt = value.CloneByteArray();
				this.Initialize();
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002DC8 RID: 11720 RVA: 0x000A3D70 File Offset: 0x000A1F70
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._hmac != null)
				{
					this._hmac.Dispose();
					this._hmac = null;
				}
				if (this._buffer != null)
				{
					Array.Clear(this._buffer, 0, this._buffer.Length);
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
			base.Dispose(disposing);
		}

		/// <summary>Returns the pseudo-random key for this object.</summary>
		/// <param name="cb">The number of pseudo-random key bytes to generate.</param>
		/// <returns>A byte array filled with pseudo-random key bytes.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="cb" /> is out of range. This parameter requires a non-negative number.</exception>
		// Token: 0x06002DC9 RID: 11721 RVA: 0x000A3DF8 File Offset: 0x000A1FF8
		public override byte[] GetBytes(int cb)
		{
			if (cb <= 0)
			{
				throw new ArgumentOutOfRangeException("cb", "Positive number required.");
			}
			byte[] array = new byte[cb];
			int i = 0;
			int num = this._endIndex - this._startIndex;
			if (num > 0)
			{
				if (cb < num)
				{
					Buffer.BlockCopy(this._buffer, this._startIndex, array, 0, cb);
					this._startIndex += cb;
					return array;
				}
				Buffer.BlockCopy(this._buffer, this._startIndex, array, 0, num);
				this._startIndex = (this._endIndex = 0);
				i += num;
			}
			while (i < cb)
			{
				byte[] src = this.Func();
				int num2 = cb - i;
				if (num2 <= this._blockSize)
				{
					Buffer.BlockCopy(src, 0, array, i, num2);
					i += num2;
					Buffer.BlockCopy(src, num2, this._buffer, this._startIndex, this._blockSize - num2);
					this._endIndex += this._blockSize - num2;
					return array;
				}
				Buffer.BlockCopy(src, 0, array, i, this._blockSize);
				i += this._blockSize;
			}
			return array;
		}

		/// <summary>Derives a cryptographic key from the <see cref="T:System.Security.Cryptography.Rfc2898DeriveBytes" /> object.</summary>
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
		// Token: 0x06002DCA RID: 11722 RVA: 0x0001B98B File Offset: 0x00019B8B
		public byte[] CryptDeriveKey(string algname, string alghashname, int keySize, byte[] rgbIV)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Resets the state of the operation.</summary>
		// Token: 0x06002DCB RID: 11723 RVA: 0x000A3F0A File Offset: 0x000A210A
		public override void Reset()
		{
			this.Initialize();
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x000A3F14 File Offset: 0x000A2114
		private HMAC OpenHmac()
		{
			HashAlgorithmName hashAlgorithm = this.HashAlgorithm;
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw new CryptographicException("The hash algorithm name cannot be null or empty.");
			}
			if (hashAlgorithm == HashAlgorithmName.SHA1)
			{
				return new HMACSHA1(this._password);
			}
			if (hashAlgorithm == HashAlgorithmName.SHA256)
			{
				return new HMACSHA256(this._password);
			}
			if (hashAlgorithm == HashAlgorithmName.SHA384)
			{
				return new HMACSHA384(this._password);
			}
			if (hashAlgorithm == HashAlgorithmName.SHA512)
			{
				return new HMACSHA512(this._password);
			}
			throw new CryptographicException(SR.Format("'{0}' is not a known hash algorithm.", hashAlgorithm.Name));
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x000A3FBC File Offset: 0x000A21BC
		private void Initialize()
		{
			if (this._buffer != null)
			{
				Array.Clear(this._buffer, 0, this._buffer.Length);
			}
			this._buffer = new byte[this._blockSize];
			this._block = 1U;
			this._startIndex = (this._endIndex = 0);
		}

		// Token: 0x06002DCE RID: 11726 RVA: 0x000A4010 File Offset: 0x000A2210
		private byte[] Func()
		{
			byte[] array = new byte[this._salt.Length + 4];
			Buffer.BlockCopy(this._salt, 0, array, 0, this._salt.Length);
			Helpers.WriteInt(this._block, array, this._salt.Length);
			byte[] array2 = ArrayPool<byte>.Shared.Rent(this._blockSize);
			byte[] result;
			try
			{
				Span<byte> span = new Span<byte>(array2, 0, this._blockSize);
				int num;
				if (!this._hmac.TryComputeHash(array, span, out num) || num != this._blockSize)
				{
					throw new CryptographicException();
				}
				byte[] array3 = new byte[this._blockSize];
				span.CopyTo(array3);
				int num2 = 2;
				while ((long)num2 <= (long)((ulong)this._iterations))
				{
					if (!this._hmac.TryComputeHash(span, span, out num) || num != this._blockSize)
					{
						throw new CryptographicException();
					}
					for (int i = 0; i < this._blockSize; i++)
					{
						byte[] array4 = array3;
						int num3 = i;
						array4[num3] ^= array2[i];
					}
					num2++;
				}
				this._block += 1U;
				result = array3;
			}
			finally
			{
				Array.Clear(array2, 0, this._blockSize);
				ArrayPool<byte>.Shared.Return(array2, false);
			}
			return result;
		}

		// Token: 0x040020C3 RID: 8387
		private const int MinimumSaltSize = 8;

		// Token: 0x040020C4 RID: 8388
		private readonly byte[] _password;

		// Token: 0x040020C5 RID: 8389
		private byte[] _salt;

		// Token: 0x040020C6 RID: 8390
		private uint _iterations;

		// Token: 0x040020C7 RID: 8391
		private HMAC _hmac;

		// Token: 0x040020C8 RID: 8392
		private int _blockSize;

		// Token: 0x040020C9 RID: 8393
		private byte[] _buffer;

		// Token: 0x040020CA RID: 8394
		private uint _block;

		// Token: 0x040020CB RID: 8395
		private int _startIndex;

		// Token: 0x040020CC RID: 8396
		private int _endIndex;

		// Token: 0x040020CD RID: 8397
		[CompilerGenerated]
		private readonly HashAlgorithmName <HashAlgorithm>k__BackingField;
	}
}
