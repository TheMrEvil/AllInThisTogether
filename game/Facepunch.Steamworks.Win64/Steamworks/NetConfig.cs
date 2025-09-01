using System;

namespace Steamworks
{
	// Token: 0x0200008A RID: 138
	internal enum NetConfig
	{
		// Token: 0x0400069A RID: 1690
		Invalid,
		// Token: 0x0400069B RID: 1691
		FakePacketLoss_Send = 2,
		// Token: 0x0400069C RID: 1692
		FakePacketLoss_Recv,
		// Token: 0x0400069D RID: 1693
		FakePacketLag_Send,
		// Token: 0x0400069E RID: 1694
		FakePacketLag_Recv,
		// Token: 0x0400069F RID: 1695
		FakePacketReorder_Send,
		// Token: 0x040006A0 RID: 1696
		FakePacketReorder_Recv,
		// Token: 0x040006A1 RID: 1697
		FakePacketReorder_Time,
		// Token: 0x040006A2 RID: 1698
		FakePacketDup_Send = 26,
		// Token: 0x040006A3 RID: 1699
		FakePacketDup_Recv,
		// Token: 0x040006A4 RID: 1700
		FakePacketDup_TimeMax,
		// Token: 0x040006A5 RID: 1701
		TimeoutInitial = 24,
		// Token: 0x040006A6 RID: 1702
		TimeoutConnected,
		// Token: 0x040006A7 RID: 1703
		SendBufferSize = 9,
		// Token: 0x040006A8 RID: 1704
		SendRateMin,
		// Token: 0x040006A9 RID: 1705
		SendRateMax,
		// Token: 0x040006AA RID: 1706
		NagleTime,
		// Token: 0x040006AB RID: 1707
		IP_AllowWithoutAuth = 23,
		// Token: 0x040006AC RID: 1708
		MTU_PacketSize = 32,
		// Token: 0x040006AD RID: 1709
		MTU_DataSize,
		// Token: 0x040006AE RID: 1710
		Unencrypted,
		// Token: 0x040006AF RID: 1711
		EnumerateDevVars,
		// Token: 0x040006B0 RID: 1712
		SDRClient_ConsecutitivePingTimeoutsFailInitial = 19,
		// Token: 0x040006B1 RID: 1713
		SDRClient_ConsecutitivePingTimeoutsFail,
		// Token: 0x040006B2 RID: 1714
		SDRClient_MinPingsBeforePingAccurate,
		// Token: 0x040006B3 RID: 1715
		SDRClient_SingleSocket,
		// Token: 0x040006B4 RID: 1716
		SDRClient_ForceRelayCluster = 29,
		// Token: 0x040006B5 RID: 1717
		SDRClient_DebugTicketAddress,
		// Token: 0x040006B6 RID: 1718
		SDRClient_ForceProxyAddr,
		// Token: 0x040006B7 RID: 1719
		SDRClient_FakeClusterPing = 36,
		// Token: 0x040006B8 RID: 1720
		LogLevel_AckRTT = 13,
		// Token: 0x040006B9 RID: 1721
		LogLevel_PacketDecode,
		// Token: 0x040006BA RID: 1722
		LogLevel_Message,
		// Token: 0x040006BB RID: 1723
		LogLevel_PacketGaps,
		// Token: 0x040006BC RID: 1724
		LogLevel_P2PRendezvous,
		// Token: 0x040006BD RID: 1725
		LogLevel_SDRRelayPings
	}
}
