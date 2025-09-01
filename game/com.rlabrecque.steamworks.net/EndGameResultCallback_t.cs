using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000089 RID: 137
	[CallbackIdentity(5215)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct EndGameResultCallback_t
	{
		// Token: 0x0400018B RID: 395
		public const int k_iCallback = 5215;

		// Token: 0x0400018C RID: 396
		public EResult m_eResult;

		// Token: 0x0400018D RID: 397
		public ulong ullUniqueGameID;
	}
}
