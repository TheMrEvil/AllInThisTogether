using System;

namespace System.Drawing
{
	// Token: 0x020000A9 RID: 169
	internal struct CGRect32
	{
		// Token: 0x06000A5A RID: 2650 RVA: 0x00017A30 File Offset: 0x00015C30
		public CGRect32(float x, float y, float width, float height)
		{
			this.origin.x = x;
			this.origin.y = y;
			this.size.width = width;
			this.size.height = height;
		}

		// Token: 0x04000639 RID: 1593
		public CGPoint32 origin;

		// Token: 0x0400063A RID: 1594
		public CGSize32 size;
	}
}
