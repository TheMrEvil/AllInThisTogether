using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200019A RID: 410
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamDatagramRelayAuthTicket
	{
		// Token: 0x060009A7 RID: 2471 RVA: 0x0000EBD9 File Offset: 0x0000CDD9
		public void Clear()
		{
		}

		// Token: 0x04000AA0 RID: 2720
		private SteamNetworkingIdentity m_identityGameserver;

		// Token: 0x04000AA1 RID: 2721
		private SteamNetworkingIdentity m_identityAuthorizedClient;

		// Token: 0x04000AA2 RID: 2722
		private uint m_unPublicIP;

		// Token: 0x04000AA3 RID: 2723
		private RTime32 m_rtimeTicketExpiry;

		// Token: 0x04000AA4 RID: 2724
		private SteamDatagramHostedAddress m_routing;

		// Token: 0x04000AA5 RID: 2725
		private uint m_nAppID;

		// Token: 0x04000AA6 RID: 2726
		private int m_nRestrictToVirtualPort;

		// Token: 0x04000AA7 RID: 2727
		private const int k_nMaxExtraFields = 16;

		// Token: 0x04000AA8 RID: 2728
		private int m_nExtraFields;

		// Token: 0x04000AA9 RID: 2729
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		private SteamDatagramRelayAuthTicket.ExtraField[] m_vecExtraFields;

		// Token: 0x020001EE RID: 494
		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		private struct ExtraField
		{
			// Token: 0x04000B26 RID: 2854
			private SteamDatagramRelayAuthTicket.ExtraField.EType m_eType;

			// Token: 0x04000B27 RID: 2855
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
			private byte[] m_szName;

			// Token: 0x04000B28 RID: 2856
			private SteamDatagramRelayAuthTicket.ExtraField.OptionValue m_val;

			// Token: 0x020001F3 RID: 499
			private enum EType
			{
				// Token: 0x04000B35 RID: 2869
				k_EType_String,
				// Token: 0x04000B36 RID: 2870
				k_EType_Int,
				// Token: 0x04000B37 RID: 2871
				k_EType_Fixed64
			}

			// Token: 0x020001F4 RID: 500
			[StructLayout(LayoutKind.Explicit)]
			private struct OptionValue
			{
				// Token: 0x04000B38 RID: 2872
				[FieldOffset(0)]
				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
				private byte[] m_szStringValue;

				// Token: 0x04000B39 RID: 2873
				[FieldOffset(0)]
				private long m_nIntValue;

				// Token: 0x04000B3A RID: 2874
				[FieldOffset(0)]
				private ulong m_nFixed64Value;
			}
		}
	}
}
