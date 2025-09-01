using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000693 RID: 1683
	internal class HttpStreamAsyncResult : IAsyncResult
	{
		// Token: 0x06003569 RID: 13673 RVA: 0x000BABAA File Offset: 0x000B8DAA
		public void Complete(Exception e)
		{
			this.Error = e;
			this.Complete();
		}

		// Token: 0x0600356A RID: 13674 RVA: 0x000BABBC File Offset: 0x000B8DBC
		public void Complete()
		{
			object obj = this.locker;
			lock (obj)
			{
				if (!this.completed)
				{
					this.completed = true;
					if (this.handle != null)
					{
						this.handle.Set();
					}
					if (this.Callback != null)
					{
						this.Callback.BeginInvoke(this, null, null);
					}
				}
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x0600356B RID: 13675 RVA: 0x000BAC34 File Offset: 0x000B8E34
		public object AsyncState
		{
			get
			{
				return this.State;
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x0600356C RID: 13676 RVA: 0x000BAC3C File Offset: 0x000B8E3C
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				object obj = this.locker;
				lock (obj)
				{
					if (this.handle == null)
					{
						this.handle = new ManualResetEvent(this.completed);
					}
				}
				return this.handle;
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x0600356D RID: 13677 RVA: 0x000BAC98 File Offset: 0x000B8E98
		public bool CompletedSynchronously
		{
			get
			{
				return this.SynchRead == this.Count;
			}
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x0600356E RID: 13678 RVA: 0x000BACA8 File Offset: 0x000B8EA8
		public bool IsCompleted
		{
			get
			{
				object obj = this.locker;
				bool result;
				lock (obj)
				{
					result = this.completed;
				}
				return result;
			}
		}

		// Token: 0x0600356F RID: 13679 RVA: 0x000BACEC File Offset: 0x000B8EEC
		public HttpStreamAsyncResult()
		{
		}

		// Token: 0x04001F11 RID: 7953
		private object locker = new object();

		// Token: 0x04001F12 RID: 7954
		private ManualResetEvent handle;

		// Token: 0x04001F13 RID: 7955
		private bool completed;

		// Token: 0x04001F14 RID: 7956
		internal byte[] Buffer;

		// Token: 0x04001F15 RID: 7957
		internal int Offset;

		// Token: 0x04001F16 RID: 7958
		internal int Count;

		// Token: 0x04001F17 RID: 7959
		internal AsyncCallback Callback;

		// Token: 0x04001F18 RID: 7960
		internal object State;

		// Token: 0x04001F19 RID: 7961
		internal int SynchRead;

		// Token: 0x04001F1A RID: 7962
		internal Exception Error;
	}
}
