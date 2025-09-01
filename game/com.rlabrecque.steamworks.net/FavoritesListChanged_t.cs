using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000078 RID: 120
	[CallbackIdentity(502)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct FavoritesListChanged_t
	{
		// Token: 0x04000138 RID: 312
		public const int k_iCallback = 502;

		// Token: 0x04000139 RID: 313
		public uint m_nIP;

		// Token: 0x0400013A RID: 314
		public uint m_nQueryPort;

		// Token: 0x0400013B RID: 315
		public uint m_nConnPort;

		// Token: 0x0400013C RID: 316
		public uint m_nAppID;

		// Token: 0x0400013D RID: 317
		public uint m_nFlags;

		// Token: 0x0400013E RID: 318
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bAdd;

		// Token: 0x0400013F RID: 319
		public AccountID_t m_unAccountId;
	}
}
