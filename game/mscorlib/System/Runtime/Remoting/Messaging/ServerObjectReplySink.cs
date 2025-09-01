using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000638 RID: 1592
	internal class ServerObjectReplySink : IMessageSink
	{
		// Token: 0x06003C16 RID: 15382 RVA: 0x000D0E75 File Offset: 0x000CF075
		public ServerObjectReplySink(ServerIdentity identity, IMessageSink replySink)
		{
			this._replySink = replySink;
			this._identity = identity;
		}

		// Token: 0x06003C17 RID: 15383 RVA: 0x000D0E8B File Offset: 0x000CF08B
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this._identity.NotifyServerDynamicSinks(false, msg, true, true);
			return this._replySink.SyncProcessMessage(msg);
		}

		// Token: 0x06003C18 RID: 15384 RVA: 0x000472C8 File Offset: 0x000454C8
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06003C19 RID: 15385 RVA: 0x000D0EA8 File Offset: 0x000CF0A8
		public IMessageSink NextSink
		{
			get
			{
				return this._replySink;
			}
		}

		// Token: 0x040026EA RID: 9962
		private IMessageSink _replySink;

		// Token: 0x040026EB RID: 9963
		private ServerIdentity _identity;
	}
}
