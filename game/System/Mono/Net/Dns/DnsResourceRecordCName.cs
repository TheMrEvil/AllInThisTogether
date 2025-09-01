using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000C0 RID: 192
	internal class DnsResourceRecordCName : DnsResourceRecord
	{
		// Token: 0x060003BE RID: 958 RVA: 0x0000B53C File Offset: 0x0000973C
		internal DnsResourceRecordCName(DnsResourceRecord rr)
		{
			base.CopyFrom(rr);
			int offset = rr.Data.Offset;
			this.cname = DnsPacket.ReadName(rr.Data.Array, ref offset);
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000B580 File Offset: 0x00009780
		public string CName
		{
			get
			{
				return this.cname;
			}
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000B588 File Offset: 0x00009788
		public override string ToString()
		{
			return base.ToString() + " CNAME: " + this.cname.ToString();
		}

		// Token: 0x0400031F RID: 799
		private string cname;
	}
}
