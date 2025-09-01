using System;

namespace System.Security.Cryptography.Asn1
{
	// Token: 0x020000FF RID: 255
	internal class BMPEncoding : SpanBasedEncoding
	{
		// Token: 0x060005E8 RID: 1512 RVA: 0x00017AE4 File Offset: 0x00015CE4
		protected unsafe override int GetBytes(ReadOnlySpan<char> chars, Span<byte> bytes, bool write)
		{
			if (chars.IsEmpty)
			{
				return 0;
			}
			int num = 0;
			for (int i = 0; i < chars.Length; i++)
			{
				char c = (char)(*chars[i]);
				if (char.IsSurrogate(c))
				{
					base.EncoderFallback.CreateFallbackBuffer().Fallback(c, i);
					throw new CryptographicException();
				}
				ushort num2 = (ushort)c;
				if (write)
				{
					*bytes[num + 1] = (byte)num2;
					*bytes[num] = (byte)(num2 >> 8);
				}
				num += 2;
			}
			return num;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00017B60 File Offset: 0x00015D60
		protected unsafe override int GetChars(ReadOnlySpan<byte> bytes, Span<char> chars, bool write)
		{
			if (bytes.IsEmpty)
			{
				return 0;
			}
			if (bytes.Length % 2 != 0)
			{
				base.DecoderFallback.CreateFallbackBuffer().Fallback(bytes.Slice(bytes.Length - 1).ToArray(), bytes.Length - 1);
				throw new CryptographicException();
			}
			int num = 0;
			for (int i = 0; i < bytes.Length; i += 2)
			{
				char c = (char)((int)(*bytes[i]) << 8 | (int)(*bytes[i + 1]));
				if (char.IsSurrogate(c))
				{
					base.DecoderFallback.CreateFallbackBuffer().Fallback(bytes.Slice(i, 2).ToArray(), i);
					throw new CryptographicException();
				}
				if (write)
				{
					*chars[num] = c;
				}
				num++;
			}
			return num;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00017C2B File Offset: 0x00015E2B
		public override int GetMaxByteCount(int charCount)
		{
			return checked(charCount * 2);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00017C30 File Offset: 0x00015E30
		public override int GetMaxCharCount(int byteCount)
		{
			return byteCount / 2;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00017C35 File Offset: 0x00015E35
		public BMPEncoding()
		{
		}
	}
}
