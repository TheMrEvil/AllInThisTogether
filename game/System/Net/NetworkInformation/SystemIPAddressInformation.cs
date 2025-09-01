using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000702 RID: 1794
	internal class SystemIPAddressInformation : IPAddressInformation
	{
		// Token: 0x06003984 RID: 14724 RVA: 0x000C8D34 File Offset: 0x000C6F34
		public SystemIPAddressInformation(IPAddress address, bool isDnsEligible, bool isTransient)
		{
			this.address = address;
			this.dnsEligible = isDnsEligible;
			this.transient = isTransient;
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06003985 RID: 14725 RVA: 0x000C8D58 File Offset: 0x000C6F58
		public override IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06003986 RID: 14726 RVA: 0x000C8D60 File Offset: 0x000C6F60
		public override bool IsTransient
		{
			get
			{
				return this.transient;
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06003987 RID: 14727 RVA: 0x000C8D68 File Offset: 0x000C6F68
		public override bool IsDnsEligible
		{
			get
			{
				return this.dnsEligible;
			}
		}

		// Token: 0x040021C7 RID: 8647
		private IPAddress address;

		// Token: 0x040021C8 RID: 8648
		internal bool transient;

		// Token: 0x040021C9 RID: 8649
		internal bool dnsEligible = true;
	}
}
