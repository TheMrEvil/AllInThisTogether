using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000F6 RID: 246
	[CallbackIdentity(702)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LowBatteryPower_t
	{
		// Token: 0x04000305 RID: 773
		public const int k_iCallback = 702;

		// Token: 0x04000306 RID: 774
		public byte m_nMinutesBatteryLeft;
	}
}
