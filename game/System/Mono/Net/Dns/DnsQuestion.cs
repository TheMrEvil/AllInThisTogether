using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000BB RID: 187
	internal class DnsQuestion
	{
		// Token: 0x060003AD RID: 941 RVA: 0x0000219B File Offset: 0x0000039B
		internal DnsQuestion()
		{
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000B301 File Offset: 0x00009501
		internal int Init(DnsPacket packet, int offset)
		{
			this.name = packet.ReadName(ref offset);
			this.type = (DnsQType)packet.ReadUInt16(ref offset);
			this._class = (DnsQClass)packet.ReadUInt16(ref offset);
			return offset;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000B32E File Offset: 0x0000952E
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000B336 File Offset: 0x00009536
		public DnsQType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000B33E File Offset: 0x0000953E
		public DnsQClass Class
		{
			get
			{
				return this._class;
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000B346 File Offset: 0x00009546
		public override string ToString()
		{
			return string.Format("Name: {0} Type: {1} Class: {2}", this.Name, this.Type, this.Class);
		}

		// Token: 0x04000302 RID: 770
		private string name;

		// Token: 0x04000303 RID: 771
		private DnsQType type;

		// Token: 0x04000304 RID: 772
		private DnsQClass _class;
	}
}
