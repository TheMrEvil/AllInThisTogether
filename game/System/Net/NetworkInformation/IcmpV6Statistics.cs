using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides Internet Control Message Protocol for Internet Protocol version 6 (ICMPv6) statistical data for the local computer.</summary>
	// Token: 0x020006ED RID: 1773
	public abstract class IcmpV6Statistics
	{
		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) messages received because of a packet having an unreachable address in its destination.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Destination Unreachable messages received.</returns>
		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06003904 RID: 14596
		public abstract long DestinationUnreachableMessagesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) messages sent because of a packet having an unreachable address in its destination.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Destination Unreachable messages sent.</returns>
		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06003905 RID: 14597
		public abstract long DestinationUnreachableMessagesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Echo Reply messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Reply messages received.</returns>
		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06003906 RID: 14598
		public abstract long EchoRepliesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Echo Reply messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Reply messages sent.</returns>
		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06003907 RID: 14599
		public abstract long EchoRepliesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Echo Request messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Request messages received.</returns>
		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06003908 RID: 14600
		public abstract long EchoRequestsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Echo Request messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of number of ICMP Echo Request messages sent.</returns>
		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06003909 RID: 14601
		public abstract long EchoRequestsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) error messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP error messages received.</returns>
		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x0600390A RID: 14602
		public abstract long ErrorsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) error messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP error messages sent.</returns>
		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x0600390B RID: 14603
		public abstract long ErrorsSent { get; }

		/// <summary>Gets the number of Internet Group management Protocol (IGMP) Group Membership Query messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Group Membership Query messages received.</returns>
		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x0600390C RID: 14604
		public abstract long MembershipQueriesReceived { get; }

		/// <summary>Gets the number of Internet Group management Protocol (IGMP) Group Membership Query messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Group Membership Query messages sent.</returns>
		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x0600390D RID: 14605
		public abstract long MembershipQueriesSent { get; }

		/// <summary>Gets the number of Internet Group Management Protocol (IGMP) Group Membership Reduction messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Group Membership Reduction messages received.</returns>
		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x0600390E RID: 14606
		public abstract long MembershipReductionsReceived { get; }

		/// <summary>Gets the number of Internet Group Management Protocol (IGMP) Group Membership Reduction messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Group Membership Reduction messages sent.</returns>
		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x0600390F RID: 14607
		public abstract long MembershipReductionsSent { get; }

		/// <summary>Gets the number of Internet Group Management Protocol (IGMP) Group Membership Report messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Group Membership Report messages received.</returns>
		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06003910 RID: 14608
		public abstract long MembershipReportsReceived { get; }

		/// <summary>Gets the number of Internet Group Management Protocol (IGMP) Group Membership Report messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Group Membership Report messages sent.</returns>
		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06003911 RID: 14609
		public abstract long MembershipReportsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMPv6 messages received.</returns>
		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06003912 RID: 14610
		public abstract long MessagesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMPv6 messages sent.</returns>
		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06003913 RID: 14611
		public abstract long MessagesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Neighbor Advertisement messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Neighbor Advertisement messages received.</returns>
		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06003914 RID: 14612
		public abstract long NeighborAdvertisementsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Neighbor Advertisement messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Neighbor Advertisement messages sent.</returns>
		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06003915 RID: 14613
		public abstract long NeighborAdvertisementsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Neighbor Solicitation messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Neighbor Solicitation messages received.</returns>
		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06003916 RID: 14614
		public abstract long NeighborSolicitsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Neighbor Solicitation messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Neighbor Solicitation messages sent.</returns>
		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06003917 RID: 14615
		public abstract long NeighborSolicitsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Packet Too Big messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Packet Too Big messages received.</returns>
		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06003918 RID: 14616
		public abstract long PacketTooBigMessagesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Packet Too Big messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Packet Too Big messages sent.</returns>
		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06003919 RID: 14617
		public abstract long PacketTooBigMessagesSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Parameter Problem messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Parameter Problem messages received.</returns>
		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x0600391A RID: 14618
		public abstract long ParameterProblemsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Parameter Problem messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Parameter Problem messages sent.</returns>
		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x0600391B RID: 14619
		public abstract long ParameterProblemsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Redirect messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Redirect messages received.</returns>
		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x0600391C RID: 14620
		public abstract long RedirectsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Redirect messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Redirect messages sent.</returns>
		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x0600391D RID: 14621
		public abstract long RedirectsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Router Advertisement messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Router Advertisement messages received.</returns>
		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x0600391E RID: 14622
		public abstract long RouterAdvertisementsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Router Advertisement messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Router Advertisement messages sent.</returns>
		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x0600391F RID: 14623
		public abstract long RouterAdvertisementsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Router Solicitation messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Router Solicitation messages received.</returns>
		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06003920 RID: 14624
		public abstract long RouterSolicitsReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Router Solicitation messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of Router Solicitation messages sent.</returns>
		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06003921 RID: 14625
		public abstract long RouterSolicitsSent { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Time Exceeded messages received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Time Exceeded messages received.</returns>
		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06003922 RID: 14626
		public abstract long TimeExceededMessagesReceived { get; }

		/// <summary>Gets the number of Internet Control Message Protocol version 6 (ICMPv6) Time Exceeded messages sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the total number of ICMP Time Exceeded messages sent.</returns>
		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06003923 RID: 14627
		public abstract long TimeExceededMessagesSent { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.IcmpV6Statistics" /> class.</summary>
		// Token: 0x06003924 RID: 14628 RVA: 0x0000219B File Offset: 0x0000039B
		protected IcmpV6Statistics()
		{
		}
	}
}
