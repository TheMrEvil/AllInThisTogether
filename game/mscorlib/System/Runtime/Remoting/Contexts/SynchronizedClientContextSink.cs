using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200059F RID: 1439
	internal class SynchronizedClientContextSink : IMessageSink
	{
		// Token: 0x060037F0 RID: 14320 RVA: 0x000C8FFE File Offset: 0x000C71FE
		public SynchronizedClientContextSink(IMessageSink next, SynchronizationAttribute att)
		{
			this._att = att;
			this._next = next;
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x060037F1 RID: 14321 RVA: 0x000C9014 File Offset: 0x000C7214
		public IMessageSink NextSink
		{
			get
			{
				return this._next;
			}
		}

		// Token: 0x060037F2 RID: 14322 RVA: 0x000C901C File Offset: 0x000C721C
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			if (this._att.IsReEntrant)
			{
				this._att.ReleaseLock();
				replySink = new SynchronizedContextReplySink(replySink, this._att, true);
			}
			return this._next.AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x060037F3 RID: 14323 RVA: 0x000C9054 File Offset: 0x000C7254
		public IMessage SyncProcessMessage(IMessage msg)
		{
			if (this._att.IsReEntrant)
			{
				this._att.ReleaseLock();
			}
			IMessage result;
			try
			{
				result = this._next.SyncProcessMessage(msg);
			}
			finally
			{
				if (this._att.IsReEntrant)
				{
					this._att.AcquireLock();
				}
			}
			return result;
		}

		// Token: 0x040025C0 RID: 9664
		private IMessageSink _next;

		// Token: 0x040025C1 RID: 9665
		private SynchronizationAttribute _att;
	}
}
