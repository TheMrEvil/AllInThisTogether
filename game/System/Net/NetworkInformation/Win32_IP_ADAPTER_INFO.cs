using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000744 RID: 1860
	internal struct Win32_IP_ADAPTER_INFO
	{
		// Token: 0x040022F5 RID: 8949
		private const int MAX_ADAPTER_NAME_LENGTH = 256;

		// Token: 0x040022F6 RID: 8950
		private const int MAX_ADAPTER_DESCRIPTION_LENGTH = 128;

		// Token: 0x040022F7 RID: 8951
		private const int MAX_ADAPTER_ADDRESS_LENGTH = 8;

		// Token: 0x040022F8 RID: 8952
		public IntPtr Next;

		// Token: 0x040022F9 RID: 8953
		public int ComboIndex;

		// Token: 0x040022FA RID: 8954
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string AdapterName;

		// Token: 0x040022FB RID: 8955
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 132)]
		public string Description;

		// Token: 0x040022FC RID: 8956
		public uint AddressLength;

		// Token: 0x040022FD RID: 8957
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public byte[] Address;

		// Token: 0x040022FE RID: 8958
		public uint Index;

		// Token: 0x040022FF RID: 8959
		public uint Type;

		// Token: 0x04002300 RID: 8960
		public uint DhcpEnabled;

		// Token: 0x04002301 RID: 8961
		public IntPtr CurrentIpAddress;

		// Token: 0x04002302 RID: 8962
		public Win32_IP_ADDR_STRING IpAddressList;

		// Token: 0x04002303 RID: 8963
		public Win32_IP_ADDR_STRING GatewayList;

		// Token: 0x04002304 RID: 8964
		public Win32_IP_ADDR_STRING DhcpServer;

		// Token: 0x04002305 RID: 8965
		public bool HaveWins;

		// Token: 0x04002306 RID: 8966
		public Win32_IP_ADDR_STRING PrimaryWinsServer;

		// Token: 0x04002307 RID: 8967
		public Win32_IP_ADDR_STRING SecondaryWinsServer;

		// Token: 0x04002308 RID: 8968
		public long LeaseObtained;

		// Token: 0x04002309 RID: 8969
		public long LeaseExpires;
	}
}
