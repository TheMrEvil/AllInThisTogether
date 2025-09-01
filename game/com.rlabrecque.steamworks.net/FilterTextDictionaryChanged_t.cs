using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000FD RID: 253
	[CallbackIdentity(739)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct FilterTextDictionaryChanged_t
	{
		// Token: 0x04000314 RID: 788
		public const int k_iCallback = 739;

		// Token: 0x04000315 RID: 789
		public int m_eLanguage;
	}
}
