using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200004D RID: 77
	[CallbackIdentity(208)]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct GSClientGroupStatus_t
	{
		// Token: 0x04000081 RID: 129
		public const int k_iCallback = 208;

		// Token: 0x04000082 RID: 130
		public CSteamID m_SteamIDUser;

		// Token: 0x04000083 RID: 131
		public CSteamID m_SteamIDGroup;

		// Token: 0x04000084 RID: 132
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bMember;

		// Token: 0x04000085 RID: 133
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bOfficer;
	}
}
