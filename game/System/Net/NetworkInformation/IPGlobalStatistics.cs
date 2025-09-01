using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides Internet Protocol (IP) statistical data.</summary>
	// Token: 0x020006E3 RID: 1763
	public abstract class IPGlobalStatistics
	{
		/// <summary>Gets the default time-to-live (TTL) value for Internet Protocol (IP) packets.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the TTL.</returns>
		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x0600389F RID: 14495
		public abstract int DefaultTtl { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that specifies whether Internet Protocol (IP) packet forwarding is enabled.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that specifies whether packet forwarding is enabled.</returns>
		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x060038A0 RID: 14496
		public abstract bool ForwardingEnabled { get; }

		/// <summary>Gets the number of network interfaces.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value containing the number of network interfaces for the address family used to obtain this <see cref="T:System.Net.NetworkInformation.IPGlobalStatistics" /> instance.</returns>
		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x060038A1 RID: 14497
		public abstract int NumberOfInterfaces { get; }

		/// <summary>Gets the number of Internet Protocol (IP) addresses assigned to the local computer.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of IP addresses assigned to the address family (Internet Protocol version 4 or Internet Protocol version 6) described by this object.</returns>
		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x060038A2 RID: 14498
		public abstract int NumberOfIPAddresses { get; }

		/// <summary>Gets the number of outbound Internet Protocol (IP) packets.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of outgoing packets.</returns>
		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x060038A3 RID: 14499
		public abstract long OutputPacketRequests { get; }

		/// <summary>Gets the number of routes that have been discarded from the routing table.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of valid routes that have been discarded.</returns>
		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x060038A4 RID: 14500
		public abstract long OutputPacketRoutingDiscards { get; }

		/// <summary>Gets the number of transmitted Internet Protocol (IP) packets that have been discarded.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of outgoing packets that have been discarded.</returns>
		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x060038A5 RID: 14501
		public abstract long OutputPacketsDiscarded { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets for which the local computer could not determine a route to the destination address.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the number of packets that could not be sent because a route could not be found.</returns>
		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x060038A6 RID: 14502
		public abstract long OutputPacketsWithNoRoute { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets that could not be fragmented.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of packets that required fragmentation but had the "Don't Fragment" bit set.</returns>
		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x060038A7 RID: 14503
		public abstract long PacketFragmentFailures { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets that required reassembly.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of packet reassemblies required.</returns>
		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x060038A8 RID: 14504
		public abstract long PacketReassembliesRequired { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets that were not successfully reassembled.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of packets that could not be reassembled.</returns>
		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x060038A9 RID: 14505
		public abstract long PacketReassemblyFailures { get; }

		/// <summary>Gets the maximum amount of time within which all fragments of an Internet Protocol (IP) packet must arrive.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the maximum number of milliseconds within which all fragments of a packet must arrive to avoid being discarded.</returns>
		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x060038AA RID: 14506
		public abstract long PacketReassemblyTimeout { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets fragmented.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of fragmented packets.</returns>
		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x060038AB RID: 14507
		public abstract long PacketsFragmented { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets reassembled.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of fragmented packets that have been successfully reassembled.</returns>
		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x060038AC RID: 14508
		public abstract long PacketsReassembled { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of IP packets received.</returns>
		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x060038AD RID: 14509
		public abstract long ReceivedPackets { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets delivered.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of IP packets delivered.</returns>
		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x060038AE RID: 14510
		public abstract long ReceivedPacketsDelivered { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets that have been received and discarded.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of incoming packets that have been discarded.</returns>
		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x060038AF RID: 14511
		public abstract long ReceivedPacketsDiscarded { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets forwarded.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of forwarded packets.</returns>
		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x060038B0 RID: 14512
		public abstract long ReceivedPacketsForwarded { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets with address errors that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of IP packets received with errors in the address portion of the header.</returns>
		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x060038B1 RID: 14513
		public abstract long ReceivedPacketsWithAddressErrors { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets with header errors that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of IP packets received and discarded due to errors in the header.</returns>
		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x060038B2 RID: 14514
		public abstract long ReceivedPacketsWithHeadersErrors { get; }

		/// <summary>Gets the number of Internet Protocol (IP) packets received on the local machine with an unknown protocol in the header.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the total number of IP packets received with an unknown protocol.</returns>
		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x060038B3 RID: 14515
		public abstract long ReceivedPacketsWithUnknownProtocol { get; }

		/// <summary>Gets the number of routes in the Internet Protocol (IP) routing table.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of routes in the routing table.</returns>
		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x060038B4 RID: 14516
		public abstract int NumberOfRoutes { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.IPGlobalStatistics" /> class.</summary>
		// Token: 0x060038B5 RID: 14517 RVA: 0x0000219B File Offset: 0x0000039B
		protected IPGlobalStatistics()
		{
		}
	}
}
