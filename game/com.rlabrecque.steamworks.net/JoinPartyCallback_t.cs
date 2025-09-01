using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200008A RID: 138
	[CallbackIdentity(5301)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct JoinPartyCallback_t
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x0000BF67 File Offset: 0x0000A167
		// (set) Token: 0x0600086B RID: 2155 RVA: 0x0000BF74 File Offset: 0x0000A174
		public string m_rgchConnectString
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_rgchConnectString_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_rgchConnectString_, 256);
			}
		}

		// Token: 0x0400018E RID: 398
		public const int k_iCallback = 5301;

		// Token: 0x0400018F RID: 399
		public EResult m_eResult;

		// Token: 0x04000190 RID: 400
		public PartyBeaconID_t m_ulBeaconID;

		// Token: 0x04000191 RID: 401
		public CSteamID m_SteamIDBeaconOwner;

		// Token: 0x04000192 RID: 402
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		private byte[] m_rgchConnectString_;
	}
}
