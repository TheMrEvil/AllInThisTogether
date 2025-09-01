using System;

namespace System.Net
{
	// Token: 0x020006BD RID: 1725
	internal enum ReadState
	{
		// Token: 0x0400204C RID: 8268
		None,
		// Token: 0x0400204D RID: 8269
		Status,
		// Token: 0x0400204E RID: 8270
		Headers,
		// Token: 0x0400204F RID: 8271
		Content,
		// Token: 0x04002050 RID: 8272
		Aborted
	}
}
