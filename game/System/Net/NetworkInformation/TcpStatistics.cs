using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides Transmission Control Protocol (TCP) statistical data.</summary>
	// Token: 0x02000707 RID: 1799
	public abstract class TcpStatistics
	{
		/// <summary>Gets the number of accepted Transmission Control Protocol (TCP) connection requests.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP connection requests accepted.</returns>
		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x0600399C RID: 14748
		public abstract long ConnectionsAccepted { get; }

		/// <summary>Gets the number of Transmission Control Protocol (TCP) connection requests made by clients.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP connections initiated by clients.</returns>
		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x0600399D RID: 14749
		public abstract long ConnectionsInitiated { get; }

		/// <summary>Specifies the total number of Transmission Control Protocol (TCP) connections established.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of connections established.</returns>
		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x0600399E RID: 14750
		public abstract long CumulativeConnections { get; }

		/// <summary>Gets the number of current Transmission Control Protocol (TCP) connections.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of current TCP connections.</returns>
		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x0600399F RID: 14751
		public abstract long CurrentConnections { get; }

		/// <summary>Gets the number of Transmission Control Protocol (TCP) errors received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP errors received.</returns>
		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x060039A0 RID: 14752
		public abstract long ErrorsReceived { get; }

		/// <summary>Gets the number of failed Transmission Control Protocol (TCP) connection attempts.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of failed TCP connection attempts.</returns>
		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x060039A1 RID: 14753
		public abstract long FailedConnectionAttempts { get; }

		/// <summary>Gets the maximum number of supported Transmission Control Protocol (TCP) connections.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP connections that can be supported.</returns>
		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x060039A2 RID: 14754
		public abstract long MaximumConnections { get; }

		/// <summary>Gets the maximum retransmission time-out value for Transmission Control Protocol (TCP) segments.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the maximum number of milliseconds permitted by a TCP implementation for the retransmission time-out value.</returns>
		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x060039A3 RID: 14755
		public abstract long MaximumTransmissionTimeout { get; }

		/// <summary>Gets the minimum retransmission time-out value for Transmission Control Protocol (TCP) segments.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the minimum number of milliseconds permitted by a TCP implementation for the retransmission time-out value.</returns>
		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x060039A4 RID: 14756
		public abstract long MinimumTransmissionTimeout { get; }

		/// <summary>Gets the number of RST packets received by Transmission Control Protocol (TCP) connections.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of reset TCP connections.</returns>
		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x060039A5 RID: 14757
		public abstract long ResetConnections { get; }

		/// <summary>Gets the number of Transmission Control Protocol (TCP) segments received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP segments received.</returns>
		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x060039A6 RID: 14758
		public abstract long SegmentsReceived { get; }

		/// <summary>Gets the number of Transmission Control Protocol (TCP) segments re-sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP segments retransmitted.</returns>
		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x060039A7 RID: 14759
		public abstract long SegmentsResent { get; }

		/// <summary>Gets the number of Transmission Control Protocol (TCP) segments sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP segments sent.</returns>
		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x060039A8 RID: 14760
		public abstract long SegmentsSent { get; }

		/// <summary>Gets the number of Transmission Control Protocol (TCP) segments sent with the reset flag set.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of TCP segments sent with the reset flag set.</returns>
		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x060039A9 RID: 14761
		public abstract long ResetsSent { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.TcpStatistics" /> class.</summary>
		// Token: 0x060039AA RID: 14762 RVA: 0x0000219B File Offset: 0x0000039B
		protected TcpStatistics()
		{
		}
	}
}
