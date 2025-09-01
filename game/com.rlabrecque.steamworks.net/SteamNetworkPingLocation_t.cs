using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200017D RID: 381
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamNetworkPingLocation_t
	{
		// Token: 0x04000A30 RID: 2608
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
		public byte[] m_data;
	}
}
