using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020001A8 RID: 424
	internal struct NetMsg
	{
		// Token: 0x06000DB1 RID: 3505
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_SteamNetworkingMessage_t_Release")]
		internal unsafe static extern void InternalRelease(NetMsg* self);

		// Token: 0x04000B4B RID: 2891
		internal IntPtr DataPtr;

		// Token: 0x04000B4C RID: 2892
		internal int DataSize;

		// Token: 0x04000B4D RID: 2893
		internal Connection Connection;

		// Token: 0x04000B4E RID: 2894
		internal NetIdentity Identity;

		// Token: 0x04000B4F RID: 2895
		internal long ConnectionUserData;

		// Token: 0x04000B50 RID: 2896
		internal long RecvTime;

		// Token: 0x04000B51 RID: 2897
		internal long MessageNumber;

		// Token: 0x04000B52 RID: 2898
		internal IntPtr FreeDataPtr;

		// Token: 0x04000B53 RID: 2899
		internal IntPtr ReleasePtr;

		// Token: 0x04000B54 RID: 2900
		internal int Channel;
	}
}
