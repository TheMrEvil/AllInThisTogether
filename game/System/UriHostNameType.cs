using System;

namespace System
{
	/// <summary>Defines host name types for the <see cref="M:System.Uri.CheckHostName(System.String)" /> method.</summary>
	// Token: 0x0200015D RID: 349
	public enum UriHostNameType
	{
		/// <summary>The type of the host name is not supplied.</summary>
		// Token: 0x0400064C RID: 1612
		Unknown,
		/// <summary>The host is set, but the type cannot be determined.</summary>
		// Token: 0x0400064D RID: 1613
		Basic,
		/// <summary>The host name is a domain name system (DNS) style host name.</summary>
		// Token: 0x0400064E RID: 1614
		Dns,
		/// <summary>The host name is an Internet Protocol (IP) version 4 host address.</summary>
		// Token: 0x0400064F RID: 1615
		IPv4,
		/// <summary>The host name is an Internet Protocol (IP) version 6 host address.</summary>
		// Token: 0x04000650 RID: 1616
		IPv6
	}
}
