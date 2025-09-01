using System;

namespace System.IO.Compression
{
	// Token: 0x02000012 RID: 18
	internal sealed class CopyEncoder
	{
		// Token: 0x0600005A RID: 90 RVA: 0x0000349C File Offset: 0x0000169C
		public void GetBlock(DeflateInput input, OutputBuffer output, bool isFinal)
		{
			int num = 0;
			if (input != null)
			{
				num = Math.Min(input.Count, output.FreeBytes - 5 - output.BitsInBuffer);
				if (num > 65531)
				{
					num = 65531;
				}
			}
			if (isFinal)
			{
				output.WriteBits(3, 1U);
			}
			else
			{
				output.WriteBits(3, 0U);
			}
			output.FlushBits();
			this.WriteLenNLen((ushort)num, output);
			if (input != null && num > 0)
			{
				output.WriteBytes(input.Buffer, input.StartIndex, num);
				input.ConsumeBytes(num);
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000351C File Offset: 0x0000171C
		private void WriteLenNLen(ushort len, OutputBuffer output)
		{
			output.WriteUInt16(len);
			ushort value = ~len;
			output.WriteUInt16(value);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000353B File Offset: 0x0000173B
		public CopyEncoder()
		{
		}

		// Token: 0x040000B4 RID: 180
		private const int PaddingSize = 5;

		// Token: 0x040000B5 RID: 181
		private const int MaxUncompressedBlockSize = 65536;
	}
}
