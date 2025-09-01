using System;

namespace WebSocketSharp.Net
{
	// Token: 0x02000036 RID: 54
	internal enum InputChunkState
	{
		// Token: 0x0400018D RID: 397
		None,
		// Token: 0x0400018E RID: 398
		Data,
		// Token: 0x0400018F RID: 399
		DataEnded,
		// Token: 0x04000190 RID: 400
		Trailer,
		// Token: 0x04000191 RID: 401
		End
	}
}
