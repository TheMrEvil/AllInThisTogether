using System;

namespace System.Net.Sockets
{
	/// <summary>Defines error codes for the <see cref="T:System.Net.Sockets.Socket" /> class.</summary>
	// Token: 0x020007B2 RID: 1970
	public enum SocketError
	{
		/// <summary>The <see cref="T:System.Net.Sockets.Socket" /> operation succeeded.</summary>
		// Token: 0x04002534 RID: 9524
		Success,
		/// <summary>An unspecified <see cref="T:System.Net.Sockets.Socket" /> error has occurred.</summary>
		// Token: 0x04002535 RID: 9525
		SocketError = -1,
		/// <summary>A blocking <see cref="T:System.Net.Sockets.Socket" /> call was canceled.</summary>
		// Token: 0x04002536 RID: 9526
		Interrupted = 10004,
		/// <summary>An attempt was made to access a <see cref="T:System.Net.Sockets.Socket" /> in a way that is forbidden by its access permissions.</summary>
		// Token: 0x04002537 RID: 9527
		AccessDenied = 10013,
		/// <summary>An invalid pointer address was detected by the underlying socket provider.</summary>
		// Token: 0x04002538 RID: 9528
		Fault,
		/// <summary>An invalid argument was supplied to a <see cref="T:System.Net.Sockets.Socket" /> member.</summary>
		// Token: 0x04002539 RID: 9529
		InvalidArgument = 10022,
		/// <summary>There are too many open sockets in the underlying socket provider.</summary>
		// Token: 0x0400253A RID: 9530
		TooManyOpenSockets = 10024,
		/// <summary>An operation on a nonblocking socket cannot be completed immediately.</summary>
		// Token: 0x0400253B RID: 9531
		WouldBlock = 10035,
		/// <summary>A blocking operation is in progress.</summary>
		// Token: 0x0400253C RID: 9532
		InProgress,
		/// <summary>The nonblocking <see cref="T:System.Net.Sockets.Socket" /> already has an operation in progress.</summary>
		// Token: 0x0400253D RID: 9533
		AlreadyInProgress,
		/// <summary>A <see cref="T:System.Net.Sockets.Socket" /> operation was attempted on a non-socket.</summary>
		// Token: 0x0400253E RID: 9534
		NotSocket,
		/// <summary>A required address was omitted from an operation on a <see cref="T:System.Net.Sockets.Socket" />.</summary>
		// Token: 0x0400253F RID: 9535
		DestinationAddressRequired,
		/// <summary>The datagram is too long.</summary>
		// Token: 0x04002540 RID: 9536
		MessageSize,
		/// <summary>The protocol type is incorrect for this <see cref="T:System.Net.Sockets.Socket" />.</summary>
		// Token: 0x04002541 RID: 9537
		ProtocolType,
		/// <summary>An unknown, invalid, or unsupported option or level was used with a <see cref="T:System.Net.Sockets.Socket" />.</summary>
		// Token: 0x04002542 RID: 9538
		ProtocolOption,
		/// <summary>The protocol is not implemented or has not been configured.</summary>
		// Token: 0x04002543 RID: 9539
		ProtocolNotSupported,
		/// <summary>The support for the specified socket type does not exist in this address family.</summary>
		// Token: 0x04002544 RID: 9540
		SocketNotSupported,
		/// <summary>The address family is not supported by the protocol family.</summary>
		// Token: 0x04002545 RID: 9541
		OperationNotSupported,
		/// <summary>The protocol family is not implemented or has not been configured.</summary>
		// Token: 0x04002546 RID: 9542
		ProtocolFamilyNotSupported,
		/// <summary>The address family specified is not supported. This error is returned if the IPv6 address family was specified and the IPv6 stack is not installed on the local machine. This error is returned if the IPv4 address family was specified and the IPv4 stack is not installed on the local machine.</summary>
		// Token: 0x04002547 RID: 9543
		AddressFamilyNotSupported,
		/// <summary>Only one use of an address is normally permitted.</summary>
		// Token: 0x04002548 RID: 9544
		AddressAlreadyInUse,
		/// <summary>The selected IP address is not valid in this context.</summary>
		// Token: 0x04002549 RID: 9545
		AddressNotAvailable,
		/// <summary>The network is not available.</summary>
		// Token: 0x0400254A RID: 9546
		NetworkDown,
		/// <summary>No route to the remote host exists.</summary>
		// Token: 0x0400254B RID: 9547
		NetworkUnreachable,
		/// <summary>The application tried to set <see cref="F:System.Net.Sockets.SocketOptionName.KeepAlive" /> on a connection that has already timed out.</summary>
		// Token: 0x0400254C RID: 9548
		NetworkReset,
		/// <summary>The connection was aborted by the .NET Framework or the underlying socket provider.</summary>
		// Token: 0x0400254D RID: 9549
		ConnectionAborted,
		/// <summary>The connection was reset by the remote peer.</summary>
		// Token: 0x0400254E RID: 9550
		ConnectionReset,
		/// <summary>No free buffer space is available for a <see cref="T:System.Net.Sockets.Socket" /> operation.</summary>
		// Token: 0x0400254F RID: 9551
		NoBufferSpaceAvailable,
		/// <summary>The <see cref="T:System.Net.Sockets.Socket" /> is already connected.</summary>
		// Token: 0x04002550 RID: 9552
		IsConnected,
		/// <summary>The application tried to send or receive data, and the <see cref="T:System.Net.Sockets.Socket" /> is not connected.</summary>
		// Token: 0x04002551 RID: 9553
		NotConnected,
		/// <summary>A request to send or receive data was disallowed because the <see cref="T:System.Net.Sockets.Socket" /> has already been closed.</summary>
		// Token: 0x04002552 RID: 9554
		Shutdown,
		/// <summary>The connection attempt timed out, or the connected host has failed to respond.</summary>
		// Token: 0x04002553 RID: 9555
		TimedOut = 10060,
		/// <summary>The remote host is actively refusing a connection.</summary>
		// Token: 0x04002554 RID: 9556
		ConnectionRefused,
		/// <summary>The operation failed because the remote host is down.</summary>
		// Token: 0x04002555 RID: 9557
		HostDown = 10064,
		/// <summary>There is no network route to the specified host.</summary>
		// Token: 0x04002556 RID: 9558
		HostUnreachable,
		/// <summary>Too many processes are using the underlying socket provider.</summary>
		// Token: 0x04002557 RID: 9559
		ProcessLimit = 10067,
		/// <summary>The network subsystem is unavailable.</summary>
		// Token: 0x04002558 RID: 9560
		SystemNotReady = 10091,
		/// <summary>The version of the underlying socket provider is out of range.</summary>
		// Token: 0x04002559 RID: 9561
		VersionNotSupported,
		/// <summary>The underlying socket provider has not been initialized.</summary>
		// Token: 0x0400255A RID: 9562
		NotInitialized,
		/// <summary>A graceful shutdown is in progress.</summary>
		// Token: 0x0400255B RID: 9563
		Disconnecting = 10101,
		/// <summary>The specified class was not found.</summary>
		// Token: 0x0400255C RID: 9564
		TypeNotFound = 10109,
		/// <summary>No such host is known. The name is not an official host name or alias.</summary>
		// Token: 0x0400255D RID: 9565
		HostNotFound = 11001,
		/// <summary>The name of the host could not be resolved. Try again later.</summary>
		// Token: 0x0400255E RID: 9566
		TryAgain,
		/// <summary>The error is unrecoverable or the requested database cannot be located.</summary>
		// Token: 0x0400255F RID: 9567
		NoRecovery,
		/// <summary>The requested name or IP address was not found on the name server.</summary>
		// Token: 0x04002560 RID: 9568
		NoData,
		/// <summary>The application has initiated an overlapped operation that cannot be completed immediately.</summary>
		// Token: 0x04002561 RID: 9569
		IOPending = 997,
		/// <summary>The overlapped operation was aborted due to the closure of the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		// Token: 0x04002562 RID: 9570
		OperationAborted = 995
	}
}
