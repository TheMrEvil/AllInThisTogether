using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200002E RID: 46
	[CallbackIdentity(1030)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct TimedTrialStatus_t
	{
		// Token: 0x04000016 RID: 22
		public const int k_iCallback = 1030;

		// Token: 0x04000017 RID: 23
		public AppId_t m_unAppID;

		// Token: 0x04000018 RID: 24
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bIsOffline;

		// Token: 0x04000019 RID: 25
		public uint m_unSecondsAllowed;

		// Token: 0x0400001A RID: 26
		public uint m_unSecondsPlayed;
	}
}
