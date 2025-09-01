using System;
using System.Globalization;
using System.Net.Sockets;

namespace System.Net
{
	/// <summary>Represents a network endpoint as an IP address and a port number.</summary>
	// Token: 0x0200057F RID: 1407
	[Serializable]
	public class IPEndPoint : EndPoint
	{
		/// <summary>Gets the Internet Protocol (IP) address family.</summary>
		/// <returns>Returns <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" />.</returns>
		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06002D96 RID: 11670 RVA: 0x0009C126 File Offset: 0x0009A326
		public override AddressFamily AddressFamily
		{
			get
			{
				return this._address.AddressFamily;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPEndPoint" /> class with the specified address and port number.</summary>
		/// <param name="address">The IP address of the Internet host.</param>
		/// <param name="port">The port number associated with the <paramref name="address" />, or 0 to specify any available port. <paramref name="port" /> is in host order.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than <see cref="F:System.Net.IPEndPoint.MinPort" />.  
		/// -or-  
		/// <paramref name="port" /> is greater than <see cref="F:System.Net.IPEndPoint.MaxPort" />.  
		/// -or-  
		/// <paramref name="address" /> is less than 0 or greater than 0x00000000FFFFFFFF.</exception>
		// Token: 0x06002D97 RID: 11671 RVA: 0x0009C133 File Offset: 0x0009A333
		public IPEndPoint(long address, int port)
		{
			if (!TcpValidationHelpers.ValidatePortNumber(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			this._port = port;
			this._address = new IPAddress(address);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPEndPoint" /> class with the specified address and port number.</summary>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" />.</param>
		/// <param name="port">The port number associated with the <paramref name="address" />, or 0 to specify any available port. <paramref name="port" /> is in host order.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than <see cref="F:System.Net.IPEndPoint.MinPort" />.  
		/// -or-  
		/// <paramref name="port" /> is greater than <see cref="F:System.Net.IPEndPoint.MaxPort" />.  
		/// -or-  
		/// <paramref name="address" /> is less than 0 or greater than 0x00000000FFFFFFFF.</exception>
		// Token: 0x06002D98 RID: 11672 RVA: 0x0009C161 File Offset: 0x0009A361
		public IPEndPoint(IPAddress address, int port)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (!TcpValidationHelpers.ValidatePortNumber(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			this._port = port;
			this._address = address;
		}

		/// <summary>Gets or sets the IP address of the endpoint.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> instance containing the IP address of the endpoint.</returns>
		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06002D99 RID: 11673 RVA: 0x0009C198 File Offset: 0x0009A398
		// (set) Token: 0x06002D9A RID: 11674 RVA: 0x0009C1A0 File Offset: 0x0009A3A0
		public IPAddress Address
		{
			get
			{
				return this._address;
			}
			set
			{
				this._address = value;
			}
		}

		/// <summary>Gets or sets the port number of the endpoint.</summary>
		/// <returns>An integer value in the range <see cref="F:System.Net.IPEndPoint.MinPort" /> to <see cref="F:System.Net.IPEndPoint.MaxPort" /> indicating the port number of the endpoint.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value that was specified for a set operation is less than <see cref="F:System.Net.IPEndPoint.MinPort" /> or greater than <see cref="F:System.Net.IPEndPoint.MaxPort" />.</exception>
		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06002D9B RID: 11675 RVA: 0x0009C1A9 File Offset: 0x0009A3A9
		// (set) Token: 0x06002D9C RID: 11676 RVA: 0x0009C1B1 File Offset: 0x0009A3B1
		public int Port
		{
			get
			{
				return this._port;
			}
			set
			{
				if (!TcpValidationHelpers.ValidatePortNumber(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._port = value;
			}
		}

		/// <summary>Returns the IP address and port number of the specified endpoint.</summary>
		/// <returns>A string containing the IP address and the port number of the specified endpoint (for example, 192.168.1.2:80).</returns>
		// Token: 0x06002D9D RID: 11677 RVA: 0x0009C1D0 File Offset: 0x0009A3D0
		public override string ToString()
		{
			return string.Format((this._address.AddressFamily == AddressFamily.InterNetworkV6) ? "[{0}]:{1}" : "{0}:{1}", this._address.ToString(), this.Port.ToString(NumberFormatInfo.InvariantInfo));
		}

		/// <summary>Serializes endpoint information into a <see cref="T:System.Net.SocketAddress" /> instance.</summary>
		/// <returns>A <see cref="T:System.Net.SocketAddress" /> instance containing the socket address for the endpoint.</returns>
		// Token: 0x06002D9E RID: 11678 RVA: 0x0009C21B File Offset: 0x0009A41B
		public override SocketAddress Serialize()
		{
			return new SocketAddress(this.Address, this.Port);
		}

		/// <summary>Creates an endpoint from a socket address.</summary>
		/// <param name="socketAddress">The <see cref="T:System.Net.SocketAddress" /> to use for the endpoint.</param>
		/// <returns>An <see cref="T:System.Net.EndPoint" /> instance using the specified socket address.</returns>
		/// <exception cref="T:System.ArgumentException">The AddressFamily of <paramref name="socketAddress" /> is not equal to the AddressFamily of the current instance.  
		///  -or-  
		///  <paramref name="socketAddress" />.Size &lt; 8.</exception>
		// Token: 0x06002D9F RID: 11679 RVA: 0x0009C230 File Offset: 0x0009A430
		public override EndPoint Create(SocketAddress socketAddress)
		{
			if (socketAddress.Family != this.AddressFamily)
			{
				throw new ArgumentException(SR.Format("The AddressFamily {0} is not valid for the {1} end point, use {2} instead.", socketAddress.Family.ToString(), base.GetType().FullName, this.AddressFamily.ToString()), "socketAddress");
			}
			if (socketAddress.Size < 8)
			{
				throw new ArgumentException(SR.Format("The supplied {0} is an invalid size for the {1} end point.", socketAddress.GetType().FullName, base.GetType().FullName), "socketAddress");
			}
			return socketAddress.GetIPEndPoint();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
		/// <param name="comparand">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002DA0 RID: 11680 RVA: 0x0009C2D0 File Offset: 0x0009A4D0
		public override bool Equals(object comparand)
		{
			IPEndPoint ipendPoint = comparand as IPEndPoint;
			return ipendPoint != null && ipendPoint._address.Equals(this._address) && ipendPoint._port == this._port;
		}

		/// <summary>Returns a hash value for a <see cref="T:System.Net.IPEndPoint" /> instance.</summary>
		/// <returns>An integer hash value.</returns>
		// Token: 0x06002DA1 RID: 11681 RVA: 0x0009C30A File Offset: 0x0009A50A
		public override int GetHashCode()
		{
			return this._address.GetHashCode() ^ this._port;
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x0009C31E File Offset: 0x0009A51E
		// Note: this type is marked as 'beforefieldinit'.
		static IPEndPoint()
		{
		}

		/// <summary>Specifies the minimum value that can be assigned to the <see cref="P:System.Net.IPEndPoint.Port" /> property. This field is read-only.</summary>
		// Token: 0x040018F2 RID: 6386
		public const int MinPort = 0;

		/// <summary>Specifies the maximum value that can be assigned to the <see cref="P:System.Net.IPEndPoint.Port" /> property. The MaxPort value is set to 0x0000FFFF. This field is read-only.</summary>
		// Token: 0x040018F3 RID: 6387
		public const int MaxPort = 65535;

		// Token: 0x040018F4 RID: 6388
		private IPAddress _address;

		// Token: 0x040018F5 RID: 6389
		private int _port;

		// Token: 0x040018F6 RID: 6390
		internal const int AnyPort = 0;

		// Token: 0x040018F7 RID: 6391
		internal static IPEndPoint Any = new IPEndPoint(IPAddress.Any, 0);

		// Token: 0x040018F8 RID: 6392
		internal static IPEndPoint IPv6Any = new IPEndPoint(IPAddress.IPv6Any, 0);
	}
}
