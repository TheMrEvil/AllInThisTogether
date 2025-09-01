using System;
using System.Net.Sockets;

namespace System.Net
{
	/// <summary>Represents a network endpoint as a host name or a string representation of an IP address and a port number.</summary>
	// Token: 0x020005CA RID: 1482
	public class DnsEndPoint : EndPoint
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.DnsEndPoint" /> class with the host name or string representation of an IP address and a port number.</summary>
		/// <param name="host">The host name or a string representation of the IP address.</param>
		/// <param name="port">The port number associated with the address, or 0 to specify any available port. <paramref name="port" /> is in host order.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="host" /> parameter contains an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="host" /> parameter is a <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than <see cref="F:System.Net.IPEndPoint.MinPort" />.  
		/// -or-  
		/// <paramref name="port" /> is greater than <see cref="F:System.Net.IPEndPoint.MaxPort" />.</exception>
		// Token: 0x06002FFE RID: 12286 RVA: 0x000A5ADB File Offset: 0x000A3CDB
		public DnsEndPoint(string host, int port) : this(host, port, AddressFamily.Unspecified)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.DnsEndPoint" /> class with the host name or string representation of an IP address, a port number, and an address family.</summary>
		/// <param name="host">The host name or a string representation of the IP address.</param>
		/// <param name="port">The port number associated with the address, or 0 to specify any available port. <paramref name="port" /> is in host order.</param>
		/// <param name="addressFamily">One of the <see cref="T:System.Net.Sockets.AddressFamily" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="host" /> parameter contains an empty string.  
		///  -or-  
		///  <paramref name="addressFamily" /> is <see cref="F:System.Net.Sockets.AddressFamily.Unknown" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="host" /> parameter is a <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than <see cref="F:System.Net.IPEndPoint.MinPort" />.  
		/// -or-  
		/// <paramref name="port" /> is greater than <see cref="F:System.Net.IPEndPoint.MaxPort" />.</exception>
		// Token: 0x06002FFF RID: 12287 RVA: 0x000A5AE8 File Offset: 0x000A3CE8
		public DnsEndPoint(string host, int port, AddressFamily addressFamily)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			if (string.IsNullOrEmpty(host))
			{
				throw new ArgumentException(SR.GetString("The parameter '{0}' cannot be an empty string.", new object[]
				{
					"host"
				}));
			}
			if (port < 0 || port > 65535)
			{
				throw new ArgumentOutOfRangeException("port");
			}
			if (addressFamily != AddressFamily.InterNetwork && addressFamily != AddressFamily.InterNetworkV6 && addressFamily != AddressFamily.Unspecified)
			{
				throw new ArgumentException(SR.GetString("The specified value is not valid."), "addressFamily");
			}
			this.m_Host = host;
			this.m_Port = port;
			this.m_Family = addressFamily;
		}

		/// <summary>Compares two <see cref="T:System.Net.DnsEndPoint" /> objects.</summary>
		/// <param name="comparand">A <see cref="T:System.Net.DnsEndPoint" /> instance to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the two <see cref="T:System.Net.DnsEndPoint" /> instances are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003000 RID: 12288 RVA: 0x000A5B7C File Offset: 0x000A3D7C
		public override bool Equals(object comparand)
		{
			DnsEndPoint dnsEndPoint = comparand as DnsEndPoint;
			return dnsEndPoint != null && (this.m_Family == dnsEndPoint.m_Family && this.m_Port == dnsEndPoint.m_Port) && this.m_Host == dnsEndPoint.m_Host;
		}

		/// <summary>Returns a hash value for a <see cref="T:System.Net.DnsEndPoint" />.</summary>
		/// <returns>An integer hash value for the <see cref="T:System.Net.DnsEndPoint" />.</returns>
		// Token: 0x06003001 RID: 12289 RVA: 0x000A5BC4 File Offset: 0x000A3DC4
		public override int GetHashCode()
		{
			return StringComparer.InvariantCultureIgnoreCase.GetHashCode(this.ToString());
		}

		/// <summary>Returns the host name or string representation of the IP address and port number of the <see cref="T:System.Net.DnsEndPoint" />.</summary>
		/// <returns>A string containing the address family, host name or IP address string, and the port number of the specified <see cref="T:System.Net.DnsEndPoint" />.</returns>
		// Token: 0x06003002 RID: 12290 RVA: 0x000A5BD8 File Offset: 0x000A3DD8
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.m_Family.ToString(),
				"/",
				this.m_Host,
				":",
				this.m_Port.ToString()
			});
		}

		/// <summary>Gets the host name or string representation of the Internet Protocol (IP) address of the host.</summary>
		/// <returns>A host name or string representation of an IP address.</returns>
		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06003003 RID: 12291 RVA: 0x000A5C2B File Offset: 0x000A3E2B
		public string Host
		{
			get
			{
				return this.m_Host;
			}
		}

		/// <summary>Gets the Internet Protocol (IP) address family.</summary>
		/// <returns>One of the <see cref="T:System.Net.Sockets.AddressFamily" /> values.</returns>
		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06003004 RID: 12292 RVA: 0x000A5C33 File Offset: 0x000A3E33
		public override AddressFamily AddressFamily
		{
			get
			{
				return this.m_Family;
			}
		}

		/// <summary>Gets the port number of the <see cref="T:System.Net.DnsEndPoint" />.</summary>
		/// <returns>An integer value in the range 0 to 0xffff indicating the port number of the <see cref="T:System.Net.DnsEndPoint" />.</returns>
		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06003005 RID: 12293 RVA: 0x000A5C3B File Offset: 0x000A3E3B
		public int Port
		{
			get
			{
				return this.m_Port;
			}
		}

		// Token: 0x04001A74 RID: 6772
		private string m_Host;

		// Token: 0x04001A75 RID: 6773
		private int m_Port;

		// Token: 0x04001A76 RID: 6774
		private AddressFamily m_Family;
	}
}
