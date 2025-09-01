using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200005D RID: 93
	[CallbackIdentity(4510)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_CanGoBackAndForward_t
	{
		// Token: 0x040000CA RID: 202
		public const int k_iCallback = 4510;

		// Token: 0x040000CB RID: 203
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000CC RID: 204
		[MarshalAs(UnmanagedType.I1)]
		public bool bCanGoBack;

		// Token: 0x040000CD RID: 205
		[MarshalAs(UnmanagedType.I1)]
		public bool bCanGoForward;
	}
}
