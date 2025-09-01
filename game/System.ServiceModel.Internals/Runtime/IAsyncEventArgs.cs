using System;

namespace System.Runtime
{
	// Token: 0x0200001E RID: 30
	internal interface IAsyncEventArgs
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000BF RID: 191
		object AsyncState { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000C0 RID: 192
		Exception Exception { get; }
	}
}
