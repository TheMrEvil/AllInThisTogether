using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000703 RID: 1795
	internal class SystemMulticastIPAddressInformation : MulticastIPAddressInformation
	{
		// Token: 0x06003988 RID: 14728 RVA: 0x000C8D70 File Offset: 0x000C6F70
		private SystemMulticastIPAddressInformation()
		{
		}

		// Token: 0x06003989 RID: 14729 RVA: 0x000C8D78 File Offset: 0x000C6F78
		public SystemMulticastIPAddressInformation(SystemIPAddressInformation addressInfo)
		{
			this.innerInfo = addressInfo;
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x0600398A RID: 14730 RVA: 0x000C8D87 File Offset: 0x000C6F87
		public override IPAddress Address
		{
			get
			{
				return this.innerInfo.Address;
			}
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x0600398B RID: 14731 RVA: 0x000C8D94 File Offset: 0x000C6F94
		public override bool IsTransient
		{
			get
			{
				return this.innerInfo.IsTransient;
			}
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x0600398C RID: 14732 RVA: 0x000C8DA1 File Offset: 0x000C6FA1
		public override bool IsDnsEligible
		{
			get
			{
				return this.innerInfo.IsDnsEligible;
			}
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x0600398D RID: 14733 RVA: 0x00003062 File Offset: 0x00001262
		public override PrefixOrigin PrefixOrigin
		{
			get
			{
				return PrefixOrigin.Other;
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x0600398E RID: 14734 RVA: 0x00003062 File Offset: 0x00001262
		public override SuffixOrigin SuffixOrigin
		{
			get
			{
				return SuffixOrigin.Other;
			}
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x0600398F RID: 14735 RVA: 0x00003062 File Offset: 0x00001262
		public override DuplicateAddressDetectionState DuplicateAddressDetectionState
		{
			get
			{
				return DuplicateAddressDetectionState.Invalid;
			}
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x06003990 RID: 14736 RVA: 0x0004D0A0 File Offset: 0x0004B2A0
		public override long AddressValidLifetime
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x06003991 RID: 14737 RVA: 0x0004D0A0 File Offset: 0x0004B2A0
		public override long AddressPreferredLifetime
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x06003992 RID: 14738 RVA: 0x0004D0A0 File Offset: 0x0004B2A0
		public override long DhcpLeaseLifetime
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x06003993 RID: 14739 RVA: 0x000C8DB0 File Offset: 0x000C6FB0
		internal static MulticastIPAddressInformationCollection ToMulticastIpAddressInformationCollection(IPAddressInformationCollection addresses)
		{
			MulticastIPAddressInformationCollection multicastIPAddressInformationCollection = new MulticastIPAddressInformationCollection();
			foreach (IPAddressInformation ipaddressInformation in addresses)
			{
				multicastIPAddressInformationCollection.InternalAdd(new SystemMulticastIPAddressInformation((SystemIPAddressInformation)ipaddressInformation));
			}
			return multicastIPAddressInformationCollection;
		}

		// Token: 0x040021CA RID: 8650
		private SystemIPAddressInformation innerInfo;
	}
}
