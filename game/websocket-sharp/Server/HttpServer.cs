using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using WebSocketSharp.Net;
using WebSocketSharp.Net.WebSockets;

namespace WebSocketSharp.Server
{
	// Token: 0x02000046 RID: 70
	public class HttpServer
	{
		// Token: 0x0600048F RID: 1167 RVA: 0x00019F3B File Offset: 0x0001813B
		public HttpServer()
		{
			this.init("*", IPAddress.Any, 80, false);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00019F59 File Offset: 0x00018159
		public HttpServer(int port) : this(port, port == 443)
		{
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00019F6C File Offset: 0x0001816C
		public HttpServer(string url)
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
			bool flag3 = !HttpServer.tryCreateUri(url, out uri, out message);
			if (flag3)
			{
				throw new ArgumentException(message, "url");
			}
			string dnsSafeHost = uri.GetDnsSafeHost(true);
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
			this.init(dnsSafeHost, ipaddress, uri.Port, uri.Scheme == "https");
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0001A044 File Offset: 0x00018244
		public HttpServer(int port, bool secure)
		{
			bool flag = !port.IsPortNumber();
			if (flag)
			{
				string message = "It is less than 1 or greater than 65535.";
				throw new ArgumentOutOfRangeException("port", message);
			}
			this.init("*", IPAddress.Any, port, secure);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0001A08C File Offset: 0x0001828C
		public HttpServer(IPAddress address, int port) : this(address, port, port == 443)
		{
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0001A0A0 File Offset: 0x000182A0
		public HttpServer(IPAddress address, int port, bool secure)
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
			this.init(address.ToString(true), address, port, secure);
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0001A11C File Offset: 0x0001831C
		public IPAddress Address
		{
			get
			{
				return this._address;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x0001A134 File Offset: 0x00018334
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x0001A154 File Offset: 0x00018354
		public WebSocketSharp.Net.AuthenticationSchemes AuthenticationSchemes
		{
			get
			{
				return this._listener.AuthenticationSchemes;
			}
			set
			{
				object sync = this._sync;
				lock (sync)
				{
					bool flag = !this.canSet();
					if (!flag)
					{
						this._listener.AuthenticationSchemes = value;
					}
				}
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x0001A1A8 File Offset: 0x000183A8
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x0001A1C0 File Offset: 0x000183C0
		public string DocumentRootPath
		{
			get
			{
				return this._docRootPath;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					throw new ArgumentNullException("value");
				}
				bool flag2 = value.Length == 0;
				if (flag2)
				{
					throw new ArgumentException("An empty string.", "value");
				}
				value = value.TrimSlashOrBackslashFromEnd();
				bool flag3 = value == "/";
				if (flag3)
				{
					throw new ArgumentException("An absolute root.", "value");
				}
				bool flag4 = value == "\\";
				if (flag4)
				{
					throw new ArgumentException("An absolute root.", "value");
				}
				bool flag5 = value.Length == 2 && value[1] == ':';
				if (flag5)
				{
					throw new ArgumentException("An absolute root.", "value");
				}
				string text = null;
				try
				{
					text = Path.GetFullPath(value);
				}
				catch (Exception innerException)
				{
					throw new ArgumentException("An invalid path string.", "value", innerException);
				}
				bool flag6 = text == "/";
				if (flag6)
				{
					throw new ArgumentException("An absolute root.", "value");
				}
				text = text.TrimSlashOrBackslashFromEnd();
				bool flag7 = text.Length == 2 && text[1] == ':';
				if (flag7)
				{
					throw new ArgumentException("An absolute root.", "value");
				}
				object sync = this._sync;
				lock (sync)
				{
					bool flag8 = !this.canSet();
					if (!flag8)
					{
						this._docRootPath = value;
					}
				}
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0001A33C File Offset: 0x0001853C
		public bool IsListening
		{
			get
			{
				return this._state == ServerState.Start;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0001A35C File Offset: 0x0001855C
		public bool IsSecure
		{
			get
			{
				return this._secure;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0001A374 File Offset: 0x00018574
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x0001A391 File Offset: 0x00018591
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

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0001A3A4 File Offset: 0x000185A4
		public Logger Log
		{
			get
			{
				return this._log;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x0001A3BC File Offset: 0x000185BC
		public int Port
		{
			get
			{
				return this._port;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0001A3D4 File Offset: 0x000185D4
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x0001A3F4 File Offset: 0x000185F4
		public string Realm
		{
			get
			{
				return this._listener.Realm;
			}
			set
			{
				object sync = this._sync;
				lock (sync)
				{
					bool flag = !this.canSet();
					if (!flag)
					{
						this._listener.Realm = value;
					}
				}
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0001A448 File Offset: 0x00018648
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x0001A468 File Offset: 0x00018668
		public bool ReuseAddress
		{
			get
			{
				return this._listener.ReuseAddress;
			}
			set
			{
				object sync = this._sync;
				lock (sync)
				{
					bool flag = !this.canSet();
					if (!flag)
					{
						this._listener.ReuseAddress = value;
					}
				}
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0001A4BC File Offset: 0x000186BC
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
				return this._listener.SslConfiguration;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x0001A4F4 File Offset: 0x000186F4
		// (set) Token: 0x060004A6 RID: 1190 RVA: 0x0001A514 File Offset: 0x00018714
		public Func<IIdentity, WebSocketSharp.Net.NetworkCredential> UserCredentialsFinder
		{
			get
			{
				return this._listener.UserCredentialsFinder;
			}
			set
			{
				object sync = this._sync;
				lock (sync)
				{
					bool flag = !this.canSet();
					if (!flag)
					{
						this._listener.UserCredentialsFinder = value;
					}
				}
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0001A568 File Offset: 0x00018768
		// (set) Token: 0x060004A8 RID: 1192 RVA: 0x0001A585 File Offset: 0x00018785
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

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0001A598 File Offset: 0x00018798
		public WebSocketServiceManager WebSocketServices
		{
			get
			{
				return this._services;
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060004AA RID: 1194 RVA: 0x0001A5B0 File Offset: 0x000187B0
		// (remove) Token: 0x060004AB RID: 1195 RVA: 0x0001A5E8 File Offset: 0x000187E8
		public event EventHandler<HttpRequestEventArgs> OnConnect
		{
			[CompilerGenerated]
			add
			{
				EventHandler<HttpRequestEventArgs> eventHandler = this.OnConnect;
				EventHandler<HttpRequestEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<HttpRequestEventArgs> value2 = (EventHandler<HttpRequestEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<HttpRequestEventArgs>>(ref this.OnConnect, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<HttpRequestEventArgs> eventHandler = this.OnConnect;
				EventHandler<HttpRequestEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<HttpRequestEventArgs> value2 = (EventHandler<HttpRequestEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<HttpRequestEventArgs>>(ref this.OnConnect, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060004AC RID: 1196 RVA: 0x0001A620 File Offset: 0x00018820
		// (remove) Token: 0x060004AD RID: 1197 RVA: 0x0001A658 File Offset: 0x00018858
		public event EventHandler<HttpRequestEventArgs> OnDelete
		{
			[CompilerGenerated]
			add
			{
				EventHandler<HttpRequestEventArgs> eventHandler = this.OnDelete;
				EventHandler<HttpRequestEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<HttpRequestEventArgs> value2 = (EventHandler<HttpRequestEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<HttpRequestEventArgs>>(ref this.OnDelete, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<HttpRequestEventArgs> eventHandler = this.OnDelete;
				EventHandler<HttpRequestEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<HttpRequestEventArgs> value2 = (EventHandler<HttpRequestEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<HttpRequestEventArgs>>(ref this.OnDelete, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060004AE RID: 1198 RVA: 0x0001A690 File Offset: 0x00018890
		// (remove) Token: 0x060004AF RID: 1199 RVA: 0x0001A6C8 File Offset: 0x000188C8
		public event EventHandler<HttpRequestEventArgs> OnGet
		{
			[CompilerGenerated]
			add
			{
				EventHandler<HttpRequestEventArgs> eventHandler = this.OnGet;
				EventHandler<HttpRequestEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<HttpRequestEventArgs> value2 = (EventHandler<HttpRequestEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<HttpRequestEventArgs>>(ref this.OnGet, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<HttpRequestEventArgs> eventHandler = this.OnGet;
				EventHandler<HttpRequestEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<HttpRequestEventArgs> value2 = (EventHandler<HttpRequestEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<HttpRequestEventArgs>>(ref this.OnGet, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060004B0 RID: 1200 RVA: 0x0001A700 File Offset: 0x00018900
		// (remove) Token: 0x060004B1 RID: 1201 RVA: 0x0001A738 File Offset: 0x00018938
		public event EventHandler<HttpRequestEventArgs> OnHead
		{
			[CompilerGenerated]
			add
			{
				EventHandler<HttpRequestEventArgs> eventHandler = this.OnHead;
				EventHandler<HttpRequestEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<HttpRequestEventArgs> value2 = (EventHandler<HttpRequestEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<HttpRequestEventArgs>>(ref this.OnHead, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<HttpRequestEventArgs> eventHandler = this.OnHead;
				EventHandler<HttpRequestEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<HttpRequestEventArgs> value2 = (EventHandler<HttpRequestEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<HttpRequestEventArgs>>(ref this.OnHead, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060004B2 RID: 1202 RVA: 0x0001A770 File Offset: 0x00018970
		// (remove) Token: 0x060004B3 RID: 1203 RVA: 0x0001A7A8 File Offset: 0x000189A8
		public event EventHandler<HttpRequestEventArgs> OnOptions
		{
			[CompilerGenerated]
			add
			{
				EventHandler<HttpRequestEventArgs> eventHandler = this.OnOptions;
				EventHandler<HttpRequestEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<HttpRequestEventArgs> value2 = (EventHandler<HttpRequestEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<HttpRequestEventArgs>>(ref this.OnOptions, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<HttpRequestEventArgs> eventHandler = this.OnOptions;
				EventHandler<HttpRequestEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<HttpRequestEventArgs> value2 = (EventHandler<HttpRequestEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<HttpRequestEventArgs>>(ref this.OnOptions, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060004B4 RID: 1204 RVA: 0x0001A7E0 File Offset: 0x000189E0
		// (remove) Token: 0x060004B5 RID: 1205 RVA: 0x0001A818 File Offset: 0x00018A18
		public event EventHandler<HttpRequestEventArgs> OnPost
		{
			[CompilerGenerated]
			add
			{
				EventHandler<HttpRequestEventArgs> eventHandler = this.OnPost;
				EventHandler<HttpRequestEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<HttpRequestEventArgs> value2 = (EventHandler<HttpRequestEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<HttpRequestEventArgs>>(ref this.OnPost, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<HttpRequestEventArgs> eventHandler = this.OnPost;
				EventHandler<HttpRequestEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<HttpRequestEventArgs> value2 = (EventHandler<HttpRequestEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<HttpRequestEventArgs>>(ref this.OnPost, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060004B6 RID: 1206 RVA: 0x0001A850 File Offset: 0x00018A50
		// (remove) Token: 0x060004B7 RID: 1207 RVA: 0x0001A888 File Offset: 0x00018A88
		public event EventHandler<HttpRequestEventArgs> OnPut
		{
			[CompilerGenerated]
			add
			{
				EventHandler<HttpRequestEventArgs> eventHandler = this.OnPut;
				EventHandler<HttpRequestEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<HttpRequestEventArgs> value2 = (EventHandler<HttpRequestEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<HttpRequestEventArgs>>(ref this.OnPut, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<HttpRequestEventArgs> eventHandler = this.OnPut;
				EventHandler<HttpRequestEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<HttpRequestEventArgs> value2 = (EventHandler<HttpRequestEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<HttpRequestEventArgs>>(ref this.OnPut, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060004B8 RID: 1208 RVA: 0x0001A8C0 File Offset: 0x00018AC0
		// (remove) Token: 0x060004B9 RID: 1209 RVA: 0x0001A8F8 File Offset: 0x00018AF8
		public event EventHandler<HttpRequestEventArgs> OnTrace
		{
			[CompilerGenerated]
			add
			{
				EventHandler<HttpRequestEventArgs> eventHandler = this.OnTrace;
				EventHandler<HttpRequestEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<HttpRequestEventArgs> value2 = (EventHandler<HttpRequestEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<HttpRequestEventArgs>>(ref this.OnTrace, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<HttpRequestEventArgs> eventHandler = this.OnTrace;
				EventHandler<HttpRequestEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<HttpRequestEventArgs> value2 = (EventHandler<HttpRequestEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<HttpRequestEventArgs>>(ref this.OnTrace, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001A930 File Offset: 0x00018B30
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
				this._services.Stop(1006, string.Empty);
			}
			catch (Exception ex)
			{
				this._log.Fatal(ex.Message);
				this._log.Debug(ex.ToString());
			}
			try
			{
				this._listener.Abort();
			}
			catch (Exception ex2)
			{
				this._log.Fatal(ex2.Message);
				this._log.Debug(ex2.ToString());
			}
			this._state = ServerState.Stop;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0001AA28 File Offset: 0x00018C28
		private bool canSet()
		{
			return this._state == ServerState.Ready || this._state == ServerState.Stop;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001AA54 File Offset: 0x00018C54
		private bool checkCertificate(out string message)
		{
			message = null;
			bool flag = this._listener.SslConfiguration.ServerCertificate != null;
			string certificateFolderPath = this._listener.CertificateFolderPath;
			bool flag2 = EndPointListener.CertificateExists(this._port, certificateFolderPath);
			bool flag3 = flag || flag2;
			bool flag4 = !flag3;
			bool result;
			if (flag4)
			{
				message = "There is no server certificate for secure connection.";
				result = false;
			}
			else
			{
				bool flag5 = flag && flag2;
				bool flag6 = flag5;
				if (flag6)
				{
					string message2 = "The server certificate associated with the port is used.";
					this._log.Warn(message2);
				}
				result = true;
			}
			return result;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001AAD8 File Offset: 0x00018CD8
		private static WebSocketSharp.Net.HttpListener createListener(string hostname, int port, bool secure)
		{
			WebSocketSharp.Net.HttpListener httpListener = new WebSocketSharp.Net.HttpListener();
			string arg = secure ? "https" : "http";
			string uriPrefix = string.Format("{0}://{1}:{2}/", arg, hostname, port);
			httpListener.Prefixes.Add(uriPrefix);
			return httpListener;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0001AB24 File Offset: 0x00018D24
		private void init(string hostname, IPAddress address, int port, bool secure)
		{
			this._hostname = hostname;
			this._address = address;
			this._port = port;
			this._secure = secure;
			this._docRootPath = "./Public";
			this._listener = HttpServer.createListener(this._hostname, this._port, this._secure);
			this._log = this._listener.Log;
			this._services = new WebSocketServiceManager(this._log);
			this._sync = new object();
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0001ABA4 File Offset: 0x00018DA4
		private void processRequest(WebSocketSharp.Net.HttpListenerContext context)
		{
			string httpMethod = context.Request.HttpMethod;
			EventHandler<HttpRequestEventArgs> eventHandler = (httpMethod == "GET") ? this.OnGet : ((httpMethod == "HEAD") ? this.OnHead : ((httpMethod == "POST") ? this.OnPost : ((httpMethod == "PUT") ? this.OnPut : ((httpMethod == "DELETE") ? this.OnDelete : ((httpMethod == "CONNECT") ? this.OnConnect : ((httpMethod == "OPTIONS") ? this.OnOptions : ((httpMethod == "TRACE") ? this.OnTrace : null)))))));
			bool flag = eventHandler == null;
			if (flag)
			{
				context.ErrorStatusCode = 501;
				context.SendError();
			}
			else
			{
				HttpRequestEventArgs e = new HttpRequestEventArgs(context, this._docRootPath);
				eventHandler(this, e);
				context.Response.Close();
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001ACB0 File Offset: 0x00018EB0
		private void processRequest(HttpListenerWebSocketContext context)
		{
			Uri requestUri = context.RequestUri;
			bool flag = requestUri == null;
			if (flag)
			{
				context.Close(WebSocketSharp.Net.HttpStatusCode.BadRequest);
			}
			else
			{
				string text = requestUri.AbsolutePath;
				bool flag2 = text.IndexOfAny(new char[]
				{
					'%',
					'+'
				}) > -1;
				if (flag2)
				{
					text = HttpUtility.UrlDecode(text, Encoding.UTF8);
				}
				WebSocketServiceHost webSocketServiceHost;
				bool flag3 = !this._services.InternalTryGetServiceHost(text, out webSocketServiceHost);
				if (flag3)
				{
					context.Close(WebSocketSharp.Net.HttpStatusCode.NotImplemented);
				}
				else
				{
					webSocketServiceHost.StartSession(context);
				}
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001AD40 File Offset: 0x00018F40
		private void receiveRequest()
		{
			for (;;)
			{
				WebSocketSharp.Net.HttpListenerContext ctx = null;
				try
				{
					ctx = this._listener.GetContext();
					ThreadPool.QueueUserWorkItem(delegate(object state)
					{
						try
						{
							bool flag5 = ctx.Request.IsUpgradeRequest("websocket");
							if (flag5)
							{
								this.processRequest(ctx.GetWebSocketContext(null));
							}
							else
							{
								this.processRequest(ctx);
							}
						}
						catch (Exception ex4)
						{
							this._log.Error(ex4.Message);
							this._log.Debug(ex4.ToString());
							ctx.Connection.Close(true);
						}
					});
				}
				catch (WebSocketSharp.Net.HttpListenerException ex)
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
					bool flag3 = ctx != null;
					if (flag3)
					{
						ctx.Connection.Close(true);
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

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001AECC File Offset: 0x000190CC
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
						string message;
						bool flag2 = !this.checkCertificate(out message);
						if (flag2)
						{
							throw new InvalidOperationException(message);
						}
					}
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

		// Token: 0x060004C3 RID: 1219 RVA: 0x0001AF90 File Offset: 0x00019190
		private void startReceiving()
		{
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

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001B000 File Offset: 0x00019200
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
				this._services.Stop(code, reason);
			}
			catch (Exception ex)
			{
				this._log.Fatal(ex.Message);
				this._log.Debug(ex.ToString());
			}
			try
			{
				this.stopReceiving(5000);
			}
			catch (Exception ex2)
			{
				this._log.Fatal(ex2.Message);
				this._log.Debug(ex2.ToString());
			}
			this._state = ServerState.Stop;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001B0F0 File Offset: 0x000192F0
		private void stopReceiving(int millisecondsTimeout)
		{
			this._listener.Stop();
			this._receiveThread.Join(millisecondsTimeout);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0001B10C File Offset: 0x0001930C
		private static bool tryCreateUri(string uriString, out Uri result, out string message)
		{
			result = null;
			message = null;
			Uri uri = uriString.ToUri();
			bool flag = uri == null;
			bool result2;
			if (flag)
			{
				message = "An invalid URI string.";
				result2 = false;
			}
			else
			{
				bool flag2 = !uri.IsAbsoluteUri;
				if (flag2)
				{
					message = "A relative URI.";
					result2 = false;
				}
				else
				{
					string scheme = uri.Scheme;
					bool flag3 = scheme == "http" || scheme == "https";
					bool flag4 = !flag3;
					if (flag4)
					{
						message = "The scheme part is not 'http' or 'https'.";
						result2 = false;
					}
					else
					{
						bool flag5 = uri.PathAndQuery != "/";
						if (flag5)
						{
							message = "It includes either or both path and query components.";
							result2 = false;
						}
						else
						{
							bool flag6 = uri.Fragment.Length > 0;
							if (flag6)
							{
								message = "It includes the fragment component.";
								result2 = false;
							}
							else
							{
								bool flag7 = uri.Port == 0;
								if (flag7)
								{
									message = "The port part is zero.";
									result2 = false;
								}
								else
								{
									result = uri;
									result2 = true;
								}
							}
						}
					}
				}
			}
			return result2;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0001B205 File Offset: 0x00019405
		public void AddWebSocketService<TBehavior>(string path) where TBehavior : WebSocketBehavior, new()
		{
			this._services.AddService<TBehavior>(path, null);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001B216 File Offset: 0x00019416
		public void AddWebSocketService<TBehavior>(string path, Action<TBehavior> initializer) where TBehavior : WebSocketBehavior, new()
		{
			this._services.AddService<TBehavior>(path, initializer);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0001B228 File Offset: 0x00019428
		public bool RemoveWebSocketService(string path)
		{
			return this._services.RemoveService(path);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001B248 File Offset: 0x00019448
		public void Start()
		{
			bool flag = this._state == ServerState.Start || this._state == ServerState.ShuttingDown;
			if (!flag)
			{
				this.start();
			}
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001B27C File Offset: 0x0001947C
		public void Stop()
		{
			bool flag = this._state != ServerState.Start;
			if (!flag)
			{
				this.stop(1001, string.Empty);
			}
		}

		// Token: 0x04000227 RID: 551
		private IPAddress _address;

		// Token: 0x04000228 RID: 552
		private string _docRootPath;

		// Token: 0x04000229 RID: 553
		private string _hostname;

		// Token: 0x0400022A RID: 554
		private WebSocketSharp.Net.HttpListener _listener;

		// Token: 0x0400022B RID: 555
		private Logger _log;

		// Token: 0x0400022C RID: 556
		private int _port;

		// Token: 0x0400022D RID: 557
		private Thread _receiveThread;

		// Token: 0x0400022E RID: 558
		private bool _secure;

		// Token: 0x0400022F RID: 559
		private WebSocketServiceManager _services;

		// Token: 0x04000230 RID: 560
		private volatile ServerState _state;

		// Token: 0x04000231 RID: 561
		private object _sync;

		// Token: 0x04000232 RID: 562
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EventHandler<HttpRequestEventArgs> OnConnect;

		// Token: 0x04000233 RID: 563
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EventHandler<HttpRequestEventArgs> OnDelete;

		// Token: 0x04000234 RID: 564
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EventHandler<HttpRequestEventArgs> OnGet;

		// Token: 0x04000235 RID: 565
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EventHandler<HttpRequestEventArgs> OnHead;

		// Token: 0x04000236 RID: 566
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EventHandler<HttpRequestEventArgs> OnOptions;

		// Token: 0x04000237 RID: 567
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EventHandler<HttpRequestEventArgs> OnPost;

		// Token: 0x04000238 RID: 568
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EventHandler<HttpRequestEventArgs> OnPut;

		// Token: 0x04000239 RID: 569
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EventHandler<HttpRequestEventArgs> OnTrace;

		// Token: 0x02000074 RID: 116
		[CompilerGenerated]
		private sealed class <>c__DisplayClass83_0
		{
			// Token: 0x060005D7 RID: 1495 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass83_0()
			{
			}

			// Token: 0x060005D8 RID: 1496 RVA: 0x0001F390 File Offset: 0x0001D590
			internal void <receiveRequest>b__0(object state)
			{
				try
				{
					bool flag = this.ctx.Request.IsUpgradeRequest("websocket");
					if (flag)
					{
						this.<>4__this.processRequest(this.ctx.GetWebSocketContext(null));
					}
					else
					{
						this.<>4__this.processRequest(this.ctx);
					}
				}
				catch (Exception ex)
				{
					this.<>4__this._log.Error(ex.Message);
					this.<>4__this._log.Debug(ex.ToString());
					this.ctx.Connection.Close(true);
				}
			}

			// Token: 0x040002FA RID: 762
			public WebSocketSharp.Net.HttpListenerContext ctx;

			// Token: 0x040002FB RID: 763
			public HttpServer <>4__this;
		}
	}
}
