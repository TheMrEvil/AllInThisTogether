using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001B4 RID: 436
	[Serializable]
	public struct SteamNetworkingErrMsg
	{
		// Token: 0x04000ACF RID: 2767
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
		public byte[] m_SteamNetworkingErrMsg;
	}
}
