using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200004F RID: 79
	[CallbackIdentity(210)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct AssociateWithClanResult_t
	{
		// Token: 0x0400008E RID: 142
		public const int k_iCallback = 210;

		// Token: 0x0400008F RID: 143
		public EResult m_eResult;
	}
}
