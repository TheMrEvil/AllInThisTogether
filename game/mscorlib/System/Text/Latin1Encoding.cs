using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
	// Token: 0x020003A9 RID: 937
	[Serializable]
	internal class Latin1Encoding : EncodingNLS, ISerializable
	{
		// Token: 0x06002669 RID: 9833 RVA: 0x000882CC File Offset: 0x000864CC
		public Latin1Encoding() : base(28591)
		{
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x000882D9 File Offset: 0x000864D9
		internal Latin1Encoding(SerializationInfo info, StreamingContext context) : base(28591)
		{
			base.DeserializeEncoding(info, context);
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x000882EE File Offset: 0x000864EE
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.SerializeEncoding(info, context);
			info.AddValue("CodePageEncoding+maxCharSize", 1);
			info.AddValue("CodePageEncoding+m_codePage", this.CodePage);
			info.AddValue("CodePageEncoding+dataItem", null);
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x00088324 File Offset: 0x00086524
		internal unsafe override int GetByteCount(char* chars, int charCount, EncoderNLS encoder)
		{
			char c = '\0';
			EncoderReplacementFallback encoderReplacementFallback;
			if (encoder != null)
			{
				c = encoder._charLeftOver;
				encoderReplacementFallback = (encoder.Fallback as EncoderReplacementFallback);
			}
			else
			{
				encoderReplacementFallback = (base.EncoderFallback as EncoderReplacementFallback);
			}
			if (encoderReplacementFallback != null && encoderReplacementFallback.MaxCharCount == 1)
			{
				if (c > '\0')
				{
					charCount++;
				}
				return charCount;
			}
			int num = 0;
			char* ptr = chars + charCount;
			EncoderFallbackBuffer encoderFallbackBuffer = null;
			if (c > '\0')
			{
				encoderFallbackBuffer = encoder.FallbackBuffer;
				encoderFallbackBuffer.InternalInitialize(chars, ptr, encoder, false);
				char* ptr2 = chars;
				encoderFallbackBuffer.InternalFallback(c, ref ptr2);
				chars = ptr2;
			}
			char c2;
			while ((c2 = ((encoderFallbackBuffer == null) ? '\0' : encoderFallbackBuffer.InternalGetNextChar())) != '\0' || chars < ptr)
			{
				if (c2 == '\0')
				{
					c2 = *chars;
					chars++;
				}
				if (c2 > 'ÿ')
				{
					if (encoderFallbackBuffer == null)
					{
						if (encoder == null)
						{
							encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
						}
						else
						{
							encoderFallbackBuffer = encoder.FallbackBuffer;
						}
						encoderFallbackBuffer.InternalInitialize(ptr - charCount, ptr, encoder, false);
					}
					char* ptr2 = chars;
					encoderFallbackBuffer.InternalFallback(c2, ref ptr2);
					chars = ptr2;
				}
				else
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x00088420 File Offset: 0x00086620
		internal unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS encoder)
		{
			char c = '\0';
			EncoderReplacementFallback encoderReplacementFallback;
			if (encoder != null)
			{
				c = encoder._charLeftOver;
				encoderReplacementFallback = (encoder.Fallback as EncoderReplacementFallback);
			}
			else
			{
				encoderReplacementFallback = (base.EncoderFallback as EncoderReplacementFallback);
			}
			char* ptr = chars + charCount;
			byte* ptr2 = bytes;
			char* ptr3 = chars;
			if (encoderReplacementFallback != null && encoderReplacementFallback.MaxCharCount == 1)
			{
				char c2 = encoderReplacementFallback.DefaultString[0];
				if (c2 <= 'ÿ')
				{
					if (c > '\0')
					{
						if (byteCount == 0)
						{
							base.ThrowBytesOverflow(encoder, true);
						}
						*(bytes++) = (byte)c2;
						byteCount--;
					}
					if (byteCount < charCount)
					{
						base.ThrowBytesOverflow(encoder, byteCount < 1);
						ptr = chars + byteCount;
					}
					while (chars < ptr)
					{
						char c3 = *(chars++);
						if (c3 > 'ÿ')
						{
							*(bytes++) = (byte)c2;
						}
						else
						{
							*(bytes++) = (byte)c3;
						}
					}
					if (encoder != null)
					{
						encoder._charLeftOver = '\0';
						encoder._charsUsed = (int)((long)(chars - ptr3));
					}
					return (int)((long)(bytes - ptr2));
				}
			}
			byte* ptr4 = bytes + byteCount;
			EncoderFallbackBuffer encoderFallbackBuffer = null;
			if (c > '\0')
			{
				encoderFallbackBuffer = encoder.FallbackBuffer;
				encoderFallbackBuffer.InternalInitialize(chars, ptr, encoder, true);
				char* ptr5 = chars;
				encoderFallbackBuffer.InternalFallback(c, ref ptr5);
				chars = ptr5;
				if ((long)encoderFallbackBuffer.Remaining > (long)(ptr4 - bytes))
				{
					base.ThrowBytesOverflow(encoder, true);
				}
			}
			char c4;
			while ((c4 = ((encoderFallbackBuffer == null) ? '\0' : encoderFallbackBuffer.InternalGetNextChar())) != '\0' || chars < ptr)
			{
				if (c4 == '\0')
				{
					c4 = *chars;
					chars++;
				}
				if (c4 > 'ÿ')
				{
					if (encoderFallbackBuffer == null)
					{
						if (encoder == null)
						{
							encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
						}
						else
						{
							encoderFallbackBuffer = encoder.FallbackBuffer;
						}
						encoderFallbackBuffer.InternalInitialize(ptr - charCount, ptr, encoder, true);
					}
					char* ptr5 = chars;
					encoderFallbackBuffer.InternalFallback(c4, ref ptr5);
					chars = ptr5;
					if ((long)encoderFallbackBuffer.Remaining > (long)(ptr4 - bytes))
					{
						chars--;
						encoderFallbackBuffer.InternalReset();
						base.ThrowBytesOverflow(encoder, chars == ptr3);
						break;
					}
				}
				else
				{
					if (bytes >= ptr4)
					{
						if (encoderFallbackBuffer == null || !encoderFallbackBuffer.bFallingBack)
						{
							chars--;
						}
						base.ThrowBytesOverflow(encoder, chars == ptr3);
						break;
					}
					*bytes = (byte)c4;
					bytes++;
				}
			}
			if (encoder != null)
			{
				if (encoderFallbackBuffer != null && !encoderFallbackBuffer.bUsedEncoder)
				{
					encoder._charLeftOver = '\0';
				}
				encoder._charsUsed = (int)((long)(chars - ptr3));
			}
			return (int)((long)(bytes - ptr2));
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x00088677 File Offset: 0x00086877
		internal unsafe override int GetCharCount(byte* bytes, int count, DecoderNLS decoder)
		{
			return count;
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x0008867C File Offset: 0x0008687C
		internal unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS decoder)
		{
			if (charCount < byteCount)
			{
				base.ThrowCharsOverflow(decoder, charCount < 1);
				byteCount = charCount;
			}
			byte* ptr = bytes + byteCount;
			while (bytes < ptr)
			{
				*chars = (char)(*bytes);
				chars++;
				bytes++;
			}
			if (decoder != null)
			{
				decoder._bytesUsed = byteCount;
			}
			return byteCount;
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x000886C4 File Offset: 0x000868C4
		public override int GetMaxByteCount(int charCount)
		{
			if (charCount < 0)
			{
				throw new ArgumentOutOfRangeException("charCount", "Non-negative number required.");
			}
			long num = (long)charCount + 1L;
			if (base.EncoderFallback.MaxCharCount > 1)
			{
				num *= (long)base.EncoderFallback.MaxCharCount;
			}
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("charCount", "Too many characters. The resulting number of bytes is larger than what can be returned as an int.");
			}
			return (int)num;
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x00088724 File Offset: 0x00086924
		public override int GetMaxCharCount(int byteCount)
		{
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount", "Non-negative number required.");
			}
			long num = (long)byteCount;
			if (base.DecoderFallback.MaxCharCount > 1)
			{
				num *= (long)base.DecoderFallback.MaxCharCount;
			}
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("byteCount", "Too many bytes. The resulting number of chars is larger than what can be returned as an int.");
			}
			return (int)num;
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06002672 RID: 9842 RVA: 0x000040F7 File Offset: 0x000022F7
		public override bool IsSingleByte
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x00088780 File Offset: 0x00086980
		public override bool IsAlwaysNormalized(NormalizationForm form)
		{
			return form == NormalizationForm.FormC;
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x00088786 File Offset: 0x00086986
		internal override char[] GetBestFitUnicodeToBytesData()
		{
			return Latin1Encoding.arrayCharBestFit;
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x0008878D File Offset: 0x0008698D
		// Note: this type is marked as 'beforefieldinit'.
		static Latin1Encoding()
		{
		}

		// Token: 0x04001DC9 RID: 7625
		internal static readonly Latin1Encoding s_default = new Latin1Encoding();

		// Token: 0x04001DCA RID: 7626
		private static readonly char[] arrayCharBestFit = new char[]
		{
			'Ā',
			'A',
			'ā',
			'a',
			'Ă',
			'A',
			'ă',
			'a',
			'Ą',
			'A',
			'ą',
			'a',
			'Ć',
			'C',
			'ć',
			'c',
			'Ĉ',
			'C',
			'ĉ',
			'c',
			'Ċ',
			'C',
			'ċ',
			'c',
			'Č',
			'C',
			'č',
			'c',
			'Ď',
			'D',
			'ď',
			'd',
			'Đ',
			'D',
			'đ',
			'd',
			'Ē',
			'E',
			'ē',
			'e',
			'Ĕ',
			'E',
			'ĕ',
			'e',
			'Ė',
			'E',
			'ė',
			'e',
			'Ę',
			'E',
			'ę',
			'e',
			'Ě',
			'E',
			'ě',
			'e',
			'Ĝ',
			'G',
			'ĝ',
			'g',
			'Ğ',
			'G',
			'ğ',
			'g',
			'Ġ',
			'G',
			'ġ',
			'g',
			'Ģ',
			'G',
			'ģ',
			'g',
			'Ĥ',
			'H',
			'ĥ',
			'h',
			'Ħ',
			'H',
			'ħ',
			'h',
			'Ĩ',
			'I',
			'ĩ',
			'i',
			'Ī',
			'I',
			'ī',
			'i',
			'Ĭ',
			'I',
			'ĭ',
			'i',
			'Į',
			'I',
			'į',
			'i',
			'İ',
			'I',
			'ı',
			'i',
			'Ĵ',
			'J',
			'ĵ',
			'j',
			'Ķ',
			'K',
			'ķ',
			'k',
			'Ĺ',
			'L',
			'ĺ',
			'l',
			'Ļ',
			'L',
			'ļ',
			'l',
			'Ľ',
			'L',
			'ľ',
			'l',
			'Ł',
			'L',
			'ł',
			'l',
			'Ń',
			'N',
			'ń',
			'n',
			'Ņ',
			'N',
			'ņ',
			'n',
			'Ň',
			'N',
			'ň',
			'n',
			'Ō',
			'O',
			'ō',
			'o',
			'Ŏ',
			'O',
			'ŏ',
			'o',
			'Ő',
			'O',
			'ő',
			'o',
			'Œ',
			'O',
			'œ',
			'o',
			'Ŕ',
			'R',
			'ŕ',
			'r',
			'Ŗ',
			'R',
			'ŗ',
			'r',
			'Ř',
			'R',
			'ř',
			'r',
			'Ś',
			'S',
			'ś',
			's',
			'Ŝ',
			'S',
			'ŝ',
			's',
			'Ş',
			'S',
			'ş',
			's',
			'Š',
			'S',
			'š',
			's',
			'Ţ',
			'T',
			'ţ',
			't',
			'Ť',
			'T',
			'ť',
			't',
			'Ŧ',
			'T',
			'ŧ',
			't',
			'Ũ',
			'U',
			'ũ',
			'u',
			'Ū',
			'U',
			'ū',
			'u',
			'Ŭ',
			'U',
			'ŭ',
			'u',
			'Ů',
			'U',
			'ů',
			'u',
			'Ű',
			'U',
			'ű',
			'u',
			'Ų',
			'U',
			'ų',
			'u',
			'Ŵ',
			'W',
			'ŵ',
			'w',
			'Ŷ',
			'Y',
			'ŷ',
			'y',
			'Ÿ',
			'Y',
			'Ź',
			'Z',
			'ź',
			'z',
			'Ż',
			'Z',
			'ż',
			'z',
			'Ž',
			'Z',
			'ž',
			'z',
			'ƀ',
			'b',
			'Ɖ',
			'D',
			'Ƒ',
			'F',
			'ƒ',
			'f',
			'Ɨ',
			'I',
			'ƚ',
			'l',
			'Ɵ',
			'O',
			'Ơ',
			'O',
			'ơ',
			'o',
			'ƫ',
			't',
			'Ʈ',
			'T',
			'Ư',
			'U',
			'ư',
			'u',
			'ƶ',
			'z',
			'Ǎ',
			'A',
			'ǎ',
			'a',
			'Ǐ',
			'I',
			'ǐ',
			'i',
			'Ǒ',
			'O',
			'ǒ',
			'o',
			'Ǔ',
			'U',
			'ǔ',
			'u',
			'Ǖ',
			'U',
			'ǖ',
			'u',
			'Ǘ',
			'U',
			'ǘ',
			'u',
			'Ǚ',
			'U',
			'ǚ',
			'u',
			'Ǜ',
			'U',
			'ǜ',
			'u',
			'Ǟ',
			'A',
			'ǟ',
			'a',
			'Ǥ',
			'G',
			'ǥ',
			'g',
			'Ǧ',
			'G',
			'ǧ',
			'g',
			'Ǩ',
			'K',
			'ǩ',
			'k',
			'Ǫ',
			'O',
			'ǫ',
			'o',
			'Ǭ',
			'O',
			'ǭ',
			'o',
			'ǰ',
			'j',
			'ɡ',
			'g',
			'ʹ',
			'\'',
			'ʺ',
			'"',
			'ʼ',
			'\'',
			'˄',
			'^',
			'ˆ',
			'^',
			'ˈ',
			'\'',
			'ˉ',
			'?',
			'ˊ',
			'?',
			'ˋ',
			'`',
			'ˍ',
			'_',
			'˚',
			'?',
			'˜',
			'~',
			'̀',
			'`',
			'̂',
			'^',
			'̃',
			'~',
			'̎',
			'"',
			'̱',
			'_',
			'̲',
			'_',
			'\u2000',
			' ',
			'\u2001',
			' ',
			'\u2002',
			' ',
			'\u2003',
			' ',
			'\u2004',
			' ',
			'\u2005',
			' ',
			'\u2006',
			' ',
			'‐',
			'-',
			'‑',
			'-',
			'–',
			'-',
			'—',
			'-',
			'‘',
			'\'',
			'’',
			'\'',
			'‚',
			',',
			'“',
			'"',
			'”',
			'"',
			'„',
			'"',
			'†',
			'?',
			'‡',
			'?',
			'•',
			'.',
			'…',
			'.',
			'‰',
			'?',
			'′',
			'\'',
			'‵',
			'`',
			'‹',
			'<',
			'›',
			'>',
			'™',
			'T',
			'！',
			'!',
			'＂',
			'"',
			'＃',
			'#',
			'＄',
			'$',
			'％',
			'%',
			'＆',
			'&',
			'＇',
			'\'',
			'（',
			'(',
			'）',
			')',
			'＊',
			'*',
			'＋',
			'+',
			'，',
			',',
			'－',
			'-',
			'．',
			'.',
			'／',
			'/',
			'０',
			'0',
			'１',
			'1',
			'２',
			'2',
			'３',
			'3',
			'４',
			'4',
			'５',
			'5',
			'６',
			'6',
			'７',
			'7',
			'８',
			'8',
			'９',
			'9',
			'：',
			':',
			'；',
			';',
			'＜',
			'<',
			'＝',
			'=',
			'＞',
			'>',
			'？',
			'?',
			'＠',
			'@',
			'Ａ',
			'A',
			'Ｂ',
			'B',
			'Ｃ',
			'C',
			'Ｄ',
			'D',
			'Ｅ',
			'E',
			'Ｆ',
			'F',
			'Ｇ',
			'G',
			'Ｈ',
			'H',
			'Ｉ',
			'I',
			'Ｊ',
			'J',
			'Ｋ',
			'K',
			'Ｌ',
			'L',
			'Ｍ',
			'M',
			'Ｎ',
			'N',
			'Ｏ',
			'O',
			'Ｐ',
			'P',
			'Ｑ',
			'Q',
			'Ｒ',
			'R',
			'Ｓ',
			'S',
			'Ｔ',
			'T',
			'Ｕ',
			'U',
			'Ｖ',
			'V',
			'Ｗ',
			'W',
			'Ｘ',
			'X',
			'Ｙ',
			'Y',
			'Ｚ',
			'Z',
			'［',
			'[',
			'＼',
			'\\',
			'］',
			']',
			'＾',
			'^',
			'＿',
			'_',
			'｀',
			'`',
			'ａ',
			'a',
			'ｂ',
			'b',
			'ｃ',
			'c',
			'ｄ',
			'd',
			'ｅ',
			'e',
			'ｆ',
			'f',
			'ｇ',
			'g',
			'ｈ',
			'h',
			'ｉ',
			'i',
			'ｊ',
			'j',
			'ｋ',
			'k',
			'ｌ',
			'l',
			'ｍ',
			'm',
			'ｎ',
			'n',
			'ｏ',
			'o',
			'ｐ',
			'p',
			'ｑ',
			'q',
			'ｒ',
			'r',
			'ｓ',
			's',
			'ｔ',
			't',
			'ｕ',
			'u',
			'ｖ',
			'v',
			'ｗ',
			'w',
			'ｘ',
			'x',
			'ｙ',
			'y',
			'ｚ',
			'z',
			'｛',
			'{',
			'｜',
			'|',
			'｝',
			'}',
			'～',
			'~'
		};
	}
}
