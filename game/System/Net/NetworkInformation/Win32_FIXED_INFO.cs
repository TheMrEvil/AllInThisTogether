using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200073D RID: 1853
	internal struct Win32_FIXED_INFO
	{
		// Token: 0x040022AE RID: 8878
		public string HostName;

		// Token: 0x040022AF RID: 8879
		public string DomainName;

		// Token: 0x040022B0 RID: 8880
		public IntPtr CurrentDnsServer;

		// Token: 0x040022B1 RID: 8881
		public Win32_IP_ADDR_STRING DnsServerList;

		// Token: 0x040022B2 RID: 8882
		public NetBiosNodeType NodeType;

		// Token: 0x040022B3 RID: 8883
		public string ScopeId;

		// Token: 0x040022B4 RID: 8884
		public uint EnableRouting;

		// Token: 0x040022B5 RID: 8885
		public uint EnableProxy;

		// Token: 0x040022B6 RID: 8886
		public uint EnableDns;
	}
}
