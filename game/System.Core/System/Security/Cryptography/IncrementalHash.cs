using System;
using Unity;

namespace System.Security.Cryptography
{
	/// <summary>Provides support for computing a hash or Hash-based Message Authentication Code (HMAC) value incrementally across several segments.</summary>
	// Token: 0x02000037 RID: 55
	public sealed class IncrementalHash : IDisposable
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00002933 File Offset: 0x00000B33
		private IncrementalHash(HashAlgorithmName name, HashAlgorithm hash)
		{
			this._algorithmName = name;
			this._hash = hash;
		}

		/// <summary>Gets the name of the algorithm being performed.</summary>
		/// <returns>The name of the algorithm being performed.</returns>
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00002949 File Offset: 0x00000B49
		public HashAlgorithmName AlgorithmName
		{
			get
			{
				return this._algorithmName;
			}
		}

		/// <summary>Appends the specified data to the data already processed in the hash or HMAC.</summary>
		/// <param name="data">The data to process.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="data" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Security.Cryptography.IncrementalHash" /> object has already been disposed.</exception>
		// Token: 0x060000BC RID: 188 RVA: 0x00002951 File Offset: 0x00000B51
		public void AppendData(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this.AppendData(data, 0, data.Length);
		}

		/// <summary>Appends the specified number of bytes from the specified data, starting at the specified offset, to the data already processed in the hash or Hash-based Message Authentication Code (HMAC).</summary>
		/// <param name="data">The data to process.</param>
		/// <param name="offset">The offset into the byte array from which to begin using data.</param>
		/// <param name="count">The number of bytes to use from <paramref name="data" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> or <paramref name="offset" /> is negative.-or-<paramref name="count" /> is larger than the lenght of <paramref name="data" />.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the data length.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Security.Cryptography.IncrementalHash" /> object has already been disposed.</exception>
		// Token: 0x060000BD RID: 189 RVA: 0x0000296C File Offset: 0x00000B6C
		public void AppendData(byte[] data, int offset, int count)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Non negative number is required.");
			}
			if (count < 0 || count > data.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (data.Length - count < offset)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (this._disposed)
			{
				throw new ObjectDisposedException(typeof(IncrementalHash).Name);
			}
			if (this._resetPending)
			{
				this._hash.Initialize();
				this._resetPending = false;
			}
			this._hash.TransformBlock(data, offset, count, null, 0);
		}

		/// <summary>Retrieves the hash or Hash-based Message Authentication Code (HMAC) for the data accumulated from prior calls to the
		///   <see cref="M:System.Security.Cryptography.IncrementalHash.AppendData(System.Byte[])" /> method,  and resets the object to its initial state.</summary>
		/// <returns>The computed hash or HMAC.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Security.Cryptography.IncrementalHash" /> object has already been disposed.</exception>
		// Token: 0x060000BE RID: 190 RVA: 0x00002A0C File Offset: 0x00000C0C
		public byte[] GetHashAndReset()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(typeof(IncrementalHash).Name);
			}
			if (this._resetPending)
			{
				this._hash.Initialize();
			}
			this._hash.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
			byte[] hash = this._hash.Hash;
			this._resetPending = true;
			return hash;
		}

		/// <summary>Releases the resources used by the current instance of the <see cref="T:System.Security.Cryptography.IncrementalHash" /> class.</summary>
		// Token: 0x060000BF RID: 191 RVA: 0x00002A6E File Offset: 0x00000C6E
		public void Dispose()
		{
			this._disposed = true;
			if (this._hash != null)
			{
				this._hash.Dispose();
				this._hash = null;
			}
		}

		/// <summary>Creates an <see cref="T:System.Security.Cryptography.IncrementalHash" /> for the specified algorithm.</summary>
		/// <param name="hashAlgorithm">The name of the hash algorithm to perform.</param>
		/// <returns>An <see cref="T:System.Security.Cryptography.IncrementalHash" /> instance ready to compute the hash algorithm specified by <paramref name="hashAlgorithm" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="hashAlgorithm" />.<see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /> is <see langword="null" /> or an empty string.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <paramref name="hashAlgorithm" /> is not a known hash algorithm.</exception>
		// Token: 0x060000C0 RID: 192 RVA: 0x00002A91 File Offset: 0x00000C91
		public static IncrementalHash CreateHash(HashAlgorithmName hashAlgorithm)
		{
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw new ArgumentException("The hash algorithm name cannot be null or empty.", "hashAlgorithm");
			}
			return new IncrementalHash(hashAlgorithm, IncrementalHash.GetHashAlgorithm(hashAlgorithm));
		}

		/// <summary>Creates an <see cref="T:System.Security.Cryptography.IncrementalHash" /> for the Hash-based Message Authentication Code (HMAC)
		///   algorithm using the specified hash algorithm and key.</summary>
		/// <param name="hashAlgorithm">The name of the hash algorithm to perform within the HMAC.</param>
		/// <param name="key">     The secret key for the HMAC. The key can be of any length, but a key longer than the output size
		///   of the specified hash algorithm will be hashed to derive a correctly-sized key. Therefore,
		///   the recommended size of the secret key is the output size of the specified hash algorithm.</param>
		/// <returns>An instance of the <see cref="T:System.Security.Cryptography.IncrementalHash" /> class ready to compute the specified hash algorithm.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="hashAlgorithm" />.<see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /> is <see langword="null" /> or
		///   an empty string.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <paramref name="hashAlgorithm" /> is not a known hash algorithm.</exception>
		// Token: 0x060000C1 RID: 193 RVA: 0x00002ABD File Offset: 0x00000CBD
		public static IncrementalHash CreateHMAC(HashAlgorithmName hashAlgorithm, byte[] key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw new ArgumentException("The hash algorithm name cannot be null or empty.", "hashAlgorithm");
			}
			return new IncrementalHash(hashAlgorithm, IncrementalHash.GetHMAC(hashAlgorithm, key));
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00002AF8 File Offset: 0x00000CF8
		private static HashAlgorithm GetHashAlgorithm(HashAlgorithmName hashAlgorithm)
		{
			if (hashAlgorithm == HashAlgorithmName.MD5)
			{
				return new MD5CryptoServiceProvider();
			}
			if (hashAlgorithm == HashAlgorithmName.SHA1)
			{
				return new SHA1CryptoServiceProvider();
			}
			if (hashAlgorithm == HashAlgorithmName.SHA256)
			{
				return new SHA256CryptoServiceProvider();
			}
			if (hashAlgorithm == HashAlgorithmName.SHA384)
			{
				return new SHA384CryptoServiceProvider();
			}
			if (hashAlgorithm == HashAlgorithmName.SHA512)
			{
				return new SHA512CryptoServiceProvider();
			}
			throw new CryptographicException(-2146893816);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00002B70 File Offset: 0x00000D70
		private static HashAlgorithm GetHMAC(HashAlgorithmName hashAlgorithm, byte[] key)
		{
			if (hashAlgorithm == HashAlgorithmName.MD5)
			{
				return new HMACMD5(key);
			}
			if (hashAlgorithm == HashAlgorithmName.SHA1)
			{
				return new HMACSHA1(key);
			}
			if (hashAlgorithm == HashAlgorithmName.SHA256)
			{
				return new HMACSHA256(key);
			}
			if (hashAlgorithm == HashAlgorithmName.SHA384)
			{
				return new HMACSHA384(key);
			}
			if (hashAlgorithm == HashAlgorithmName.SHA512)
			{
				return new HMACSHA512(key);
			}
			throw new CryptographicException(-2146893816);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00002BEB File Offset: 0x00000DEB
		public void AppendData(ReadOnlySpan<byte> data)
		{
			this.AppendData(data.ToArray());
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00002BFC File Offset: 0x00000DFC
		public bool TryGetHashAndReset(Span<byte> destination, out int bytesWritten)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(typeof(IncrementalHash).Name);
			}
			byte[] hashAndReset = this.GetHashAndReset();
			if (hashAndReset.AsSpan<byte>().TryCopyTo(destination))
			{
				bytesWritten = hashAndReset.Length;
				return true;
			}
			bytesWritten = 0;
			return false;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000235B File Offset: 0x0000055B
		internal IncrementalHash()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040002D5 RID: 725
		private const int NTE_BAD_ALGID = -2146893816;

		// Token: 0x040002D6 RID: 726
		private readonly HashAlgorithmName _algorithmName;

		// Token: 0x040002D7 RID: 727
		private HashAlgorithm _hash;

		// Token: 0x040002D8 RID: 728
		private bool _disposed;

		// Token: 0x040002D9 RID: 729
		private bool _resetPending;
	}
}
