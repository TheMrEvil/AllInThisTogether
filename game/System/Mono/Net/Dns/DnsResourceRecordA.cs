using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000BE RID: 190
	internal class DnsResourceRecordA : DnsResourceRecordIPAddress
	{
		// Token: 0x060003BC RID: 956 RVA: 0x0000B526 File Offset: 0x00009726
		internal DnsResourceRecordA(DnsResourceRecord rr) : base(rr, 4)
		{
		}
	}
}
