using System;

namespace System.Xml
{
	// Token: 0x02000044 RID: 68
	internal class IncrementalReadCharsDecoder : IncrementalReadDecoder
	{
		// Token: 0x0600021E RID: 542 RVA: 0x00002DA9 File Offset: 0x00000FA9
		internal IncrementalReadCharsDecoder()
		{
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000D1CB File Offset: 0x0000B3CB
		internal override int DecodedCount
		{
			get
			{
				return this.curIndex - this.startIndex;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000D1DA File Offset: 0x0000B3DA
		internal override bool IsFull
		{
			get
			{
				return this.curIndex == this.endIndex;
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000D1EC File Offset: 0x0000B3EC
		internal override int Decode(char[] chars, int startPos, int len)
		{
			int num = this.endIndex - this.curIndex;
			if (num > len)
			{
				num = len;
			}
			Buffer.BlockCopy(chars, startPos * 2, this.buffer, this.curIndex * 2, num * 2);
			this.curIndex += num;
			return num;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000D238 File Offset: 0x0000B438
		internal override int Decode(string str, int startPos, int len)
		{
			int num = this.endIndex - this.curIndex;
			if (num > len)
			{
				num = len;
			}
			str.CopyTo(startPos, this.buffer, this.curIndex, num);
			this.curIndex += num;
			return num;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000B528 File Offset: 0x00009728
		internal override void Reset()
		{
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000D27C File Offset: 0x0000B47C
		internal override void SetNextOutputBuffer(Array buffer, int index, int count)
		{
			this.buffer = (char[])buffer;
			this.startIndex = index;
			this.curIndex = index;
			this.endIndex = index + count;
		}

		// Token: 0x04000608 RID: 1544
		private char[] buffer;

		// Token: 0x04000609 RID: 1545
		private int startIndex;

		// Token: 0x0400060A RID: 1546
		private int curIndex;

		// Token: 0x0400060B RID: 1547
		private int endIndex;
	}
}
