using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020005A9 RID: 1449
	internal class ExceptionFilterSink : IMessageSink
	{
		// Token: 0x06003844 RID: 14404 RVA: 0x000CA160 File Offset: 0x000C8360
		public ExceptionFilterSink(IMessage call, IMessageSink next)
		{
			this._call = call;
			this._next = next;
		}

		// Token: 0x06003845 RID: 14405 RVA: 0x000CA176 File Offset: 0x000C8376
		public IMessage SyncProcessMessage(IMessage msg)
		{
			return this._next.SyncProcessMessage(ChannelServices.CheckReturnMessage(this._call, msg));
		}

		// Token: 0x06003846 RID: 14406 RVA: 0x00084B75 File Offset: 0x00082D75
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06003847 RID: 14407 RVA: 0x000CA18F File Offset: 0x000C838F
		public IMessageSink NextSink
		{
			get
			{
				return this._next;
			}
		}

		// Token: 0x040025D6 RID: 9686
		private IMessageSink _next;

		// Token: 0x040025D7 RID: 9687
		private IMessage _call;
	}
}
