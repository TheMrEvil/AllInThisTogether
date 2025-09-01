using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides configuration and statistical information for a network interface.</summary>
	// Token: 0x020006F8 RID: 1784
	public abstract class NetworkInterface
	{
		/// <summary>Returns objects that describe the network interfaces on the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.NetworkInterface" /> array that contains objects that describe the available network interfaces, or an empty array if no interfaces are detected.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">A Windows system function call failed.</exception>
		// Token: 0x06003958 RID: 14680 RVA: 0x000C87A3 File Offset: 0x000C69A3
		public static NetworkInterface[] GetAllNetworkInterfaces()
		{
			return SystemNetworkInterface.GetNetworkInterfaces();
		}

		/// <summary>Indicates whether any network connection is available.</summary>
		/// <returns>
		///   <see langword="true" /> if a network connection is available; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003959 RID: 14681 RVA: 0x000C87AA File Offset: 0x000C69AA
		public static bool GetIsNetworkAvailable()
		{
			return SystemNetworkInterface.InternalGetIsNetworkAvailable();
		}

		/// <summary>Gets the index of the IPv4 loopback interface.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that contains the index for the IPv4 loopback interface.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">This property is not valid on computers running only Ipv6.</exception>
		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x0600395A RID: 14682 RVA: 0x000C87B1 File Offset: 0x000C69B1
		public static int LoopbackInterfaceIndex
		{
			get
			{
				return SystemNetworkInterface.InternalLoopbackInterfaceIndex;
			}
		}

		/// <summary>Gets the index of the IPv6 loopback interface.</summary>
		/// <returns>The index for the IPv6 loopback interface.</returns>
		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x0600395B RID: 14683 RVA: 0x000C87B8 File Offset: 0x000C69B8
		public static int IPv6LoopbackInterfaceIndex
		{
			get
			{
				return SystemNetworkInterface.InternalIPv6LoopbackInterfaceIndex;
			}
		}

		/// <summary>Gets the identifier of the network adapter.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the identifier.</returns>
		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x0600395C RID: 14684 RVA: 0x0000829A File Offset: 0x0000649A
		public virtual string Id
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the name of the network adapter.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the adapter name.</returns>
		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x0600395D RID: 14685 RVA: 0x0000829A File Offset: 0x0000649A
		public virtual string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the description of the interface.</summary>
		/// <returns>A <see cref="T:System.String" /> that describes this interface.</returns>
		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x0600395E RID: 14686 RVA: 0x0000829A File Offset: 0x0000649A
		public virtual string Description
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Returns an object that describes the configuration of this network interface.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPInterfaceProperties" /> object that describes this network interface.</returns>
		// Token: 0x0600395F RID: 14687 RVA: 0x0000829A File Offset: 0x0000649A
		public virtual IPInterfaceProperties GetIPProperties()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the IPv4 statistics for this <see cref="T:System.Net.NetworkInformation.NetworkInterface" /> instance.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPv4InterfaceStatistics" /> object.</returns>
		// Token: 0x06003960 RID: 14688 RVA: 0x0000829A File Offset: 0x0000649A
		public virtual IPv4InterfaceStatistics GetIPv4Statistics()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the IP statistics for this <see cref="T:System.Net.NetworkInformation.NetworkInterface" /> instance.</summary>
		/// <returns>The IP statistics.</returns>
		// Token: 0x06003961 RID: 14689 RVA: 0x0000829A File Offset: 0x0000649A
		public virtual IPInterfaceStatistics GetIPStatistics()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the current operational state of the network connection.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.OperationalStatus" /> values.</returns>
		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06003962 RID: 14690 RVA: 0x0000829A File Offset: 0x0000649A
		public virtual OperationalStatus OperationalStatus
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the speed of the network interface.</summary>
		/// <returns>A <see cref="T:System.Int64" /> value that specifies the speed in bits per second.</returns>
		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06003963 RID: 14691 RVA: 0x0000829A File Offset: 0x0000649A
		public virtual long Speed
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the network interface is set to only receive data packets.</summary>
		/// <returns>
		///   <see langword="true" /> if the interface only receives network traffic; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP.</exception>
		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06003964 RID: 14692 RVA: 0x0000829A File Offset: 0x0000649A
		public virtual bool IsReceiveOnly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the network interface is enabled to receive multicast packets.</summary>
		/// <returns>
		///   <see langword="true" /> if the interface receives multicast packets; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP.</exception>
		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06003965 RID: 14693 RVA: 0x0000829A File Offset: 0x0000649A
		public virtual bool SupportsMulticast
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Returns the Media Access Control (MAC) or physical address for this adapter.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PhysicalAddress" /> object that contains the physical address.</returns>
		// Token: 0x06003966 RID: 14694 RVA: 0x0000829A File Offset: 0x0000649A
		public virtual PhysicalAddress GetPhysicalAddress()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the interface type.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.NetworkInterfaceType" /> value that specifies the network interface type.</returns>
		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06003967 RID: 14695 RVA: 0x0000829A File Offset: 0x0000649A
		public virtual NetworkInterfaceType NetworkInterfaceType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the interface supports the specified protocol.</summary>
		/// <param name="networkInterfaceComponent">A <see cref="T:System.Net.NetworkInformation.NetworkInterfaceComponent" /> value.</param>
		/// <returns>
		///   <see langword="true" /> if the specified protocol is supported; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003968 RID: 14696 RVA: 0x0000829A File Offset: 0x0000649A
		public virtual bool Supports(NetworkInterfaceComponent networkInterfaceComponent)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInterface" /> class.</summary>
		// Token: 0x06003969 RID: 14697 RVA: 0x0000219B File Offset: 0x0000039B
		protected NetworkInterface()
		{
		}
	}
}
