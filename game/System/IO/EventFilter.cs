using System;

namespace System.IO
{
	// Token: 0x0200051B RID: 1307
	internal enum EventFilter : short
	{
		// Token: 0x0400168A RID: 5770
		Read = -1,
		// Token: 0x0400168B RID: 5771
		Write = -2,
		// Token: 0x0400168C RID: 5772
		Aio = -3,
		// Token: 0x0400168D RID: 5773
		Vnode = -4,
		// Token: 0x0400168E RID: 5774
		Proc = -5,
		// Token: 0x0400168F RID: 5775
		Signal = -6,
		// Token: 0x04001690 RID: 5776
		Timer = -7,
		// Token: 0x04001691 RID: 5777
		MachPort = -8,
		// Token: 0x04001692 RID: 5778
		FS = -9,
		// Token: 0x04001693 RID: 5779
		User = -10,
		// Token: 0x04001694 RID: 5780
		VM = -11
	}
}
