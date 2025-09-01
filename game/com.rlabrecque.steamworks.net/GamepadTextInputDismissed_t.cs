using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000FA RID: 250
	[CallbackIdentity(714)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GamepadTextInputDismissed_t
	{
		// Token: 0x0400030E RID: 782
		public const int k_iCallback = 714;

		// Token: 0x0400030F RID: 783
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bSubmitted;

		// Token: 0x04000310 RID: 784
		public uint m_unSubmittedText;

		// Token: 0x04000311 RID: 785
		public AppId_t m_unAppID;
	}
}
