using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000076 RID: 118
	[CallbackIdentity(4704)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamInventoryStartPurchaseResult_t
	{
		// Token: 0x04000131 RID: 305
		public const int k_iCallback = 4704;

		// Token: 0x04000132 RID: 306
		public EResult m_result;

		// Token: 0x04000133 RID: 307
		public ulong m_ulOrderID;

		// Token: 0x04000134 RID: 308
		public ulong m_ulTransID;
	}
}
