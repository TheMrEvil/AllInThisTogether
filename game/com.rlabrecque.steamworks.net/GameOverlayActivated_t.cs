using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000030 RID: 48
	[CallbackIdentity(331)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameOverlayActivated_t
	{
		// Token: 0x0400001E RID: 30
		public const int k_iCallback = 331;

		// Token: 0x0400001F RID: 31
		public byte m_bActive;

		// Token: 0x04000020 RID: 32
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUserInitiated;

		// Token: 0x04000021 RID: 33
		public AppId_t m_nAppID;
	}
}
