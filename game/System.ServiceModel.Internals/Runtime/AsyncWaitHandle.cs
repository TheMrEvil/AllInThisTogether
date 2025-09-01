using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x0200000E RID: 14
	internal class AsyncWaitHandle
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002796 File Offset: 0x00000996
		public AsyncWaitHandle() : this(EventResetMode.AutoReset)
		{
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000279F File Offset: 0x0000099F
		public AsyncWaitHandle(EventResetMode resetMode)
		{
			this.resetMode = resetMode;
			this.syncObject = new object();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000027BC File Offset: 0x000009BC
		public bool WaitAsync(Action<object, TimeoutException> callback, object state, TimeSpan timeout)
		{
			if (!this.isSignaled || (this.isSignaled && this.resetMode == EventResetMode.AutoReset))
			{
				object obj = this.syncObject;
				lock (obj)
				{
					if (this.isSignaled && this.resetMode == EventResetMode.AutoReset)
					{
						this.isSignaled = false;
					}
					else if (!this.isSignaled)
					{
						AsyncWaitHandle.AsyncWaiter asyncWaiter = new AsyncWaitHandle.AsyncWaiter(this, callback, state);
						if (this.asyncWaiters == null)
						{
							this.asyncWaiters = new List<AsyncWaitHandle.AsyncWaiter>();
						}
						this.asyncWaiters.Add(asyncWaiter);
						if (timeout != TimeSpan.MaxValue)
						{
							if (AsyncWaitHandle.timerCompleteCallback == null)
							{
								AsyncWaitHandle.timerCompleteCallback = new Action<object>(AsyncWaitHandle.OnTimerComplete);
							}
							asyncWaiter.SetTimer(AsyncWaitHandle.timerCompleteCallback, asyncWaiter, timeout);
						}
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002898 File Offset: 0x00000A98
		private static void OnTimerComplete(object state)
		{
			AsyncWaitHandle.AsyncWaiter asyncWaiter = (AsyncWaitHandle.AsyncWaiter)state;
			AsyncWaitHandle parent = asyncWaiter.Parent;
			bool flag = false;
			object obj = parent.syncObject;
			lock (obj)
			{
				if (parent.asyncWaiters != null && parent.asyncWaiters.Remove(asyncWaiter))
				{
					asyncWaiter.TimedOut = true;
					flag = true;
				}
			}
			asyncWaiter.CancelTimer();
			if (flag)
			{
				asyncWaiter.Call();
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002914 File Offset: 0x00000B14
		public bool Wait(TimeSpan timeout)
		{
			if (!this.isSignaled || (this.isSignaled && this.resetMode == EventResetMode.AutoReset))
			{
				object obj = this.syncObject;
				lock (obj)
				{
					if (this.isSignaled && this.resetMode == EventResetMode.AutoReset)
					{
						this.isSignaled = false;
					}
					else if (!this.isSignaled)
					{
						bool flag2 = false;
						try
						{
							try
							{
							}
							finally
							{
								this.syncWaiterCount++;
								flag2 = true;
							}
							if (timeout == TimeSpan.MaxValue)
							{
								if (!Monitor.Wait(this.syncObject, -1))
								{
									return false;
								}
							}
							else if (!Monitor.Wait(this.syncObject, timeout))
							{
								return false;
							}
						}
						finally
						{
							if (flag2)
							{
								this.syncWaiterCount--;
							}
						}
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002A04 File Offset: 0x00000C04
		public void Set()
		{
			List<AsyncWaitHandle.AsyncWaiter> list = null;
			AsyncWaitHandle.AsyncWaiter asyncWaiter = null;
			if (!this.isSignaled)
			{
				object obj = this.syncObject;
				lock (obj)
				{
					if (!this.isSignaled)
					{
						if (this.resetMode == EventResetMode.ManualReset)
						{
							this.isSignaled = true;
							Monitor.PulseAll(this.syncObject);
							list = this.asyncWaiters;
							this.asyncWaiters = null;
						}
						else if (this.syncWaiterCount > 0)
						{
							Monitor.Pulse(this.syncObject);
						}
						else if (this.asyncWaiters != null && this.asyncWaiters.Count > 0)
						{
							asyncWaiter = this.asyncWaiters[0];
							this.asyncWaiters.RemoveAt(0);
						}
						else
						{
							this.isSignaled = true;
						}
					}
				}
			}
			if (list != null)
			{
				foreach (AsyncWaitHandle.AsyncWaiter asyncWaiter2 in list)
				{
					asyncWaiter2.CancelTimer();
					asyncWaiter2.Call();
				}
			}
			if (asyncWaiter != null)
			{
				asyncWaiter.CancelTimer();
				asyncWaiter.Call();
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B24 File Offset: 0x00000D24
		public void Reset()
		{
			this.isSignaled = false;
		}

		// Token: 0x04000052 RID: 82
		private static Action<object> timerCompleteCallback;

		// Token: 0x04000053 RID: 83
		private List<AsyncWaitHandle.AsyncWaiter> asyncWaiters;

		// Token: 0x04000054 RID: 84
		private bool isSignaled;

		// Token: 0x04000055 RID: 85
		private EventResetMode resetMode;

		// Token: 0x04000056 RID: 86
		private object syncObject;

		// Token: 0x04000057 RID: 87
		private int syncWaiterCount;

		// Token: 0x0200005D RID: 93
		private class AsyncWaiter : ActionItem
		{
			// Token: 0x06000356 RID: 854 RVA: 0x00010ED0 File Offset: 0x0000F0D0
			[SecuritySafeCritical]
			public AsyncWaiter(AsyncWaitHandle parent, Action<object, TimeoutException> callback, object state)
			{
				this.Parent = parent;
				this.callback = callback;
				this.state = state;
			}

			// Token: 0x1700007D RID: 125
			// (get) Token: 0x06000357 RID: 855 RVA: 0x00010EED File Offset: 0x0000F0ED
			// (set) Token: 0x06000358 RID: 856 RVA: 0x00010EF5 File Offset: 0x0000F0F5
			public AsyncWaitHandle Parent
			{
				[CompilerGenerated]
				get
				{
					return this.<Parent>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<Parent>k__BackingField = value;
				}
			}

			// Token: 0x1700007E RID: 126
			// (get) Token: 0x06000359 RID: 857 RVA: 0x00010EFE File Offset: 0x0000F0FE
			// (set) Token: 0x0600035A RID: 858 RVA: 0x00010F06 File Offset: 0x0000F106
			public bool TimedOut
			{
				[CompilerGenerated]
				get
				{
					return this.<TimedOut>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<TimedOut>k__BackingField = value;
				}
			}

			// Token: 0x0600035B RID: 859 RVA: 0x00010F0F File Offset: 0x0000F10F
			[SecuritySafeCritical]
			public void Call()
			{
				base.Schedule();
			}

			// Token: 0x0600035C RID: 860 RVA: 0x00010F17 File Offset: 0x0000F117
			[SecurityCritical]
			protected override void Invoke()
			{
				this.callback(this.state, this.TimedOut ? new TimeoutException(InternalSR.TimeoutOnOperation(this.originalTimeout)) : null);
			}

			// Token: 0x0600035D RID: 861 RVA: 0x00010F4A File Offset: 0x0000F14A
			public void SetTimer(Action<object> callback, object state, TimeSpan timeout)
			{
				if (this.timer != null)
				{
					throw Fx.Exception.AsError(new InvalidOperationException("Must Cancel Old Timer"));
				}
				this.originalTimeout = timeout;
				this.timer = new IOThreadTimer(callback, state, false);
				this.timer.Set(timeout);
			}

			// Token: 0x0600035E RID: 862 RVA: 0x00010F8A File Offset: 0x0000F18A
			public void CancelTimer()
			{
				if (this.timer != null)
				{
					this.timer.Cancel();
					this.timer = null;
				}
			}

			// Token: 0x04000216 RID: 534
			[SecurityCritical]
			private Action<object, TimeoutException> callback;

			// Token: 0x04000217 RID: 535
			[SecurityCritical]
			private object state;

			// Token: 0x04000218 RID: 536
			private IOThreadTimer timer;

			// Token: 0x04000219 RID: 537
			private TimeSpan originalTimeout;

			// Token: 0x0400021A RID: 538
			[CompilerGenerated]
			private AsyncWaitHandle <Parent>k__BackingField;

			// Token: 0x0400021B RID: 539
			[CompilerGenerated]
			private bool <TimedOut>k__BackingField;
		}
	}
}
