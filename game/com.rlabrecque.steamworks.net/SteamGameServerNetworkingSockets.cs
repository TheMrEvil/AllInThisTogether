using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200000B RID: 11
	public static class SteamGameServerNetworkingSockets
	{
		// Token: 0x0600013A RID: 314 RVA: 0x00004D87 File Offset: 0x00002F87
		public static HSteamListenSocket CreateListenSocketIP(ref SteamNetworkingIPAddr localAddress, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (HSteamListenSocket)NativeMethods.ISteamNetworkingSockets_CreateListenSocketIP(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), ref localAddress, nOptions, pOptions);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00004DA0 File Offset: 0x00002FA0
		public static HSteamNetConnection ConnectByIPAddress(ref SteamNetworkingIPAddr address, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (HSteamNetConnection)NativeMethods.ISteamNetworkingSockets_ConnectByIPAddress(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), ref address, nOptions, pOptions);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00004DB9 File Offset: 0x00002FB9
		public static HSteamListenSocket CreateListenSocketP2P(int nLocalVirtualPort, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (HSteamListenSocket)NativeMethods.ISteamNetworkingSockets_CreateListenSocketP2P(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), nLocalVirtualPort, nOptions, pOptions);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004DD2 File Offset: 0x00002FD2
		public static HSteamNetConnection ConnectP2P(ref SteamNetworkingIdentity identityRemote, int nRemoteVirtualPort, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (HSteamNetConnection)NativeMethods.ISteamNetworkingSockets_ConnectP2P(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), ref identityRemote, nRemoteVirtualPort, nOptions, pOptions);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004DEC File Offset: 0x00002FEC
		public static EResult AcceptConnection(HSteamNetConnection hConn)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_AcceptConnection(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hConn);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004E00 File Offset: 0x00003000
		public static bool CloseConnection(HSteamNetConnection hPeer, int nReason, string pszDebug, bool bEnableLinger)
		{
			InteropHelp.TestIfAvailableGameServer();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszDebug))
			{
				result = NativeMethods.ISteamNetworkingSockets_CloseConnection(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hPeer, nReason, utf8StringHandle, bEnableLinger);
			}
			return result;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004E48 File Offset: 0x00003048
		public static bool CloseListenSocket(HSteamListenSocket hSocket)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_CloseListenSocket(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hSocket);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004E5A File Offset: 0x0000305A
		public static bool SetConnectionUserData(HSteamNetConnection hPeer, long nUserData)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_SetConnectionUserData(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hPeer, nUserData);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004E6D File Offset: 0x0000306D
		public static long GetConnectionUserData(HSteamNetConnection hPeer)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_GetConnectionUserData(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hPeer);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004E80 File Offset: 0x00003080
		public static void SetConnectionName(HSteamNetConnection hPeer, string pszName)
		{
			InteropHelp.TestIfAvailableGameServer();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszName))
			{
				NativeMethods.ISteamNetworkingSockets_SetConnectionName(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hPeer, utf8StringHandle);
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004EC4 File Offset: 0x000030C4
		public static bool GetConnectionName(HSteamNetConnection hPeer, out string pszName, int nMaxLen)
		{
			InteropHelp.TestIfAvailableGameServer();
			IntPtr intPtr = Marshal.AllocHGlobal(nMaxLen);
			bool flag = NativeMethods.ISteamNetworkingSockets_GetConnectionName(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hPeer, intPtr, nMaxLen);
			pszName = (flag ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004F00 File Offset: 0x00003100
		public static EResult SendMessageToConnection(HSteamNetConnection hConn, IntPtr pData, uint cbData, int nSendFlags, out long pOutMessageNumber)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_SendMessageToConnection(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hConn, pData, cbData, nSendFlags, out pOutMessageNumber);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00004F17 File Offset: 0x00003117
		public static void SendMessages(int nMessages, SteamNetworkingMessage_t[] pMessages, long[] pOutMessageNumberOrResult)
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamNetworkingSockets_SendMessages(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), nMessages, pMessages, pOutMessageNumberOrResult);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004F2B File Offset: 0x0000312B
		public static EResult FlushMessagesOnConnection(HSteamNetConnection hConn)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_FlushMessagesOnConnection(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hConn);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004F3D File Offset: 0x0000313D
		public static int ReceiveMessagesOnConnection(HSteamNetConnection hConn, IntPtr[] ppOutMessages, int nMaxMessages)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_ReceiveMessagesOnConnection(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hConn, ppOutMessages, nMaxMessages);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004F51 File Offset: 0x00003151
		public static bool GetConnectionInfo(HSteamNetConnection hConn, out SteamNetConnectionInfo_t pInfo)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_GetConnectionInfo(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hConn, out pInfo);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004F64 File Offset: 0x00003164
		public static EResult GetConnectionRealTimeStatus(HSteamNetConnection hConn, ref SteamNetConnectionRealTimeStatus_t pStatus, int nLanes, ref SteamNetConnectionRealTimeLaneStatus_t pLanes)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_GetConnectionRealTimeStatus(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hConn, ref pStatus, nLanes, ref pLanes);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004F7C File Offset: 0x0000317C
		public static int GetDetailedConnectionStatus(HSteamNetConnection hConn, out string pszBuf, int cbBuf)
		{
			InteropHelp.TestIfAvailableGameServer();
			IntPtr intPtr = Marshal.AllocHGlobal(cbBuf);
			int num = NativeMethods.ISteamNetworkingSockets_GetDetailedConnectionStatus(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hConn, intPtr, cbBuf);
			pszBuf = ((num != -1) ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return num;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004FB9 File Offset: 0x000031B9
		public static bool GetListenSocketAddress(HSteamListenSocket hSocket, out SteamNetworkingIPAddr address)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_GetListenSocketAddress(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hSocket, out address);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004FCC File Offset: 0x000031CC
		public static bool CreateSocketPair(out HSteamNetConnection pOutConnection1, out HSteamNetConnection pOutConnection2, bool bUseNetworkLoopback, ref SteamNetworkingIdentity pIdentity1, ref SteamNetworkingIdentity pIdentity2)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_CreateSocketPair(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), out pOutConnection1, out pOutConnection2, bUseNetworkLoopback, ref pIdentity1, ref pIdentity2);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004FE3 File Offset: 0x000031E3
		public static EResult ConfigureConnectionLanes(HSteamNetConnection hConn, int nNumLanes, out int pLanePriorities, out ushort pLaneWeights)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_ConfigureConnectionLanes(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hConn, nNumLanes, out pLanePriorities, out pLaneWeights);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00004FF8 File Offset: 0x000031F8
		public static bool GetIdentity(out SteamNetworkingIdentity pIdentity)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_GetIdentity(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), out pIdentity);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000500A File Offset: 0x0000320A
		public static ESteamNetworkingAvailability InitAuthentication()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_InitAuthentication(CSteamGameServerAPIContext.GetSteamNetworkingSockets());
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000501B File Offset: 0x0000321B
		public static ESteamNetworkingAvailability GetAuthenticationStatus(out SteamNetAuthenticationStatus_t pDetails)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_GetAuthenticationStatus(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), out pDetails);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000502D File Offset: 0x0000322D
		public static HSteamNetPollGroup CreatePollGroup()
		{
			InteropHelp.TestIfAvailableGameServer();
			return (HSteamNetPollGroup)NativeMethods.ISteamNetworkingSockets_CreatePollGroup(CSteamGameServerAPIContext.GetSteamNetworkingSockets());
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005043 File Offset: 0x00003243
		public static bool DestroyPollGroup(HSteamNetPollGroup hPollGroup)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_DestroyPollGroup(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hPollGroup);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005055 File Offset: 0x00003255
		public static bool SetConnectionPollGroup(HSteamNetConnection hConn, HSteamNetPollGroup hPollGroup)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_SetConnectionPollGroup(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hConn, hPollGroup);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005068 File Offset: 0x00003268
		public static int ReceiveMessagesOnPollGroup(HSteamNetPollGroup hPollGroup, IntPtr[] ppOutMessages, int nMaxMessages)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_ReceiveMessagesOnPollGroup(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hPollGroup, ppOutMessages, nMaxMessages);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000507C File Offset: 0x0000327C
		public static bool ReceivedRelayAuthTicket(IntPtr pvTicket, int cbTicket, out SteamDatagramRelayAuthTicket pOutParsedTicket)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_ReceivedRelayAuthTicket(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), pvTicket, cbTicket, out pOutParsedTicket);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005090 File Offset: 0x00003290
		public static int FindRelayAuthTicketForServer(ref SteamNetworkingIdentity identityGameServer, int nRemoteVirtualPort, out SteamDatagramRelayAuthTicket pOutParsedTicket)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_FindRelayAuthTicketForServer(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), ref identityGameServer, nRemoteVirtualPort, out pOutParsedTicket);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000050A4 File Offset: 0x000032A4
		public static HSteamNetConnection ConnectToHostedDedicatedServer(ref SteamNetworkingIdentity identityTarget, int nRemoteVirtualPort, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (HSteamNetConnection)NativeMethods.ISteamNetworkingSockets_ConnectToHostedDedicatedServer(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), ref identityTarget, nRemoteVirtualPort, nOptions, pOptions);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000050BE File Offset: 0x000032BE
		public static ushort GetHostedDedicatedServerPort()
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_GetHostedDedicatedServerPort(CSteamGameServerAPIContext.GetSteamNetworkingSockets());
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000050CF File Offset: 0x000032CF
		public static SteamNetworkingPOPID GetHostedDedicatedServerPOPID()
		{
			InteropHelp.TestIfAvailableGameServer();
			return (SteamNetworkingPOPID)NativeMethods.ISteamNetworkingSockets_GetHostedDedicatedServerPOPID(CSteamGameServerAPIContext.GetSteamNetworkingSockets());
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000050E5 File Offset: 0x000032E5
		public static EResult GetHostedDedicatedServerAddress(out SteamDatagramHostedAddress pRouting)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_GetHostedDedicatedServerAddress(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), out pRouting);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000050F7 File Offset: 0x000032F7
		public static HSteamListenSocket CreateHostedDedicatedServerListenSocket(int nLocalVirtualPort, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (HSteamListenSocket)NativeMethods.ISteamNetworkingSockets_CreateHostedDedicatedServerListenSocket(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), nLocalVirtualPort, nOptions, pOptions);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005110 File Offset: 0x00003310
		public static EResult GetGameCoordinatorServerLogin(IntPtr pLoginInfo, out int pcbSignedBlob, IntPtr pBlob)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_GetGameCoordinatorServerLogin(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), pLoginInfo, out pcbSignedBlob, pBlob);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005124 File Offset: 0x00003324
		public static HSteamNetConnection ConnectP2PCustomSignaling(out ISteamNetworkingConnectionSignaling pSignaling, ref SteamNetworkingIdentity pPeerIdentity, int nRemoteVirtualPort, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (HSteamNetConnection)NativeMethods.ISteamNetworkingSockets_ConnectP2PCustomSignaling(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), out pSignaling, ref pPeerIdentity, nRemoteVirtualPort, nOptions, pOptions);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005140 File Offset: 0x00003340
		public static bool ReceivedP2PCustomSignal(IntPtr pMsg, int cbMsg, out ISteamNetworkingSignalingRecvContext pContext)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_ReceivedP2PCustomSignal(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), pMsg, cbMsg, out pContext);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00005154 File Offset: 0x00003354
		public static bool GetCertificateRequest(out int pcbBlob, IntPtr pBlob, out SteamNetworkingErrMsg errMsg)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_GetCertificateRequest(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), out pcbBlob, pBlob, out errMsg);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00005168 File Offset: 0x00003368
		public static bool SetCertificate(IntPtr pCertificate, int cbCertificate, out SteamNetworkingErrMsg errMsg)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_SetCertificate(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), pCertificate, cbCertificate, out errMsg);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000517C File Offset: 0x0000337C
		public static void ResetIdentity(ref SteamNetworkingIdentity pIdentity)
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamNetworkingSockets_ResetIdentity(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), ref pIdentity);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000518E File Offset: 0x0000338E
		public static void RunCallbacks()
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamNetworkingSockets_RunCallbacks(CSteamGameServerAPIContext.GetSteamNetworkingSockets());
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000519F File Offset: 0x0000339F
		public static bool BeginAsyncRequestFakeIP(int nNumPorts)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_BeginAsyncRequestFakeIP(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), nNumPorts);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000051B1 File Offset: 0x000033B1
		public static void GetFakeIP(int idxFirstPort, out SteamNetworkingFakeIPResult_t pInfo)
		{
			InteropHelp.TestIfAvailableGameServer();
			NativeMethods.ISteamNetworkingSockets_GetFakeIP(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), idxFirstPort, out pInfo);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000051C4 File Offset: 0x000033C4
		public static HSteamListenSocket CreateListenSocketP2PFakeIP(int idxFakePort, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableGameServer();
			return (HSteamListenSocket)NativeMethods.ISteamNetworkingSockets_CreateListenSocketP2PFakeIP(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), idxFakePort, nOptions, pOptions);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000051DD File Offset: 0x000033DD
		public static EResult GetRemoteFakeIPForConnection(HSteamNetConnection hConn, out SteamNetworkingIPAddr pOutAddr)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_GetRemoteFakeIPForConnection(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), hConn, out pOutAddr);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000051F0 File Offset: 0x000033F0
		public static IntPtr CreateFakeUDPPort(int idxFakeServerPort)
		{
			InteropHelp.TestIfAvailableGameServer();
			return NativeMethods.ISteamNetworkingSockets_CreateFakeUDPPort(CSteamGameServerAPIContext.GetSteamNetworkingSockets(), idxFakeServerPort);
		}
	}
}
