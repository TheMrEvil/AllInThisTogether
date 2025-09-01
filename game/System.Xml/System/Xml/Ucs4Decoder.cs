using System;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000236 RID: 566
	internal abstract class Ucs4Decoder : Decoder
	{
		// Token: 0x06001541 RID: 5441 RVA: 0x000835AC File Offset: 0x000817AC
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return (count + this.lastBytesCount) / 4;
		}

		// Token: 0x06001542 RID: 5442
		internal abstract int GetFullChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex);

		// Token: 0x06001543 RID: 5443 RVA: 0x000835B8 File Offset: 0x000817B8
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			int num = this.lastBytesCount;
			if (this.lastBytesCount > 0)
			{
				while (this.lastBytesCount < 4 && byteCount > 0)
				{
					this.lastBytes[this.lastBytesCount] = bytes[byteIndex];
					byteIndex++;
					byteCount--;
					this.lastBytesCount++;
				}
				if (this.lastBytesCount < 4)
				{
					return 0;
				}
				num = this.GetFullChars(this.lastBytes, 0, 4, chars, charIndex);
				charIndex += num;
				this.lastBytesCount = 0;
			}
			else
			{
				num = 0;
			}
			num = this.GetFullChars(bytes, byteIndex, byteCount, chars, charIndex) + num;
			int num2 = byteCount & 3;
			if (num2 >= 0)
			{
				for (int i = 0; i < num2; i++)
				{
					this.lastBytes[i] = bytes[byteIndex + byteCount - num2 + i];
				}
				this.lastBytesCount = num2;
			}
			return num;
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x00083678 File Offset: 0x00081878
		public override void Convert(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
		{
			bytesUsed = 0;
			charsUsed = 0;
			int num = this.lastBytesCount;
			int num2;
			if (num > 0)
			{
				while (num < 4 && byteCount > 0)
				{
					this.lastBytes[num] = bytes[byteIndex];
					byteIndex++;
					byteCount--;
					bytesUsed++;
					num++;
				}
				if (num < 4)
				{
					this.lastBytesCount = num;
					completed = true;
					return;
				}
				num2 = this.GetFullChars(this.lastBytes, 0, 4, chars, charIndex);
				charIndex += num2;
				charCount -= num2;
				charsUsed = num2;
				this.lastBytesCount = 0;
				if (charCount == 0)
				{
					completed = (byteCount == 0);
					return;
				}
			}
			else
			{
				num2 = 0;
			}
			if (charCount * 4 < byteCount)
			{
				byteCount = charCount * 4;
				completed = false;
			}
			else
			{
				completed = true;
			}
			bytesUsed += byteCount;
			charsUsed = this.GetFullChars(bytes, byteIndex, byteCount, chars, charIndex) + num2;
			int num3 = byteCount & 3;
			if (num3 >= 0)
			{
				for (int i = 0; i < num3; i++)
				{
					this.lastBytes[i] = bytes[byteIndex + byteCount - num3 + i];
				}
				this.lastBytesCount = num3;
			}
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x0008376A File Offset: 0x0008196A
		internal void Ucs4ToUTF16(uint code, char[] chars, int charIndex)
		{
			chars[charIndex] = (char)(55296 + (ushort)((code >> 16) - 1U) + (ushort)(code >> 10 & 63U));
			chars[charIndex + 1] = (char)(56320 + (ushort)(code & 1023U));
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x0008379A File Offset: 0x0008199A
		protected Ucs4Decoder()
		{
		}

		// Token: 0x040012D3 RID: 4819
		internal byte[] lastBytes = new byte[4];

		// Token: 0x040012D4 RID: 4820
		internal int lastBytesCount;
	}
}
