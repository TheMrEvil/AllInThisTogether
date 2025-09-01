using System;
using System.Net;

namespace Mono.Net.Dns
{
	// Token: 0x020000C1 RID: 193
	internal abstract class DnsResourceRecordIPAddress : DnsResourceRecord
	{
		// Token: 0x060003C1 RID: 961 RVA: 0x0000B5A8 File Offset: 0x000097A8
		internal DnsResourceRecordIPAddress(DnsResourceRecord rr, int address_size)
		{
			base.CopyFrom(rr);
			ArraySegment<byte> data = rr.Data;
			byte[] dst = new byte[address_size];
			Buffer.BlockCopy(data.Array, data.Offset, dst, 0, address_size);
			this.address = new IPAddress(dst);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000B5F2 File Offset: 0x000097F2
		public override string ToString()
		{
			string str = base.ToString();
			string str2 = " Address: ";
			IPAddress ipaddress = this.address;
			return str + str2 + ((ipaddress != null) ? ipaddress.ToString() : null);
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000B616 File Offset: 0x00009816
		public IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x04000320 RID: 800
		private IPAddress address;
	}
}
