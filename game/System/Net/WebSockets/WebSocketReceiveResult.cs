using System;
using System.Runtime.CompilerServices;

namespace System.Net.WebSockets
{
	/// <summary>An instance of this class represents the result of performing a single ReceiveAsync operation on a WebSocket.</summary>
	// Token: 0x020007F3 RID: 2035
	public class WebSocketReceiveResult
	{
		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketReceiveResult" /> class.</summary>
		/// <param name="count">The number of bytes received.</param>
		/// <param name="messageType">The type of message that was received.</param>
		/// <param name="endOfMessage">Indicates whether this is the final message.</param>
		// Token: 0x060040DA RID: 16602 RVA: 0x000DF1B8 File Offset: 0x000DD3B8
		public WebSocketReceiveResult(int count, WebSocketMessageType messageType, bool endOfMessage) : this(count, messageType, endOfMessage, null, null)
		{
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketReceiveResult" /> class.</summary>
		/// <param name="count">The number of bytes received.</param>
		/// <param name="messageType">The type of message that was received.</param>
		/// <param name="endOfMessage">Indicates whether this is the final message.</param>
		/// <param name="closeStatus">Indicates the <see cref="T:System.Net.WebSockets.WebSocketCloseStatus" /> of the connection.</param>
		/// <param name="closeStatusDescription">The description of <paramref name="closeStatus" />.</param>
		// Token: 0x060040DB RID: 16603 RVA: 0x000DF1D8 File Offset: 0x000DD3D8
		public WebSocketReceiveResult(int count, WebSocketMessageType messageType, bool endOfMessage, WebSocketCloseStatus? closeStatus, string closeStatusDescription)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.Count = count;
			this.EndOfMessage = endOfMessage;
			this.MessageType = messageType;
			this.CloseStatus = closeStatus;
			this.CloseStatusDescription = closeStatusDescription;
		}

		/// <summary>Indicates the number of bytes that the WebSocket received.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.</returns>
		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x060040DC RID: 16604 RVA: 0x000DF214 File Offset: 0x000DD414
		public int Count
		{
			[CompilerGenerated]
			get
			{
				return this.<Count>k__BackingField;
			}
		}

		/// <summary>Indicates whether the message has been received completely.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.</returns>
		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x060040DD RID: 16605 RVA: 0x000DF21C File Offset: 0x000DD41C
		public bool EndOfMessage
		{
			[CompilerGenerated]
			get
			{
				return this.<EndOfMessage>k__BackingField;
			}
		}

		/// <summary>Indicates whether the current message is a UTF-8 message or a binary message.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocketMessageType" />.</returns>
		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x060040DE RID: 16606 RVA: 0x000DF224 File Offset: 0x000DD424
		public WebSocketMessageType MessageType
		{
			[CompilerGenerated]
			get
			{
				return this.<MessageType>k__BackingField;
			}
		}

		/// <summary>Indicates the reason why the remote endpoint initiated the close handshake.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocketCloseStatus" />.</returns>
		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x060040DF RID: 16607 RVA: 0x000DF22C File Offset: 0x000DD42C
		public WebSocketCloseStatus? CloseStatus
		{
			[CompilerGenerated]
			get
			{
				return this.<CloseStatus>k__BackingField;
			}
		}

		/// <summary>Returns the optional description that describes why the close handshake has been initiated by the remote endpoint.</summary>
		/// <returns>Returns <see cref="T:System.String" />.</returns>
		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x060040E0 RID: 16608 RVA: 0x000DF234 File Offset: 0x000DD434
		public string CloseStatusDescription
		{
			[CompilerGenerated]
			get
			{
				return this.<CloseStatusDescription>k__BackingField;
			}
		}

		// Token: 0x04002750 RID: 10064
		[CompilerGenerated]
		private readonly int <Count>k__BackingField;

		// Token: 0x04002751 RID: 10065
		[CompilerGenerated]
		private readonly bool <EndOfMessage>k__BackingField;

		// Token: 0x04002752 RID: 10066
		[CompilerGenerated]
		private readonly WebSocketMessageType <MessageType>k__BackingField;

		// Token: 0x04002753 RID: 10067
		[CompilerGenerated]
		private readonly WebSocketCloseStatus? <CloseStatus>k__BackingField;

		// Token: 0x04002754 RID: 10068
		[CompilerGenerated]
		private readonly string <CloseStatusDescription>k__BackingField;
	}
}
