using System;

namespace System.Drawing.Internal
{
	// Token: 0x020000F5 RID: 245
	internal struct GPRECTF
	{
		// Token: 0x06000C07 RID: 3079 RVA: 0x0001BBB1 File Offset: 0x00019DB1
		internal GPRECTF(float x, float y, float width, float height)
		{
			this.X = x;
			this.Y = y;
			this.Width = width;
			this.Height = height;
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0001BBD0 File Offset: 0x00019DD0
		internal GPRECTF(RectangleF rect)
		{
			this.X = rect.X;
			this.Y = rect.Y;
			this.Width = rect.Width;
			this.Height = rect.Height;
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x0001BC06 File Offset: 0x00019E06
		internal SizeF SizeF
		{
			get
			{
				return new SizeF(this.Width, this.Height);
			}
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0001BC19 File Offset: 0x00019E19
		internal RectangleF ToRectangleF()
		{
			return new RectangleF(this.X, this.Y, this.Width, this.Height);
		}

		// Token: 0x04000836 RID: 2102
		internal float X;

		// Token: 0x04000837 RID: 2103
		internal float Y;

		// Token: 0x04000838 RID: 2104
		internal float Width;

		// Token: 0x04000839 RID: 2105
		internal float Height;
	}
}
