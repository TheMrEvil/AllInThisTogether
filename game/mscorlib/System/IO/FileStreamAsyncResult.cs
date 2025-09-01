using System;
using System.Threading;

namespace System.IO
{
	// Token: 0x02000B61 RID: 2913
	internal class FileStreamAsyncResult : IAsyncResult
	{
		// Token: 0x060069BD RID: 27069 RVA: 0x001696AF File Offset: 0x001678AF
		public FileStreamAsyncResult(AsyncCallback cb, object state)
		{
			this.state = state;
			this.realcb = cb;
			if (this.realcb != null)
			{
				this.cb = new AsyncCallback(FileStreamAsyncResult.CBWrapper);
			}
			this.wh = new ManualResetEvent(false);
		}

		// Token: 0x060069BE RID: 27070 RVA: 0x001696EB File Offset: 0x001678EB
		private static void CBWrapper(IAsyncResult ares)
		{
			((FileStreamAsyncResult)ares).realcb.BeginInvoke(ares, null, null);
		}

		// Token: 0x060069BF RID: 27071 RVA: 0x00169701 File Offset: 0x00167901
		public void SetComplete(Exception e)
		{
			this.exc = e;
			this.completed = true;
			this.wh.Set();
			if (this.cb != null)
			{
				this.cb(this);
			}
		}

		// Token: 0x060069C0 RID: 27072 RVA: 0x00169731 File Offset: 0x00167931
		public void SetComplete(Exception e, int nbytes)
		{
			this.BytesRead = nbytes;
			this.SetComplete(e);
		}

		// Token: 0x060069C1 RID: 27073 RVA: 0x00169741 File Offset: 0x00167941
		public void SetComplete(Exception e, int nbytes, bool synch)
		{
			this.completedSynch = synch;
			this.SetComplete(e, nbytes);
		}

		// Token: 0x1700123A RID: 4666
		// (get) Token: 0x060069C2 RID: 27074 RVA: 0x00169752 File Offset: 0x00167952
		public object AsyncState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x1700123B RID: 4667
		// (get) Token: 0x060069C3 RID: 27075 RVA: 0x0016975A File Offset: 0x0016795A
		public bool CompletedSynchronously
		{
			get
			{
				return this.completedSynch;
			}
		}

		// Token: 0x1700123C RID: 4668
		// (get) Token: 0x060069C4 RID: 27076 RVA: 0x00169762 File Offset: 0x00167962
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return this.wh;
			}
		}

		// Token: 0x1700123D RID: 4669
		// (get) Token: 0x060069C5 RID: 27077 RVA: 0x0016976A File Offset: 0x0016796A
		public bool IsCompleted
		{
			get
			{
				return this.completed;
			}
		}

		// Token: 0x1700123E RID: 4670
		// (get) Token: 0x060069C6 RID: 27078 RVA: 0x00169772 File Offset: 0x00167972
		public Exception Exception
		{
			get
			{
				return this.exc;
			}
		}

		// Token: 0x1700123F RID: 4671
		// (get) Token: 0x060069C7 RID: 27079 RVA: 0x0016977A File Offset: 0x0016797A
		// (set) Token: 0x060069C8 RID: 27080 RVA: 0x00169782 File Offset: 0x00167982
		public bool Done
		{
			get
			{
				return this.done;
			}
			set
			{
				this.done = value;
			}
		}

		// Token: 0x04003D40 RID: 15680
		private object state;

		// Token: 0x04003D41 RID: 15681
		private bool completed;

		// Token: 0x04003D42 RID: 15682
		private bool done;

		// Token: 0x04003D43 RID: 15683
		private Exception exc;

		// Token: 0x04003D44 RID: 15684
		private ManualResetEvent wh;

		// Token: 0x04003D45 RID: 15685
		private AsyncCallback cb;

		// Token: 0x04003D46 RID: 15686
		private bool completedSynch;

		// Token: 0x04003D47 RID: 15687
		public byte[] Buffer;

		// Token: 0x04003D48 RID: 15688
		public int Offset;

		// Token: 0x04003D49 RID: 15689
		public int Count;

		// Token: 0x04003D4A RID: 15690
		public int OriginalCount;

		// Token: 0x04003D4B RID: 15691
		public int BytesRead;

		// Token: 0x04003D4C RID: 15692
		private AsyncCallback realcb;
	}
}
