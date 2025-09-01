using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000A7 RID: 167
	[CallbackIdentity(1281)]
	public struct SteamRelayNetworkStatus_t
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x0000BFA7 File Offset: 0x0000A1A7
		// (set) Token: 0x0600086F RID: 2159 RVA: 0x0000BFB4 File Offset: 0x0000A1B4
		public string m_debugMsg
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_debugMsg_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_debugMsg_, 256);
			}
		}

		// Token: 0x040001C9 RID: 457
		public const int k_iCallback = 1281;

		// Token: 0x040001CA RID: 458
		public ESteamNetworkingAvailability m_eAvail;

		// Token: 0x040001CB RID: 459
		public int m_bPingMeasurementInProgress;

		// Token: 0x040001CC RID: 460
		public ESteamNetworkingAvailability m_eAvailNetworkConfig;

		// Token: 0x040001CD RID: 461
		public ESteamNetworkingAvailability m_eAvailAnyRelay;

		// Token: 0x040001CE RID: 462
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		private byte[] m_debugMsg_;
	}
}
