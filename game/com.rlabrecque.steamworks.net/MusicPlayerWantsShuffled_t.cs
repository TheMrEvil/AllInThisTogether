using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200009A RID: 154
	[CallbackIdentity(4109)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MusicPlayerWantsShuffled_t
	{
		// Token: 0x040001A8 RID: 424
		public const int k_iCallback = 4109;

		// Token: 0x040001A9 RID: 425
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bShuffled;
	}
}
