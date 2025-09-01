using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading;

namespace WebSocketSharp.Net.WebSockets
{
	// Token: 0x02000042 RID: 66
	public class HttpListenerWebSocketContext : WebSocketContext
	{
		// Token: 0x0600041E RID: 1054 RVA: 0x00018964 File Offset: 0x00016B64
		internal HttpListenerWebSocketContext(HttpListenerContext context, string protocol)
		{
			this._context = context;
			this._websocket = new WebSocket(this, protocol);
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x00018984 File Offset: 0x00016B84
		internal Logger Log
		{
			get
			{
				return this._context.Listener.Log;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x000189A8 File Offset: 0x00016BA8
		internal Stream Stream
		{
			get
			{
				return this._context.Connection.Stream;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x000189CC File Offset: 0x00016BCC
		public override CookieCollection CookieCollection
		{
			get
			{
				return this._context.Request.Cookies;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x000189F0 File Offset: 0x00016BF0
		public override NameValueCollection Headers
		{
			get
			{
				return this._context.Request.Headers;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x00018A14 File Offset: 0x00016C14
		public override string Host
		{
			get
			{
				return this._context.Request.UserHostName;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x00018A38 File Offset: 0x00016C38
		public override bool IsAuthenticated
		{
			get
			{
				return this._context.Request.IsAuthenticated;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x00018A5C File Offset: 0x00016C5C
		public override bool IsLocal
		{
			get
			{
				return this._context.Request.IsLocal;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x00018A80 File Offset: 0x00016C80
		public override bool IsSecureConnection
		{
			get
			{
				return this._context.Request.IsSecureConnection;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x00018AA4 File Offset: 0x00016CA4
		public override bool IsWebSocketRequest
		{
			get
			{
				return this._context.Request.IsWebSocketRequest;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x00018AC8 File Offset: 0x00016CC8
		public override string Origin
		{
			get
			{
				return this._context.Request.Headers["Origin"];
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00018AF4 File Offset: 0x00016CF4
		public override NameValueCollection QueryString
		{
			get
			{
				return this._context.Request.QueryString;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x00018B18 File Offset: 0x00016D18
		public override Uri RequestUri
		{
			get
			{
				return this._context.Request.Url;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00018B3C File Offset: 0x00016D3C
		public override string SecWebSocketKey
		{
			get
			{
				return this._context.Request.Headers["Sec-WebSocket-Key"];
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x00018B68 File Offset: 0x00016D68
		public override IEnumerable<string> SecWebSocketProtocols
		{
			get
			{
				string val = this._context.Request.Headers["Sec-WebSocket-Protocol"];
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

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00018B88 File Offset: 0x00016D88
		public override string SecWebSocketVersion
		{
			get
			{
				return this._context.Request.Headers["Sec-WebSocket-Version"];
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x00018BB4 File Offset: 0x00016DB4
		public override IPEndPoint ServerEndPoint
		{
			get
			{
				return this._context.Request.LocalEndPoint;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00018BD8 File Offset: 0x00016DD8
		public override IPrincipal User
		{
			get
			{
				return this._context.User;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x00018BF8 File Offset: 0x00016DF8
		public override IPEndPoint UserEndPoint
		{
			get
			{
				return this._context.Request.RemoteEndPoint;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x00018C1C File Offset: 0x00016E1C
		public override WebSocket WebSocket
		{
			get
			{
				return this._websocket;
			}
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00018C34 File Offset: 0x00016E34
		internal void Close()
		{
			this._context.Connection.Close(true);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00018C49 File Offset: 0x00016E49
		internal void Close(HttpStatusCode code)
		{
			this._context.Response.StatusCode = (int)code;
			this._context.Response.Close();
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00018C70 File Offset: 0x00016E70
		public override string ToString()
		{
			return this._context.Request.ToString();
		}

		// Token: 0x04000206 RID: 518
		private HttpListenerContext _context;

		// Token: 0x04000207 RID: 519
		private WebSocket _websocket;

		// Token: 0x02000070 RID: 112
		[CompilerGenerated]
		private sealed class <get_SecWebSocketProtocols>d__30 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IDisposable, IEnumerator
		{
			// Token: 0x060005C3 RID: 1475 RVA: 0x0001EEF0 File Offset: 0x0001D0F0
			[DebuggerHidden]
			public <get_SecWebSocketProtocols>d__30(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x060005C4 RID: 1476 RVA: 0x0001DF38 File Offset: 0x0001C138
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060005C5 RID: 1477 RVA: 0x0001EF10 File Offset: 0x0001D110
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
					val = this._context.Request.Headers["Sec-WebSocket-Protocol"];
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
					goto IL_FF;
				}
				IL_F1:
				i++;
				IL_FF:
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
					goto IL_F1;
				}
				this.<>2__current = protocol;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170001B0 RID: 432
			// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0001F037 File Offset: 0x0001D237
			string IEnumerator<string>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060005C7 RID: 1479 RVA: 0x0001E136 File Offset: 0x0001C336
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001B1 RID: 433
			// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0001F037 File Offset: 0x0001D237
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060005C9 RID: 1481 RVA: 0x0001F040 File Offset: 0x0001D240
			[DebuggerHidden]
			IEnumerator<string> IEnumerable<string>.GetEnumerator()
			{
				HttpListenerWebSocketContext.<get_SecWebSocketProtocols>d__30 <get_SecWebSocketProtocols>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<get_SecWebSocketProtocols>d__ = this;
				}
				else
				{
					<get_SecWebSocketProtocols>d__ = new HttpListenerWebSocketContext.<get_SecWebSocketProtocols>d__30(0);
					<get_SecWebSocketProtocols>d__.<>4__this = this;
				}
				return <get_SecWebSocketProtocols>d__;
			}

			// Token: 0x060005CA RID: 1482 RVA: 0x0001F088 File Offset: 0x0001D288
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.String>.GetEnumerator();
			}

			// Token: 0x040002DF RID: 735
			private int <>1__state;

			// Token: 0x040002E0 RID: 736
			private string <>2__current;

			// Token: 0x040002E1 RID: 737
			private int <>l__initialThreadId;

			// Token: 0x040002E2 RID: 738
			public HttpListenerWebSocketContext <>4__this;

			// Token: 0x040002E3 RID: 739
			private string <val>5__1;

			// Token: 0x040002E4 RID: 740
			private string[] <>s__2;

			// Token: 0x040002E5 RID: 741
			private int <>s__3;

			// Token: 0x040002E6 RID: 742
			private string <elm>5__4;

			// Token: 0x040002E7 RID: 743
			private string <protocol>5__5;
		}
	}
}
