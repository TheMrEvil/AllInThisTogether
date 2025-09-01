using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E0 RID: 224
	[CallbackIdentity(143)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct ValidateAuthTicketResponse_t
	{
		// Token: 0x040002B1 RID: 689
		public const int k_iCallback = 143;

		// Token: 0x040002B2 RID: 690
		public CSteamID m_SteamID;

		// Token: 0x040002B3 RID: 691
		public EAuthSessionResponse m_eAuthSessionResponse;

		// Token: 0x040002B4 RID: 692
		public CSteamID m_OwnerSteamID;
	}
}
