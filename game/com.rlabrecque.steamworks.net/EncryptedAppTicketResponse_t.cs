using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E2 RID: 226
	[CallbackIdentity(154)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct EncryptedAppTicketResponse_t
	{
		// Token: 0x040002B9 RID: 697
		public const int k_iCallback = 154;

		// Token: 0x040002BA RID: 698
		public EResult m_eResult;
	}
}
