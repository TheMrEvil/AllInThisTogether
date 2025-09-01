using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies the states of a Transmission Control Protocol (TCP) connection.</summary>
	// Token: 0x02000706 RID: 1798
	public enum TcpState
	{
		/// <summary>The TCP connection state is unknown.</summary>
		// Token: 0x040021CF RID: 8655
		Unknown,
		/// <summary>The TCP connection is closed.</summary>
		// Token: 0x040021D0 RID: 8656
		Closed,
		/// <summary>The local endpoint of the TCP connection is listening for a connection request from any remote endpoint.</summary>
		// Token: 0x040021D1 RID: 8657
		Listen,
		/// <summary>The local endpoint of the TCP connection has sent the remote endpoint a segment header with the synchronize (SYN) control bit set and is waiting for a matching connection request.</summary>
		// Token: 0x040021D2 RID: 8658
		SynSent,
		/// <summary>The local endpoint of the TCP connection has sent and received a connection request and is waiting for an acknowledgment.</summary>
		// Token: 0x040021D3 RID: 8659
		SynReceived,
		/// <summary>The TCP handshake is complete. The connection has been established and data can be sent.</summary>
		// Token: 0x040021D4 RID: 8660
		Established,
		/// <summary>The local endpoint of the TCP connection is waiting for a connection termination request from the remote endpoint or for an acknowledgement of the connection termination request sent previously.</summary>
		// Token: 0x040021D5 RID: 8661
		FinWait1,
		/// <summary>The local endpoint of the TCP connection is waiting for a connection termination request from the remote endpoint.</summary>
		// Token: 0x040021D6 RID: 8662
		FinWait2,
		/// <summary>The local endpoint of the TCP connection is waiting for a connection termination request from the local user.</summary>
		// Token: 0x040021D7 RID: 8663
		CloseWait,
		/// <summary>The local endpoint of the TCP connection is waiting for an acknowledgement of the connection termination request sent previously.</summary>
		// Token: 0x040021D8 RID: 8664
		Closing,
		/// <summary>The local endpoint of the TCP connection is waiting for the final acknowledgement of the connection termination request sent previously.</summary>
		// Token: 0x040021D9 RID: 8665
		LastAck,
		/// <summary>The local endpoint of the TCP connection is waiting for enough time to pass to ensure that the remote endpoint received the acknowledgement of its connection termination request.</summary>
		// Token: 0x040021DA RID: 8666
		TimeWait,
		/// <summary>The transmission control buffer (TCB) for the TCP connection is being deleted.</summary>
		// Token: 0x040021DB RID: 8667
		DeleteTcb
	}
}
