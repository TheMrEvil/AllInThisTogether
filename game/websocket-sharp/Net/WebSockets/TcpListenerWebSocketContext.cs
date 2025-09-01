using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading;

namespace WebSocketSharp.Net.WebSockets
{
	// Token: 0x02000043 RID: 67
	internal class TcpListenerWebSocketContext : WebSocketContext
	{
		// Token: 0x06000435 RID: 1077 RVA: 0x00018C94 File Offset: 0x00016E94
		internal TcpListenerWebSocketContext(TcpClient tcpClient, string protocol, bool secure, ServerSslConfiguration sslConfig, Logger log)
		{
			this._tcpClient = tcpClient;
			this._secure = secure;
			this._log = log;
			NetworkStream stream = tcpClient.GetStream();
			if (secure)
			{
				SslStream sslStream = new SslStream(stream, false, sslConfig.ClientCertificateValidationCallback);
				sslStream.AuthenticateAsServer(sslConfig.ServerCertificate, sslConfig.ClientCertificateRequired, sslConfig.EnabledSslProtocols, sslConfig.CheckCertificateRevocation);
				this._stream = sslStream;
			}
			else
			{
				this._stream = stream;
			}
			Socket client = tcpClient.Client;
			this._serverEndPoint = client.LocalEndPoint;
			this._userEndPoint = client.RemoteEndPoint;
			this._request = HttpRequest.Read(this._stream, 90000);
			this._websocket = new WebSocket(this, protocol);
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x00018D54 File Offset: 0x00016F54
		internal Logger Log
		{
			get
			{
				return this._log;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x00018D6C File Offset: 0x00016F6C
		internal Stream Stream
		{
			get
			{
				return this._stream;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x00018D84 File Offset: 0x00016F84
		public override CookieCollection CookieCollection
		{
			get
			{
				return this._request.Cookies;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x00018DA4 File Offset: 0x00016FA4
		public override NameValueCollection Headers
		{
			get
			{
				return this._request.Headers;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x00018DC4 File Offset: 0x00016FC4
		public override string Host
		{
			get
			{
				return this._request.Headers["Host"];
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00018DEC File Offset: 0x00016FEC
		public override bool IsAuthenticated
		{
			get
			{
				return this._user != null;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x00018E08 File Offset: 0x00017008
		public override bool IsLocal
		{
			get
			{
				return this.UserEndPoint.Address.IsLocal();
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x00018E2C File Offset: 0x0001702C
		public override bool IsSecureConnection
		{
			get
			{
				return this._secure;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x00018E44 File Offset: 0x00017044
		public override bool IsWebSocketRequest
		{
			get
			{
				return this._request.IsWebSocketRequest;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x00018E64 File Offset: 0x00017064
		public override string Origin
		{
			get
			{
				return this._request.Headers["Origin"];
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x00018E8C File Offset: 0x0001708C
		public override NameValueCollection QueryString
		{
			get
			{
				bool flag = this._queryString == null;
				if (flag)
				{
					Uri requestUri = this.RequestUri;
					this._queryString = QueryStringCollection.Parse((requestUri != null) ? requestUri.Query : null, Encoding.UTF8);
				}
				return this._queryString;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x00018EDC File Offset: 0x000170DC
		public override Uri RequestUri
		{
			get
			{
				bool flag = this._requestUri == null;
				if (flag)
				{
					this._requestUri = HttpUtility.CreateRequestUrl(this._request.RequestUri, this._request.Headers["Host"], this._request.IsWebSocketRequest, this._secure);
				}
				return this._requestUri;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x00018F44 File Offset: 0x00017144
		public override string SecWebSocketKey
		{
			get
			{
				return this._request.Headers["Sec-WebSocket-Key"];
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x00018F6C File Offset: 0x0001716C
		public override IEnumerable<string> SecWebSocketProtocols
		{
			get
			{
				string val = this._request.Headers["Sec-WebSocket-Protocol"];
				bool flag = val == null || val.Length == 0;
				if (flag)
				{
					yield break;
				}
				foreach (string elm in val.Split(new char[]
				{
					','
				}))
				{
					string protocol = elm.Trim();
					bool flag2 = protocol.Length == 0;
					if (!flag2)
					{
						yield return protocol;
						protocol = null;
						elm = null;
					}
				}
				string[] array = null;
				yield break;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x00018F8C File Offset: 0x0001718C
		public override string SecWebSocketVersion
		{
			get
			{
				return this._request.Headers["Sec-WebSocket-Version"];
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x00018FB4 File Offset: 0x000171B4
		public override IPEndPoint ServerEndPoint
		{
			get
			{
				return (IPEndPoint)this._serverEndPoint;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x00018FD4 File Offset: 0x000171D4
		public override IPrincipal User
		{
			get
			{
				return this._user;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x00018FEC File Offset: 0x000171EC
		public override IPEndPoint UserEndPoint
		{
			get
			{
				return (IPEndPoint)this._userEndPoint;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0001900C File Offset: 0x0001720C
		public override WebSocket WebSocket
		{
			get
			{
				return this._websocket;
			}
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00019024 File Offset: 0x00017224
		private HttpRequest sendAuthenticationChallenge(string challenge)
		{
			HttpResponse httpResponse = HttpResponse.CreateUnauthorizedResponse(challenge);
			byte[] array = httpResponse.ToByteArray();
			this._stream.Write(array, 0, array.Length);
			return HttpRequest.Read(this._stream, 15000);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00019068 File Offset: 0x00017268
		internal bool Authenticate(AuthenticationSchemes scheme, string realm, Func<IIdentity, NetworkCredential> credentialsFinder)
		{
			string chal = new AuthenticationChallenge(scheme, realm).ToString();
			int retry = -1;
			Func<bool> auth = null;
			auth = delegate()
			{
				int retry = retry;
				retry++;
				bool flag = retry > 99;
				bool result;
				if (flag)
				{
					result = false;
				}
				else
				{
					IPrincipal principal = HttpUtility.CreateUser(this._request.Headers["Authorization"], scheme, realm, this._request.HttpMethod, credentialsFinder);
					bool flag2 = principal != null && principal.Identity.IsAuthenticated;
					if (flag2)
					{
						this._user = principal;
						result = true;
					}
					else
					{
						this._request = this.sendAuthenticationChallenge(chal);
						result = auth();
					}
				}
				return result;
			};
			return auth();
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x000190E3 File Offset: 0x000172E3
		internal void Close()
		{
			this._stream.Close();
			this._tcpClient.Close();
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00019100 File Offset: 0x00017300
		internal void Close(HttpStatusCode code)
		{
			HttpResponse httpResponse = HttpResponse.CreateCloseResponse(code);
			byte[] array = httpResponse.ToByteArray();
			this._stream.Write(array, 0, array.Length);
			this._stream.Close();
			this._tcpClient.Close();
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00019148 File Offset: 0x00017348
		public override string ToString()
		{
			return this._request.ToString();
		}

		// Token: 0x04000208 RID: 520
		private Logger _log;

		// Token: 0x04000209 RID: 521
		private NameValueCollection _queryString;

		// Token: 0x0400020A RID: 522
		private HttpRequest _request;

		// Token: 0x0400020B RID: 523
		private Uri _requestUri;

		// Token: 0x0400020C RID: 524
		private bool _secure;

		// Token: 0x0400020D RID: 525
		private EndPoint _serverEndPoint;

		// Token: 0x0400020E RID: 526
		private Stream _stream;

		// Token: 0x0400020F RID: 527
		private TcpClient _tcpClient;

		// Token: 0x04000210 RID: 528
		private IPrincipal _user;

		// Token: 0x04000211 RID: 529
		private EndPoint _userEndPoint;

		// Token: 0x04000212 RID: 530
		private WebSocket _websocket;

		// Token: 0x02000071 RID: 113
		[CompilerGenerated]
		private sealed class <get_SecWebSocketProtocols>d__39 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IDisposable, IEnumerator
		{
			// Token: 0x060005CB RID: 1483 RVA: 0x0001F090 File Offset: 0x0001D290
			[DebuggerHidden]
			public <get_SecWebSocketProtocols>d__39(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x060005CC RID: 1484 RVA: 0x0001DF38 File Offset: 0x0001C138
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060005CD RID: 1485 RVA: 0x0001F0B0 File Offset: 0x0001D2B0
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					protocol = null;
					elm = null;
				}
				else
				{
					this.<>1__state = -1;
					val = this._request.Headers["Sec-WebSocket-Protocol"];
					bool flag = val == null || val.Length == 0;
					if (flag)
					{
						return false;
					}
					array = val.Split(new char[]
					{
						','
					});
					i = 0;
					goto IL_FA;
				}
				IL_EC:
				i++;
				IL_FA:
				if (i >= array.Length)
				{
					array = null;
					return false;
				}
				elm = array[i];
				protocol = elm.Trim();
				bool flag2 = protocol.Length == 0;
				if (flag2)
				{
					goto IL_EC;
				}
				this.<>2__current = protocol;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170001B2 RID: 434
			// (get) Token: 0x060005CE RID: 1486 RVA: 0x0001F1D2 File Offset: 0x0001D3D2
			string IEnumerator<string>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060005CF RID: 1487 RVA: 0x0001E136 File Offset: 0x0001C336
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001B3 RID: 435
			// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0001F1D2 File Offset: 0x0001D3D2
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060005D1 RID: 1489 RVA: 0x0001F1DC File Offset: 0x0001D3DC
			[DebuggerHidden]
			IEnumerator<string> IEnumerable<string>.GetEnumerator()
			{
				TcpListenerWebSocketContext.<get_SecWebSocketProtocols>d__39 <get_SecWebSocketProtocols>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<get_SecWebSocketProtocols>d__ = this;
				}
				else
				{
					<get_SecWebSocketProtocols>d__ = new TcpListenerWebSocketContext.<get_SecWebSocketProtocols>d__39(0);
					<get_SecWebSocketProtocols>d__.<>4__this = this;
				}
				return <get_SecWebSocketProtocols>d__;
			}

			// Token: 0x060005D2 RID: 1490 RVA: 0x0001F224 File Offset: 0x0001D424
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.String>.GetEnumerator();
			}

			// Token: 0x040002E8 RID: 744
			private int <>1__state;

			// Token: 0x040002E9 RID: 745
			private string <>2__current;

			// Token: 0x040002EA RID: 746
			private int <>l__initialThreadId;

			// Token: 0x040002EB RID: 747
			public TcpListenerWebSocketContext <>4__this;

			// Token: 0x040002EC RID: 748
			private string <val>5__1;

			// Token: 0x040002ED RID: 749
			private string[] <>s__2;

			// Token: 0x040002EE RID: 750
			private int <>s__3;

			// Token: 0x040002EF RID: 751
			private string <elm>5__4;

			// Token: 0x040002F0 RID: 752
			private string <protocol>5__5;
		}

		// Token: 0x02000072 RID: 114
		[CompilerGenerated]
		private sealed class <>c__DisplayClass51_0
		{
			// Token: 0x060005D3 RID: 1491 RVA: 0x0001DA3E File Offset: 0x0001BC3E
			public <>c__DisplayClass51_0()
			{
			}

			// Token: 0x060005D4 RID: 1492 RVA: 0x0001F22C File Offset: 0x0001D42C
			internal bool <Authenticate>b__0()
			{
				int num = this.retry;
				this.retry = num + 1;
				bool flag = this.retry > 99;
				bool result;
				if (flag)
				{
					result = false;
				}
				else
				{
					IPrincipal principal = HttpUtility.CreateUser(this.<>4__this._request.Headers["Authorization"], this.scheme, this.realm, this.<>4__this._request.HttpMethod, this.credentialsFinder);
					bool flag2 = principal != null && principal.Identity.IsAuthenticated;
					if (flag2)
					{
						this.<>4__this._user = principal;
						result = true;
					}
					else
					{
						this.<>4__this._request = this.<>4__this.sendAuthenticationChallenge(this.chal);
						result = this.auth();
					}
				}
				return result;
			}

			// Token: 0x040002F1 RID: 753
			public int retry;

			// Token: 0x040002F2 RID: 754
			public TcpListenerWebSocketContext <>4__this;

			// Token: 0x040002F3 RID: 755
			public AuthenticationSchemes scheme;

			// Token: 0x040002F4 RID: 756
			public string realm;

			// Token: 0x040002F5 RID: 757
			public Func<IIdentity, NetworkCredential> credentialsFinder;

			// Token: 0x040002F6 RID: 758
			public string chal;

			// Token: 0x040002F7 RID: 759
			public Func<bool> auth;
		}
	}
}
