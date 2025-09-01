using System;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x02000295 RID: 661
	internal class SNIMarsQueuedPacket
	{
		// Token: 0x06001E78 RID: 7800 RVA: 0x000903B0 File Offset: 0x0008E5B0
		public SNIMarsQueuedPacket(SNIPacket packet, SNIAsyncCallback callback)
		{
			this._packet = packet;
			this._callback = callback;
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001E79 RID: 7801 RVA: 0x000903C6 File Offset: 0x0008E5C6
		// (set) Token: 0x06001E7A RID: 7802 RVA: 0x000903CE File Offset: 0x0008E5CE
		public SNIPacket Packet
		{
			get
			{
				return this._packet;
			}
			set
			{
				this._packet = value;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001E7B RID: 7803 RVA: 0x000903D7 File Offset: 0x0008E5D7
		// (set) Token: 0x06001E7C RID: 7804 RVA: 0x000903DF File Offset: 0x0008E5DF
		public SNIAsyncCallback Callback
		{
			get
			{
				return this._callback;
			}
			set
			{
				this._callback = value;
			}
		}

		// Token: 0x04001522 RID: 5410
		private SNIPacket _packet;

		// Token: 0x04001523 RID: 5411
		private SNIAsyncCallback _callback;
	}
}
