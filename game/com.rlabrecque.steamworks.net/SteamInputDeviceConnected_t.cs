using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200006E RID: 110
	[CallbackIdentity(2801)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamInputDeviceConnected_t
	{
		// Token: 0x04000114 RID: 276
		public const int k_iCallback = 2801;

		// Token: 0x04000115 RID: 277
		public InputHandle_t m_ulConnectedDeviceHandle;
	}
}
