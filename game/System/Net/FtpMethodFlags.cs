using System;

namespace System.Net
{
	// Token: 0x0200058C RID: 1420
	[Flags]
	internal enum FtpMethodFlags
	{
		// Token: 0x04001959 RID: 6489
		None = 0,
		// Token: 0x0400195A RID: 6490
		IsDownload = 1,
		// Token: 0x0400195B RID: 6491
		IsUpload = 2,
		// Token: 0x0400195C RID: 6492
		TakesParameter = 4,
		// Token: 0x0400195D RID: 6493
		MayTakeParameter = 8,
		// Token: 0x0400195E RID: 6494
		DoesNotTakeParameter = 16,
		// Token: 0x0400195F RID: 6495
		ParameterIsDirectory = 32,
		// Token: 0x04001960 RID: 6496
		ShouldParseForResponseUri = 64,
		// Token: 0x04001961 RID: 6497
		HasHttpCommand = 128,
		// Token: 0x04001962 RID: 6498
		MustChangeWorkingDirectoryToPath = 256
	}
}
