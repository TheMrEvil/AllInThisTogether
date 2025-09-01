using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000071 RID: 113
	[CallbackIdentity(2804)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamInputGamepadSlotChange_t
	{
		// Token: 0x04000120 RID: 288
		public const int k_iCallback = 2804;

		// Token: 0x04000121 RID: 289
		public AppId_t m_unAppID;

		// Token: 0x04000122 RID: 290
		public InputHandle_t m_ulDeviceHandle;

		// Token: 0x04000123 RID: 291
		public ESteamInputType m_eDeviceType;

		// Token: 0x04000124 RID: 292
		public int m_nOldGamepadSlot;

		// Token: 0x04000125 RID: 293
		public int m_nNewGamepadSlot;
	}
}
