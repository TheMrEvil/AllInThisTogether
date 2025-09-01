using System;

namespace System
{
	// Token: 0x0200026D RID: 621
	internal struct InputRecord
	{
		// Token: 0x040019C2 RID: 6594
		public short EventType;

		// Token: 0x040019C3 RID: 6595
		public bool KeyDown;

		// Token: 0x040019C4 RID: 6596
		public short RepeatCount;

		// Token: 0x040019C5 RID: 6597
		public short VirtualKeyCode;

		// Token: 0x040019C6 RID: 6598
		public short VirtualScanCode;

		// Token: 0x040019C7 RID: 6599
		public char Character;

		// Token: 0x040019C8 RID: 6600
		public int ControlKeyState;

		// Token: 0x040019C9 RID: 6601
		private int pad1;

		// Token: 0x040019CA RID: 6602
		private bool pad2;
	}
}
