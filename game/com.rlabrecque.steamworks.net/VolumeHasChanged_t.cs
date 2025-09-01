using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000091 RID: 145
	[CallbackIdentity(4002)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct VolumeHasChanged_t
	{
		// Token: 0x0400019E RID: 414
		public const int k_iCallback = 4002;

		// Token: 0x0400019F RID: 415
		public float m_flNewVolume;
	}
}
