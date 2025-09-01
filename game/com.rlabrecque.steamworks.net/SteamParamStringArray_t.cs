using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000175 RID: 373
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamParamStringArray_t
	{
		// Token: 0x040009E8 RID: 2536
		public IntPtr m_ppStrings;

		// Token: 0x040009E9 RID: 2537
		public int m_nNumStrings;
	}
}
