using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x0200001A RID: 26
	internal class ISteamMatchmakingPingResponse : SteamInterface
	{
		// Token: 0x06000395 RID: 917 RVA: 0x00006F91 File Offset: 0x00005191
		internal ISteamMatchmakingPingResponse(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x06000396 RID: 918
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingPingResponse_ServerResponded")]
		private static extern void _ServerResponded(IntPtr self, ref gameserveritem_t server);

		// Token: 0x06000397 RID: 919 RVA: 0x00006FA3 File Offset: 0x000051A3
		internal void ServerResponded(ref gameserveritem_t server)
		{
			ISteamMatchmakingPingResponse._ServerResponded(this.Self, ref server);
		}

		// Token: 0x06000398 RID: 920
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamMatchmakingPingResponse_ServerFailedToRespond")]
		private static extern void _ServerFailedToRespond(IntPtr self);

		// Token: 0x06000399 RID: 921 RVA: 0x00006FB3 File Offset: 0x000051B3
		internal void ServerFailedToRespond()
		{
			ISteamMatchmakingPingResponse._ServerFailedToRespond(this.Self);
		}
	}
}
