using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000588 RID: 1416
	internal class LeaseSink : IMessageSink
	{
		// Token: 0x06003779 RID: 14201 RVA: 0x000C7FA3 File Offset: 0x000C61A3
		public LeaseSink(IMessageSink nextSink)
		{
			this._nextSink = nextSink;
		}

		// Token: 0x0600377A RID: 14202 RVA: 0x000C7FB2 File Offset: 0x000C61B2
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this.RenewLease(msg);
			return this._nextSink.SyncProcessMessage(msg);
		}

		// Token: 0x0600377B RID: 14203 RVA: 0x000C7FC7 File Offset: 0x000C61C7
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			this.RenewLease(msg);
			return this._nextSink.AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x0600377C RID: 14204 RVA: 0x000C7FE0 File Offset: 0x000C61E0
		private void RenewLease(IMessage msg)
		{
			ILease lease = ((ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg)).Lease;
			if (lease != null && lease.CurrentLeaseTime < lease.RenewOnCallTime)
			{
				lease.Renew(lease.RenewOnCallTime);
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x0600377D RID: 14205 RVA: 0x000C8021 File Offset: 0x000C6221
		public IMessageSink NextSink
		{
			get
			{
				return this._nextSink;
			}
		}

		// Token: 0x04002593 RID: 9619
		private IMessageSink _nextSink;
	}
}
