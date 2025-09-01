using System;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x02000290 RID: 656
	internal abstract class SNIHandle
	{
		// Token: 0x06001E3E RID: 7742
		public abstract void Dispose();

		// Token: 0x06001E3F RID: 7743
		public abstract void SetAsyncCallbacks(SNIAsyncCallback receiveCallback, SNIAsyncCallback sendCallback);

		// Token: 0x06001E40 RID: 7744
		public abstract void SetBufferSize(int bufferSize);

		// Token: 0x06001E41 RID: 7745
		public abstract uint Send(SNIPacket packet);

		// Token: 0x06001E42 RID: 7746
		public abstract uint SendAsync(SNIPacket packet, bool disposePacketAfterSendAsync, SNIAsyncCallback callback = null);

		// Token: 0x06001E43 RID: 7747
		public abstract uint Receive(out SNIPacket packet, int timeoutInMilliseconds);

		// Token: 0x06001E44 RID: 7748
		public abstract uint ReceiveAsync(ref SNIPacket packet);

		// Token: 0x06001E45 RID: 7749
		public abstract uint EnableSsl(uint options);

		// Token: 0x06001E46 RID: 7750
		public abstract void DisableSsl();

		// Token: 0x06001E47 RID: 7751
		public abstract uint CheckConnection();

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001E48 RID: 7752
		public abstract uint Status { get; }

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001E49 RID: 7753
		public abstract Guid ConnectionId { get; }

		// Token: 0x06001E4A RID: 7754 RVA: 0x00003D93 File Offset: 0x00001F93
		protected SNIHandle()
		{
		}
	}
}
