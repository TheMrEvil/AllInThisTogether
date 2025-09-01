using System;

namespace System.Text
{
	/// <summary>Provides a failure-handling mechanism, called a fallback, for an encoded input byte sequence that cannot be converted to an input character. The fallback throws an exception instead of decoding the input byte sequence. This class cannot be inherited.</summary>
	// Token: 0x02000393 RID: 915
	[Serializable]
	public sealed class DecoderExceptionFallback : DecoderFallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.DecoderExceptionFallback" /> class.</summary>
		// Token: 0x060025A7 RID: 9639 RVA: 0x00085EEF File Offset: 0x000840EF
		public DecoderExceptionFallback()
		{
		}

		/// <summary>Returns a decoder fallback buffer that throws an exception if it cannot convert a sequence of bytes to a character.</summary>
		/// <returns>A decoder fallback buffer that throws an exception when it cannot decode a byte sequence.</returns>
		// Token: 0x060025A8 RID: 9640 RVA: 0x00085EF7 File Offset: 0x000840F7
		public override DecoderFallbackBuffer CreateFallbackBuffer()
		{
			return new DecoderExceptionFallbackBuffer();
		}

		/// <summary>Gets the maximum number of characters this instance can return.</summary>
		/// <returns>The return value is always zero.</returns>
		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060025A9 RID: 9641 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public override int MaxCharCount
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Indicates whether the current <see cref="T:System.Text.DecoderExceptionFallback" /> object and a specified object are equal.</summary>
		/// <param name="value">An object that derives from the <see cref="T:System.Text.DecoderExceptionFallback" /> class.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is not <see langword="null" /> and is a <see cref="T:System.Text.DecoderExceptionFallback" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060025AA RID: 9642 RVA: 0x00085EFE File Offset: 0x000840FE
		public override bool Equals(object value)
		{
			return value is DecoderExceptionFallback;
		}

		/// <summary>Retrieves the hash code for this instance.</summary>
		/// <returns>The return value is always the same arbitrary value, and has no special significance.</returns>
		// Token: 0x060025AB RID: 9643 RVA: 0x00085F0B File Offset: 0x0008410B
		public override int GetHashCode()
		{
			return 879;
		}
	}
}
