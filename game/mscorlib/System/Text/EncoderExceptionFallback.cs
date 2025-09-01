using System;

namespace System.Text
{
	/// <summary>Provides a failure-handling mechanism, called a fallback, for an input character that cannot be converted to an output byte sequence. The fallback throws an exception if an input character cannot be converted to an output byte sequence. This class cannot be inherited.</summary>
	// Token: 0x0200039E RID: 926
	[Serializable]
	public sealed class EncoderExceptionFallback : EncoderFallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderExceptionFallback" /> class.</summary>
		// Token: 0x06002604 RID: 9732 RVA: 0x00086FFF File Offset: 0x000851FF
		public EncoderExceptionFallback()
		{
		}

		/// <summary>Returns an encoder fallback buffer that throws an exception if it cannot convert a character sequence to a byte sequence.</summary>
		/// <returns>An encoder fallback buffer that throws an exception when it cannot encode a character sequence.</returns>
		// Token: 0x06002605 RID: 9733 RVA: 0x00087007 File Offset: 0x00085207
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			return new EncoderExceptionFallbackBuffer();
		}

		/// <summary>Gets the maximum number of characters this instance can return.</summary>
		/// <returns>The return value is always zero.</returns>
		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06002606 RID: 9734 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public override int MaxCharCount
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Indicates whether the current <see cref="T:System.Text.EncoderExceptionFallback" /> object and a specified object are equal.</summary>
		/// <param name="value">An object that derives from the <see cref="T:System.Text.EncoderExceptionFallback" /> class.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is not <see langword="null" /> (<see langword="Nothing" /> in Visual Basic .NET) and is a <see cref="T:System.Text.EncoderExceptionFallback" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002607 RID: 9735 RVA: 0x0008700E File Offset: 0x0008520E
		public override bool Equals(object value)
		{
			return value is EncoderExceptionFallback;
		}

		/// <summary>Retrieves the hash code for this instance.</summary>
		/// <returns>The return value is always the same arbitrary value, and has no special significance.</returns>
		// Token: 0x06002608 RID: 9736 RVA: 0x0008701B File Offset: 0x0008521B
		public override int GetHashCode()
		{
			return 654;
		}
	}
}
