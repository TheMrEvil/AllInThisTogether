using System;
using System.Security.Principal;
using System.Text;
using WebSocketSharp.Net.WebSockets;

namespace WebSocketSharp.Net
{
	// Token: 0x02000021 RID: 33
	public sealed class HttpListenerContext
	{
		// Token: 0x06000260 RID: 608 RVA: 0x0000FD24 File Offset: 0x0000DF24
		internal HttpListenerContext(HttpConnection connection)
		{
			this._connection = connection;
			this._errorStatusCode = 400;
			this._request = new HttpListenerRequest(this);
			this._response = new HttpListenerResponse(this);
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000FD58 File Offset: 0x0000DF58
		internal HttpConnection Connection
		{
			get
			{
				return this._connection;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000FD70 File Offset: 0x0000DF70
		// (set) Token: 0x06000263 RID: 611 RVA: 0x0000FD88 File Offset: 0x0000DF88
		internal string ErrorMessage
		{
			get
			{
				return this._errorMessage;
			}
			set
			{
				this._errorMessage = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000FD94 File Offset: 0x0000DF94
		// (set) Token: 0x06000265 RID: 613 RVA: 0x0000FDAC File Offset: 0x0000DFAC
		internal int ErrorStatusCode
		{
			get
			{
				return this._errorStatusCode;
			}
			set
			{
				this._errorStatusCode = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000FDB8 File Offset: 0x0000DFB8
		internal bool HasErrorMessage
		{
			get
			{
				return this._errorMessage != null;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000FDD4 File Offset: 0x0000DFD4
		// (set) Token: 0x06000268 RID: 616 RVA: 0x0000FDEC File Offset: 0x0000DFEC
		internal HttpListener Listener
		{
			get
			{
				return this._listener;
			}
			set
			{
				this._listener = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000FDF8 File Offset: 0x0000DFF8
		public HttpListenerRequest Request
		{
			get
			{
				return this._request;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000FE10 File Offset: 0x0000E010
		public HttpListenerResponse Response
		{
			get
			{
				return this._response;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000FE28 File Offset: 0x0000E028
		// (set) Token: 0x0600026C RID: 620 RVA: 0x0000FE40 File Offset: 0x0000E040
		public IPrincipal User
		{
			get
			{
				return this._user;
			}
			internal set
			{
				this._user = value;
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000FE4C File Offset: 0x0000E04C
		private static string createErrorContent(int statusCode, string statusDescription, string message)
		{
			return (message != null && message.Length > 0) ? string.Format("<html><body><h1>{0} {1} ({2})</h1></body></html>", statusCode, statusDescription, message) : string.Format("<html><body><h1>{0} {1}</h1></body></html>", statusCode, statusDescription);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000FE90 File Offset: 0x0000E090
		internal HttpListenerWebSocketContext GetWebSocketContext(string protocol)
		{
			this._websocketContext = new HttpListenerWebSocketContext(this, protocol);
			return this._websocketContext;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000FEB8 File Offset: 0x0000E0B8
		internal void SendAuthenticationChallenge(AuthenticationSchemes scheme, string realm)
		{
			this._response.StatusCode = 401;
			string value = new AuthenticationChallenge(scheme, realm).ToString();
			this._response.Headers.InternalSet("WWW-Authenticate", value, true);
			this._response.Close();
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000FF08 File Offset: 0x0000E108
		internal void SendError()
		{
			try
			{
				this._response.StatusCode = this._errorStatusCode;
				this._response.ContentType = "text/html";
				string s = HttpListenerContext.createErrorContent(this._errorStatusCode, this._response.StatusDescription, this._errorMessage);
				Encoding utf = Encoding.UTF8;
				byte[] bytes = utf.GetBytes(s);
				this._response.ContentEncoding = utf;
				this._response.ContentLength64 = (long)bytes.Length;
				this._response.Close(bytes, true);
			}
			catch
			{
				this._connection.Close(true);
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000FFB4 File Offset: 0x0000E1B4
		internal void SendError(int statusCode)
		{
			this._errorStatusCode = statusCode;
			this.SendError();
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000FFC5 File Offset: 0x0000E1C5
		internal void SendError(int statusCode, string message)
		{
			this._errorStatusCode = statusCode;
			this._errorMessage = message;
			this.SendError();
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000FFE0 File Offset: 0x0000E1E0
		internal void Unregister()
		{
			bool flag = this._listener == null;
			if (!flag)
			{
				this._listener.UnregisterContext(this);
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0001000C File Offset: 0x0000E20C
		public HttpListenerWebSocketContext AcceptWebSocket(string protocol)
		{
			bool flag = this._websocketContext != null;
			if (flag)
			{
				string message = "The accepting is already in progress.";
				throw new InvalidOperationException(message);
			}
			bool flag2 = protocol != null;
			if (flag2)
			{
				bool flag3 = protocol.Length == 0;
				if (flag3)
				{
					string message2 = "An empty string.";
					throw new ArgumentException(message2, "protocol");
				}
				bool flag4 = !protocol.IsToken();
				if (flag4)
				{
					string message3 = "It contains an invalid character.";
					throw new ArgumentException(message3, "protocol");
				}
			}
			return this.GetWebSocketContext(protocol);
		}

		// Token: 0x040000ED RID: 237
		private HttpConnection _connection;

		// Token: 0x040000EE RID: 238
		private string _errorMessage;

		// Token: 0x040000EF RID: 239
		private int _errorStatusCode;

		// Token: 0x040000F0 RID: 240
		private HttpListener _listener;

		// Token: 0x040000F1 RID: 241
		private HttpListenerRequest _request;

		// Token: 0x040000F2 RID: 242
		private HttpListenerResponse _response;

		// Token: 0x040000F3 RID: 243
		private IPrincipal _user;

		// Token: 0x040000F4 RID: 244
		private HttpListenerWebSocketContext _websocketContext;
	}
}
