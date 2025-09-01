using System;

namespace System.Net.WebSockets
{
	/// <summary>Contains the list of possible WebSocket errors.</summary>
	// Token: 0x020007F0 RID: 2032
	public enum WebSocketError
	{
		/// <summary>Indicates that there was no native error information for the exception.</summary>
		// Token: 0x04002741 RID: 10049
		Success,
		/// <summary>Indicates that a WebSocket frame with an unknown opcode was received.</summary>
		// Token: 0x04002742 RID: 10050
		InvalidMessageType,
		/// <summary>Indicates a general error.</summary>
		// Token: 0x04002743 RID: 10051
		Faulted,
		/// <summary>Indicates that an unknown native error occurred.</summary>
		// Token: 0x04002744 RID: 10052
		NativeError,
		/// <summary>Indicates that the incoming request was not a valid websocket request.</summary>
		// Token: 0x04002745 RID: 10053
		NotAWebSocket,
		/// <summary>Indicates that the client requested an unsupported version of the WebSocket protocol.</summary>
		// Token: 0x04002746 RID: 10054
		UnsupportedVersion,
		/// <summary>Indicates that the client requested an unsupported WebSocket subprotocol.</summary>
		// Token: 0x04002747 RID: 10055
		UnsupportedProtocol,
		/// <summary>Indicates an error occurred when parsing the HTTP headers during the opening handshake.</summary>
		// Token: 0x04002748 RID: 10056
		HeaderError,
		/// <summary>Indicates that the connection was terminated unexpectedly.</summary>
		// Token: 0x04002749 RID: 10057
		ConnectionClosedPrematurely,
		/// <summary>Indicates the WebSocket is an invalid state for the given operation (such as being closed or aborted).</summary>
		// Token: 0x0400274A RID: 10058
		InvalidState
	}
}
