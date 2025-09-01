using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	// Token: 0x02000170 RID: 368
	[StructLayout(LayoutKind.Sequential)]
	internal abstract class IOAsyncResult : IAsyncResult
	{
		// Token: 0x060009C9 RID: 2505 RVA: 0x0000219B File Offset: 0x0000039B
		protected IOAsyncResult()
		{
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0002B567 File Offset: 0x00029767
		protected void Init(AsyncCallback async_callback, object async_state)
		{
			this.async_callback = async_callback;
			this.async_state = async_state;
			this.completed = false;
			this.completed_synchronously = false;
			if (this.wait_handle != null)
			{
				this.wait_handle.Reset();
			}
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0002B599 File Offset: 0x00029799
		protected IOAsyncResult(AsyncCallback async_callback, object async_state)
		{
			this.async_callback = async_callback;
			this.async_state = async_state;
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x0002B5AF File Offset: 0x000297AF
		public AsyncCallback AsyncCallback
		{
			get
			{
				return this.async_callback;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x0002B5B7 File Offset: 0x000297B7
		public object AsyncState
		{
			get
			{
				return this.async_state;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x0002B5C0 File Offset: 0x000297C0
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				WaitHandle result;
				lock (this)
				{
					if (this.wait_handle == null)
					{
						this.wait_handle = new ManualResetEvent(this.completed);
					}
					result = this.wait_handle;
				}
				return result;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x0002B618 File Offset: 0x00029818
		// (set) Token: 0x060009D0 RID: 2512 RVA: 0x0002B620 File Offset: 0x00029820
		public bool CompletedSynchronously
		{
			get
			{
				return this.completed_synchronously;
			}
			protected set
			{
				this.completed_synchronously = value;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x0002B629 File Offset: 0x00029829
		// (set) Token: 0x060009D2 RID: 2514 RVA: 0x0002B634 File Offset: 0x00029834
		public bool IsCompleted
		{
			get
			{
				return this.completed;
			}
			protected set
			{
				this.completed = value;
				lock (this)
				{
					if (value && this.wait_handle != null)
					{
						this.wait_handle.Set();
					}
				}
			}
		}

		// Token: 0x060009D3 RID: 2515
		internal abstract void CompleteDisposed();

		// Token: 0x040006A4 RID: 1700
		private AsyncCallback async_callback;

		// Token: 0x040006A5 RID: 1701
		private object async_state;

		// Token: 0x040006A6 RID: 1702
		private ManualResetEvent wait_handle;

		// Token: 0x040006A7 RID: 1703
		private bool completed_synchronously;

		// Token: 0x040006A8 RID: 1704
		private bool completed;
	}
}
