using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200009F RID: 159
	[CallbackIdentity(4114)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MusicPlayerWantsPlayingRepeatStatus_t
	{
		// Token: 0x040001B2 RID: 434
		public const int k_iCallback = 4114;

		// Token: 0x040001B3 RID: 435
		public int m_nPlayingRepeatStatus;
	}
}
