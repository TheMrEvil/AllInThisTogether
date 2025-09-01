using System;

namespace System.Net.Sockets
{
	/// <summary>Defines configuration option names.</summary>
	// Token: 0x020007B7 RID: 1975
	public enum SocketOptionName
	{
		/// <summary>Record debugging information.</summary>
		// Token: 0x0400257D RID: 9597
		Debug = 1,
		/// <summary>The socket is listening.</summary>
		// Token: 0x0400257E RID: 9598
		AcceptConnection,
		/// <summary>Allows the socket to be bound to an address that is already in use.</summary>
		// Token: 0x0400257F RID: 9599
		ReuseAddress = 4,
		/// <summary>Use keep-alives.</summary>
		// Token: 0x04002580 RID: 9600
		KeepAlive = 8,
		/// <summary>Do not route; send the packet directly to the interface addresses.</summary>
		// Token: 0x04002581 RID: 9601
		DontRoute = 16,
		/// <summary>Permit sending broadcast messages on the socket.</summary>
		// Token: 0x04002582 RID: 9602
		Broadcast = 32,
		/// <summary>Bypass hardware when possible.</summary>
		// Token: 0x04002583 RID: 9603
		UseLoopback = 64,
		/// <summary>Linger on close if unsent data is present.</summary>
		// Token: 0x04002584 RID: 9604
		Linger = 128,
		/// <summary>Receives out-of-band data in the normal data stream.</summary>
		// Token: 0x04002585 RID: 9605
		OutOfBandInline = 256,
		/// <summary>Close the socket gracefully without lingering.</summary>
		// Token: 0x04002586 RID: 9606
		DontLinger = -129,
		/// <summary>Enables a socket to be bound for exclusive access.</summary>
		// Token: 0x04002587 RID: 9607
		ExclusiveAddressUse = -5,
		/// <summary>Specifies the total per-socket buffer space reserved for sends. This is unrelated to the maximum message size or the size of a TCP window.</summary>
		// Token: 0x04002588 RID: 9608
		SendBuffer = 4097,
		/// <summary>Specifies the total per-socket buffer space reserved for receives. This is unrelated to the maximum message size or the size of a TCP window.</summary>
		// Token: 0x04002589 RID: 9609
		ReceiveBuffer,
		/// <summary>Specifies the low water mark for <see cref="Overload:System.Net.Sockets.Socket.Send" /> operations.</summary>
		// Token: 0x0400258A RID: 9610
		SendLowWater,
		/// <summary>Specifies the low water mark for <see cref="Overload:System.Net.Sockets.Socket.Receive" /> operations.</summary>
		// Token: 0x0400258B RID: 9611
		ReceiveLowWater,
		/// <summary>Send a time-out. This option applies only to synchronous methods; it has no effect on asynchronous methods such as the <see cref="M:System.Net.Sockets.Socket.BeginSend(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object)" /> method.</summary>
		// Token: 0x0400258C RID: 9612
		SendTimeout,
		/// <summary>Receive a time-out. This option applies only to synchronous methods; it has no effect on asynchronous methods such as the <see cref="M:System.Net.Sockets.Socket.BeginSend(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object)" /> method.</summary>
		// Token: 0x0400258D RID: 9613
		ReceiveTimeout,
		/// <summary>Gets the error status and clear.</summary>
		// Token: 0x0400258E RID: 9614
		Error,
		/// <summary>Gets the socket type.</summary>
		// Token: 0x0400258F RID: 9615
		Type,
		/// <summary>Indicates that the system should defer ephemeral port allocation for outbound connections. This is equivalent to using the Winsock2 SO_REUSE_UNICASTPORT socket option.</summary>
		// Token: 0x04002590 RID: 9616
		ReuseUnicastPort = 12295,
		/// <summary>Not supported; will throw a <see cref="T:System.Net.Sockets.SocketException" /> if used.</summary>
		// Token: 0x04002591 RID: 9617
		MaxConnections = 2147483647,
		/// <summary>Specifies the IP options to be inserted into outgoing datagrams.</summary>
		// Token: 0x04002592 RID: 9618
		IPOptions = 1,
		/// <summary>Indicates that the application provides the IP header for outgoing datagrams.</summary>
		// Token: 0x04002593 RID: 9619
		HeaderIncluded,
		/// <summary>Change the IP header type of the service field.</summary>
		// Token: 0x04002594 RID: 9620
		TypeOfService,
		/// <summary>Set the IP header Time-to-Live field.</summary>
		// Token: 0x04002595 RID: 9621
		IpTimeToLive,
		/// <summary>Set the interface for outgoing multicast packets.</summary>
		// Token: 0x04002596 RID: 9622
		MulticastInterface = 9,
		/// <summary>An IP multicast Time to Live.</summary>
		// Token: 0x04002597 RID: 9623
		MulticastTimeToLive,
		/// <summary>An IP multicast loopback.</summary>
		// Token: 0x04002598 RID: 9624
		MulticastLoopback,
		/// <summary>Add an IP group membership.</summary>
		// Token: 0x04002599 RID: 9625
		AddMembership,
		/// <summary>Drop an IP group membership.</summary>
		// Token: 0x0400259A RID: 9626
		DropMembership,
		/// <summary>Do not fragment IP datagrams.</summary>
		// Token: 0x0400259B RID: 9627
		DontFragment,
		/// <summary>Join a source group.</summary>
		// Token: 0x0400259C RID: 9628
		AddSourceMembership,
		/// <summary>Drop a source group.</summary>
		// Token: 0x0400259D RID: 9629
		DropSourceMembership,
		/// <summary>Block data from a source.</summary>
		// Token: 0x0400259E RID: 9630
		BlockSource,
		/// <summary>Unblock a previously blocked source.</summary>
		// Token: 0x0400259F RID: 9631
		UnblockSource,
		/// <summary>Return information about received packets.</summary>
		// Token: 0x040025A0 RID: 9632
		PacketInformation,
		/// <summary>Specifies the maximum number of router hops for an Internet Protocol version 6 (IPv6) packet. This is similar to Time to Live (TTL) for Internet Protocol version 4.</summary>
		// Token: 0x040025A1 RID: 9633
		HopLimit = 21,
		/// <summary>Enables restriction of a IPv6 socket to a specified scope, such as addresses with the same link local or site local prefix.This socket option enables applications to place access restrictions on IPv6 sockets. Such restrictions enable an application running on a private LAN to simply and robustly harden itself against external attacks. This socket option widens or narrows the scope of a listening socket, enabling unrestricted access from public and private users when appropriate, or restricting access only to the same site, as required. This socket option has defined protection levels specified in the <see cref="T:System.Net.Sockets.IPProtectionLevel" /> enumeration.</summary>
		// Token: 0x040025A2 RID: 9634
		IPProtectionLevel = 23,
		/// <summary>Indicates if a socket created for the AF_INET6 address family is restricted to IPv6 communications only. Sockets created for the AF_INET6 address family may be used for both IPv6 and IPv4 communications. Some applications may want to restrict their use of a socket created for the AF_INET6 address family to IPv6 communications only. When this value is non-zero (the default on Windows), a socket created for the AF_INET6 address family can be used to send and receive IPv6 packets only. When this value is zero, a socket created for the AF_INET6 address family can be used to send and receive packets to and from an IPv6 address or an IPv4 address. Note that the ability to interact with an IPv4 address requires the use of IPv4 mapped addresses. This socket option is supported on Windows Vista or later.</summary>
		// Token: 0x040025A3 RID: 9635
		IPv6Only = 27,
		/// <summary>Disables the Nagle algorithm for send coalescing.</summary>
		// Token: 0x040025A4 RID: 9636
		NoDelay = 1,
		/// <summary>Use urgent data as defined in RFC-1222. This option can be set only once; after it is set, it cannot be turned off.</summary>
		// Token: 0x040025A5 RID: 9637
		BsdUrgent,
		/// <summary>Use expedited data as defined in RFC-1222. This option can be set only once; after it is set, it cannot be turned off.</summary>
		// Token: 0x040025A6 RID: 9638
		Expedited = 2,
		/// <summary>Send UDP datagrams with checksum set to zero.</summary>
		// Token: 0x040025A7 RID: 9639
		NoChecksum = 1,
		/// <summary>Set or get the UDP checksum coverage.</summary>
		// Token: 0x040025A8 RID: 9640
		ChecksumCoverage = 20,
		/// <summary>Updates an accepted socket's properties by using those of an existing socket. This is equivalent to using the Winsock2 SO_UPDATE_ACCEPT_CONTEXT socket option and is supported only on connection-oriented sockets.</summary>
		// Token: 0x040025A9 RID: 9641
		UpdateAcceptContext = 28683,
		/// <summary>Updates a connected socket's properties by using those of an existing socket. This is equivalent to using the Winsock2 SO_UPDATE_CONNECT_CONTEXT socket option and is supported only on connection-oriented sockets.</summary>
		// Token: 0x040025AA RID: 9642
		UpdateConnectContext = 28688
	}
}
