using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Security.Principal;

namespace WebSocketSharp.Net.WebSockets
{
	// Token: 0x02000044 RID: 68
	public abstract class WebSocketContext
	{
		// Token: 0x0600044E RID: 1102 RVA: 0x000094E4 File Offset: 0x000076E4
		protected WebSocketContext()
		{
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600044F RID: 1103
		public abstract CookieCollection CookieCollection { get; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000450 RID: 1104
		public abstract NameValueCollection Headers { get; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000451 RID: 1105
		public abstract string Host { get; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000452 RID: 1106
		public abstract bool IsAuthenticated { get; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000453 RID: 1107
		public abstract bool IsLocal { get; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000454 RID: 1108
		public abstract bool IsSecureConnection { get; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000455 RID: 1109
		public abstract bool IsWebSocketRequest { get; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000456 RID: 1110
		public abstract string Origin { get; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000457 RID: 1111
		public abstract NameValueCollection QueryString { get; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000458 RID: 1112
		public abstract Uri RequestUri { get; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000459 RID: 1113
		public abstract string SecWebSocketKey { get; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600045A RID: 1114
		public abstract IEnumerable<string> SecWebSocketProtocols { get; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600045B RID: 1115
		public abstract string SecWebSocketVersion { get; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600045C RID: 1116
		public abstract IPEndPoint ServerEndPoint { get; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600045D RID: 1117
		public abstract IPrincipal User { get; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600045E RID: 1118
		public abstract IPEndPoint UserEndPoint { get; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600045F RID: 1119
		public abstract WebSocket WebSocket { get; }
	}
}
