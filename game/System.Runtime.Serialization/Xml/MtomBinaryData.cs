using System;

namespace System.Xml
{
	// Token: 0x0200008C RID: 140
	internal class MtomBinaryData
	{
		// Token: 0x0600074B RID: 1867 RVA: 0x0001F90B File Offset: 0x0001DB0B
		internal MtomBinaryData(IStreamProvider provider)
		{
			this.type = MtomBinaryDataType.Provider;
			this.provider = provider;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0001F921 File Offset: 0x0001DB21
		internal MtomBinaryData(byte[] buffer, int offset, int count)
		{
			this.type = MtomBinaryDataType.Segment;
			this.chunk = new byte[count];
			Buffer.BlockCopy(buffer, offset, this.chunk, 0, count);
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x0001F94B File Offset: 0x0001DB4B
		internal long Length
		{
			get
			{
				if (this.type == MtomBinaryDataType.Segment)
				{
					return (long)this.chunk.Length;
				}
				return -1L;
			}
		}

		// Token: 0x0400036B RID: 875
		internal MtomBinaryDataType type;

		// Token: 0x0400036C RID: 876
		internal IStreamProvider provider;

		// Token: 0x0400036D RID: 877
		internal byte[] chunk;
	}
}
