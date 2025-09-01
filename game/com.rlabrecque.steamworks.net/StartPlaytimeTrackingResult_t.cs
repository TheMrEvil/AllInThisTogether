using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000D0 RID: 208
	[CallbackIdentity(3410)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct StartPlaytimeTrackingResult_t
	{
		// Token: 0x0400027C RID: 636
		public const int k_iCallback = 3410;

		// Token: 0x0400027D RID: 637
		public EResult m_eResult;
	}
}
