using System;

namespace System.Net.Sockets
{
	// Token: 0x020007C4 RID: 1988
	internal enum SocketOperation
	{
		// Token: 0x04002607 RID: 9735
		Accept,
		// Token: 0x04002608 RID: 9736
		Connect,
		// Token: 0x04002609 RID: 9737
		Receive,
		// Token: 0x0400260A RID: 9738
		ReceiveFrom,
		// Token: 0x0400260B RID: 9739
		Send,
		// Token: 0x0400260C RID: 9740
		SendTo,
		// Token: 0x0400260D RID: 9741
		RecvJustCallback,
		// Token: 0x0400260E RID: 9742
		SendJustCallback,
		// Token: 0x0400260F RID: 9743
		Disconnect,
		// Token: 0x04002610 RID: 9744
		AcceptReceive,
		// Token: 0x04002611 RID: 9745
		ReceiveGeneric,
		// Token: 0x04002612 RID: 9746
		SendGeneric
	}
}
