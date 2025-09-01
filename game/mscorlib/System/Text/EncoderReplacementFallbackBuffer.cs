using System;

namespace System.Text
{
	/// <summary>Represents a substitute input string that is used when the original input character cannot be encoded. This class cannot be inherited.</summary>
	// Token: 0x020003A5 RID: 933
	public sealed class EncoderReplacementFallbackBuffer : EncoderFallbackBuffer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.EncoderReplacementFallbackBuffer" /> class using the value of a <see cref="T:System.Text.EncoderReplacementFallback" /> object.</summary>
		/// <param name="fallback">A <see cref="T:System.Text.EncoderReplacementFallback" /> object.</param>
		// Token: 0x06002641 RID: 9793 RVA: 0x000878FF File Offset: 0x00085AFF
		public EncoderReplacementFallbackBuffer(EncoderReplacementFallback fallback)
		{
			this._strDefault = fallback.DefaultString + fallback.DefaultString;
		}

		/// <summary>Prepares the replacement fallback buffer to use the current replacement string.</summary>
		/// <param name="charUnknown">An input character. This parameter is ignored in this operation unless an exception is thrown.</param>
		/// <param name="index">The index position of the character in the input buffer. This parameter is ignored in this operation.</param>
		/// <returns>
		///   <see langword="true" /> if the replacement string is not empty; <see langword="false" /> if the replacement string is empty.</returns>
		/// <exception cref="T:System.ArgumentException">This method is called again before the <see cref="M:System.Text.EncoderReplacementFallbackBuffer.GetNextChar" /> method has read all the characters in the replacement fallback buffer.</exception>
		// Token: 0x06002642 RID: 9794 RVA: 0x0008792C File Offset: 0x00085B2C
		public override bool Fallback(char charUnknown, int index)
		{
			if (this._fallbackCount >= 1)
			{
				if (char.IsHighSurrogate(charUnknown) && this._fallbackCount >= 0 && char.IsLowSurrogate(this._strDefault[this._fallbackIndex + 1]))
				{
					base.ThrowLastCharRecursive(char.ConvertToUtf32(charUnknown, this._strDefault[this._fallbackIndex + 1]));
				}
				base.ThrowLastCharRecursive((int)charUnknown);
			}
			this._fallbackCount = this._strDefault.Length / 2;
			this._fallbackIndex = -1;
			return this._fallbackCount != 0;
		}

		/// <summary>Indicates whether a replacement string can be used when an input surrogate pair cannot be encoded, or whether the surrogate pair can be ignored. Parameters specify the surrogate pair and the index position of the pair in the input.</summary>
		/// <param name="charUnknownHigh">The high surrogate of the input pair.</param>
		/// <param name="charUnknownLow">The low surrogate of the input pair.</param>
		/// <param name="index">The index position of the surrogate pair in the input buffer.</param>
		/// <returns>
		///   <see langword="true" /> if the replacement string is not empty; <see langword="false" /> if the replacement string is empty.</returns>
		/// <exception cref="T:System.ArgumentException">This method is called again before the <see cref="M:System.Text.EncoderReplacementFallbackBuffer.GetNextChar" /> method has read all the replacement string characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="charUnknownHigh" /> is less than U+D800 or greater than U+D8FF.  
		///  -or-  
		///  The value of <paramref name="charUnknownLow" /> is less than U+DC00 or greater than U+DFFF.</exception>
		// Token: 0x06002643 RID: 9795 RVA: 0x000879B8 File Offset: 0x00085BB8
		public override bool Fallback(char charUnknownHigh, char charUnknownLow, int index)
		{
			if (!char.IsHighSurrogate(charUnknownHigh))
			{
				throw new ArgumentOutOfRangeException("charUnknownHigh", SR.Format("Valid values are between {0} and {1}, inclusive.", 55296, 56319));
			}
			if (!char.IsLowSurrogate(charUnknownLow))
			{
				throw new ArgumentOutOfRangeException("charUnknownLow", SR.Format("Valid values are between {0} and {1}, inclusive.", 56320, 57343));
			}
			if (this._fallbackCount >= 1)
			{
				base.ThrowLastCharRecursive(char.ConvertToUtf32(charUnknownHigh, charUnknownLow));
			}
			this._fallbackCount = this._strDefault.Length;
			this._fallbackIndex = -1;
			return this._fallbackCount != 0;
		}

		/// <summary>Retrieves the next character in the replacement fallback buffer.</summary>
		/// <returns>The next Unicode character in the replacement fallback buffer that the application can encode.</returns>
		// Token: 0x06002644 RID: 9796 RVA: 0x00087A60 File Offset: 0x00085C60
		public override char GetNextChar()
		{
			this._fallbackCount--;
			this._fallbackIndex++;
			if (this._fallbackCount < 0)
			{
				return '\0';
			}
			if (this._fallbackCount == 2147483647)
			{
				this._fallbackCount = -1;
				return '\0';
			}
			return this._strDefault[this._fallbackIndex];
		}

		/// <summary>Causes the next call to the <see cref="M:System.Text.EncoderReplacementFallbackBuffer.GetNextChar" /> method to access the character position in the replacement fallback buffer prior to the current character position.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="M:System.Text.EncoderReplacementFallbackBuffer.MovePrevious" /> operation was successful; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002645 RID: 9797 RVA: 0x00087ABB File Offset: 0x00085CBB
		public override bool MovePrevious()
		{
			if (this._fallbackCount >= -1 && this._fallbackIndex >= 0)
			{
				this._fallbackIndex--;
				this._fallbackCount++;
				return true;
			}
			return false;
		}

		/// <summary>Gets the number of characters in the replacement fallback buffer that remain to be processed.</summary>
		/// <returns>The number of characters in the replacement fallback buffer that have not yet been processed.</returns>
		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06002646 RID: 9798 RVA: 0x00087AEE File Offset: 0x00085CEE
		public override int Remaining
		{
			get
			{
				if (this._fallbackCount >= 0)
				{
					return this._fallbackCount;
				}
				return 0;
			}
		}

		/// <summary>Initializes all internal state information and data in this instance of <see cref="T:System.Text.EncoderReplacementFallbackBuffer" />.</summary>
		// Token: 0x06002647 RID: 9799 RVA: 0x00087B01 File Offset: 0x00085D01
		public override void Reset()
		{
			this._fallbackCount = -1;
			this._fallbackIndex = 0;
			this.charStart = null;
			this.bFallingBack = false;
		}

		// Token: 0x04001DC1 RID: 7617
		private string _strDefault;

		// Token: 0x04001DC2 RID: 7618
		private int _fallbackCount = -1;

		// Token: 0x04001DC3 RID: 7619
		private int _fallbackIndex = -1;
	}
}
