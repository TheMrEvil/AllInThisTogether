using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020001B1 RID: 433
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamTVRegion_t
	{
		// Token: 0x04000B8B RID: 2955
		internal uint UnMinX;

		// Token: 0x04000B8C RID: 2956
		internal uint UnMinY;

		// Token: 0x04000B8D RID: 2957
		internal uint UnMaxX;

		// Token: 0x04000B8E RID: 2958
		internal uint UnMaxY;
	}
}
