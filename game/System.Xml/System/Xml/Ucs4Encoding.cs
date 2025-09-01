using System;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000231 RID: 561
	internal class Ucs4Encoding : Encoding
	{
		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001523 RID: 5411 RVA: 0x00083472 File Offset: 0x00081672
		public override string WebName
		{
			get
			{
				return this.EncodingName;
			}
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x0008347A File Offset: 0x0008167A
		public override Decoder GetDecoder()
		{
			return this.ucs4Decoder;
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x00083482 File Offset: 0x00081682
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return checked(count * 4);
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x00083487 File Offset: 0x00081687
		public override int GetByteCount(char[] chars)
		{
			return chars.Length * 4;
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0001DA42 File Offset: 0x0001BC42
		public override byte[] GetBytes(string s)
		{
			return null;
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			return 0;
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override int GetMaxByteCount(int charCount)
		{
			return 0;
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x0008348E File Offset: 0x0008168E
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return this.ucs4Decoder.GetCharCount(bytes, index, count);
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0008349E File Offset: 0x0008169E
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			return this.ucs4Decoder.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x000834B2 File Offset: 0x000816B2
		public override int GetMaxCharCount(int byteCount)
		{
			return (byteCount + 3) / 4;
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x0600152D RID: 5421 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override int CodePage
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x000834B9 File Offset: 0x000816B9
		public override int GetCharCount(byte[] bytes)
		{
			return bytes.Length / 4;
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x0001DA42 File Offset: 0x0001BC42
		public override Encoder GetEncoder()
		{
			return null;
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x000834C0 File Offset: 0x000816C0
		internal static Encoding UCS4_Littleendian
		{
			get
			{
				return new Ucs4Encoding4321();
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001531 RID: 5425 RVA: 0x000834C7 File Offset: 0x000816C7
		internal static Encoding UCS4_Bigendian
		{
			get
			{
				return new Ucs4Encoding1234();
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x000834CE File Offset: 0x000816CE
		internal static Encoding UCS4_2143
		{
			get
			{
				return new Ucs4Encoding2143();
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x000834D5 File Offset: 0x000816D5
		internal static Encoding UCS4_3412
		{
			get
			{
				return new Ucs4Encoding3412();
			}
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x000834DC File Offset: 0x000816DC
		public Ucs4Encoding()
		{
		}

		// Token: 0x040012D2 RID: 4818
		internal Ucs4Decoder ucs4Decoder;
	}
}
