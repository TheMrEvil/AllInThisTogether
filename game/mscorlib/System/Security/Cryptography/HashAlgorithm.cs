using System;
using System.Buffers;
using System.IO;

namespace System.Security.Cryptography
{
	/// <summary>Represents the base class from which all implementations of cryptographic hash algorithms must derive.</summary>
	// Token: 0x02000471 RID: 1137
	public abstract class HashAlgorithm : IDisposable, ICryptoTransform
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.HashAlgorithm" /> class.</summary>
		// Token: 0x06002E01 RID: 11777 RVA: 0x0000259F File Offset: 0x0000079F
		protected HashAlgorithm()
		{
		}

		/// <summary>Creates an instance of the default implementation of a hash algorithm.</summary>
		/// <returns>A new <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" /> instance, unless the default settings have been changed using the .</returns>
		// Token: 0x06002E02 RID: 11778 RVA: 0x000A59F0 File Offset: 0x000A3BF0
		public static HashAlgorithm Create()
		{
			return CryptoConfigForwarder.CreateDefaultHashAlgorithm();
		}

		/// <summary>Creates an instance of the specified implementation of a hash algorithm.</summary>
		/// <param name="hashName">The hash algorithm implementation to use. The following table shows the valid values for the <paramref name="hashName" /> parameter and the algorithms they map to.  
		///   Parameter value  
		///
		///   Implements  
		///
		///   SHA  
		///
		///  <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" /> SHA1  
		///
		///  <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" /> System.Security.Cryptography.SHA1  
		///
		///  <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" /> System.Security.Cryptography.HashAlgorithm  
		///
		///  <see cref="T:System.Security.Cryptography.SHA1CryptoServiceProvider" /> MD5  
		///
		///  <see cref="T:System.Security.Cryptography.MD5CryptoServiceProvider" /> System.Security.Cryptography.MD5  
		///
		///  <see cref="T:System.Security.Cryptography.MD5CryptoServiceProvider" /> SHA256  
		///
		///  <see cref="T:System.Security.Cryptography.SHA256Managed" /> SHA-256  
		///
		///  <see cref="T:System.Security.Cryptography.SHA256Managed" /> System.Security.Cryptography.SHA256  
		///
		///  <see cref="T:System.Security.Cryptography.SHA256Managed" /> SHA384  
		///
		///  <see cref="T:System.Security.Cryptography.SHA384Managed" /> SHA-384  
		///
		///  <see cref="T:System.Security.Cryptography.SHA384Managed" /> System.Security.Cryptography.SHA384  
		///
		///  <see cref="T:System.Security.Cryptography.SHA384Managed" /> SHA512  
		///
		///  <see cref="T:System.Security.Cryptography.SHA512Managed" /> SHA-512  
		///
		///  <see cref="T:System.Security.Cryptography.SHA512Managed" /> System.Security.Cryptography.SHA512  
		///
		///  <see cref="T:System.Security.Cryptography.SHA512Managed" /></param>
		/// <returns>A new instance of the specified hash algorithm, or <see langword="null" /> if <paramref name="hashName" /> is not a valid hash algorithm.</returns>
		// Token: 0x06002E03 RID: 11779 RVA: 0x000A59F7 File Offset: 0x000A3BF7
		public static HashAlgorithm Create(string hashName)
		{
			return (HashAlgorithm)CryptoConfigForwarder.CreateFromName(hashName);
		}

		/// <summary>Gets the size, in bits, of the computed hash code.</summary>
		/// <returns>The size, in bits, of the computed hash code.</returns>
		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06002E04 RID: 11780 RVA: 0x000A5A04 File Offset: 0x000A3C04
		public virtual int HashSize
		{
			get
			{
				return this.HashSizeValue;
			}
		}

