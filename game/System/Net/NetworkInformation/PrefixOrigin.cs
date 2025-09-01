using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies how an IP address network prefix was located.</summary>
	// Token: 0x020006FE RID: 1790
	public enum PrefixOrigin
	{
		/// <summary>The prefix was located using an unspecified source.</summary>
		// Token: 0x040021B1 RID: 8625
		Other,
		/// <summary>The prefix was manually configured.</summary>
		// Token: 0x040021B2 RID: 8626
		Manual,
		/// <summary>The prefix is a well-known prefix. Well-known prefixes are specified in standard-track Request for Comments (RFC) documents and assigned by the Internet Assigned Numbers Authority (Iana) or an address registry. Such prefixes are reserved for special purposes.</summary>
		// Token: 0x040021B3 RID: 8627
		WellKnown,
		/// <summary>The prefix was supplied by a Dynamic Host Configuration Protocol (DHCP) server.</summary>
		// Token: 0x040021B4 RID: 8628
		Dhcp,
		/// <summary>The prefix was supplied by a router advertisement.</summary>
		// Token: 0x040021B5 RID: 8629
		RouterAdvertisement
	}
}
