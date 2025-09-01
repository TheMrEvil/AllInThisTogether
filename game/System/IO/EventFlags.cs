using System;

namespace System.IO
{
	// Token: 0x0200051A RID: 1306
	[Flags]
	internal enum EventFlags : ushort
	{
		// Token: 0x0400167C RID: 5756
		Add = 1,
		// Token: 0x0400167D RID: 5757
		Delete = 2,
		// Token: 0x0400167E RID: 5758
		Enable = 4,
		// Token: 0x0400167F RID: 5759
		Disable = 8,
		// Token: 0x04001680 RID: 5760
		OneShot = 16,
		// Token: 0x04001681 RID: 5761
		Clear = 32,
		// Token: 0x04001682 RID: 5762
		Receipt = 64,
		// Token: 0x04001683 RID: 5763
		Dispatch = 128,
		// Token: 0x04001684 RID: 5764
		Flag0 = 4096,
		// Token: 0x04001685 RID: 5765
		Flag1 = 8192,
		// Token: 0x04001686 RID: 5766
		SystemFlags = 61440,
		// Token: 0x04001687 RID: 5767
		EOF = 32768,
		// Token: 0x04001688 RID: 5768
		Error = 16384
	}
}
