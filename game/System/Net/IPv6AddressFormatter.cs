using System;
using System.Text;

namespace System.Net
{
	// Token: 0x0200069E RID: 1694
	internal struct IPv6AddressFormatter
	{
		// Token: 0x06003642 RID: 13890 RVA: 0x000BE25C File Offset: 0x000BC45C
		public IPv6AddressFormatter(ushort[] addr, long scopeId)
		{
			this.address = addr;
			this.scopeId = scopeId;
		}

		// Token: 0x06003643 RID: 13891 RVA: 0x000BE26C File Offset: 0x000BC46C
		private static ushort SwapUShort(ushort number)
		{
			return (ushort)((number >> 8 & 255) + ((int)number << 8 & 65280));
		}

		// Token: 0x06003644 RID: 13892 RVA: 0x000BE282 File Offset: 0x000BC482
		private uint AsIPv4Int()
		{
			return (uint)(((int)IPv6AddressFormatter.SwapUShort(this.address[7]) << 16) + (int)IPv6AddressFormatter.SwapUShort(this.address[6]));
		}

		// Token: 0x06003645 RID: 13893 RVA: 0x000BE2A4 File Offset: 0x000BC4A4
		private bool IsIPv4Compatible()
		{
			for (int i = 0; i < 6; i++)
			{
				if (this.address[i] != 0)
				{
					return false;
				}
			}
			return this.address[6] != 0 && this.AsIPv4Int() > 1U;
		}

		// Token: 0x06003646 RID: 13894 RVA: 0x000BE2E0 File Offset: 0x000BC4E0
		private bool IsIPv4Mapped()
		{
			for (int i = 0; i < 5; i++)
			{
				if (this.address[i] != 0)
				{
					return false;
				}
			}
			return this.address[6] != 0 && this.address[5] == ushort.MaxValue;
		}

		// Token: 0x06003647 RID: 13895 RVA: 0x000BE320 File Offset: 0x000BC520
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.IsIPv4Compatible() || this.IsIPv4Mapped())
			{
				stringBuilder.Append("::");
				if (this.IsIPv4Mapped())
				{
					stringBuilder.Append("ffff:");
				}
				stringBuilder.Append(new IPAddress((long)((ulong)this.AsIPv4Int())).ToString());
				return stringBuilder.ToString();
			}
			int num = -1;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < 8; i++)
			{
				if (this.address[i] != 0)
				{
					if (num3 > num2 && num3 > 1)
					{
						num2 = num3;
						num = i - num3;
					}
					num3 = 0;
				}
				else
				{
					num3++;
				}
			}
			if (num3 > num2 && num3 > 1)
			{
				num2 = num3;
				num = 8 - num3;
			}
			if (num == 0)
			{
				stringBuilder.Append(":");
			}
			for (int j = 0; j < 8; j++)
			{
				if (j == num)
				{
					stringBuilder.Append(":");
					j += num2 - 1;
				}
				else
				{
					stringBuilder.AppendFormat("{0:x}", this.address[j]);
					if (j < 7)
					{
						stringBuilder.Append(':');
					}
				}
			}
			if (this.scopeId != 0L)
			{
				stringBuilder.Append('%').Append(this.scopeId);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001F97 RID: 8087
		private ushort[] address;

		// Token: 0x04001F98 RID: 8088
		private long scopeId;
	}
}
