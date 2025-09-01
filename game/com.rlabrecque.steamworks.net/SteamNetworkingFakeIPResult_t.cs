using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000100 RID: 256
	[CallbackIdentity(1223)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamNetworkingFakeIPResult_t
	{
		// Token: 0x0400031D RID: 797
		public const int k_iCallback = 1223;

		// Token: 0x0400031E RID: 798
		public EResult m_eResult;

		// Token: 0x0400031F RID: 799
		public SteamNetworkingIdentity m_identity;

		// Token: 0x04000320 RID: 800
		public uint m_unIP;

		// Token: 0x04000321 RID: 801
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public ushort[] m_unPorts;
	}
}
