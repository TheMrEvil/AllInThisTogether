using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000070 RID: 112
	[CallbackIdentity(2803)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamInputConfigurationLoaded_t
	{
		// Token: 0x04000118 RID: 280
		public const int k_iCallback = 2803;

		// Token: 0x04000119 RID: 281
		public AppId_t m_unAppID;

		// Token: 0x0400011A RID: 282
		public InputHandle_t m_ulDeviceHandle;

		// Token: 0x0400011B RID: 283
		public CSteamID m_ulMappingCreator;

		// Token: 0x0400011C RID: 284
		public uint m_unMajorRevision;

		// Token: 0x0400011D RID: 285
		public uint m_unMinorRevision;

		// Token: 0x0400011E RID: 286
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUsesSteamInputAPI;

		// Token: 0x0400011F RID: 287
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUsesGamepadAPI;
	}
}
