using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200008D RID: 141
	[CallbackIdentity(5304)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ChangeNumOpenSlotsCallback_t
	{
		// Token: 0x04000199 RID: 409
		public const int k_iCallback = 5304;

		// Token: 0x0400019A RID: 410
		public EResult m_eResult;
	}
}
