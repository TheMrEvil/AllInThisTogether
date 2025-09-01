using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000058 RID: 88
	[CallbackIdentity(4505)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_URLChanged_t
	{
		// Token: 0x040000B5 RID: 181
		public const int k_iCallback = 4505;

		// Token: 0x040000B6 RID: 182
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000B7 RID: 183
		public string pchURL;

		// Token: 0x040000B8 RID: 184
		public string pchPostData;

		// Token: 0x040000B9 RID: 185
		[MarshalAs(UnmanagedType.I1)]
		public bool bIsRedirect;

		// Token: 0x040000BA RID: 186
		public string pchPageTitle;

		// Token: 0x040000BB RID: 187
		[MarshalAs(UnmanagedType.I1)]
		public bool bNewNavigation;
	}
}
