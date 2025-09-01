using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000EF RID: 239
	[CallbackIdentity(1107)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct NumberOfCurrentPlayers_t
	{
		// Token: 0x040002F1 RID: 753
		public const int k_iCallback = 1107;

		// Token: 0x040002F2 RID: 754
		public byte m_bSuccess;

		// Token: 0x040002F3 RID: 755
		public int m_cPlayers;
	}
}
