using System;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about the network connectivity of the local computer.</summary>
	// Token: 0x020006E2 RID: 1762
	public abstract class IPGlobalProperties
	{
		/// <summary>Gets an object that provides information about the local computer's network connectivity and traffic statistics.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.IPGlobalProperties" /> object that contains information about the local computer.</returns>
		// Token: 0x06003888 RID: 14472 RVA: 0x000C8249 File Offset: 0x000C6449
		public static IPGlobalProperties GetIPGlobalProperties()
		{
			return IPGlobalPropertiesFactoryPal.Create();
		}

		// Token: 0x06003889 RID: 14473 RVA: 0x000C8250 File Offset: 0x000C6450
		internal static IPGlobalProperties InternalGetIPGlobalProperties()
		{
			return IPGlobalProperties.GetIPGlobalProperties();
		}

		/// <summary>Returns information about the Internet Protocol version 4 (IPv4) and IPv6 User Datagram Protocol (UDP) listeners on the local computer.</summary>
		/// <returns>An <see cref="T:System.Net.IPEndPoint" /> array that contains objects that describe the UDP listeners, or an empty array if no UDP listeners are detected.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the Win32 function <see langword="GetUdpTable" /> failed.</exception>
		// Token: 0x0600388A RID: 14474
		public abstract IPEndPoint[] GetActiveUdpListeners();

		/// <summary>Returns endpoint information about the Internet Protocol version 4 (IPv4) and IPv6 Transmission Control Protocol (TCP) listeners on the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.IPEndPoint" /> array that contains objects that describe the active TCP listeners, or an empty array, if no active TCP listeners are detected.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The Win32 function <see langword="GetTcpTable" /> failed.</exception>
		// Token: 0x0600388B RID: 14475
		public abstract IPEndPoint[] GetActiveTcpListeners();

		/// <summary>Returns information about the Internet Protocol version 4 (IPv4) and IPv6 Transmission Control Protocol (TCP) connections on the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.TcpConnectionInformation" /> array that contains objects that describe the active TCP connections, or an empty array if no active TCP connections are detected.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The Win32 function <see langword="GetTcpTable" /> failed.</exception>
		// Token: 0x0600388C RID: 14476
		public abstract TcpConnectionInformation[] GetActiveTcpConnections();

		/// <summary>Gets the Dynamic Host Configuration Protocol (DHCP) scope name.</summary>
		/// <returns>A <see cref="T:System.String" /> instance that contains the computer's DHCP scope name.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">A Win32 function call failed.</exception>
		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x0600388D RID: 14477
		public abstract string DhcpScopeName { get; }

		/// <summary>Gets the domain in which the local computer is registered.</summary>
		/// <returns>A <see cref="T:System.String" /> instance that contains the computer's domain name. If the computer does not belong to a domain, returns <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">A Win32 function call failed.</exception>
		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x0600388E RID: 14478
		public abstract string DomainName { get; }

		/// <summary>Gets the host name for the local computer.</summary>
		/// <returns>A <see cref="T:System.String" /> instance that contains the computer's NetBIOS name.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">A Win32 function call failed.</exception>
		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x0600388F RID: 14479
		public abstract string HostName { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that specifies whether the local computer is acting as a Windows Internet Name Service (WINS) proxy.</summary>
		/// <returns>
		///   <see langword="true" /> if the local computer is a WINS proxy; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">A Win32 function call failed.</exception>
		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x06003890 RID: 14480
		public abstract bool IsWinsProxy { get; }

		/// <summary>Gets the Network Basic Input/Output System (NetBIOS) node type of the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.NetBiosNodeType" /> value.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">A Win32 function call failed.</exception>
		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x06003891 RID: 14481
		public abstract NetBiosNodeType NodeType { get; }

		/// <summary>Provides Transmission Control Protocol/Internet Protocol version 4 (TCP/IPv4) statistical data for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.TcpStatistics" /> object that provides TCP/IPv4 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the Win32 function <see langword="GetTcpStatistics" /> failed.</exception>
		// Token: 0x06003892 RID: 14482
		public abstract TcpStatistics GetTcpIPv4Statistics();

		/// <summary>Provides Transmission Control Protocol/Internet Protocol version 6 (TCP/IPv6) statistical data for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.TcpStatistics" /> object that provides TCP/IPv6 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the Win32 function <see langword="GetTcpStatistics" /> failed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The local computer is not running an operating system that supports IPv6.</exception>
		// Token: 0x06003893 RID: 14483
		public abstract TcpStatistics GetTcpIPv6Statistics();

		/// <summary>Provides User Datagram Protocol/Internet Protocol version 4 (UDP/IPv4) statistical data for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.UdpStatistics" /> object that provides UDP/IPv4 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the Win32 function GetUdpStatistics failed.</exception>
		// Token: 0x06003894 RID: 14484
		public abstract UdpStatistics GetUdpIPv4Statistics();

		/// <summary>Provides User Datagram Protocol/Internet Protocol version 6 (UDP/IPv6) statistical data for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.UdpStatistics" /> object that provides UDP/IPv6 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the Win32 function <see langword="GetUdpStatistics" /> failed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The local computer is not running an operating system that supports IPv6.</exception>
		// Token: 0x06003895 RID: 14485
		public abstract UdpStatistics GetUdpIPv6Statistics();

		/// <summary>Provides Internet Control Message Protocol (ICMP) version 4 statistical data for the local computer.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IcmpV4Statistics" /> object that provides ICMP version 4 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The Win32 function <see langword="GetIcmpStatistics" /> failed.</exception>
		// Token: 0x06003896 RID: 14486
		public abstract IcmpV4Statistics GetIcmpV4Statistics();

		/// <summary>Provides Internet Control Message Protocol (ICMP) version 6 statistical data for the local computer.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IcmpV6Statistics" /> object that provides ICMP version 6 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The Win32 function <see langword="GetIcmpStatisticsEx" /> failed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The local computer's operating system is not Windows XP or later.</exception>
		// Token: 0x06003897 RID: 14487
		public abstract IcmpV6Statistics GetIcmpV6Statistics();

		/// <summary>Provides Internet Protocol version 4 (IPv4) statistical data for the local computer.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPGlobalStatistics" /> object that provides IPv4 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the Win32 function <see langword="GetIpStatistics" /> failed.</exception>
		// Token: 0x06003898 RID: 14488
		public abstract IPGlobalStatistics GetIPv4GlobalStatistics();

		/// <summary>Provides Internet Protocol version 6 (IPv6) statistical data for the local computer.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPGlobalStatistics" /> object that provides IPv6 traffic statistics for the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the Win32 function <see langword="GetIpStatistics" /> failed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The local computer is not running an operating system that supports IPv6.</exception>
		// Token: 0x06003899 RID: 14489
		public abstract IPGlobalStatistics GetIPv6GlobalStatistics();

		/// <summary>Retrieves the stable unicast IP address table on the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformationCollection" /> that contains a list of stable unicast IP addresses on the local computer.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the native <see langword="GetAdaptersAddresses" /> function failed.</exception>
		/// <exception cref="T:System.NotImplementedException">This method is not implemented on the platform. This method uses the native <see langword="NotifyStableUnicastIpAddressTable" /> function that is supported on Windows Vista and later.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have necessary <see cref="F:System.Net.NetworkInformation.NetworkInformationAccess.Read" /> permission.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The call to the native <see langword="NotifyStableUnicastIpAddressTable" /> function failed.</exception>
		// Token: 0x0600389A RID: 14490 RVA: 0x000A5C4A File Offset: 0x000A3E4A
		public virtual UnicastIPAddressInformationCollection GetUnicastAddresses()
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		/// <summary>Begins an asynchronous request to retrieve the stable unicast IP address table on the local computer.</summary>
		/// <param name="callback">The <see cref="T:System.AsyncCallback" /> delegate.</param>
		/// <param name="state">An object that contains state information for this request.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous request.</returns>
		/// <exception cref="T:System.NotImplementedException">This method is not implemented on the platform. This method uses the native <see langword="NotifyStableUnicastIpAddressTable" /> function that is supported on Windows Vista and later.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The call to the native <see langword="NotifyStableUnicastIpAddressTable" /> function failed.</exception>
		// Token: 0x0600389B RID: 14491 RVA: 0x000A5C4A File Offset: 0x000A3E4A
		public virtual IAsyncResult BeginGetUnicastAddresses(AsyncCallback callback, object state)
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		/// <summary>Ends a pending asynchronous request to retrieve the stable unicast IP address table on the local computer.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> that references the asynchronous request.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that stores state information and any user defined data for this asynchronous operation.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the native <see langword="GetAdaptersAddresses" /> function failed.</exception>
		/// <exception cref="T:System.NotImplementedException">This method is not implemented on the platform. This method uses the native <see langword="NotifyStableUnicastIpAddressTable" /> function that is supported on Windows Vista and later.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have necessary <see cref="F:System.Net.NetworkInformation.NetworkInformationAccess.Read" /> permission.</exception>
		// Token: 0x0600389C RID: 14492 RVA: 0x000A5C4A File Offset: 0x000A3E4A
		public virtual UnicastIPAddressInformationCollection EndGetUnicastAddresses(IAsyncResult asyncResult)
		{
			throw ExceptionHelper.MethodNotImplementedException;
		}

		/// <summary>Retrieves the stable unicast IP address table on the local computer as an asynchronous operation.</summary>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">The call to the native <see langword="GetAdaptersAddresses" /> function failed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have necessary <see cref="F:System.Net.NetworkInformation.NetworkInformationAccess.Read" /> permission.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The call to the native <see langword="NotifyStableUnicastIpAddressTable" /> function failed.</exception>
		// Token: 0x0600389D RID: 14493 RVA: 0x000C8257 File Offset: 0x000C6457
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task<UnicastIPAddressInformationCollection> GetUnicastAddressesAsync()
		{
			return Task<UnicastIPAddressInformationCollection>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginGetUnicastAddresses), new Func<IAsyncResult, UnicastIPAddressInformationCollection>(this.EndGetUnicastAddresses), null);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.IPGlobalProperties" /> class.</summary>
		// Token: 0x0600389E RID: 14494 RVA: 0x0000219B File Offset: 0x0000039B
		protected IPGlobalProperties()
		{
		}
	}
}
