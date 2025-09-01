using System;

namespace System.Runtime
{
	// Token: 0x02000033 RID: 51
	internal enum TraceEventOpcode
	{
		// Token: 0x040000EF RID: 239
		Info,
		// Token: 0x040000F0 RID: 240
		Start,
		// Token: 0x040000F1 RID: 241
		Stop,
		// Token: 0x040000F2 RID: 242
		Reply = 6,
		// Token: 0x040000F3 RID: 243
		Resume,
		// Token: 0x040000F4 RID: 244
		Suspend,
		// Token: 0x040000F5 RID: 245
		Send,
		// Token: 0x040000F6 RID: 246
		Receive = 240
	}
}
