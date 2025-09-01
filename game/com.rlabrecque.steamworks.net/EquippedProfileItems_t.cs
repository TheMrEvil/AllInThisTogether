using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000044 RID: 68
	[CallbackIdentity(351)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct EquippedProfileItems_t
	{
		// Token: 0x04000061 RID: 97
		public const int k_iCallback = 351;

		// Token: 0x04000062 RID: 98
		public EResult m_eResult;

		// Token: 0x04000063 RID: 99
		public CSteamID m_steamID;

		// Token: 0x04000064 RID: 100
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bHasAnimatedAvatar;

		// Token: 0x04000065 RID: 101
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bHasAvatarFrame;

		// Token: 0x04000066 RID: 102
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bHasProfileModifier;

		// Token: 0x04000067 RID: 103
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bHasProfileBackground;

		// Token: 0x04000068 RID: 104
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bHasMiniProfileBackground;
	}
}
