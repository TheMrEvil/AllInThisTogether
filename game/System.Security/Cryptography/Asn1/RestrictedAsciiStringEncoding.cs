using System;
using System.Collections.Generic;

namespace System.Security.Cryptography.Asn1
{
	// Token: 0x020000FE RID: 254
	internal abstract class RestrictedAsciiStringEncoding : SpanBasedEncoding
	{
		// Token: 0x060005E2 RID: 1506 RVA: 0x00017944 File Offset: 0x00015B44
		protected RestrictedAsciiStringEncoding(byte minCharAllowed, byte maxCharAllowed)
		{
			bool[] array = new bool[128];
			for (byte b = minCharAllowed; b <= maxCharAllowed; b += 1)
			{
				array[(int)b] = true;
			}
			this._isAllowed = array;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001797C File Offset: 0x00015B7C
		protected RestrictedAsciiStringEncoding(IEnumerable<char> allowedChars)
		{
			bool[] array = new bool[127];
			foreach (char c in allowedChars)
			{
				if ((int)c >= array.Length)
				{
					throw new ArgumentOutOfRangeException("allowedChars");
				}
				array[(int)c] = true;
			}
			this._isAllowed = array;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x000179E8 File Offset: 0x00015BE8
		public override int GetMaxByteCount(int charCount)
		{
			return charCount;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x000179E8 File Offset: 0x00015BE8
		public override int GetMaxCharCount(int byteCount)
		{
			return byteCount;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x000179EC File Offset: 0x00015BEC
		protected unsafe override int GetBytes(ReadOnlySpan<char> chars, Span<byte> bytes, bool write)
		{
			if (chars.IsEmpty)
			{
				return 0;
			}
			for (int i = 0; i < chars.Length; i++)
			{
				char c = (char)(*chars[i]);
				if ((int)c >= this._isAllowed.Length || !this._isAllowed[(int)c])
				{
					base.EncoderFallback.CreateFallbackBuffer().Fallback(c, i);
					throw new CryptographicException();
				}
				if (write)
				{
					*bytes[i] = (byte)c;
				}
			}
			return chars.Length;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00017A64 File Offset: 0x00015C64
		protected unsafe override int GetChars(ReadOnlySpan<byte> bytes, Span<char> chars, bool write)
		{
			if (bytes.IsEmpty)
			{
				return 0;
			}
			for (int i = 0; i < bytes.Length; i++)
			{
				byte b = *bytes[i];
				if ((int)b >= this._isAllowed.Length || !this._isAllowed[(int)b])
				{
					base.DecoderFallback.CreateFallbackBuffer().Fallback(new byte[]
					{
						b
					}, i);
					throw new CryptographicException();
				}
				if (write)
				{
					*chars[i] = (char)b;
				}
			}
			return bytes.Length;
		}

		// Token: 0x0400040C RID: 1036
		private readonly bool[] _isAllowed;
	}
}
