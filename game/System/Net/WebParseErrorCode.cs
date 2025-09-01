using System;

namespace System.Net
{
	// Token: 0x02000624 RID: 1572
	internal enum WebParseErrorCode
	{
		// Token: 0x04001CEF RID: 7407
		Generic,
		// Token: 0x04001CF0 RID: 7408
		InvalidHeaderName,
		// Token: 0x04001CF1 RID: 7409
		InvalidContentLength,
		// Token: 0x04001CF2 RID: 7410
		IncompleteHeaderLine,
		// Token: 0x04001CF3 RID: 7411
		CrLfError,
		// Token: 0x04001CF4 RID: 7412
		InvalidChunkFormat,
		// Token: 0x04001CF5 RID: 7413
		UnexpectedServerResponse
	}
}
