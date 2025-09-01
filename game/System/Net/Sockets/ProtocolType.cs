using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies the protocols that the <see cref="T:System.Net.Sockets.Socket" /> class supports.</summary>
	// Token: 0x020007AD RID: 1965
	public enum ProtocolType
	{
		/// <summary>Internet Protocol.</summary>
		// Token: 0x04002503 RID: 9475
		IP,
		/// <summary>IPv6 Hop by Hop Options header.</summary>
		// Token: 0x04002504 RID: 9476
		IPv6HopByHopOptions = 0,
		/// <summary>Internet Control Message Protocol.</summary>
		// Token: 0x04002505 RID: 9477
		Icmp,
		/// <summary>Internet Group Management Protocol.</summary>
		// Token: 0x04002506 RID: 9478
		Igmp,
		/// <summary>Gateway To Gateway Protocol.</summary>
		// Token: 0x04002507 RID: 9479
		Ggp,
		/// <summary>Internet Protocol version 4.</summary>
		// Token: 0x04002508 RID: 9480
		IPv4,
		/// <summary>Transmission Control Protocol.</summary>
		// Token: 0x04002509 RID: 9481
		Tcp = 6,
		/// <summary>PARC Universal Packet Protocol.</summary>
		// Token: 0x0400250A RID: 9482
		Pup = 12,
		/// <summary>User Datagram Protocol.</summary>
		// Token: 0x0400250B RID: 9483
		Udp = 17,
		/// <summary>Internet Datagram Protocol.</summary>
		// Token: 0x0400250C RID: 9484
		Idp = 22,
		/// <summary>Internet Protocol version 6 (IPv6).</summary>
		// Token: 0x0400250D RID: 9485
		IPv6 = 41,
		/// <summary>IPv6 Routing header.</summary>
		// Token: 0x0400250E RID: 9486
		IPv6RoutingHeader = 43,
		/// <summary>IPv6 Fragment header.</summary>
		// Token: 0x0400250F RID: 9487
		IPv6FragmentHeader,
		/// <summary>IPv6 Encapsulating Security Payload header.</summary>
		// Token: 0x04002510 RID: 9488
		IPSecEncapsulatingSecurityPayload = 50,
		/// <summary>IPv6 Authentication header. For details, see RFC 2292 section 2.2.1, available at https://www.ietf.org.</summary>
		// Token: 0x04002511 RID: 9489
		IPSecAuthenticationHeader,
		/// <summary>Internet Control Message Protocol for IPv6.</summary>
		// Token: 0x04002512 RID: 9490
		IcmpV6 = 58,
		/// <summary>IPv6 No next header.</summary>
		// Token: 0x04002513 RID: 9491
		IPv6NoNextHeader,
		/// <summary>IPv6 Destination Options header.</summary>
		// Token: 0x04002514 RID: 9492
		IPv6DestinationOptions,
		/// <summary>Net Disk Protocol (unofficial).</summary>
		// Token: 0x04002515 RID: 9493
		ND = 77,
		/// <summary>Raw IP packet protocol.</summary>
		// Token: 0x04002516 RID: 9494
		Raw = 255,
		/// <summary>Unspecified protocol.</summary>
		// Token: 0x04002517 RID: 9495
		Unspecified = 0,
		/// <summary>Internet Packet Exchange Protocol.</summary>
		// Token: 0x04002518 RID: 9496
		Ipx = 1000,
		/// <summary>Sequenced Packet Exchange protocol.</summary>
		// Token: 0x04002519 RID: 9497
		Spx = 1256,
		/// <summary>Sequenced Packet Exchange version 2 protocol.</summary>
		// Token: 0x0400251A RID: 9498
		SpxII,
		/// <summary>Unknown protocol.</summary>
		// Token: 0x0400251B RID: 9499
		Unknown = -1
	}
}
