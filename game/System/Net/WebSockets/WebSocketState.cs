using System;

namespace System.Net.WebSockets
{
	/// <summary>Defines the different states a WebSockets instance can be in.</summary>
	// Token: 0x020007F4 RID: 2036
	public enum WebSocketState
	{
		/// <summary>Reserved for future use.</summary>
		// Token: 0x04002756 RID: 10070
		None,
		/// <summary>The connection is negotiating the handshake with the remote endpoint.</summary>
		// Token: 0x04002757 RID: 10071
		Connecting,
		/// <summary>The initial state after the HTTP handshake has been completed.</summary>
		// Token: 0x04002758 RID: 10072
		Open,
		/// <summary>A close message was sent to the remote endpoint.</summary>
		// Token: 0x04002759 RID: 10073
		CloseSent,
		/// <summary>A close message was received from the remote endpoint.</summary>
		// Token: 0x0400275A RID: 10074
		CloseReceived,
		/// <summary>Indicates the WebSocket close handshake completed gracefully.</summary>
		// Token: 0x0400275B RID: 10075
		Closed,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x0400275C RID: 10076
		Aborted
	}
}
