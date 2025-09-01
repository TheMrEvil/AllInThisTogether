using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides Internet Control Message Protocol for IPv4 (ICMPv4) statistical data for the local computer.</summary>
	// Token: 0x020006EC RID: 1772
	public abstract class IcmpV4Statistics
	{
		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Address Mask Reply messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Address Mask Reply messages that were received.</returns>
		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x060038E9 RID: 14569
		public abstract long AddressMaskRepliesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Address Mask Reply messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Address Mask Reply messages that were sent.</returns>
		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x060038EA RID: 14570
		public abstract long AddressMaskRepliesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Address Mask Request messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Address Mask Request messages that were received.</returns>
		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x060038EB RID: 14571
		public abstract long AddressMaskRequestsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Address Mask Request messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Address Mask Request messages that were sent.</returns>
		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x060038EC RID: 14572
		public abstract long AddressMaskRequestsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) messages that were received because of a packet having an unreachable address in its destination.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Destination Unreachable messages that were received.</returns>
		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x060038ED RID: 14573
		public abstract long DestinationUnreachableMessagesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) messages that were sent because of a packet having an unreachable address in its destination.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Destination Unreachable messages sent.</returns>
		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x060038EE RID: 14574
		public abstract long DestinationUnreachableMessagesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Echo Reply messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Reply messages that were received.</returns>
		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x060038EF RID: 14575
		public abstract long EchoRepliesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Echo Reply messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Reply messages that were sent.</returns>
		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x060038F0 RID: 14576
		public abstract long EchoRepliesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Echo Request messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Request messages that were received.</returns>
		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x060038F1 RID: 14577
		public abstract long EchoRequestsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Echo Request messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Request messages that were sent.</returns>
		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x060038F2 RID: 14578
		public abstract long EchoRequestsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) error messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP error messages that were received.</returns>
		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x060038F3 RID: 14579
		public abstract long ErrorsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) error messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP error messages that were sent.</returns>
		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x060038F4 RID: 14580
		public abstract long ErrorsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMPv4 messages that were received.</returns>
		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x060038F5 RID: 14581
		public abstract long MessagesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMPv4 messages that were sent.</returns>
		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x060038F6 RID: 14582
		public abstract long MessagesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Parameter Problem messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Parameter Problem messages that were received.</returns>
		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x060038F7 RID: 14583
		public abstract long ParameterProblemsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Parameter Problem messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Parameter Problem messages that were sent.</returns>
		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x060038F8 RID: 14584
		public abstract long ParameterProblemsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Redirect messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Redirect messages that were received.</returns>
		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x060038F9 RID: 14585
		public abstract long RedirectsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Redirect messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Redirect messages that were sent.</returns>
		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x060038FA RID: 14586
		public abstract long RedirectsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Source Quench messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Source Quench messages that were received.</returns>
		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x060038FB RID: 14587
		public abstract long SourceQuenchesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Source Quench messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Source Quench messages that were sent.</returns>
		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x060038FC RID: 14588
		public abstract long SourceQuenchesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Time Exceeded messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Time Exceeded messages that were received.</returns>
		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x060038FD RID: 14589
		public abstract long TimeExceededMessagesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Time Exceeded messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Time Exceeded messages that were sent.</returns>
		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x060038FE RID: 14590
		public abstract long TimeExceededMessagesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Timestamp Reply messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Timestamp Reply messages that were received.</returns>
		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x060038FF RID: 14591
		public abstract long TimestampRepliesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Timestamp Reply messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Timestamp Reply messages that were sent.</returns>
		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x06003900 RID: 14592
		public abstract long TimestampRepliesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Timestamp Request messages that were received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Timestamp Request messages that were received.</returns>
		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06003901 RID: 14593
		public abstract long TimestampRequestsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 4 (ICMPv4) Timestamp Request messages that were sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Timestamp Request messages that were sent.</returns>
		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06003902 RID: 14594
		public abstract long TimestampRequestsSent { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.IcmpV4Statistics" /> class.</summary>
		// Token: 0x06003903 RID: 14595 RVA: 0x0000219B File Offset: 0x0000039B
		protected IcmpV4Statistics()
		{
		}
	}
}
