using System;

namespace System.Drawing.Internal
{
	// Token: 0x020000F4 RID: 244
	internal struct GPRECT
	{
		// Token: 0x06000C05 RID: 3077 RVA: 0x0001BB5C File Offset: 0x00019D5C
		internal GPRECT(int x, int y, int width, int height)
		{
			this.X = x;
			this.Y = y;
			this.Width = width;
			this.Height = height;
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0001BB7B File Offset: 0x00019D7B
		internal GPRECT(Rectangle rect)
		{
			this.X = rect.X;
			this.Y = rect.Y;
			this.Width = rect.Width;
			this.Height = rect.Height;
		}

		// Token: 0x04000832 RID: 2098
		internal int X;

		// Token: 0x04000833 RID: 2099
		internal int Y;

		// Token: 0x04000834 RID: 2100
		internal int Width;

		// Token: 0x04000835 RID: 2101
		internal int Height;
	}
}
