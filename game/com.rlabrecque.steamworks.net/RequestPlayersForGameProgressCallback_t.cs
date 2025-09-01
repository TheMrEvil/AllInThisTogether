using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000085 RID: 133
	[CallbackIdentity(5211)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct RequestPlayersForGameProgressCallback_t
	{
		// Token: 0x04000175 RID: 373
		public const int k_iCallback = 5211;

		// Token: 0x04000176 RID: 374
		public EResult m_eResult;

		// Token: 0x04000177 RID: 375
		public ulong m_ullSearchID;
	}
}
