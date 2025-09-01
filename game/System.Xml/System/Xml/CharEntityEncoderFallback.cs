using System;
using System.Text;

namespace System.Xml
{
	// Token: 0x0200002C RID: 44
	internal class CharEntityEncoderFallback : EncoderFallback
	{
		// Token: 0x06000168 RID: 360 RVA: 0x0000B2A0 File Offset: 0x000094A0
		internal CharEntityEncoderFallback()
		{
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000B2A8 File Offset: 0x000094A8
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			if (this.fallbackBuffer == null)
			{
				this.fallbackBuffer = new CharEntityEncoderFallbackBuffer(this);
			}
			return this.fallbackBuffer;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000B2C4 File Offset: 0x000094C4
		public override int MaxCharCount
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000B2C8 File Offset: 0x000094C8
		// (set) Token: 0x0600016C RID: 364 RVA: 0x0000B2D0 File Offset: 0x000094D0
		internal int StartOffset
		{
			get
			{
				return this.startOffset;
			}
			set
			{
				this.startOffset = value;
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000B2D9 File Offset: 0x000094D9
		internal void Reset(int[] textContentMarks, int endMarkPos)
		{
			this.textContentMarks = textContentMarks;
			this.endMarkPos = endMarkPos;
			this.curMarkPos = 0;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000B2F0 File Offset: 0x000094F0
		internal bool CanReplaceAt(int index)
		{
			int num = this.curMarkPos;
			int num2 = this.startOffset + index;
			while (num < this.endMarkPos && num2 >= this.textContentMarks[num + 1])
			{
				num++;
			}
			this.curMarkPos = num;
			return (num & 1) != 0;
		}

		// Token: 0x040005D7 RID: 1495
		private CharEntityEncoderFallbackBuffer fallbackBuffer;

		// Token: 0x040005D8 RID: 1496
		private int[] textContentMarks;

		// Token: 0x040005D9 RID: 1497
		private int endMarkPos;

		// Token: 0x040005DA RID: 1498
		private int curMarkPos;

		// Token: 0x040005DB RID: 1499
		private int startOffset;
	}
}
