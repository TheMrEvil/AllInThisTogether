using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000DE RID: 222
	[CallbackIdentity(117)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct IPCFailure_t
	{
		// Token: 0x040002AE RID: 686
		public const int k_iCallback = 117;

		// Token: 0x040002AF RID: 687
		public byte m_eFailureType;
	}
}
