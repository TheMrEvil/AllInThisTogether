using System;
using WebSocketSharp.Net.WebSockets;

namespace WebSocketSharp.Server
{
	// Token: 0x02000049 RID: 73
	public interface IWebSocketSession
	{
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060004E2 RID: 1250
		WebSocketState ConnectionState { get; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060004E3 RID: 1251
		WebSocketContext Context { get; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060004E4 RID: 1252
		string ID { get; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060004E5 RID: 1253
		string Protocol { get; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060004E6 RID: 1254
		DateTime StartTime { get; }
	}
}
