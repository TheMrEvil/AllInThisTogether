using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000FF RID: 255
	[CallbackIdentity(4624)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GetOPFSettingsResult_t
	{
		// Token: 0x0400031A RID: 794
		public const int k_iCallback = 4624;

		// Token: 0x0400031B RID: 795
		public EResult m_eResult;

		// Token: 0x0400031C RID: 796
		public AppId_t m_unVideoAppID;
	}
}
