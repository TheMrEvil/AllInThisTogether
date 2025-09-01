using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract class from which all implementations of Hash-based Message Authentication Code (HMAC) must derive.</summary>
	// Token: 0x0200048E RID: 1166
	[ComVisible(true)]
	public abstract class HMAC : KeyedHashAlgorithm
	{
		/// <summary>Gets or sets the block size to use in the hash value.</summary>
		/// <returns>The block size to use in the hash value.</returns>
		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06002EE9 RID: 12009 RVA: 0x000A789B File Offset: 0x000A5A9B
		// (set) Token: 0x06002EEA RID: 12010 RVA: 0x000A78A3 File Offset: 0x000A5AA3
		protected int BlockSizeValue
		{
			get
			{
				return this.blockSizeValue;
			}
			set
			{
				this.blockSizeValue = value;
			}
		}

		// Token: 0x06002EEB RID: 12011 RVA: 0x000A78AC File Offset: 0x000A5AAC
		private void UpdateIOPadBuffers()
		{
			if (this.m_inner == null)
			{
				this.m_inner = new byte[this.BlockSizeValue];
			}
			if (this.m_outer == null)
			{
				this.m_outer = new byte[this.BlockSizeValue];
			}
			for (int i = 0; i < this.BlockSizeValue; i++)
			{
				this.m_inner[i] = 54;
				this.m_outer[i] = 92;
			}
			for (int i = 0; i < this.KeyValue.Length; i++)
			{
				byte[] inner = this.m_inner;
				int num = i;
				inner[num] ^= this.KeyValue[i];
				byte[] outer = this.m_outer;
				int num2 = i;
				outer[num2] ^= this.KeyValue[i];
			}
		}

		// Token: 0x06002EEC RID: 12012 RVA: 0x000A7958 File Offset: 0x000A5B58
		internal void InitializeKey(byte[] key)
		{
			this.m_inner = null;
			this.m_outer = null;
			if (key.Length > this.BlockSizeValue)
			{
				this.KeyValue = this.m_hash1.ComputeHash(key);
			}
			else
			{
				this.KeyValue = (byte[])key.Clone();
			}
			this.UpdateIOPadBuffers();
		}

		/// <summary>Gets or sets the key to use in the hash algorithm.</summary>
		/// <returns>The key to use in the hash algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An attempt is made to change the <see cref="P:System.Security.Cryptography.HMAC.Key" /> property after hashing has begun.</exception>
		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06002EED RID: 12013 RVA: 0x000A79A9 File Offset: 0x000A5BA9
		// (set) Token: 0x06002EEE RID: 12014 RVA: 0x000A79BB File Offset: 0x000A5BBB
		public override byte[] Key
		{
			get
			{
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (this.m_hashing)
				{
					throw new CryptographicException(Environment.GetResourceString("Hash key cannot be changed after the first write to the stream."));
				}
				this.InitializeKey(value);
			}
		}

		/// <summary>Gets or sets the name of the hash algorithm to use for hashing.</summary>
		/// <returns>The name of the hash algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The current hash algorithm cannot be changed.</exception>
		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06002EEF RID: 12015 RVA: 0x000A79DC File Offset: 0x000A5BDC
		// (set) Token: 0x06002EF0 RID: 12016 RVA: 0x000A79E4 File Offset: 0x000A5BE4
		public string HashName
		{
			get
			{
				return this.m_hashName;
			}
			set
			{
				if (this.m_hashing)
				{
					throw new CryptographicException(Environment.GetResourceString("Hash name cannot be changed after the first write to the stream."));
				}
				this.m_hashName = value;
				this.m_hash1 = HashAlgorithm.Create(this.m_hashName);
				this.m_hash2 = HashAlgorithm.Create(this.m_hashName);
			}
		}

		/// <summary>Creates an instance of the default implementation of a Hash-based Message Authentication Code (HMAC).</summary>
		/// <returns>A new SHA-1 instance, unless the default settings have been changed by using the &lt;cryptoClass&gt; element.</returns>
		// Token: 0x06002EF1 RID: 12017 RVA: 0x000A7A32 File Offset: 0x000A5C32
		public new static HMAC Create()
		{
			return HMAC.Create("System.Security.Cryptography.HMAC");
		}

		/// <summary>Creates an instance of the specified implementation of a Hash-based Message Authentication Code (HMAC).</summary>
		/// <param name="algorithmName">The HMAC implementation to use. The following table shows the valid values for the <paramref name="algorithmName" /> parameter and the algorithms they map to.  
		///   Parameter value  
		///
		///   Implements  
		///
		///   System.Security.Cryptography.HMAC  
		///
		///  <see cref="T:System.Security.Cryptography.HMACSHA1" /> System.Security.Cryptography.KeyedHashAlgorithm  
		///
		///  <see cref="T:System.Security.Cryptography.HMACSHA1" /> HMACMD5  
		///
		///  <see cref="T:System.Security.Cryptography.HMACMD5" /> System.Security.Cryptography.HMACMD5  
		///
		///  <see cref="T:System.Security.Cryptography.HMACMD5" /> HMACRIPEMD160  
		///
		///  <see cref="T:System.Security.Cryptography.HMACRIPEMD160" /> System.Security.Cryptography.HMACRIPEMD160  
		///
		///  <see cref="T:System.Security.Cryptography.HMACRIPEMD160" /> HMACSHA1  
		///
		///  <see cref="T:System.Security.Cryptography.HMACSHA1" /> System.Security.Cryptography.HMACSHA1  
		///
		///  <see cref="T:System.Security.Cryptography.HMACSHA1" /> HMACSHA256  
		///
		///  <see cref="T:System.Security.Cryptography.HMACSHA256" /> System.Security.Cryptography.HMACSHA256  
		///
		///  <see cref="T:System.Security.Cryptography.HMACSHA256" /> HMACSHA384  
		///
		///  <see cref="T:System.Security.Cryptography.HMACSHA384" /> System.Security.Cryptography.HMACSHA384  
		///
		///  <see cref="T:System.Security.Cryptography.HMACSHA384" /> HMACSHA512  
		///
		///  <see cref="T:System.Security.Cryptography.HMACSHA512" /> System.Security.Cryptography.HMACSHA512  
		///
		///  <see cref="T:System.Security.Cryptography.HMACSHA512" /> MACTripleDES  
		///
		///  <see cref="T:System.Security.Cryptography.MACTripleDES" /> System.Security.Cryptography.MACTripleDES  
		///
		///  <see cref="T:System.Security.Cryptography.MACTripleDES" /></param>
		/// <returns>A new instance of the specified HMAC implementation.</returns>
		// Token: 0x06002EF2 RID: 12018 RVA: 0x000A7A3E File Offset: 0x000A5C3E
		public new static HMAC Create(string algorithmName)
		{
			return (HMAC)CryptoConfig.CreateFromName(algorithmName);
		}

		/// <summary>Initializes an instance of the default implementation of <see cref="T:System.Security.Cryptography.HMAC" />.</summary>
		// Token: 0x06002EF3 RID: 12019 RVA: 0x000A7A4B File Offset: 0x000A5C4B
		public override void Initialize()
		{
			this.m_hash1.Initialize();
			this.m_hash2.Initialize();
			this.m_hashing = false;
		}

		/// <summary>When overridden in a derived class, routes data written to the object into the default <see cref="T:System.Security.Cryptography.HMAC" /> hash algorithm for computing the hash value.</summary>
		/// <param name="rgb">The input data.</param>
		/// <param name="ib">The offset into the byte array from which to begin using data.</param>
		/// <param name="cb">The number of bytes in the array to use as data.</param>
		// Token: 0x06002EF4 RID: 12020 RVA: 0x000A7A6C File Offset: 0x000A5C6C
		protected override void HashCore(byte[] rgb, int ib, int cb)
		{
			if (!this.m_hashing)
			{
				this.m_hash1.TransformBlock(this.m_inner, 0, this.m_inner.Length, this.m_inner, 0);
				this.m_hashing = true;
			}
			this.m_hash1.TransformBlock(rgb, ib, cb, rgb, ib);
		}

		/// <summary>When overridden in a derived class, finalizes the hash computation after the last data is processed by the cryptographic stream object.</summary>
		/// <returns>The computed hash code in a byte array.</returns>
		// Token: 0x06002EF5 RID: 12021 RVA: 0x000A7ABC File Offset: 0x000A5CBC
		protected override byte[] HashFinal()
		{
			if (!this.m_hashing)
			{
				this.m_hash1.TransformBlock(this.m_inner, 0, this.m_inner.Length, this.m_inner, 0);
				this.m_hashing = true;
			}
			this.m_hash1.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
			byte[] hashValue = this.m_hash1.HashValue;
			this.m_hash2.TransformBlock(this.m_outer, 0, this.m_outer.Length, this.m_outer, 0);
			this.m_hash2.TransformBlock(hashValue, 0, hashValue.Length, hashValue, 0);
			this.m_hashing = false;
			this.m_hash2.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
			return this.m_hash2.HashValue;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.HMAC" /> class when a key change is legitimate and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002EF6 RID: 12022 RVA: 0x000A7B74 File Offset: 0x000A5D74
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.m_hash1 != null)
				{
					((IDisposable)this.m_hash1).Dispose();
				}
				if (this.m_hash2 != null)
				{
					((IDisposable)this.m_hash2).Dispose();
				}
				if (this.m_inner != null)
				{
					Array.Clear(this.m_inner, 0, this.m_inner.Length);
				}
				if (this.m_outer != null)
				{
					Array.Clear(this.m_outer, 0, this.m_outer.Length);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x000A7BEC File Offset: 0x000A5DEC
		internal static HashAlgorithm GetHashAlgorithmWithFipsFallback(Func<HashAlgorithm> createStandardHashAlgorithmCallback, Func<HashAlgorithm> createFipsHashAlgorithmCallback)
		{
			if (CryptoConfig.AllowOnlyFipsAlgorithms)
			{
				try
				{
					return createFipsHashAlgorithmCallback();
				}
				catch (PlatformNotSupportedException ex)
				{
					throw new InvalidOperationException(ex.Message, ex);
				}
			}
			return createStandardHashAlgorithmCallback();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.HMAC" /> class.</summary>
		// Token: 0x06002EF8 RID: 12024 RVA: 0x000A7C30 File Offset: 0x000A5E30
		protected HMAC()
		{
		}

		// Token: 0x04002165 RID: 8549
		private int blockSizeValue = 64;

		// Token: 0x04002166 RID: 8550
		internal string m_hashName;

		// Token: 0x04002167 RID: 8551
		internal HashAlgorithm m_hash1;

		// Token: 0x04002168 RID: 8552
		internal HashAlgorithm m_hash2;

		// Token: 0x04002169 RID: 8553
		private byte[] m_inner;

		// Token: 0x0400216A RID: 8554
		private byte[] m_outer;

		// Token: 0x0400216B RID: 8555
		private bool m_hashing;
	}
}
