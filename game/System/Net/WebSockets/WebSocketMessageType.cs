using System;

namespace System.Net.WebSockets
{
	/// <summary>Indicates the message type.</summary>
	// Token: 0x020007F2 RID: 2034
	public enum WebSocketMessageType
	{
		/// <summary>The message is clear text.</summary>
		// Token: 0x0400274D RID: 10061
		Text,
		/// <summary>The message is in binary format.</summary>
		// Token: 0x0400274E RID: 10062
		Binary,
		/// <summary>A receive has completed because a close message was received.</summary>
		// Token: 0x0400274F RID: 10063
		Close
	}
}
