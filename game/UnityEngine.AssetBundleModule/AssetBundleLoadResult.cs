using System;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	public enum AssetBundleLoadResult
	{
		// Token: 0x04000002 RID: 2
		Success,
		// Token: 0x04000003 RID: 3
		Cancelled,
		// Token: 0x04000004 RID: 4
		NotMatchingCrc,
		// Token: 0x04000005 RID: 5
		FailedCache,
		// Token: 0x04000006 RID: 6
		NotValidAssetBundle,
		// Token: 0x04000007 RID: 7
		NoSerializedData,
		// Token: 0x04000008 RID: 8
		NotCompatible,
		// Token: 0x04000009 RID: 9
		AlreadyLoaded,
		// Token: 0x0400000A RID: 10
		FailedRead,
		// Token: 0x0400000B RID: 11
		FailedDecompression,
		// Token: 0x0400000C RID: 12
		FailedWrite,
		// Token: 0x0400000D RID: 13
		FailedDeleteRecompressionTarget,
		// Token: 0x0400000E RID: 14
		RecompressionTargetIsLoaded,
		// Token: 0x0400000F RID: 15
		RecompressionTargetExistsButNotArchive
	}
}
