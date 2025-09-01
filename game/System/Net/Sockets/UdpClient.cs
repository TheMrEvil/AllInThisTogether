using System;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Net.Sockets
{
	/// <summary>Provides User Datagram Protocol (UDP) network services.</summary>
	// Token: 0x020007BD RID: 1981
	public class UdpClient : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.UdpClient" /> class.</summary>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		// Token: 0x06003F16 RID: 16150 RVA: 0x000D781B File Offset: 0x000D5A1B
		public UdpClient() : this(AddressFamily.InterNetwork)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.UdpClient" /> class.</summary>
		/// <param name="family">One of the <see cref="T:System.Net.Sockets.AddressFamily" /> values that specifies the addressing scheme of the socket.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="family" /> is not <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		// Token: 0x06003F17 RID: 16151 RVA: 0x000D7824 File Offset: 0x000D5A24
		public UdpClient(AddressFamily family)
		{
			this.m_Buffer = new byte[65536];
			this.m_Family = AddressFamily.InterNetwork;
			base..ctor();
			if (family != AddressFamily.InterNetwork && family != AddressFamily.InterNetworkV6)
			{
				throw new ArgumentException(SR.GetString("'{0}' Client can only accept InterNetwork or InterNetworkV6 addresses.", new object[]
				{
					"UDP"
				}), "family");
			}
			this.m_Family = family;
			this.createClientSocket();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.UdpClient" /> class and binds it to the local port number provided.</summary>
		/// <param name="port">The local port number from which you intend to communicate.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="port" /> parameter is greater than <see cref="F:System.Net.IPEndPoint.MaxPort" /> or less than <see cref="F:System.Net.IPEndPoint.MinPort" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		// Token: 0x06003F18 RID: 16152 RVA: 0x000D7887 File Offset: 0x000D5A87
		public UdpClient(int port) : this(port, AddressFamily.InterNetwork)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.UdpClient" /> class and binds it to the local port number provided.</summary>
		/// <param name="port">The port on which to listen for incoming connection attempts.</param>
		/// <param name="family">One of the <see cref="T:System.Net.Sockets.AddressFamily" /> values that specifies the addressing scheme of the socket.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="family" /> is not <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is greater than <see cref="F:System.Net.IPEndPoint.MaxPort" /> or less than <see cref="F:System.Net.IPEndPoint.MinPort" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		// Token: 0x06003F19 RID: 16153 RVA: 0x000D7894 File Offset: 0x000D5A94
		public UdpClient(int port, AddressFamily family)
		{
			this.m_Buffer = new byte[65536];
			this.m_Family = AddressFamily.InterNetwork;
			base..ctor();
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			if (family != AddressFamily.InterNetwork && family != AddressFamily.InterNetworkV6)
			{
				throw new ArgumentException(SR.GetString("'{0}' Client can only accept InterNetwork or InterNetworkV6 addresses."), "family");
			}
			this.m_Family = family;
			IPEndPoint localEP;
			if (this.m_Family == AddressFamily.InterNetwork)
			{
				localEP = new IPEndPoint(IPAddress.Any, port);
			}
			else
			{
				localEP = new IPEndPoint(IPAddress.IPv6Any, port);
			}
			this.createClientSocket();
			this.Client.Bind(localEP);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.UdpClient" /> class and binds it to the specified local endpoint.</summary>
		/// <param name="localEP">An <see cref="T:System.Net.IPEndPoint" /> that respresents the local endpoint to which you bind the UDP connection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="localEP" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		// Token: 0x06003F1A RID: 16154 RVA: 0x000D792C File Offset: 0x000D5B2C
		public UdpClient(IPEndPoint localEP)
		{
			this.m_Buffer = new byte[65536];
			this.m_Family = AddressFamily.InterNetwork;
			base..ctor();
			if (localEP == null)
			{
				throw new ArgumentNullException("localEP");
			}
			this.m_Family = localEP.AddressFamily;
			this.createClientSocket();
			this.Client.Bind(localEP);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.UdpClient" /> class and establishes a default remote host.</summary>
		/// <param name="hostname">The name of the remote DNS host to which you intend to connect.</param>
		/// <param name="port">The remote port number to which you intend to connect.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostname" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is not between <see cref="F:System.Net.IPEndPoint.MinPort" /> and <see cref="F:System.Net.IPEndPoint.MaxPort" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		// Token: 0x06003F1B RID: 16155 RVA: 0x000D7984 File Offset: 0x000D5B84
		public UdpClient(string hostname, int port)
		{
			this.m_Buffer = new byte[65536];
			this.m_Family = AddressFamily.InterNetwork;
			base..ctor();
			if (hostname == null)
			{
				throw new ArgumentNullException("hostname");
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			this.Connect(hostname, port);
		}

		/// <summary>Gets or sets the underlying network <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>The underlying Network <see cref="T:System.Net.Sockets.Socket" />.</returns>
		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x06003F1C RID: 16156 RVA: 0x000D79D7 File Offset: 0x000D5BD7
		// (set) Token: 0x06003F1D RID: 16157 RVA: 0x000D79DF File Offset: 0x000D5BDF
		public Socket Client
		{
			get
			{
				return this.m_ClientSocket;
			}
			set
			{
				this.m_ClientSocket = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a default remote host has been established.</summary>
		/// <returns>
		///   <see langword="true" /> if a connection is active; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x06003F1E RID: 16158 RVA: 0x000D79E8 File Offset: 0x000D5BE8
		// (set) Token: 0x06003F1F RID: 16159 RVA: 0x000D79F0 File Offset: 0x000D5BF0
		protected bool Active
		{
			get
			{
				return this.m_Active;
			}
			set
			{
				this.m_Active = value;
			}
		}

		/// <summary>Gets the amount of data received from the network that is available to read.</summary>
		/// <returns>The number of bytes of data received from the network.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred while attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x06003F20 RID: 16160 RVA: 0x000D79F9 File Offset: 0x000D5BF9
		public int Available
		{
			get
			{
				return this.m_ClientSocket.Available;
			}
		}

		/// <summary>Gets or sets a value that specifies the Time to Live (TTL) value of Internet Protocol (IP) packets sent by the <see cref="T:System.Net.Sockets.UdpClient" />.</summary>
		/// <returns>The TTL value.</returns>
		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06003F21 RID: 16161 RVA: 0x000D7A06 File Offset: 0x000D5C06
		// (set) Token: 0x06003F22 RID: 16162 RVA: 0x000D7A13 File Offset: 0x000D5C13
		public short Ttl
		{
			get
			{
				return this.m_ClientSocket.Ttl;
			}
			set
			{
				this.m_ClientSocket.Ttl = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Net.Sockets.UdpClient" /> allows Internet Protocol (IP) datagrams to be fragmented.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.Sockets.UdpClient" /> allows datagram fragmentation; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This property can be set only for sockets that use the <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> flag or the <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> flag.</exception>
		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x06003F23 RID: 16163 RVA: 0x000D7A21 File Offset: 0x000D5C21
		// (set) Token: 0x06003F24 RID: 16164 RVA: 0x000D7A2E File Offset: 0x000D5C2E
		public bool DontFragment
		{
			get
			{
				return this.m_ClientSocket.DontFragment;
			}
			set
			{
				this.m_ClientSocket.DontFragment = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether outgoing multicast packets are delivered to the sending application.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.Sockets.UdpClient" /> receives outgoing multicast packets; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x06003F25 RID: 16165 RVA: 0x000D7A3C File Offset: 0x000D5C3C
		// (set) Token: 0x06003F26 RID: 16166 RVA: 0x000D7A49 File Offset: 0x000D5C49
		public bool MulticastLoopback
		{
			get
			{
				return this.m_ClientSocket.MulticastLoopback;
			}
			set
			{
				this.m_ClientSocket.MulticastLoopback = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Net.Sockets.UdpClient" /> may send or receive broadcast packets.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.Sockets.UdpClient" /> allows broadcast packets; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x06003F27 RID: 16167 RVA: 0x000D7A57 File Offset: 0x000D5C57
		// (set) Token: 0x06003F28 RID: 16168 RVA: 0x000D7A64 File Offset: 0x000D5C64
		public bool EnableBroadcast
		{
			get
			{
				return this.m_ClientSocket.EnableBroadcast;
			}
			set
			{
				this.m_ClientSocket.EnableBroadcast = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Net.Sockets.UdpClient" /> allows only one client to use a port.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.Sockets.UdpClient" /> allows only one client to use a specific port; otherwise, <see langword="false" />. The default is <see langword="true" /> for Windows Server 2003 and Windows XP Service Pack 2 and later, and <see langword="false" /> for all other versions.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the underlying socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x06003F29 RID: 16169 RVA: 0x000D7A72 File Offset: 0x000D5C72
		// (set) Token: 0x06003F2A RID: 16170 RVA: 0x000D7A7F File Offset: 0x000D5C7F
		public bool ExclusiveAddressUse
		{
			get
			{
				return this.m_ClientSocket.ExclusiveAddressUse;
			}
			set
			{
				this.m_ClientSocket.ExclusiveAddressUse = value;
			}
		}

		/// <summary>Enables or disables Network Address Translation (NAT) traversal on a <see cref="T:System.Net.Sockets.UdpClient" /> instance.</summary>
		/// <param name="allowed">A Boolean value that specifies whether to enable or disable NAT traversal.</param>
		// Token: 0x06003F2B RID: 16171 RVA: 0x000D7A8D File Offset: 0x000D5C8D
		public void AllowNatTraversal(bool allowed)
		{
			if (allowed)
			{
				this.m_ClientSocket.SetIPProtectionLevel(IPProtectionLevel.Unrestricted);
				return;
			}
			this.m_ClientSocket.SetIPProtectionLevel(IPProtectionLevel.EdgeRestricted);
		}

		/// <summary>Closes the UDP connection.</summary>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		// Token: 0x06003F2C RID: 16172 RVA: 0x000D7AAD File Offset: 0x000D5CAD
		public void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06003F2D RID: 16173 RVA: 0x000D7AB8 File Offset: 0x000D5CB8
		private void FreeResources()
		{
			if (this.m_CleanedUp)
			{
				return;
			}
			Socket client = this.Client;
			if (client != null)
			{
				client.InternalShutdown(SocketShutdown.Both);
				client.Close();
				this.Client = null;
			}
			this.m_CleanedUp = true;
		}

		/// <summary>Releases the managed and unmanaged resources used by the <see cref="T:System.Net.Sockets.UdpClient" />.</summary>
		// Token: 0x06003F2E RID: 16174 RVA: 0x000D7AAD File Offset: 0x000D5CAD
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Sockets.UdpClient" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06003F2F RID: 16175 RVA: 0x000D7AF3 File Offset: 0x000D5CF3
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.FreeResources();
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>Establishes a default remote host using the specified host name and port number.</summary>
		/// <param name="hostname">The DNS name of the remote host to which you intend send data.</param>
		/// <param name="port">The port number on the remote host to which you intend to send data.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.UdpClient" /> is closed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is not between <see cref="F:System.Net.IPEndPoint.MinPort" /> and <see cref="F:System.Net.IPEndPoint.MaxPort" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		// Token: 0x06003F30 RID: 16176 RVA: 0x000D7B04 File Offset: 0x000D5D04
		public void Connect(string hostname, int port)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (hostname == null)
			{
				throw new ArgumentNullException("hostname");
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			IPAddress[] hostAddresses = Dns.GetHostAddresses(hostname);
			Exception ex = null;
			Socket socket = null;
			Socket socket2 = null;
			try
			{
				if (this.m_ClientSocket == null)
				{
					if (Socket.OSSupportsIPv4)
					{
						socket2 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
					}
					if (Socket.OSSupportsIPv6)
					{
						socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Dgram, ProtocolType.Udp);
					}
				}
				foreach (IPAddress ipaddress in hostAddresses)
				{
					try
					{
						if (this.m_ClientSocket == null)
						{
							if (ipaddress.AddressFamily == AddressFamily.InterNetwork && socket2 != null)
							{
								socket2.Connect(ipaddress, port);
								this.m_ClientSocket = socket2;
								if (socket != null)
								{
									socket.Close();
								}
							}
							else if (socket != null)
							{
								socket.Connect(ipaddress, port);
								this.m_ClientSocket = socket;
								if (socket2 != null)
								{
									socket2.Close();
								}
							}
							this.m_Family = ipaddress.AddressFamily;
							this.m_Active = true;
							break;
						}
						if (ipaddress.AddressFamily == this.m_Family)
						{
							this.Connect(new IPEndPoint(ipaddress, port));
							this.m_Active = true;
							break;
						}
					}
					catch (Exception ex2)
					{
						if (NclUtilities.IsFatal(ex2))
						{
							throw;
						}
						ex = ex2;
					}
				}
			}
			catch (Exception ex3)
			{
				if (NclUtilities.IsFatal(ex3))
				{
					throw;
				}
				ex = ex3;
			}
			finally
			{
				if (!this.m_Active)
				{
					if (socket != null)
					{
						socket.Close();
					}
					if (socket2 != null)
					{
						socket2.Close();
					}
					if (ex != null)
					{
						throw ex;
					}
					throw new SocketException(SocketError.NotConnected);
				}
			}
		}

		/// <summary>Establishes a default remote host using the specified IP address and port number.</summary>
		/// <param name="addr">The <see cref="T:System.Net.IPAddress" /> of the remote host to which you intend to send data.</param>
		/// <param name="port">The port number to which you intend send data.</param>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="T:System.Net.Sockets.UdpClient" /> is closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="addr" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is not between <see cref="F:System.Net.IPEndPoint.MinPort" /> and <see cref="F:System.Net.IPEndPoint.MaxPort" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		// Token: 0x06003F31 RID: 16177 RVA: 0x000D7CA0 File Offset: 0x000D5EA0
		public void Connect(IPAddress addr, int port)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (addr == null)
			{
				throw new ArgumentNullException("addr");
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			IPEndPoint endPoint = new IPEndPoint(addr, port);
			this.Connect(endPoint);
		}

		/// <summary>Establishes a default remote host using the specified network endpoint.</summary>
		/// <param name="endPoint">An <see cref="T:System.Net.IPEndPoint" /> that specifies the network endpoint to which you intend to send data.</param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="endPoint" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.UdpClient" /> is closed.</exception>
		// Token: 0x06003F32 RID: 16178 RVA: 0x000D7CF8 File Offset: 0x000D5EF8
		public void Connect(IPEndPoint endPoint)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (endPoint == null)
			{
				throw new ArgumentNullException("endPoint");
			}
			this.CheckForBroadcast(endPoint.Address);
			this.Client.Connect(endPoint);
			this.m_Active = true;
		}

		// Token: 0x06003F33 RID: 16179 RVA: 0x000D7D4B File Offset: 0x000D5F4B
		private void CheckForBroadcast(IPAddress ipAddress)
		{
			if (this.Client != null && !this.m_IsBroadcast && UdpClient.IsBroadcast(ipAddress))
			{
				this.m_IsBroadcast = true;
				this.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
			}
		}

		// Token: 0x06003F34 RID: 16180 RVA: 0x000D7D7F File Offset: 0x000D5F7F
		private static bool IsBroadcast(IPAddress address)
		{
			return address.AddressFamily != AddressFamily.InterNetworkV6 && address.Equals(IPAddress.Broadcast);
		}

		/// <summary>Sends a UDP datagram to the host at the specified remote endpoint.</summary>
		/// <param name="dgram">An array of type <see cref="T:System.Byte" /> that specifies the UDP datagram that you intend to send, represented as an array of bytes.</param>
		/// <param name="bytes">The number of bytes in the datagram.</param>
		/// <param name="endPoint">An <see cref="T:System.Net.IPEndPoint" /> that represents the host and port to which to send the datagram.</param>
		/// <returns>The number of bytes sent.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dgram" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="T:System.Net.Sockets.UdpClient" /> has already established a default remote host.</exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="T:System.Net.Sockets.UdpClient" /> is closed.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		// Token: 0x06003F35 RID: 16181 RVA: 0x000D7D98 File Offset: 0x000D5F98
		public int Send(byte[] dgram, int bytes, IPEndPoint endPoint)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (dgram == null)
			{
				throw new ArgumentNullException("dgram");
			}
			if (this.m_Active && endPoint != null)
			{
				throw new InvalidOperationException(SR.GetString("Cannot send packets to an arbitrary host while connected."));
			}
			if (endPoint == null)
			{
				return this.Client.Send(dgram, 0, bytes, SocketFlags.None);
			}
			this.CheckForBroadcast(endPoint.Address);
			return this.Client.SendTo(dgram, 0, bytes, SocketFlags.None, endPoint);
		}

		/// <summary>Sends a UDP datagram to a specified port on a specified remote host.</summary>
		/// <param name="dgram">An array of type <see cref="T:System.Byte" /> that specifies the UDP datagram that you intend to send represented as an array of bytes.</param>
		/// <param name="bytes">The number of bytes in the datagram.</param>
		/// <param name="hostname">The name of the remote host to which you intend to send the datagram.</param>
		/// <param name="port">The remote port number with which you intend to communicate.</param>
		/// <returns>The number of bytes sent.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dgram" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.UdpClient" /> has already established a default remote host.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.UdpClient" /> is closed.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		// Token: 0x06003F36 RID: 16182 RVA: 0x000D7E18 File Offset: 0x000D6018
		public int Send(byte[] dgram, int bytes, string hostname, int port)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (dgram == null)
			{
				throw new ArgumentNullException("dgram");
			}
			if (this.m_Active && (hostname != null || port != 0))
			{
				throw new InvalidOperationException(SR.GetString("Cannot send packets to an arbitrary host while connected."));
			}
			if (hostname == null || port == 0)
			{
				return this.Client.Send(dgram, 0, bytes, SocketFlags.None);
			}
			IPAddress[] hostAddresses = Dns.GetHostAddresses(hostname);
			int num = 0;
			while (num < hostAddresses.Length && hostAddresses[num].AddressFamily != this.m_Family)
			{
				num++;
			}
			if (hostAddresses.Length == 0 || num == hostAddresses.Length)
			{
				throw new ArgumentException(SR.GetString("None of the discovered or specified addresses match the socket address family."), "hostname");
			}
			this.CheckForBroadcast(hostAddresses[num]);
			IPEndPoint remoteEP = new IPEndPoint(hostAddresses[num], port);
			return this.Client.SendTo(dgram, 0, bytes, SocketFlags.None, remoteEP);
		}

		/// <summary>Sends a UDP datagram to a remote host.</summary>
		/// <param name="dgram">An array of type <see cref="T:System.Byte" /> that specifies the UDP datagram that you intend to send represented as an array of bytes.</param>
		/// <param name="bytes">The number of bytes in the datagram.</param>
		/// <returns>The number of bytes sent.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dgram" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.UdpClient" /> has already established a default remote host.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.UdpClient" /> is closed.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		// Token: 0x06003F37 RID: 16183 RVA: 0x000D7EEC File Offset: 0x000D60EC
		public int Send(byte[] dgram, int bytes)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (dgram == null)
			{
				throw new ArgumentNullException("dgram");
			}
			if (!this.m_Active)
			{
				throw new InvalidOperationException(SR.GetString("The operation is not allowed on non-connected sockets."));
			}
			return this.Client.Send(dgram, 0, bytes, SocketFlags.None);
		}

		/// <summary>Sends a datagram to a destination asynchronously. The destination is specified by a <see cref="T:System.Net.EndPoint" />.</summary>
		/// <param name="datagram">A <see cref="T:System.Byte" /> array that contains the data to be sent.</param>
		/// <param name="bytes">The number of bytes to send.</param>
		/// <param name="endPoint">The <see cref="T:System.Net.EndPoint" /> that represents the destination for the data.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the send operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that references the asynchronous send.</returns>
		// Token: 0x06003F38 RID: 16184 RVA: 0x000D7F48 File Offset: 0x000D6148
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginSend(byte[] datagram, int bytes, IPEndPoint endPoint, AsyncCallback requestCallback, object state)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (datagram == null)
			{
				throw new ArgumentNullException("datagram");
			}
			if (bytes > datagram.Length || bytes < 0)
			{
				throw new ArgumentOutOfRangeException("bytes");
			}
			if (this.m_Active && endPoint != null)
			{
				throw new InvalidOperationException(SR.GetString("Cannot send packets to an arbitrary host while connected."));
			}
			if (endPoint == null)
			{
				return this.Client.BeginSend(datagram, 0, bytes, SocketFlags.None, requestCallback, state);
			}
			this.CheckForBroadcast(endPoint.Address);
			return this.Client.BeginSendTo(datagram, 0, bytes, SocketFlags.None, endPoint, requestCallback, state);
		}

		/// <summary>Sends a datagram to a destination asynchronously. The destination is specified by the host name and port number.</summary>
		/// <param name="datagram">A <see cref="T:System.Byte" /> array that contains the data to be sent.</param>
		/// <param name="bytes">The number of bytes to send.</param>
		/// <param name="hostname">The destination host.</param>
		/// <param name="port">The destination port number.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the send operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that references the asynchronous send.</returns>
		// Token: 0x06003F39 RID: 16185 RVA: 0x000D7FE4 File Offset: 0x000D61E4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginSend(byte[] datagram, int bytes, string hostname, int port, AsyncCallback requestCallback, object state)
		{
			if (this.m_Active && (hostname != null || port != 0))
			{
				throw new InvalidOperationException(SR.GetString("Cannot send packets to an arbitrary host while connected."));
			}
			IPEndPoint endPoint = null;
			if (hostname != null && port != 0)
			{
				IPAddress[] hostAddresses = Dns.GetHostAddresses(hostname);
				int num = 0;
				while (num < hostAddresses.Length && hostAddresses[num].AddressFamily != this.m_Family)
				{
					num++;
				}
				if (hostAddresses.Length == 0 || num == hostAddresses.Length)
				{
					throw new ArgumentException(SR.GetString("None of the discovered or specified addresses match the socket address family."), "hostname");
				}
				this.CheckForBroadcast(hostAddresses[num]);
				endPoint = new IPEndPoint(hostAddresses[num], port);
			}
			return this.BeginSend(datagram, bytes, endPoint, requestCallback, state);
		}

		/// <summary>Sends a datagram to a remote host asynchronously. The destination was specified previously by a call to <see cref="Overload:System.Net.Sockets.UdpClient.Connect" />.</summary>
		/// <param name="datagram">A <see cref="T:System.Byte" /> array that contains the data to be sent.</param>
		/// <param name="bytes">The number of bytes to send.</param>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the send operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that references the asynchronous send.</returns>
		// Token: 0x06003F3A RID: 16186 RVA: 0x000D807E File Offset: 0x000D627E
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginSend(byte[] datagram, int bytes, AsyncCallback requestCallback, object state)
		{
			return this.BeginSend(datagram, bytes, null, requestCallback, state);
		}

		/// <summary>Ends a pending asynchronous send.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> object returned by a call to <see cref="Overload:System.Net.Sockets.UdpClient.BeginSend" />.</param>
		/// <returns>If successful, the number of bytes sent to the <see cref="T:System.Net.Sockets.UdpClient" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.Socket.BeginSend(System.Byte[],System.Int32,System.Int32,System.Net.Sockets.SocketFlags,System.AsyncCallback,System.Object)" /> method.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.Socket.EndSend(System.IAsyncResult)" /> was previously called for the asynchronous read.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the underlying socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x06003F3B RID: 16187 RVA: 0x000D808C File Offset: 0x000D628C
		public int EndSend(IAsyncResult asyncResult)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (this.m_Active)
			{
				return this.Client.EndSend(asyncResult);
			}
			return this.Client.EndSendTo(asyncResult);
		}

		/// <summary>Returns a UDP datagram that was sent by a remote host.</summary>
		/// <param name="remoteEP">An <see cref="T:System.Net.IPEndPoint" /> that represents the remote host from which the data was sent.</param>
		/// <returns>An array of type <see cref="T:System.Byte" /> that contains datagram data.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		// Token: 0x06003F3C RID: 16188 RVA: 0x000D80C8 File Offset: 0x000D62C8
		public byte[] Receive(ref IPEndPoint remoteEP)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			EndPoint endPoint;
			if (this.m_Family == AddressFamily.InterNetwork)
			{
				endPoint = IPEndPoint.Any;
			}
			else
			{
				endPoint = IPEndPoint.IPv6Any;
			}
			int num = this.Client.ReceiveFrom(this.m_Buffer, 65536, SocketFlags.None, ref endPoint);
			remoteEP = (IPEndPoint)endPoint;
			if (num < 65536)
			{
				byte[] array = new byte[num];
				Buffer.BlockCopy(this.m_Buffer, 0, array, 0, num);
				return array;
			}
			return this.m_Buffer;
		}

		/// <summary>Receives a datagram from a remote host asynchronously.</summary>
		/// <param name="requestCallback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object that contains information about the receive operation. This object is passed to the <paramref name="requestCallback" /> delegate when the operation is complete.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that references the asynchronous receive.</returns>
		// Token: 0x06003F3D RID: 16189 RVA: 0x000D8150 File Offset: 0x000D6350
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginReceive(AsyncCallback requestCallback, object state)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			EndPoint endPoint;
			if (this.m_Family == AddressFamily.InterNetwork)
			{
				endPoint = IPEndPoint.Any;
			}
			else
			{
				endPoint = IPEndPoint.IPv6Any;
			}
			return this.Client.BeginReceiveFrom(this.m_Buffer, 0, 65536, SocketFlags.None, ref endPoint, requestCallback, state);
		}

		/// <summary>Ends a pending asynchronous receive.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> object returned by a call to <see cref="M:System.Net.Sockets.UdpClient.BeginReceive(System.AsyncCallback,System.Object)" />.</param>
		/// <param name="remoteEP">The specified remote endpoint.</param>
		/// <returns>If successful, an array of bytes that contains datagram data.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not returned by a call to the <see cref="M:System.Net.Sockets.UdpClient.BeginReceive(System.AsyncCallback,System.Object)" /> method.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.Sockets.UdpClient.EndReceive(System.IAsyncResult,System.Net.IPEndPoint@)" /> was previously called for the asynchronous read.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the underlying <see cref="T:System.Net.Sockets.Socket" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x06003F3E RID: 16190 RVA: 0x000D81AC File Offset: 0x000D63AC
		public byte[] EndReceive(IAsyncResult asyncResult, ref IPEndPoint remoteEP)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			EndPoint endPoint;
			if (this.m_Family == AddressFamily.InterNetwork)
			{
				endPoint = IPEndPoint.Any;
			}
			else
			{
				endPoint = IPEndPoint.IPv6Any;
			}
			int num = this.Client.EndReceiveFrom(asyncResult, ref endPoint);
			remoteEP = (IPEndPoint)endPoint;
			if (num < 65536)
			{
				byte[] array = new byte[num];
				Buffer.BlockCopy(this.m_Buffer, 0, array, 0, num);
				return array;
			}
			return this.m_Buffer;
		}

		/// <summary>Adds a <see cref="T:System.Net.Sockets.UdpClient" /> to a multicast group.</summary>
		/// <param name="multicastAddr">The multicast <see cref="T:System.Net.IPAddress" /> of the group you want to join.</param>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		/// <exception cref="T:System.ArgumentException">The IP address is not compatible with the <see cref="T:System.Net.Sockets.AddressFamily" /> value that defines the addressing scheme of the socket.</exception>
		// Token: 0x06003F3F RID: 16191 RVA: 0x000D8228 File Offset: 0x000D6428
		public void JoinMulticastGroup(IPAddress multicastAddr)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (multicastAddr == null)
			{
				throw new ArgumentNullException("multicastAddr");
			}
			if (multicastAddr.AddressFamily != this.m_Family)
			{
				throw new ArgumentException(SR.GetString("Multicast family is not the same as the family of the '{0}' Client.", new object[]
				{
					"UDP"
				}), "multicastAddr");
			}
			if (this.m_Family == AddressFamily.InterNetwork)
			{
				MulticastOption optionValue = new MulticastOption(multicastAddr);
				this.Client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, optionValue);
				return;
			}
			IPv6MulticastOption optionValue2 = new IPv6MulticastOption(multicastAddr);
			this.Client.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.AddMembership, optionValue2);
		}

		/// <summary>Adds a <see cref="T:System.Net.Sockets.UdpClient" /> to a multicast group.</summary>
		/// <param name="multicastAddr">The multicast <see cref="T:System.Net.IPAddress" /> of the group you want to join.</param>
		/// <param name="localAddress">The local <see cref="T:System.Net.IPAddress" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		// Token: 0x06003F40 RID: 16192 RVA: 0x000D82C4 File Offset: 0x000D64C4
		public void JoinMulticastGroup(IPAddress multicastAddr, IPAddress localAddress)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (this.m_Family != AddressFamily.InterNetwork)
			{
				throw new SocketException(SocketError.OperationNotSupported);
			}
			MulticastOption optionValue = new MulticastOption(multicastAddr, localAddress);
			this.Client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, optionValue);
		}

		/// <summary>Adds a <see cref="T:System.Net.Sockets.UdpClient" /> to a multicast group.</summary>
		/// <param name="ifindex">The interface index associated with the local IP address on which to join the multicast group.</param>
		/// <param name="multicastAddr">The multicast <see cref="T:System.Net.IPAddress" /> of the group you want to join.</param>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		// Token: 0x06003F41 RID: 16193 RVA: 0x000D8318 File Offset: 0x000D6518
		public void JoinMulticastGroup(int ifindex, IPAddress multicastAddr)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (multicastAddr == null)
			{
				throw new ArgumentNullException("multicastAddr");
			}
			if (ifindex < 0)
			{
				throw new ArgumentException(SR.GetString("The specified value cannot be negative."), "ifindex");
			}
			if (this.m_Family != AddressFamily.InterNetworkV6)
			{
				throw new SocketException(SocketError.OperationNotSupported);
			}
			IPv6MulticastOption optionValue = new IPv6MulticastOption(multicastAddr, (long)ifindex);
			this.Client.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.AddMembership, optionValue);
		}

		/// <summary>Adds a <see cref="T:System.Net.Sockets.UdpClient" /> to a multicast group with the specified Time to Live (TTL).</summary>
		/// <param name="multicastAddr">The <see cref="T:System.Net.IPAddress" /> of the multicast group to join.</param>
		/// <param name="timeToLive">The Time to Live (TTL), measured in router hops.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The TTL provided is not between 0 and 255</exception>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="multicastAddr" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The IP address is not compatible with the <see cref="T:System.Net.Sockets.AddressFamily" /> value that defines the addressing scheme of the socket.</exception>
		// Token: 0x06003F42 RID: 16194 RVA: 0x000D8394 File Offset: 0x000D6594
		public void JoinMulticastGroup(IPAddress multicastAddr, int timeToLive)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (multicastAddr == null)
			{
				throw new ArgumentNullException("multicastAddr");
			}
			if (!ValidationHelper.ValidateRange(timeToLive, 0, 255))
			{
				throw new ArgumentOutOfRangeException("timeToLive");
			}
			this.JoinMulticastGroup(multicastAddr);
			this.Client.SetSocketOption((this.m_Family == AddressFamily.InterNetwork) ? SocketOptionLevel.IP : SocketOptionLevel.IPv6, SocketOptionName.MulticastTimeToLive, timeToLive);
		}

		/// <summary>Leaves a multicast group.</summary>
		/// <param name="multicastAddr">The <see cref="T:System.Net.IPAddress" /> of the multicast group to leave.</param>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		/// <exception cref="T:System.ArgumentException">The IP address is not compatible with the <see cref="T:System.Net.Sockets.AddressFamily" /> value that defines the addressing scheme of the socket.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="multicastAddr" /> is <see langword="null" />.</exception>
		// Token: 0x06003F43 RID: 16195 RVA: 0x000D8404 File Offset: 0x000D6604
		public void DropMulticastGroup(IPAddress multicastAddr)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (multicastAddr == null)
			{
				throw new ArgumentNullException("multicastAddr");
			}
			if (multicastAddr.AddressFamily != this.m_Family)
			{
				throw new ArgumentException(SR.GetString("Multicast family is not the same as the family of the '{0}' Client.", new object[]
				{
					"UDP"
				}), "multicastAddr");
			}
			if (this.m_Family == AddressFamily.InterNetwork)
			{
				MulticastOption optionValue = new MulticastOption(multicastAddr);
				this.Client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.DropMembership, optionValue);
				return;
			}
			IPv6MulticastOption optionValue2 = new IPv6MulticastOption(multicastAddr);
			this.Client.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.DropMembership, optionValue2);
		}

		/// <summary>Leaves a multicast group.</summary>
		/// <param name="multicastAddr">The <see cref="T:System.Net.IPAddress" /> of the multicast group to leave.</param>
		/// <param name="ifindex">The local address of the multicast group to leave.</param>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		/// <exception cref="T:System.ArgumentException">The IP address is not compatible with the <see cref="T:System.Net.Sockets.AddressFamily" /> value that defines the addressing scheme of the socket.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="multicastAddr" /> is <see langword="null" />.</exception>
		// Token: 0x06003F44 RID: 16196 RVA: 0x000D84A0 File Offset: 0x000D66A0
		public void DropMulticastGroup(IPAddress multicastAddr, int ifindex)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (multicastAddr == null)
			{
				throw new ArgumentNullException("multicastAddr");
			}
			if (ifindex < 0)
			{
				throw new ArgumentException(SR.GetString("The specified value cannot be negative."), "ifindex");
			}
			if (this.m_Family != AddressFamily.InterNetworkV6)
			{
				throw new SocketException(SocketError.OperationNotSupported);
			}
			IPv6MulticastOption optionValue = new IPv6MulticastOption(multicastAddr, (long)ifindex);
			this.Client.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.DropMembership, optionValue);
		}

		/// <summary>Sends a UDP datagram asynchronously to a remote host.</summary>
		/// <param name="datagram">An array of type <see cref="T:System.Byte" /> that specifies the UDP datagram that you intend to send represented as an array of bytes.</param>
		/// <param name="bytes">The number of bytes in the datagram.</param>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dgram" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.UdpClient" /> has already established a default remote host.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.UdpClient" /> is closed.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		// Token: 0x06003F45 RID: 16197 RVA: 0x000D851B File Offset: 0x000D671B
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<int> SendAsync(byte[] datagram, int bytes)
		{
			return Task<int>.Factory.FromAsync<byte[], int>(new Func<byte[], int, AsyncCallback, object, IAsyncResult>(this.BeginSend), new Func<IAsyncResult, int>(this.EndSend), datagram, bytes, null);
		}

		/// <summary>Sends a UDP datagram asynchronously to a remote host.</summary>
		/// <param name="datagram">An array of type <see cref="T:System.Byte" /> that specifies the UDP datagram that you intend to send represented as an array of bytes.</param>
		/// <param name="bytes">The number of bytes in the datagram.</param>
		/// <param name="endPoint">An <see cref="T:System.Net.IPEndPoint" /> that represents the host and port to which to send the datagram.</param>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dgram" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="T:System.Net.Sockets.UdpClient" /> has already established a default remote host.</exception>
		/// <exception cref="T:System.ObjectDisposedException">
		///   <see cref="T:System.Net.Sockets.UdpClient" /> is closed.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		// Token: 0x06003F46 RID: 16198 RVA: 0x000D8542 File Offset: 0x000D6742
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<int> SendAsync(byte[] datagram, int bytes, IPEndPoint endPoint)
		{
			return Task<int>.Factory.FromAsync<byte[], int, IPEndPoint>(new Func<byte[], int, IPEndPoint, AsyncCallback, object, IAsyncResult>(this.BeginSend), new Func<IAsyncResult, int>(this.EndSend), datagram, bytes, endPoint, null);
		}

		/// <summary>Sends a UDP datagram asynchronously to a remote host.</summary>
		/// <param name="datagram">An array of type <see cref="T:System.Byte" /> that specifies the UDP datagram that you intend to send represented as an array of bytes.</param>
		/// <param name="bytes">The number of bytes in the datagram.</param>
		/// <param name="hostname">The name of the remote host to which you intend to send the datagram.</param>
		/// <param name="port">The remote port number with which you intend to communicate.</param>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dgram" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.UdpClient" /> has already established a default remote host.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.UdpClient" /> is closed.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		// Token: 0x06003F47 RID: 16199 RVA: 0x000D856C File Offset: 0x000D676C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<int> SendAsync(byte[] datagram, int bytes, string hostname, int port)
		{
			return Task<int>.Factory.FromAsync((AsyncCallback callback, object state) => this.BeginSend(datagram, bytes, hostname, port, callback, state), new Func<IAsyncResult, int>(this.EndSend), null);
		}

		/// <summary>Returns a UDP datagram asynchronously that was sent by a remote host.</summary>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when accessing the socket.</exception>
		// Token: 0x06003F48 RID: 16200 RVA: 0x000D85C6 File Offset: 0x000D67C6
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<UdpReceiveResult> ReceiveAsync()
		{
			return Task<UdpReceiveResult>.Factory.FromAsync((AsyncCallback callback, object state) => this.BeginReceive(callback, state), delegate(IAsyncResult ar)
			{
				IPEndPoint remoteEndPoint = null;
				return new UdpReceiveResult(this.EndReceive(ar, ref remoteEndPoint), remoteEndPoint);
			}, null);
		}

		// Token: 0x06003F49 RID: 16201 RVA: 0x000D85EB File Offset: 0x000D67EB
		private void createClientSocket()
		{
			this.Client = new Socket(this.m_Family, SocketType.Dgram, ProtocolType.Udp);
		}

		// Token: 0x06003F4A RID: 16202 RVA: 0x000D8601 File Offset: 0x000D6801
		[CompilerGenerated]
		private IAsyncResult <ReceiveAsync>b__65_0(AsyncCallback callback, object state)
		{
			return this.BeginReceive(callback, state);
		}

		// Token: 0x06003F4B RID: 16203 RVA: 0x000D860C File Offset: 0x000D680C
		[CompilerGenerated]
		private UdpReceiveResult <ReceiveAsync>b__65_1(IAsyncResult ar)
		{
			IPEndPoint remoteEndPoint = null;
			return new UdpReceiveResult(this.EndReceive(ar, ref remoteEndPoint), remoteEndPoint);
		}

		// Token: 0x040025C6 RID: 9670
		private const int MaxUDPSize = 65536;

		// Token: 0x040025C7 RID: 9671
		private Socket m_ClientSocket;

		// Token: 0x040025C8 RID: 9672
		private bool m_Active;

		// Token: 0x040025C9 RID: 9673
		private byte[] m_Buffer;

		// Token: 0x040025CA RID: 9674
		private AddressFamily m_Family;

		// Token: 0x040025CB RID: 9675
		private bool m_CleanedUp;

		// Token: 0x040025CC RID: 9676
		private bool m_IsBroadcast;

		// Token: 0x020007BE RID: 1982
		[CompilerGenerated]
		private sealed class <>c__DisplayClass64_0
		{
			// Token: 0x06003F4C RID: 16204 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass64_0()
			{
			}

			// Token: 0x06003F4D RID: 16205 RVA: 0x000D862A File Offset: 0x000D682A
			internal IAsyncResult <SendAsync>b__0(AsyncCallback callback, object state)
			{
				return this.<>4__this.BeginSend(this.datagram, this.bytes, this.hostname, this.port, callback, state);
			}

			// Token: 0x040025CD RID: 9677
			public UdpClient <>4__this;

			// Token: 0x040025CE RID: 9678
			public byte[] datagram;

			// Token: 0x040025CF RID: 9679
			public int bytes;

			// Token: 0x040025D0 RID: 9680
			public string hostname;

			// Token: 0x040025D1 RID: 9681
			public int port;
		}
	}
}
