using System;

namespace System.Net
{
	// Token: 0x02000586 RID: 1414
	internal enum FtpLoginState : byte
	{
		// Token: 0x04001923 RID: 6435
		NotLoggedIn,
		// Token: 0x04001924 RID: 6436
		LoggedIn,
		// Token: 0x04001925 RID: 6437
		LoggedInButNeedsRelogin,
		// Token: 0x04001926 RID: 6438
		ReloginFailed
	}
}
