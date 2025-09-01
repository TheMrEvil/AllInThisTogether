using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E3 RID: 227
	[CallbackIdentity(163)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GetAuthSessionTicketResponse_t
	{
		// Token: 0x040002BB RID: 699
		public const int k_iCallback = 163;

		// Token: 0x040002BC RID: 700
		public HAuthTicket m_hAuthTicket;

		// Token: 0x040002BD RID: 701
		public EResult m_eResult;
	}
}
