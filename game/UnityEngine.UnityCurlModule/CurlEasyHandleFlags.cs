using System;

namespace Unity.Curl
{
	// Token: 0x02000002 RID: 2
	[Flags]
	internal enum CurlEasyHandleFlags : uint
	{
		// Token: 0x04000002 RID: 2
		kSendBody = 1U,
		// Token: 0x04000003 RID: 3
		kReceiveHeaders = 2U,
		// Token: 0x04000004 RID: 4
		kReceiveBody = 4U,
		// Token: 0x04000005 RID: 5
		kFollowRedirects = 8U
	}
}
