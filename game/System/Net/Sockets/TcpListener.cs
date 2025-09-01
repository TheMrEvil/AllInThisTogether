using System;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Net.Sockets
{
	/// <summary>Listens for connections from TCP network clients.</summary>
	// Token: 0x020007BB RID: 1979
	public class TcpListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.TcpListener" /> class with the specified local endpoint.</summary>
		/// <param name="localEP">An <see cref="T:System.Net.IPEndPoint" /> that represents the local endpoint to which to bind the listener <see cref="T:System.Net.Sockets.Socket" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="localEP" /> is <see langword="null" />.</exception>
		// Token: 0x06003F00 RID: 16128 RVA: 0x000D7374 File Offset: 0x000D5574
		public TcpListener(IPEndPoint localEP)
		{
			bool on = Logging.On;
			if (localEP == null)
			{
				throw new ArgumentNullException("localEP");
			}
			this.m_ServerSocketEP = localEP;
			this.m_ServerSocket = new Socket(this.m_ServerSocketEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			bool on2 = Logging.On;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.TcpListener" /> class that listens for incoming connection attempts on the specified local IP address and port number.</summary>
		/// <param name="localaddr">An <see cref="T:System.Net.IPAddress" /> that represents the local IP address.</param>
		/// <param name="port">The port on which to listen for incoming connection attempts.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="localaddr" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is not between <see cref="F:System.Net.IPEndPoint.MinPort" /> and <see cref="F:System.Net.IPEndPoint.MaxPort" />.</exception>
		// Token: 0x06003F01 RID: 16129 RVA: 0x000D73C0 File Offset: 0x000D55C0
		public TcpListener(IPAddress localaddr, int port)
		{
			bool on = Logging.On;
			if (localaddr == null)
			{
				throw new ArgumentNullException("localaddr");
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			this.m_ServerSocketEP = new IPEndPoint(localaddr, port);
			this.m_ServerSocket = new Socket(this.m_ServerSocketEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			bool on2 = Logging.On;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.TcpListener" /> class that listens on the specified port.</summary>
		/// <param name="port">The port on which to listen for incoming connection attempts.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is not between <see cref="F:System.Net.IPEndPoint.MinPort" /> and <see cref="F:System.Net.IPEndPoint.MaxPort" />.</exception>
		// Token: 0x06003F02 RID: 16130 RVA: 0x000D7428 File Offset: 0x000D5628
		[Obsolete("This method has been deprecated. Please use TcpListener(IPAddress localaddr, int port) instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public TcpListener(int port)
		{
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			this.m_ServerSocketEP = new IPEndPoint(IPAddress.Any, port);
			this.m_ServerSocket = new Socket(this.m_ServerSocketEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
		}

		/// <summary>Creates a new <see cref="T:System.Net.Sockets.TcpListener" /> instance to listen on the specified port.</summary>
		/// <param name="port">The port on which to listen for incoming connection attempts.</param>
		/// <returns>A new <see cref="T:System.Net.Sockets.TcpListener" /> instance to listen on the specified port.</returns>
		// Token: 0x06003F03 RID: 16131 RVA: 0x000D7477 File Offset: 0x000D5677
		public static TcpListener Create(int port)
		{
			bool on = Logging.On;
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			TcpListener tcpListener = new TcpListener(IPAddress.IPv6Any, port);
			tcpListener.Server.DualMode = true;
			bool on2 = Logging.On;
			return tcpListener;
		}

		/// <summary>Gets the underlying network <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>The underlying <see cref="T:System.Net.Sockets.Socket" />.</returns>
		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x06003F04 RID: 16132 RVA: 0x000D74AF File Offset: 0x000D56AF
		public Socket Server
		{
			get
			{
				return this.m_ServerSocket;
			}
		}

		/// <summary>Gets a value that indicates whether <see cref="T:System.Net.Sockets.TcpListener" /> is actively listening for client connections.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="T:System.Net.Sockets.TcpListener" /> is actively listening; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x06003F05 RID: 16133 RVA: 0x000D74B7 File Offset: 0x000D56B7
		protected bool Active
		{
			get
			{
				return this.m_Active;
			}
		}

		/// <summary>Gets the underlying <see cref="T:System.Net.EndPoint" /> of the current <see cref="T:System.Net.Sockets.TcpListener" />.</summary>
		/// <returns>The <see cref="T:System.Net.EndPoint" /> to which the <see cref="T:System.Net.Sockets.Socket" /> is bound.</returns>
		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x06003F06 RID: 16134 RVA: 0x000D74BF File Offset: 0x000D56BF
		public EndPoint LocalEndpoint
		{
			get
			{
				if (!this.m_Active)
				{
					return this.m_ServerSocketEP;
				}
				return this.m_ServerSocket.LocalEndPoint;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the <see cref="T:System.Net.Sockets.TcpListener" /> allows only one underlying socket to listen to a specific port.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.Sockets.TcpListener" /> allows only one <see cref="T:System.Net.Sockets.TcpListener" /> to listen to a specific port; otherwise, <see langword="false" />. . The default is <see langword="true" /> for Windows Server 2003 and Windows XP Service Pack 2 and later, and <see langword="false" /> for all other versions.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Net.Sockets.TcpListener" /> has been started. Call the <see cref="M:System.Net.Sockets.TcpListener.Stop" /> method and then set the <see cref="P:System.Net.Sockets.Socket.ExclusiveAddressUse" /> property.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the underlying socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x06003F07 RID: 16135 RVA: 0x000D74DB File Offset: 0x000D56DB
		// (set) Token: 0x06003F08 RID: 16136 RVA: 0x000D74E8 File Offset: 0x000D56E8
		public bool ExclusiveAddressUse
		{
			get
			{
				return this.m_ServerSocket.ExclusiveAddressUse;
			}
			set
			{
				if (this.m_Active)
				{
					throw new InvalidOperationException(SR.GetString("The TcpListener must not be listening before performing this operation."));
				}
				this.m_ServerSocket.ExclusiveAddressUse = value;
				this.m_ExclusiveAddressUse = value;
			}
		}

		/// <summary>Enables or disables Network Address Translation (NAT) traversal on a <see cref="T:System.Net.Sockets.TcpListener" /> instance.</summary>
		/// <param name="allowed">A Boolean value that specifies whether to enable or disable NAT traversal.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Net.Sockets.TcpListener.AllowNatTraversal(System.Boolean)" /> method was called after calling the <see cref="M:System.Net.Sockets.TcpListener.Start" /> method</exception>
		// Token: 0x06003F09 RID: 16137 RVA: 0x000D7515 File Offset: 0x000D5715
		public void AllowNatTraversal(bool allowed)
		{
			if (this.m_Active)
			{
				throw new InvalidOperationException(SR.GetString("The TcpListener must not be listening before performing this operation."));
			}
			if (allowed)
			{
				this.m_ServerSocket.SetIPProtectionLevel(IPProtectionLevel.Unrestricted);
				return;
			}
			this.m_ServerSocket.SetIPProtectionLevel(IPProtectionLevel.EdgeRestricted);
		}

		/// <summary>Starts listening for incoming connection requests.</summary>
		/// <exception cref="T:System.Net.Sockets.SocketException">Use the <see cref="P:System.Net.Sockets.SocketException.ErrorCode" /> property to obtain the specific error code. When you have obtained this code, you can refer to the Windows Sockets version 2 API error code documentation for a detailed description of the error.</exception>
		// Token: 0x06003F0A RID: 16138 RVA: 0x000D754D File Offset: 0x000D574D
		public void Start()
		{
			this.Start(int.MaxValue);
		}

		/// <summary>Starts listening for incoming connection requests with a maximum number of pending connection.</summary>
		/// <param name="backlog">The maximum length of the pending connections queue.</param>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred while accessing the socket.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="backlog" /> parameter is less than zero or exceeds the maximum number of permitted connections.</exception>
		/// <exception cref="T:System.InvalidOperationException">The underlying <see cref="T:System.Net.Sockets.Socket" /> is null.</exception>
		// Token: 0x06003F0B RID: 16139 RVA: 0x000D755C File Offset: 0x000D575C
		public void Start(int backlog)
		{
			if (backlog > 2147483647 || backlog < 0)
			{
				throw new ArgumentOutOfRangeException("backlog");
			}
			bool on = Logging.On;
			if (this.m_ServerSocket == null)
			{
				throw new InvalidOperationException(SR.GetString("The socket handle is not valid."));
			}
			if (this.m_Active)
			{
				bool on2 = Logging.On;
				return;
			}
			this.m_ServerSocket.Bind(this.m_ServerSocketEP);
			try
			{
				this.m_ServerSocket.Listen(backlog);
			}
			catch (SocketException)
			{
				this.Stop();
				throw;
			}
			this.m_Active = true;
			bool on3 = Logging.On;
		}

		/// <summary>Closes the listener.</summary>
		/// <exception cref="T:System.Net.Sockets.SocketException">Use the <see cref="P:System.Net.Sockets.SocketException.ErrorCode" /> property to obtain the specific error code. When you have obtained this code, you can refer to the Windows Sockets version 2 API error code documentation for a detailed description of the error.</exception>
		// Token: 0x06003F0C RID: 16140 RVA: 0x000D75F4 File Offset: 0x000D57F4
		public void Stop()
		{
			bool on = Logging.On;
			if (this.m_ServerSocket != null)
			{
				this.m_ServerSocket.Close();
				this.m_ServerSocket = null;
			}
			this.m_Active = false;
			this.m_ServerSocket = new Socket(this.m_ServerSocketEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			if (this.m_ExclusiveAddressUse)
			{
				this.m_ServerSocket.ExclusiveAddressUse = true;
			}
			bool on2 = Logging.On;
		}

		/// <summary>Determines if there are pending connection requests.</summary>
		/// <returns>
		///   <see langword="true" /> if connections are pending; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The listener has not been started with a call to <see cref="M:System.Net.Sockets.TcpListener.Start" />.</exception>
		// Token: 0x06003F0D RID: 16141 RVA: 0x000D765A File Offset: 0x000D585A
		public bool Pending()
		{
			if (!this.m_Active)
			{
				throw new InvalidOperationException(SR.GetString("Not listening. You must call the Start() method before calling this method."));
			}
			return this.m_ServerSocket.Poll(0, SelectMode.SelectRead);
		}

		/// <summary>Accepts a pending connection request.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.Socket" /> used to send and receive data.</returns>
		/// <exception cref="T:System.InvalidOperationException">The listener has not been started with a call to <see cref="M:System.Net.Sockets.TcpListener.Start" />.</exception>
		// Token: 0x06003F0E RID: 16142 RVA: 0x000D7681 File Offset: 0x000D5881
		public Socket AcceptSocket()
		{
			bool on = Logging.On;
			if (!this.m_Active)
			{
				throw new InvalidOperationException(SR.GetString("Not listening. You must call the Start() method before calling this method."));
			}
			Socket result = this.m_ServerSocket.Accept();
			bool on2 = Logging.On;
			return result;
		}

		/// <summary>Accepts a pending connection request.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.TcpClient" /> used to send and receive data.</returns>
		/// <exception cref="T:System.InvalidOperationException">The listener has not been started with a call to <see cref="M:System.Net.Sockets.TcpListener.Start" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">Use the <see cref="P:System.Net.Sockets.SocketException.ErrorCode" /> property to obtain the specific error code. When you have obtained this code, you can refer to the Windows Sockets version 2 API error code documentation for a detailed description of the error.</exception>
		// Token: 0x06003F0F RID: 16143 RVA: 0x000D76B2 File Offset: 0x000D58B2
		public TcpClient AcceptTcpClient()
		{
			bool on = Logging.On;
			if (!this.m_Active)
			{
				throw new InvalidOperationException(SR.GetString("Not listening. You must call the Start() method before calling this method."));
			}
			TcpClient result = new TcpClient(this.m_ServerSocket.Accept());
			bool on2 = Logging.On;
			return result;
		}

		/// <summary>Begins an asynchronous operation to accept an incoming connection attempt.</summary>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object containing information about the accept operation. This object is passed to the <paramref name="callback" /> delegate when the operation is complete.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous creation of the <see cref="T:System.Net.Sockets.Socket" />.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred while attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x06003F10 RID: 16144 RVA: 0x000D76E8 File Offset: 0x000D58E8
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginAcceptSocket(AsyncCallback callback, object state)
		{
			bool on = Logging.On;
			if (!this.m_Active)
			{
				throw new InvalidOperationException(SR.GetString("Not listening. You must call the Start() method before calling this method."));
			}
			IAsyncResult result = this.m_ServerSocket.BeginAccept(callback, state);
			bool on2 = Logging.On;
			return result;
		}

		/// <summary>Asynchronously accepts an incoming connection attempt and creates a new <see cref="T:System.Net.Sockets.Socket" /> to handle remote host communication.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> returned by a call to the <see cref="M:System.Net.Sockets.TcpListener.BeginAcceptSocket(System.AsyncCallback,System.Object)" /> method.</param>
		/// <returns>A <see cref="T:System.Net.Sockets.Socket" />.  
		///  The <see cref="T:System.Net.Sockets.Socket" /> used to send and receive data.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The underlying <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="asyncResult" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="asyncResult" /> parameter was not created by a call to the <see cref="M:System.Net.Sockets.TcpListener.BeginAcceptSocket(System.AsyncCallback,System.Object)" /> method.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Net.Sockets.TcpListener.EndAcceptSocket(System.IAsyncResult)" /> method was previously called.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred while attempting to access the <see cref="T:System.Net.Sockets.Socket" />.</exception>
		// Token: 0x06003F11 RID: 16145 RVA: 0x000D771C File Offset: 0x000D591C
		public Socket EndAcceptSocket(IAsyncResult asyncResult)
		{
			bool on = Logging.On;
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			SocketAsyncResult socketAsyncResult = asyncResult as SocketAsyncResult;
			object obj = (socketAsyncResult == null) ? null : socketAsyncResult.socket;
			if (obj == null)
			{
				throw new ArgumentException(SR.GetString("The IAsyncResult object was not returned from the corresponding asynchronous method on this class."), "asyncResult");
			}
			Socket result = obj.EndAccept(asyncResult);
			bool on2 = Logging.On;
			return result;
		}

		/// <summary>Begins an asynchronous operation to accept an incoming connection attempt.</summary>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete.</param>
		/// <param name="state">A user-defined object containing information about the accept operation. This object is passed to the <paramref name="callback" /> delegate when the operation is complete.</param>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that references the asynchronous creation of the <see cref="T:System.Net.Sockets.TcpClient" />.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">An error occurred while attempting to access the socket.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.Sockets.Socket" /> has been closed.</exception>
		// Token: 0x06003F12 RID: 16146 RVA: 0x000D76E8 File Offset: 0x000D58E8
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginAcceptTcpClient(AsyncCallback callback, object state)
		{
			bool on = Logging.On;
			if (!this.m_Active)
			{
				throw new InvalidOperationException(SR.GetString("Not listening. You must call the Start() method before calling this method."));
			}
			IAsyncResult result = this.m_ServerSocket.BeginAccept(callback, state);
			bool on2 = Logging.On;
			return result;
		}

		/// <summary>Asynchronously accepts an incoming connection attempt and creates a new <see cref="T:System.Net.Sockets.TcpClient" /> to handle remote host communication.</summary>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> returned by a call to the <see cref="M:System.Net.Sockets.TcpListener.BeginAcceptTcpClient(System.AsyncCallback,System.Object)" /> method.</param>
		/// <returns>A <see cref="T:System.Net.Sockets.TcpClient" />.  
		///  The <see cref="T:System.Net.Sockets.TcpClient" /> used to send and receive data.</returns>
		// Token: 0x06003F13 RID: 16147 RVA: 0x000D7774 File Offset: 0x000D5974
		public TcpClient EndAcceptTcpClient(IAsyncResult asyncResult)
		{
			bool on = Logging.On;
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			SocketAsyncResult socketAsyncResult = asyncResult as SocketAsyncResult;
			object obj = (socketAsyncResult == null) ? null : socketAsyncResult.socket;
			if (obj == null)
			{
				throw new ArgumentException(SR.GetString("The IAsyncResult object was not returned from the corresponding asynchronous method on this class."), "asyncResult");
			}
			Socket acceptedSocket = obj.EndAccept(asyncResult);
			bool on2 = Logging.On;
			return new TcpClient(acceptedSocket);
		}

		/// <summary>Accepts a pending connection request as an asynchronous operation.</summary>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Net.Sockets.Socket" /> used to send and receive data.</returns>
		/// <exception cref="T:System.InvalidOperationException">The listener has not been started with a call to <see cref="M:System.Net.Sockets.TcpListener.Start" />.</exception>
		// Token: 0x06003F14 RID: 16148 RVA: 0x000D77D1 File Offset: 0x000D59D1
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<Socket> AcceptSocketAsync()
		{
			return Task<Socket>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginAcceptSocket), new Func<IAsyncResult, Socket>(this.EndAcceptSocket), null);
		}

		/// <summary>Accepts a pending connection request as an asynchronous operation.</summary>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns a <see cref="T:System.Net.Sockets.TcpClient" /> used to send and receive data.</returns>
		/// <exception cref="T:System.InvalidOperationException">The listener has not been started with a call to <see cref="M:System.Net.Sockets.TcpListener.Start" />.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">Use the <see cref="P:System.Net.Sockets.SocketException.ErrorCode" /> property to obtain the specific error code. When you have obtained this code, you can refer to the Windows Sockets version 2 API error code documentation for a detailed description of the error.</exception>
		// Token: 0x06003F15 RID: 16149 RVA: 0x000D77F6 File Offset: 0x000D59F6
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<TcpClient> AcceptTcpClientAsync()
		{
			return Task<TcpClient>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginAcceptTcpClient), new Func<IAsyncResult, TcpClient>(this.EndAcceptTcpClient), null);
		}

		// Token: 0x040025BB RID: 9659
		private IPEndPoint m_ServerSocketEP;

		// Token: 0x040025BC RID: 9660
		private Socket m_ServerSocket;

		// Token: 0x040025BD RID: 9661
		private bool m_Active;

		// Token: 0x040025BE RID: 9662
		private bool m_ExclusiveAddressUse;
	}
}
