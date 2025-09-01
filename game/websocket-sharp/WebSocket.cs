using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using WebSocketSharp.Net;
using WebSocketSharp.Net.WebSockets;

namespace WebSocketSharp
{
	// Token: 0x02000007 RID: 7
	public class WebSocket : IDisposable
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00004230 File Offset: 0x00002430
		static WebSocket()
		{
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000425C File Offset: 0x0000245C
		internal WebSocket(HttpListenerWebSocketContext context, string protocol)
		{
			this._context = context;
			this._protocol = protocol;
			this._closeContext = new Action(context.Close);
			this._logger = context.Log;
			this._message = new Action<MessageEventArgs>(this.messages);
			this._secure = context.IsSecureConnection;
			this._stream = context.Stream;
			this._waitTime = TimeSpan.FromSeconds(1.0);
			this.init();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000042E4 File Offset: 0x000024E4
		internal WebSocket(TcpListenerWebSocketContext context, string protocol)
		{
			this._context = context;
			this._protocol = protocol;
			this._closeContext = new Action(context.Close);
			this._logger = context.Log;
			this._message = new Action<MessageEventArgs>(this.messages);
			this._secure = context.IsSecureConnection;
			this._stream = context.Stream;
			this._waitTime = TimeSpan.FromSeconds(1.0);
			this.init();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000436C File Offset: 0x0000256C
		public WebSocket(string url, params string[] protocols)
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
			string message;
			bool flag3 = !url.TryCreateWebSocketUri(out this._uri, out message);
			if (flag3)
			{
				throw new ArgumentException(message, "url");
			}
			bool flag4 = protocols != null && protocols.Length != 0;
			if (flag4)
			{
				bool flag5 = !WebSocket.checkProtocols(protocols, out message);
				if (flag5)
				{
					throw new ArgumentException(message, "protocols");
				}
				this._protocols = protocols;
			}
			this._base64Key = WebSocket.CreateBase64Key();
			this._client = true;
			this._logger = new Logger();
			this._message = new Action<MessageEventArgs>(this.messagec);
			this._secure = (this._uri.Scheme == "wss");
			this._waitTime = TimeSpan.FromSeconds(5.0);
			this.init();
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00004470 File Offset: 0x00002670
		internal CookieCollection CookieCollection
		{
			get
			{
				return this._cookies;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00004488 File Offset: 0x00002688
		// (set) Token: 0x0600007A RID: 122 RVA: 0x000044A0 File Offset: 0x000026A0
		internal Func<WebSocketContext, string> CustomHandshakeRequestChecker
		{
			get
			{
				return this._handshakeRequestChecker;
			}
			set
			{
				this._handshakeRequestChecker = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000044AC File Offset: 0x000026AC
		internal bool HasMessage
		{
			get
			{
				object forMessageEventQueue = this._forMessageEventQueue;
				bool result;
				lock (forMessageEventQueue)
				{
					result = (this._messageEventQueue.Count > 0);
				}
				return result;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000044F4 File Offset: 0x000026F4
		// (set) Token: 0x0600007D RID: 125 RVA: 0x0000450C File Offset: 0x0000270C
		internal bool IgnoreExtensions
		{
			get
			{
				return this._ignoreExtensions;
			}
			set
			{
				this._ignoreExtensions = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00004518 File Offset: 0x00002718
		internal bool IsConnected
		{
			get
			{
				return this._readyState == WebSocketState.Open || this._readyState == WebSocketState.Closing;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00004544 File Offset: 0x00002744
		// (set) Token: 0x06000080 RID: 128 RVA: 0x0000455C File Offset: 0x0000275C
		public CompressionMethod Compression
		{
			get
			{
				return this._compression;
			}
			set
			{
				string message = null;
				bool flag = !this._client;
				if (flag)
				{
					message = "This instance is not a client.";
					throw new InvalidOperationException(message);
				}
				bool flag2 = !this.canSet(out message);
				if (flag2)
				{
					this._logger.Warn(message);
				}
				else
				{
					object forState = this._forState;
					lock (forState)
					{
						bool flag3 = !this.canSet(out message);
						if (flag3)
						{
							this._logger.Warn(message);
						}
						else
						{
							this._compression = value;
						}
					}
				}
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000045FC File Offset: 0x000027FC
		public IEnumerable<Cookie> Cookies
		{
			get
			{
				object obj = this._cookies.SyncRoot;
				lock (obj)
				{
					foreach (Cookie cookie in this._cookies)
					{
						yield return cookie;
						cookie = null;
					}
					IEnumerator<Cookie> enumerator = null;
				}
				obj = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000461C File Offset: 0x0000281C
		public NetworkCredential Credentials
		{
			get
			{
				return this._credentials;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00004634 File Offset: 0x00002834
		// (set) Token: 0x06000084 RID: 132 RVA: 0x0000464C File Offset: 0x0000284C
		public bool EmitOnPing
		{
			get
			{
				return this._emitOnPing;
			}
			set
			{
				this._emitOnPing = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00004658 File Offset: 0x00002858
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00004670 File Offset: 0x00002870
		public bool EnableRedirection
		{
			get
			{
				return this._enableRedirection;
			}
			set
			{
				string message = null;
				bool flag = !this._client;
				if (flag)
				{
					message = "This instance is not a client.";
					throw new InvalidOperationException(message);
				}
				bool flag2 = !this.canSet(out message);
				if (flag2)
				{
					this._logger.Warn(message);
				}
				else
				{
					object forState = this._forState;
					lock (forState)
					{
						bool flag3 = !this.canSet(out message);
						if (flag3)
						{
							this._logger.Warn(message);
						}
						else
						{
							this._enableRedirection = value;
						}
					}
				}
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00004710 File Offset: 0x00002910
		public string Extensions
		{
			get
			{
				return this._extensions ?? string.Empty;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00004734 File Offset: 0x00002934
		public bool IsAlive
		{
			get
			{
				return this.ping(WebSocket.EmptyBytes);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00004754 File Offset: 0x00002954
		public bool IsSecure
		{
			get
			{
				return this._secure;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000476C File Offset: 0x0000296C
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00004786 File Offset: 0x00002986
		public Logger Log
		{
			get
			{
				return this._logger;
			}
			internal set
			{
				this._logger = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00004794 File Offset: 0x00002994
		// (set) Token: 0x0600008D RID: 141 RVA: 0x000047AC File Offset: 0x000029AC
		public string Origin
		{
			get
			{
				return this._origin;
			}
			set
			{
				string message = null;
				bool flag = !this._client;
				if (flag)
				{
					message = "This instance is not a client.";
					throw new InvalidOperationException(message);
				}
				bool flag2 = !value.IsNullOrEmpty();
				if (flag2)
				{
					Uri uri;
					bool flag3 = !Uri.TryCreate(value, UriKind.Absolute, out uri);
					if (flag3)
					{
						message = "Not an absolute URI string.";
						throw new ArgumentException(message, "value");
					}
					bool flag4 = uri.Segments.Length > 1;
					if (flag4)
					{
						message = "It includes the path segments.";
						throw new ArgumentException(message, "value");
					}
				}
				bool flag5 = !this.canSet(out message);
				if (flag5)
				{
					this._logger.Warn(message);
				}
				else
				{
					object forState = this._forState;
					lock (forState)
					{
						bool flag6 = !this.canSet(out message);
						if (flag6)
						{
							this._logger.Warn(message);
						}
						else
						{
							this._origin = ((!value.IsNullOrEmpty()) ? value.TrimEnd(new char[]
							{
								'/'
							}) : value);
						}
					}
				}
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000048C4 File Offset: 0x00002AC4
		// (set) Token: 0x0600008F RID: 143 RVA: 0x000048E5 File Offset: 0x00002AE5
		public string Protocol
		{
			get
			{
				return this._protocol ?? string.Empty;
			}
			internal set
			{
				this._protocol = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000048F0 File Offset: 0x00002AF0
		public WebSocketState ReadyState
		{
			get
			{
				return this._readyState;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000490C File Offset: 0x00002B0C
		public ClientSslConfiguration SslConfiguration
		{
			get
			{
				bool flag = !this._client;
				if (flag)
				{
					string message = "This instance is not a client.";
					throw new InvalidOperationException(message);
				}
				bool flag2 = !this._secure;
				if (flag2)
				{
					string message2 = "This instance does not use a secure connection.";
					throw new InvalidOperationException(message2);
				}
				return this.getSslConfiguration();
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000495C File Offset: 0x00002B5C
		public Uri Url
		{
			get
			{
				return this._client ? this._uri : this._context.RequestUri;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000498C File Offset: 0x00002B8C
		// (set) Token: 0x06000094 RID: 148 RVA: 0x000049A4 File Offset: 0x00002BA4
		public TimeSpan WaitTime
		{
			get
			{
				return this._waitTime;
			}
			set
			{
				bool flag = value <= TimeSpan.Zero;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("value", "Zero or less.");
				}
				string message;
				bool flag2 = !this.canSet(out message);
				if (flag2)
				{
					this._logger.Warn(message);
				}
				else
				{
					object forState = this._forState;
					lock (forState)
					{
						bool flag3 = !this.canSet(out message);
						if (flag3)
						{
							this._logger.Warn(message);
						}
						else
						{
							this._waitTime = value;
						}
					}
				}
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000095 RID: 149 RVA: 0x00004A48 File Offset: 0x00002C48
		// (remove) Token: 0x06000096 RID: 150 RVA: 0x00004A80 File Offset: 0x00002C80
		public event EventHandler<CloseEventArgs> OnClose
		{
			[CompilerGenerated]
			add
			{
				EventHandler<CloseEventArgs> eventHandler = this.OnClose;
				EventHandler<CloseEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<CloseEventArgs> value2 = (EventHandler<CloseEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<CloseEventArgs>>(ref this.OnClose, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<CloseEventArgs> eventHandler = this.OnClose;
				EventHandler<CloseEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<CloseEventArgs> value2 = (EventHandler<CloseEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<CloseEventArgs>>(ref this.OnClose, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000097 RID: 151 RVA: 0x00004AB8 File Offset: 0x00002CB8
		// (remove) Token: 0x06000098 RID: 152 RVA: 0x00004AF0 File Offset: 0x00002CF0
		public event EventHandler<ErrorEventArgs> OnError
		{
			[CompilerGenerated]
			add
			{
				EventHandler<ErrorEventArgs> eventHandler = this.OnError;
				EventHandler<ErrorEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<ErrorEventArgs> value2 = (EventHandler<ErrorEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<ErrorEventArgs>>(ref this.OnError, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<ErrorEventArgs> eventHandler = this.OnError;
				EventHandler<ErrorEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<ErrorEventArgs> value2 = (EventHandler<ErrorEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<ErrorEventArgs>>(ref this.OnError, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000099 RID: 153 RVA: 0x00004B28 File Offset: 0x00002D28
		// (remove) Token: 0x0600009A RID: 154 RVA: 0x00004B60 File Offset: 0x00002D60
		public event EventHandler<MessageEventArgs> OnMessage
		{
			[CompilerGenerated]
			add
			{
				EventHandler<MessageEventArgs> eventHandler = this.OnMessage;
				EventHandler<MessageEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<MessageEventArgs> value2 = (EventHandler<MessageEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<MessageEventArgs>>(ref this.OnMessage, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<MessageEventArgs> eventHandler = this.OnMessage;
				EventHandler<MessageEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<MessageEventArgs> value2 = (EventHandler<MessageEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<MessageEventArgs>>(ref this.OnMessage, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600009B RID: 155 RVA: 0x00004B98 File Offset: 0x00002D98
		// (remove) Token: 0x0600009C RID: 156 RVA: 0x00004BD0 File Offset: 0x00002DD0
		public event EventHandler OnOpen
		{
			[CompilerGenerated]
			add
			{
				EventHandler eventHandler = this.OnOpen;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnOpen, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler eventHandler = this.OnOpen;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnOpen, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004C08 File Offset: 0x00002E08
		private bool accept()
		{
			bool flag = this._readyState == WebSocketState.Open;
			bool result;
			if (flag)
			{
				string message = "The handshake request has already been accepted.";
				this._logger.Warn(message);
				result = false;
			}
			else
			{
				object forState = this._forState;
				lock (forState)
				{
					bool flag2 = this._readyState == WebSocketState.Open;
					if (flag2)
					{
						string message2 = "The handshake request has already been accepted.";
						this._logger.Warn(message2);
						result = false;
					}
					else
					{
						bool flag3 = this._readyState == WebSocketState.Closing;
						if (flag3)
						{
							string message3 = "The close process has set in.";
							this._logger.Error(message3);
							message3 = "An interruption has occurred while attempting to accept.";
							this.error(message3, null);
							result = false;
						}
						else
						{
							bool flag4 = this._readyState == WebSocketState.Closed;
							if (flag4)
							{
								string message4 = "The connection has been closed.";
								this._logger.Error(message4);
								message4 = "An interruption has occurred while attempting to accept.";
								this.error(message4, null);
								result = false;
							}
							else
							{
								try
								{
									bool flag5 = !this.acceptHandshake();
									if (flag5)
									{
										return false;
									}
								}
								catch (Exception ex)
								{
									this._logger.Fatal(ex.Message);
									this._logger.Debug(ex.ToString());
									string message5 = "An exception has occurred while attempting to accept.";
									this.fatal(message5, ex);
									return false;
								}
								this._readyState = WebSocketState.Open;
								result = true;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004DA8 File Offset: 0x00002FA8
		private bool acceptHandshake()
		{
			this._logger.Debug(string.Format("A handshake request from {0}:\n{1}", this._context.UserEndPoint, this._context));
			string message;
			bool flag = !this.checkHandshakeRequest(this._context, out message);
			bool result;
			if (flag)
			{
				this._logger.Error(message);
				this.refuseHandshake(CloseStatusCode.ProtocolError, "A handshake error has occurred while attempting to accept.");
				result = false;
			}
			else
			{
				bool flag2 = !this.customCheckHandshakeRequest(this._context, out message);
				if (flag2)
				{
					this._logger.Error(message);
					this.refuseHandshake(CloseStatusCode.PolicyViolation, "A handshake error has occurred while attempting to accept.");
					result = false;
				}
				else
				{
					this._base64Key = this._context.Headers["Sec-WebSocket-Key"];
					bool flag3 = this._protocol != null;
					if (flag3)
					{
						IEnumerable<string> secWebSocketProtocols = this._context.SecWebSocketProtocols;
						this.processSecWebSocketProtocolClientHeader(secWebSocketProtocols);
					}
					bool flag4 = !this._ignoreExtensions;
					if (flag4)
					{
						string value = this._context.Headers["Sec-WebSocket-Extensions"];
						this.processSecWebSocketExtensionsClientHeader(value);
					}
					result = this.sendHttpResponse(this.createHandshakeResponse());
				}
			}
			return result;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004EDC File Offset: 0x000030DC
		private bool canSet(out string message)
		{
			message = null;
			bool flag = this._readyState == WebSocketState.Open;
			bool result;
			if (flag)
			{
				message = "The connection has already been established.";
				result = false;
			}
			else
			{
				bool flag2 = this._readyState == WebSocketState.Closing;
				if (flag2)
				{
					message = "The connection is closing.";
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004F28 File Offset: 0x00003128
		private bool checkHandshakeRequest(WebSocketContext context, out string message)
		{
			message = null;
			bool flag = !context.IsWebSocketRequest;
			bool result;
			if (flag)
			{
				message = "Not a handshake request.";
				result = false;
			}
			else
			{
				bool flag2 = context.RequestUri == null;
				if (flag2)
				{
					message = "It specifies an invalid Request-URI.";
					result = false;
				}
				else
				{
					NameValueCollection headers = context.Headers;
					string text = headers["Sec-WebSocket-Key"];
					bool flag3 = text == null;
					if (flag3)
					{
						message = "It includes no Sec-WebSocket-Key header.";
						result = false;
					}
					else
					{
						bool flag4 = text.Length == 0;
						if (flag4)
						{
							message = "It includes an invalid Sec-WebSocket-Key header.";
							result = false;
						}
						else
						{
							string text2 = headers["Sec-WebSocket-Version"];
							bool flag5 = text2 == null;
							if (flag5)
							{
								message = "It includes no Sec-WebSocket-Version header.";
								result = false;
							}
							else
							{
								bool flag6 = text2 != "13";
								if (flag6)
								{
									message = "It includes an invalid Sec-WebSocket-Version header.";
									result = false;
								}
								else
								{
									string text3 = headers["Sec-WebSocket-Protocol"];
									bool flag7 = text3 != null && text3.Length == 0;
									if (flag7)
									{
										message = "It includes an invalid Sec-WebSocket-Protocol header.";
										result = false;
									}
									else
									{
										bool flag8 = !this._ignoreExtensions;
										if (flag8)
										{
											string text4 = headers["Sec-WebSocket-Extensions"];
											bool flag9 = text4 != null && text4.Length == 0;
											if (flag9)
											{
												message = "It includes an invalid Sec-WebSocket-Extensions header.";
												return false;
											}
										}
										result = true;
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00005084 File Offset: 0x00003284
		private bool checkHandshakeResponse(HttpResponse response, out string message)
		{
			message = null;
			bool isRedirect = response.IsRedirect;
			bool result;
			if (isRedirect)
			{
				message = "Indicates the redirection.";
				result = false;
			}
			else
			{
				bool isUnauthorized = response.IsUnauthorized;
				if (isUnauthorized)
				{
					message = "Requires the authentication.";
					result = false;
				}
				else
				{
					bool flag = !response.IsWebSocketResponse;
					if (flag)
					{
						message = "Not a WebSocket handshake response.";
						result = false;
					}
					else
					{
						NameValueCollection headers = response.Headers;
						bool flag2 = !this.validateSecWebSocketAcceptHeader(headers["Sec-WebSocket-Accept"]);
						if (flag2)
						{
							message = "Includes no Sec-WebSocket-Accept header, or it has an invalid value.";
							result = false;
						}
						else
						{
							bool flag3 = !this.validateSecWebSocketProtocolServerHeader(headers["Sec-WebSocket-Protocol"]);
							if (flag3)
							{
								message = "Includes no Sec-WebSocket-Protocol header, or it has an invalid value.";
								result = false;
							}
							else
							{
								bool flag4 = !this.validateSecWebSocketExtensionsServerHeader(headers["Sec-WebSocket-Extensions"]);
								if (flag4)
								{
									message = "Includes an invalid Sec-WebSocket-Extensions header.";
									result = false;
								}
								else
								{
									bool flag5 = !this.validateSecWebSocketVersionServerHeader(headers["Sec-WebSocket-Version"]);
									if (flag5)
									{
										message = "Includes an invalid Sec-WebSocket-Version header.";
										result = false;
									}
									else
									{
										result = true;
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000518C File Offset: 0x0000338C
		private static bool checkProtocols(string[] protocols, out string message)
		{
			message = null;
			Func<string, bool> condition = (string protocol) => protocol.IsNullOrEmpty() || !protocol.IsToken();
			bool flag = protocols.Contains(condition);
			bool result;
			if (flag)
			{
				message = "It contains a value that is not a token.";
				result = false;
			}
			else
			{
				bool flag2 = protocols.ContainsTwice();
				if (flag2)
				{
					message = "It contains a value twice.";
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000051F0 File Offset: 0x000033F0
		private bool checkReceivedFrame(WebSocketFrame frame, out string message)
		{
			message = null;
			bool isMasked = frame.IsMasked;
			bool flag = this._client && isMasked;
			bool result;
			if (flag)
			{
				message = "A frame from the server is masked.";
				result = false;
			}
			else
			{
				bool flag2 = !this._client && !isMasked;
				if (flag2)
				{
					message = "A frame from a client is not masked.";
					result = false;
				}
				else
				{
					bool flag3 = this._inContinuation && frame.IsData;
					if (flag3)
					{
						message = "A data frame has been received while receiving continuation frames.";
						result = false;
					}
					else
					{
						bool flag4 = frame.IsCompressed && this._compression == CompressionMethod.None;
						if (flag4)
						{
							message = "A compressed frame has been received without any agreement for it.";
							result = false;
						}
						else
						{
							bool flag5 = frame.Rsv2 == Rsv.On;
							if (flag5)
							{
								message = "The RSV2 of a frame is non-zero without any negotiation for it.";
								result = false;
							}
							else
							{
								bool flag6 = frame.Rsv3 == Rsv.On;
								if (flag6)
								{
									message = "The RSV3 of a frame is non-zero without any negotiation for it.";
									result = false;
								}
								else
								{
									result = true;
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000052CC File Offset: 0x000034CC
		private void close(ushort code, string reason)
		{
			bool flag = this._readyState == WebSocketState.Closing;
			if (flag)
			{
				this._logger.Info("The closing is already in progress.");
			}
			else
			{
				bool flag2 = this._readyState == WebSocketState.Closed;
				if (flag2)
				{
					this._logger.Info("The connection has already been closed.");
				}
				else
				{
					bool flag3 = code == 1005;
					if (flag3)
					{
						this.close(PayloadData.Empty, true, true, false);
					}
					else
					{
						bool flag4 = !code.IsReserved();
						this.close(new PayloadData(code, reason), flag4, flag4, false);
					}
				}
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00005360 File Offset: 0x00003560
		private void close(PayloadData payloadData, bool send, bool receive, bool received)
		{
			object forState = this._forState;
			lock (forState)
			{
				bool flag = this._readyState == WebSocketState.Closing;
				if (flag)
				{
					this._logger.Info("The closing is already in progress.");
					return;
				}
				bool flag2 = this._readyState == WebSocketState.Closed;
				if (flag2)
				{
					this._logger.Info("The connection has already been closed.");
					return;
				}
				send = (send && this._readyState == WebSocketState.Open);
				receive = (send && receive);
				this._readyState = WebSocketState.Closing;
			}
			this._logger.Trace("Begin closing the connection.");
			bool clean = this.closeHandshake(payloadData, send, receive, received);
			this.releaseResources();
			this._logger.Trace("End closing the connection.");
			this._readyState = WebSocketState.Closed;
			CloseEventArgs e = new CloseEventArgs(payloadData, clean);
			try
			{
				this.OnClose.Emit(this, e);
			}
			catch (Exception ex)
			{
				this._logger.Error(ex.Message);
				this._logger.Debug(ex.ToString());
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000054A4 File Offset: 0x000036A4
		private void closeAsync(ushort code, string reason)
		{
			bool flag = this._readyState == WebSocketState.Closing;
			if (flag)
			{
				this._logger.Info("The closing is already in progress.");
			}
			else
			{
				bool flag2 = this._readyState == WebSocketState.Closed;
				if (flag2)
				{
					this._logger.Info("The connection has already been closed.");
				}
				else
				{
					bool flag3 = code == 1005;
					if (flag3)
					{
						this.closeAsync(PayloadData.Empty, true, true, false);
					}
					else
					{
						bool flag4 = !code.IsReserved();
						this.closeAsync(new PayloadData(code, reason), flag4, flag4, false);
					}
				}
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00005538 File Offset: 0x00003738
		private void closeAsync(PayloadData payloadData, bool send, bool receive, bool received)
		{
			Action<PayloadData, bool, bool, bool> closer = new Action<PayloadData, bool, bool, bool>(this.close);
			closer.BeginInvoke(payloadData, send, receive, received, delegate(IAsyncResult ar)
			{
				closer.EndInvoke(ar);
			}, null);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000557C File Offset: 0x0000377C
		private bool closeHandshake(byte[] frameAsBytes, bool receive, bool received)
		{
			bool flag = frameAsBytes != null && this.sendBytes(frameAsBytes);
			bool flag2 = !received && flag && receive && this._receivingExited != null;
			bool flag3 = flag2;
			if (flag3)
			{
				received = this._receivingExited.WaitOne(this._waitTime);
			}
			bool flag4 = flag && received;
			this._logger.Debug(string.Format("Was clean?: {0}\n  sent: {1}\n  received: {2}", flag4, flag, received));
			return flag4;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000055FC File Offset: 0x000037FC
		private bool closeHandshake(PayloadData payloadData, bool send, bool receive, bool received)
		{
			bool flag = false;
			if (send)
			{
				WebSocketFrame webSocketFrame = WebSocketFrame.CreateCloseFrame(payloadData, this._client);
				flag = this.sendBytes(webSocketFrame.ToArray());
				bool client = this._client;
				if (client)
				{
					webSocketFrame.Unmask();
				}
			}
			bool flag2 = !received && flag && receive && this._receivingExited != null;
			bool flag3 = flag2;
			if (flag3)
			{
				received = this._receivingExited.WaitOne(this._waitTime);
			}
			bool flag4 = flag && received;
			this._logger.Debug(string.Format("Was clean?: {0}\n  sent: {1}\n  received: {2}", flag4, flag, received));
			return flag4;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000056AC File Offset: 0x000038AC
		private bool connect()
		{
			bool flag = this._readyState == WebSocketState.Open;
			bool result;
			if (flag)
			{
				string message = "The connection has already been established.";
				this._logger.Warn(message);
				result = false;
			}
			else
			{
				object forState = this._forState;
				lock (forState)
				{
					bool flag2 = this._readyState == WebSocketState.Open;
					if (flag2)
					{
						string message2 = "The connection has already been established.";
						this._logger.Warn(message2);
						result = false;
					}
					else
					{
						bool flag3 = this._readyState == WebSocketState.Closing;
						if (flag3)
						{
							string message3 = "The close process has set in.";
							this._logger.Error(message3);
							message3 = "An interruption has occurred while attempting to connect.";
							this.error(message3, null);
							result = false;
						}
						else
						{
							bool flag4 = this._retryCountForConnect > WebSocket._maxRetryCountForConnect;
							if (flag4)
							{
								string message4 = "An opportunity for reconnecting has been lost.";
								this._logger.Error(message4);
								message4 = "An interruption has occurred while attempting to connect.";
								this.error(message4, null);
								result = false;
							}
							else
							{
								this._readyState = WebSocketState.Connecting;
								try
								{
									this.doHandshake();
								}
								catch (Exception ex)
								{
									this._retryCountForConnect++;
									this._logger.Fatal(ex.Message);
									this._logger.Debug(ex.ToString());
									string message5 = "An exception has occurred while attempting to connect.";
									this.fatal(message5, ex);
									return false;
								}
								this._retryCountForConnect = 1;
								this._readyState = WebSocketState.Open;
								result = true;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00005860 File Offset: 0x00003A60
		private string createExtensions()
		{
			StringBuilder stringBuilder = new StringBuilder(80);
			bool flag = this._compression > CompressionMethod.None;
			if (flag)
			{
				string arg = this._compression.ToExtensionString(new string[]
				{
					"server_no_context_takeover",
					"client_no_context_takeover"
				});
				stringBuilder.AppendFormat("{0}, ", arg);
			}
			int length = stringBuilder.Length;
			bool flag2 = length > 2;
			string result;
			if (flag2)
			{
				stringBuilder.Length = length - 2;
				result = stringBuilder.ToString();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000058E4 File Offset: 0x00003AE4
		private HttpResponse createHandshakeFailureResponse(HttpStatusCode code)
		{
			HttpResponse httpResponse = HttpResponse.CreateCloseResponse(code);
			httpResponse.Headers["Sec-WebSocket-Version"] = "13";
			return httpResponse;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00005914 File Offset: 0x00003B14
		private HttpRequest createHandshakeRequest()
		{
			HttpRequest httpRequest = HttpRequest.CreateWebSocketRequest(this._uri);
			NameValueCollection headers = httpRequest.Headers;
			bool flag = !this._origin.IsNullOrEmpty();
			if (flag)
			{
				headers["Origin"] = this._origin;
			}
			headers["Sec-WebSocket-Key"] = this._base64Key;
			this._protocolsRequested = (this._protocols != null);
			bool protocolsRequested = this._protocolsRequested;
			if (protocolsRequested)
			{
				headers["Sec-WebSocket-Protocol"] = this._protocols.ToString(", ");
			}
			this._extensionsRequested = (this._compression > CompressionMethod.None);
			bool extensionsRequested = this._extensionsRequested;
			if (extensionsRequested)
			{
				headers["Sec-WebSocket-Extensions"] = this.createExtensions();
			}
			headers["Sec-WebSocket-Version"] = "13";
			AuthenticationResponse authenticationResponse = null;
			bool flag2 = this._authChallenge != null && this._credentials != null;
			if (flag2)
			{
				authenticationResponse = new AuthenticationResponse(this._authChallenge, this._credentials, this._nonceCount);
				this._nonceCount = authenticationResponse.NonceCount;
			}
			else
			{
				bool preAuth = this._preAuth;
				if (preAuth)
				{
					authenticationResponse = new AuthenticationResponse(this._credentials);
				}
			}
			bool flag3 = authenticationResponse != null;
			if (flag3)
			{
				headers["Authorization"] = authenticationResponse.ToString();
			}
			bool flag4 = this._cookies.Count > 0;
			if (flag4)
			{
				httpRequest.SetCookies(this._cookies);
			}
			return httpRequest;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00005A84 File Offset: 0x00003C84
		private HttpResponse createHandshakeResponse()
		{
			HttpResponse httpResponse = HttpResponse.CreateWebSocketResponse();
			NameValueCollection headers = httpResponse.Headers;
			headers["Sec-WebSocket-Accept"] = WebSocket.CreateResponseKey(this._base64Key);
			bool flag = this._protocol != null;
			if (flag)
			{
				headers["Sec-WebSocket-Protocol"] = this._protocol;
			}
			bool flag2 = this._extensions != null;
			if (flag2)
			{
				headers["Sec-WebSocket-Extensions"] = this._extensions;
			}
			bool flag3 = this._cookies.Count > 0;
			if (flag3)
			{
				httpResponse.SetCookies(this._cookies);
			}
			return httpResponse;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00005B1C File Offset: 0x00003D1C
		private bool customCheckHandshakeRequest(WebSocketContext context, out string message)
		{
			message = null;
			bool flag = this._handshakeRequestChecker == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				message = this._handshakeRequestChecker(context);
				result = (message == null);
			}
			return result;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00005B58 File Offset: 0x00003D58
		private MessageEventArgs dequeueFromMessageEventQueue()
		{
			object forMessageEventQueue = this._forMessageEventQueue;
			MessageEventArgs result;
			lock (forMessageEventQueue)
			{
				result = ((this._messageEventQueue.Count > 0) ? this._messageEventQueue.Dequeue() : null);
			}
			return result;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005BAC File Offset: 0x00003DAC
		private void doHandshake()
		{
			this.setClientStream();
			HttpResponse httpResponse = this.sendHandshakeRequest();
			string message;
			bool flag = !this.checkHandshakeResponse(httpResponse, out message);
			if (flag)
			{
				throw new WebSocketException(CloseStatusCode.ProtocolError, message);
			}
			bool protocolsRequested = this._protocolsRequested;
			if (protocolsRequested)
			{
				this._protocol = httpResponse.Headers["Sec-WebSocket-Protocol"];
			}
			bool extensionsRequested = this._extensionsRequested;
			if (extensionsRequested)
			{
				this.processSecWebSocketExtensionsServerHeader(httpResponse.Headers["Sec-WebSocket-Extensions"]);
			}
			this.processCookies(httpResponse.Cookies);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005C34 File Offset: 0x00003E34
		private void enqueueToMessageEventQueue(MessageEventArgs e)
		{
			object forMessageEventQueue = this._forMessageEventQueue;
			lock (forMessageEventQueue)
			{
				this._messageEventQueue.Enqueue(e);
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00005C78 File Offset: 0x00003E78
		private void error(string message, Exception exception)
		{
			try
			{
				this.OnError.Emit(this, new ErrorEventArgs(message, exception));
			}
			catch (Exception ex)
			{
				this._logger.Error(ex.Message);
				this._logger.Debug(ex.ToString());
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00005CDC File Offset: 0x00003EDC
		private void fatal(string message, Exception exception)
		{
			CloseStatusCode code = (exception is WebSocketException) ? ((WebSocketException)exception).Code : CloseStatusCode.Abnormal;
			this.fatal(message, (ushort)code);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00005D10 File Offset: 0x00003F10
		private void fatal(string message, ushort code)
		{
			PayloadData payloadData = new PayloadData(code, message);
			this.close(payloadData, !code.IsReserved(), false, false);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005D39 File Offset: 0x00003F39
		private void fatal(string message, CloseStatusCode code)
		{
			this.fatal(message, (ushort)code);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00005D48 File Offset: 0x00003F48
		private ClientSslConfiguration getSslConfiguration()
		{
			bool flag = this._sslConfig == null;
			if (flag)
			{
				this._sslConfig = new ClientSslConfiguration(this._uri.DnsSafeHost);
			}
			return this._sslConfig;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00005D84 File Offset: 0x00003F84
		private void init()
		{
			this._compression = CompressionMethod.None;
			this._cookies = new CookieCollection();
			this._forPing = new object();
			this._forSend = new object();
			this._forState = new object();
			this._messageEventQueue = new Queue<MessageEventArgs>();
			this._forMessageEventQueue = ((ICollection)this._messageEventQueue).SyncRoot;
			this._readyState = WebSocketState.Connecting;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00005DEC File Offset: 0x00003FEC
		private void message()
		{
			MessageEventArgs obj = null;
			object forMessageEventQueue = this._forMessageEventQueue;
			lock (forMessageEventQueue)
			{
				bool flag = this._inMessage || this._messageEventQueue.Count == 0 || this._readyState != WebSocketState.Open;
				if (flag)
				{
					return;
				}
				this._inMessage = true;
				obj = this._messageEventQueue.Dequeue();
			}
			this._message(obj);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005E78 File Offset: 0x00004078
		private void messagec(MessageEventArgs e)
		{
			for (;;)
			{
				try
				{
					this.OnMessage.Emit(this, e);
				}
				catch (Exception ex)
				{
					this._logger.Error(ex.ToString());
					this.error("An error has occurred during an OnMessage event.", ex);
				}
				object forMessageEventQueue = this._forMessageEventQueue;
				lock (forMessageEventQueue)
				{
					bool flag = this._messageEventQueue.Count == 0 || this._readyState != WebSocketState.Open;
					if (flag)
					{
						this._inMessage = false;
						break;
					}
					e = this._messageEventQueue.Dequeue();
				}
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005F38 File Offset: 0x00004138
		private void messages(MessageEventArgs e)
		{
			try
			{
				this.OnMessage.Emit(this, e);
			}
			catch (Exception ex)
			{
				this._logger.Error(ex.ToString());
				this.error("An error has occurred during an OnMessage event.", ex);
			}
			object forMessageEventQueue = this._forMessageEventQueue;
			lock (forMessageEventQueue)
			{
				bool flag = this._messageEventQueue.Count == 0 || this._readyState != WebSocketState.Open;
				if (flag)
				{
					this._inMessage = false;
					return;
				}
				e = this._messageEventQueue.Dequeue();
			}
			ThreadPool.QueueUserWorkItem(delegate(object state)
			{
				this.messages(e);
			});
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00006020 File Offset: 0x00004220
		private void open()
		{
			this._inMessage = true;
			this.startReceiving();
			try
			{
				this.OnOpen.Emit(this, EventArgs.Empty);
			}
			catch (Exception ex)
			{
				this._logger.Error(ex.ToString());
				this.error("An error has occurred during the OnOpen event.", ex);
			}
			MessageEventArgs obj = null;
			object forMessageEventQueue = this._forMessageEventQueue;
			lock (forMessageEventQueue)
			{
				bool flag = this._messageEventQueue.Count == 0 || this._readyState != WebSocketState.Open;
				if (flag)
				{
					this._inMessage = false;
					return;
				}
				obj = this._messageEventQueue.Dequeue();
			}
			this._message.BeginInvoke(obj, delegate(IAsyncResult ar)
			{
				this._message.EndInvoke(ar);
			}, null);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00006108 File Offset: 0x00004308
		private bool ping(byte[] data)
		{
			bool flag = this._readyState != WebSocketState.Open;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				ManualResetEvent pongReceived = this._pongReceived;
				bool flag2 = pongReceived == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					object forPing = this._forPing;
					lock (forPing)
					{
						try
						{
							pongReceived.Reset();
							bool flag3 = !this.send(Fin.Final, Opcode.Ping, data, false);
							if (flag3)
							{
								result = false;
							}
							else
							{
								result = pongReceived.WaitOne(this._waitTime);
							}
						}
						catch (ObjectDisposedException)
						{
							result = false;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000061AC File Offset: 0x000043AC
		private bool processCloseFrame(WebSocketFrame frame)
		{
			PayloadData payloadData = frame.PayloadData;
			this.close(payloadData, !payloadData.HasReservedCode, false, true);
			return false;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000061DC File Offset: 0x000043DC
		private void processCookies(CookieCollection cookies)
		{
			bool flag = cookies.Count == 0;
			if (!flag)
			{
				this._cookies.SetOrRemove(cookies);
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00006208 File Offset: 0x00004408
		private bool processDataFrame(WebSocketFrame frame)
		{
			this.enqueueToMessageEventQueue(frame.IsCompressed ? new MessageEventArgs(frame.Opcode, frame.PayloadData.ApplicationData.Decompress(this._compression)) : new MessageEventArgs(frame));
			return true;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00006254 File Offset: 0x00004454
		private bool processFragmentFrame(WebSocketFrame frame)
		{
			bool flag = !this._inContinuation;
			if (flag)
			{
				bool isContinuation = frame.IsContinuation;
				if (isContinuation)
				{
					return true;
				}
				this._fragmentsOpcode = frame.Opcode;
				this._fragmentsCompressed = frame.IsCompressed;
				this._fragmentsBuffer = new MemoryStream();
				this._inContinuation = true;
			}
			this._fragmentsBuffer.WriteBytes(frame.PayloadData.ApplicationData, 1024);
			bool isFinal = frame.IsFinal;
			if (isFinal)
			{
				using (this._fragmentsBuffer)
				{
					byte[] rawData = this._fragmentsCompressed ? this._fragmentsBuffer.DecompressToArray(this._compression) : this._fragmentsBuffer.ToArray();
					this.enqueueToMessageEventQueue(new MessageEventArgs(this._fragmentsOpcode, rawData));
				}
				this._fragmentsBuffer = null;
				this._inContinuation = false;
			}
			return true;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000634C File Offset: 0x0000454C
		private bool processPingFrame(WebSocketFrame frame)
		{
			this._logger.Trace("A ping was received.");
			WebSocketFrame webSocketFrame = WebSocketFrame.CreatePongFrame(frame.PayloadData, this._client);
			object forState = this._forState;
			lock (forState)
			{
				bool flag = this._readyState != WebSocketState.Open;
				if (flag)
				{
					this._logger.Error("The connection is closing.");
					return true;
				}
				bool flag2 = !this.sendBytes(webSocketFrame.ToArray());
				if (flag2)
				{
					return false;
				}
			}
			this._logger.Trace("A pong to this ping has been sent.");
			bool emitOnPing = this._emitOnPing;
			if (emitOnPing)
			{
				bool client = this._client;
				if (client)
				{
					webSocketFrame.Unmask();
				}
				this.enqueueToMessageEventQueue(new MessageEventArgs(frame));
			}
			return true;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00006434 File Offset: 0x00004634
		private bool processPongFrame(WebSocketFrame frame)
		{
			this._logger.Trace("A pong was received.");
			try
			{
				this._pongReceived.Set();
			}
			catch (NullReferenceException ex)
			{
				this._logger.Error(ex.Message);
				this._logger.Debug(ex.ToString());
				return false;
			}
			catch (ObjectDisposedException ex2)
			{
				this._logger.Error(ex2.Message);
				this._logger.Debug(ex2.ToString());
				return false;
			}
			this._logger.Trace("It has been signaled.");
			return true;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000064F8 File Offset: 0x000046F8
		private bool processReceivedFrame(WebSocketFrame frame)
		{
			string message;
			bool flag = !this.checkReceivedFrame(frame, out message);
			if (flag)
			{
				throw new WebSocketException(CloseStatusCode.ProtocolError, message);
			}
			frame.Unmask();
			return frame.IsFragment ? this.processFragmentFrame(frame) : (frame.IsData ? this.processDataFrame(frame) : (frame.IsPing ? this.processPingFrame(frame) : (frame.IsPong ? this.processPongFrame(frame) : (frame.IsClose ? this.processCloseFrame(frame) : this.processUnsupportedFrame(frame)))));
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000658C File Offset: 0x0000478C
		private void processSecWebSocketExtensionsClientHeader(string value)
		{
			bool flag = value == null;
			if (!flag)
			{
				StringBuilder stringBuilder = new StringBuilder(80);
				bool flag2 = false;
				foreach (string text in value.SplitHeaderValue(new char[]
				{
					','
				}))
				{
					string text2 = text.Trim();
					bool flag3 = text2.Length == 0;
					if (!flag3)
					{
						bool flag4 = !flag2;
						if (flag4)
						{
							bool flag5 = text2.IsCompressionExtension(CompressionMethod.Deflate);
							if (flag5)
							{
								this._compression = CompressionMethod.Deflate;
								stringBuilder.AppendFormat("{0}, ", this._compression.ToExtensionString(new string[]
								{
									"client_no_context_takeover",
									"server_no_context_takeover"
								}));
								flag2 = true;
							}
						}
					}
				}
				int length = stringBuilder.Length;
				bool flag6 = length <= 2;
				if (!flag6)
				{
					stringBuilder.Length = length - 2;
					this._extensions = stringBuilder.ToString();
				}
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000669C File Offset: 0x0000489C
		private void processSecWebSocketExtensionsServerHeader(string value)
		{
			bool flag = value == null;
			if (flag)
			{
				this._compression = CompressionMethod.None;
			}
			else
			{
				this._extensions = value;
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000066C4 File Offset: 0x000048C4
		private void processSecWebSocketProtocolClientHeader(IEnumerable<string> values)
		{
			bool flag = values.Contains((string val) => val == this._protocol);
			if (!flag)
			{
				this._protocol = null;
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000066F4 File Offset: 0x000048F4
		private bool processUnsupportedFrame(WebSocketFrame frame)
		{
			this._logger.Fatal("An unsupported frame:" + frame.PrintToString(false));
			this.fatal("There is no way to handle it.", CloseStatusCode.PolicyViolation);
			return false;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00006738 File Offset: 0x00004938
		private void refuseHandshake(CloseStatusCode code, string reason)
		{
			this._readyState = WebSocketState.Closing;
			HttpResponse response = this.createHandshakeFailureResponse(HttpStatusCode.BadRequest);
			this.sendHttpResponse(response);
			this.releaseServerResources();
			this._readyState = WebSocketState.Closed;
			CloseEventArgs e = new CloseEventArgs((ushort)code, reason, false);
			try
			{
				this.OnClose.Emit(this, e);
			}
			catch (Exception ex)
			{
				this._logger.Error(ex.Message);
				this._logger.Debug(ex.ToString());
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000067CC File Offset: 0x000049CC
		private void releaseClientResources()
		{
			bool flag = this._stream != null;
			if (flag)
			{
				this._stream.Dispose();
				this._stream = null;
			}
			bool flag2 = this._tcpClient != null;
			if (flag2)
			{
				this._tcpClient.Close();
				this._tcpClient = null;
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00006820 File Offset: 0x00004A20
		private void releaseCommonResources()
		{
			bool flag = this._fragmentsBuffer != null;
			if (flag)
			{
				this._fragmentsBuffer.Dispose();
				this._fragmentsBuffer = null;
				this._inContinuation = false;
			}
			bool flag2 = this._pongReceived != null;
			if (flag2)
			{
				this._pongReceived.Close();
				this._pongReceived = null;
			}
			bool flag3 = this._receivingExited != null;
			if (flag3)
			{
				this._receivingExited.Close();
				this._receivingExited = null;
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000689C File Offset: 0x00004A9C
		private void releaseResources()
		{
			bool client = this._client;
			if (client)
			{
				this.releaseClientResources();
			}
			else
			{
				this.releaseServerResources();
			}
			this.releaseCommonResources();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000068CC File Offset: 0x00004ACC
		private void releaseServerResources()
		{
			bool flag = this._closeContext == null;
			if (!flag)
			{
				this._closeContext();
				this._closeContext = null;
				this._stream = null;
				this._context = null;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000690C File Offset: 0x00004B0C
		private bool send(Opcode opcode, Stream stream)
		{
			object forSend = this._forSend;
			bool result;
			lock (forSend)
			{
				Stream stream2 = stream;
				bool flag = false;
				bool flag2 = false;
				try
				{
					bool flag3 = this._compression > CompressionMethod.None;
					if (flag3)
					{
						stream = stream.Compress(this._compression);
						flag = true;
					}
					flag2 = this.send(opcode, stream, flag);
					bool flag4 = !flag2;
					if (flag4)
					{
						this.error("A send has been interrupted.", null);
					}
				}
				catch (Exception ex)
				{
					this._logger.Error(ex.ToString());
					this.error("An error has occurred during a send.", ex);
				}
				finally
				{
					bool flag5 = flag;
					if (flag5)
					{
						stream.Dispose();
					}
					stream2.Dispose();
				}
				result = flag2;
			}
			return result;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000069F0 File Offset: 0x00004BF0
		private bool send(Opcode opcode, Stream stream, bool compressed)
		{
			long length = stream.Length;
			bool flag = length == 0L;
			bool result;
			if (flag)
			{
				result = this.send(Fin.Final, opcode, WebSocket.EmptyBytes, false);
			}
			else
			{
				long num = length / (long)WebSocket.FragmentLength;
				int num2 = (int)(length % (long)WebSocket.FragmentLength);
				bool flag2 = num == 0L;
				if (flag2)
				{
					byte[] array = new byte[num2];
					result = (stream.Read(array, 0, num2) == num2 && this.send(Fin.Final, opcode, array, compressed));
				}
				else
				{
					bool flag3 = num == 1L && num2 == 0;
					if (flag3)
					{
						byte[] array = new byte[WebSocket.FragmentLength];
						result = (stream.Read(array, 0, WebSocket.FragmentLength) == WebSocket.FragmentLength && this.send(Fin.Final, opcode, array, compressed));
					}
					else
					{
						byte[] array = new byte[WebSocket.FragmentLength];
						bool flag4 = stream.Read(array, 0, WebSocket.FragmentLength) == WebSocket.FragmentLength && this.send(Fin.More, opcode, array, compressed);
						bool flag5 = !flag4;
						if (flag5)
						{
							result = false;
						}
						else
						{
							long num3 = (num2 == 0) ? (num - 2L) : (num - 1L);
							for (long num4 = 0L; num4 < num3; num4 += 1L)
							{
								flag4 = (stream.Read(array, 0, WebSocket.FragmentLength) == WebSocket.FragmentLength && this.send(Fin.More, Opcode.Cont, array, false));
								bool flag6 = !flag4;
								if (flag6)
								{
									return false;
								}
							}
							bool flag7 = num2 == 0;
							if (flag7)
							{
								num2 = WebSocket.FragmentLength;
							}
							else
							{
								array = new byte[num2];
							}
							result = (stream.Read(array, 0, num2) == num2 && this.send(Fin.Final, Opcode.Cont, array, false));
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00006B8C File Offset: 0x00004D8C
		private bool send(Fin fin, Opcode opcode, byte[] data, bool compressed)
		{
			object forState = this._forState;
			bool result;
			lock (forState)
			{
				bool flag = this._readyState != WebSocketState.Open;
				if (flag)
				{
					this._logger.Error("The connection is closing.");
					result = false;
				}
				else
				{
					WebSocketFrame webSocketFrame = new WebSocketFrame(fin, opcode, data, compressed, this._client);
					result = this.sendBytes(webSocketFrame.ToArray());
				}
			}
			return result;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00006C0C File Offset: 0x00004E0C
		private void sendAsync(Opcode opcode, Stream stream, Action<bool> completed)
		{
			Func<Opcode, Stream, bool> sender = new Func<Opcode, Stream, bool>(this.send);
			sender.BeginInvoke(opcode, stream, delegate(IAsyncResult ar)
			{
				try
				{
					bool obj = sender.EndInvoke(ar);
					bool flag = completed != null;
					if (flag)
					{
						completed(obj);
					}
				}
				catch (Exception ex)
				{
					this._logger.Error(ex.ToString());
					this.error("An error has occurred during the callback for an async send.", ex);
				}
			}, null);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00006C5C File Offset: 0x00004E5C
		private bool sendBytes(byte[] bytes)
		{
			try
			{
				this._stream.Write(bytes, 0, bytes.Length);
			}
			catch (Exception ex)
			{
				this._logger.Error(ex.Message);
				this._logger.Debug(ex.ToString());
				return false;
			}
			return true;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00006CC4 File Offset: 0x00004EC4
		private HttpResponse sendHandshakeRequest()
		{
			HttpRequest httpRequest = this.createHandshakeRequest();
			HttpResponse httpResponse = this.sendHttpRequest(httpRequest, 90000);
			bool isUnauthorized = httpResponse.IsUnauthorized;
			if (isUnauthorized)
			{
				string text = httpResponse.Headers["WWW-Authenticate"];
				this._logger.Warn(string.Format("Received an authentication requirement for '{0}'.", text));
				bool flag = text.IsNullOrEmpty();
				if (flag)
				{
					this._logger.Error("No authentication challenge is specified.");
					return httpResponse;
				}
				this._authChallenge = AuthenticationChallenge.Parse(text);
				bool flag2 = this._authChallenge == null;
				if (flag2)
				{
					this._logger.Error("An invalid authentication challenge is specified.");
					return httpResponse;
				}
				bool flag3 = this._credentials != null && (!this._preAuth || this._authChallenge.Scheme == AuthenticationSchemes.Digest);
				if (flag3)
				{
					bool hasConnectionClose = httpResponse.HasConnectionClose;
					if (hasConnectionClose)
					{
						this.releaseClientResources();
						this.setClientStream();
					}
					AuthenticationResponse authenticationResponse = new AuthenticationResponse(this._authChallenge, this._credentials, this._nonceCount);
					this._nonceCount = authenticationResponse.NonceCount;
					httpRequest.Headers["Authorization"] = authenticationResponse.ToString();
					httpResponse = this.sendHttpRequest(httpRequest, 15000);
				}
			}
			bool isRedirect = httpResponse.IsRedirect;
			if (isRedirect)
			{
				string text2 = httpResponse.Headers["Location"];
				this._logger.Warn(string.Format("Received a redirection to '{0}'.", text2));
				bool enableRedirection = this._enableRedirection;
				if (enableRedirection)
				{
					bool flag4 = text2.IsNullOrEmpty();
					if (flag4)
					{
						this._logger.Error("No url to redirect is located.");
						return httpResponse;
					}
					Uri uri;
					string str;
					bool flag5 = !text2.TryCreateWebSocketUri(out uri, out str);
					if (flag5)
					{
						this._logger.Error("An invalid url to redirect is located: " + str);
						return httpResponse;
					}
					this.releaseClientResources();
					this._uri = uri;
					this._secure = (uri.Scheme == "wss");
					this.setClientStream();
					return this.sendHandshakeRequest();
				}
			}
			return httpResponse;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00006EF8 File Offset: 0x000050F8
		private HttpResponse sendHttpRequest(HttpRequest request, int millisecondsTimeout)
		{
			this._logger.Debug("A request to the server:\n" + request.ToString());
			HttpResponse response = request.GetResponse(this._stream, millisecondsTimeout);
			this._logger.Debug("A response to this request:\n" + response.ToString());
			return response;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00006F58 File Offset: 0x00005158
		private bool sendHttpResponse(HttpResponse response)
		{
			this._logger.Debug(string.Format("A response to {0}:\n{1}", this._context.UserEndPoint, response));
			return this.sendBytes(response.ToByteArray());
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00006F9C File Offset: 0x0000519C
		private void sendProxyConnectRequest()
		{
			HttpRequest httpRequest = HttpRequest.CreateConnectRequest(this._uri);
			HttpResponse httpResponse = this.sendHttpRequest(httpRequest, 90000);
			bool isProxyAuthenticationRequired = httpResponse.IsProxyAuthenticationRequired;
			if (isProxyAuthenticationRequired)
			{
				string text = httpResponse.Headers["Proxy-Authenticate"];
				this._logger.Warn(string.Format("Received a proxy authentication requirement for '{0}'.", text));
				bool flag = text.IsNullOrEmpty();
				if (flag)
				{
					throw new WebSocketException("No proxy authentication challenge is specified.");
				}
				AuthenticationChallenge authenticationChallenge = AuthenticationChallenge.Parse(text);
				bool flag2 = authenticationChallenge == null;
				if (flag2)
				{
					throw new WebSocketException("An invalid proxy authentication challenge is specified.");
				}
				bool flag3 = this._proxyCredentials != null;
				if (flag3)
				{
					bool hasConnectionClose = httpResponse.HasConnectionClose;
					if (hasConnectionClose)
					{
						this.releaseClientResources();
						this._tcpClient = new TcpClient(this._proxyUri.DnsSafeHost, this._proxyUri.Port);
						this._stream = this._tcpClient.GetStream();
					}
					AuthenticationResponse authenticationResponse = new AuthenticationResponse(authenticationChallenge, this._proxyCredentials, 0U);
					httpRequest.Headers["Proxy-Authorization"] = authenticationResponse.ToString();
					httpResponse = this.sendHttpRequest(httpRequest, 15000);
				}
				bool isProxyAuthenticationRequired2 = httpResponse.IsProxyAuthenticationRequired;
				if (isProxyAuthenticationRequired2)
				{
					throw new WebSocketException("A proxy authentication is required.");
				}
			}
			bool flag4 = httpResponse.StatusCode[0] != '2';
			if (flag4)
			{
				throw new WebSocketException("The proxy has failed a connection to the requested host and port.");
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000070FC File Offset: 0x000052FC
		private void setClientStream()
		{
			bool flag = this._proxyUri != null;
			if (flag)
			{
				this._tcpClient = new TcpClient(this._proxyUri.DnsSafeHost, this._proxyUri.Port);
				this._stream = this._tcpClient.GetStream();
				this.sendProxyConnectRequest();
			}
			else
			{
				this._tcpClient = new TcpClient(this._uri.DnsSafeHost, this._uri.Port);
				this._stream = this._tcpClient.GetStream();
			}
			bool secure = this._secure;
			if (secure)
			{
				ClientSslConfiguration sslConfiguration = this.getSslConfiguration();
				string targetHost = sslConfiguration.TargetHost;
				bool flag2 = targetHost != this._uri.DnsSafeHost;
				if (flag2)
				{
					throw new WebSocketException(CloseStatusCode.TlsHandshakeFailure, "An invalid host name is specified.");
				}
				try
				{
					SslStream sslStream = new SslStream(this._stream, false, sslConfiguration.ServerCertificateValidationCallback, sslConfiguration.ClientCertificateSelectionCallback);
					sslStream.AuthenticateAsClient(targetHost, sslConfiguration.ClientCertificates, sslConfiguration.EnabledSslProtocols, sslConfiguration.CheckCertificateRevocation);
					this._stream = sslStream;
				}
				catch (Exception innerException)
				{
					throw new WebSocketException(CloseStatusCode.TlsHandshakeFailure, innerException);
				}
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00007234 File Offset: 0x00005434
		private void startReceiving()
		{
			bool flag = this._messageEventQueue.Count > 0;
			if (flag)
			{
				this._messageEventQueue.Clear();
			}
			this._pongReceived = new ManualResetEvent(false);
			this._receivingExited = new ManualResetEvent(false);
			Action receive = null;
			Action<WebSocketFrame> <>9__1;
			Action<Exception> <>9__2;
			receive = delegate()
			{
				Stream stream = this._stream;
				bool unmask = false;
				Action<WebSocketFrame> completed;
				if ((completed = <>9__1) == null)
				{
					completed = (<>9__1 = delegate(WebSocketFrame frame)
					{
						bool flag2 = !this.processReceivedFrame(frame) || this._readyState == WebSocketState.Closed;
						if (flag2)
						{
							ManualResetEvent receivingExited = this._receivingExited;
							bool flag3 = receivingExited != null;
							if (flag3)
							{
								receivingExited.Set();
							}
						}
						else
						{
							receive();
							bool flag4 = this._inMessage || !this.HasMessage || this._readyState != WebSocketState.Open;
							if (!flag4)
							{
								this.message();
							}
						}
					});
				}
				Action<Exception> error;
				if ((error = <>9__2) == null)
				{
					error = (<>9__2 = delegate(Exception ex)
					{
						this._logger.Fatal(ex.ToString());
						this.fatal("An exception has occurred while receiving.", ex);
					});
				}
				WebSocketFrame.ReadFrameAsync(stream, unmask, completed, error);
			};
			receive();
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000072AC File Offset: 0x000054AC
		private bool validateSecWebSocketAcceptHeader(string value)
		{
			return value != null && value == WebSocket.CreateResponseKey(this._base64Key);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000072D8 File Offset: 0x000054D8
		private bool validateSecWebSocketExtensionsServerHeader(string value)
		{
			bool flag = value == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = value.Length == 0;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = !this._extensionsRequested;
					if (flag3)
					{
						result = false;
					}
					else
					{
						bool flag4 = this._compression > CompressionMethod.None;
						foreach (string text in value.SplitHeaderValue(new char[]
						{
							','
						}))
						{
							string text2 = text.Trim();
							bool flag5 = flag4 && text2.IsCompressionExtension(this._compression);
							if (!flag5)
							{
								return false;
							}
							bool flag6 = !text2.Contains("server_no_context_takeover");
							if (flag6)
							{
								this._logger.Error("The server hasn't sent back 'server_no_context_takeover'.");
								return false;
							}
							bool flag7 = !text2.Contains("client_no_context_takeover");
							if (flag7)
							{
								this._logger.Warn("The server hasn't sent back 'client_no_context_takeover'.");
							}
							string method = this._compression.ToExtensionString(new string[0]);
							bool flag8 = text2.SplitHeaderValue(new char[]
							{
								';'
							}).Contains(delegate(string t)
							{
								t = t.Trim();
								return t != method && t != "server_no_context_takeover" && t != "client_no_context_takeover";
							});
							bool flag9 = flag8;
							if (flag9)
							{
								return false;
							}
						}
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000745C File Offset: 0x0000565C
		private bool validateSecWebSocketProtocolServerHeader(string value)
		{
			bool flag = value == null;
			bool result;
			if (flag)
			{
				result = !this._protocolsRequested;
			}
			else
			{
				bool flag2 = value.Length == 0;
				result = (!flag2 && this._protocolsRequested && this._protocols.Contains((string p) => p == value));
			}
			return result;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000074CC File Offset: 0x000056CC
		private bool validateSecWebSocketVersionServerHeader(string value)
		{
			return value == null || value == "13";
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000074EF File Offset: 0x000056EF
		internal void Close(HttpResponse response)
		{
			this._readyState = WebSocketState.Closing;
			this.sendHttpResponse(response);
			this.releaseServerResources();
			this._readyState = WebSocketState.Closed;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00007513 File Offset: 0x00005713
		internal void Close(HttpStatusCode code)
		{
			this.Close(this.createHandshakeFailureResponse(code));
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00007524 File Offset: 0x00005724
		internal void Close(PayloadData payloadData, byte[] frameAsBytes)
		{
			object forState = this._forState;
			lock (forState)
			{
				bool flag = this._readyState == WebSocketState.Closing;
				if (flag)
				{
					this._logger.Info("The closing is already in progress.");
					return;
				}
				bool flag2 = this._readyState == WebSocketState.Closed;
				if (flag2)
				{
					this._logger.Info("The connection has already been closed.");
					return;
				}
				this._readyState = WebSocketState.Closing;
			}
			this._logger.Trace("Begin closing the connection.");
			bool flag3 = frameAsBytes != null && this.sendBytes(frameAsBytes);
			bool flag4 = flag3 && this._receivingExited != null && this._receivingExited.WaitOne(this._waitTime);
			bool flag5 = flag3 && flag4;
			this._logger.Debug(string.Format("Was clean?: {0}\n  sent: {1}\n  received: {2}", flag5, flag3, flag4));
			this.releaseServerResources();
			this.releaseCommonResources();
			this._logger.Trace("End closing the connection.");
			this._readyState = WebSocketState.Closed;
			CloseEventArgs e = new CloseEventArgs(payloadData, flag5);
			try
			{
				this.OnClose.Emit(this, e);
			}
			catch (Exception ex)
			{
				this._logger.Error(ex.Message);
				this._logger.Debug(ex.ToString());
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000076AC File Offset: 0x000058AC
		internal static string CreateBase64Key()
		{
			byte[] array = new byte[16];
			WebSocket.RandomNumber.GetBytes(array);
			return Convert.ToBase64String(array);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000076D8 File Offset: 0x000058D8
		internal static string CreateResponseKey(string base64Key)
		{
			StringBuilder stringBuilder = new StringBuilder(base64Key, 64);
			stringBuilder.Append("258EAFA5-E914-47DA-95CA-C5AB0DC85B11");
			SHA1 sha = new SHA1CryptoServiceProvider();
			byte[] inArray = sha.ComputeHash(stringBuilder.ToString().GetUTF8EncodedBytes());
			return Convert.ToBase64String(inArray);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00007720 File Offset: 0x00005920
		internal void InternalAccept()
		{
			try
			{
				bool flag = !this.acceptHandshake();
				if (flag)
				{
					return;
				}
			}
			catch (Exception ex)
			{
				this._logger.Fatal(ex.Message);
				this._logger.Debug(ex.ToString());
				string message = "An exception has occurred while attempting to accept.";
				this.fatal(message, ex);
				return;
			}
			this._readyState = WebSocketState.Open;
			this.open();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000779C File Offset: 0x0000599C
		internal bool Ping(byte[] frameAsBytes, TimeSpan timeout)
		{
			bool flag = this._readyState != WebSocketState.Open;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				ManualResetEvent pongReceived = this._pongReceived;
				bool flag2 = pongReceived == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					object forPing = this._forPing;
					lock (forPing)
					{
						try
						{
							pongReceived.Reset();
							object forState = this._forState;
							lock (forState)
							{
								bool flag3 = this._readyState != WebSocketState.Open;
								if (flag3)
								{
									return false;
								}
								bool flag4 = !this.sendBytes(frameAsBytes);
								if (flag4)
								{
									return false;
								}
							}
							result = pongReceived.WaitOne(timeout);
						}
						catch (ObjectDisposedException)
						{
							result = false;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000787C File Offset: 0x00005A7C
		internal void Send(Opcode opcode, byte[] data, Dictionary<CompressionMethod, byte[]> cache)
		{
			object forSend = this._forSend;
			lock (forSend)
			{
				object forState = this._forState;
				lock (forState)
				{
					bool flag = this._readyState != WebSocketState.Open;
					if (flag)
					{
						this._logger.Error("The connection is closing.");
					}
					else
					{
						byte[] array;
						bool flag2 = !cache.TryGetValue(this._compression, out array);
						if (flag2)
						{
							array = new WebSocketFrame(Fin.Final, opcode, data.Compress(this._compression), this._compression > CompressionMethod.None, false).ToArray();
							cache.Add(this._compression, array);
						}
						this.sendBytes(array);
					}
				}
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00007954 File Offset: 0x00005B54
		internal void Send(Opcode opcode, Stream stream, Dictionary<CompressionMethod, Stream> cache)
		{
			object forSend = this._forSend;
			lock (forSend)
			{
				Stream stream2;
				bool flag = !cache.TryGetValue(this._compression, out stream2);
				if (flag)
				{
					stream2 = stream.Compress(this._compression);
					cache.Add(this._compression, stream2);
				}
				else
				{
					stream2.Position = 0L;
				}
				this.send(opcode, stream2, this._compression > CompressionMethod.None);
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000079E0 File Offset: 0x00005BE0
		public void Accept()
		{
			bool client = this._client;
			if (client)
			{
				string message = "This instance is a client.";
				throw new InvalidOperationException(message);
			}
			bool flag = this._readyState == WebSocketState.Closing;
			if (flag)
			{
				string message2 = "The close process is in progress.";
				throw new InvalidOperationException(message2);
			}
			bool flag2 = this._readyState == WebSocketState.Closed;
			if (flag2)
			{
				string message3 = "The connection has already been closed.";
				throw new InvalidOperationException(message3);
			}
			bool flag3 = this.accept();
			if (flag3)
			{
				this.open();
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00007A58 File Offset: 0x00005C58
		public void AcceptAsync()
		{
			bool client = this._client;
			if (client)
			{
				string message = "This instance is a client.";
				throw new InvalidOperationException(message);
			}
			bool flag = this._readyState == WebSocketState.Closing;
			if (flag)
			{
				string message2 = "The close process is in progress.";
				throw new InvalidOperationException(message2);
			}
			bool flag2 = this._readyState == WebSocketState.Closed;
			if (flag2)
			{
				string message3 = "The connection has already been closed.";
				throw new InvalidOperationException(message3);
			}
			Func<bool> acceptor = new Func<bool>(this.accept);
			acceptor.BeginInvoke(delegate(IAsyncResult ar)
			{
				bool flag3 = acceptor.EndInvoke(ar);
				if (flag3)
				{
					this.open();
				}
			}, null);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00007AF6 File Offset: 0x00005CF6
		public void Close()
		{
			this.close(1005, string.Empty);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00007B0C File Offset: 0x00005D0C
		public void Close(ushort code)
		{
			bool flag = !code.IsCloseStatusCode();
			if (flag)
			{
				string message = "Less than 1000 or greater than 4999.";
				throw new ArgumentOutOfRangeException("code", message);
			}
			bool flag2 = this._client && code == 1011;
			if (flag2)
			{
				string message2 = "1011 cannot be used.";
				throw new ArgumentException(message2, "code");
			}
			bool flag3 = !this._client && code == 1010;
			if (flag3)
			{
				string message3 = "1010 cannot be used.";
				throw new ArgumentException(message3, "code");
			}
			this.close(code, string.Empty);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00007BA0 File Offset: 0x00005DA0
		public void Close(CloseStatusCode code)
		{
			bool flag = this._client && code == CloseStatusCode.ServerError;
			if (flag)
			{
				string message = "ServerError cannot be used.";
				throw new ArgumentException(message, "code");
			}
			bool flag2 = !this._client && code == CloseStatusCode.MandatoryExtension;
			if (flag2)
			{
				string message2 = "MandatoryExtension cannot be used.";
				throw new ArgumentException(message2, "code");
			}
			this.close((ushort)code, string.Empty);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00007C10 File Offset: 0x00005E10
		public void Close(ushort code, string reason)
		{
			bool flag = !code.IsCloseStatusCode();
			if (flag)
			{
				string message = "Less than 1000 or greater than 4999.";
				throw new ArgumentOutOfRangeException("code", message);
			}
			bool flag2 = this._client && code == 1011;
			if (flag2)
			{
				string message2 = "1011 cannot be used.";
				throw new ArgumentException(message2, "code");
			}
			bool flag3 = !this._client && code == 1010;
			if (flag3)
			{
				string message3 = "1010 cannot be used.";
				throw new ArgumentException(message3, "code");
			}
			bool flag4 = reason.IsNullOrEmpty();
			if (flag4)
			{
				this.close(code, string.Empty);
			}
			else
			{
				bool flag5 = code == 1005;
				if (flag5)
				{
					string message4 = "1005 cannot be used.";
					throw new ArgumentException(message4, "code");
				}
				byte[] array;
				bool flag6 = !reason.TryGetUTF8EncodedBytes(out array);
				if (flag6)
				{
					string message5 = "It could not be UTF-8-encoded.";
					throw new ArgumentException(message5, "reason");
				}
				bool flag7 = array.Length > 123;
				if (flag7)
				{
					string message6 = "Its size is greater than 123 bytes.";
					throw new ArgumentOutOfRangeException("reason", message6);
				}
				this.close(code, reason);
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00007D28 File Offset: 0x00005F28
		public void Close(CloseStatusCode code, string reason)
		{
			bool flag = this._client && code == CloseStatusCode.ServerError;
			if (flag)
			{
				string message = "ServerError cannot be used.";
				throw new ArgumentException(message, "code");
			}
			bool flag2 = !this._client && code == CloseStatusCode.MandatoryExtension;
			if (flag2)
			{
				string message2 = "MandatoryExtension cannot be used.";
				throw new ArgumentException(message2, "code");
			}
			bool flag3 = reason.IsNullOrEmpty();
			if (flag3)
			{
				this.close((ushort)code, string.Empty);
			}
			else
			{
				bool flag4 = code == CloseStatusCode.NoStatus;
				if (flag4)
				{
					string message3 = "NoStatus cannot be used.";
					throw new ArgumentException(message3, "code");
				}
				byte[] array;
				bool flag5 = !reason.TryGetUTF8EncodedBytes(out array);
				if (flag5)
				{
					string message4 = "It could not be UTF-8-encoded.";
					throw new ArgumentException(message4, "reason");
				}
				bool flag6 = array.Length > 123;
				if (flag6)
				{
					string message5 = "Its size is greater than 123 bytes.";
					throw new ArgumentOutOfRangeException("reason", message5);
				}
				this.close((ushort)code, reason);
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00007E1C File Offset: 0x0000601C
		public void CloseAsync()
		{
			this.closeAsync(1005, string.Empty);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00007E30 File Offset: 0x00006030
		public void CloseAsync(ushort code)
		{
			bool flag = !code.IsCloseStatusCode();
			if (flag)
			{
				string message = "Less than 1000 or greater than 4999.";
				throw new ArgumentOutOfRangeException("code", message);
			}
			bool flag2 = this._client && code == 1011;
			if (flag2)
			{
				string message2 = "1011 cannot be used.";
				throw new ArgumentException(message2, "code");
			}
			bool flag3 = !this._client && code == 1010;
			if (flag3)
			{
				string message3 = "1010 cannot be used.";
				throw new ArgumentException(message3, "code");
			}
			this.closeAsync(code, string.Empty);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00007EC4 File Offset: 0x000060C4
		public void CloseAsync(CloseStatusCode code)
		{
			bool flag = this._client && code == CloseStatusCode.ServerError;
			if (flag)
			{
				string message = "ServerError cannot be used.";
				throw new ArgumentException(message, "code");
			}
			bool flag2 = !this._client && code == CloseStatusCode.MandatoryExtension;
			if (flag2)
			{
				string message2 = "MandatoryExtension cannot be used.";
				throw new ArgumentException(message2, "code");
			}
			this.closeAsync((ushort)code, string.Empty);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00007F34 File Offset: 0x00006134
		public void CloseAsync(ushort code, string reason)
		{
			bool flag = !code.IsCloseStatusCode();
			if (flag)
			{
				string message = "Less than 1000 or greater than 4999.";
				throw new ArgumentOutOfRangeException("code", message);
			}
			bool flag2 = this._client && code == 1011;
			if (flag2)
			{
				string message2 = "1011 cannot be used.";
				throw new ArgumentException(message2, "code");
			}
			bool flag3 = !this._client && code == 1010;
			if (flag3)
			{
				string message3 = "1010 cannot be used.";
				throw new ArgumentException(message3, "code");
			}
			bool flag4 = reason.IsNullOrEmpty();
			if (flag4)
			{
				this.closeAsync(code, string.Empty);
			}
			else
			{
				bool flag5 = code == 1005;
				if (flag5)
				{
					string message4 = "1005 cannot be used.";
					throw new ArgumentException(message4, "code");
				}
				byte[] array;
				bool flag6 = !reason.TryGetUTF8EncodedBytes(out array);
				if (flag6)
				{
					string message5 = "It could not be UTF-8-encoded.";
					throw new ArgumentException(message5, "reason");
				}
				bool flag7 = array.Length > 123;
				if (flag7)
				{
					string message6 = "Its size is greater than 123 bytes.";
					throw new ArgumentOutOfRangeException("reason", message6);
				}
				this.closeAsync(code, reason);
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000804C File Offset: 0x0000624C
		public void CloseAsync(CloseStatusCode code, string reason)
		{
			bool flag = this._client && code == CloseStatusCode.ServerError;
			if (flag)
			{
				string message = "ServerError cannot be used.";
				throw new ArgumentException(message, "code");
			}
			bool flag2 = !this._client && code == CloseStatusCode.MandatoryExtension;
			if (flag2)
			{
				string message2 = "MandatoryExtension cannot be used.";
				throw new ArgumentException(message2, "code");
			}
			bool flag3 = reason.IsNullOrEmpty();
			if (flag3)
			{
				this.closeAsync((ushort)code, string.Empty);
			}
			else
			{
				bool flag4 = code == CloseStatusCode.NoStatus;
				if (flag4)
				{
					string message3 = "NoStatus cannot be used.";
					throw new ArgumentException(message3, "code");
				}
				byte[] array;
				bool flag5 = !reason.TryGetUTF8EncodedBytes(out array);
				if (flag5)
				{
					string message4 = "It could not be UTF-8-encoded.";
					throw new ArgumentException(message4, "reason");
				}
				bool flag6 = array.Length > 123;
				if (flag6)
				{
					string message5 = "Its size is greater than 123 bytes.";
					throw new ArgumentOutOfRangeException("reason", message5);
				}
				this.closeAsync((ushort)code, reason);
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00008140 File Offset: 0x00006340
		public void Connect()
		{
			bool flag = !this._client;
			if (flag)
			{
				string message = "This instance is not a client.";
				throw new InvalidOperationException(message);
			}
			bool flag2 = this._readyState == WebSocketState.Closing;
			if (flag2)
			{
				string message2 = "The close process is in progress.";
				throw new InvalidOperationException(message2);
			}
			bool flag3 = this._retryCountForConnect > WebSocket._maxRetryCountForConnect;
			if (flag3)
			{
				string message3 = "A series of reconnecting has failed.";
				throw new InvalidOperationException(message3);
			}
			bool flag4 = this.connect();
			if (flag4)
			{
				this.open();
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000081BC File Offset: 0x000063BC
		public void ConnectAsync()
		{
			bool flag = !this._client;
			if (flag)
			{
				string message = "This instance is not a client.";
				throw new InvalidOperationException(message);
			}
			bool flag2 = this._readyState == WebSocketState.Closing;
			if (flag2)
			{
				string message2 = "The close process is in progress.";
				throw new InvalidOperationException(message2);
			}
			bool flag3 = this._retryCountForConnect > WebSocket._maxRetryCountForConnect;
			if (flag3)
			{
				string message3 = "A series of reconnecting has failed.";
				throw new InvalidOperationException(message3);
			}
			Func<bool> connector = new Func<bool>(this.connect);
			connector.BeginInvoke(delegate(IAsyncResult ar)
			{
				bool flag4 = connector.EndInvoke(ar);
				if (flag4)
				{
					this.open();
				}
			}, null);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00008260 File Offset: 0x00006460
		public bool Ping()
		{
			return this.ping(WebSocket.EmptyBytes);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00008280 File Offset: 0x00006480
		public bool Ping(string message)
		{
			bool flag = message.IsNullOrEmpty();
			bool result;
			if (flag)
			{
				result = this.ping(WebSocket.EmptyBytes);
			}
			else
			{
				byte[] array;
				bool flag2 = !message.TryGetUTF8EncodedBytes(out array);
				if (flag2)
				{
					string message2 = "It could not be UTF-8-encoded.";
					throw new ArgumentException(message2, "message");
				}
				bool flag3 = array.Length > 125;
				if (flag3)
				{
					string message3 = "Its size is greater than 125 bytes.";
					throw new ArgumentOutOfRangeException("message", message3);
				}
				result = this.ping(array);
			}
			return result;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000082F8 File Offset: 0x000064F8
		public void Send(byte[] data)
		{
			bool flag = this._readyState != WebSocketState.Open;
			if (flag)
			{
				string message = "The current state of the connection is not Open.";
				throw new InvalidOperationException(message);
			}
			bool flag2 = data == null;
			if (flag2)
			{
				throw new ArgumentNullException("data");
			}
			this.send(Opcode.Binary, new MemoryStream(data));
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00008348 File Offset: 0x00006548
		public void Send(FileInfo fileInfo)
		{
			bool flag = this._readyState != WebSocketState.Open;
			if (flag)
			{
				string message = "The current state of the connection is not Open.";
				throw new InvalidOperationException(message);
			}
			bool flag2 = fileInfo == null;
			if (flag2)
			{
				throw new ArgumentNullException("fileInfo");
			}
			bool flag3 = !fileInfo.Exists;
			if (flag3)
			{
				string message2 = "The file does not exist.";
				throw new ArgumentException(message2, "fileInfo");
			}
			FileStream stream;
			bool flag4 = !fileInfo.TryOpenRead(out stream);
			if (flag4)
			{
				string message3 = "The file could not be opened.";
				throw new ArgumentException(message3, "fileInfo");
			}
			this.send(Opcode.Binary, stream);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000083DC File Offset: 0x000065DC
		public void Send(string data)
		{
			bool flag = this._readyState != WebSocketState.Open;
			if (flag)
			{
				string message = "The current state of the connection is not Open.";
				throw new InvalidOperationException(message);
			}
			bool flag2 = data == null;
			if (flag2)
			{
				throw new ArgumentNullException("data");
			}
			byte[] buffer;
			bool flag3 = !data.TryGetUTF8EncodedBytes(out buffer);
			if (flag3)
			{
				string message2 = "It could not be UTF-8-encoded.";
				throw new ArgumentException(message2, "data");
			}
			this.send(Opcode.Text, new MemoryStream(buffer));
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00008454 File Offset: 0x00006654
		public void Send(Stream stream, int length)
		{
			bool flag = this._readyState != WebSocketState.Open;
			if (flag)
			{
				string message = "The current state of the connection is not Open.";
				throw new InvalidOperationException(message);
			}
			bool flag2 = stream == null;
			if (flag2)
			{
				throw new ArgumentNullException("stream");
			}
			bool flag3 = !stream.CanRead;
			if (flag3)
			{
				string message2 = "It cannot be read.";
				throw new ArgumentException(message2, "stream");
			}
			bool flag4 = length < 1;
			if (flag4)
			{
				string message3 = "Less than 1.";
				throw new ArgumentException(message3, "length");
			}
			byte[] array = stream.ReadBytes(length);
			int num = array.Length;
			bool flag5 = num == 0;
			if (flag5)
			{
				string message4 = "No data could be read from it.";
				throw new ArgumentException(message4, "stream");
			}
			bool flag6 = num < length;
			if (flag6)
			{
				this._logger.Warn(string.Format("Only {0} byte(s) of data could be read from the stream.", num));
			}
			this.send(Opcode.Binary, new MemoryStream(array));
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00008540 File Offset: 0x00006740
		public void SendAsync(byte[] data, Action<bool> completed)
		{
			bool flag = this._readyState != WebSocketState.Open;
			if (flag)
			{
				string message = "The current state of the connection is not Open.";
				throw new InvalidOperationException(message);
			}
			bool flag2 = data == null;
			if (flag2)
			{
				throw new ArgumentNullException("data");
			}
			this.sendAsync(Opcode.Binary, new MemoryStream(data), completed);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00008590 File Offset: 0x00006790
		public void SendAsync(FileInfo fileInfo, Action<bool> completed)
		{
			bool flag = this._readyState != WebSocketState.Open;
			if (flag)
			{
				string message = "The current state of the connection is not Open.";
				throw new InvalidOperationException(message);
			}
			bool flag2 = fileInfo == null;
			if (flag2)
			{
				throw new ArgumentNullException("fileInfo");
			}
			bool flag3 = !fileInfo.Exists;
			if (flag3)
			{
				string message2 = "The file does not exist.";
				throw new ArgumentException(message2, "fileInfo");
			}
			FileStream stream;
			bool flag4 = !fileInfo.TryOpenRead(out stream);
			if (flag4)
			{
				string message3 = "The file could not be opened.";
				throw new ArgumentException(message3, "fileInfo");
			}
			this.sendAsync(Opcode.Binary, stream, completed);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00008628 File Offset: 0x00006828
		public void SendAsync(string data, Action<bool> completed)
		{
			bool flag = this._readyState != WebSocketState.Open;
			if (flag)
			{
				string message = "The current state of the connection is not Open.";
				throw new InvalidOperationException(message);
			}
			bool flag2 = data == null;
			if (flag2)
			{
				throw new ArgumentNullException("data");
			}
			byte[] buffer;
			bool flag3 = !data.TryGetUTF8EncodedBytes(out buffer);
			if (flag3)
			{
				string message2 = "It could not be UTF-8-encoded.";
				throw new ArgumentException(message2, "data");
			}
			this.sendAsync(Opcode.Text, new MemoryStream(buffer), completed);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000086A0 File Offset: 0x000068A0
		public void SendAsync(Stream stream, int length, Action<bool> completed)
		{
			bool flag = this._readyState != WebSocketState.Open;
			if (flag)
			{
				string message = "The current state of the connection is not Open.";
				throw new InvalidOperationException(message);
			}
			bool flag2 = stream == null;
			if (flag2)
			{
				throw new ArgumentNullException("stream");
			}
			bool flag3 = !stream.CanRead;
			if (flag3)
			{
				string message2 = "It cannot be read.";
				throw new ArgumentException(message2, "stream");
			}
			bool flag4 = length < 1;
			if (flag4)
			{
				string message3 = "Less than 1.";
				throw new ArgumentException(message3, "length");
			}
			byte[] array = stream.ReadBytes(length);
			int num = array.Length;
			bool flag5 = num == 0;
			if (flag5)
			{
				string message4 = "No data could be read from it.";
				throw new ArgumentException(message4, "stream");
			}
			bool flag6 = num < length;
			if (flag6)
			{
				this._logger.Warn(string.Format("Only {0} byte(s) of data could be read from the stream.", num));
			}
			this.sendAsync(Opcode.Binary, new MemoryStream(array), completed);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000878C File Offset: 0x0000698C
		public void SetCookie(Cookie cookie)
		{
			string message = null;
			bool flag = !this._client;
			if (flag)
			{
				message = "This instance is not a client.";
				throw new InvalidOperationException(message);
			}
			bool flag2 = cookie == null;
			if (flag2)
			{
				throw new ArgumentNullException("cookie");
			}
			bool flag3 = !this.canSet(out message);
			if (flag3)
			{
				this._logger.Warn(message);
			}
			else
			{
				object forState = this._forState;
				lock (forState)
				{
					bool flag4 = !this.canSet(out message);
					if (flag4)
					{
						this._logger.Warn(message);
					}
					else
					{
						object syncRoot = this._cookies.SyncRoot;
						lock (syncRoot)
						{
							this._cookies.SetOrRemove(cookie);
						}
					}
				}
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00008874 File Offset: 0x00006A74
		public void SetCredentials(string username, string password, bool preAuth)
		{
			string message = null;
			bool flag = !this._client;
			if (flag)
			{
				message = "This instance is not a client.";
				throw new InvalidOperationException(message);
			}
			bool flag2 = !username.IsNullOrEmpty();
			if (flag2)
			{
				bool flag3 = username.Contains(new char[]
				{
					':'
				}) || !username.IsText();
				if (flag3)
				{
					message = "It contains an invalid character.";
					throw new ArgumentException(message, "username");
				}
			}
			bool flag4 = !password.IsNullOrEmpty();
			if (flag4)
			{
				bool flag5 = !password.IsText();
				if (flag5)
				{
					message = "It contains an invalid character.";
					throw new ArgumentException(message, "password");
				}
			}
			bool flag6 = !this.canSet(out message);
			if (flag6)
			{
				this._logger.Warn(message);
			}
			else
			{
				object forState = this._forState;
				lock (forState)
				{
					bool flag7 = !this.canSet(out message);
					if (flag7)
					{
						this._logger.Warn(message);
					}
					else
					{
						bool flag8 = username.IsNullOrEmpty();
						if (flag8)
						{
							this._credentials = null;
							this._preAuth = false;
						}
						else
						{
							this._credentials = new NetworkCredential(username, password, this._uri.PathAndQuery, new string[0]);
							this._preAuth = preAuth;
						}
					}
				}
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000089D0 File Offset: 0x00006BD0
		public void SetProxy(string url, string username, string password)
		{
			string message = null;
			bool flag = !this._client;
			if (flag)
			{
				message = "This instance is not a client.";
				throw new InvalidOperationException(message);
			}
			Uri uri = null;
			bool flag2 = !url.IsNullOrEmpty();
			if (flag2)
			{
				bool flag3 = !Uri.TryCreate(url, UriKind.Absolute, out uri);
				if (flag3)
				{
					message = "Not an absolute URI string.";
					throw new ArgumentException(message, "url");
				}
				bool flag4 = uri.Scheme != "http";
				if (flag4)
				{
					message = "The scheme part is not http.";
					throw new ArgumentException(message, "url");
				}
				bool flag5 = uri.Segments.Length > 1;
				if (flag5)
				{
					message = "It includes the path segments.";
					throw new ArgumentException(message, "url");
				}
			}
			bool flag6 = !username.IsNullOrEmpty();
			if (flag6)
			{
				bool flag7 = username.Contains(new char[]
				{
					':'
				}) || !username.IsText();
				if (flag7)
				{
					message = "It contains an invalid character.";
					throw new ArgumentException(message, "username");
				}
			}
			bool flag8 = !password.IsNullOrEmpty();
			if (flag8)
			{
				bool flag9 = !password.IsText();
				if (flag9)
				{
					message = "It contains an invalid character.";
					throw new ArgumentException(message, "password");
				}
			}
			bool flag10 = !this.canSet(out message);
			if (flag10)
			{
				this._logger.Warn(message);
			}
			else
			{
				object forState = this._forState;
				lock (forState)
				{
					bool flag11 = !this.canSet(out message);
					if (flag11)
					{
						this._logger.Warn(message);
					}
					else
					{
						bool flag12 = url.IsNullOrEmpty();
						if (flag12)
						{
							this._proxyUri = null;
							this._proxyCredentials = null;
						}
						else
						{
							this._proxyUri = uri;
							this._proxyCredentials = ((!username.IsNullOrEmpty()) ? new NetworkCredential(username, password, string.Format("{0}:{1}", this._uri.DnsSafeHost, this._uri.Port), new string[0]) : null);
						}
					}
				}
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00008BD8 File Offset: 0x00006DD8
		void IDisposable.Dispose()
		{
			this.close(1001, string.Empty);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00008BEC File Offset: 0x00006DEC
		[CompilerGenerated]
		private void <open>b__146_0(IAsyncResult ar)
		{
			this._message.EndInvoke(ar);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00008BFB File Offset: 0x00006DFB
		[CompilerGenerated]
		private bool <processSecWebSocketProtocolClientHeader>b__157_0(string val)
		{
			return val == this._protocol;
		}

		// Token: 0x0400000F RID: 15
		private AuthenticationChallenge _authChallenge;

		// Token: 0x04000010 RID: 16
		private string _base64Key;

		// Token: 0x04000011 RID: 17
		private bool _client;

		// Token: 0x04000012 RID: 18
		private Action _closeContext;

		// Token: 0x04000013 RID: 19
		private CompressionMethod _compression;

		// Token: 0x04000014 RID: 20
		private WebSocketContext _context;

		// Token: 0x04000015 RID: 21
		private CookieCollection _cookies;

		// Token: 0x04000016 RID: 22
		private NetworkCredential _credentials;

		// Token: 0x04000017 RID: 23
		private bool _emitOnPing;

		// Token: 0x04000018 RID: 24
		private bool _enableRedirection;

		// Token: 0x04000019 RID: 25
		private string _extensions;

		// Token: 0x0400001A RID: 26
		private bool _extensionsRequested;

		// Token: 0x0400001B RID: 27
		private object _forMessageEventQueue;

		// Token: 0x0400001C RID: 28
		private object _forPing;

		// Token: 0x0400001D RID: 29
		private object _forSend;

		// Token: 0x0400001E RID: 30
		private object _forState;

		// Token: 0x0400001F RID: 31
		private MemoryStream _fragmentsBuffer;

		// Token: 0x04000020 RID: 32
		private bool _fragmentsCompressed;

		// Token: 0x04000021 RID: 33
		private Opcode _fragmentsOpcode;

		// Token: 0x04000022 RID: 34
		private const string _guid = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";

		// Token: 0x04000023 RID: 35
		private Func<WebSocketContext, string> _handshakeRequestChecker;

		// Token: 0x04000024 RID: 36
		private bool _ignoreExtensions;

		// Token: 0x04000025 RID: 37
		private bool _inContinuation;

		// Token: 0x04000026 RID: 38
		private volatile bool _inMessage;

		// Token: 0x04000027 RID: 39
		private volatile Logger _logger;

		// Token: 0x04000028 RID: 40
		private static readonly int _maxRetryCountForConnect = 10;

		// Token: 0x04000029 RID: 41
		private Action<MessageEventArgs> _message;

		// Token: 0x0400002A RID: 42
		private Queue<MessageEventArgs> _messageEventQueue;

		// Token: 0x0400002B RID: 43
		private uint _nonceCount;

		// Token: 0x0400002C RID: 44
		private string _origin;

		// Token: 0x0400002D RID: 45
		private ManualResetEvent _pongReceived;

		// Token: 0x0400002E RID: 46
		private bool _preAuth;

		// Token: 0x0400002F RID: 47
		private string _protocol;

		// Token: 0x04000030 RID: 48
		private string[] _protocols;

		// Token: 0x04000031 RID: 49
		private bool _protocolsRequested;

		// Token: 0x04000032 RID: 50
		private NetworkCredential _proxyCredentials;

		// Token: 0x04000033 RID: 51
		private Uri _proxyUri;

		// Token: 0x04000034 RID: 52
		private volatile WebSocketState _readyState;

		// Token: 0x04000035 RID: 53
		private ManualResetEvent _receivingExited;

		// Token: 0x04000036 RID: 54
		private int _retryCountForConnect;

		// Token: 0x04000037 RID: 55
		private bool _secure;

		// Token: 0x04000038 RID: 56
		private ClientSslConfiguration _sslConfig;

		// Token: 0x04000039 RID: 57
		private Stream _stream;

		// Token: 0x0400003A RID: 58
		private TcpClient _tcpClient;

		// Token: 0x0400003B RID: 59
		private Uri _uri;

		// Token: 0x0400003C RID: 60
		private const string _version = "13";

		// Token: 0x0400003D RID: 61
		private TimeSpan _waitTime;

		// Token: 0x0400003E RID: 62
		internal static readonly byte[] EmptyBytes = new byte[0];

		// Token: 0x0400003F RID: 63
		internal static readonly int FragmentLength = 1016;

		// Token: 0x04000040 RID: 64
		internal static readonly RandomNumberGenerator RandomNumber = new RNGCryptoServiceProvider();

		// Token: 0x04000041 RID: 65
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EventHandler<CloseEventArgs> OnClose;

		// Token: 0x04000042 RID: 66
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EventHandler<ErrorEventArgs> OnError;

		// Token: 0x04000043 RID: 67
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EventHandler<MessageEventArgs> OnMessage;

		// Token: 0x04000044 RID: 68
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EventHandler OnOpen;

		// Token: 0x02000058 RID: 88
		[CompilerGenerated]
		private sealed class <get_Cookies>d__70 : IEnumerable<Cookie>, IEnumerable, IEnumerator<Cookie>, IDisposable, IEnumerator
		{
			// Token: 0x06000575 RID: 1397 RVA: 0x0001E3A0 File Offset: 0x0001C5A0
			[DebuggerHidden]
			public <get_Cookies>d__70(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06000576 RID: 1398 RVA: 0x0001E3C0 File Offset: 0x0001C5C0
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num - -4 <= 1 || num == 1)
				{
					try
					{
						if (num == -4 || num == 1)
						{
							try
							{
							}
							finally
							{
								this.<>m__Finally2();
							}
						}
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x06000577 RID: 1399 RVA: 0x0001E424 File Offset: 0x0001C624
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					if (num != 0)
					{
						if (num != 1)
						{
							return false;
						}
						this.<>1__state = -4;
						cookie = null;
					}
					else
					{
						this.<>1__state = -1;
						obj = this._cookies.SyncRoot;
						Monitor.Enter(obj);
						this.<>1__state = -3;
						enumerator = this._cookies.GetEnumerator();
						this.<>1__state = -4;
					}
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally2();
						enumerator = null;
						this.<>m__Finally1();
						obj = null;
						result = false;
					}
					else
					{
						cookie = enumerator.Current;
						this.<>2__current = cookie;
						this.<>1__state = 1;
						result = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x06000578 RID: 1400 RVA: 0x0001E524 File Offset: 0x0001C724
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				Monitor.Exit(obj);
			}

			// Token: 0x06000579 RID: 1401 RVA: 0x0001E539 File Offset: 0x0001C739
			private void <>m__Finally2()
			{
				this.<>1__state = -3;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x170001A8 RID: 424
			// (get) Token: 0x0600057A RID: 1402 RVA: 0x0001E557 File Offset: 0x0001C757
			Cookie IEnumerator<Cookie>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600057B RID: 1403 RVA: 0x0001E136 File Offset: 0x0001C336
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001A9 RID: 425
			// (get) Token: 0x0600057C RID: 1404 RVA: 0x0001E557 File Offset: 0x0001C757
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600057D RID: 1405 RVA: 0x0001E560 File Offset: 0x0001C760
			[DebuggerHidden]
			IEnumerator<Cookie> IEnumerable<Cookie>.GetEnumerator()
			{
				WebSocket.<get_Cookies>d__70 <get_Cookies>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<get_Cookies>d__ = this;
				}
				else
				{
					<get_Cookies>d__ = new WebSocket.<get_Cookies>d__70(0);
					<get_Cookies>d__.<>4__this = this;
				}
				return <get_Cookies>d__;
			}

			// Token: 0x0600057E RID: 1406 RVA: 0x0001E5A8 File Offset: 0x0001C7A8
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<WebSocketSharp.Net.Cookie>.GetEnumerator();
			}

			// Token: 0x04000295 RID: 661
			private int <>1__state;

			// Token: 0x04000296 RID: 662
			private Cookie <>2__current;

			// Token: 0x04000297 RID: 663
			private int <>l__initialThreadId;

			// Token: 0x04000298 RID: 664
			public WebSocket <>4__this;

			// Token: 0x04000299 RID: 665
			private object <>s__1;

			// Token: 0x0400029A RID: 666
			private IEnumerator<Cookie> <>s__2;

			// Token: 0x0400029B RID: 667
			private Cookie <cookie>5__3;
		}

		// Token: 0x02000059 RID: 89
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600057F RID: 1407 RVA: 0x0001E5B0 File Offset: 0x0001C7B0
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000580 RID: 1408 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c()
			{
			}

			// Token: 0x06000581 RID: 1409 RVA: 0x0001E5BC File Offset: 0x0001C7BC
			internal bool <checkProtocols>b__120_0(string protocol)
			{
				return protocol.IsNullOrEmpty() || !protocol.IsToken();
			}

			// Token: 0x0400029C RID: 668
			public static readonly WebSocket.<>c <>9 = new WebSocket.<>c();

			// Token: 0x0400029D RID: 669
			public static Func<string, bool> <>9__120_0;
		}

		// Token: 0x0200005A RID: 90
		[CompilerGenerated]
		private sealed class <>c__DisplayClass125_0
		{
			// Token: 0x06000582 RID: 1410 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass125_0()
			{
			}

			// Token: 0x06000583 RID: 1411 RVA: 0x0001E5D2 File Offset: 0x0001C7D2
			internal void <closeAsync>b__0(IAsyncResult ar)
			{
				this.closer.EndInvoke(ar);
			}

			// Token: 0x0400029E RID: 670
			public Action<PayloadData, bool, bool, bool> closer;
		}

		// Token: 0x0200005B RID: 91
		[CompilerGenerated]
		private sealed class <>c__DisplayClass145_0
		{
			// Token: 0x06000584 RID: 1412 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass145_0()
			{
			}

			// Token: 0x06000585 RID: 1413 RVA: 0x0001E5E1 File Offset: 0x0001C7E1
			internal void <messages>b__0(object state)
			{
				this.<>4__this.messages(this.e);
			}

			// Token: 0x0400029F RID: 671
			public WebSocket <>4__this;

			// Token: 0x040002A0 RID: 672
			public MessageEventArgs e;
		}

		// Token: 0x0200005C RID: 92
		[CompilerGenerated]
		private sealed class <>c__DisplayClass167_0
		{
			// Token: 0x06000586 RID: 1414 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass167_0()
			{
			}

			// Token: 0x06000587 RID: 1415 RVA: 0x0001E5F8 File Offset: 0x0001C7F8
			internal void <sendAsync>b__0(IAsyncResult ar)
			{
				try
				{
					bool obj = this.sender.EndInvoke(ar);
					bool flag = this.completed != null;
					if (flag)
					{
						this.completed(obj);
					}
				}
				catch (Exception ex)
				{
					this.<>4__this._logger.Error(ex.ToString());
					this.<>4__this.error("An error has occurred during the callback for an async send.", ex);
				}
			}

			// Token: 0x040002A1 RID: 673
			public Func<Opcode, Stream, bool> sender;

			// Token: 0x040002A2 RID: 674
			public Action<bool> completed;

			// Token: 0x040002A3 RID: 675
			public WebSocket <>4__this;
		}

		// Token: 0x0200005D RID: 93
		[CompilerGenerated]
		private sealed class <>c__DisplayClass174_0
		{
			// Token: 0x06000588 RID: 1416 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass174_0()
			{
			}

			// Token: 0x06000589 RID: 1417 RVA: 0x0001E674 File Offset: 0x0001C874
			internal void <startReceiving>b__0()
			{
				Stream stream = this.<>4__this._stream;
				bool unmask = false;
				Action<WebSocketFrame> completed;
				if ((completed = this.<>9__1) == null)
				{
					completed = (this.<>9__1 = delegate(WebSocketFrame frame)
					{
						bool flag = !this.<>4__this.processReceivedFrame(frame) || this.<>4__this._readyState == WebSocketState.Closed;
						if (flag)
						{
							ManualResetEvent receivingExited = this.<>4__this._receivingExited;
							bool flag2 = receivingExited != null;
							if (flag2)
							{
								receivingExited.Set();
							}
						}
						else
						{
							this.receive();
							bool flag3 = this.<>4__this._inMessage || !this.<>4__this.HasMessage || this.<>4__this._readyState != WebSocketState.Open;
							if (!flag3)
							{
								this.<>4__this.message();
							}
						}
					});
				}
				Action<Exception> error;
				if ((error = this.<>9__2) == null)
				{
					error = (this.<>9__2 = delegate(Exception ex)
					{
						this.<>4__this._logger.Fatal(ex.ToString());
						this.<>4__this.fatal("An exception has occurred while receiving.", ex);
					});
				}
				WebSocketFrame.ReadFrameAsync(stream, unmask, completed, error);
			}

			// Token: 0x0600058A RID: 1418 RVA: 0x0001E6D4 File Offset: 0x0001C8D4
			internal void <startReceiving>b__1(WebSocketFrame frame)
			{
				bool flag = !this.<>4__this.processReceivedFrame(frame) || this.<>4__this._readyState == WebSocketState.Closed;
				if (flag)
				{
					ManualResetEvent receivingExited = this.<>4__this._receivingExited;
					bool flag2 = receivingExited != null;
					if (flag2)
					{
						receivingExited.Set();
					}
				}
				else
				{
					this.receive();
					bool flag3 = this.<>4__this._inMessage || !this.<>4__this.HasMessage || this.<>4__this._readyState != WebSocketState.Open;
					if (!flag3)
					{
						this.<>4__this.message();
					}
				}
			}

			// Token: 0x0600058B RID: 1419 RVA: 0x0001E775 File Offset: 0x0001C975
			internal void <startReceiving>b__2(Exception ex)
			{
				this.<>4__this._logger.Fatal(ex.ToString());
				this.<>4__this.fatal("An exception has occurred while receiving.", ex);
			}

			// Token: 0x040002A4 RID: 676
			public WebSocket <>4__this;

			// Token: 0x040002A5 RID: 677
			public Action receive;

			// Token: 0x040002A6 RID: 678
			public Action<WebSocketFrame> <>9__1;

			// Token: 0x040002A7 RID: 679
			public Action<Exception> <>9__2;
		}

		// Token: 0x0200005E RID: 94
		[CompilerGenerated]
		private sealed class <>c__DisplayClass176_0
		{
			// Token: 0x0600058C RID: 1420 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass176_0()
			{
			}

			// Token: 0x0600058D RID: 1421 RVA: 0x0001E7A4 File Offset: 0x0001C9A4
			internal bool <validateSecWebSocketExtensionsServerHeader>b__0(string t)
			{
				t = t.Trim();
				return t != this.method && t != "server_no_context_takeover" && t != "client_no_context_takeover";
			}

			// Token: 0x040002A8 RID: 680
			public string method;
		}

		// Token: 0x0200005F RID: 95
		[CompilerGenerated]
		private sealed class <>c__DisplayClass177_0
		{
			// Token: 0x0600058E RID: 1422 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass177_0()
			{
			}

			// Token: 0x0600058F RID: 1423 RVA: 0x0001E7E7 File Offset: 0x0001C9E7
			internal bool <validateSecWebSocketProtocolServerHeader>b__0(string p)
			{
				return p == this.value;
			}

			// Token: 0x040002A9 RID: 681
			public string value;
		}

		// Token: 0x02000060 RID: 96
		[CompilerGenerated]
		private sealed class <>c__DisplayClass189_0
		{
			// Token: 0x06000590 RID: 1424 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass189_0()
			{
			}

			// Token: 0x06000591 RID: 1425 RVA: 0x0001E7F8 File Offset: 0x0001C9F8
			internal void <AcceptAsync>b__0(IAsyncResult ar)
			{
				bool flag = this.acceptor.EndInvoke(ar);
				if (flag)
				{
					this.<>4__this.open();
				}
			}

			// Token: 0x040002AA RID: 682
			public Func<bool> acceptor;

			// Token: 0x040002AB RID: 683
			public WebSocket <>4__this;
		}

		// Token: 0x02000061 RID: 97
		[CompilerGenerated]
		private sealed class <>c__DisplayClass201_0
		{
			// Token: 0x06000592 RID: 1426 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass201_0()
			{
			}

			// Token: 0x06000593 RID: 1427 RVA: 0x0001E824 File Offset: 0x0001CA24
			internal void <ConnectAsync>b__0(IAsyncResult ar)
			{
				bool flag = this.connector.EndInvoke(ar);
				if (flag)
				{
					this.<>4__this.open();
				}
			}

			// Token: 0x040002AC RID: 684
			public Func<bool> connector;

			// Token: 0x040002AD RID: 685
			public WebSocket <>4__this;
		}
	}
}
