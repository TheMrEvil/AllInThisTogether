using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000060 RID: 96
	[CallbackIdentity(4513)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_LinkAtPosition_t
	{
		// Token: 0x040000DC RID: 220
		public const int k_iCallback = 4513;

		// Token: 0x040000DD RID: 221
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040000DE RID: 222
		public uint x;

		// Token: 0x040000DF RID: 223
		public uint y;

		// Token: 0x040000E0 RID: 224
		public string pchURL;

		// Token: 0x040000E1 RID: 225
		[MarshalAs(UnmanagedType.I1)]
		public bool bInput;

		// Token: 0x040000E2 RID: 226
		[MarshalAs(UnmanagedType.I1)]
		public bool bLiveLink;
	}
}
