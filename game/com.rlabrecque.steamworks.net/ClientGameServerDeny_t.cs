using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000DD RID: 221
	[CallbackIdentity(113)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ClientGameServerDeny_t
	{
		// Token: 0x040002A8 RID: 680
		public const int k_iCallback = 113;

		// Token: 0x040002A9 RID: 681
		public uint m_uAppID;

		// Token: 0x040002AA RID: 682
		public uint m_unGameServerIP;

		// Token: 0x040002AB RID: 683
		public ushort m_usGameServerPort;

		// Token: 0x040002AC RID: 684
		public ushort m_bSecure;

		// Token: 0x040002AD RID: 685
		public uint m_uReason;
	}
}
