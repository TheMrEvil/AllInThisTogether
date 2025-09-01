using System;

namespace System
{
	// Token: 0x0200026F RID: 623
	internal struct Coord
	{
		// Token: 0x06001C2A RID: 7210 RVA: 0x000694EC File Offset: 0x000676EC
		public Coord(int x, int y)
		{
			this.X = (short)x;
			this.Y = (short)y;
		}

		// Token: 0x040019CD RID: 6605
		public short X;

		// Token: 0x040019CE RID: 6606
		public short Y;
	}
}
