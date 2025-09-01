using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200009C RID: 156
	[CallbackIdentity(4011)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MusicPlayerWantsVolume_t
	{
		// Token: 0x040001AC RID: 428
		public const int k_iCallback = 4011;

		// Token: 0x040001AD RID: 429
		public float m_flNewVolume;
	}
}
