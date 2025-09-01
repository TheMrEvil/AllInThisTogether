using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000179 RID: 377
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CallbackMsg_t
	{
		// Token: 0x04000A0B RID: 2571
		public int m_hSteamUser;

		// Token: 0x04000A0C RID: 2572
		public int m_iCallback;

		// Token: 0x04000A0D RID: 2573
		public IntPtr m_pubParam;

		// Token: 0x04000A0E RID: 2574
		public int m_cubParam;
	}
}
