using System;

namespace Steamworks
{
	// Token: 0x02000087 RID: 135
	public enum NetConnectionEnd
	{
		// Token: 0x04000670 RID: 1648
		Invalid,
		// Token: 0x04000671 RID: 1649
		App_Min = 1000,
		// Token: 0x04000672 RID: 1650
		App_Generic = 1000,
		// Token: 0x04000673 RID: 1651
		App_Max = 1999,
		// Token: 0x04000674 RID: 1652
		AppException_Min,
		// Token: 0x04000675 RID: 1653
		AppException_Generic = 2000,
		// Token: 0x04000676 RID: 1654
		AppException_Max = 2999,
		// Token: 0x04000677 RID: 1655
		Local_Min,
		// Token: 0x04000678 RID: 1656
		Local_OfflineMode,
		// Token: 0x04000679 RID: 1657
		Local_ManyRelayConnectivity,
		// Token: 0x0400067A RID: 1658
		Local_HostedServerPrimaryRelay,
		// Token: 0x0400067B RID: 1659
		Local_NetworkConfig,
		// Token: 0x0400067C RID: 1660
		Local_Rights,
		// Token: 0x0400067D RID: 1661
		Local_Max = 3999,
		// Token: 0x0400067E RID: 1662
		Remote_Min,
		// Token: 0x0400067F RID: 1663
		Remote_Timeout,
		// Token: 0x04000680 RID: 1664
		Remote_BadCrypt,
		// Token: 0x04000681 RID: 1665
		Remote_BadCert,
		// Token: 0x04000682 RID: 1666
		Remote_NotLoggedIn,
		// Token: 0x04000683 RID: 1667
		Remote_NotRunningApp,
		// Token: 0x04000684 RID: 1668
		Remote_BadProtocolVersion,
		// Token: 0x04000685 RID: 1669
		Remote_Max = 4999,
		// Token: 0x04000686 RID: 1670
		Misc_Min,
		// Token: 0x04000687 RID: 1671
		Misc_Generic,
		// Token: 0x04000688 RID: 1672
		Misc_InternalError,
		// Token: 0x04000689 RID: 1673
		Misc_Timeout,
		// Token: 0x0400068A RID: 1674
		Misc_RelayConnectivity,
		// Token: 0x0400068B RID: 1675
		Misc_SteamConnectivity,
		// Token: 0x0400068C RID: 1676
		Misc_NoRelaySessionsToClient,
		// Token: 0x0400068D RID: 1677
		Misc_Max = 5999
	}
}
