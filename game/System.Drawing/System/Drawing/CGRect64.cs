using System;

namespace System.Drawing
{
	// Token: 0x020000AC RID: 172
	internal struct CGRect64
	{
		// Token: 0x06000A5B RID: 2651 RVA: 0x00017A63 File Offset: 0x00015C63
		public CGRect64(double x, double y, double width, double height)
		{
			this.origin.x = x;
			this.origin.y = y;
			this.size.width = width;
			this.size.height = height;
		}

		// Token: 0x0400063F RID: 1599
		public CGPoint64 origin;

		// Token: 0x04000640 RID: 1600
		public CGSize64 size;
	}
}
