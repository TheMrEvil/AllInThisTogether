using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020001B2 RID: 434
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamNetworkingQuickConnectionStatus
	{
		// Token: 0x04000B8F RID: 2959
		internal ConnectionState State;

		// Token: 0x04000B90 RID: 2960
		internal int Ping;

		// Token: 0x04000B91 RID: 2961
		internal float ConnectionQualityLocal;

		// Token: 0x04000B92 RID: 2962
		internal float ConnectionQualityRemote;

		// Token: 0x04000B93 RID: 2963
		internal float OutPacketsPerSec;

		// Token: 0x04000B94 RID: 2964
		internal float OutBytesPerSec;

		// Token: 0x04000B95 RID: 2965
		internal float InPacketsPerSec;

		// Token: 0x04000B96 RID: 2966
		internal float InBytesPerSec;

		// Token: 0x04000B97 RID: 2967
		internal int SendRateBytesPerSecond;

		// Token: 0x04000B98 RID: 2968
		internal int CbPendingUnreliable;

		// Token: 0x04000B99 RID: 2969
		internal int CbPendingReliable;

		// Token: 0x04000B9A RID: 2970
		internal int CbSentUnackedReliable;

		// Token: 0x04000B9B RID: 2971
		internal long EcQueueTime;

		// Token: 0x04000B9C RID: 2972
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.U4)]
		internal uint[] Reserved;
	}
}
