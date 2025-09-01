using System;

namespace System.Xml
{
	// Token: 0x02000042 RID: 66
	internal abstract class IncrementalReadDecoder
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000210 RID: 528
		internal abstract int DecodedCount { get; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000211 RID: 529
		internal abstract bool IsFull { get; }

		// Token: 0x06000212 RID: 530
		internal abstract void SetNextOutputBuffer(Array array, int offset, int len);

		// Token: 0x06000213 RID: 531
		internal abstract int Decode(char[] chars, int startPos, int len);

		// Token: 0x06000214 RID: 532
		internal abstract int Decode(string str, int startPos, int len);

		// Token: 0x06000215 RID: 533
		internal abstract void Reset();

		// Token: 0x06000216 RID: 534 RVA: 0x0000216B File Offset: 0x0000036B
		protected IncrementalReadDecoder()
		{
		}
	}
}
