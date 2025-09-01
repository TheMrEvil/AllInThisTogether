using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200017A RID: 378
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamNetConnectionInfo_t
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x0000C237 File Offset: 0x0000A437
		// (set) Token: 0x06000898 RID: 2200 RVA: 0x0000C244 File Offset: 0x0000A444
		public string m_szEndDebug
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_szEndDebug_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_szEndDebug_, 128);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x0000C257 File Offset: 0x0000A457
		// (set) Token: 0x0600089A RID: 2202 RVA: 0x0000C264 File Offset: 0x0000A464
		public string m_szConnectionDescription
		{
			get
			{
				return InteropHelp.ByteArrayToStringUTF8(this.m_szConnectionDescription_);
			}
			set
			{
				InteropHelp.StringToByteArrayUTF8(value, this.m_szConnectionDescription_, 128);
			}
		}

		// Token: 0x04000A0F RID: 2575
		public SteamNetworkingIdentity m_identityRemote;

		// Token: 0x04000A10 RID: 2576
		public long m_nUserData;

		// Token: 0x04000A11 RID: 2577
		public HSteamListenSocket m_hListenSocket;

		// Token: 0x04000A12 RID: 2578
		public SteamNetworkingIPAddr m_addrRemote;

		// Token: 0x04000A13 RID: 2579
		public ushort m__pad1;

		// Token: 0x04000A14 RID: 2580
		public SteamNetworkingPOPID m_idPOPRemote;

		// Token: 0x04000A15 RID: 2581
		public SteamNetworkingPOPID m_idPOPRelay;

		// Token: 0x04000A16 RID: 2582
		public ESteamNetworkingConnectionState m_eState;

		// Token: 0x04000A17 RID: 2583
		public int m_eEndReason;

		// Token: 0x04000A18 RID: 2584
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		private byte[] m_szEndDebug_;

		// Token: 0x04000A19 RID: 2585
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		private byte[] m_szConnectionDescription_;

		// Token: 0x04000A1A RID: 2586
		public int m_nFlags;

		// Token: 0x04000A1B RID: 2587
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 63)]
		public uint[] reserved;
	}
}
