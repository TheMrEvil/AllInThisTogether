using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000B7 RID: 183
	internal abstract class DnsPacket
	{
		// Token: 0x0600039E RID: 926 RVA: 0x0000219B File Offset: 0x0000039B
		protected DnsPacket()
		{
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000AFE9 File Offset: 0x000091E9
		protected DnsPacket(int length) : this(new byte[length], length)
		{
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000AFF8 File Offset: 0x000091F8
		protected DnsPacket(byte[] buffer, int length)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (length <= 0)
			{
				throw new ArgumentOutOfRangeException("length", "Must be greater than zero.");
			}
			this.packet = buffer;
			this.position = length;
			this.header = new DnsHeader(new ArraySegment<byte>(this.packet, 0, 12));
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000B054 File Offset: 0x00009254
		public byte[] Packet
		{
			get
			{
				return this.packet;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000B05C File Offset: 0x0000925C
		public int Length
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000B064 File Offset: 0x00009264
		public DnsHeader Header
		{
			get
			{
				return this.header;
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000B06C File Offset: 0x0000926C
		protected void WriteUInt16(ushort v)
		{
			byte[] array = this.packet;
			int num = this.position;
			this.position = num + 1;
			array[num] = (byte)((v & 65280) >> 8);
			byte[] array2 = this.packet;
			num = this.position;
			this.position = num + 1;
			array2[num] = (byte)(v & 255);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000B0BC File Offset: 0x000092BC
		protected void WriteStringBytes(string str, int offset, int count)
		{
			int num = offset;
			int i = 0;
			while (i < count)
			{
				byte[] array = this.packet;
				int num2 = this.position;
				this.position = num2 + 1;
				array[num2] = (byte)str[num];
				i++;
				num++;
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000B0FC File Offset: 0x000092FC
		protected void WriteLabel(string str, int offset, int count)
		{
			byte[] array = this.packet;
			int num = this.position;
			this.position = num + 1;
			array[num] = (byte)count;
			this.WriteStringBytes(str, offset, count);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000B12C File Offset: 0x0000932C
		protected void WriteDnsName(string name)
		{
			if (!DnsUtil.IsValidDnsName(name))
			{
				throw new ArgumentException("Invalid DNS name");
			}
			if (!string.IsNullOrEmpty(name))
			{
				int length = name.Length;
				int num = 0;
				int num2 = 0;
				for (int i = 0; i < length; i++)
				{
					if (name[i] != '.')
					{
						num2++;
					}
					else
					{
						if (i == 0)
						{
							break;
						}
						this.WriteLabel(name, num, num2);
						num += num2 + 1;
						num2 = 0;
					}
				}
				if (num2 > 0)
				{
					this.WriteLabel(name, num, num2);
				}
			}
			byte[] array = this.packet;
			int num3 = this.position;
			this.position = num3 + 1;
			array[num3] = 0;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000B1B9 File Offset: 0x000093B9
		protected internal string ReadName(ref int offset)
		{
			return DnsUtil.ReadName(this.packet, ref offset);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000B1C7 File Offset: 0x000093C7
		protected internal static string ReadName(byte[] buffer, ref int offset)
		{
			return DnsUtil.ReadName(buffer, ref offset);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000B1D0 File Offset: 0x000093D0
		protected internal ushort ReadUInt16(ref int offset)
		{
			byte[] array = this.packet;
			int num = offset;
			offset = num + 1;
			ushort num2 = array[num] << 8;
			byte[] array2 = this.packet;
			num = offset;
			offset = num + 1;
			return num2 + array2[num];
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000B204 File Offset: 0x00009404
		protected internal int ReadInt32(ref int offset)
		{
			byte[] array = this.packet;
			int num = offset;
			offset = num + 1;
			int num2 = array[num] << 24;
			byte[] array2 = this.packet;
			num = offset;
			offset = num + 1;
			int num3 = num2 + (array2[num] << 16);
			byte[] array3 = this.packet;
			num = offset;
			offset = num + 1;
			int num4 = num3 + (array3[num] << 8);
			byte[] array4 = this.packet;
			num = offset;
			offset = num + 1;
			return num4 + array4[num];
		}

		// Token: 0x040002AD RID: 685
		protected byte[] packet;

		// Token: 0x040002AE RID: 686
		protected int position;

		// Token: 0x040002AF RID: 687
		protected DnsHeader header;
	}
}
