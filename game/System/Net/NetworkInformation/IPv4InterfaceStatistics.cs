using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides statistical data for a network interface on the local computer.</summary>
	// Token: 0x020006E6 RID: 1766
	public abstract class IPv4InterfaceStatistics
	{
		/// <summary>Gets the number of bytes that were received on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of bytes that were received on the interface.</returns>
		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x060038D0 RID: 14544
		public abstract long BytesReceived { get; }

		/// <summary>Gets the number of bytes that were sent on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of bytes that were transmitted on the interface.</returns>
		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x060038D1 RID: 14545
		public abstract long BytesSent { get; }

		/// <summary>Gets the number of incoming packets that were discarded.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of discarded incoming packets.</returns>
		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x060038D2 RID: 14546
		public abstract long IncomingPacketsDiscarded { get; }

		/// <summary>Gets the number of incoming packets with errors.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of incoming packets with errors.</returns>
		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x060038D3 RID: 14547
		public abstract long IncomingPacketsWithErrors { get; }

		/// <summary>Gets the number of incoming packets with an unknown protocol that were received on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of incoming packets with an unknown protocol.</returns>
		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x060038D4 RID: 14548
		public abstract long IncomingUnknownProtocolPackets { get; }

		/// <summary>Gets the number of non-unicast packets that were received on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of non-unicast packets that were received on the interface.</returns>
		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x060038D5 RID: 14549
		public abstract long NonUnicastPacketsReceived { get; }

		/// <summary>Gets the number of non-unicast packets that were sent on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of non-unicast packets that were sent on the interface.</returns>
		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x060038D6 RID: 14550
		public abstract long NonUnicastPacketsSent { get; }

		/// <summary>Gets the number of outgoing packets that were discarded.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of discarded outgoing packets.</returns>
		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x060038D7 RID: 14551
		public abstract long OutgoingPacketsDiscarded { get; }

		/// <summary>Gets the number of outgoing packets with errors.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of outgoing packets with errors.</returns>
		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x060038D8 RID: 14552
		public abstract long OutgoingPacketsWithErrors { get; }

		/// <summary>Gets the length of the output queue.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of packets in the output queue.</returns>
		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x060038D9 RID: 14553
		public abstract long OutputQueueLength { get; }

		/// <summary>Gets the number of unicast packets that were received on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of unicast packets that were received on the interface.</returns>
		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x060038DA RID: 14554
		public abstract long UnicastPacketsReceived { get; }

		/// <summary>Gets the number of unicast packets that were sent on the interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of unicast packets that were sent on the interface.</returns>
		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x060038DB RID: 14555
		public abstract long UnicastPacketsSent { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.IPv4InterfaceStatistics" /> class.</summary>
		// Token: 0x060038DC RID: 14556 RVA: 0x0000219B File Offset: 0x0000039B
		protected IPv4InterfaceStatistics()
		{
		}
	}
}
