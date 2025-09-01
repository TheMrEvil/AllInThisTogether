using System;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x02000024 RID: 36
	internal class ISteamNetworkingSockets : SteamInterface
	{
		// Token: 0x0600044D RID: 1101 RVA: 0x0000799F File Offset: 0x00005B9F
		internal ISteamNetworkingSockets(bool IsGameServer)
		{
			base.SetupInterface(IsGameServer);
		}

		// Token: 0x0600044E RID: 1102
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamNetworkingSockets_v008();

		// Token: 0x0600044F RID: 1103 RVA: 0x000079B1 File Offset: 0x00005BB1
		public override IntPtr GetUserInterfacePointer()
		{
			return ISteamNetworkingSockets.SteamAPI_SteamNetworkingSockets_v008();
		}

		// Token: 0x06000450 RID: 1104
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SteamAPI_SteamGameServerNetworkingSockets_v008();

		// Token: 0x06000451 RID: 1105 RVA: 0x000079B8 File Offset: 0x00005BB8
		public override IntPtr GetServerInterfacePointer()
		{
			return ISteamNetworkingSockets.SteamAPI_SteamGameServerNetworkingSockets_v008();
		}

		// Token: 0x06000452 RID: 1106
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_CreateListenSocketIP")]
		private static extern Socket _CreateListenSocketIP(IntPtr self, ref NetAddress localAddress, int nOptions, [In] [Out] NetKeyValue[] pOptions);

		// Token: 0x06000453 RID: 1107 RVA: 0x000079C0 File Offset: 0x00005BC0
		internal Socket CreateListenSocketIP(ref NetAddress localAddress, int nOptions, [In] [Out] NetKeyValue[] pOptions)
		{
			return ISteamNetworkingSockets._CreateListenSocketIP(this.Self, ref localAddress, nOptions, pOptions);
		}

		// Token: 0x06000454 RID: 1108
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_ConnectByIPAddress")]
		private static extern Connection _ConnectByIPAddress(IntPtr self, ref NetAddress address, int nOptions, [In] [Out] NetKeyValue[] pOptions);

		// Token: 0x06000455 RID: 1109 RVA: 0x000079E4 File Offset: 0x00005BE4
		internal Connection ConnectByIPAddress(ref NetAddress address, int nOptions, [In] [Out] NetKeyValue[] pOptions)
		{
			return ISteamNetworkingSockets._ConnectByIPAddress(this.Self, ref address, nOptions, pOptions);
		}

		// Token: 0x06000456 RID: 1110
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_CreateListenSocketP2P")]
		private static extern Socket _CreateListenSocketP2P(IntPtr self, int nVirtualPort, int nOptions, [In] [Out] NetKeyValue[] pOptions);

		// Token: 0x06000457 RID: 1111 RVA: 0x00007A08 File Offset: 0x00005C08
		internal Socket CreateListenSocketP2P(int nVirtualPort, int nOptions, [In] [Out] NetKeyValue[] pOptions)
		{
			return ISteamNetworkingSockets._CreateListenSocketP2P(this.Self, nVirtualPort, nOptions, pOptions);
		}

		// Token: 0x06000458 RID: 1112
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_ConnectP2P")]
		private static extern Connection _ConnectP2P(IntPtr self, ref NetIdentity identityRemote, int nVirtualPort, int nOptions, [In] [Out] NetKeyValue[] pOptions);

		// Token: 0x06000459 RID: 1113 RVA: 0x00007A2C File Offset: 0x00005C2C
		internal Connection ConnectP2P(ref NetIdentity identityRemote, int nVirtualPort, int nOptions, [In] [Out] NetKeyValue[] pOptions)
		{
			return ISteamNetworkingSockets._ConnectP2P(this.Self, ref identityRemote, nVirtualPort, nOptions, pOptions);
		}

		// Token: 0x0600045A RID: 1114
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_AcceptConnection")]
		private static extern Result _AcceptConnection(IntPtr self, Connection hConn);

		// Token: 0x0600045B RID: 1115 RVA: 0x00007A50 File Offset: 0x00005C50
		internal Result AcceptConnection(Connection hConn)
		{
			return ISteamNetworkingSockets._AcceptConnection(this.Self, hConn);
		}

		// Token: 0x0600045C RID: 1116
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_CloseConnection")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _CloseConnection(IntPtr self, Connection hPeer, int nReason, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszDebug, [MarshalAs(UnmanagedType.U1)] bool bEnableLinger);

		// Token: 0x0600045D RID: 1117 RVA: 0x00007A70 File Offset: 0x00005C70
		internal bool CloseConnection(Connection hPeer, int nReason, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszDebug, [MarshalAs(UnmanagedType.U1)] bool bEnableLinger)
		{
			return ISteamNetworkingSockets._CloseConnection(this.Self, hPeer, nReason, pszDebug, bEnableLinger);
		}

		// Token: 0x0600045E RID: 1118
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_CloseListenSocket")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _CloseListenSocket(IntPtr self, Socket hSocket);

		// Token: 0x0600045F RID: 1119 RVA: 0x00007A94 File Offset: 0x00005C94
		internal bool CloseListenSocket(Socket hSocket)
		{
			return ISteamNetworkingSockets._CloseListenSocket(this.Self, hSocket);
		}

		// Token: 0x06000460 RID: 1120
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_SetConnectionUserData")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetConnectionUserData(IntPtr self, Connection hPeer, long nUserData);

		// Token: 0x06000461 RID: 1121 RVA: 0x00007AB4 File Offset: 0x00005CB4
		internal bool SetConnectionUserData(Connection hPeer, long nUserData)
		{
			return ISteamNetworkingSockets._SetConnectionUserData(this.Self, hPeer, nUserData);
		}

		// Token: 0x06000462 RID: 1122
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_GetConnectionUserData")]
		private static extern long _GetConnectionUserData(IntPtr self, Connection hPeer);

		// Token: 0x06000463 RID: 1123 RVA: 0x00007AD8 File Offset: 0x00005CD8
		internal long GetConnectionUserData(Connection hPeer)
		{
			return ISteamNetworkingSockets._GetConnectionUserData(this.Self, hPeer);
		}

		// Token: 0x06000464 RID: 1124
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_SetConnectionName")]
		private static extern void _SetConnectionName(IntPtr self, Connection hPeer, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszName);

		// Token: 0x06000465 RID: 1125 RVA: 0x00007AF8 File Offset: 0x00005CF8
		internal void SetConnectionName(Connection hPeer, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = Steamworks.Utf8StringToNative)] string pszName)
		{
			ISteamNetworkingSockets._SetConnectionName(this.Self, hPeer, pszName);
		}

		// Token: 0x06000466 RID: 1126
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_GetConnectionName")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetConnectionName(IntPtr self, Connection hPeer, IntPtr pszName, int nMaxLen);

		// Token: 0x06000467 RID: 1127 RVA: 0x00007B0C File Offset: 0x00005D0C
		internal bool GetConnectionName(Connection hPeer, out string pszName)
		{
			IntPtr intPtr = Helpers.TakeMemory();
			bool result = ISteamNetworkingSockets._GetConnectionName(this.Self, hPeer, intPtr, 32768);
			pszName = Helpers.MemoryToString(intPtr);
			return result;
		}

		// Token: 0x06000468 RID: 1128
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_SendMessageToConnection")]
		private static extern Result _SendMessageToConnection(IntPtr self, Connection hConn, IntPtr pData, uint cbData, int nSendFlags, ref long pOutMessageNumber);

		// Token: 0x06000469 RID: 1129 RVA: 0x00007B40 File Offset: 0x00005D40
		internal Result SendMessageToConnection(Connection hConn, IntPtr pData, uint cbData, int nSendFlags, ref long pOutMessageNumber)
		{
			return ISteamNetworkingSockets._SendMessageToConnection(this.Self, hConn, pData, cbData, nSendFlags, ref pOutMessageNumber);
		}

		// Token: 0x0600046A RID: 1130
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_SendMessages")]
		private static extern void _SendMessages(IntPtr self, int nMessages, ref NetMsg pMessages, [In] [Out] long[] pOutMessageNumberOrResult);

		// Token: 0x0600046B RID: 1131 RVA: 0x00007B66 File Offset: 0x00005D66
		internal void SendMessages(int nMessages, ref NetMsg pMessages, [In] [Out] long[] pOutMessageNumberOrResult)
		{
			ISteamNetworkingSockets._SendMessages(this.Self, nMessages, ref pMessages, pOutMessageNumberOrResult);
		}

		// Token: 0x0600046C RID: 1132
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_FlushMessagesOnConnection")]
		private static extern Result _FlushMessagesOnConnection(IntPtr self, Connection hConn);

		// Token: 0x0600046D RID: 1133 RVA: 0x00007B78 File Offset: 0x00005D78
		internal Result FlushMessagesOnConnection(Connection hConn)
		{
			return ISteamNetworkingSockets._FlushMessagesOnConnection(this.Self, hConn);
		}

		// Token: 0x0600046E RID: 1134
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_ReceiveMessagesOnConnection")]
		private static extern int _ReceiveMessagesOnConnection(IntPtr self, Connection hConn, IntPtr ppOutMessages, int nMaxMessages);

		// Token: 0x0600046F RID: 1135 RVA: 0x00007B98 File Offset: 0x00005D98
		internal int ReceiveMessagesOnConnection(Connection hConn, IntPtr ppOutMessages, int nMaxMessages)
		{
			return ISteamNetworkingSockets._ReceiveMessagesOnConnection(this.Self, hConn, ppOutMessages, nMaxMessages);
		}

		// Token: 0x06000470 RID: 1136
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_GetConnectionInfo")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetConnectionInfo(IntPtr self, Connection hConn, ref ConnectionInfo pInfo);

		// Token: 0x06000471 RID: 1137 RVA: 0x00007BBC File Offset: 0x00005DBC
		internal bool GetConnectionInfo(Connection hConn, ref ConnectionInfo pInfo)
		{
			return ISteamNetworkingSockets._GetConnectionInfo(this.Self, hConn, ref pInfo);
		}

		// Token: 0x06000472 RID: 1138
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_GetQuickConnectionStatus")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetQuickConnectionStatus(IntPtr self, Connection hConn, ref SteamNetworkingQuickConnectionStatus pStats);

		// Token: 0x06000473 RID: 1139 RVA: 0x00007BE0 File Offset: 0x00005DE0
		internal bool GetQuickConnectionStatus(Connection hConn, ref SteamNetworkingQuickConnectionStatus pStats)
		{
			return ISteamNetworkingSockets._GetQuickConnectionStatus(this.Self, hConn, ref pStats);
		}

		// Token: 0x06000474 RID: 1140
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_GetDetailedConnectionStatus")]
		private static extern int _GetDetailedConnectionStatus(IntPtr self, Connection hConn, IntPtr pszBuf, int cbBuf);

		// Token: 0x06000475 RID: 1141 RVA: 0x00007C04 File Offset: 0x00005E04
		internal int GetDetailedConnectionStatus(Connection hConn, out string pszBuf)
		{
			IntPtr intPtr = Helpers.TakeMemory();
			int result = ISteamNetworkingSockets._GetDetailedConnectionStatus(this.Self, hConn, intPtr, 32768);
			pszBuf = Helpers.MemoryToString(intPtr);
			return result;
		}

		// Token: 0x06000476 RID: 1142
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_GetListenSocketAddress")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetListenSocketAddress(IntPtr self, Socket hSocket, ref NetAddress address);

		// Token: 0x06000477 RID: 1143 RVA: 0x00007C38 File Offset: 0x00005E38
		internal bool GetListenSocketAddress(Socket hSocket, ref NetAddress address)
		{
			return ISteamNetworkingSockets._GetListenSocketAddress(this.Self, hSocket, ref address);
		}

		// Token: 0x06000478 RID: 1144
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_CreateSocketPair")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _CreateSocketPair(IntPtr self, [In] [Out] Connection[] pOutConnection1, [In] [Out] Connection[] pOutConnection2, [MarshalAs(UnmanagedType.U1)] bool bUseNetworkLoopback, ref NetIdentity pIdentity1, ref NetIdentity pIdentity2);

		// Token: 0x06000479 RID: 1145 RVA: 0x00007C5C File Offset: 0x00005E5C
		internal bool CreateSocketPair([In] [Out] Connection[] pOutConnection1, [In] [Out] Connection[] pOutConnection2, [MarshalAs(UnmanagedType.U1)] bool bUseNetworkLoopback, ref NetIdentity pIdentity1, ref NetIdentity pIdentity2)
		{
			return ISteamNetworkingSockets._CreateSocketPair(this.Self, pOutConnection1, pOutConnection2, bUseNetworkLoopback, ref pIdentity1, ref pIdentity2);
		}

		// Token: 0x0600047A RID: 1146
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_GetIdentity")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetIdentity(IntPtr self, ref NetIdentity pIdentity);

		// Token: 0x0600047B RID: 1147 RVA: 0x00007C84 File Offset: 0x00005E84
		internal bool GetIdentity(ref NetIdentity pIdentity)
		{
			return ISteamNetworkingSockets._GetIdentity(this.Self, ref pIdentity);
		}

		// Token: 0x0600047C RID: 1148
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_InitAuthentication")]
		private static extern SteamNetworkingAvailability _InitAuthentication(IntPtr self);

		// Token: 0x0600047D RID: 1149 RVA: 0x00007CA4 File Offset: 0x00005EA4
		internal SteamNetworkingAvailability InitAuthentication()
		{
			return ISteamNetworkingSockets._InitAuthentication(this.Self);
		}

		// Token: 0x0600047E RID: 1150
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_GetAuthenticationStatus")]
		private static extern SteamNetworkingAvailability _GetAuthenticationStatus(IntPtr self, ref SteamNetAuthenticationStatus_t pDetails);

		// Token: 0x0600047F RID: 1151 RVA: 0x00007CC4 File Offset: 0x00005EC4
		internal SteamNetworkingAvailability GetAuthenticationStatus(ref SteamNetAuthenticationStatus_t pDetails)
		{
			return ISteamNetworkingSockets._GetAuthenticationStatus(this.Self, ref pDetails);
		}

		// Token: 0x06000480 RID: 1152
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_CreatePollGroup")]
		private static extern HSteamNetPollGroup _CreatePollGroup(IntPtr self);

		// Token: 0x06000481 RID: 1153 RVA: 0x00007CE4 File Offset: 0x00005EE4
		internal HSteamNetPollGroup CreatePollGroup()
		{
			return ISteamNetworkingSockets._CreatePollGroup(this.Self);
		}

		// Token: 0x06000482 RID: 1154
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_DestroyPollGroup")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _DestroyPollGroup(IntPtr self, HSteamNetPollGroup hPollGroup);

		// Token: 0x06000483 RID: 1155 RVA: 0x00007D04 File Offset: 0x00005F04
		internal bool DestroyPollGroup(HSteamNetPollGroup hPollGroup)
		{
			return ISteamNetworkingSockets._DestroyPollGroup(this.Self, hPollGroup);
		}

		// Token: 0x06000484 RID: 1156
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_SetConnectionPollGroup")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetConnectionPollGroup(IntPtr self, Connection hConn, HSteamNetPollGroup hPollGroup);

		// Token: 0x06000485 RID: 1157 RVA: 0x00007D24 File Offset: 0x00005F24
		internal bool SetConnectionPollGroup(Connection hConn, HSteamNetPollGroup hPollGroup)
		{
			return ISteamNetworkingSockets._SetConnectionPollGroup(this.Self, hConn, hPollGroup);
		}

		// Token: 0x06000486 RID: 1158
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_ReceiveMessagesOnPollGroup")]
		private static extern int _ReceiveMessagesOnPollGroup(IntPtr self, HSteamNetPollGroup hPollGroup, IntPtr ppOutMessages, int nMaxMessages);

		// Token: 0x06000487 RID: 1159 RVA: 0x00007D48 File Offset: 0x00005F48
		internal int ReceiveMessagesOnPollGroup(HSteamNetPollGroup hPollGroup, IntPtr ppOutMessages, int nMaxMessages)
		{
			return ISteamNetworkingSockets._ReceiveMessagesOnPollGroup(this.Self, hPollGroup, ppOutMessages, nMaxMessages);
		}

		// Token: 0x06000488 RID: 1160
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_ReceivedRelayAuthTicket")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _ReceivedRelayAuthTicket(IntPtr self, IntPtr pvTicket, int cbTicket, [In] [Out] SteamDatagramRelayAuthTicket[] pOutParsedTicket);

		// Token: 0x06000489 RID: 1161 RVA: 0x00007D6C File Offset: 0x00005F6C
		internal bool ReceivedRelayAuthTicket(IntPtr pvTicket, int cbTicket, [In] [Out] SteamDatagramRelayAuthTicket[] pOutParsedTicket)
		{
			return ISteamNetworkingSockets._ReceivedRelayAuthTicket(this.Self, pvTicket, cbTicket, pOutParsedTicket);
		}

		// Token: 0x0600048A RID: 1162
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_FindRelayAuthTicketForServer")]
		private static extern int _FindRelayAuthTicketForServer(IntPtr self, ref NetIdentity identityGameServer, int nVirtualPort, [In] [Out] SteamDatagramRelayAuthTicket[] pOutParsedTicket);

		// Token: 0x0600048B RID: 1163 RVA: 0x00007D90 File Offset: 0x00005F90
		internal int FindRelayAuthTicketForServer(ref NetIdentity identityGameServer, int nVirtualPort, [In] [Out] SteamDatagramRelayAuthTicket[] pOutParsedTicket)
		{
			return ISteamNetworkingSockets._FindRelayAuthTicketForServer(this.Self, ref identityGameServer, nVirtualPort, pOutParsedTicket);
		}

		// Token: 0x0600048C RID: 1164
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_ConnectToHostedDedicatedServer")]
		private static extern Connection _ConnectToHostedDedicatedServer(IntPtr self, ref NetIdentity identityTarget, int nVirtualPort, int nOptions, [In] [Out] NetKeyValue[] pOptions);

		// Token: 0x0600048D RID: 1165 RVA: 0x00007DB4 File Offset: 0x00005FB4
		internal Connection ConnectToHostedDedicatedServer(ref NetIdentity identityTarget, int nVirtualPort, int nOptions, [In] [Out] NetKeyValue[] pOptions)
		{
			return ISteamNetworkingSockets._ConnectToHostedDedicatedServer(this.Self, ref identityTarget, nVirtualPort, nOptions, pOptions);
		}

		// Token: 0x0600048E RID: 1166
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_GetHostedDedicatedServerPort")]
		private static extern ushort _GetHostedDedicatedServerPort(IntPtr self);

		// Token: 0x0600048F RID: 1167 RVA: 0x00007DD8 File Offset: 0x00005FD8
		internal ushort GetHostedDedicatedServerPort()
		{
			return ISteamNetworkingSockets._GetHostedDedicatedServerPort(this.Self);
		}

		// Token: 0x06000490 RID: 1168
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_GetHostedDedicatedServerPOPID")]
		private static extern SteamNetworkingPOPID _GetHostedDedicatedServerPOPID(IntPtr self);

		// Token: 0x06000491 RID: 1169 RVA: 0x00007DF8 File Offset: 0x00005FF8
		internal SteamNetworkingPOPID GetHostedDedicatedServerPOPID()
		{
			return ISteamNetworkingSockets._GetHostedDedicatedServerPOPID(this.Self);
		}

		// Token: 0x06000492 RID: 1170
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_GetHostedDedicatedServerAddress")]
		private static extern Result _GetHostedDedicatedServerAddress(IntPtr self, ref SteamDatagramHostedAddress pRouting);

		// Token: 0x06000493 RID: 1171 RVA: 0x00007E18 File Offset: 0x00006018
		internal Result GetHostedDedicatedServerAddress(ref SteamDatagramHostedAddress pRouting)
		{
			return ISteamNetworkingSockets._GetHostedDedicatedServerAddress(this.Self, ref pRouting);
		}

		// Token: 0x06000494 RID: 1172
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_CreateHostedDedicatedServerListenSocket")]
		private static extern Socket _CreateHostedDedicatedServerListenSocket(IntPtr self, int nVirtualPort, int nOptions, [In] [Out] NetKeyValue[] pOptions);

		// Token: 0x06000495 RID: 1173 RVA: 0x00007E38 File Offset: 0x00006038
		internal Socket CreateHostedDedicatedServerListenSocket(int nVirtualPort, int nOptions, [In] [Out] NetKeyValue[] pOptions)
		{
			return ISteamNetworkingSockets._CreateHostedDedicatedServerListenSocket(this.Self, nVirtualPort, nOptions, pOptions);
		}

		// Token: 0x06000496 RID: 1174
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_GetGameCoordinatorServerLogin")]
		private static extern Result _GetGameCoordinatorServerLogin(IntPtr self, ref SteamDatagramGameCoordinatorServerLogin pLoginInfo, ref int pcbSignedBlob, IntPtr pBlob);

		// Token: 0x06000497 RID: 1175 RVA: 0x00007E5C File Offset: 0x0000605C
		internal Result GetGameCoordinatorServerLogin(ref SteamDatagramGameCoordinatorServerLogin pLoginInfo, ref int pcbSignedBlob, IntPtr pBlob)
		{
			return ISteamNetworkingSockets._GetGameCoordinatorServerLogin(this.Self, ref pLoginInfo, ref pcbSignedBlob, pBlob);
		}

		// Token: 0x06000498 RID: 1176
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_ConnectP2PCustomSignaling")]
		private static extern Connection _ConnectP2PCustomSignaling(IntPtr self, IntPtr pSignaling, ref NetIdentity pPeerIdentity, int nOptions, [In] [Out] NetKeyValue[] pOptions);

		// Token: 0x06000499 RID: 1177 RVA: 0x00007E80 File Offset: 0x00006080
		internal Connection ConnectP2PCustomSignaling(IntPtr pSignaling, ref NetIdentity pPeerIdentity, int nOptions, [In] [Out] NetKeyValue[] pOptions)
		{
			return ISteamNetworkingSockets._ConnectP2PCustomSignaling(this.Self, pSignaling, ref pPeerIdentity, nOptions, pOptions);
		}

		// Token: 0x0600049A RID: 1178
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_ReceivedP2PCustomSignal")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _ReceivedP2PCustomSignal(IntPtr self, IntPtr pMsg, int cbMsg, IntPtr pContext);

		// Token: 0x0600049B RID: 1179 RVA: 0x00007EA4 File Offset: 0x000060A4
		internal bool ReceivedP2PCustomSignal(IntPtr pMsg, int cbMsg, IntPtr pContext)
		{
			return ISteamNetworkingSockets._ReceivedP2PCustomSignal(this.Self, pMsg, cbMsg, pContext);
		}

		// Token: 0x0600049C RID: 1180
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_GetCertificateRequest")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _GetCertificateRequest(IntPtr self, ref int pcbBlob, IntPtr pBlob, ref NetErrorMessage errMsg);

		// Token: 0x0600049D RID: 1181 RVA: 0x00007EC8 File Offset: 0x000060C8
		internal bool GetCertificateRequest(ref int pcbBlob, IntPtr pBlob, ref NetErrorMessage errMsg)
		{
			return ISteamNetworkingSockets._GetCertificateRequest(this.Self, ref pcbBlob, pBlob, ref errMsg);
		}

		// Token: 0x0600049E RID: 1182
		[DllImport("steam_api64", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SteamAPI_ISteamNetworkingSockets_SetCertificate")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool _SetCertificate(IntPtr self, IntPtr pCertificate, int cbCertificate, ref NetErrorMessage errMsg);

		// Token: 0x0600049F RID: 1183 RVA: 0x00007EEC File Offset: 0x000060EC
		internal bool SetCertificate(IntPtr pCertificate, int cbCertificate, ref NetErrorMessage errMsg)
		{
			return ISteamNetworkingSockets._SetCertificate(this.Self, pCertificate, cbCertificate, ref errMsg);
		}
	}
}
