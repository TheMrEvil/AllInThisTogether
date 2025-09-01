using System;

namespace Steamworks
{
	// Token: 0x02000168 RID: 360
	public enum ESteamNetworkingConfigValue
	{
		// Token: 0x04000959 RID: 2393
		k_ESteamNetworkingConfig_Invalid,
		// Token: 0x0400095A RID: 2394
		k_ESteamNetworkingConfig_TimeoutInitial = 24,
		// Token: 0x0400095B RID: 2395
		k_ESteamNetworkingConfig_TimeoutConnected,
		// Token: 0x0400095C RID: 2396
		k_ESteamNetworkingConfig_SendBufferSize = 9,
		// Token: 0x0400095D RID: 2397
		k_ESteamNetworkingConfig_ConnectionUserData = 40,
		// Token: 0x0400095E RID: 2398
		k_ESteamNetworkingConfig_SendRateMin = 10,
		// Token: 0x0400095F RID: 2399
		k_ESteamNetworkingConfig_SendRateMax,
		// Token: 0x04000960 RID: 2400
		k_ESteamNetworkingConfig_NagleTime,
		// Token: 0x04000961 RID: 2401
		k_ESteamNetworkingConfig_IP_AllowWithoutAuth = 23,
		// Token: 0x04000962 RID: 2402
		k_ESteamNetworkingConfig_MTU_PacketSize = 32,
		// Token: 0x04000963 RID: 2403
		k_ESteamNetworkingConfig_MTU_DataSize,
		// Token: 0x04000964 RID: 2404
		k_ESteamNetworkingConfig_Unencrypted,
		// Token: 0x04000965 RID: 2405
		k_ESteamNetworkingConfig_SymmetricConnect = 37,
		// Token: 0x04000966 RID: 2406
		k_ESteamNetworkingConfig_LocalVirtualPort,
		// Token: 0x04000967 RID: 2407
		k_ESteamNetworkingConfig_DualWifi_Enable,
		// Token: 0x04000968 RID: 2408
		k_ESteamNetworkingConfig_EnableDiagnosticsUI = 46,
		// Token: 0x04000969 RID: 2409
		k_ESteamNetworkingConfig_FakePacketLoss_Send = 2,
		// Token: 0x0400096A RID: 2410
		k_ESteamNetworkingConfig_FakePacketLoss_Recv,
		// Token: 0x0400096B RID: 2411
		k_ESteamNetworkingConfig_FakePacketLag_Send,
		// Token: 0x0400096C RID: 2412
		k_ESteamNetworkingConfig_FakePacketLag_Recv,
		// Token: 0x0400096D RID: 2413
		k_ESteamNetworkingConfig_FakePacketReorder_Send,
		// Token: 0x0400096E RID: 2414
		k_ESteamNetworkingConfig_FakePacketReorder_Recv,
		// Token: 0x0400096F RID: 2415
		k_ESteamNetworkingConfig_FakePacketReorder_Time,
		// Token: 0x04000970 RID: 2416
		k_ESteamNetworkingConfig_FakePacketDup_Send = 26,
		// Token: 0x04000971 RID: 2417
		k_ESteamNetworkingConfig_FakePacketDup_Recv,
		// Token: 0x04000972 RID: 2418
		k_ESteamNetworkingConfig_FakePacketDup_TimeMax,
		// Token: 0x04000973 RID: 2419
		k_ESteamNetworkingConfig_PacketTraceMaxBytes = 41,
		// Token: 0x04000974 RID: 2420
		k_ESteamNetworkingConfig_FakeRateLimit_Send_Rate,
		// Token: 0x04000975 RID: 2421
		k_ESteamNetworkingConfig_FakeRateLimit_Send_Burst,
		// Token: 0x04000976 RID: 2422
		k_ESteamNetworkingConfig_FakeRateLimit_Recv_Rate,
		// Token: 0x04000977 RID: 2423
		k_ESteamNetworkingConfig_FakeRateLimit_Recv_Burst,
		// Token: 0x04000978 RID: 2424
		k_ESteamNetworkingConfig_Callback_ConnectionStatusChanged = 201,
		// Token: 0x04000979 RID: 2425
		k_ESteamNetworkingConfig_Callback_AuthStatusChanged,
		// Token: 0x0400097A RID: 2426
		k_ESteamNetworkingConfig_Callback_RelayNetworkStatusChanged,
		// Token: 0x0400097B RID: 2427
		k_ESteamNetworkingConfig_Callback_MessagesSessionRequest,
		// Token: 0x0400097C RID: 2428
		k_ESteamNetworkingConfig_Callback_MessagesSessionFailed,
		// Token: 0x0400097D RID: 2429
		k_ESteamNetworkingConfig_Callback_CreateConnectionSignaling,
		// Token: 0x0400097E RID: 2430
		k_ESteamNetworkingConfig_Callback_FakeIPResult,
		// Token: 0x0400097F RID: 2431
		k_ESteamNetworkingConfig_P2P_STUN_ServerList = 103,
		// Token: 0x04000980 RID: 2432
		k_ESteamNetworkingConfig_P2P_Transport_ICE_Enable,
		// Token: 0x04000981 RID: 2433
		k_ESteamNetworkingConfig_P2P_Transport_ICE_Penalty,
		// Token: 0x04000982 RID: 2434
		k_ESteamNetworkingConfig_P2P_Transport_SDR_Penalty,
		// Token: 0x04000983 RID: 2435
		k_ESteamNetworkingConfig_P2P_TURN_ServerList,
		// Token: 0x04000984 RID: 2436
		k_ESteamNetworkingConfig_P2P_TURN_UserList,
		// Token: 0x04000985 RID: 2437
		k_ESteamNetworkingConfig_P2P_TURN_PassList,
		// Token: 0x04000986 RID: 2438
		k_ESteamNetworkingConfig_P2P_Transport_ICE_Implementation,
		// Token: 0x04000987 RID: 2439
		k_ESteamNetworkingConfig_SDRClient_ConsecutitivePingTimeoutsFailInitial = 19,
		// Token: 0x04000988 RID: 2440
		k_ESteamNetworkingConfig_SDRClient_ConsecutitivePingTimeoutsFail,
		// Token: 0x04000989 RID: 2441
		k_ESteamNetworkingConfig_SDRClient_MinPingsBeforePingAccurate,
		// Token: 0x0400098A RID: 2442
		k_ESteamNetworkingConfig_SDRClient_SingleSocket,
		// Token: 0x0400098B RID: 2443
		k_ESteamNetworkingConfig_SDRClient_ForceRelayCluster = 29,
		// Token: 0x0400098C RID: 2444
		k_ESteamNetworkingConfig_SDRClient_DebugTicketAddress,
		// Token: 0x0400098D RID: 2445
		k_ESteamNetworkingConfig_SDRClient_ForceProxyAddr,
		// Token: 0x0400098E RID: 2446
		k_ESteamNetworkingConfig_SDRClient_FakeClusterPing = 36,
		// Token: 0x0400098F RID: 2447
		k_ESteamNetworkingConfig_LogLevel_AckRTT = 13,
		// Token: 0x04000990 RID: 2448
		k_ESteamNetworkingConfig_LogLevel_PacketDecode,
		// Token: 0x04000991 RID: 2449
		k_ESteamNetworkingConfig_LogLevel_Message,
		// Token: 0x04000992 RID: 2450
		k_ESteamNetworkingConfig_LogLevel_PacketGaps,
		// Token: 0x04000993 RID: 2451
		k_ESteamNetworkingConfig_LogLevel_P2PRendezvous,
		// Token: 0x04000994 RID: 2452
		k_ESteamNetworkingConfig_LogLevel_SDRRelayPings,
		// Token: 0x04000995 RID: 2453
		k_ESteamNetworkingConfig_DELETED_EnumerateDevVars = 35,
		// Token: 0x04000996 RID: 2454
		k_ESteamNetworkingConfigValue__Force32Bit = 2147483647
	}
}
