using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020005B1 RID: 1457
	internal class AsyncRequest
	{
		// Token: 0x0600386E RID: 14446 RVA: 0x000CA71F File Offset: 0x000C891F
		public AsyncRequest(IMessage msgRequest, IMessageSink replySink)
		{
			this.ReplySink = replySink;
			this.MsgRequest = msgRequest;
		}

		// Token: 0x040025E7 RID: 9703
		internal IMessageSink ReplySink;

		// Token: 0x040025E8 RID: 9704
		internal IMessage MsgRequest;
	}
}
