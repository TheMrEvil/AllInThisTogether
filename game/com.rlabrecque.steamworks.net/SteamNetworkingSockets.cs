using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200001C RID: 28
	public static class SteamNetworkingSockets
	{
		// Token: 0x06000335 RID: 821 RVA: 0x00008C40 File Offset: 0x00006E40
		public static HSteamListenSocket CreateListenSocketIP(ref SteamNetworkingIPAddr localAddress, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableClient();
			return (HSteamListenSocket)NativeMethods.ISteamNetworkingSockets_CreateListenSocketIP(CSteamAPIContext.GetSteamNetworkingSockets(), ref localAddress, nOptions, pOptions);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00008C59 File Offset: 0x00006E59
		public static HSteamNetConnection ConnectByIPAddress(ref SteamNetworkingIPAddr address, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableClient();
			return (HSteamNetConnection)NativeMethods.ISteamNetworkingSockets_ConnectByIPAddress(CSteamAPIContext.GetSteamNetworkingSockets(), ref address, nOptions, pOptions);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00008C72 File Offset: 0x00006E72
		public static HSteamListenSocket CreateListenSocketP2P(int nLocalVirtualPort, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableClient();
			return (HSteamListenSocket)NativeMethods.ISteamNetworkingSockets_CreateListenSocketP2P(CSteamAPIContext.GetSteamNetworkingSockets(), nLocalVirtualPort, nOptions, pOptions);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00008C8B File Offset: 0x00006E8B
		public static HSteamNetConnection ConnectP2P(ref SteamNetworkingIdentity identityRemote, int nRemoteVirtualPort, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableClient();
			return (HSteamNetConnection)NativeMethods.ISteamNetworkingSockets_ConnectP2P(CSteamAPIContext.GetSteamNetworkingSockets(), ref identityRemote, nRemoteVirtualPort, nOptions, pOptions);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00008CA5 File Offset: 0x00006EA5
		public static EResult AcceptConnection(HSteamNetConnection hConn)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_AcceptConnection(CSteamAPIContext.GetSteamNetworkingSockets(), hConn);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00008CB8 File Offset: 0x00006EB8
		public static bool CloseConnection(HSteamNetConnection hPeer, int nReason, string pszDebug, bool bEnableLinger)
		{
			InteropHelp.TestIfAvailableClient();
			bool result;
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszDebug))
			{
				result = NativeMethods.ISteamNetworkingSockets_CloseConnection(CSteamAPIContext.GetSteamNetworkingSockets(), hPeer, nReason, utf8StringHandle, bEnableLinger);
			}
			return result;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00008D00 File Offset: 0x00006F00
		public static bool CloseListenSocket(HSteamListenSocket hSocket)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_CloseListenSocket(CSteamAPIContext.GetSteamNetworkingSockets(), hSocket);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00008D12 File Offset: 0x00006F12
		public static bool SetConnectionUserData(HSteamNetConnection hPeer, long nUserData)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_SetConnectionUserData(CSteamAPIContext.GetSteamNetworkingSockets(), hPeer, nUserData);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00008D25 File Offset: 0x00006F25
		public static long GetConnectionUserData(HSteamNetConnection hPeer)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_GetConnectionUserData(CSteamAPIContext.GetSteamNetworkingSockets(), hPeer);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00008D38 File Offset: 0x00006F38
		public static void SetConnectionName(HSteamNetConnection hPeer, string pszName)
		{
			InteropHelp.TestIfAvailableClient();
			using (InteropHelp.UTF8StringHandle utf8StringHandle = new InteropHelp.UTF8StringHandle(pszName))
			{
				NativeMethods.ISteamNetworkingSockets_SetConnectionName(CSteamAPIContext.GetSteamNetworkingSockets(), hPeer, utf8StringHandle);
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00008D7C File Offset: 0x00006F7C
		public static bool GetConnectionName(HSteamNetConnection hPeer, out string pszName, int nMaxLen)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(nMaxLen);
			bool flag = NativeMethods.ISteamNetworkingSockets_GetConnectionName(CSteamAPIContext.GetSteamNetworkingSockets(), hPeer, intPtr, nMaxLen);
			pszName = (flag ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return flag;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00008DB8 File Offset: 0x00006FB8
		public static EResult SendMessageToConnection(HSteamNetConnection hConn, IntPtr pData, uint cbData, int nSendFlags, out long pOutMessageNumber)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_SendMessageToConnection(CSteamAPIContext.GetSteamNetworkingSockets(), hConn, pData, cbData, nSendFlags, out pOutMessageNumber);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00008DCF File Offset: 0x00006FCF
		public static void SendMessages(int nMessages, SteamNetworkingMessage_t[] pMessages, long[] pOutMessageNumberOrResult)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamNetworkingSockets_SendMessages(CSteamAPIContext.GetSteamNetworkingSockets(), nMessages, pMessages, pOutMessageNumberOrResult);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00008DE3 File Offset: 0x00006FE3
		public static EResult FlushMessagesOnConnection(HSteamNetConnection hConn)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_FlushMessagesOnConnection(CSteamAPIContext.GetSteamNetworkingSockets(), hConn);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00008DF5 File Offset: 0x00006FF5
		public static int ReceiveMessagesOnConnection(HSteamNetConnection hConn, IntPtr[] ppOutMessages, int nMaxMessages)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_ReceiveMessagesOnConnection(CSteamAPIContext.GetSteamNetworkingSockets(), hConn, ppOutMessages, nMaxMessages);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00008E09 File Offset: 0x00007009
		public static bool GetConnectionInfo(HSteamNetConnection hConn, out SteamNetConnectionInfo_t pInfo)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_GetConnectionInfo(CSteamAPIContext.GetSteamNetworkingSockets(), hConn, out pInfo);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00008E1C File Offset: 0x0000701C
		public static EResult GetConnectionRealTimeStatus(HSteamNetConnection hConn, ref SteamNetConnectionRealTimeStatus_t pStatus, int nLanes, ref SteamNetConnectionRealTimeLaneStatus_t pLanes)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_GetConnectionRealTimeStatus(CSteamAPIContext.GetSteamNetworkingSockets(), hConn, ref pStatus, nLanes, ref pLanes);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00008E34 File Offset: 0x00007034
		public static int GetDetailedConnectionStatus(HSteamNetConnection hConn, out string pszBuf, int cbBuf)
		{
			InteropHelp.TestIfAvailableClient();
			IntPtr intPtr = Marshal.AllocHGlobal(cbBuf);
			int num = NativeMethods.ISteamNetworkingSockets_GetDetailedConnectionStatus(CSteamAPIContext.GetSteamNetworkingSockets(), hConn, intPtr, cbBuf);
			pszBuf = ((num != -1) ? InteropHelp.PtrToStringUTF8(intPtr) : null);
			Marshal.FreeHGlobal(intPtr);
			return num;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00008E71 File Offset: 0x00007071
		public static bool GetListenSocketAddress(HSteamListenSocket hSocket, out SteamNetworkingIPAddr address)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_GetListenSocketAddress(CSteamAPIContext.GetSteamNetworkingSockets(), hSocket, out address);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00008E84 File Offset: 0x00007084
		public static bool CreateSocketPair(out HSteamNetConnection pOutConnection1, out HSteamNetConnection pOutConnection2, bool bUseNetworkLoopback, ref SteamNetworkingIdentity pIdentity1, ref SteamNetworkingIdentity pIdentity2)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_CreateSocketPair(CSteamAPIContext.GetSteamNetworkingSockets(), out pOutConnection1, out pOutConnection2, bUseNetworkLoopback, ref pIdentity1, ref pIdentity2);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00008E9B File Offset: 0x0000709B
		public static EResult ConfigureConnectionLanes(HSteamNetConnection hConn, int nNumLanes, out int pLanePriorities, out ushort pLaneWeights)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_ConfigureConnectionLanes(CSteamAPIContext.GetSteamNetworkingSockets(), hConn, nNumLanes, out pLanePriorities, out pLaneWeights);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00008EB0 File Offset: 0x000070B0
		public static bool GetIdentity(out SteamNetworkingIdentity pIdentity)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_GetIdentity(CSteamAPIContext.GetSteamNetworkingSockets(), out pIdentity);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00008EC2 File Offset: 0x000070C2
		public static ESteamNetworkingAvailability InitAuthentication()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_InitAuthentication(CSteamAPIContext.GetSteamNetworkingSockets());
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00008ED3 File Offset: 0x000070D3
		public static ESteamNetworkingAvailability GetAuthenticationStatus(out SteamNetAuthenticationStatus_t pDetails)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_GetAuthenticationStatus(CSteamAPIContext.GetSteamNetworkingSockets(), out pDetails);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00008EE5 File Offset: 0x000070E5
		public static HSteamNetPollGroup CreatePollGroup()
		{
			InteropHelp.TestIfAvailableClient();
			return (HSteamNetPollGroup)NativeMethods.ISteamNetworkingSockets_CreatePollGroup(CSteamAPIContext.GetSteamNetworkingSockets());
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00008EFB File Offset: 0x000070FB
		public static bool DestroyPollGroup(HSteamNetPollGroup hPollGroup)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_DestroyPollGroup(CSteamAPIContext.GetSteamNetworkingSockets(), hPollGroup);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00008F0D File Offset: 0x0000710D
		public static bool SetConnectionPollGroup(HSteamNetConnection hConn, HSteamNetPollGroup hPollGroup)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_SetConnectionPollGroup(CSteamAPIContext.GetSteamNetworkingSockets(), hConn, hPollGroup);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00008F20 File Offset: 0x00007120
		public static int ReceiveMessagesOnPollGroup(HSteamNetPollGroup hPollGroup, IntPtr[] ppOutMessages, int nMaxMessages)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_ReceiveMessagesOnPollGroup(CSteamAPIContext.GetSteamNetworkingSockets(), hPollGroup, ppOutMessages, nMaxMessages);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00008F34 File Offset: 0x00007134
		public static bool ReceivedRelayAuthTicket(IntPtr pvTicket, int cbTicket, out SteamDatagramRelayAuthTicket pOutParsedTicket)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_ReceivedRelayAuthTicket(CSteamAPIContext.GetSteamNetworkingSockets(), pvTicket, cbTicket, out pOutParsedTicket);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00008F48 File Offset: 0x00007148
		public static int FindRelayAuthTicketForServer(ref SteamNetworkingIdentity identityGameServer, int nRemoteVirtualPort, out SteamDatagramRelayAuthTicket pOutParsedTicket)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_FindRelayAuthTicketForServer(CSteamAPIContext.GetSteamNetworkingSockets(), ref identityGameServer, nRemoteVirtualPort, out pOutParsedTicket);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00008F5C File Offset: 0x0000715C
		public static HSteamNetConnection ConnectToHostedDedicatedServer(ref SteamNetworkingIdentity identityTarget, int nRemoteVirtualPort, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableClient();
			return (HSteamNetConnection)NativeMethods.ISteamNetworkingSockets_ConnectToHostedDedicatedServer(CSteamAPIContext.GetSteamNetworkingSockets(), ref identityTarget, nRemoteVirtualPort, nOptions, pOptions);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00008F76 File Offset: 0x00007176
		public static ushort GetHostedDedicatedServerPort()
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_GetHostedDedicatedServerPort(CSteamAPIContext.GetSteamNetworkingSockets());
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00008F87 File Offset: 0x00007187
		public static SteamNetworkingPOPID GetHostedDedicatedServerPOPID()
		{
			InteropHelp.TestIfAvailableClient();
			return (SteamNetworkingPOPID)NativeMethods.ISteamNetworkingSockets_GetHostedDedicatedServerPOPID(CSteamAPIContext.GetSteamNetworkingSockets());
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00008F9D File Offset: 0x0000719D
		public static EResult GetHostedDedicatedServerAddress(out SteamDatagramHostedAddress pRouting)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_GetHostedDedicatedServerAddress(CSteamAPIContext.GetSteamNetworkingSockets(), out pRouting);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00008FAF File Offset: 0x000071AF
		public static HSteamListenSocket CreateHostedDedicatedServerListenSocket(int nLocalVirtualPort, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableClient();
			return (HSteamListenSocket)NativeMethods.ISteamNetworkingSockets_CreateHostedDedicatedServerListenSocket(CSteamAPIContext.GetSteamNetworkingSockets(), nLocalVirtualPort, nOptions, pOptions);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00008FC8 File Offset: 0x000071C8
		public static EResult GetGameCoordinatorServerLogin(IntPtr pLoginInfo, out int pcbSignedBlob, IntPtr pBlob)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_GetGameCoordinatorServerLogin(CSteamAPIContext.GetSteamNetworkingSockets(), pLoginInfo, out pcbSignedBlob, pBlob);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00008FDC File Offset: 0x000071DC
		public static HSteamNetConnection ConnectP2PCustomSignaling(out ISteamNetworkingConnectionSignaling pSignaling, ref SteamNetworkingIdentity pPeerIdentity, int nRemoteVirtualPort, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableClient();
			return (HSteamNetConnection)NativeMethods.ISteamNetworkingSockets_ConnectP2PCustomSignaling(CSteamAPIContext.GetSteamNetworkingSockets(), out pSignaling, ref pPeerIdentity, nRemoteVirtualPort, nOptions, pOptions);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00008FF8 File Offset: 0x000071F8
		public static bool ReceivedP2PCustomSignal(IntPtr pMsg, int cbMsg, out ISteamNetworkingSignalingRecvContext pContext)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_ReceivedP2PCustomSignal(CSteamAPIContext.GetSteamNetworkingSockets(), pMsg, cbMsg, out pContext);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000900C File Offset: 0x0000720C
		public static bool GetCertificateRequest(out int pcbBlob, IntPtr pBlob, out SteamNetworkingErrMsg errMsg)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_GetCertificateRequest(CSteamAPIContext.GetSteamNetworkingSockets(), out pcbBlob, pBlob, out errMsg);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00009020 File Offset: 0x00007220
		public static bool SetCertificate(IntPtr pCertificate, int cbCertificate, out SteamNetworkingErrMsg errMsg)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_SetCertificate(CSteamAPIContext.GetSteamNetworkingSockets(), pCertificate, cbCertificate, out errMsg);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00009034 File Offset: 0x00007234
		public static void ResetIdentity(ref SteamNetworkingIdentity pIdentity)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamNetworkingSockets_ResetIdentity(CSteamAPIContext.GetSteamNetworkingSockets(), ref pIdentity);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00009046 File Offset: 0x00007246
		public static void RunCallbacks()
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamNetworkingSockets_RunCallbacks(CSteamAPIContext.GetSteamNetworkingSockets());
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00009057 File Offset: 0x00007257
		public static bool BeginAsyncRequestFakeIP(int nNumPorts)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_BeginAsyncRequestFakeIP(CSteamAPIContext.GetSteamNetworkingSockets(), nNumPorts);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00009069 File Offset: 0x00007269
		public static void GetFakeIP(int idxFirstPort, out SteamNetworkingFakeIPResult_t pInfo)
		{
			InteropHelp.TestIfAvailableClient();
			NativeMethods.ISteamNetworkingSockets_GetFakeIP(CSteamAPIContext.GetSteamNetworkingSockets(), idxFirstPort, out pInfo);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000907C File Offset: 0x0000727C
		public static HSteamListenSocket CreateListenSocketP2PFakeIP(int idxFakePort, int nOptions, SteamNetworkingConfigValue_t[] pOptions)
		{
			InteropHelp.TestIfAvailableClient();
			return (HSteamListenSocket)NativeMethods.ISteamNetworkingSockets_CreateListenSocketP2PFakeIP(CSteamAPIContext.GetSteamNetworkingSockets(), idxFakePort, nOptions, pOptions);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00009095 File Offset: 0x00007295
		public static EResult GetRemoteFakeIPForConnection(HSteamNetConnection hConn, out SteamNetworkingIPAddr pOutAddr)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_GetRemoteFakeIPForConnection(CSteamAPIContext.GetSteamNetworkingSockets(), hConn, out pOutAddr);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x000090A8 File Offset: 0x000072A8
		public static IntPtr CreateFakeUDPPort(int idxFakeServerPort)
		{
			InteropHelp.TestIfAvailableClient();
			return NativeMethods.ISteamNetworkingSockets_CreateFakeUDPPort(CSteamAPIContext.GetSteamNetworkingSockets(), idxFakeServerPort);
		}
	}
}
