using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200067D RID: 1661
	internal class DnsAsyncResult : IAsyncResult
	{
		// Token: 0x0600344A RID: 13386 RVA: 0x000B6372 File Offset: 0x000B4572
		public DnsAsyncResult(AsyncCallback cb, object state)
		{
			this.callback = cb;
			this.state = state;
		}

		// Token: 0x0600344B RID: 13387 RVA: 0x000B6388 File Offset: 0x000B4588
		public void SetCompleted(bool synch, IPHostEntry entry, Exception e)
		{
			this.synch = synch;
			this.entry = entry;
			this.exc = e;
			lock (this)
			{
				if (this.is_completed)
				{
					return;
				}
				this.is_completed = true;
				if (this.handle != null)
				{
					this.handle.Set();
				}
			}
			if (this.callback != null)
			{
				ThreadPool.QueueUserWorkItem(DnsAsyncResult.internal_cb, this);
			}
		}

		// Token: 0x0600344C RID: 13388 RVA: 0x000B640C File Offset: 0x000B460C
		public void SetCompleted(bool synch, Exception e)
		{
			this.SetCompleted(synch, null, e);
		}

		// Token: 0x0600344D RID: 13389 RVA: 0x000B6417 File Offset: 0x000B4617
		public void SetCompleted(bool synch, IPHostEntry entry)
		{
			this.SetCompleted(synch, entry, null);
		}

		// Token: 0x0600344E RID: 13390 RVA: 0x000B6424 File Offset: 0x000B4624
		private static void CB(object _this)
		{
			DnsAsyncResult dnsAsyncResult = (DnsAsyncResult)_this;
			dnsAsyncResult.callback(dnsAsyncResult);
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x0600344F RID: 13391 RVA: 0x000B6444 File Offset: 0x000B4644
		public object AsyncState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06003450 RID: 13392 RVA: 0x000B644C File Offset: 0x000B464C
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				lock (this)
				{
					if (this.handle == null)
					{
						this.handle = new ManualResetEvent(this.is_completed);
					}
				}
				return this.handle;
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06003451 RID: 13393 RVA: 0x000B64A0 File Offset: 0x000B46A0
		public Exception Exception
		{
			get
			{
				return this.exc;
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06003452 RID: 13394 RVA: 0x000B64A8 File Offset: 0x000B46A8
		public IPHostEntry HostEntry
		{
			get
			{
				return this.entry;
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06003453 RID: 13395 RVA: 0x000B64B0 File Offset: 0x000B46B0
		public bool CompletedSynchronously
		{
			get
			{
				return this.synch;
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06003454 RID: 13396 RVA: 0x000B64B8 File Offset: 0x000B46B8
		public bool IsCompleted
		{
			get
			{
				bool result;
				lock (this)
				{
					result = this.is_completed;
				}
				return result;
			}
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x000B64F8 File Offset: 0x000B46F8
		// Note: this type is marked as 'beforefieldinit'.
		static DnsAsyncResult()
		{
		}

		// Token: 0x04001E85 RID: 7813
		private static WaitCallback internal_cb = new WaitCallback(DnsAsyncResult.CB);

		// Token: 0x04001E86 RID: 7814
		private ManualResetEvent handle;

		// Token: 0x04001E87 RID: 7815
		private bool synch;

		// Token: 0x04001E88 RID: 7816
		private bool is_completed;

		// Token: 0x04001E89 RID: 7817
		private AsyncCallback callback;

		// Token: 0x04001E8A RID: 7818
		private object state;

		// Token: 0x04001E8B RID: 7819
		private IPHostEntry entry;

		// Token: 0x04001E8C RID: 7820
		private Exception exc;
	}
}
