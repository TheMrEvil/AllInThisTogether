using System;

namespace Steamworks
{
	// Token: 0x0200000A RID: 10
	public static class SteamGameServerNetworking
	{
		// Token: 0x06000124 RID: 292 RVA: 0x00004BC0 File Offset: 0x00002DC0
		public static bool SendP2PPacket(CSteamID steamIDRemote, byte[] pubData, uint cubData, EP2PSend eP2PSendType, int nChannel = 0)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_SendP2PPacket(CSteamGameServerAPIContext.GetSteamNetworking(), steamIDRemote, pubData, cubData, eP2PSendType, nChannel);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004BD7 File Offset: 0x00002DD7
		public static bool IsP2PPacketAvailable(out uint pcubMsgSize, int nChannel = 0)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_IsP2PPacketAvailable(CSteamGameServerAPIContext.GetSteamNetworking(), out pcubMsgSize, nChannel);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004BEA File Offset: 0x00002DEA
		public static bool ReadP2PPacket(byte[] pubDest, uint cubDest, out uint pcubMsgSize, out CSteamID psteamIDRemote, int nChannel = 0)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_ReadP2PPacket(CSteamGameServerAPIContext.GetSteamNetworking(), pubDest, cubDest, out pcubMsgSize, out psteamIDRemote, nChannel);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004C01 File Offset: 0x00002E01
		public static bool AcceptP2PSessionWithUser(CSteamID steamIDRemote)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_AcceptP2PSessionWithUser(CSteamGameServerAPIContext.GetSteamNetworking(), steamIDRemote);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004C13 File Offset: 0x00002E13
		public static bool CloseP2PSessionWithUser(CSteamID steamIDRemote)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_CloseP2PSessionWithUser(CSteamGameServerAPIContext.GetSteamNetworking(), steamIDRemote);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00004C25 File Offset: 0x00002E25
		public static bool CloseP2PChannelWithUser(CSteamID steamIDRemote, int nChannel)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_CloseP2PChannelWithUser(CSteamGameServerAPIContext.GetSteamNetworking(), steamIDRemote, nChannel);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00004C38 File Offset: 0x00002E38
		public static bool GetP2PSessionState(CSteamID steamIDRemote, out P2PSessionState_t pConnectionState)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_GetP2PSessionState(CSteamGameServerAPIContext.GetSteamNetworking(), steamIDRemote, out pConnectionState);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004C4B File Offset: 0x00002E4B
		public static bool AllowP2PPacketRelay(bool bAllow)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_AllowP2PPacketRelay(CSteamGameServerAPIContext.GetSteamNetworking(), bAllow);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00004C5D File Offset: 0x00002E5D
		public static SNetListenSocket_t CreateListenSocket(int nVirtualP2PPort, SteamIPAddress_t nIP, ushort nPort, bool bAllowUseOfPacketRelay)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SNetListenSocket_t)NativeMethods.ISteamNetworking_CreateListenSocket(CSteamGameServerAPIContext.GetSteamNetworking(), nVirtualP2PPort, nIP, nPort, bAllowUseOfPacketRelay);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004C77 File Offset: 0x00002E77
		public static SNetSocket_t CreateP2PConnectionSocket(CSteamID steamIDTarget, int nVirtualPort, int nTimeoutSec, bool bAllowUseOfPacketRelay)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SNetSocket_t)NativeMethods.ISteamNetworking_CreateP2PConnectionSocket(CSteamGameServerAPIContext.GetSteamNetworking(), steamIDTarget, nVirtualPort, nTimeoutSec, bAllowUseOfPacketRelay);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004C91 File Offset: 0x00002E91
		public static SNetSocket_t CreateConnectionSocket(SteamIPAddress_t nIP, ushort nPort, int nTimeoutSec)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SNetSocket_t)NativeMethods.ISteamNetworking_CreateConnectionSocket(CSteamGameServerAPIContext.GetSteamNetworking(), nIP, nPort, nTimeoutSec);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004CAA File Offset: 0x00002EAA
		public static bool DestroySocket(SNetSocket_t hSocket, bool bNotifyRemoteEnd)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_DestroySocket(CSteamGameServerAPIContext.GetSteamNetworking(), hSocket, bNotifyRemoteEnd);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004CBD File Offset: 0x00002EBD
		public static bool DestroyListenSocket(SNetListenSocket_t hSocket, bool bNotifyRemoteEnd)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_DestroyListenSocket(CSteamGameServerAPIContext.GetSteamNetworking(), hSocket, bNotifyRemoteEnd);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004CD0 File Offset: 0x00002ED0
		public static bool SendDataOnSocket(SNetSocket_t hSocket, byte[] pubData, uint cubData, bool bReliable)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_SendDataOnSocket(CSteamGameServerAPIContext.GetSteamNetworking(), hSocket, pubData, cubData, bReliable);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004CE5 File Offset: 0x00002EE5
		public static bool IsDataAvailableOnSocket(SNetSocket_t hSocket, out uint pcubMsgSize)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_IsDataAvailableOnSocket(CSteamGameServerAPIContext.GetSteamNetworking(), hSocket, out pcubMsgSize);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004CF8 File Offset: 0x00002EF8
		public static bool RetrieveDataFromSocket(SNetSocket_t hSocket, byte[] pubDest, uint cubDest, out uint pcubMsgSize)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_RetrieveDataFromSocket(CSteamGameServerAPIContext.GetSteamNetworking(), hSocket, pubDest, cubDest, out pcubMsgSize);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004D0D File Offset: 0x00002F0D
		public static bool IsDataAvailable(SNetListenSocket_t hListenSocket, out uint pcubMsgSize, out SNetSocket_t phSocket)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_IsDataAvailable(CSteamGameServerAPIContext.GetSteamNetworking(), hListenSocket, out pcubMsgSize, out phSocket);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004D21 File Offset: 0x00002F21
		public static bool RetrieveData(SNetListenSocket_t hListenSocket, byte[] pubDest, uint cubDest, out uint pcubMsgSize, out SNetSocket_t phSocket)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_RetrieveData(CSteamGameServerAPIContext.GetSteamNetworking(), hListenSocket, pubDest, cubDest, out pcubMsgSize, out phSocket);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004D38 File Offset: 0x00002F38
		public static bool GetSocketInfo(SNetSocket_t hSocket, out CSteamID pSteamIDRemote, out int peSocketStatus, out SteamIPAddress_t punIPRemote, out ushort punPortRemote)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_GetSocketInfo(CSteamGameServerAPIContext.GetSteamNetworking(), hSocket, out pSteamIDRemote, out peSocketStatus, out punIPRemote, out punPortRemote);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004D4F File Offset: 0x00002F4F
		public static bool GetListenSocketInfo(SNetListenSocket_t hListenSocket, out SteamIPAddress_t pnIP, out ushort pnPort)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_GetListenSocketInfo(CSteamGameServerAPIContext.GetSteamNetworking(), hListenSocket, out pnIP, out pnPort);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004D63 File Offset: 0x00002F63
		public static ESNetSocketConnectionType GetSocketConnectionType(SNetSocket_t hSocket)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_GetSocketConnectionType(CSteamGameServerAPIContext.GetSteamNetworking(), hSocket);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004D75 File Offset: 0x00002F75
		public static int GetMaxPacketSize(SNetSocket_t hSocket)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworking_GetMaxPacketSize(CSteamGameServerAPIContext.GetSteamNetworking(), hSocket);
		}
	}
}
