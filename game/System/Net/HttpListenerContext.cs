using System;
using System.Net.WebSockets;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace System.Net
{
	/// <summary>Provides access to the request and response objects used by the <see cref="T:System.Net.HttpListener" /> class. This class cannot be inherited.</summary>
	// Token: 0x0200068B RID: 1675
	public sealed class HttpListenerContext
	{
		// Token: 0x060034DE RID: 13534 RVA: 0x000B8F6A File Offset: 0x000B716A
		internal HttpListenerContext(HttpConnection cnc)
		{
			this.err_status = 400;
			base..ctor();
			this.cnc = cnc;
			this.request = new HttpListenerRequest(this);
			this.response = new HttpListenerResponse(this);
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x060034DF RID: 13535 RVA: 0x000B8F9C File Offset: 0x000B719C
		// (set) Token: 0x060034E0 RID: 13536 RVA: 0x000B8FA4 File Offset: 0x000B71A4
		internal int ErrorStatus
		{
			get
			{
				return this.err_status;
			}
			set
			{
				this.err_status = value;
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x060034E1 RID: 13537 RVA: 0x000B8FAD File Offset: 0x000B71AD
		// (set) Token: 0x060034E2 RID: 13538 RVA: 0x000B8FB5 File Offset: 0x000B71B5
		internal string ErrorMessage
		{
			get
			{
				return this.error;
			}
			set
			{
				this.error = value;
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x060034E3 RID: 13539 RVA: 0x000B8FBE File Offset: 0x000B71BE
		internal bool HaveError
		{
			get
			{
				return this.error != null;
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x060034E4 RID: 13540 RVA: 0x000B8FC9 File Offset: 0x000B71C9
		internal HttpConnection Connection
		{
			get
			{
				return this.cnc;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.HttpListenerRequest" /> that represents a client's request for a resource.</summary>
		/// <returns>An <see cref="T:System.Net.HttpListenerRequest" /> object that represents the client request.</returns>
		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x060034E5 RID: 13541 RVA: 0x000B8FD1 File Offset: 0x000B71D1
		public HttpListenerRequest Request
		{
			get
			{
				return this.request;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.HttpListenerResponse" /> object that will be sent to the client in response to the client's request.</summary>
		/// <returns>An <see cref="T:System.Net.HttpListenerResponse" /> object used to send a response back to the client.</returns>
		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x060034E6 RID: 13542 RVA: 0x000B8FD9 File Offset: 0x000B71D9
		public HttpListenerResponse Response
		{
			get
			{
				return this.response;
			}
		}

		/// <summary>Gets an object used to obtain identity, authentication information, and security roles for the client whose request is represented by this <see cref="T:System.Net.HttpListenerContext" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Principal.IPrincipal" /> object that describes the client, or <see langword="null" /> if the <see cref="T:System.Net.HttpListener" /> that supplied this <see cref="T:System.Net.HttpListenerContext" /> does not require authentication.</returns>
		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x060034E7 RID: 13543 RVA: 0x000B8FE1 File Offset: 0x000B71E1
		public IPrincipal User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x060034E8 RID: 13544 RVA: 0x000B8FEC File Offset: 0x000B71EC
		internal void ParseAuthentication(AuthenticationSchemes expectedSchemes)
		{
			if (expectedSchemes == AuthenticationSchemes.Anonymous)
			{
				return;
			}
			string text = this.request.Headers["Authorization"];
			if (text == null || text.Length < 2)
			{
				return;
			}
			string[] array = text.Split(new char[]
			{
				' '
			}, 2);
			if (string.Compare(array[0], "basic", true) == 0)
			{
				this.user = this.ParseBasicAuthentication(array[1]);
			}
		}

		// Token: 0x060034E9 RID: 13545 RVA: 0x000B9058 File Offset: 0x000B7258
		internal IPrincipal ParseBasicAuthentication(string authData)
		{
			IPrincipal result;
			try
			{
				string text = Encoding.Default.GetString(Convert.FromBase64String(authData));
				int num = text.IndexOf(':');
				string password = text.Substring(num + 1);
				text = text.Substring(0, num);
				num = text.IndexOf('\\');
				string username;
				if (num > 0)
				{
					username = text.Substring(num);
				}
				else
				{
					username = text;
				}
				result = new GenericPrincipal(new HttpListenerBasicIdentity(username, password), new string[0]);
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		/// <summary>Accept a WebSocket connection as an asynchronous operation.</summary>
		/// <param name="subProtocol">The supported WebSocket sub-protocol.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an <see cref="T:System.Net.WebSockets.HttpListenerWebSocketContext" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="subProtocol" /> is an empty string  
		/// -or-  
		/// <paramref name="subProtocol" /> contains illegal characters.</exception>
		/// <exception cref="T:System.Net.WebSockets.WebSocketException">An error occurred when sending the response to complete the WebSocket handshake.</exception>
		// Token: 0x060034EA RID: 13546 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public Task<HttpListenerWebSocketContext> AcceptWebSocketAsync(string subProtocol)
		{
			throw new NotImplementedException();
		}

		/// <summary>Accept a WebSocket connection specifying the supported WebSocket sub-protocol  and WebSocket keep-alive interval as an asynchronous operation.</summary>
		/// <param name="subProtocol">The supported WebSocket sub-protocol.</param>
		/// <param name="keepAliveInterval">The WebSocket protocol keep-alive interval in milliseconds.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an <see cref="T:System.Net.WebSockets.HttpListenerWebSocketContext" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="subProtocol" /> is an empty string  
		/// -or-  
		/// <paramref name="subProtocol" /> contains illegal characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="keepAliveInterval" /> is too small.</exception>
		/// <exception cref="T:System.Net.WebSockets.WebSocketException">An error occurred when sending the response to complete the WebSocket handshake.</exception>
		// Token: 0x060034EB RID: 13547 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public Task<HttpListenerWebSocketContext> AcceptWebSocketAsync(string subProtocol, TimeSpan keepAliveInterval)
		{
			throw new NotImplementedException();
		}

		/// <summary>Accept a WebSocket connection specifying the supported WebSocket sub-protocol, receive buffer size, and WebSocket keep-alive interval as an asynchronous operation.</summary>
		/// <param name="subProtocol">The supported WebSocket sub-protocol.</param>
		/// <param name="receiveBufferSize">The receive buffer size in bytes.</param>
		/// <param name="keepAliveInterval">The WebSocket protocol keep-alive interval in milliseconds.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an <see cref="T:System.Net.WebSockets.HttpListenerWebSocketContext" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="subProtocol" /> is an empty string  
		/// -or-  
		/// <paramref name="subProtocol" /> contains illegal characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="keepAliveInterval" /> is too small.  
		/// -or-  
		/// <paramref name="receiveBufferSize" /> is less than 16 bytes  
		/// -or-  
		/// <paramref name="receiveBufferSize" /> is greater than 64K bytes.</exception>
		/// <exception cref="T:System.Net.WebSockets.WebSocketException">An error occurred when sending the response to complete the WebSocket handshake.</exception>
		// Token: 0x060034EC RID: 13548 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public Task<HttpListenerWebSocketContext> AcceptWebSocketAsync(string subProtocol, int receiveBufferSize, TimeSpan keepAliveInterval)
		{
			throw new NotImplementedException();
		}

		/// <summary>Accept a WebSocket connection specifying the supported WebSocket sub-protocol, receive buffer size, WebSocket keep-alive interval, and the internal buffer as an asynchronous operation.</summary>
		/// <param name="subProtocol">The supported WebSocket sub-protocol.</param>
		/// <param name="receiveBufferSize">The receive buffer size in bytes.</param>
		/// <param name="keepAliveInterval">The WebSocket protocol keep-alive interval in milliseconds.</param>
		/// <param name="internalBuffer">An internal buffer to use for this operation.</param>
		/// <returns>The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an <see cref="T:System.Net.WebSockets.HttpListenerWebSocketContext" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="subProtocol" /> is an empty string  
		/// -or-  
		/// <paramref name="subProtocol" /> contains illegal characters.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="keepAliveInterval" /> is too small.  
		/// -or-  
		/// <paramref name="receiveBufferSize" /> is less than 16 bytes  
		/// -or-  
		/// <paramref name="receiveBufferSize" /> is greater than 64K bytes.</exception>
		/// <exception cref="T:System.Net.WebSockets.WebSocketException">An error occurred when sending the response to complete the WebSocket handshake.</exception>
		// Token: 0x060034ED RID: 13549 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public Task<HttpListenerWebSocketContext> AcceptWebSocketAsync(string subProtocol, int receiveBufferSize, TimeSpan keepAliveInterval, ArraySegment<byte> internalBuffer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060034EE RID: 13550 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal HttpListenerContext()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001EE0 RID: 7904
		private HttpListenerRequest request;

		// Token: 0x04001EE1 RID: 7905
		private HttpListenerResponse response;

		// Token: 0x04001EE2 RID: 7906
		private IPrincipal user;

		// Token: 0x04001EE3 RID: 7907
		private HttpConnection cnc;

		// Token: 0x04001EE4 RID: 7908
		private string error;

		// Token: 0x04001EE5 RID: 7909
		private int err_status;

		// Token: 0x04001EE6 RID: 7910
		internal HttpListener Listener;
	}
}
