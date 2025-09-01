using System;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000611 RID: 1553
	internal class ClientContextTerminatorSink : IMessageSink
	{
		// Token: 0x06003AB1 RID: 15025 RVA: 0x000CDD68 File Offset: 0x000CBF68
		public ClientContextTerminatorSink(Context ctx)
		{
			this._context = ctx;
		}

		// Token: 0x06003AB2 RID: 15026 RVA: 0x000CDD78 File Offset: 0x000CBF78
		public IMessage SyncProcessMessage(IMessage msg)
		{
			Context.NotifyGlobalDynamicSinks(true, msg, true, false);
			this._context.NotifyDynamicSinks(true, msg, true, false);
			IMessage result;
			if (msg is IConstructionCallMessage)
			{
				result = ActivationServices.RemoteActivate((IConstructionCallMessage)msg);
			}
			else
			{
				result = RemotingServices.GetMessageTargetIdentity(msg).ChannelSink.SyncProcessMessage(msg);
			}
			Context.NotifyGlobalDynamicSinks(false, msg, true, false);
			this._context.NotifyDynamicSinks(false, msg, true, false);
			return result;
		}

		// Token: 0x06003AB3 RID: 15027 RVA: 0x000CDDE0 File Offset: 0x000CBFE0
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			if (this._context.HasDynamicSinks || Context.HasGlobalDynamicSinks)
			{
				Context.NotifyGlobalDynamicSinks(true, msg, true, true);
				this._context.NotifyDynamicSinks(true, msg, true, true);
				if (replySink != null)
				{
					replySink = new ClientContextReplySink(this._context, replySink);
				}
			}
			IMessageCtrl result = RemotingServices.GetMessageTargetIdentity(msg).ChannelSink.AsyncProcessMessage(msg, replySink);
			if (replySink == null && (this._context.HasDynamicSinks || Context.HasGlobalDynamicSinks))
			{
				Context.NotifyGlobalDynamicSinks(false, msg, true, true);
				this._context.NotifyDynamicSinks(false, msg, true, true);
			}
			return result;
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06003AB4 RID: 15028 RVA: 0x0000AF5E File Offset: 0x0000915E
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04002681 RID: 9857
		private Context _context;
	}
}
