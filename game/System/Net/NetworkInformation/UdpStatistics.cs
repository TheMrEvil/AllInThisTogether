using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides User Datagram Protocol (UDP) statistical data.</summary>
	// Token: 0x02000708 RID: 1800
	public abstract class UdpStatistics
	{
		/// <summary>Gets the number of User Datagram Protocol (UDP) datagrams that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of datagrams that were delivered to UDP users.</returns>
		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x060039AB RID: 14763
		public abstract long DatagramsReceived { get; }

		/// <summary>Gets the number of User Datagram Protocol (UDP) datagrams that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of datagrams that were sent.</returns>
		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x060039AC RID: 14764
		public abstract long DatagramsSent { get; }

		/// <summary>Gets the number of User Datagram Protocol (UDP) datagrams that were received and discarded because of port errors.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of received UDP datagrams that were discarded because there was no listening application at the destination port.</returns>
		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x060039AD RID: 14765
		public abstract long IncomingDatagramsDiscarded { get; }

		/// <summary>Gets the number of User Datagram Protocol (UDP) datagrams that were received and discarded because of errors other than bad port information.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of received UDP datagrams that could not be delivered for reasons other than the lack of an application at the destination port.</returns>
		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x060039AE RID: 14766
		public abstract long IncomingDatagramsWithErrors { get; }

		/// <summary>Gets the number of local endpoints that are listening for User Datagram Protocol (UDP) datagrams.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of sockets that are listening for UDP datagrams.</returns>
		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x060039AF RID: 14767
		public abstract int UdpListeners { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.UdpStatistics" /> class.</summary>
		// Token: 0x060039B0 RID: 14768 RVA: 0x0000219B File Offset: 0x0000039B
		protected UdpStatistics()
		{
		}
	}
}
