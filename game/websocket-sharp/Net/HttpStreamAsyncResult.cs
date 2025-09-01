using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace WebSocketSharp.Net
{
	// Token: 0x02000026 RID: 38
	internal class HttpStreamAsyncResult : IAsyncResult
	{
		// Token: 0x060002DB RID: 731 RVA: 0x00011CE4 File Offset: 0x0000FEE4
		internal HttpStreamAsyncResult(AsyncCallback callback, object state)
		{
			this._callback = callback;
			this._state = state;
			this._sync = new object();
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002DC RID: 732 RVA: 0x00011D08 File Offset: 0x0000FF08
		// (set) Token: 0x060002DD RID: 733 RVA: 0x00011D20 File Offset: 0x0000FF20
		internal byte[] Buffer
		{
			get
			{
				return this._buffer;
			}
			set
			{
				this._buffer = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00011D2C File Offset: 0x0000FF2C
		// (set) Token: 0x060002DF RID: 735 RVA: 0x00011D44 File Offset: 0x0000FF44
		internal int Count
		{
			get
			{
				return this._count;
			}
			set
			{
				this._count = value;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00011D50 File Offset: 0x0000FF50
		internal Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00011D68 File Offset: 0x0000FF68
		internal bool HasException
		{
			get
			{
				return this._exception != null;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00011D84 File Offset: 0x0000FF84
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x00011D9C File Offset: 0x0000FF9C
		internal int Offset
		{
			get
			{
				return this._offset;
			}
			set
			{
				this._offset = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00011DA8 File Offset: 0x0000FFA8
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x00011DC0 File Offset: 0x0000FFC0
		internal int SyncRead
		{
			get
			{
				return this._syncRead;
			}
			set
			{
				this._syncRead = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00011DCC File Offset: 0x0000FFCC
		public object AsyncState
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00011DE4 File Offset: 0x0000FFE4
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

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00011E44 File Offset: 0x00010044
		public bool CompletedSynchronously
		{
			get
			{
				return this._syncRead == this._count;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00011E64 File Offset: 0x00010064
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

		// Token: 0x060002EA RID: 746 RVA: 0x00011EA4 File Offset: 0x000100A4
		internal void Complete()
		{
			object sync = this._sync;
			lock (sync)
			{
				bool completed = this._completed;
				if (!completed)
				{
					this._completed = true;
					bool flag = this._waitHandle != null;
					if (flag)
					{
						this._waitHandle.Set();
					}
					bool flag2 = this._callback != null;
					if (flag2)
					{
						this._callback.BeginInvoke(this, delegate(IAsyncResult ar)
						{
							this._callback.EndInvoke(ar);
						}, null);
					}
				}
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00011F30 File Offset: 0x00010130
		internal void Complete(Exception exception)
		{
			object sync = this._sync;
			lock (sync)
			{
				bool completed = this._completed;
				if (!completed)
				{
					this._completed = true;
					this._exception = exception;
					bool flag = this._waitHandle != null;
					if (flag)
					{
						this._waitHandle.Set();
					}
					bool flag2 = this._callback != null;
					if (flag2)
					{
						this._callback.BeginInvoke(this, delegate(IAsyncResult ar)
						{
							this._callback.EndInvoke(ar);
						}, null);
					}
				}
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00011FC4 File Offset: 0x000101C4
		[CompilerGenerated]
		private void <Complete>b__35_0(IAsyncResult ar)
		{
			this._callback.EndInvoke(ar);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00011FC4 File Offset: 0x000101C4
		[CompilerGenerated]
		private void <Complete>b__36_0(IAsyncResult ar)
		{
			this._callback.EndInvoke(ar);
		}

		// Token: 0x0400011B RID: 283
		private byte[] _buffer;

		// Token: 0x0400011C RID: 284
		private AsyncCallback _callback;

		// Token: 0x0400011D RID: 285
		private bool _completed;

		// Token: 0x0400011E RID: 286
		private int _count;

		// Token: 0x0400011F RID: 287
		private Exception _exception;

		// Token: 0x04000120 RID: 288
		private int _offset;

		// Token: 0x04000121 RID: 289
		private object _state;

		// Token: 0x04000122 RID: 290
		private object _sync;

		// Token: 0x04000123 RID: 291
		private int _syncRead;

		// Token: 0x04000124 RID: 292
		private ManualResetEvent _waitHandle;
	}
}
