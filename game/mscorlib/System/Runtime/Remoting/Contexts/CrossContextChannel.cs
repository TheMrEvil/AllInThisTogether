using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000591 RID: 1425
	internal class CrossContextChannel : IMessageSink
	{
		// Token: 0x060037C3 RID: 14275 RVA: 0x000C8B14 File Offset: 0x000C6D14
		public IMessage SyncProcessMessage(IMessage msg)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			Context context = null;
			if (Thread.CurrentContext != serverIdentity.Context)
			{
				context = Context.SwitchToContext(serverIdentity.Context);
			}
			IMessage result;
			try
			{
				Context.NotifyGlobalDynamicSinks(true, msg, false, false);
				Thread.CurrentContext.NotifyDynamicSinks(true, msg, false, false);
				result = serverIdentity.Context.GetServerContextSinkChain().SyncProcessMessage(msg);
				Context.NotifyGlobalDynamicSinks(false, msg, false, false);
				Thread.CurrentContext.NotifyDynamicSinks(false, msg, false, false);
			}
			catch (Exception e)
			{
				result = new ReturnMessage(e, (IMethodCallMessage)msg);
			}
			finally
			{
				if (context != null)
				{
					Context.SwitchToContext(context);
				}
			}
			return result;
		}

		// Token: 0x060037C4 RID: 14276 RVA: 0x000C8BC4 File Offset: 0x000C6DC4
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			Context context = null;
			if (Thread.CurrentContext != serverIdentity.Context)
			{
				context = Context.SwitchToContext(serverIdentity.Context);
			}
			IMessageCtrl result;
			try
			{
				Context.NotifyGlobalDynamicSinks(true, msg, false, true);
				Thread.CurrentContext.NotifyDynamicSinks(true, msg, false, false);
				if (replySink != null)
				{
					replySink = new CrossContextChannel.ContextRestoreSink(replySink, context, msg);
				}
				IMessageCtrl messageCtrl = serverIdentity.AsyncObjectProcessMessage(msg, replySink);
				if (replySink == null)
				{
					Context.NotifyGlobalDynamicSinks(false, msg, false, false);
					Thread.CurrentContext.NotifyDynamicSinks(false, msg, false, false);
				}
				result = messageCtrl;
			}
			catch (Exception e)
			{
				if (replySink != null)
				{
					replySink.SyncProcessMessage(new ReturnMessage(e, (IMethodCallMessage)msg));
				}
				result = null;
			}
			finally
			{
				if (context != null)
				{
					Context.SwitchToContext(context);
				}
			}
			return result;
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x060037C5 RID: 14277 RVA: 0x0000AF5E File Offset: 0x0000915E
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060037C6 RID: 14278 RVA: 0x0000259F File Offset: 0x0000079F
		public CrossContextChannel()
		{
		}

		// Token: 0x02000592 RID: 1426
		private class ContextRestoreSink : IMessageSink
		{
			// Token: 0x060037C7 RID: 14279 RVA: 0x000C8C88 File Offset: 0x000C6E88
			public ContextRestoreSink(IMessageSink next, Context context, IMessage call)
			{
				this._next = next;
				this._context = context;
				this._call = call;
			}

			// Token: 0x060037C8 RID: 14280 RVA: 0x000C8CA8 File Offset: 0x000C6EA8
			public IMessage SyncProcessMessage(IMessage msg)
			{
				IMessage result;
				try
				{
					Context.NotifyGlobalDynamicSinks(false, msg, false, false);
					Thread.CurrentContext.NotifyDynamicSinks(false, msg, false, false);
					result = this._next.SyncProcessMessage(msg);
				}
				catch (Exception e)
				{
					result = new ReturnMessage(e, (IMethodCallMessage)this._call);
				}
				finally
				{
					if (this._context != null)
					{
						Context.SwitchToContext(this._context);
					}
				}
				return result;
			}

			// Token: 0x060037C9 RID: 14281 RVA: 0x000472C8 File Offset: 0x000454C8
			public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
			{
				throw new NotSupportedException();
			}

			// Token: 0x170007D1 RID: 2001
			// (get) Token: 0x060037CA RID: 14282 RVA: 0x000C8D20 File Offset: 0x000C6F20
			public IMessageSink NextSink
			{
				get
				{
					return this._next;
				}
			}

			// Token: 0x040025B4 RID: 9652
			private IMessageSink _next;

			// Token: 0x040025B5 RID: 9653
			private Context _context;

			// Token: 0x040025B6 RID: 9654
			private IMessage _call;
		}
	}
}
