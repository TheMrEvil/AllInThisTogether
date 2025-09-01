using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000F9 RID: 249
	[CallbackIdentity(705)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CheckFileSignature_t
	{
		// Token: 0x0400030C RID: 780
		public const int k_iCallback = 705;

		// Token: 0x0400030D RID: 781
		public ECheckFileSignature m_eCheckFileSignature;
	}
}
