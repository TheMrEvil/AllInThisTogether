using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000072 RID: 114
	[CallbackIdentity(4700)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamInventoryResultReady_t
	{
		// Token: 0x04000126 RID: 294
		public const int k_iCallback = 4700;

		// Token: 0x04000127 RID: 295
		public SteamInventoryResult_t m_handle;

		// Token: 0x04000128 RID: 296
		public EResult m_result;
	}
}
