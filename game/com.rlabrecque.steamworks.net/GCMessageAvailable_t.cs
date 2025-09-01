using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000045 RID: 69
	[CallbackIdentity(1701)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GCMessageAvailable_t
	{
		// Token: 0x04000069 RID: 105
		public const int k_iCallback = 1701;

		// Token: 0x0400006A RID: 106
		public uint m_nMessageSize;
	}
}
