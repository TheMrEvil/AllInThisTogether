using System;

namespace System
{
	// Token: 0x02000271 RID: 625
	internal struct ConsoleScreenBufferInfo
	{
		// Token: 0x040019D3 RID: 6611
		public Coord Size;

		// Token: 0x040019D4 RID: 6612
		public Coord CursorPosition;

		// Token: 0x040019D5 RID: 6613
		public short Attribute;

		// Token: 0x040019D6 RID: 6614
		public SmallRect Window;

		// Token: 0x040019D7 RID: 6615
		public Coord MaxWindowSize;
	}
}
