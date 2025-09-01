using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000BD RID: 189
	internal class DnsResourceRecord
	{
		// Token: 0x060003B3 RID: 947 RVA: 0x0000219B File Offset: 0x0000039B
		internal DnsResourceRecord()
		{
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000B370 File Offset: 0x00009570
		internal void CopyFrom(DnsResourceRecord rr)
		{
			this.name = rr.name;
			this.type = rr.type;
			this.klass = rr.klass;
			this.ttl = rr.ttl;
			this.rdlength = rr.rdlength;
			this.m_rdata = rr.m_rdata;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000B3C8 File Offset: 0x000095C8
		internal static DnsResourceRecord CreateFromBuffer(DnsPacket packet, int size, ref int offset)
		{
			string text = packet.ReadName(ref offset);
			DnsType dnsType = (DnsType)packet.ReadUInt16(ref offset);
			DnsClass dnsClass = (DnsClass)packet.ReadUInt16(ref offset);
			int num = packet.ReadInt32(ref offset);
			ushort num2 = packet.ReadUInt16(ref offset);
			DnsResourceRecord dnsResourceRecord = new DnsResourceRecord();
			dnsResourceRecord.name = text;
			dnsResourceRecord.type = dnsType;
			dnsResourceRecord.klass = dnsClass;
			dnsResourceRecord.ttl = num;
			dnsResourceRecord.rdlength = num2;
			dnsResourceRecord.m_rdata = new ArraySegment<byte>(packet.Packet, offset, (int)num2);
			offset += (int)num2;
			if (dnsClass == DnsClass.Internet)
			{
				if (dnsType <= DnsType.CNAME)
				{
					if (dnsType != DnsType.A)
					{
						if (dnsType == DnsType.CNAME)
						{
							dnsResourceRecord = new DnsResourceRecordCName(dnsResourceRecord);
						}
					}
					else
					{
						dnsResourceRecord = new DnsResourceRecordA(dnsResourceRecord);
					}
				}
				else if (dnsType != DnsType.PTR)
				{
					if (dnsType == DnsType.AAAA)
					{
						dnsResourceRecord = new DnsResourceRecordAAAA(dnsResourceRecord);
					}
				}
				else
				{
					dnsResourceRecord = new DnsResourceRecordPTR(dnsResourceRecord);
				}
			}
			return dnsResourceRecord;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000B495 File Offset: 0x00009695
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0000B49D File Offset: 0x0000969D
		public DnsType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000B4A5 File Offset: 0x000096A5
		public DnsClass Class
		{
			get
			{
				return this.klass;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000B4AD File Offset: 0x000096AD
		public int Ttl
		{
			get
			{
				return this.ttl;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000B4B5 File Offset: 0x000096B5
		public ArraySegment<byte> Data
		{
			get
			{
				return this.m_rdata;
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000B4C0 File Offset: 0x000096C0
		public override string ToString()
		{
			return string.Format("Name: {0}, Type: {1}, Class: {2}, Ttl: {3}, Data length: {4}", new object[]
			{
				this.name,
				this.type,
				this.klass,
				this.ttl,
				this.Data.Count
			});
		}

		// Token: 0x04000319 RID: 793
		private string name;

		// Token: 0x0400031A RID: 794
		private DnsType type;

		// Token: 0x0400031B RID: 795
		private DnsClass klass;

		// Token: 0x0400031C RID: 796
		private int ttl;

		// Token: 0x0400031D RID: 797
		private ushort rdlength;

		// Token: 0x0400031E RID: 798
		private ArraySegment<byte> m_rdata;
	}
}
