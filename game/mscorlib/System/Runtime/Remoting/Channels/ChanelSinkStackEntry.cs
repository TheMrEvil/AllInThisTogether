using System;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020005AA RID: 1450
	internal class ChanelSinkStackEntry
	{
		// Token: 0x06003848 RID: 14408 RVA: 0x000CA197 File Offset: 0x000C8397
		public ChanelSinkStackEntry(IChannelSinkBase sink, object state, ChanelSinkStackEntry next)
		{
			this.Sink = sink;
			this.State = state;
			this.Next = next;
		}

		// Token: 0x040025D8 RID: 9688
		public IChannelSinkBase Sink;

		// Token: 0x040025D9 RID: 9689
		public object State;

		// Token: 0x040025DA RID: 9690
		public ChanelSinkStackEntry Next;
	}
}
