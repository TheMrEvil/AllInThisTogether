using System;
using UnityEngine.Scripting;

namespace UnityEngineInternal.Video
{
	// Token: 0x02000011 RID: 17
	[UsedByNativeCode]
	internal enum VideoError
	{
		// Token: 0x0400002B RID: 43
		NoErr,
		// Token: 0x0400002C RID: 44
		OutOfMemoryErr,
		// Token: 0x0400002D RID: 45
		CantReadFile,
		// Token: 0x0400002E RID: 46
		CantWriteFile,
		// Token: 0x0400002F RID: 47
		BadParams,
		// Token: 0x04000030 RID: 48
		NoData,
		// Token: 0x04000031 RID: 49
		BadPermissions,
		// Token: 0x04000032 RID: 50
		DeviceNotAvailable,
		// Token: 0x04000033 RID: 51
		ResourceNotAvailable,
		// Token: 0x04000034 RID: 52
		NetworkErr
	}
}
