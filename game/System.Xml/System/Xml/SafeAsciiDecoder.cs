using System;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000230 RID: 560
	internal class SafeAsciiDecoder : Decoder
	{
		// Token: 0x0600151F RID: 5407 RVA: 0x000833F5 File Offset: 0x000815F5
		public SafeAsciiDecoder()
		{
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x0000D1C8 File Offset: 0x0000B3C8
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return count;
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x00083400 File Offset: 0x00081600
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			int i = byteIndex;
			int num = charIndex;
			while (i < byteIndex + byteCount)
			{
				chars[num++] = (char)bytes[i++];
			}
			return byteCount;
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x0008342C File Offset: 0x0008162C
		public override void Convert(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
		{
			if (charCount < byteCount)
			{
				byteCount = charCount;
				completed = false;
			}
			else
			{
				completed = true;
			}
			int i = byteIndex;
			int num = charIndex;
			int num2 = byteIndex + byteCount;
			while (i < num2)
			{
				chars[num++] = (char)bytes[i++];
			}
			charsUsed = byteCount;
			bytesUsed = byteCount;
		}
	}
}
