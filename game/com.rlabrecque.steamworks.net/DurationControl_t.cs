using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E7 RID: 231
	[CallbackIdentity(167)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DurationControl_t
	{
		// Token: 0x040002C8 RID: 712
		public const int k_iCallback = 167;

		// Token: 0x040002C9 RID: 713
		public EResult m_eResult;

		// Token: 0x040002CA RID: 714
		public AppId_t m_appid;

		// Token: 0x040002CB RID: 715
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bApplicable;

		// Token: 0x040002CC RID: 716
		public int m_csecsLast5h;

		// Token: 0x040002CD RID: 717
		public EDurationControlProgress m_progress;

		// Token: 0x040002CE RID: 718
		public EDurationControlNotification m_notification;

		// Token: 0x040002CF RID: 719
		public int m_csecsToday;

		// Token: 0x040002D0 RID: 720
		public int m_csecsRemaining;
	}
}
