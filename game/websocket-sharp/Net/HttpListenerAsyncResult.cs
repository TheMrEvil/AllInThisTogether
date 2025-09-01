using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace WebSocketSharp.Net
{
	// Token: 0x0200003F RID: 63
	internal class HttpListenerAsyncResult : IAsyncResult
	{
		// Token: 0x06000411 RID: 1041 RVA: 0x0001872B File Offset: 0x0001692B
		internal HttpListenerAsyncResult(AsyncCallback callback, object state)
		{
			this._callback = callback;
			this._state = state;
			this._sync = new object();
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x00018750 File Offset: 0x00016950
		internal HttpListenerContext Context
		{
			get
			{
				bool flag = this._exception != null;
				if (flag)
				{
					throw this._exception;
				}
				return this._context;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0001877C File Offset: 0x0001697C
		// (set) Token: 0x06000414 RID: 1044 RVA: 0x00018794 File Offset: 0x00016994
		internal bool EndCalled
		{
			get
			{
				return this._endCalled;
			}
			set
			{
				this._endCalled = value;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x000187A0 File Offset: 0x000169A0
		internal object SyncRoot
		{
			get
			{
				return this._sync;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x000187B8 File Offset: 0x000169B8
		public object AsyncState
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x000187D0 File Offset: 0x000169D0
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				object sync = this._sync;
				WaitHandle waitHandle;
				lock (sync)
				{
					bool flag = this._waitHandle == null;
					if (flag)
					{
						this._waitHandle = new ManualResetEvent(this._completed);
					}
					waitHandle = this._waitHandle;
				}
				return waitHandle;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00018830 File Offset: 0x00016A30
		public bool CompletedSynchronously
		{
			get
			{
				return this._completedSynchronously;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x00018848 File Offset: 0x00016A48
		public bool IsCompleted
		{
			get
			{
				object sync = this._sync;
				bool completed;
				lock (sync)
				{
					completed = this._completed;
				}
				return completed;
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00018888 File Offset: 0x00016A88
		private void complete()
		{
			object sync = this._sync;
			lock (sync)
			{
				this._completed = true;
				bool flag = this._waitHandle != null;
				if (flag)
				{
					this._waitHandle.Set();
				}
			}
			bool flag2 = this._callback == null;
			if (!flag2)
			{
				ThreadPool.QueueUserWorkItem(delegate(object state)
				{
					try
					{
						this._callback(this);
					}
					catch
					{
					}
				}, null);
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00018904 File Offset: 0x00016B04
		internal void Complete(Exception exception)
		{
			this._exception = exception;
			this.complete();
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00018915 File Offset: 0x00016B15
		internal void Complete(HttpListenerContext context, bool completedSynchronously)
		{
			this._context = context;
			this._completedSynchronously = completedSynchronously;
			this.complete();
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00018930 File Offset: 0x00016B30
		[CompilerGenerated]
		private void <complete>b__25_0(object state)
		{
			try
			{
				this._callback(this);
			}
			catch
			{
			}
		}

		// Token: 0x040001AC RID: 428
		private AsyncCallback _callback;

		// Token: 0x040001AD RID: 429
		private bool _completed;

		// Token: 0x040001AE RID: 430
		private bool _completedSynchronously;

		// Token: 0x040001AF RID: 431
		private HttpListenerContext _context;

		// Token: 0x040001B0 RID: 432
		private bool _endCalled;

		// Token: 0x040001B1 RID: 433
		private Exception _exception;

		// Token: 0x040001B2 RID: 434
		private object _state;

		// Token: 0x040001B3 RID: 435
		private object _sync;

		// Token: 0x040001B4 RID: 436
		private ManualResetEvent _waitHandle;
	}
}
