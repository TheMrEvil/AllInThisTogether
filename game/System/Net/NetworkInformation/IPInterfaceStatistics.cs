using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides Internet Protocol (IP) statistical data for an network interface on the local computer.</summary>
	// Token: 0x020006E5 RID: 1765
	public abstract class IPInterfaceStatistics
	{
		/// <summary>Gets the number of bytes that were received on the interface.</summary>
		/// <returns>The total number of bytes that were received on the interface.</returns>
		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x060038C3 RID: 14531
		public abstract long BytesReceived { get; }

		/// <summary>Gets the number of bytes that were sent on the interface.</summary>
		/// <returns>The total number of bytes that were sent on the interface.</returns>
		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x060038C4 RID: 14532
		public abstract long BytesSent { get; }

		/// <summary>Gets the number of incoming packets that were discarded.</summary>
		/// <returns>The total number of incoming packets that were discarded.</returns>
		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x060038C5 RID: 14533
		public abstract long IncomingPacketsDiscarded { get; }

		/// <summary>Gets the number of incoming packets with errors.</summary>
		/// <returns>The total number of incoming packets with errors.</returns>
		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x060038C6 RID: 14534
		public abstract long IncomingPacketsWithErrors { get; }

		/// <summary>Gets the number of incoming packets with an unknown protocol that were received on the interface.</summary>
		/// <returns>The total number of incoming packets with an unknown protocol that were received on the interface.</returns>
		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x060038C7 RID: 14535
		public abstract long IncomingUnknownProtocolPackets { get; }

		/// <summary>Gets the number of non-unicast packets that were received on the interface.</summary>
		/// <returns>The total number of incoming non-unicast packets received on the interface.</returns>
		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x060038C8 RID: 14536
		public abstract long NonUnicastPacketsReceived { get; }

		/// <summary>Gets the number of non-unicast packets that were sent on the interface.</summary>
		/// <returns>The total number of non-unicast packets that were sent on the interface.</returns>
		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x060038C9 RID: 14537
		public abstract long NonUnicastPacketsSent { get; }

		/// <summary>Gets the number of outgoing packets that were discarded.</summary>
		/// <returns>The total number of outgoing packets that were discarded.</returns>
		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x060038CA RID: 14538
		public abstract long OutgoingPacketsDiscarded { get; }

		/// <summary>Gets the number of outgoing packets with errors.</summary>
		/// <returns>The total number of outgoing packets with errors.</returns>
		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x060038CB RID: 14539
		public abstract long OutgoingPacketsWithErrors { get; }

		/// <summary>Gets the length of the output queue.</summary>
		/// <returns>The total number of packets in the output queue.</returns>
		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x060038CC RID: 14540
		public abstract long OutputQueueLength { get; }

		/// <summary>Gets the number of unicast packets that were received on the interface.</summary>
		/// <returns>The total number of unicast packets that were received on the interface.</returns>
		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x060038CD RID: 14541
		public abstract long UnicastPacketsReceived { get; }

		/// <summary>Gets the number of unicast packets that were sent on the interface.</summary>
		/// <returns>The total number of unicast packets that were sent on the interface.</returns>
		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x060038CE RID: 14542
		public abstract long UnicastPacketsSent { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.IPInterfaceStatistics" /> class.</summary>
		// Token: 0x060038CF RID: 14543 RVA: 0x0000219B File Offset: 0x0000039B
		protected IPInterfaceStatistics()
		{
		}
	}
}
