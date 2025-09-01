using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies how an IP address host suffix was located.</summary>
	// Token: 0x02000700 RID: 1792
	public enum SuffixOrigin
	{
		/// <summary>The suffix was located using an unspecified source.</summary>
		// Token: 0x040021C0 RID: 8640
		Other,
		/// <summary>The suffix was manually configured.</summary>
		// Token: 0x040021C1 RID: 8641
		Manual,
		/// <summary>The suffix is a well-known suffix. Well-known suffixes are specified in standard-track Request for Comments (RFC) documents and assigned by the Internet Assigned Numbers Authority (Iana) or an address registry. Such suffixes are reserved for special purposes.</summary>
		// Token: 0x040021C2 RID: 8642
		WellKnown,
		/// <summary>The suffix was supplied by a Dynamic Host Configuration Protocol (DHCP) server.</summary>
		// Token: 0x040021C3 RID: 8643
		OriginDhcp,
		/// <summary>The suffix is a link-local suffix.</summary>
		// Token: 0x040021C4 RID: 8644
		LinkLayerAddress,
		/// <summary>The suffix was randomly assigned.</summary>
		// Token: 0x040021C5 RID: 8645
		Random
	}
}
