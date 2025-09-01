using System;

namespace MagicaCloth2
{
	// Token: 0x020000E3 RID: 227
	public struct DataChunk
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x000207E3 File Offset: 0x0001E9E3
		public bool IsValid
		{
			get
			{
				return this.dataLength > 0;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x000207F0 File Offset: 0x0001E9F0
		public static DataChunk Empty
		{
			get
			{
				return default(DataChunk);
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00020806 File Offset: 0x0001EA06
		public DataChunk(int sindex, int length)
		{
			this.startIndex = sindex;
			this.dataLength = length;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00020816 File Offset: 0x0001EA16
		public void Clear()
		{
			this.startIndex = 0;
			this.dataLength = 0;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00020826 File Offset: 0x0001EA26
		public override string ToString()
		{
			return string.Format("[startIndex={0}, dataLength={1}]", this.startIndex, this.dataLength);
		}

		// Token: 0x04000634 RID: 1588
		public int startIndex;

		// Token: 0x04000635 RID: 1589
		public int dataLength;
	}
}
