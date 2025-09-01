using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x02000021 RID: 33
	internal class ISteamNetworking : SteamInterface
	{
		// Token: 0x0600042C RID: 1068 RVA: 0x000077AF File Offset: 0x000059AF
		internal ISteamNetworking(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x0600042D RID: 1069
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamNetworking_v006();

		// Token: 0x0600042E RID: 1070 RVA: 0x000077C1 File Offset: 0x000059C1
		public override IntPtr GetUserInterfacePointer()
		{
			return ISteamNetworking.SteamAPI_SteamNetworking_v006();
		}

		// Token: 0x0600042F RID: 1071
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamGameServerNetworking_v006();

		// Token: 0x06000430 RID: 1072 RVA: 0x000077C8 File Offset: 0x000059C8
		public override IntPtr GetServerInterfacePointer()
		{
			return ISteamNetworking.SteamAPI_SteamGameServerNetworking_v006();
		}

		// Token: 0x06000431 RID: 1073
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_SendP2PPacket")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SendP2PPacket(IntPtr self, SteamId steamIDRemote, IntPtr pubData, uint cubData, P2PSend eP2PSendType, int nChannel);

		// Token: 0x06000432 RID: 1074 RVA: 0x000077D0 File Offset: 0x000059D0
		internal bool SendP2PPacket(SteamId steamIDRemote, IntPtr pubData, uint cubData, P2PSend eP2PSendType, int nChannel)
		{
			return ISteamNetworking._SendP2PPacket(this.Self, steamIDRemote, pubData, cubData, eP2PSendType, nChannel);
		}

		// Token: 0x06000433 RID: 1075
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_IsP2PPacketAvailable")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _IsP2PPacketAvailable(IntPtr self, ref uint pcubMsgSize, int nChannel);

		// Token: 0x06000434 RID: 1076 RVA: 0x000077F8 File Offset: 0x000059F8
		internal bool IsP2PPacketAvailable(ref uint pcubMsgSize, int nChannel)
		{
			return ISteamNetworking._IsP2PPacketAvailable(this.Self, ref pcubMsgSize, nChannel);
		}

		// Token: 0x06000435 RID: 1077
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_ReadP2PPacket")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _ReadP2PPacket(IntPtr self, IntPtr pubDest, uint cubDest, ref uint pcubMsgSize, ref SteamId psteamIDRemote, int nChannel);

		// Token: 0x06000436 RID: 1078 RVA: 0x0000781C File Offset: 0x00005A1C
		internal bool ReadP2PPacket(IntPtr pubDest, uint cubDest, ref uint pcubMsgSize, ref SteamId psteamIDRemote, int nChannel)
		{
			return ISteamNetworking._ReadP2PPacket(this.Self, pubDest, cubDest, ref pcubMsgSize, ref psteamIDRemote, nChannel);
		}

		// Token: 0x06000437 RID: 1079
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_AcceptP2PSessionWithUser")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _AcceptP2PSessionWithUser(IntPtr self, SteamId steamIDRemote);

		// Token: 0x06000438 RID: 1080 RVA: 0x00007844 File Offset: 0x00005A44
		internal bool AcceptP2PSessionWithUser(SteamId steamIDRemote)
		{
			return ISteamNetworking._AcceptP2PSessionWithUser(this.Self, steamIDRemote);
		}

		// Token: 0x06000439 RID: 1081
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_CloseP2PSessionWithUser")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _CloseP2PSessionWithUser(IntPtr self, SteamId steamIDRemote);

		// Token: 0x0600043A RID: 1082 RVA: 0x00007864 File Offset: 0x00005A64
		internal bool CloseP2PSessionWithUser(SteamId steamIDRemote)
		{
			return ISteamNetworking._CloseP2PSessionWithUser(this.Self, steamIDRemote);
		}

		// Token: 0x0600043B RID: 1083
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_CloseP2PChannelWithUser")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _CloseP2PChannelWithUser(IntPtr self, SteamId steamIDRemote, int nChannel);

		// Token: 0x0600043C RID: 1084 RVA: 0x00007884 File Offset: 0x00005A84
		internal bool CloseP2PChannelWithUser(SteamId steamIDRemote, int nChannel)
		{
			return ISteamNetworking._CloseP2PChannelWithUser(this.Self, steamIDRemote, nChannel);
		}

		// Token: 0x0600043D RID: 1085
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_GetP2PSessionState")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetP2PSessionState(IntPtr self, SteamId steamIDRemote, ref P2PSessionState_t pConnectionState);

		// Token: 0x0600043E RID: 1086 RVA: 0x000078A8 File Offset: 0x00005AA8
		internal bool GetP2PSessionState(SteamId steamIDRemote, ref P2PSessionState_t pConnectionState)
		{
			return ISteamNetworking._GetP2PSessionState(this.Self, steamIDRemote, ref pConnectionState);
		}

		// Token: 0x0600043F RID: 1087
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_AllowP2PPacketRelay")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _AllowP2PPacketRelay(IntPtr self, [MarshalAs(UnmanagedType.U1)] bool bAllow);

		// Token: 0x06000440 RID: 1088 RVA: 0x000078CC File Offset: 0x00005ACC
		internal bool AllowP2PPacketRelay([MarshalAs(UnmanagedType.U1)] bool bAllow)
		{
			return ISteamNetworking._AllowP2PPacketRelay(this.Self, bAllow);
		}

		// Token: 0x06000441 RID: 1089
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworking_CreateP2PConnectionSocket")]
		private static extern SNetSocket_t _CreateP2PConnectionSocket(IntPtr self, SteamId steamIDTarget, int nVirtualPort, int nTimeoutSec, [MarshalAs(UnmanagedType.U1)] bool bAllowUseOfPacketRelay);

		// Token: 0x06000442 RID: 1090 RVA: 0x000078EC File Offset: 0x00005AEC
		internal SNetSocket_t CreateP2PConnectionSocket(SteamId steamIDTarget, int nVirtualPort, int nTimeoutSec, [MarshalAs(UnmanagedType.U1)] bool bAllowUseOfPacketRelay)
		{
			return ISteamNetworking._CreateP2PConnectionSocket(this.Self, steamIDTarget, nVirtualPort, nTimeoutSec, bAllowUseOfPacketRelay);
		}
	}
}
