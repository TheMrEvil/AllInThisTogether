using System;
using System.Text;

namespace System.Data.SqlClient
{
	// Token: 0x02000229 RID: 553
	internal sealed class SqlUnicodeEncoding : UnicodeEncoding
	{
		// Token: 0x06001AAD RID: 6829 RVA: 0x0007AFF4 File Offset: 0x000791F4
		private SqlUnicodeEncoding() : base(false, false, false)
		{
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x0007AFFF File Offset: 0x000791FF
		public override Decoder GetDecoder()
		{
			return new SqlUnicodeEncoding.SqlUnicodeDecoder();
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x0007B006 File Offset: 0x00079206
		public override int GetMaxByteCount(int charCount)
		{
			return charCount * 2;
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001AB0 RID: 6832 RVA: 0x0007B00B File Offset: 0x0007920B
		public static Encoding SqlUnicodeEncodingInstance
		{
			get
			{
				return SqlUnicodeEncoding.s_singletonEncoding;
			}
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x0007B012 File Offset: 0x00079212
		// Note: this type is marked as 'beforefieldinit'.
		static SqlUnicodeEncoding()
		{
		}

		// Token: 0x04001111 RID: 4369
		private static SqlUnicodeEncoding s_singletonEncoding = new SqlUnicodeEncoding();

		// Token: 0x0200022A RID: 554
		private sealed class SqlUnicodeDecoder : Decoder
		{
			// Token: 0x06001AB2 RID: 6834 RVA: 0x0007B01E File Offset: 0x0007921E
			public override int GetCharCount(byte[] bytes, int index, int count)
			{
				return count / 2;
			}

			// Token: 0x06001AB3 RID: 6835 RVA: 0x0007B024 File Offset: 0x00079224
			public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
			{
				int num;
				int result;
				bool flag;
				this.Convert(bytes, byteIndex, byteCount, chars, charIndex, chars.Length - charIndex, true, out num, out result, out flag);
				return result;
			}

			// Token: 0x06001AB4 RID: 6836 RVA: 0x0007B04D File Offset: 0x0007924D
			public override void Convert(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
			{
				charsUsed = Math.Min(charCount, byteCount / 2);
				bytesUsed = charsUsed * 2;
				completed = (bytesUsed == byteCount);
				Buffer.BlockCopy(bytes, byteIndex, chars, charIndex * 2, bytesUsed);
			}

			// Token: 0x06001AB5 RID: 6837 RVA: 0x0007B07D File Offset: 0x0007927D
			public SqlUnicodeDecoder()
			{
			}
		}
	}
}
