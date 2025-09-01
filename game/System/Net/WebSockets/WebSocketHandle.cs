using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebSockets
{
	// Token: 0x020007E3 RID: 2019
	internal sealed class WebSocketHandle
	{
		// Token: 0x06004070 RID: 16496 RVA: 0x000DD530 File Offset: 0x000DB730
		public static WebSocketHandle Create()
		{
			return new WebSocketHandle();
		}

		// Token: 0x06004071 RID: 16497 RVA: 0x000DD537 File Offset: 0x000DB737
		public static bool IsValid(WebSocketHandle handle)
		{
			return handle != null;
		}

		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x06004072 RID: 16498 RVA: 0x000DD540 File Offset: 0x000DB740
		public WebSocketCloseStatus? CloseStatus
		{
			get
			{
				WebSocket webSocket = this._webSocket;
				if (webSocket == null)
				{
					return null;
				}
				return webSocket.CloseStatus;
			}
		}

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x06004073 RID: 16499 RVA: 0x000DD566 File Offset: 0x000DB766
		public string CloseStatusDescription
		{
			get
			{
				WebSocket webSocket = this._webSocket;
				if (webSocket == null)
				{
					return null;
				}
				return webSocket.CloseStatusDescription;
			}
		}

		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x06004074 RID: 16500 RVA: 0x000DD579 File Offset: 0x000DB779
		public WebSocketState State
		{
			get
			{
				WebSocket webSocket = this._webSocket;
				if (webSocket == null)
				{
					return this._state;
				}
				return webSocket.State;
			}
		}

		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x06004075 RID: 16501 RVA: 0x000DD591 File Offset: 0x000DB791
		public string SubProtocol
		{
			get
			{
				WebSocket webSocket = this._webSocket;
				if (webSocket == null)
				{
					return null;
				}
				return webSocket.SubProtocol;
			}
		}

		// Token: 0x06004076 RID: 16502 RVA: 0x00003917 File Offset: 0x00001B17
		public static void CheckPlatformSupport()
		{
		}

		// Token: 0x06004077 RID: 16503 RVA: 0x000DD5A4 File Offset: 0x000DB7A4
		public void Dispose()
		{
			this._state = WebSocketState.Closed;
			WebSocket webSocket = this._webSocket;
			if (webSocket == null)
			{
				return;
			}
			webSocket.Dispose();
		}

		// Token: 0x06004078 RID: 16504 RVA: 0x000DD5BD File Offset: 0x000DB7BD
		public void Abort()
		{
			this._abortSource.Cancel();
			WebSocket webSocket = this._webSocket;
			if (webSocket == null)
			{
				return;
			}
			webSocket.Abort();
		}

		// Token: 0x06004079 RID: 16505 RVA: 0x000DD5DA File Offset: 0x000DB7DA
		public Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
		{
			return this._webSocket.SendAsync(buffer, messageType, endOfMessage, cancellationToken);
		}

		// Token: 0x0600407A RID: 16506 RVA: 0x000DD5EC File Offset: 0x000DB7EC
		public ValueTask SendAsync(ReadOnlyMemory<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
		{
			return this._webSocket.SendAsync(buffer, messageType, endOfMessage, cancellationToken);
		}

		// Token: 0x0600407B RID: 16507 RVA: 0x000DD5FE File Offset: 0x000DB7FE
		public Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
		{
			return this._webSocket.ReceiveAsync(buffer, cancellationToken);
		}

		// Token: 0x0600407C RID: 16508 RVA: 0x000DD60D File Offset: 0x000DB80D
		public ValueTask<ValueWebSocketReceiveResult> ReceiveAsync(Memory<byte> buffer, CancellationToken cancellationToken)
		{
			return this._webSocket.ReceiveAsync(buffer, cancellationToken);
		}

		// Token: 0x0600407D RID: 16509 RVA: 0x000DD61C File Offset: 0x000DB81C
		public Task CloseAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			return this._webSocket.CloseAsync(closeStatus, statusDescription, cancellationToken);
		}

		// Token: 0x0600407E RID: 16510 RVA: 0x000DD62C File Offset: 0x000DB82C
		public Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string statusDescription, CancellationToken cancellationToken)
		{
			return this._webSocket.CloseOutputAsync(closeStatus, statusDescription, cancellationToken);
		}

		// Token: 0x0600407F RID: 16511 RVA: 0x000DD63C File Offset: 0x000DB83C
		public Task ConnectAsyncCore(Uri uri, CancellationToken cancellationToken, ClientWebSocketOptions options)
		{
			WebSocketHandle.<ConnectAsyncCore>d__26 <ConnectAsyncCore>d__;
			<ConnectAsyncCore>d__.<>4__this = this;
			<ConnectAsyncCore>d__.uri = uri;
			<ConnectAsyncCore>d__.cancellationToken = cancellationToken;
			<ConnectAsyncCore>d__.options = options;
			<ConnectAsyncCore>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ConnectAsyncCore>d__.<>1__state = -1;
			<ConnectAsyncCore>d__.<>t__builder.Start<WebSocketHandle.<ConnectAsyncCore>d__26>(ref <ConnectAsyncCore>d__);
			return <ConnectAsyncCore>d__.<>t__builder.Task;
		}

		// Token: 0x06004080 RID: 16512 RVA: 0x000DD698 File Offset: 0x000DB898
		private Task<Socket> ConnectSocketAsync(string host, int port, CancellationToken cancellationToken)
		{
			WebSocketHandle.<ConnectSocketAsync>d__27 <ConnectSocketAsync>d__;
			<ConnectSocketAsync>d__.<>4__this = this;
			<ConnectSocketAsync>d__.host = host;
			<ConnectSocketAsync>d__.port = port;
			<ConnectSocketAsync>d__.cancellationToken = cancellationToken;
			<ConnectSocketAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Socket>.Create();
			<ConnectSocketAsync>d__.<>1__state = -1;
			<ConnectSocketAsync>d__.<>t__builder.Start<WebSocketHandle.<ConnectSocketAsync>d__27>(ref <ConnectSocketAsync>d__);
			return <ConnectSocketAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06004081 RID: 16513 RVA: 0x000DD6F4 File Offset: 0x000DB8F4
		private static byte[] BuildRequestHeader(Uri uri, ClientWebSocketOptions options, string secKey)
		{
			StringBuilder stringBuilder;
			if ((stringBuilder = WebSocketHandle.t_cachedStringBuilder) == null)
			{
				stringBuilder = (WebSocketHandle.t_cachedStringBuilder = new StringBuilder());
			}
			StringBuilder stringBuilder2 = stringBuilder;
			byte[] bytes;
			try
			{
				stringBuilder2.Append("GET ").Append(uri.PathAndQuery).Append(" HTTP/1.1\r\n");
				string value = options.RequestHeaders["Host"];
				stringBuilder2.Append("Host: ");
				if (string.IsNullOrEmpty(value))
				{
					stringBuilder2.Append(uri.IdnHost).Append(':').Append(uri.Port).Append("\r\n");
				}
				else
				{
					stringBuilder2.Append(value).Append("\r\n");
				}
				stringBuilder2.Append("Connection: Upgrade\r\n");
				stringBuilder2.Append("Upgrade: websocket\r\n");
				stringBuilder2.Append("Sec-WebSocket-Version: 13\r\n");
				stringBuilder2.Append("Sec-WebSocket-Key: ").Append(secKey).Append("\r\n");
				foreach (string text in options.RequestHeaders.AllKeys)
				{
					if (!string.Equals(text, "Host", StringComparison.OrdinalIgnoreCase))
					{
						stringBuilder2.Append(text).Append(": ").Append(options.RequestHeaders[text]).Append("\r\n");
					}
				}
				if (options.RequestedSubProtocols.Count > 0)
				{
					stringBuilder2.Append("Sec-WebSocket-Protocol").Append(": ");
					stringBuilder2.Append(options.RequestedSubProtocols[0]);
					for (int j = 1; j < options.RequestedSubProtocols.Count; j++)
					{
						stringBuilder2.Append(", ").Append(options.RequestedSubProtocols[j]);
					}
					stringBuilder2.Append("\r\n");
				}
				if (options.Cookies != null)
				{
					string cookieHeader = options.Cookies.GetCookieHeader(uri);
					if (!string.IsNullOrWhiteSpace(cookieHeader))
					{
						stringBuilder2.Append("Cookie").Append(": ").Append(cookieHeader).Append("\r\n");
					}
				}
				stringBuilder2.Append("\r\n");
				bytes = WebSocketHandle.s_defaultHttpEncoding.GetBytes(stringBuilder2.ToString());
			}
			finally
			{
				stringBuilder2.Clear();
			}
			return bytes;
		}

		// Token: 0x06004082 RID: 16514 RVA: 0x000DD940 File Offset: 0x000DBB40
		private static KeyValuePair<string, string> CreateSecKeyAndSecWebSocketAccept()
		{
			string text = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
			KeyValuePair<string, string> result;
			using (SHA1 sha = SHA1.Create())
			{
				result = new KeyValuePair<string, string>(text, Convert.ToBase64String(sha.ComputeHash(Encoding.ASCII.GetBytes(text + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"))));
			}
			return result;
		}

		// Token: 0x06004083 RID: 16515 RVA: 0x000DD9AC File Offset: 0x000DBBAC
		private Task<string> ParseAndValidateConnectResponseAsync(Stream stream, ClientWebSocketOptions options, string expectedSecWebSocketAccept, CancellationToken cancellationToken)
		{
			WebSocketHandle.<ParseAndValidateConnectResponseAsync>d__30 <ParseAndValidateConnectResponseAsync>d__;
			<ParseAndValidateConnectResponseAsync>d__.stream = stream;
			<ParseAndValidateConnectResponseAsync>d__.options = options;
			<ParseAndValidateConnectResponseAsync>d__.expectedSecWebSocketAccept = expectedSecWebSocketAccept;
			<ParseAndValidateConnectResponseAsync>d__.cancellationToken = cancellationToken;
			<ParseAndValidateConnectResponseAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<ParseAndValidateConnectResponseAsync>d__.<>1__state = -1;
			<ParseAndValidateConnectResponseAsync>d__.<>t__builder.Start<WebSocketHandle.<ParseAndValidateConnectResponseAsync>d__30>(ref <ParseAndValidateConnectResponseAsync>d__);
			return <ParseAndValidateConnectResponseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06004084 RID: 16516 RVA: 0x000DDA08 File Offset: 0x000DBC08
		private static void ValidateAndTrackHeader(string targetHeaderName, string targetHeaderValue, string foundHeaderName, string foundHeaderValue, ref bool foundHeader)
		{
			bool flag = string.Equals(targetHeaderName, foundHeaderName, StringComparison.OrdinalIgnoreCase);
			if (!foundHeader)
			{
				if (flag)
				{
					if (!string.Equals(targetHeaderValue, foundHeaderValue, StringComparison.OrdinalIgnoreCase))
					{
						throw new WebSocketException(SR.Format("The '{0}' header value '{1}' is invalid.", targetHeaderName, foundHeaderValue));
					}
					foundHeader = true;
					return;
				}
			}
			else if (flag)
			{
				throw new WebSocketException(SR.Format("Unable to connect to the remote server", Array.Empty<object>()));
			}
		}

		// Token: 0x06004085 RID: 16517 RVA: 0x000DDA60 File Offset: 0x000DBC60
		private static Task<string> ReadResponseHeaderLineAsync(Stream stream, CancellationToken cancellationToken)
		{
			WebSocketHandle.<ReadResponseHeaderLineAsync>d__32 <ReadResponseHeaderLineAsync>d__;
			<ReadResponseHeaderLineAsync>d__.stream = stream;
			<ReadResponseHeaderLineAsync>d__.cancellationToken = cancellationToken;
			<ReadResponseHeaderLineAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<ReadResponseHeaderLineAsync>d__.<>1__state = -1;
			<ReadResponseHeaderLineAsync>d__.<>t__builder.Start<WebSocketHandle.<ReadResponseHeaderLineAsync>d__32>(ref <ReadResponseHeaderLineAsync>d__);
			return <ReadResponseHeaderLineAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06004086 RID: 16518 RVA: 0x000DDAAB File Offset: 0x000DBCAB
		public WebSocketHandle()
		{
		}

		// Token: 0x06004087 RID: 16519 RVA: 0x000DDAC5 File Offset: 0x000DBCC5
		// Note: this type is marked as 'beforefieldinit'.
		static WebSocketHandle()
		{
		}

		// Token: 0x040026EA RID: 9962
		[ThreadStatic]
		private static StringBuilder t_cachedStringBuilder;

		// Token: 0x040026EB RID: 9963
		private static readonly Encoding s_defaultHttpEncoding = Encoding.GetEncoding(28591);

		// Token: 0x040026EC RID: 9964
		private const int DefaultReceiveBufferSize = 4096;

		// Token: 0x040026ED RID: 9965
		private const string WSServerGuid = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";

		// Token: 0x040026EE RID: 9966
		private readonly CancellationTokenSource _abortSource = new CancellationTokenSource();

		// Token: 0x040026EF RID: 9967
		private WebSocketState _state = WebSocketState.Connecting;

		// Token: 0x040026F0 RID: 9968
		private WebSocket _webSocket;

		// Token: 0x020007E4 RID: 2020
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004088 RID: 16520 RVA: 0x000DDAD6 File Offset: 0x000DBCD6
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004089 RID: 16521 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c()
			{
			}

			// Token: 0x0600408A RID: 16522 RVA: 0x000DDAE2 File Offset: 0x000DBCE2
			internal void <ConnectAsyncCore>b__26_0(object s)
			{
				((WebSocketHandle)s).Abort();
			}

			// Token: 0x0600408B RID: 16523 RVA: 0x000DDAEF File Offset: 0x000DBCEF
			internal void <ConnectSocketAsync>b__27_0(object s)
			{
				((Socket)s).Dispose();
			}

			// Token: 0x0600408C RID: 16524 RVA: 0x000DDAEF File Offset: 0x000DBCEF
			internal void <ConnectSocketAsync>b__27_1(object s)
			{
				((Socket)s).Dispose();
			}

			// Token: 0x040026F1 RID: 9969
			public static readonly WebSocketHandle.<>c <>9 = new WebSocketHandle.<>c();

			// Token: 0x040026F2 RID: 9970
			public static Action<object> <>9__26_0;

			// Token: 0x040026F3 RID: 9971
			public static Action<object> <>9__27_0;

			// Token: 0x040026F4 RID: 9972
			public static Action<object> <>9__27_1;
		}

		// Token: 0x020007E5 RID: 2021
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ConnectAsyncCore>d__26 : IAsyncStateMachine
		{
			// Token: 0x0600408D RID: 16525 RVA: 0x000DDAFC File Offset: 0x000DBCFC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				WebSocketHandle webSocketHandle = this.<>4__this;
				try
				{
					if (num > 3)
					{
						this.<registration>5__2 = this.cancellationToken.Register(new Action<object>(WebSocketHandle.<>c.<>9.<ConnectAsyncCore>b__26_0), webSocketHandle);
					}
					try
					{
						ConfiguredTaskAwaitable<Socket>.ConfiguredTaskAwaiter awaiter;
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
						ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter awaiter3;
						switch (num)
						{
						case 0:
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable<Socket>.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
							break;
						case 1:
							awaiter2 = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_199;
						case 2:
							awaiter2 = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_24C;
						case 3:
							awaiter3 = this.<>u__3;
							this.<>u__3 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_2D1;
						default:
							awaiter = webSocketHandle.ConnectSocketAsync(this.uri.Host, this.uri.Port, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Socket>.ConfiguredTaskAwaiter, WebSocketHandle.<ConnectAsyncCore>d__26>(ref awaiter, ref this);
								return;
							}
							break;
						}
						Socket result = awaiter.GetResult();
						this.<stream>5__3 = new NetworkStream(result, true);
						if (!(this.uri.Scheme == "wss"))
						{
							goto IL_1B3;
						}
						this.<sslStream>5__5 = new SslStream(this.<stream>5__3);
						awaiter2 = this.<sslStream>5__5.AuthenticateAsClientAsync(this.uri.Host, this.options.ClientCertificates, SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12, false).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (this.<>1__state = 1);
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, WebSocketHandle.<ConnectAsyncCore>d__26>(ref awaiter2, ref this);
							return;
						}
						IL_199:
						awaiter2.GetResult();
						this.<stream>5__3 = this.<sslStream>5__5;
						this.<sslStream>5__5 = null;
						IL_1B3:
						this.<secKeyAndSecWebSocketAccept>5__4 = WebSocketHandle.CreateSecKeyAndSecWebSocketAccept();
						byte[] array = WebSocketHandle.BuildRequestHeader(this.uri, this.options, this.<secKeyAndSecWebSocketAccept>5__4.Key);
						awaiter2 = this.<stream>5__3.WriteAsync(array, 0, array.Length, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (this.<>1__state = 2);
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, WebSocketHandle.<ConnectAsyncCore>d__26>(ref awaiter2, ref this);
							return;
						}
						IL_24C:
						awaiter2.GetResult();
						awaiter3 = webSocketHandle.ParseAndValidateConnectResponseAsync(this.<stream>5__3, this.options, this.<secKeyAndSecWebSocketAccept>5__4.Value, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (this.<>1__state = 3);
							this.<>u__3 = awaiter3;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter, WebSocketHandle.<ConnectAsyncCore>d__26>(ref awaiter3, ref this);
							return;
						}
						IL_2D1:
						string result2 = awaiter3.GetResult();
						webSocketHandle._webSocket = WebSocket.CreateClientWebSocket(this.<stream>5__3, result2, this.options.ReceiveBufferSize, this.options.SendBufferSize, this.options.KeepAliveInterval, false, this.options.Buffer.GetValueOrDefault());
						if (webSocketHandle._state == WebSocketState.Aborted)
						{
							webSocketHandle._webSocket.Abort();
						}
						else if (webSocketHandle._state == WebSocketState.Closed)
						{
							webSocketHandle._webSocket.Dispose();
						}
						this.<stream>5__3 = null;
						this.<secKeyAndSecWebSocketAccept>5__4 = default(KeyValuePair<string, string>);
					}
					catch (Exception ex)
					{
						if (webSocketHandle._state < WebSocketState.Closed)
						{
							webSocketHandle._state = WebSocketState.Closed;
						}
						webSocketHandle.Abort();
						if (ex is WebSocketException)
						{
							throw;
						}
						throw new WebSocketException("Unable to connect to the remote server", ex);
					}
					finally
					{
						if (num < 0)
						{
							this.<registration>5__2.Dispose();
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<registration>5__2 = default(CancellationTokenRegistration);
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<registration>5__2 = default(CancellationTokenRegistration);
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600408E RID: 16526 RVA: 0x000DDF40 File Offset: 0x000DC140
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040026F5 RID: 9973
			public int <>1__state;

			// Token: 0x040026F6 RID: 9974
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040026F7 RID: 9975
			public CancellationToken cancellationToken;

			// Token: 0x040026F8 RID: 9976
			public WebSocketHandle <>4__this;

			// Token: 0x040026F9 RID: 9977
			public Uri uri;

			// Token: 0x040026FA RID: 9978
			public ClientWebSocketOptions options;

			// Token: 0x040026FB RID: 9979
			private CancellationTokenRegistration <registration>5__2;

			// Token: 0x040026FC RID: 9980
			private Stream <stream>5__3;

			// Token: 0x040026FD RID: 9981
			private KeyValuePair<string, string> <secKeyAndSecWebSocketAccept>5__4;

			// Token: 0x040026FE RID: 9982
			private ConfiguredTaskAwaitable<Socket>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x040026FF RID: 9983
			private SslStream <sslStream>5__5;

			// Token: 0x04002700 RID: 9984
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;

			// Token: 0x04002701 RID: 9985
			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter <>u__3;
		}

		// Token: 0x020007E6 RID: 2022
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ConnectSocketAsync>d__27 : IAsyncStateMachine
		{
			// Token: 0x0600408F RID: 16527 RVA: 0x000DDF50 File Offset: 0x000DC150
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				WebSocketHandle webSocketHandle = this.<>4__this;
				Socket result2;
				try
				{
					ConfiguredTaskAwaitable<IPAddress[]>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							goto IL_BF;
						}
						awaiter = Dns.GetHostAddressesAsync(this.host).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 0);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<IPAddress[]>.ConfiguredTaskAwaiter, WebSocketHandle.<ConnectSocketAsync>d__27>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<IPAddress[]>.ConfiguredTaskAwaiter);
						num = (this.<>1__state = -1);
					}
					IPAddress[] result = awaiter.GetResult();
					ExceptionDispatchInfo exceptionDispatchInfo = null;
					this.<>7__wrap1 = result;
					this.<>7__wrap2 = 0;
					goto IL_29B;
					IL_BF:
					IPAddress ipaddress;
					try
					{
						if (num != 1)
						{
							this.<>7__wrap4 = this.cancellationToken.Register(new Action<object>(WebSocketHandle.<>c.<>9.<ConnectSocketAsync>b__27_0), this.<socket>5__4);
						}
						CancellationToken token;
						try
						{
							if (num != 1)
							{
								token = webSocketHandle._abortSource.Token;
								this.<>7__wrap5 = token.Register(new Action<object>(WebSocketHandle.<>c.<>9.<ConnectSocketAsync>b__27_1), this.<socket>5__4);
							}
							try
							{
								ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
								if (num != 1)
								{
									awaiter2 = this.<socket>5__4.ConnectAsync(ipaddress, this.port).ConfigureAwait(false).GetAwaiter();
									if (!awaiter2.IsCompleted)
									{
										num = (this.<>1__state = 1);
										this.<>u__2 = awaiter2;
										this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, WebSocketHandle.<ConnectSocketAsync>d__27>(ref awaiter2, ref this);
										return;
									}
								}
								else
								{
									awaiter2 = this.<>u__2;
									this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
									num = (this.<>1__state = -1);
								}
								awaiter2.GetResult();
							}
							catch (ObjectDisposedException innerException)
							{
								CancellationToken token2 = this.cancellationToken.IsCancellationRequested ? this.cancellationToken : webSocketHandle._abortSource.Token;
								if (token2.IsCancellationRequested)
								{
									throw new OperationCanceledException(new OperationCanceledException().Message, innerException, token2);
								}
							}
							finally
							{
								if (num < 0)
								{
									((IDisposable)this.<>7__wrap5).Dispose();
								}
							}
							this.<>7__wrap5 = default(CancellationTokenRegistration);
						}
						finally
						{
							if (num < 0)
							{
								((IDisposable)this.<>7__wrap4).Dispose();
							}
						}
						this.<>7__wrap4 = default(CancellationTokenRegistration);
						this.cancellationToken.ThrowIfCancellationRequested();
						token = webSocketHandle._abortSource.Token;
						token.ThrowIfCancellationRequested();
						result2 = this.<socket>5__4;
						goto IL_2E4;
					}
					catch (Exception source)
					{
						this.<socket>5__4.Dispose();
						exceptionDispatchInfo = ExceptionDispatchInfo.Capture(source);
					}
					this.<socket>5__4 = null;
					this.<>7__wrap2++;
					IL_29B:
					if (this.<>7__wrap2 >= this.<>7__wrap1.Length)
					{
						this.<>7__wrap1 = null;
						if (exceptionDispatchInfo != null)
						{
							exceptionDispatchInfo.Throw();
						}
						throw new WebSocketException("Unable to connect to the remote server");
					}
					ipaddress = this.<>7__wrap1[this.<>7__wrap2];
					this.<socket>5__4 = new Socket(ipaddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
					goto IL_BF;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_2E4:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x06004090 RID: 16528 RVA: 0x000DE2D4 File Offset: 0x000DC4D4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04002702 RID: 9986
			public int <>1__state;

			// Token: 0x04002703 RID: 9987
			public AsyncTaskMethodBuilder<Socket> <>t__builder;

			// Token: 0x04002704 RID: 9988
			public string host;

			// Token: 0x04002705 RID: 9989
			public CancellationToken cancellationToken;

			// Token: 0x04002706 RID: 9990
			public WebSocketHandle <>4__this;

			// Token: 0x04002707 RID: 9991
			public int port;

			// Token: 0x04002708 RID: 9992
			private ConfiguredTaskAwaitable<IPAddress[]>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04002709 RID: 9993
			private IPAddress[] <>7__wrap1;

			// Token: 0x0400270A RID: 9994
			private int <>7__wrap2;

			// Token: 0x0400270B RID: 9995
			private Socket <socket>5__4;

			// Token: 0x0400270C RID: 9996
			private CancellationTokenRegistration <>7__wrap4;

			// Token: 0x0400270D RID: 9997
			private CancellationTokenRegistration <>7__wrap5;

			// Token: 0x0400270E RID: 9998
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020007E7 RID: 2023
		[CompilerGenerated]
		private sealed class <>c__DisplayClass30_0
		{
			// Token: 0x06004091 RID: 16529 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass30_0()
			{
			}

			// Token: 0x06004092 RID: 16530 RVA: 0x000DE2E2 File Offset: 0x000DC4E2
			internal bool <ParseAndValidateConnectResponseAsync>b__0(string requested)
			{
				return string.Equals(requested, this.headerValue, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x0400270F RID: 9999
			public string headerValue;
		}

		// Token: 0x020007E8 RID: 2024
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseAndValidateConnectResponseAsync>d__30 : IAsyncStateMachine
		{
			// Token: 0x06004093 RID: 16531 RVA: 0x000DE2F4 File Offset: 0x000DC4F4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				string result3;
				try
				{
					ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_2A8;
						}
						awaiter = WebSocketHandle.ReadResponseHeaderLineAsync(this.stream, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter, WebSocketHandle.<ParseAndValidateConnectResponseAsync>d__30>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					string result = awaiter.GetResult();
					if (string.IsNullOrEmpty(result))
					{
						throw new WebSocketException(SR.Format("Unable to connect to the remote server", Array.Empty<object>()));
					}
					if (!result.StartsWith("HTTP/1.1 ", StringComparison.Ordinal) || result.Length < "HTTP/1.1 101".Length)
					{
						throw new WebSocketException(WebSocketError.HeaderError);
					}
					if (!result.StartsWith("HTTP/1.1 101", StringComparison.Ordinal) || (result.Length > "HTTP/1.1 101".Length && !char.IsWhiteSpace(result["HTTP/1.1 101".Length])))
					{
						throw new WebSocketException("Unable to connect to the remote server");
					}
					this.<foundUpgrade>5__2 = false;
					this.<foundConnection>5__3 = false;
					this.<foundSecWebSocketAccept>5__4 = false;
					this.<subprotocol>5__5 = null;
					IL_23C:
					awaiter = WebSocketHandle.ReadResponseHeaderLineAsync(this.stream, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter, WebSocketHandle.<ParseAndValidateConnectResponseAsync>d__30>(ref awaiter, ref this);
						return;
					}
					IL_2A8:
					string result2;
					if (string.IsNullOrEmpty(result2 = awaiter.GetResult()))
					{
						if (!this.<foundUpgrade>5__2 || !this.<foundConnection>5__3 || !this.<foundSecWebSocketAccept>5__4)
						{
							throw new WebSocketException("Unable to connect to the remote server");
						}
						result3 = this.<subprotocol>5__5;
					}
					else
					{
						WebSocketHandle.<>c__DisplayClass30_0 CS$<>8__locals1 = new WebSocketHandle.<>c__DisplayClass30_0();
						int num2 = result2.IndexOf(':');
						if (num2 == -1)
						{
							throw new WebSocketException(WebSocketError.HeaderError);
						}
						string text = result2.SubstringTrim(0, num2);
						CS$<>8__locals1.headerValue = result2.SubstringTrim(num2 + 1);
						WebSocketHandle.ValidateAndTrackHeader("Connection", "Upgrade", text, CS$<>8__locals1.headerValue, ref this.<foundConnection>5__3);
						WebSocketHandle.ValidateAndTrackHeader("Upgrade", "websocket", text, CS$<>8__locals1.headerValue, ref this.<foundUpgrade>5__2);
						WebSocketHandle.ValidateAndTrackHeader("Sec-WebSocket-Accept", this.expectedSecWebSocketAccept, text, CS$<>8__locals1.headerValue, ref this.<foundSecWebSocketAccept>5__4);
						if (!string.Equals("Sec-WebSocket-Protocol", text, StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(CS$<>8__locals1.headerValue))
						{
							goto IL_23C;
						}
						string text2 = this.options.RequestedSubProtocols.Find(new Predicate<string>(CS$<>8__locals1.<ParseAndValidateConnectResponseAsync>b__0));
						if (text2 == null || this.<subprotocol>5__5 != null)
						{
							throw new WebSocketException(WebSocketError.UnsupportedProtocol, SR.Format("The WebSocket client request requested '{0}' protocol(s), but server is only accepting '{1}' protocol(s).", string.Join(", ", this.options.RequestedSubProtocols), this.<subprotocol>5__5));
						}
						this.<subprotocol>5__5 = text2;
						goto IL_23C;
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<subprotocol>5__5 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<subprotocol>5__5 = null;
				this.<>t__builder.SetResult(result3);
			}

			// Token: 0x06004094 RID: 16532 RVA: 0x000DE640 File Offset: 0x000DC840
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04002710 RID: 10000
			public int <>1__state;

			// Token: 0x04002711 RID: 10001
			public AsyncTaskMethodBuilder<string> <>t__builder;

			// Token: 0x04002712 RID: 10002
			public Stream stream;

			// Token: 0x04002713 RID: 10003
			public CancellationToken cancellationToken;

			// Token: 0x04002714 RID: 10004
			public string expectedSecWebSocketAccept;

			// Token: 0x04002715 RID: 10005
			public ClientWebSocketOptions options;

			// Token: 0x04002716 RID: 10006
			private bool <foundUpgrade>5__2;

			// Token: 0x04002717 RID: 10007
			private bool <foundConnection>5__3;

			// Token: 0x04002718 RID: 10008
			private bool <foundSecWebSocketAccept>5__4;

			// Token: 0x04002719 RID: 10009
			private string <subprotocol>5__5;

			// Token: 0x0400271A RID: 10010
			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020007E9 RID: 2025
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadResponseHeaderLineAsync>d__32 : IAsyncStateMachine
		{
			// Token: 0x06004095 RID: 16533 RVA: 0x000DE650 File Offset: 0x000DC850
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				string result;
				try
				{
					if (num != 0)
					{
						this.<sb>5__2 = WebSocketHandle.t_cachedStringBuilder;
						if (this.<sb>5__2 != null)
						{
							WebSocketHandle.t_cachedStringBuilder = null;
						}
						else
						{
							this.<sb>5__2 = new StringBuilder();
						}
						this.<arr>5__3 = new byte[1];
						this.<prevChar>5__4 = '\0';
					}
					try
					{
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
						if (num == 0)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_EC;
						}
						IL_7B:
						awaiter = this.stream.ReadAsync(this.<arr>5__3, 0, 1, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 0);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, WebSocketHandle.<ReadResponseHeaderLineAsync>d__32>(ref awaiter, ref this);
							return;
						}
						IL_EC:
						if (awaiter.GetResult() == 1)
						{
							char c = (char)this.<arr>5__3[0];
							if (this.<prevChar>5__4 != '\r' || c != '\n')
							{
								this.<sb>5__2.Append(c);
								this.<prevChar>5__4 = c;
								goto IL_7B;
							}
						}
						if (this.<sb>5__2.Length > 0 && this.<sb>5__2[this.<sb>5__2.Length - 1] == '\r')
						{
							this.<sb>5__2.Length = this.<sb>5__2.Length - 1;
						}
						result = this.<sb>5__2.ToString();
					}
					finally
					{
						if (num < 0)
						{
							this.<sb>5__2.Clear();
							WebSocketHandle.t_cachedStringBuilder = this.<sb>5__2;
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<sb>5__2 = null;
					this.<arr>5__3 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<sb>5__2 = null;
				this.<arr>5__3 = null;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06004096 RID: 16534 RVA: 0x000DE840 File Offset: 0x000DCA40
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400271B RID: 10011
			public int <>1__state;

			// Token: 0x0400271C RID: 10012
			public AsyncTaskMethodBuilder<string> <>t__builder;

			// Token: 0x0400271D RID: 10013
			public Stream stream;

			// Token: 0x0400271E RID: 10014
			public CancellationToken cancellationToken;

			// Token: 0x0400271F RID: 10015
			private StringBuilder <sb>5__2;

			// Token: 0x04002720 RID: 10016
			private byte[] <arr>5__3;

			// Token: 0x04002721 RID: 10017
			private char <prevChar>5__4;

			// Token: 0x04002722 RID: 10018
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
