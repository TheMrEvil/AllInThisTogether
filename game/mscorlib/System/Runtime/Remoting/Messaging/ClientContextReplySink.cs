using System;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000612 RID: 1554
	internal class ClientContextReplySink : IMessageSink
	{
		// Token: 0x06003AB5 RID: 15029 RVA: 0x000CDE6B File Offset: 0x000CC06B
		public ClientContextReplySink(Context ctx, IMessageSink replySink)
		{
			this._replySink = replySink;
			this._context = ctx;
		}

		// Token: 0x06003AB6 RID: 15030 RVA: 0x000CDE81 File Offset: 0x000CC081
		public IMessage SyncProcessMessage(IMessage msg)
		{
			Context.NotifyGlobalDynamicSinks(false, msg, true, true);
			this._context.NotifyDynamicSinks(false, msg, true, true);
			return this._replySink.SyncProcessMessage(msg);
		}

		// Token: 0x06003AB7 RID: 15031 RVA: 0x000472C8 File Offset: 0x000454C8
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06003AB8 RID: 15032 RVA: 0x000CDEA7 File Offset: 0x000CC0A7
		public IMessageSink NextSink
		{
			get
			{
				return this._replySink;
			}
		}

		// Token: 0x04002682 RID: 9858
		private IMessageSink _replySink;

		// Token: 0x04002683 RID: 9859
		private Context _context;
	}
}
