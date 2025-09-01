using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000743 RID: 1859
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct Win32_IP_ADAPTER_ADDRESSES
	{
		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x06003B22 RID: 15138 RVA: 0x000CBDFC File Offset: 0x000C9FFC
		public bool DdnsEnabled
		{
			get
			{
				return (this.Flags & 1U) > 0U;
			}
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x06003B23 RID: 15139 RVA: 0x000CBE09 File Offset: 0x000CA009
		public bool DhcpEnabled
		{
			get
			{
				return (this.Flags & 4U) > 0U;
			}
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x06003B24 RID: 15140 RVA: 0x000CBE16 File Offset: 0x000CA016
		public bool IsReceiveOnly
		{
			get
			{
				return (this.Flags & 8U) > 0U;
			}
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x06003B25 RID: 15141 RVA: 0x000CBE23 File Offset: 0x000CA023
		public bool NoMulticast
		{
			get
			{
				return (this.Flags & 16U) > 0U;
			}
		}

		// Token: 0x040022C9 RID: 8905
		public AlignmentUnion Alignment;

		// Token: 0x040022CA RID: 8906
		public IntPtr Next;

		// Token: 0x040022CB RID: 8907
		[MarshalAs(UnmanagedType.LPStr)]
		public string AdapterName;

		// Token: 0x040022CC RID: 8908
		public IntPtr FirstUnicastAddress;

		// Token: 0x040022CD RID: 8909
		public IntPtr FirstAnycastAddress;

		// Token: 0x040022CE RID: 8910
		public IntPtr FirstMulticastAddress;

		// Token: 0x040022CF RID: 8911
		public IntPtr FirstDnsServerAddress;

		// Token: 0x040022D0 RID: 8912
		public string DnsSuffix;

		// Token: 0x040022D1 RID: 8913
		public string Description;

		// Token: 0x040022D2 RID: 8914
		public string FriendlyName;

		// Token: 0x040022D3 RID: 8915
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public byte[] PhysicalAddress;

		// Token: 0x040022D4 RID: 8916
		public uint PhysicalAddressLength;

		// Token: 0x040022D5 RID: 8917
		public uint Flags;

		// Token: 0x040022D6 RID: 8918
		public uint Mtu;

		// Token: 0x040022D7 RID: 8919
		public NetworkInterfaceType IfType;

		// Token: 0x040022D8 RID: 8920
		public OperationalStatus OperStatus;

		// Token: 0x040022D9 RID: 8921
		public int Ipv6IfIndex;

		// Token: 0x040022DA RID: 8922
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public uint[] ZoneIndices;

		// Token: 0x040022DB RID: 8923
		public IntPtr FirstPrefix;

		// Token: 0x040022DC RID: 8924
		public ulong TransmitLinkSpeed;

		// Token: 0x040022DD RID: 8925
		public ulong ReceiveLinkSpeed;

		// Token: 0x040022DE RID: 8926
		public IntPtr FirstWinsServerAddress;

		// Token: 0x040022DF RID: 8927
		public IntPtr FirstGatewayAddress;

		// Token: 0x040022E0 RID: 8928
		public uint Ipv4Metric;

		// Token: 0x040022E1 RID: 8929
		public uint Ipv6Metric;

		// Token: 0x040022E2 RID: 8930
		public ulong Luid;

		// Token: 0x040022E3 RID: 8931
		public Win32_SOCKET_ADDRESS Dhcpv4Server;

		// Token: 0x040022E4 RID: 8932
		public uint CompartmentId;

		// Token: 0x040022E5 RID: 8933
		public ulong NetworkGuid;

		// Token: 0x040022E6 RID: 8934
		public int ConnectionType;

		// Token: 0x040022E7 RID: 8935
		public int TunnelType;

		// Token: 0x040022E8 RID: 8936
		public Win32_SOCKET_ADDRESS Dhcpv6Server;

		// Token: 0x040022E9 RID: 8937
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 130)]
		public byte[] Dhcpv6ClientDuid;

		// Token: 0x040022EA RID: 8938
		public ulong Dhcpv6ClientDuidLength;

		// Token: 0x040022EB RID: 8939
		public ulong Dhcpv6Iaid;

		// Token: 0x040022EC RID: 8940
		public IntPtr FirstDnsSuffix;

		// Token: 0x040022ED RID: 8941
		public const int GAA_FLAG_INCLUDE_WINS_INFO = 64;

		// Token: 0x040022EE RID: 8942
		public const int GAA_FLAG_INCLUDE_GATEWAYS = 128;

		// Token: 0x040022EF RID: 8943
		private const int MAX_ADAPTER_ADDRESS_LENGTH = 8;

		// Token: 0x040022F0 RID: 8944
		private const int MAX_DHCPV6_DUID_LENGTH = 130;

		// Token: 0x040022F1 RID: 8945
		private const int IP_ADAPTER_DDNS_ENABLED = 1;

		// Token: 0x040022F2 RID: 8946
		private const int IP_ADAPTER_DHCP_ENABLED = 4;

		// Token: 0x040022F3 RID: 8947
		private const int IP_ADAPTER_RECEIVE_ONLY = 8;

		// Token: 0x040022F4 RID: 8948
		private const int IP_ADAPTER_NO_MULTICAST = 16;
	}
}
