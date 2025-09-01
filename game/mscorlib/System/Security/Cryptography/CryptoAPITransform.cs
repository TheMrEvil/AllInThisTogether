using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	/// <summary>Performs a cryptographic transformation of data. This class cannot be inherited.</summary>
	// Token: 0x020004C5 RID: 1221
	[ComVisible(true)]
	public sealed class CryptoAPITransform : ICryptoTransform, IDisposable
	{
		// Token: 0x060030CD RID: 12493 RVA: 0x000B19F7 File Offset: 0x000AFBF7
		internal CryptoAPITransform()
		{
			this.m_disposed = false;
		}

		/// <summary>Gets a value indicating whether the current transform can be reused.</summary>
		/// <returns>Always <see langword="true" />.</returns>
		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x060030CE RID: 12494 RVA: 0x000040F7 File Offset: 0x000022F7
		public bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether multiple blocks can be transformed.</summary>
		/// <returns>
		///   <see langword="true" /> if multiple blocks can be transformed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x060030CF RID: 12495 RVA: 0x000040F7 File Offset: 0x000022F7
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the input block size.</summary>
		/// <returns>The input block size in bytes.</returns>
		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x060030D0 RID: 12496 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public int InputBlockSize
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the key handle.</summary>
		/// <returns>The key handle.</returns>
		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x060030D1 RID: 12497 RVA: 0x000B1A06 File Offset: 0x000AFC06
		public IntPtr KeyHandle
		{
			[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
			get
			{
				return IntPtr.Zero;
			}
		}

		/// <summary>Gets the output block size.</summary>
		/// <returns>The output block size in bytes.</returns>
		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x060030D2 RID: 12498 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public int OutputBlockSize
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Security.Cryptography.CryptoAPITransform" /> class.</summary>
		// Token: 0x060030D3 RID: 12499 RVA: 0x000B1A0D File Offset: 0x000AFC0D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Cryptography.CryptoAPITransform" /> method.</summary>
		// Token: 0x060030D4 RID: 12500 RVA: 0x000B1A1C File Offset: 0x000AFC1C
		public void Clear()
		{
			this.Dispose(false);
		}

		// Token: 0x060030D5 RID: 12501 RVA: 0x000B1A25 File Offset: 0x000AFC25
		private void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				this.m_disposed = true;
			}
		}

		/// <summary>Computes the transformation for the specified region of the input byte array and copies the resulting transformation to the specified region of the output byte array.</summary>
		/// <param name="inputBuffer">The input on which to perform the operation on.</param>
		/// <param name="inputOffset">The offset into the input byte array from which to begin using data from.</param>
		/// <param name="inputCount">The number of bytes in the input byte array to use as data.</param>
		/// <param name="outputBuffer">The output to which to write the data to.</param>
		/// <param name="outputOffset">The offset into the output byte array from which to begin writing data from.</param>
		/// <returns>The number of bytes written.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputBuffer" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="outputBuffer" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The length of the input buffer is less than the sum of the input offset and the input count.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="inputOffset" /> is out of range. This parameter requires a non-negative number.</exception>
		// Token: 0x060030D6 RID: 12502 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		[SecuritySafeCritical]
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			return 0;
		}

		/// <summary>Computes the transformation for the specified region of the specified byte array.</summary>
		/// <param name="inputBuffer">The input on which to perform the operation on.</param>
		/// <param name="inputOffset">The offset into the byte array from which to begin using data from.</param>
		/// <param name="inputCount">The number of bytes in the byte array to use as data.</param>
		/// <returns>The computed transformation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputBuffer" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="inputOffset" /> parameter is less than zero.  
		///  -or-  
		///  The <paramref name="inputCount" /> parameter is less than zero.  
		///  -or-  
		///  The length of the input buffer is less than the sum of the input offset and the input count.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="F:System.Security.Cryptography.PaddingMode.PKCS7" /> padding is invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="inputOffset" /> parameter is out of range. This parameter requires a non-negative number.</exception>
		// Token: 0x060030D7 RID: 12503 RVA: 0x0000AF5E File Offset: 0x0000915E
		[SecuritySafeCritical]
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			return null;
		}

		/// <summary>Resets the internal state of <see cref="T:System.Security.Cryptography.CryptoAPITransform" /> so that it can be used again to do a different encryption or decryption.</summary>
		// Token: 0x060030D8 RID: 12504 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[ComVisible(false)]
		public void Reset()
		{
		}

		// Token: 0x04002243 RID: 8771
		private bool m_disposed;
	}
}
