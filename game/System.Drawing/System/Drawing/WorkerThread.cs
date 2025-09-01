using System;
using System.Threading;

namespace System.Drawing
{
	// Token: 0x02000081 RID: 129
	internal class WorkerThread
	{
		// Token: 0x0600062D RID: 1581 RVA: 0x00011F91 File Offset: 0x00010191
		public WorkerThread(EventHandler frmChgHandler, AnimateEventArgs aniEvtArgs, int[] delay)
		{
			this.frameChangeHandler = frmChgHandler;
			this.animateEventArgs = aniEvtArgs;
			this.delay = delay;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00011FB0 File Offset: 0x000101B0
		public void LoopHandler()
		{
			try
			{
				int num = 0;
				for (;;)
				{
					Thread.Sleep(this.delay[num++]);
					this.frameChangeHandler(null, this.animateEventArgs);
					if (num == this.delay.Length)
					{
						num = 0;
					}
				}
			}
			catch (ThreadAbortException)
			{
				Thread.ResetAbort();
			}
		}

		// Token: 0x040004D0 RID: 1232
		private EventHandler frameChangeHandler;

		// Token: 0x040004D1 RID: 1233
		private AnimateEventArgs animateEventArgs;

		// Token: 0x040004D2 RID: 1234
		private int[] delay;
	}
}
