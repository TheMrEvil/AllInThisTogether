using System;
using System.Net.Sockets;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000754 RID: 1876
	internal class Win32UnicastIPAddressInformation : UnicastIPAddressInformation
	{
		// Token: 0x06003B3E RID: 15166 RVA: 0x000CBFDC File Offset: 0x000CA1DC
		public Win32UnicastIPAddressInformation(Win32_IP_ADAPTER_UNICAST_ADDRESS info)
		{
			this.info = info;
			IPAddress ipaddress = info.Address.GetIPAddress();
			if (ipaddress.AddressFamily == AddressFamily.InterNetwork)
			{
				this.ipv4Mask = Win32UnicastIPAddressInformation.PrefixLengthToSubnetMask(info.OnLinkPrefixLength, ipaddress.AddressFamily);
			}
		}

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06003B3F RID: 15167 RVA: 0x000CC023 File Offset: 0x000CA223
		public override IPAddress Address
		{
			get
			{
				return this.info.Address.GetIPAddress();
			}
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06003B40 RID: 15168 RVA: 0x000CC035 File Offset: 0x000CA235
		public override bool IsDnsEligible
		{
			get
			{
				return this.info.LengthFlags.IsDnsEligible;
			}
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06003B41 RID: 15169 RVA: 0x000CC047 File Offset: 0x000CA247
		public override bool IsTransient
		{
			get
			{
				return this.info.LengthFlags.IsTransient;
			}
		}

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06003B42 RID: 15170 RVA: 0x000CC059 File Offset: 0x000CA259
		public override long AddressPreferredLifetime
		{
			get
			{
				return (long)((ulong)this.info.PreferredLifetime);
			}
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x06003B43 RID: 15171 RVA: 0x000CC067 File Offset: 0x000CA267
		public override long AddressValidLifetime
		{
			get
			{
				return (long)((ulong)this.info.ValidLifetime);
			}
		}

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x06003B44 RID: 15172 RVA: 0x000CC075 File Offset: 0x000CA275
		public override long DhcpLeaseLifetime
		{
			get
			{
				return (long)((ulong)this.info.LeaseLifetime);
			}
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x06003B45 RID: 15173 RVA: 0x000CC083 File Offset: 0x000CA283
		public override DuplicateAddressDetectionState DuplicateAddressDetectionState
		{
			get
			{
				return this.info.DadState;
			}
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x06003B46 RID: 15174 RVA: 0x000CC090 File Offset: 0x000CA290
		public override IPAddress IPv4Mask
		{
			get
			{
				if (this.Address.AddressFamily != AddressFamily.InterNetwork)
				{
					return IPAddress.Any;
				}
				return this.ipv4Mask;
			}
		}

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x06003B47 RID: 15175 RVA: 0x000CC0AC File Offset: 0x000CA2AC
		public override PrefixOrigin PrefixOrigin
		{
			get
			{
				return this.info.PrefixOrigin;
			}
		}

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x06003B48 RID: 15176 RVA: 0x000CC0B9 File Offset: 0x000CA2B9
		public override SuffixOrigin SuffixOrigin
		{
			get
			{
				return this.info.SuffixOrigin;
			}
		}

		// Token: 0x06003B49 RID: 15177 RVA: 0x000CC0C8 File Offset: 0x000CA2C8
		private static IPAddress PrefixLengthToSubnetMask(byte prefixLength, AddressFamily family)
		{
			byte[] array;
			if (family == AddressFamily.InterNetwork)
			{
				array = new byte[4];
			}
			else
			{
				array = new byte[16];
			}
			for (int i = 0; i < (int)prefixLength; i++)
			{
				byte[] array2 = array;
				int num = i / 8;
				array2[num] |= (byte)(128 >> i % 8);
			}
			return new IPAddress(array);
		}

		// Token: 0x04002361 RID: 9057
		private Win32_IP_ADAPTER_UNICAST_ADDRESS info;

		// Token: 0x04002362 RID: 9058
		private IPAddress ipv4Mask;
	}
}
