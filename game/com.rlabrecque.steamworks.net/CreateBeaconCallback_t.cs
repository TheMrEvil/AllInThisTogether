using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200008B RID: 139
	[CallbackIdentity(5302)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CreateBeaconCallback_t
	{
		// Token: 0x04000193 RID: 403
		public const int k_iCallback = 5302;

		// Token: 0x04000194 RID: 404
		public EResult m_eResult;

		// Token: 0x04000195 RID: 405
		public PartyBeaconID_t m_ulBeaconID;
	}
}
