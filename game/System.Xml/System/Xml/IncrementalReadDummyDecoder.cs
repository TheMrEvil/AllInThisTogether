using System;

namespace System.Xml
{
	// Token: 0x02000043 RID: 67
	internal class IncrementalReadDummyDecoder : IncrementalReadDecoder
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000D1C2 File Offset: 0x0000B3C2
		internal override int DecodedCount
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		internal override bool IsFull
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000B528 File Offset: 0x00009728
		internal override void SetNextOutputBuffer(Array array, int offset, int len)
		{
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000D1C8 File Offset: 0x0000B3C8
		internal override int Decode(char[] chars, int startPos, int len)
		{
			return len;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000D1C8 File Offset: 0x0000B3C8
		internal override int Decode(string str, int startPos, int len)
		{
			return len;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000B528 File Offset: 0x00009728
		internal override void Reset()
		{
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00002DA9 File Offset: 0x00000FA9
		public IncrementalReadDummyDecoder()
		{
		}
	}
}