		/// <summary>Gets the value of the computed hash code.</summary>
		/// <returns>The current value of the computed hash code.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">
		///   <see cref="F:System.Security.Cryptography.HashAlgorithm.HashValue" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06002E05 RID: 11781 RVA: 0x000A5A0C File Offset: 0x000A3C0C
		public virtual byte[] Hash
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException(null);
				}
				if (this.State != 0)
				{
					throw new CryptographicUnexpectedOperationException("Hash must be finalized before the hash value is retrieved.");
				}
				byte[] hashValue = this.HashValue;
				return (byte[])((hashValue != null) ? hashValue.Clone() : null);
			}
		}

		/// <summary>Computes the hash value for the specified byte array.</summary>
		/// <param name="buffer">The input to compute the hash code for.</param>
		/// <returns>The computed hash code.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x06002E06 RID: 11782 RVA: 0x000A5A47 File Offset: 0x000A3C47
		public byte[] ComputeHash(byte[] buffer)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(null);
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.HashCore(buffer, 0, buffer.Length);
			return this.CaptureHashCodeAndReinitialize();
		}

		// Token: 0x06002E07 RID: 11783 RVA: 0x000A5A78 File Offset: 0x000A3C78
		public bool TryComputeHash(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(null);
			}
			if (destination.Length < this.HashSizeValue / 8)
			{
				bytesWritten = 0;
				return false;
			}
			this.HashCore(source);
			if (!this.TryHashFinal(destination, out bytesWritten))
			{
				throw new InvalidOperationException("The algorithm's implementation is incorrect.");
			}
			this.HashValue = null;
			this.Initialize();
			return true;
		}

		/// <summary>Computes the hash value for the specified region of the specified byte array.</summary>
		/// <param name="buffer">The input to compute the hash code for.</param>
		/// <param name="offset">The offset into the byte array from which to begin using data.</param>
		/// <param name="count">The number of bytes in the array to use as data.</param>
		/// <returns>The computed hash code.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="count" /> is an invalid value.  
		/// -or-  
		/// <paramref name="buffer" /> length is invalid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is out of range. This parameter requires a non-negative number.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x06002E08 RID: 11784 RVA: 0x000A5AD4 File Offset: 0x000A3CD4
		public byte[] ComputeHash(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (count < 0 || count > buffer.Length)
			{
				throw new ArgumentException("Argument {0} should be larger than {1}.");
			}
			if (buffer.Length - count < offset)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (this._disposed)
			{
				throw new ObjectDisposedException(null);
			}
			this.HashCore(buffer, offset, count);
			return this.CaptureHashCodeAndReinitialize();
		}

		/// <summary>Computes the hash value for the specified <see cref="T:System.IO.Stream" /> object.</summary>
		/// <param name="inputStream">The input to compute the hash code for.</param>
		/// <returns>The computed hash code.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x06002E09 RID: 11785 RVA: 0x000A5B4C File Offset: 0x000A3D4C
		public byte[] ComputeHash(Stream inputStream)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(null);
			}
			byte[] array = ArrayPool<byte>.Shared.Rent(4096);
			byte[] result;
			try
			{
				int cbSize;
				while ((cbSize = inputStream.Read(array, 0, array.Length)) > 0)
				{
					this.HashCore(array, 0, cbSize);
				}
				result = this.CaptureHashCodeAndReinitialize();
			}
			finally
			{
				CryptographicOperations.ZeroMemory(array);
				ArrayPool<byte>.Shared.Return(array, false);
			}
			return result;
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x000A5BC8 File Offset: 0x000A3DC8
		private byte[] CaptureHashCodeAndReinitialize()
		{
			this.HashValue = this.HashFinal();
			byte[] result = (byte[])this.HashValue.Clone();
			this.Initialize();
			return result;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Security.Cryptography.HashAlgorithm" /> class.</summary>
		// Token: 0x06002E0B RID: 11787 RVA: 0x000A5BEC File Offset: 0x000A3DEC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Cryptography.HashAlgorithm" /> class.</summary>
		// Token: 0x06002E0C RID: 11788 RVA: 0x000A5BFB File Offset: 0x000A3DFB
		public void Clear()
		{
			((IDisposable)this).Dispose();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.HashAlgorithm" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002E0D RID: 11789 RVA: 0x000A5C03 File Offset: 0x000A3E03
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._disposed = true;
			}
		}

		/// <summary>When overridden in a derived class, gets the input block size.</summary>
		/// <returns>The input block size.</returns>
		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06002E0E RID: 11790 RVA: 0x000040F7 File Offset: 0x000022F7
		public virtual int InputBlockSize
		{
			get
			{
				return 1;
			}
		}

		/// <summary>When overridden in a derived class, gets the output block size.</summary>
		/// <returns>The output block size.</returns>
		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06002E0F RID: 11791 RVA: 0x000040F7 File Offset: 0x000022F7
		public virtual int OutputBlockSize
		{
			get
			{
				return 1;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether multiple blocks can be transformed.</summary>
		/// <returns>
		///   <see langword="true" /> if multiple blocks can be transformed; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06002E10 RID: 11792 RVA: 0x000040F7 File Offset: 0x000022F7
		public virtual bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the current transform can be reused.</summary>
		/// <returns>Always <see langword="true" />.</returns>
		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06002E11 RID: 11793 RVA: 0x000040F7 File Offset: 0x000022F7
		public virtual bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		/// <summary>Computes the hash value for the specified region of the input byte array and copies the specified region of the input byte array to the specified region of the output byte array.</summary>
		/// <param name="inputBuffer">The input to compute the hash code for.</param>
		/// <param name="inputOffset">The offset into the input byte array from which to begin using data.</param>
		/// <param name="inputCount">The number of bytes in the input byte array to use as data.</param>
		/// <param name="outputBuffer">A copy of the part of the input array used to compute the hash code.</param>
		/// <param name="outputOffset">The offset into the output byte array from which to begin writing data.</param>
		/// <returns>The number of bytes written.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="inputCount" /> uses an invalid value.  
		/// -or-  
		/// <paramref name="inputBuffer" /> has an invalid length.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inputBuffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="inputOffset" /> is out of range. This parameter requires a non-negative number.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x06002E12 RID: 11794 RVA: 0x000A5C0F File Offset: 0x000A3E0F
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			this.ValidateTransformBlock(inputBuffer, inputOffset, inputCount);
			this.State = 1;
			this.HashCore(inputBuffer, inputOffset, inputCount);
			if (outputBuffer != null && (inputBuffer != outputBuffer || inputOffset != outputOffset))
			{
				Buffer.BlockCopy(inputBuffer, inputOffset, outputBuffer, outputOffset, inputCount);
			}
			return inputCount;
		}

		/// <summary>Computes the hash value for the specified region of the specified byte array.</summary>
		/// <param name="inputBuffer">The input to compute the hash code for.</param>
		/// <param name="inputOffset">The offset into the byte array from which to begin using data.</param>
		/// <param name="inputCount">The number of bytes in the byte array to use as data.</param>
		/// <returns>An array that is a copy of the part of the input that is hashed.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="inputCount" /> uses an invalid value.  
		/// -or-  
		/// <paramref name="inputBuffer" /> has an invalid offset length.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inputBuffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="inputOffset" /> is out of range. This parameter requires a non-negative number.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x06002E13 RID: 11795 RVA: 0x000A5C48 File Offset: 0x000A3E48
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			this.ValidateTransformBlock(inputBuffer, inputOffset, inputCount);
			this.HashCore(inputBuffer, inputOffset, inputCount);
			this.HashValue = this.CaptureHashCodeAndReinitialize();
			byte[] array;
			if (inputCount != 0)
			{
				array = new byte[inputCount];
				Buffer.BlockCopy(inputBuffer, inputOffset, array, 0, inputCount);
			}
			else
			{
				array = Array.Empty<byte>();
			}
			this.State = 0;
			return array;
		}

		// Token: 0x06002E14 RID: 11796 RVA: 0x000A5C98 File Offset: 0x000A3E98
		private void ValidateTransformBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (inputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("inputOffset", "Non-negative number required.");
			}
			if (inputCount < 0 || inputCount > inputBuffer.Length)
			{
				throw new ArgumentException("Argument {0} should be larger than {1}.");
			}
			if (inputBuffer.Length - inputCount < inputOffset)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (this._disposed)
			{
				throw new ObjectDisposedException(null);
			}
		}

		/// <summary>When overridden in a derived class, routes data written to the object into the hash algorithm for computing the hash.</summary>
		/// <param name="array">The input to compute the hash code for.</param>
		/// <param name="ibStart">The offset into the byte array from which to begin using data.</param>
		/// <param name="cbSize">The number of bytes in the byte array to use as data.</param>
		// Token: 0x06002E15 RID: 11797
		protected abstract void HashCore(byte[] array, int ibStart, int cbSize);

		/// <summary>When overridden in a derived class, finalizes the hash computation after the last data is processed by the cryptographic stream object.</summary>
		/// <returns>The computed hash code.</returns>
		// Token: 0x06002E16 RID: 11798
		protected abstract byte[] HashFinal();

		/// <summary>Initializes an implementation of the <see cref="T:System.Security.Cryptography.HashAlgorithm" /> class.</summary>
		// Token: 0x06002E17 RID: 11799
		public abstract void Initialize();

		// Token: 0x06002E18 RID: 11800 RVA: 0x000A5D00 File Offset: 0x000A3F00
		protected virtual void HashCore(ReadOnlySpan<byte> source)
		{
			byte[] array = ArrayPool<byte>.Shared.Rent(source.Length);
			try
			{
				source.CopyTo(array);
				this.HashCore(array, 0, source.Length);
			}
			finally
			{
				Array.Clear(array, 0, source.Length);
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x000A5D68 File Offset: 0x000A3F68
		protected virtual bool TryHashFinal(Span<byte> destination, out int bytesWritten)
		{
			int num = this.HashSizeValue / 8;
			if (destination.Length < num)
			{
				bytesWritten = 0;
				return false;
			}
			byte[] array = this.HashFinal();
			if (array.Length == num)
			{
				new ReadOnlySpan<byte>(array).CopyTo(destination);
				bytesWritten = array.Length;
				return true;
			}
			throw new InvalidOperationException("The algorithm's implementation is incorrect.");
		}

		// Token: 0x04002114 RID: 8468
		private bool _disposed;

		/// <summary>Represents the size, in bits, of the computed hash code.</summary>
		// Token: 0x04002115 RID: 8469
		protected int HashSizeValue;

		/// <summary>Represents the value of the computed hash code.</summary>
		// Token: 0x04002116 RID: 8470
		protected internal byte[] HashValue;

		/// <summary>Represents the state of the hash computation.</summary>
		// Token: 0x04002117 RID: 8471
		protected int State;
	}
}
