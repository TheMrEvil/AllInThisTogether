using System;

namespace System.Resources
{
	// Token: 0x0200087A RID: 2170
	internal class ICONDIRENTRY
	{
		// Token: 0x06004846 RID: 18502 RVA: 0x000EDD8C File Offset: 0x000EBF8C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"ICONDIRENTRY (",
				this.bWidth.ToString(),
				"x",
				this.bHeight.ToString(),
				" ",
				this.wBitCount.ToString(),
				" bpp)"
			});
		}

		// Token: 0x06004847 RID: 18503 RVA: 0x0000259F File Offset: 0x0000079F
		public ICONDIRENTRY()
		{
		}

		// Token: 0x04002E3F RID: 11839
		public byte bWidth;

		// Token: 0x04002E40 RID: 11840
		public byte bHeight;

		// Token: 0x04002E41 RID: 11841
		public byte bColorCount;

		// Token: 0x04002E42 RID: 11842
		public byte bReserved;

		// Token: 0x04002E43 RID: 11843
		public short wPlanes;

		// Token: 0x04002E44 RID: 11844
		public short wBitCount;

		// Token: 0x04002E45 RID: 11845
		public int dwBytesInRes;

		// Token: 0x04002E46 RID: 11846
		public int dwImageOffset;

		// Token: 0x04002E47 RID: 11847
		public byte[] image;
	}
}
