using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000701 RID: 1793
	internal class SystemGatewayIPAddressInformation : GatewayIPAddressInformation
	{
		// Token: 0x06003981 RID: 14721 RVA: 0x000C8CC8 File Offset: 0x000C6EC8
		internal SystemGatewayIPAddressInformation(IPAddress address)
		{
			this.address = address;
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06003982 RID: 14722 RVA: 0x000C8CD7 File Offset: 0x000C6ED7
		public override IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x06003983 RID: 14723 RVA: 0x000C8CE0 File Offset: 0x000C6EE0
		internal static GatewayIPAddressInformationCollection ToGatewayIpAddressInformationCollection(IPAddressCollection addresses)
		{
			GatewayIPAddressInformationCollection gatewayIPAddressInformationCollection = new GatewayIPAddressInformationCollection();
			foreach (IPAddress ipaddress in addresses)
			{
				gatewayIPAddressInformationCollection.InternalAdd(new SystemGatewayIPAddressInformation(ipaddress));
			}
			return gatewayIPAddressInformationCollection;
		}

		// Token: 0x040021C6 RID: 8646
		private IPAddress address;
	}
}
