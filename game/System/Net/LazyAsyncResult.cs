using System;
using System.Diagnostics;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200062C RID: 1580
	internal class LazyAsyncResult : IAsyncResult
	{
		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x060031D8 RID: 12760 RVA: 0x000AD1C8 File Offset: 0x000AB3C8
		private static LazyAsyncResult.ThreadContext CurrentThreadContext
		{
			get
			{
				LazyAsyncResult.ThreadContext threadContext = LazyAsyncResult.t_ThreadContext;
				if (threadContext == null)
				{
					threadContext = new LazyAsyncResult.ThreadContext();
					LazyAsyncResult.t_ThreadContext = threadContext;
				}
				return threadContext;
			}
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x000AD1EB File Offset: 0x000AB3EB
		internal LazyAsyncResult(object myObject, object myState, AsyncCallback myCallBack)
		{
			this.m_AsyncObject = myObject;
			this.m_AsyncState = myState;
			this.m_AsyncCallback = myCallBack;
			this.m_Result = DBNull.Value;
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x000AD213 File Offset: 0x000AB413
		internal LazyAsyncResult(object myObject, object myState, AsyncCallback myCallBack, object result)
		{
			this.m_AsyncObject = myObject;
			this.m_AsyncState = myState;
			this.m_AsyncCallback = myCallBack;
			this.m_Result = result;
			this.m_IntCompleted = 1;
			if (this.m_AsyncCallback != null)
			{
				this.m_AsyncCallback(this);
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x060031DB RID: 12763 RVA: 0x000AD253 File Offset: 0x000AB453
		internal object AsyncObject
		{
			get
			{
				return this.m_AsyncObject;
			}
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x060031DC RID: 12764 RVA: 0x000AD25B File Offset: 0x000AB45B
		public object AsyncState
		{
			get
			{
				return this.m_AsyncState;
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x060031DD RID: 12765 RVA: 0x000AD263 File Offset: 0x000AB463
		// (set) Token: 0x060031DE RID: 12766 RVA: 0x000AD26B File Offset: 0x000AB46B
		protected AsyncCallback AsyncCallback
		{
			get
			{
				return this.m_AsyncCallback;
			}
			set
			{
				this.m_AsyncCallback = value;
			}
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x060031DF RID: 12767 RVA: 0x000AD274 File Offset: 0x000AB474
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				this.m_UserEvent = true;
				if (this.m_IntCompleted == 0)
				{
					Interlocked.CompareExchange(ref this.m_IntCompleted, int.MinValue, 0);
				}
				ManualResetEvent manualResetEvent = (ManualResetEvent)this.m_Event;
				while (manualResetEvent == null)
				{
					this.LazilyCreateEvent(out manualResetEvent);
				}
				return manualResetEvent;
			}
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x000AD2C0 File Offset: 0x000AB4C0
		private bool LazilyCreateEvent(out ManualResetEvent waitHandle)
		{
			waitHandle = new ManualResetEvent(false);
			bool result;
			try
			{
				if (Interlocked.CompareExchange(ref this.m_Event, waitHandle, null) == null)
				{
					if (this.InternalPeekCompleted)
					{
						waitHandle.Set();
					}
					result = true;
				}
				else
				{
					waitHandle.Close();
					waitHandle = (ManualResetEvent)this.m_Event;
					result = false;
				}
			}
			catch
			{
				this.m_Event = null;
				if (waitHandle != null)
				{
					waitHandle.Close();
				}
				throw;
			}
			return result;
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("DEBUG")]
		protected void DebugProtectState(bool protect)
		{
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x060031E2 RID: 12770 RVA: 0x000AD338 File Offset: 0x000AB538
		public bool CompletedSynchronously
		{
			get
			{
				int num = this.m_IntCompleted;
				if (num == 0)
				{
					num = Interlocked.CompareExchange(ref this.m_IntCompleted, int.MinValue, 0);
				}
				return num > 0;
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x060031E3 RID: 12771 RVA: 0x000AD368 File Offset: 0x000AB568
		public bool IsCompleted
		{
			get
			{
				int num = this.m_IntCompleted;
				if (num == 0)
				{
					num = Interlocked.CompareExchange(ref this.m_IntCompleted, int.MinValue, 0);
				}
				return (num & int.MaxValue) != 0;
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x060031E4 RID: 12772 RVA: 0x000AD39B File Offset: 0x000AB59B
		internal bool InternalPeekCompleted
		{
			get
			{
				return (this.m_IntCompleted & int.MaxValue) != 0;
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x060031E5 RID: 12773 RVA: 0x000AD3AC File Offset: 0x000AB5AC
		// (set) Token: 0x060031E6 RID: 12774 RVA: 0x000AD3C3 File Offset: 0x000AB5C3
		internal object Result
		{
			get
			{
				if (this.m_Result != DBNull.Value)
				{
					return this.m_Result;
				}
				return null;
			}
			set
			{
				this.m_Result = value;
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x060031E7 RID: 12775 RVA: 0x000AD3CC File Offset: 0x000AB5CC
		// (set) Token: 0x060031E8 RID: 12776 RVA: 0x000AD3D4 File Offset: 0x000AB5D4
		internal bool EndCalled
		{
			get
			{
				return this.m_EndCalled;
			}
			set
			{
				this.m_EndCalled = value;
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x060031E9 RID: 12777 RVA: 0x000AD3DD File Offset: 0x000AB5DD
		// (set) Token: 0x060031EA RID: 12778 RVA: 0x000AD3E5 File Offset: 0x000AB5E5
		internal int ErrorCode
		{
			get
			{
				return this.m_ErrorCode;
			}
			set
			{
				this.m_ErrorCode = value;
			}
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x000AD3F0 File Offset: 0x000AB5F0
		protected void ProtectedInvokeCallback(object result, IntPtr userToken)
		{
			if (result == DBNull.Value)
			{
				throw new ArgumentNullException("result");
			}
			if ((this.m_IntCompleted & 2147483647) == 0 && (Interlocked.Increment(ref this.m_IntCompleted) & 2147483647) == 1)
			{
				if (this.m_Result == DBNull.Value)
				{
					this.m_Result = result;
				}
				ManualResetEvent manualResetEvent = (ManualResetEvent)this.m_Event;
				if (manualResetEvent != null)
				{
					try
					{
						manualResetEvent.Set();
					}
					catch (ObjectDisposedException)
					{
					}
				}
				this.Complete(userToken);
			}
		}

		// Token: 0x060031EC RID: 12780 RVA: 0x000AD478 File Offset: 0x000AB678
		internal void InvokeCallback(object result)
		{
			this.ProtectedInvokeCallback(result, IntPtr.Zero);
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x000AD486 File Offset: 0x000AB686
		internal void InvokeCallback()
		{
			this.ProtectedInvokeCallback(null, IntPtr.Zero);
		}

		// Token: 0x060031EE RID: 12782 RVA: 0x000AD494 File Offset: 0x000AB694
		protected virtual void Complete(IntPtr userToken)
		{
			bool flag = false;
			LazyAsyncResult.ThreadContext currentThreadContext = LazyAsyncResult.CurrentThreadContext;
			try
			{
				currentThreadContext.m_NestedIOCount++;
				if (this.m_AsyncCallback != null)
				{
					if (currentThreadContext.m_NestedIOCount >= 50)
					{
						ThreadPool.QueueUserWorkItem(new WaitCallback(this.WorkerThreadComplete));
						flag = true;
					}
					else
					{
						this.m_AsyncCallback(this);
					}
				}
			}
			finally
			{
				currentThreadContext.m_NestedIOCount--;
				if (!flag)
				{
					this.Cleanup();
				}
			}
		}

		// Token: 0x060031EF RID: 12783 RVA: 0x000AD518 File Offset: 0x000AB718
		private void WorkerThreadComplete(object state)
		{
			try
			{
				this.m_AsyncCallback(this);
			}
			finally
			{
				this.Cleanup();
			}
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void Cleanup()
		{
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x000AD54C File Offset: 0x000AB74C
		internal object InternalWaitForCompletion()
		{
			return this.WaitForCompletion(true);
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x000AD558 File Offset: 0x000AB758
		private object WaitForCompletion(bool snap)
		{
			ManualResetEvent manualResetEvent = null;
			bool flag = false;
			if (!(snap ? this.IsCompleted : this.InternalPeekCompleted))
			{
				manualResetEvent = (ManualResetEvent)this.m_Event;
				if (manualResetEvent == null)
				{
					flag = this.LazilyCreateEvent(out manualResetEvent);
				}
			}
			if (manualResetEvent == null)
			{
				goto IL_73;
			}
			try
			{
				manualResetEvent.WaitOne(-1, false);
				goto IL_73;
			}
			catch (ObjectDisposedException)
			{
				goto IL_73;
			}
			finally
			{
				if (flag && !this.m_UserEvent)
				{
					ManualResetEvent manualResetEvent2 = (ManualResetEvent)this.m_Event;
					this.m_Event = null;
					if (!this.m_UserEvent)
					{
						manualResetEvent2.Close();
					}
				}
			}
			IL_6D:
			Thread.SpinWait(1);
			IL_73:
			if (this.m_Result != DBNull.Value)
			{
				return this.m_Result;
			}
			goto IL_6D;
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x000AD608 File Offset: 0x000AB808
		internal void InternalCleanup()
		{
			if ((this.m_IntCompleted & 2147483647) == 0 && (Interlocked.Increment(ref this.m_IntCompleted) & 2147483647) == 1)
			{
				this.m_Result = null;
				this.Cleanup();
			}
		}

		// Token: 0x04001D2E RID: 7470
		private const int c_HighBit = -2147483648;

		// Token: 0x04001D2F RID: 7471
		private const int c_ForceAsyncCount = 50;

		// Token: 0x04001D30 RID: 7472
		[ThreadStatic]
		private static LazyAsyncResult.ThreadContext t_ThreadContext;

		// Token: 0x04001D31 RID: 7473
		private object m_AsyncObject;

		// Token: 0x04001D32 RID: 7474
		private object m_AsyncState;

		// Token: 0x04001D33 RID: 7475
		private AsyncCallback m_AsyncCallback;

		// Token: 0x04001D34 RID: 7476
		private object m_Result;

		// Token: 0x04001D35 RID: 7477
		private int m_ErrorCode;

		// Token: 0x04001D36 RID: 7478
		private int m_IntCompleted;

		// Token: 0x04001D37 RID: 7479
		private bool m_EndCalled;

		// Token: 0x04001D38 RID: 7480
		private bool m_UserEvent;

		// Token: 0x04001D39 RID: 7481
		private object m_Event;

		// Token: 0x0200062D RID: 1581
		private class ThreadContext
		{
			// Token: 0x060031F4 RID: 12788 RVA: 0x0000219B File Offset: 0x0000039B
			public ThreadContext()
			{
			}

			// Token: 0x04001D3A RID: 7482
			internal int m_NestedIOCount;
		}
	}
}
