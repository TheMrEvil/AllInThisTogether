using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200004B RID: 75
	[CallbackIdentity(115)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSPolicyResponse_t
	{
		// Token: 0x0400007A RID: 122
		public const int k_iCallback = 115;

		// Token: 0x0400007B RID: 123
		public byte m_bSecure;
	}
}
