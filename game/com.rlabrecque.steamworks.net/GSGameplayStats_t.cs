using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200004C RID: 76
	[CallbackIdentity(207)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSGameplayStats_t
	{
		// Token: 0x0400007C RID: 124
		public const int k_iCallback = 207;

		// Token: 0x0400007D RID: 125
		public EResult m_eResult;

		// Token: 0x0400007E RID: 126
		public int m_nRank;

		// Token: 0x0400007F RID: 127
		public uint m_unTotalConnects;

		// Token: 0x04000080 RID: 128
		public uint m_unTotalMinutesPlayed;
	}
}
