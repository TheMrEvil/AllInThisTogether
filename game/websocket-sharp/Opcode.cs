using System;

namespace WebSocketSharp
{
	// Token: 0x0200000B RID: 11
	internal enum Opcode : byte
	{
		// Token: 0x0400005A RID: 90
		Cont,
		// Token: 0x0400005B RID: 91
		Text,
		// Token: 0x0400005C RID: 92
		Binary,
		// Token: 0x0400005D RID: 93
		Close = 8,
		// Token: 0x0400005E RID: 94
		Ping,
		// Token: 0x0400005F RID: 95
		Pong
	}
}
