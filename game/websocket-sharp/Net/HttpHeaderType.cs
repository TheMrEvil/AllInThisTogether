using System;

namespace WebSocketSharp.Net
{
	// Token: 0x0200002D RID: 45
	[Flags]
	internal enum HttpHeaderType
	{
		// Token: 0x0400016E RID: 366
		Unspecified = 0,
		// Token: 0x0400016F RID: 367
		Request = 1,
		// Token: 0x04000170 RID: 368
		Response = 2,
		// Token: 0x04000171 RID: 369
		Restricted = 4,
		// Token: 0x04000172 RID: 370
		MultiValue = 8,
		// Token: 0x04000173 RID: 371
		MultiValueInRequest = 16,
		// Token: 0x04000174 RID: 372
		MultiValueInResponse = 32
	}
}
