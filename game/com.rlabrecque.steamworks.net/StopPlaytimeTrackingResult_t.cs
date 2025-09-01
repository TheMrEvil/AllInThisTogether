using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000D1 RID: 209
	[CallbackIdentity(3411)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct StopPlaytimeTrackingResult_t
	{
		// Token: 0x0400027E RID: 638
		public const int k_iCallback = 3411;

		// Token: 0x0400027F RID: 639
		public EResult m_eResult;
	}
}
