using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000E1 RID: 225
	[CallbackIdentity(152)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MicroTxnAuthorizationResponse_t
	{
		// Token: 0x040002B5 RID: 693
		public const int k_iCallback = 152;

		// Token: 0x040002B6 RID: 694
		public uint m_unAppID;

		// Token: 0x040002B7 RID: 695
		public ulong m_ulOrderID;

		// Token: 0x040002B8 RID: 696
		public byte m_bAuthorized;
	}
}
