using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E8 RID: 232
	[CallbackIdentity(168)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GetTicketForWebApiResponse_t
	{
		// Token: 0x040002D1 RID: 721
		public const int k_iCallback = 168;

		// Token: 0x040002D2 RID: 722
		public HAuthTicket m_hAuthTicket;

		// Token: 0x040002D3 RID: 723
		public EResult m_eResult;

		// Token: 0x040002D4 RID: 724
		public int m_cubTicket;

		// Token: 0x040002D5 RID: 725
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2560)]
		public byte[] m_rgubTicket;
	}
}
