using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200002F RID: 47
	[CallbackIdentity(304)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct PersonaStateChange_t
	{
		// Token: 0x0400001B RID: 27
		public const int k_iCallback = 304;

		// Token: 0x0400001C RID: 28
		public ulong m_ulSteamID;

		// Token: 0x0400001D RID: 29
		public EPersonaChange m_nChangeFlags;
	}
}
