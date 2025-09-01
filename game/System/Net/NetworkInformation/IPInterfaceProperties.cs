using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about network interfaces that support Internet Protocol version 4 (IPv4) or Internet Protocol version 6 (IPv6).</summary>
	// Token: 0x020006E4 RID: 1764
	public abstract class IPInterfaceProperties
	{
		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether NetBt is configured to use DNS name resolution on this interface.</summary>
		/// <returns>
		///   <see langword="true" /> if NetBt is configured to use DNS name resolution on this interface; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x060038B6 RID: 14518
		public abstract bool IsDnsEnabled { get; }

		/// <summary>Gets the Domain Name System (DNS) suffix associated with this interface.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the DNS suffix for this interface, or <see cref="F:System.String.Empty" /> if there is no DNS suffix for the interface.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows 2000.</exception>
		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x060038B7 RID: 14519
		public abstract string DnsSuffix { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether this interface is configured to automatically register its IP address information with the Domain Name System (DNS).</summary>
		/// <returns>
		///   <see langword="true" /> if this interface is configured to automatically register a mapping between its dynamic IP address and static domain names; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x060038B8 RID: 14520
		public abstract bool IsDynamicDnsEnabled { get; }

		/// <summary>Gets the unicast addresses assigned to this interface.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformationCollection" /> that contains the unicast addresses for this interface.</returns>
		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x060038B9 RID: 14521
		public abstract UnicastIPAddressInformationCollection UnicastAddresses { get; }

		/// <summary>Gets the multicast addresses assigned to this interface.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformationCollection" /> that contains the multicast addresses for this interface.</returns>
		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x060038BA RID: 14522
		public abstract MulticastIPAddressInformationCollection MulticastAddresses { get; }

		/// <summary>Gets the anycast IP addresses assigned to this interface.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPAddressInformationCollection" /> that contains the anycast addresses for this interface.</returns>
		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x060038BB RID: 14523
		public abstract IPAddressInformationCollection AnycastAddresses { get; }

		/// <summary>Gets the addresses of Domain Name System (DNS) servers for this interface.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.IPAddressCollection" /> that contains the DNS server addresses.</returns>
		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x060038BC RID: 14524
		public abstract IPAddressCollection DnsAddresses { get; }

		/// <summary>Gets the IPv4 network gateway addresses for this interface.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.GatewayIPAddressInformationCollection" /> that contains the address information for network gateways, or an empty array if no gateways are found.</returns>
		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x060038BD RID: 14525
		public abstract GatewayIPAddressInformationCollection GatewayAddresses { get; }

		/// <summary>Gets the addresses of Dynamic Host Configuration Protocol (DHCP) servers for this interface.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPAddressCollection" /> that contains the address information for DHCP servers, or an empty array if no servers are found.</returns>
		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x060038BE RID: 14526
		public abstract IPAddressCollection DhcpServerAddresses { get; }

		/// <summary>Gets the addresses of Windows Internet Name Service (WINS) servers.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPAddressCollection" /> that contains the address information for WINS servers, or an empty array if no servers are found.</returns>
		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x060038BF RID: 14527
		public abstract IPAddressCollection WinsServersAddresses { get; }

		/// <summary>Provides Internet Protocol version 4 (IPv4) configuration data for this network interface.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPv4InterfaceProperties" /> object that contains IPv4 configuration data, or <see langword="null" /> if no data is available for the interface.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The interface does not support the IPv4 protocol.</exception>
		// Token: 0x060038C0 RID: 14528
		public abstract IPv4InterfaceProperties GetIPv4Properties();

		/// <summary>Provides Internet Protocol version 6 (IPv6) configuration data for this network interface.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPv6InterfaceProperties" /> object that contains IPv6 configuration data.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The interface does not support the IPv6 protocol.</exception>
		// Token: 0x060038C1 RID: 14529
		public abstract IPv6InterfaceProperties GetIPv6Properties();

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.IPInterfaceProperties" /> class.</summary>
		// Token: 0x060038C2 RID: 14530 RVA: 0x0000219B File Offset: 0x0000039B
		protected IPInterfaceProperties()
		{
		}
	}
}
