using System;
using System.Text;

namespace System.Security.Cryptography.Asn1
{
	// Token: 0x020000FA RID: 250
	internal abstract class SpanBasedEncoding : Encoding
	{
		// Token: 0x060005D1 RID: 1489 RVA: 0x00017830 File Offset: 0x00015A30
		protected SpanBasedEncoding() : base(0, EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback)
		{
		}

		// Token: 0x060005D2 RID: 1490
		protected abstract int GetBytes(ReadOnlySpan<char> chars, Span<byte> bytes, bool write);

		// Token: 0x060005D3 RID: 1491
		protected abstract int GetChars(ReadOnlySpan<byte> bytes, Span<char> chars, bool write);

		// Token: 0x060005D4 RID: 1492 RVA: 0x00017843 File Offset: 0x00015A43
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return this.GetByteCount(new ReadOnlySpan<char>(chars, index, count));
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00017853 File Offset: 0x00015A53
		public unsafe override int GetByteCount(char* chars, int count)
		{
			return this.GetByteCount(new ReadOnlySpan<char>((void*)chars, count));
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00017862 File Offset: 0x00015A62
		public override int GetByteCount(string s)
		{
			return this.GetByteCount(s.AsSpan());
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00017870 File Offset: 0x00015A70
		public new int GetByteCount(ReadOnlySpan<char> chars)
		{
			return this.GetBytes(chars, Span<byte>.Empty, false);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001787F File Offset: 0x00015A7F
		public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			return this.GetBytes(new ReadOnlySpan<char>(chars, charIndex, charCount), new Span<byte>(bytes, byteIndex, bytes.Length - byteIndex), true);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x000178A0 File Offset: 0x00015AA0
		public unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
		{
			return this.GetBytes(new ReadOnlySpan<char>((void*)chars, charCount), new Span<byte>((void*)bytes, byteCount), true);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x000178B8 File Offset: 0x00015AB8
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return this.GetCharCount(new ReadOnlySpan<byte>(bytes, index, count));
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000178C8 File Offset: 0x00015AC8
		public unsafe override int GetCharCount(byte* bytes, int count)
		{
			return this.GetCharCount(new ReadOnlySpan<byte>((void*)bytes, count));
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x000178D7 File Offset: 0x00015AD7
		public new int GetCharCount(ReadOnlySpan<byte> bytes)
		{
			return this.GetChars(bytes, Span<char>.Empty, false);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x000178E6 File Offset: 0x00015AE6
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			return this.GetChars(new ReadOnlySpan<byte>(bytes, byteIndex, byteCount), new Span<char>(chars, charIndex, chars.Length - charIndex), true);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00017907 File Offset: 0x00015B07
		public unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount)
		{
			return this.GetChars(new ReadOnlySpan<byte>((void*)bytes, byteCount), new Span<char>((void*)chars, charCount), true);
		}
	}
}
