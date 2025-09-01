using System;
using System.Globalization;

namespace System.Text
{
	/// <summary>Throws <see cref="T:System.Text.DecoderFallbackException" /> when an encoded input byte sequence cannot be converted to a decoded output character. This class cannot be inherited.</summary>
	// Token: 0x02000394 RID: 916
	public sealed class DecoderExceptionFallbackBuffer : DecoderFallbackBuffer
	{
		/// <summary>Throws <see cref="T:System.Text.DecoderFallbackException" /> when the input byte sequence cannot be decoded. The nominal return value is not used.</summary>
		/// <param name="bytesUnknown">An input array of bytes.</param>
		/// <param name="index">The index position of a byte in the input.</param>
		/// <returns>None. No value is returned because the <see cref="M:System.Text.DecoderExceptionFallbackBuffer.Fallback(System.Byte[],System.Int32)" /> method always throws an exception.  
		///  The nominal return value is <see langword="true" />. A return value is defined, although it is unchanging, because this method implements an abstract method.</returns>
		/// <exception cref="T:System.Text.DecoderFallbackException">This method always throws an exception that reports the value and index position of the input byte that cannot be decoded.</exception>
		// Token: 0x060025AC RID: 9644 RVA: 0x00085F12 File Offset: 0x00084112
		public override bool Fallback(byte[] bytesUnknown, int index)
		{
			this.Throw(bytesUnknown, index);
			return true;
		}

		/// <summary>Retrieves the next character in the exception data buffer.</summary>
		/// <returns>The return value is always the Unicode character NULL (U+0000).  
		///  A return value is defined, although it is unchanging, because this method implements an abstract method.</returns>
		// Token: 0x060025AD RID: 9645 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public override char GetNextChar()
		{
			return '\0';
		}

		/// <summary>Causes the next call to <see cref="M:System.Text.DecoderExceptionFallbackBuffer.GetNextChar" /> to access the exception data buffer character position that is prior to the current position.</summary>
		/// <returns>The return value is always <see langword="false" />.  
		///  A return value is defined, although it is unchanging, because this method implements an abstract method.</returns>
		// Token: 0x060025AE RID: 9646 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public override bool MovePrevious()
		{
			return false;
		}

		/// <summary>Gets the number of characters in the current <see cref="T:System.Text.DecoderExceptionFallbackBuffer" /> object that remain to be processed.</summary>
		/// <returns>The return value is always zero.  
		///  A return value is defined, although it is unchanging, because this method implements an abstract method.</returns>
		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060025AF RID: 9647 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public override int Remaining
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060025B0 RID: 9648 RVA: 0x00085F20 File Offset: 0x00084120
		private void Throw(byte[] bytesUnknown, int index)
		{
			StringBuilder stringBuilder = new StringBuilder(bytesUnknown.Length * 3);
			int num = 0;
			while (num < bytesUnknown.Length && num < 20)
			{
				stringBuilder.Append('[');
				stringBuilder.Append(bytesUnknown[num].ToString("X2", CultureInfo.InvariantCulture));
				stringBuilder.Append(']');
				num++;
			}
			if (num == 20)
			{
				stringBuilder.Append(" ...");
			}
			throw new DecoderFallbackException(SR.Format("Unable to translate bytes {0} at index {1} from specified code page to Unicode.", stringBuilder, index), bytesUnknown, index);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderExceptionFallbackBuffer" /> class.</summary>
		// Token: 0x060025B1 RID: 9649 RVA: 0x00085FA3 File Offset: 0x000841A3
		public DecoderExceptionFallbackBuffer()
		{
		}
	}
}
