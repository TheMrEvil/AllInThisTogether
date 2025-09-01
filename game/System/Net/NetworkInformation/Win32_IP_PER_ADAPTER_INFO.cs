using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200072F RID: 1839
	[StructLayout(LayoutKind.Sequential)]
	internal class Win32_IP_PER_ADAPTER_INFO
	{
		// Token: 0x06003AB6 RID: 15030 RVA: 0x0000219B File Offset: 0x0000039B
		public Win32_IP_PER_ADAPTER_INFO()
		{
		}

		// Token: 0x04002279 RID: 8825
		public uint AutoconfigEnabled;

		// Token: 0x0400227A RID: 8826
		public uint AutoconfigActive;

		// Token: 0x0400227B RID: 8827
		public IntPtr CurrentDnsServer;

		// Token: 0x0400227C RID: 8828
		public Win32_IP_ADDR_STRING DnsServerList;
	}
}
