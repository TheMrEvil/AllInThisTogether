using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using WebSocketSharp.Net;
using WebSocketSharp.Net.WebSockets;

namespace WebSocketSharp.Server
{
	// Token: 0x02000045 RID: 69
	public class WebSocketServer
	{
		// Token: 0x06000460 RID: 1120 RVA: 0x00019165 File Offset: 0x00017365
		static WebSocketServer()
		{
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00019174 File Offset: 0x00017374
		public WebSocketServer()
		{
			IPAddress any = IPAddress.Any;
			this.init(any.ToString(), any, 80, false);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x000191A0 File Offset: 0x000173A0
		public WebSocketServer(int port) : this(port, port == 443)
		{
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x000191B4 File Offset: 0x000173B4
		public WebSocketServer(string url)
		{
			bool flag = url == null;
			if (flag)
			{
				throw new ArgumentNullException("url");
			}
			bool flag2 = url.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("An empty string.", "url");
			}
			Uri uri;
			string message;
			bool flag3 = !WebSocketServer.tryCreateUri(url, out uri, out message);
			if (flag3)
			{
				throw new ArgumentException(message, "url");
			}
			string dnsSafeHost = uri.DnsSafeHost;
			IPAddress ipaddress = dnsSafeHost.ToIPAddress();
			bool flag4 = ipaddress == null;
			if (flag4)
			{
				message = "The host part could not be converted to an IP address.";
				throw new ArgumentException(message, "url");
			}
			bool flag5 = !ipaddress.IsLocal();
			if (flag5)
			{
				message = "The IP address of the host is not a local IP address.";
				throw new ArgumentException(message, "url");
			}
			this.init(dnsSafeHost, ipaddress, uri.Port, uri.Scheme == "wss");
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00019288 File Offset: 0x00017488
		public WebSocketServer(int port, bool secure)
		{
			bool flag = !port.IsPortNumber();
			if (flag)
			{
				string message = "It is less than 1 or greater than 65535.";
				throw new ArgumentOutOfRangeException("port", message);
			}
			IPAddress any = IPAddress.Any;
			this.init(any.ToString(), any, port, secure);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x000192D3 File Offset: 0x000174D3
		public WebSocketServer(IPAddress address, int port) : this(address, port, port == 443)
		{
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x000192E8 File Offset: 0x000174E8
		public WebSocketServer(IPAddress address, int port, bool secure)
		{
			bool flag = address == null;
			if (flag)
			{
				throw new ArgumentNullException("address");
			}
			bool flag2 = !address.IsLocal();
			if (flag2)
			{
				string message = "It is not a local IP address.";
				throw new ArgumentException(message, "address");
			}
			bool flag3 = !port.IsPortNumber();
			if (flag3)
			{
				string message2 = "It is less than 1 or greater than 65535.";
				throw new ArgumentOutOfRangeException("port", message2);
			}
			this.init(address.ToString(), address, port, secure);
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00019364 File Offset: 0x00017564
		public IPAddress Address
		{
			get
			{
				return this._address;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0001937C File Offset: 0x0001757C
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x00019394 File Offset: 0x00017594
		public bool AllowForwardedRequest
		{
			get
			{
				return this._allowForwardedRequest;
			}
			set
			{
				object sync = this._sync;
				lock (sync)
				{
					bool flag = !this.canSet();
					if (!flag)
					{
						this._allowForwardedRequest = value;
					}
				}
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x000193E4 File Offset: 0x000175E4
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x000193FC File Offset: 0x000175FC
		public WebSocketSharp.Net.AuthenticationSchemes AuthenticationSchemes
		{
			get
			{
				return this._authSchemes;
			}
			set
			{
				object sync = this._sync;
				lock (sync)
				{
					bool flag = !this.canSet();
					if (!flag)
					{
						this._authSchemes = value;
					}
				}
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0001944C File Offset: 0x0001764C
		public bool IsListening
		{
			get
			{
				return this._state == ServerState.Start;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0001946C File Offset: 0x0001766C
		public bool IsSecure
		{
			get
			{
				return this._secure;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x00019484 File Offset: 0x00017684
		// (set) Token: 0x0600046F RID: 1135 RVA: 0x000194A1 File Offset: 0x000176A1
		public bool KeepClean
		{
			get
			{
				return this._services.KeepClean;
			}
			set
			{
				this._services.KeepClean = value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x000194B4 File Offset: 0x000176B4
		public Logger Log
		{
			get
			{
				return this._log;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x000194CC File Offset: 0x000176CC
		public int Port
		{
			get
			{
				return this._port;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x000194E4 File Offset: 0x000176E4
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x000194FC File Offset: 0x000176FC
		public string Realm
		{
			get
			{
				return this._realm;
			}
			set
			{
				object sync = this._sync;
				lock (sync)
				{
					bool flag = !this.canSet();
					if (!flag)
					{
						this._realm = value;
					}
				}
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x0001954C File Offset: 0x0001774C
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x00019564 File Offset: 0x00017764
		public bool ReuseAddress
		{
			get
			{
				return this._reuseAddress;
			}
			set
			{
				object sync = this._sync;
				lock (sync)
				{
					bool flag = !this.canSet();
					if (!flag)
					{
						this._reuseAddress = value;
					}
				}
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x000195B4 File Offset: 0x000177B4
		public ServerSslConfiguration SslConfiguration
		{
			get
			{
				bool flag = !this._secure;
				if (flag)
				{
					string message = "The server does not provide secure connections.";
					throw new InvalidOperationException(message);
				}
				return this.getSslConfiguration();
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x000195E8 File Offset: 0x000177E8
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x00019600 File Offset: 0x00017800
		public Func<IIdentity, WebSocketSharp.Net.NetworkCredential> UserCredentialsFinder
		{
			get
			{
				return this._userCredFinder;
			}
			set
			{
				object sync = this._sync;
				lock (sync)
				{
					bool flag = !this.canSet();
					if (!flag)
					{
						this._userCredFinder = value;
					}
				}
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00019650 File Offset: 0x00017850
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x0001966D File Offset: 0x0001786D
		public TimeSpan WaitTime
		{
			get
			{
				return this._services.WaitTime;
			}
			set
			{
				this._services.WaitTime = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x00019680 File Offset: 0x00017880
		public WebSocketServiceManager WebSocketServices
		{
			get
			{
				return this._services;
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00019698 File Offset: 0x00017898
		private void abort()
		{
			object sync = this._sync;
			lock (sync)
			{
				bool flag = this._state != ServerState.Start;
				if (flag)
				{
					return;
				}
				this._state = ServerState.ShuttingDown;
			}
			try
			{
				this._listener.Stop();
			}
			catch (Exception ex)
			{
				this._log.Fatal(ex.Message);
				this._log.Debug(ex.ToString());
			}
			try
			{
				this._services.Stop(1006, string.Empty);
			}
			catch (Exception ex2)
			{
				this._log.Fatal(ex2.Message);
				this._log.Debug(ex2.ToString());
			}
			this._state = ServerState.Stop;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00019790 File Offset: 0x00017990
		private bool authenticateClient(TcpListenerWebSocketContext context)
		{
			bool flag = this._authSchemes == WebSocketSharp.Net.AuthenticationSchemes.Anonymous;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = this._authSchemes == WebSocketSharp.Net.AuthenticationSchemes.None;
				result = (!flag2 && context.Authenticate(this._authSchemes, this._realmInUse, this._userCredFinder));
			}
			return result;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000197E0 File Offset: 0x000179E0
		private bool canSet()
		{
			return this._state == ServerState.Ready || this._state == ServerState.Stop;
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0001980C File Offset: 0x00017A0C
		private bool checkHostNameForRequest(string name)
		{
			return !this._dnsStyle || Uri.CheckHostName(name) != UriHostNameType.Dns || name == this._hostname;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00019840 File Offset: 0x00017A40
		private string getRealm()
		{
			string realm = this._realm;
			return (realm != null && realm.Length > 0) ? realm : WebSocketServer._defaultRealm;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00019870 File Offset: 0x00017A70
		private ServerSslConfiguration getSslConfiguration()
		{
			bool flag = this._sslConfig == null;
			if (flag)
			{
				this._sslConfig = new ServerSslConfiguration();
			}
			return this._sslConfig;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x000198A0 File Offset: 0x00017AA0
		private void init(string hostname, IPAddress address, int port, bool secure)
		{
			this._hostname = hostname;
			this._address = address;
			this._port = port;
			this._secure = secure;
			this._authSchemes = WebSocketSharp.Net.AuthenticationSchemes.Anonymous;
			this._dnsStyle = (Uri.CheckHostName(hostname) == UriHostNameType.Dns);
			this._listener = new TcpListener(address, port);
			this._log = new Logger();
			this._services = new WebSocketServiceManager(this._log);
			this._sync = new object();
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0001991C File Offset: 0x00017B1C
		private void processRequest(TcpListenerWebSocketContext context)
		{
			bool flag = !this.authenticateClient(context);
			if (flag)
			{
				context.Close(WebSocketSharp.Net.HttpStatusCode.Forbidden);
			}
			else
			{
				Uri requestUri = context.RequestUri;
				bool flag2 = requestUri == null;
				if (flag2)
				{
					context.Close(WebSocketSharp.Net.HttpStatusCode.BadRequest);
				}
				else
				{
					bool flag3 = !this._allowForwardedRequest;
					if (flag3)
					{
						bool flag4 = requestUri.Port != this._port;
						if (flag4)
						{
							context.Close(WebSocketSharp.Net.HttpStatusCode.BadRequest);
							return;
						}
						bool flag5 = !this.checkHostNameForRequest(requestUri.DnsSafeHost);
						if (flag5)
						{
							context.Close(WebSocketSharp.Net.HttpStatusCode.NotFound);
							return;
						}
					}
					string text = requestUri.AbsolutePath;
					bool flag6 = text.IndexOfAny(new char[]
					{
						'%',
						'+'
					}) > -1;
					if (flag6)
					{
						text = HttpUtility.UrlDecode(text, Encoding.UTF8);
					}
					WebSocketServiceHost webSocketServiceHost;
					bool flag7 = !this._services.InternalTryGetServiceHost(text, out webSocketServiceHost);
					if (flag7)
					{
						context.Close(WebSocketSharp.Net.HttpStatusCode.NotImplemented);
					}
					else
					{
						webSocketServiceHost.StartSession(context);
					}
				}
			}
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00019A30 File Offset: 0x00017C30
		private void receiveRequest()
		{
			for (;;)
			{
				TcpClient cl = null;
				try
				{
					cl = this._listener.AcceptTcpClient();
					ThreadPool.QueueUserWorkItem(delegate(object state)
					{
						try
						{
							TcpListenerWebSocketContext context = new TcpListenerWebSocketContext(cl, null, this._secure, this._sslConfigInUse, this._log);
							this.processRequest(context);
						}
						catch (Exception ex4)
						{
							this._log.Error(ex4.Message);
							this._log.Debug(ex4.ToString());
							cl.Close();
						}
					});
				}
				catch (SocketException ex)
				{
					bool flag = this._state == ServerState.ShuttingDown;
					if (flag)
					{
						this._log.Info("The underlying listener is stopped.");
						return;
					}
					this._log.Fatal(ex.Message);
					this._log.Debug(ex.ToString());
					break;
				}
				catch (InvalidOperationException ex2)
				{
					bool flag2 = this._state == ServerState.ShuttingDown;
					if (flag2)
					{
						this._log.Info("The underlying listener is stopped.");
						return;
					}
					this._log.Fatal(ex2.Message);
					this._log.Debug(ex2.ToString());
					break;
				}
				catch (Exception ex3)
				{
					this._log.Fatal(ex3.Message);
					this._log.Debug(ex3.ToString());
					bool flag3 = cl != null;
					if (flag3)
					{
						cl.Close();
					}
					bool flag4 = this._state == ServerState.ShuttingDown;
					if (flag4)
					{
						return;
					}
					break;
				}
			}
			this.abort();
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00019BB4 File Offset: 0x00017DB4
		private void start()
		{
			object sync = this._sync;
			lock (sync)
			{
				bool flag = this._state == ServerState.Start || this._state == ServerState.ShuttingDown;
				if (!flag)
				{
					bool secure = this._secure;
					if (secure)
					{
						ServerSslConfiguration sslConfiguration = this.getSslConfiguration();
						ServerSslConfiguration serverSslConfiguration = new ServerSslConfiguration(sslConfiguration);
						bool flag2 = serverSslConfiguration.ServerCertificate == null;
						if (flag2)
						{
							string message = "There is no server certificate for secure connection.";
							throw new InvalidOperationException(message);
						}
						this._sslConfigInUse = serverSslConfiguration;
					}
					this._realmInUse = this.getRealm();
					this._services.Start();
					try
					{
						this.startReceiving();
					}
					catch
					{
						this._services.Stop(1011, string.Empty);
						throw;
					}
					this._state = ServerState.Start;
				}
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00019CA4 File Offset: 0x00017EA4
		private void startReceiving()
		{
			bool reuseAddress = this._reuseAddress;
			if (reuseAddress)
			{
				this._listener.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
			}
			try
			{
				this._listener.Start();
			}
			catch (Exception innerException)
			{
				string message = "The underlying listener has failed to start.";
				throw new InvalidOperationException(message, innerException);
			}
			ThreadStart start = new ThreadStart(this.receiveRequest);
			this._receiveThread = new Thread(start);
			this._receiveThread.IsBackground = true;
			this._receiveThread.Start();
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00019D3C File Offset: 0x00017F3C
		private void stop(ushort code, string reason)
		{
			object sync = this._sync;
			lock (sync)
			{
				bool flag = this._state != ServerState.Start;
				if (flag)
				{
					return;
				}
				this._state = ServerState.ShuttingDown;
			}
			try
			{
				this.stopReceiving(5000);
			}
			catch (Exception ex)
			{
				this._log.Fatal(ex.Message);
				this._log.Debug(ex.ToString());
			}
			try
			{
				this._services.Stop(code, reason);
			}
			catch (Exception ex2)
			{
				this._log.Fatal(ex2.Message);
				this._log.Debug(ex2.ToString());
			}
			this._state = ServerState.Stop;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00019E2C File Offset: 0x0001802C
		private void stopReceiving(int millisecondsTimeout)
		{
			this._listener.Stop();
			this._receiveThread.Join(millisecondsTimeout);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00019E48 File Offset: 0x00018048
		private static bool tryCreateUri(string uriString, out Uri result, out string message)
		{
			bool flag = !uriString.TryCreateWebSocketUri(out result, out message);
			bool result2;
			if (flag)
			{
				result2 = false;
			}
			else
			{
				bool flag2 = result.PathAndQuery != "/";
				if (flag2)
				{
					result = null;
					message = "It includes either or both path and query components.";
					result2 = false;
				}
				else
				{
					result2 = true;
				}
			}
			return result2;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00019E92 File Offset: 0x00018092
		public void AddWebSocketService<TBehavior>(string path) where TBehavior : WebSocketBehavior, new()
		{
			this._services.AddService<TBehavior>(path, null);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00019EA3 File Offset: 0x000180A3
		public void AddWebSocketService<TBehavior>(string path, Action<TBehavior> initializer) where TBehavior : WebSocketBehavior, new()
		{
			this._services.AddService<TBehavior>(path, initializer);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00019EB4 File Offset: 0x000180B4
		public bool RemoveWebSocketService(string path)
		{
			return this._services.RemoveService(path);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00019ED4 File Offset: 0x000180D4
		public void Start()
		{
			bool flag = this._state == ServerState.Start || this._state == ServerState.ShuttingDown;
			if (!flag)
			{
				this.start();
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00019F08 File Offset: 0x00018108
		public void Stop()
		{
			bool flag = this._state != ServerState.Start;
			if (!flag)
			{
				this.stop(1001, string.Empty);
			}
		}

		// Token: 0x04000213 RID: 531
		private IPAddress _address;

		// Token: 0x04000214 RID: 532
		private bool _allowForwardedRequest;

		// Token: 0x04000215 RID: 533
		private WebSocketSharp.Net.AuthenticationSchemes _authSchemes;

		// Token: 0x04000216 RID: 534
		private static readonly string _defaultRealm = "SECRET AREA";

		// Token: 0x04000217 RID: 535
		private bool _dnsStyle;

		// Token: 0x04000218 RID: 536
		private string _hostname;

		// Token: 0x04000219 RID: 537
		private TcpListener _listener;

		// Token: 0x0400021A RID: 538
		private Logger _log;

		// Token: 0x0400021B RID: 539
		private int _port;

		// Token: 0x0400021C RID: 540
		private string _realm;

		// Token: 0x0400021D RID: 541
		private string _realmInUse;

		// Token: 0x0400021E RID: 542
		private Thread _receiveThread;

		// Token: 0x0400021F RID: 543
		private bool _reuseAddress;

		// Token: 0x04000220 RID: 544
		private bool _secure;

		// Token: 0x04000221 RID: 545
		private WebSocketServiceManager _services;

		// Token: 0x04000222 RID: 546
		private ServerSslConfiguration _sslConfig;

		// Token: 0x04000223 RID: 547
		private ServerSslConfiguration _sslConfigInUse;

		// Token: 0x04000224 RID: 548
		private volatile ServerState _state;

		// Token: 0x04000225 RID: 549
		private object _sync;

		// Token: 0x04000226 RID: 550
		private Func<IIdentity, WebSocketSharp.Net.NetworkCredential> _userCredFinder;

		// Token: 0x02000073 RID: 115
		[CompilerGenerated]
		private sealed class <>c__DisplayClass70_0
		{
			// Token: 0x060005D5 RID: 1493 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass70_0()
			{
			}

			// Token: 0x060005D6 RID: 1494 RVA: 0x0001F2F4 File Offset: 0x0001D4F4
			internal void <receiveRequest>b__0(object state)
			{
				try
				{
					TcpListenerWebSocketContext context = new TcpListenerWebSocketContext(this.cl, null, this.<>4__this._secure, this.<>4__this._sslConfigInUse, this.<>4__this._log);
					this.<>4__this.processRequest(context);
				}
				catch (Exception ex)
				{
					this.<>4__this._log.Error(ex.Message);
					this.<>4__this._log.Debug(ex.ToString());
					this.cl.Close();
				}
			}

			// Token: 0x040002F8 RID: 760
			public TcpClient cl;

			// Token: 0x040002F9 RID: 761
			public WebSocketServer <>4__this;
		}
	}
}
