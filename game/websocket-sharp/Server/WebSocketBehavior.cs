using System;
using System.Collections.Specialized;
using System.IO;
using WebSocketSharp.Net;
using WebSocketSharp.Net.WebSockets;

namespace WebSocketSharp.Server
{
	// Token: 0x0200004D RID: 77
	public abstract class WebSocketBehavior : IWebSocketSession
	{
		// Token: 0x06000527 RID: 1319 RVA: 0x0001D169 File Offset: 0x0001B369
		protected WebSocketBehavior()
		{
			this._startTime = DateTime.MaxValue;
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x0001D180 File Offset: 0x0001B380
		protected NameValueCollection Headers
		{
			get
			{
				return (this._context != null) ? this._context.Headers : null;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x0001D1A8 File Offset: 0x0001B3A8
		protected NameValueCollection QueryString
		{
			get
			{
				return (this._context != null) ? this._context.QueryString : null;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0001D1D0 File Offset: 0x0001B3D0
		protected WebSocketSessionManager Sessions
		{
			get
			{
				return this._sessions;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0001D1E8 File Offset: 0x0001B3E8
		public WebSocketState ConnectionState
		{
			get
			{
				return (this._websocket != null) ? this._websocket.ReadyState : WebSocketState.Connecting;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x0001D210 File Offset: 0x0001B410
		public WebSocketContext Context
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x0001D228 File Offset: 0x0001B428
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x0001D240 File Offset: 0x0001B440
		public Func<CookieCollection, CookieCollection, bool> CookiesValidator
		{
			get
			{
				return this._cookiesValidator;
			}
			set
			{
				this._cookiesValidator = value;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x0001D24C File Offset: 0x0001B44C
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x0001D27C File Offset: 0x0001B47C
		public bool EmitOnPing
		{
			get
			{
				return (this._websocket != null) ? this._websocket.EmitOnPing : this._emitOnPing;
			}
			set
			{
				bool flag = this._websocket != null;
				if (flag)
				{
					this._websocket.EmitOnPing = value;
				}
				else
				{
					this._emitOnPing = value;
				}
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0001D2B0 File Offset: 0x0001B4B0
		public string ID
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0001D2C8 File Offset: 0x0001B4C8
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x0001D2E0 File Offset: 0x0001B4E0
		public bool IgnoreExtensions
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

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0001D2EC File Offset: 0x0001B4EC
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x0001D304 File Offset: 0x0001B504
		public Func<string, bool> OriginValidator
		{
			get
			{
				return this._originValidator;
			}
			set
			{
				this._originValidator = value;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0001D310 File Offset: 0x0001B510
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x0001D348 File Offset: 0x0001B548
		public string Protocol
		{
			get
			{
				return (this._websocket != null) ? this._websocket.Protocol : (this._protocol ?? string.Empty);
			}
			set
			{
				bool flag = this._websocket != null;
				if (flag)
				{
					string message = "The session has already started.";
					throw new InvalidOperationException(message);
				}
				bool flag2 = value == null || value.Length == 0;
				if (flag2)
				{
					this._protocol = null;
				}
				else
				{
					bool flag3 = !value.IsToken();
					if (flag3)
					{
						string message2 = "It is not a token.";
						throw new ArgumentException(message2, "value");
					}
					this._protocol = value;
				}
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x0001D3B8 File Offset: 0x0001B5B8
		public DateTime StartTime
		{
			get
			{
				return this._startTime;
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0001D3D0 File Offset: 0x0001B5D0
		private string checkHandshakeRequest(WebSocketContext context)
		{
			bool flag = this._originValidator != null;
			if (flag)
			{
				bool flag2 = !this._originValidator(context.Origin);
				if (flag2)
				{
					return "It includes no Origin header or an invalid one.";
				}
			}
			bool flag3 = this._cookiesValidator != null;
			if (flag3)
			{
				CookieCollection cookieCollection = context.CookieCollection;
				CookieCollection cookieCollection2 = context.WebSocket.CookieCollection;
				bool flag4 = !this._cookiesValidator(cookieCollection, cookieCollection2);
				if (flag4)
				{
					return "It includes no cookie or an invalid one.";
				}
			}
			return null;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0001D458 File Offset: 0x0001B658
		private void onClose(object sender, CloseEventArgs e)
		{
			bool flag = this._id == null;
			if (!flag)
			{
				this._sessions.Remove(this._id);
				this.OnClose(e);
			}
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001D48F File Offset: 0x0001B68F
		private void onError(object sender, ErrorEventArgs e)
		{
			this.OnError(e);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001D49A File Offset: 0x0001B69A
		private void onMessage(object sender, MessageEventArgs e)
		{
			this.OnMessage(e);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0001D4A8 File Offset: 0x0001B6A8
		private void onOpen(object sender, EventArgs e)
		{
			this._id = this._sessions.Add(this);
			bool flag = this._id == null;
			if (flag)
			{
				this._websocket.Close(CloseStatusCode.Away);
			}
			else
			{
				this._startTime = DateTime.Now;
				this.OnOpen();
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0001D4FC File Offset: 0x0001B6FC
		internal void Start(WebSocketContext context, WebSocketSessionManager sessions)
		{
			this._context = context;
			this._sessions = sessions;
			this._websocket = context.WebSocket;
			this._websocket.CustomHandshakeRequestChecker = new Func<WebSocketContext, string>(this.checkHandshakeRequest);
			this._websocket.EmitOnPing = this._emitOnPing;
			this._websocket.IgnoreExtensions = this._ignoreExtensions;
			this._websocket.Protocol = this._protocol;
			TimeSpan waitTime = sessions.WaitTime;
			bool flag = waitTime != this._websocket.WaitTime;
			if (flag)
			{
				this._websocket.WaitTime = waitTime;
			}
			this._websocket.OnOpen += this.onOpen;
			this._websocket.OnMessage += this.onMessage;
			this._websocket.OnError += this.onError;
			this._websocket.OnClose += this.onClose;
			this._websocket.InternalAccept();
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001D608 File Offset: 0x0001B808
		protected void Close()
		{
			bool flag = this._websocket == null;
			if (flag)
			{
				string message = "The session has not started yet.";
				throw new InvalidOperationException(message);
			}
			this._websocket.Close();
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001D640 File Offset: 0x0001B840
		protected void Close(ushort code, string reason)
		{
			bool flag = this._websocket == null;
			if (flag)
			{
				string message = "The session has not started yet.";
				throw new InvalidOperationException(message);
			}
			this._websocket.Close(code, reason);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001D678 File Offset: 0x0001B878
		protected void Close(CloseStatusCode code, string reason)
		{
			bool flag = this._websocket == null;
			if (flag)
			{
				string message = "The session has not started yet.";
				throw new InvalidOperationException(message);
			}
			this._websocket.Close(code, reason);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0001D6B0 File Offset: 0x0001B8B0
		protected void CloseAsync()
		{
			bool flag = this._websocket == null;
			if (flag)
			{
				string message = "The session has not started yet.";
				throw new InvalidOperationException(message);
			}
			this._websocket.CloseAsync();
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001D6E8 File Offset: 0x0001B8E8
		protected void CloseAsync(ushort code, string reason)
		{
			bool flag = this._websocket == null;
			if (flag)
			{
				string message = "The session has not started yet.";
				throw new InvalidOperationException(message);
			}
			this._websocket.CloseAsync(code, reason);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001D720 File Offset: 0x0001B920
		protected void CloseAsync(CloseStatusCode code, string reason)
		{
			bool flag = this._websocket == null;
			if (flag)
			{
				string message = "The session has not started yet.";
				throw new InvalidOperationException(message);
			}
			this._websocket.CloseAsync(code, reason);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00014B49 File Offset: 0x00012D49
		protected virtual void OnClose(CloseEventArgs e)
		{
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00014B49 File Offset: 0x00012D49
		protected virtual void OnError(ErrorEventArgs e)
		{
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00014B49 File Offset: 0x00012D49
		protected virtual void OnMessage(MessageEventArgs e)
		{
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00014B49 File Offset: 0x00012D49
		protected virtual void OnOpen()
		{
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0001D758 File Offset: 0x0001B958
		protected bool Ping()
		{
			bool flag = this._websocket == null;
			if (flag)
			{
				string message = "The session has not started yet.";
				throw new InvalidOperationException(message);
			}
			return this._websocket.Ping();
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001D790 File Offset: 0x0001B990
		protected bool Ping(string message)
		{
			bool flag = this._websocket == null;
			if (flag)
			{
				string message2 = "The session has not started yet.";
				throw new InvalidOperationException(message2);
			}
			return this._websocket.Ping(message);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0001D7CC File Offset: 0x0001B9CC
		protected void Send(byte[] data)
		{
			bool flag = this._websocket == null;
			if (flag)
			{
				string message = "The current state of the connection is not Open.";
				throw new InvalidOperationException(message);
			}
			this._websocket.Send(data);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0001D804 File Offset: 0x0001BA04
		protected void Send(FileInfo fileInfo)
		{
			bool flag = this._websocket == null;
			if (flag)
			{
				string message = "The current state of the connection is not Open.";
				throw new InvalidOperationException(message);
			}
			this._websocket.Send(fileInfo);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001D83C File Offset: 0x0001BA3C
		protected void Send(string data)
		{
			bool flag = this._websocket == null;
			if (flag)
			{
				string message = "The current state of the connection is not Open.";
				throw new InvalidOperationException(message);
			}
			this._websocket.Send(data);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0001D874 File Offset: 0x0001BA74
		protected void Send(Stream stream, int length)
		{
			bool flag = this._websocket == null;
			if (flag)
			{
				string message = "The current state of the connection is not Open.";
				throw new InvalidOperationException(message);
			}
			this._websocket.Send(stream, length);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0001D8AC File Offset: 0x0001BAAC
		protected void SendAsync(byte[] data, Action<bool> completed)
		{
			bool flag = this._websocket == null;
			if (flag)
			{
				string message = "The current state of the connection is not Open.";
				throw new InvalidOperationException(message);
			}
			this._websocket.SendAsync(data, completed);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0001D8E4 File Offset: 0x0001BAE4
		protected void SendAsync(FileInfo fileInfo, Action<bool> completed)
		{
			bool flag = this._websocket == null;
			if (flag)
			{
				string message = "The current state of the connection is not Open.";
				throw new InvalidOperationException(message);
			}
			this._websocket.SendAsync(fileInfo, completed);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0001D91C File Offset: 0x0001BB1C
		protected void SendAsync(string data, Action<bool> completed)
		{
			bool flag = this._websocket == null;
			if (flag)
			{
				string message = "The current state of the connection is not Open.";
				throw new InvalidOperationException(message);
			}
			this._websocket.SendAsync(data, completed);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0001D954 File Offset: 0x0001BB54
		protected void SendAsync(Stream stream, int length, Action<bool> completed)
		{
			bool flag = this._websocket == null;
			if (flag)
			{
				string message = "The current state of the connection is not Open.";
				throw new InvalidOperationException(message);
			}
			this._websocket.SendAsync(stream, length, completed);
		}

		// Token: 0x04000254 RID: 596
		private WebSocketContext _context;

		// Token: 0x04000255 RID: 597
		private Func<CookieCollection, CookieCollection, bool> _cookiesValidator;

		// Token: 0x04000256 RID: 598
		private bool _emitOnPing;

		// Token: 0x04000257 RID: 599
		private string _id;

		// Token: 0x04000258 RID: 600
		private bool _ignoreExtensions;

		// Token: 0x04000259 RID: 601
		private Func<string, bool> _originValidator;

		// Token: 0x0400025A RID: 602
		private string _protocol;

		// Token: 0x0400025B RID: 603
		private WebSocketSessionManager _sessions;

		// Token: 0x0400025C RID: 604
		private DateTime _startTime;

		// Token: 0x0400025D RID: 605
		private WebSocket _websocket;
	}
}
