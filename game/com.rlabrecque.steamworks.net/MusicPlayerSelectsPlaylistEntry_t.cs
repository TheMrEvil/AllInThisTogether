using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200009E RID: 158
	[CallbackIdentity(4013)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MusicPlayerSelectsPlaylistEntry_t
	{
		// Token: 0x040001B0 RID: 432
		public const int k_iCallback = 4013;

		// Token: 0x040001B1 RID: 433
		public int nID;
	}
}
