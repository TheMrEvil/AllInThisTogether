using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000637 RID: 1591
	internal class ServerObjectTerminatorSink : IMessageSink
	{
		// Token: 0x06003C12 RID: 15378 RVA: 0x000D0DCE File Offset: 0x000CEFCE
		public ServerObjectTerminatorSink(IMessageSink nextSink)
		{
			this._nextSink = nextSink;
		}

		// Token: 0x06003C13 RID: 15379 RVA: 0x000D0DE0 File Offset: 0x000CEFE0
		public IMessage SyncProcessMessage(IMessage msg)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			serverIdentity.NotifyServerDynamicSinks(true, msg, false, false);
			IMessage result = this._nextSink.SyncProcessMessage(msg);
			serverIdentity.NotifyServerDynamicSinks(false, msg, false, false);
			return result;
		}

		// Token: 0x06003C14 RID: 15380 RVA: 0x000D0E1C File Offset: 0x000CF01C
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			if (serverIdentity.HasServerDynamicSinks)
			{
				serverIdentity.NotifyServerDynamicSinks(true, msg, false, true);
				if (replySink != null)
				{
					replySink = new ServerObjectReplySink(serverIdentity, replySink);
				}
			}
			IMessageCtrl result = this._nextSink.AsyncProcessMessage(msg, replySink);
			if (replySink == null)
			{
				serverIdentity.NotifyServerDynamicSinks(false, msg, true, true);
			}
			return result;
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06003C15 RID: 15381 RVA: 0x000D0E6D File Offset: 0x000CF06D
		public IMessageSink NextSink
		{
			get
			{
				return this._nextSink;
			}
		}

		// Token: 0x040026E9 RID: 9961
		private IMessageSink _nextSink;
	}
}
