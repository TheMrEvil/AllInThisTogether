using System;

namespace System.Net
{
	// Token: 0x0200058B RID: 1419
	internal enum FtpOperation
	{
		// Token: 0x0400194A RID: 6474
		DownloadFile,
		// Token: 0x0400194B RID: 6475
		ListDirectory,
		// Token: 0x0400194C RID: 6476
		ListDirectoryDetails,
		// Token: 0x0400194D RID: 6477
		UploadFile,
		// Token: 0x0400194E RID: 6478
		UploadFileUnique,
		// Token: 0x0400194F RID: 6479
		AppendFile,
		// Token: 0x04001950 RID: 6480
		DeleteFile,
		// Token: 0x04001951 RID: 6481
		GetDateTimestamp,
		// Token: 0x04001952 RID: 6482
		GetFileSize,
		// Token: 0x04001953 RID: 6483
		Rename,
		// Token: 0x04001954 RID: 6484
		MakeDirectory,
		// Token: 0x04001955 RID: 6485
		RemoveDirectory,
		// Token: 0x04001956 RID: 6486
		PrintWorkingDirectory,
		// Token: 0x04001957 RID: 6487
		Other
	}
}
