using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000D6 RID: 214
	[CallbackIdentity(3416)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GetAppDependenciesResult_t
	{
		// Token: 0x04000290 RID: 656
		public const int k_iCallback = 3416;

		// Token: 0x04000291 RID: 657
		public EResult m_eResult;

		// Token: 0x04000292 RID: 658
		public PublishedFileId_t m_nPublishedFileId;

		// Token: 0x04000293 RID: 659
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		public AppId_t[] m_rgAppIDs;

		// Token: 0x04000294 RID: 660
		public uint m_nNumAppDependencies;

		// Token: 0x04000295 RID: 661
		public uint m_nTotalNumAppDependencies;
	}
}
