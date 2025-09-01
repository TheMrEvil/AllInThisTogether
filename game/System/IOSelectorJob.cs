using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	// Token: 0x02000171 RID: 369
	[StructLayout(LayoutKind.Sequential)]
	internal class IOSelectorJob : IThreadPoolWorkItem
	{
		// Token: 0x060009D4 RID: 2516 RVA: 0x0002B688 File Offset: 0x00029888
		public IOSelectorJob(IOOperation operation, IOAsyncCallback callback, IOAsyncResult state)
		{
			this.operation = operation;
			this.callback = callback;
			this.state = state;
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0002B6A5 File Offset: 0x000298A5
		void IThreadPoolWorkItem.ExecuteWorkItem()
		{
			this.callback(this.state);
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00003917 File Offset: 0x00001B17
		void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
		{
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0002B6B8 File Offset: 0x000298B8
		public void MarkDisposed()
		{
			this.state.CompleteDisposed();
		}

		// Token: 0x040006A9 RID: 1705
		private IOOperation operation;

		// Token: 0x040006AA RID: 1706
		private IOAsyncCallback callback;

		// Token: 0x040006AB RID: 1707
		private IOAsyncResult state;
	}
}
