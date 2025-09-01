using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200073E RID: 1854
	internal struct Win32_FIXED_INFO_Marshal
	{
		// Token: 0x040022B7 RID: 8887
		private const int MAX_HOSTNAME_LEN = 128;

		// Token: 0x040022B8 RID: 8888
		private const int MAX_DOMAIN_NAME_LEN = 128;

		// Token: 0x040022B9 RID: 8889
		private const int MAX_SCOPE_ID_LEN = 256;

		// Token: 0x040022BA RID: 8890
		[FixedBuffer(typeof(byte), 132)]
		public Win32_FIXED_INFO_Marshal.<HostName>e__FixedBuffer HostName;

		// Token: 0x040022BB RID: 8891
		[FixedBuffer(typeof(byte), 132)]
		public Win32_FIXED_INFO_Marshal.<DomainName>e__FixedBuffer DomainName;

		// Token: 0x040022BC RID: 8892
		public IntPtr CurrentDnsServer;

		// Token: 0x040022BD RID: 8893
		public Win32_IP_ADDR_STRING DnsServerList;

		// Token: 0x040022BE RID: 8894
		public NetBiosNodeType NodeType;

		// Token: 0x040022BF RID: 8895
		[FixedBuffer(typeof(byte), 260)]
		public Win32_FIXED_INFO_Marshal.<ScopeId>e__FixedBuffer ScopeId;

		// Token: 0x040022C0 RID: 8896
		public uint EnableRouting;

		// Token: 0x040022C1 RID: 8897
		public uint EnableProxy;

		// Token: 0x040022C2 RID: 8898
		public uint EnableDns;

		// Token: 0x0200073F RID: 1855
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 132)]
		public struct <HostName>e__FixedBuffer
		{
			// Token: 0x040022C3 RID: 8899
			public byte FixedElementField;
		}

		// Token: 0x02000740 RID: 1856
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 132)]
		public struct <DomainName>e__FixedBuffer
		{
			// Token: 0x040022C4 RID: 8900
			public byte FixedElementField;
		}

		// Token: 0x02000741 RID: 1857
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 260)]
		public struct <ScopeId>e__FixedBuffer
		{
			// Token: 0x040022C5 RID: 8901
			public byte FixedElementField;
		}
	}
}
