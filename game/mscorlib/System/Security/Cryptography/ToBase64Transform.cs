using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Cryptography
{
	/// <summary>Converts a <see cref="T:System.Security.Cryptography.CryptoStream" /> to base 64.</summary>
	// Token: 0x0200047E RID: 1150
	[ComVisible(true)]
	public class ToBase64Transform : ICryptoTransform, IDisposable
	{
		/// <summary>Gets the input block size.</summary>
		/// <returns>The size of the input data blocks in bytes.</returns>
		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06002E76 RID: 11894 RVA: 0x000221D2 File Offset: 0x000203D2
		public int InputBlockSize
		{
			get
			{
				return 3;
			}
		}

		/// <summary>Gets the output block size.</summary>
		/// <returns>The size of the output data blocks in bytes.</returns>
		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06002E77 RID: 11895 RVA: 0x00022807 File Offset: 0x00020A07
		public int OutputBlockSize
		{
			get
			{
				return 4;
			}
		}

		/// <summary>Gets a value that indicates whether multiple blocks can be transformed.</summary>
		/// <returns>Always <see langword="false" />.</returns>
		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06002E78 RID: 11896 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the current transform can be reused.</summary>
		/// <returns>Always <see langword="true" />.</returns>
		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06002E79 RID: 11897 RVA: 0x000040F7 File Offset: 0x000022F7
		public virtual bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		/// <summary>Converts the specified region of the input byte array to base 64 and copies the result to the specified region of the output byte array.</summary>
		/// <param name="inputBuffer">The input to compute to base 64.</param>
		/// <param name="inputOffset">The offset into the input byte array from which to begin using data.</param>
		/// <param name="inputCount">The number of bytes in the input byte array to use as data.</param>
		/// <param name="outputBuffer">The output to which to write the result.</param>
		/// <param name="outputOffset">The offset into the output byte array from which to begin writing data.</param>
		/// <returns>The number of bytes written.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current <see cref="T:System.Security.Cryptography.ToBase64Transform" /> object has already been disposed.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The data size is not valid.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="inputBuffer" /> parameter contains an invalid offset length.  
		///  -or-  
		///  The <paramref name="inputCount" /> parameter contains an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputBuffer" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="inputBuffer" /> parameter requires a non-negative number.</exception>
		// Token: 0x06002E7A RID: 11898 RVA: 0x000A6274 File Offset: 0x000A4474
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (inputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("inputOffset", Environment.GetResourceString("Non-negative number required."));
			}
			if (inputCount < 0 || inputCount > inputBuffer.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Value was invalid."));
			}
			if (inputBuffer.Length - inputCount < inputOffset)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			char[] array = new char[4];
			Convert.ToBase64CharArray(inputBuffer, inputOffset, 3, array, 0);
			byte[] bytes = Encoding.ASCII.GetBytes(array);
			if (bytes.Length != 4)
			{
				throw new CryptographicException(Environment.GetResourceString("Length of the data to encrypt is invalid."));
			}
			Buffer.BlockCopy(bytes, 0, outputBuffer, outputOffset, bytes.Length);
			return bytes.Length;
		}

		/// <summary>Converts the specified region of the specified byte array to base 64.</summary>
		/// <param name="inputBuffer">The input to convert to base 64.</param>
		/// <param name="inputOffset">The offset into the byte array from which to begin using data.</param>
		/// <param name="inputCount">The number of bytes in the byte array to use as data.</param>
		/// <returns>The computed base 64 conversion.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current <see cref="T:System.Security.Cryptography.ToBase64Transform" /> object has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="inputBuffer" /> parameter contains an invalid offset length.  
		///  -or-  
		///  The <paramref name="inputCount" /> parameter contains an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputBuffer" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="inputBuffer" /> parameter requires a non-negative number.</exception>
		// Token: 0x06002E7B RID: 11899 RVA: 0x000A6320 File Offset: 0x000A4520
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (inputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("inputOffset", Environment.GetResourceString("Non-negative number required."));
			}
			if (inputCount < 0 || inputCount > inputBuffer.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Value was invalid."));
			}
			if (inputBuffer.Length - inputCount < inputOffset)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (inputCount == 0)
			{
				return EmptyArray<byte>.Value;
			}
			char[] array = new char[4];
			Convert.ToBase64CharArray(inputBuffer, inputOffset, inputCount, array, 0);
			return Encoding.ASCII.GetBytes(array);
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Security.Cryptography.ToBase64Transform" /> class.</summary>
		// Token: 0x06002E7C RID: 11900 RVA: 0x000A63AC File Offset: 0x000A45AC
		public void Dispose()
		{
			this.Clear();
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Cryptography.ToBase64Transform" />.</summary>
		// Token: 0x06002E7D RID: 11901 RVA: 0x000A63B4 File Offset: 0x000A45B4
		public void Clear()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.ToBase64Transform" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002E7E RID: 11902 RVA: 0x00004BF9 File Offset: 0x00002DF9
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.ToBase64Transform" />.</summary>
		// Token: 0x06002E7F RID: 11903 RVA: 0x000A63C4 File Offset: 0x000A45C4
		~ToBase64Transform()
		{
			this.Dispose(false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.ToBase64Transform" /> class.</summary>
		// Token: 0x06002E80 RID: 11904 RVA: 0x0000259F File Offset: 0x0000079F
		public ToBase64Transform()
		{
		}
	}
}
