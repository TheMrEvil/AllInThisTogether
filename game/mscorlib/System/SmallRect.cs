using System;

namespace System
{
	// Token: 0x02000270 RID: 624
	internal struct SmallRect
	{
		// Token: 0x06001C2B RID: 7211 RVA: 0x000694FE File Offset: 0x000676FE
		public SmallRect(int left, int top, int right, int bottom)
		{
			this.Left = (short)left;
			this.Top = (short)top;
			this.Right = (short)right;
			this.Bottom = (short)bottom;
		}

		// Token: 0x040019CF RID: 6607
		public short Left;

		// Token: 0x040019D0 RID: 6608
		public short Top;

		// Token: 0x040019D1 RID: 6609
		public short Right;

		// Token: 0x040019D2 RID: 6610
		public short Bottom;
	}
}
