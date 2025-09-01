using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200009B RID: 155
	[CallbackIdentity(4110)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MusicPlayerWantsLooped_t
	{
		// Token: 0x040001AA RID: 426
		public const int k_iCallback = 4110;

		// Token: 0x040001AB RID: 427
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bLooped;
	}
}
