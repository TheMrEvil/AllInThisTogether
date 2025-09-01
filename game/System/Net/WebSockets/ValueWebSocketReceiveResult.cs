using System;

namespace System.Net.WebSockets
{
	// Token: 0x020007EA RID: 2026
	public readonly struct ValueWebSocketReceiveResult
	{
		// Token: 0x06004097 RID: 16535 RVA: 0x000DE84E File Offset: 0x000DCA4E
		public ValueWebSocketReceiveResult(int count, WebSocketMessageType messageType, bool endOfMessage)
		{
			if (count < 0)
			{
				ValueWebSocketReceiveResult.ThrowCountOutOfRange();
			}
			if (messageType > WebSocketMessageType.Close)
			{
				ValueWebSocketReceiveResult.ThrowMessageTypeOutOfRange();
			}
			this._countAndEndOfMessage = (uint)(count | (endOfMessage ? int.MinValue : 0));
			this._messageType = messageType;
		}

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x06004098 RID: 16536 RVA: 0x000DE87C File Offset: 0x000DCA7C
		public int Count
		{
			get
			{
				return (int)(this._countAndEndOfMessage & 2147483647U);
			}
		}

		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x06004099 RID: 16537 RVA: 0x000DE88A File Offset: 0x000DCA8A
		public bool EndOfMessage
		{
			get
			{
				return (this._countAndEndOfMessage & 2147483648U) == 2147483648U;
			}
		}

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x0600409A RID: 16538 RVA: 0x000DE89F File Offset: 0x000DCA9F
		public WebSocketMessageType MessageType
		{
			get
			{
				return this._messageType;
			}
		}

		// Token: 0x0600409B RID: 16539 RVA: 0x000DE8A7 File Offset: 0x000DCAA7
		private static void ThrowCountOutOfRange()
		{
			throw new ArgumentOutOfRangeException("count");
		}

		// Token: 0x0600409C RID: 16540 RVA: 0x000DE8B3 File Offset: 0x000DCAB3
		private static void ThrowMessageTypeOutOfRange()
		{
			throw new ArgumentOutOfRangeException("messageType");
		}

		// Token: 0x04002723 RID: 10019
		private readonly uint _countAndEndOfMessage;

		// Token: 0x04002724 RID: 10020
		private readonly WebSocketMessageType _messageType;
	}
}
