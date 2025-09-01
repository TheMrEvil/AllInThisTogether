using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000056 RID: 86
	[CallbackIdentity(4503)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_StartRequest_t
	{
		// Token: 0x040000AD RID: 173
		public const int k_iCallback = 4503;

		// Token: 0x040000AE RID: 174
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000AF RID: 175
		public string pchURL;

		// Token: 0x040000B0 RID: 176
		public string pchTarget;

		// Token: 0x040000B1 RID: 177
		public string pchPostData;

		// Token: 0x040000B2 RID: 178
		[MarshalAs(UnmanagedType.I1)]
		public bool bIsRedirect;
	}
}
