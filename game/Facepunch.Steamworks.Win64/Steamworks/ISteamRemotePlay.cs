using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x02000028 RID: 40
	internal class ISteamRemotePlay : SteamInterface
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x000085F9 File Offset: 0x000067F9
		internal ISteamRemotePlay(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x0600050A RID: 1290
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamRemotePlay_v001();

		// Token: 0x0600050B RID: 1291 RVA: 0x0000860B File Offset: 0x0000680B
		public override IntPtr GetUserInterfacePointer()
		{
			return ISteamRemotePlay.SteamAPI_SteamRemotePlay_v001();
		}

		// Token: 0x0600050C RID: 1292
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemotePlay_GetSessionCount")]
		private static extern uint _GetSessionCount(IntPtr self);

		// Token: 0x0600050D RID: 1293 RVA: 0x00008614 File Offset: 0x00006814
		internal uint GetSessionCount()
		{
			return ISteamRemotePlay._GetSessionCount(this.Self);
		}

		// Token: 0x0600050E RID: 1294
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemotePlay_GetSessionID")]
		private static extern RemotePlaySessionID_t _GetSessionID(IntPtr self, int iSessionIndex);

		// Token: 0x0600050F RID: 1295 RVA: 0x00008634 File Offset: 0x00006834
		internal RemotePlaySessionID_t GetSessionID(int iSessionIndex)
		{
			return ISteamRemotePlay._GetSessionID(this.Self, iSessionIndex);
		}

		// Token: 0x06000510 RID: 1296
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemotePlay_GetSessionSteamID")]
		private static extern SteamId _GetSessionSteamID(IntPtr self, RemotePlaySessionID_t unSessionID);

		// Token: 0x06000511 RID: 1297 RVA: 0x00008654 File Offset: 0x00006854
		internal SteamId GetSessionSteamID(RemotePlaySessionID_t unSessionID)
		{
			return ISteamRemotePlay._GetSessionSteamID(this.Self, unSessionID);
		}

		// Token: 0x06000512 RID: 1298
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemotePlay_GetSessionClientName")]
		private static extern Utf8StringPointer _GetSessionClientName(IntPtr self, RemotePlaySessionID_t unSessionID);

		// Token: 0x06000513 RID: 1299 RVA: 0x00008674 File Offset: 0x00006874
		internal string GetSessionClientName(RemotePlaySessionID_t unSessionID)
		{
			Utf8StringPointer p = ISteamRemotePlay._GetSessionClientName(this.Self, unSessionID);
			return p;
		}

		// Token: 0x06000514 RID: 1300
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemotePlay_GetSessionClientFormFactor")]
		private static extern SteamDeviceFormFactor _GetSessionClientFormFactor(IntPtr self, RemotePlaySessionID_t unSessionID);

		// Token: 0x06000515 RID: 1301 RVA: 0x0000869C File Offset: 0x0000689C
		internal SteamDeviceFormFactor GetSessionClientFormFactor(RemotePlaySessionID_t unSessionID)
		{
			return ISteamRemotePlay._GetSessionClientFormFactor(this.Self, unSessionID);
		}

		// Token: 0x06000516 RID: 1302
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemotePlay_BGetSessionClientResolution")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BGetSessionClientResolution(IntPtr self, RemotePlaySessionID_t unSessionID, ref int pnResolutionX, ref int pnResolutionY);

		// Token: 0x06000517 RID: 1303 RVA: 0x000086BC File Offset: 0x000068BC
		internal bool BGetSessionClientResolution(RemotePlaySessionID_t unSessionID, ref int pnResolutionX, ref int pnResolutionY)
		{
			return ISteamRemotePlay._BGetSessionClientResolution(this.Self, unSessionID, ref pnResolutionX, ref pnResolutionY);
		}

		// Token: 0x06000518 RID: 1304
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamRemotePlay_BSendRemotePlayTogetherInvite")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _BSendRemotePlayTogetherInvite(IntPtr self, SteamId steamIDFriend);

		// Token: 0x06000519 RID: 1305 RVA: 0x000086E0 File Offset: 0x000068E0
		internal bool BSendRemotePlayTogetherInvite(SteamId steamIDFriend)
		{
			return ISteamRemotePlay._BSendRemotePlayTogetherInvite(this.Self, steamIDFriend);
		}
	}
}
