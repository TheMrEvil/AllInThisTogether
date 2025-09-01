using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200009D RID: 157
	[CallbackIdentity(4012)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MusicPlayerSelectsQueueEntry_t
	{
		// Token: 0x040001AE RID: 430
		public const int k_iCallback = 4012;

		// Token: 0x040001AF RID: 431
		public int nID;
	}
}
